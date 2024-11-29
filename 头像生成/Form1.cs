using System;
using System.Drawing;
using System.Windows.Forms;
using 头像生成.Properties;

namespace 头像生成
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void butMake_Click(object sender, EventArgs e)
        {
            string name = textName.Text;
            pictureBox1.Image = MakeHead(name);
        }

        private Image MakeHead(string name)
        {
            using var backImage = Resources.无忧背景;
            var image = new Bitmap(400, 400);
            using var g = Graphics.FromImage(image);
            g.DrawImage(backImage, 0, 0, 400, 400);
            if (name.Length == 2)
            {
                string str1 = name.Substring(0, 1);
                string str2 = name.Substring(1,1);
                g.DrawString(str1, new Font("华文隶书", 72), Brushes.Black, 69, 84);
                g.DrawString(str2, new Font("华文隶书", 72), Brushes.Black, 123, 181);
            }
            else if (name.Length == 3)
            {
                string str1 = name.Substring(0, 1);
                string str2 = name.Substring(1,1);
                string str3 = name.Substring(2,1);
                g.DrawString(str1, new Font("华文隶书", 72), Brushes.Black,72, 69);
                g.DrawString(str2, new Font("华文隶书", 72), Brushes.Black, 72, 152);
                g.DrawString(str3, new Font("华文隶书", 72), Brushes.Black, 132, 225);
            }

            return image;
        }
    }
}