using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notepad.Formss.Сlassss
{
    internal class Tab_Image:TabPage
    {
        public Tab_Image(string title, string PathToImage)
        {


            this.Text = title;
            this.Size = new Size(300, 300);
            Image i;
            if (File.Exists(PathToImage))
            {
                i = Image.FromFile(PathToImage);
            }
            else
            {
                i = Properties.Resources.pngwing_com__2_;
            }
            this.Controls.Add(new PictureBox() { Image = i, Size = new Size(100, 100), SizeMode = PictureBoxSizeMode.StretchImage });
            this.Controls.Add(new Label() { Size = new Size(100, 20), Location = new Point(0, 110), Text = PathToImage });
            this.Click += Tab_Image_Click;

        }
        public Tab_Image() : this("title", "")
        {

        }

        private void Tab_Image_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Text);
        }
    }

}

