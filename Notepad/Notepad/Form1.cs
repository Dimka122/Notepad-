﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notepad
{
    
    public partial class Form1 : Form
    {
        public string filename;
        public bool isFileChanged;
        public Form1()
        {
            InitializeComponent();

            Init();
        }
        public void Init()
        {
            filename = "";
            isFileChanged = false;
        }
        public void CreateNewDocument(object sender,EventArgs e)
        {
            textBox1.Text = "";
            filename = "";
        }
        public void OpenFile(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    StreamReader sr=new StreamReader(openFileDialog1.FileName);
                    textBox1.Text=sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл");
                }
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
                StreamWriter sw=new StreamWriter(_filename);
                sw.Write(textBox1.Text);
                sw.Close();
                filename = _filename;
                isFileChanged = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл");
            }
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

            }
        }
    }
}
