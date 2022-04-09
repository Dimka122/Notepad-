using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
using static Notepad.Class1;
using System.Threading;
using Notepad.Formss;

namespace Notepad
{
    
    public partial class Form1 : Form
    {
        static SpeechSynthesizer synth;
        C_Singelton s1 = C_Singelton.GetInstance();
        private int value = 100;
        //private int rate;

        public int fontSize = 0;
        public System.Drawing.FontStyle fs = FontStyle.Regular;

        public string filename;
        public bool isFileChanged;

        public FontSettings fontSetts;
        public Form2 form2;
        public Form1()
        {
            InitializeComponent();

            Init();

            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SpeakCompleted += Synth_SpeakCompleted;
        }
        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            //synth.SpeakAsync(textBox1.Text);
            button1.Text = "Голос";
        }
        //private void TextBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    rate = Int32.Parse(textBox1.Text);
        //}

        public void Init()
        {
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
        }
        public void CreateNewDocument(object sender,EventArgs e)
        {
            SavethissavedFile();
            textBox1.Text = "";
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
        }
        public void OpenFile(object sender, EventArgs e)
        {
            SavethissavedFile();
            openFileDialog1.FileName = "";
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    StreamReader sr=new StreamReader(openFileDialog1.FileName);
                    textBox1.Text=sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                    isFileChanged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл");
                }
                UpdateTextWithTitle();
            }
        }
        public void SaveFile(string _filename)
        {
            if(_filename =="")
            {
                if(saveFileDialog1.ShowDialog()==DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw=new StreamWriter(_filename+".txt");
                sw.Write(textBox1.Text);
                sw.Close();
                filename = _filename;
                isFileChanged = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл");
            }
            UpdateTextWithTitle();
        }
        public void Save(object sender,EventArgs e)
        {
            SaveFile(filename);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }
        private void OnTextChanged(object sender, EventArgs e)
        {
            if(!isFileChanged)
            {
                this.Text = this.Text.Replace("*", " ");
                isFileChanged = true;
                this.Text = "*" + this.Text;
            }
        }
        public void UpdateTextWithTitle()
        {
            if (filename != "")
                this.Text = filename + "-Блокнот";
            else this.Text = filename + "Безымянный-Блокнот";
        }
        public void SavethissavedFile()
        {
            if(isFileChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if(result==DialogResult.Yes)
                {
                    SaveFile(filename);
                }
            }
        }
        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }
        public void OutText()
        {
            Clipboard.SetText(textBox1.Text.Substring(textBox1.SelectionStart, textBox1.SelectionLength));
            textBox1.Text=textBox1.Text.Remove(textBox1.SelectionStart,textBox1.SelectionLength);
        }
        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length + textBox1.SelectionStart);
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnOutClick(object sender, EventArgs e)
        {
            OutText();
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SavethissavedFile();
        }

        private void OnFontClick(object sender, EventArgs e)
        {
            fontSetts = new FontSettings();
            fontSetts.Show();
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if(fontSetts !=null)
            {
                fontSize = fontSetts.fontSize;
                fs = fontSetts.fs;
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize, fs);
                fontSetts.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            if (text.Trim().Length == 0)
            {
                MessageBox.Show("Не удается прочитать пустой контент!","Сообщение об ошибке");
                return;
            }

            if (button1.Text == "Голос")
            {

                synth = new SpeechSynthesizer();

                new Thread(Speak).Start();

                button1.Text = "Остановить";

            }
            //button1.Text == "Остановить"
            else
            {

                synth.SpeakAsyncCancelAll(); // Прекратить чтение

                button1.Text = "Голос";
            }

        }
        private void Speak()
        {

            //synth.Rate = rate;
            //synth.SelectVoice("Microsoft Lili "); // Установить диктор (китайский)
            //synth.SelectVoice("Microsoft Anna "); // Английский
            synth.Volume = value;
            synth.SpeakAsync(textBox1.Text); // Метод чтения речи
            synth.SpeakCompleted += Synth_SpeakCompleted; // Событие привязки
        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Show();
        }
    }
}
