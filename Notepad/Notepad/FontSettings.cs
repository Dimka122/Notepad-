using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FontSettings : Form
    {
        public FontSettings()
        {
            InitializeComponent();
            fontBox.SelectedItem = fontBox.Items[0];
            styleBox.SelectedItem = styleBox.Items[0];
        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(fontBox.SelectedItem.ToString()), ExampleText.Font.Style);
        }

        private void OnStyleChanged(object sender, EventArgs e)
        {
            switch (styleBox.SelectedItem.ToString())
            {
                case "Обычный":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(fontBox.SelectedItem.ToString()), FontStyle.Regular);
                    break;
                case "Курсив":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(fontBox.SelectedItem.ToString()), FontStyle.Italic);
                    break;
                case "Полужирный":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(fontBox.SelectedItem.ToString()), FontStyle.Bold);
                    break;
                case "Линия по середине":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(fontBox.SelectedItem.ToString()), FontStyle.Strikeout);
                    break;
                case "Подчеркивание":
                    ExampleText.Font = new Font(ExampleText.Font.FontFamily, int.Parse(fontBox.SelectedItem.ToString()), FontStyle.Underline);
                    break;

            }
        }
    }
}
