using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 改变图片尺寸
{
    public partial class MainForm : Form
    {
        string _workPath;
        public MainForm()
        {
            InitializeComponent();
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = System.Environment.CurrentDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _workPath = dialog.SelectedPath;
                    labWorkPath.Text =
                        _workPath.Length < 30 ? _workPath : _workPath.Substring(0, 10) + "……" + _workPath.Substring(_workPath.Length - 10);
                }
            }
        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
            if (_workPath == null)
            {
                MessageBox.Show("先选择文件夹");
                return;
            }

            string outPath = Path.Combine(_workPath, "新文件");
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }

            try
            {
                double length = Convert.ToDouble(textLength.Text);
                double width = Convert.ToDouble(textWidth.Text);
                double height = Convert.ToDouble(textHeight.Text);

                int lengthPix = (int)(length * 118.11);
                int widthPix = (int)(width * 118.11);
                int heightPix = (int)(height * 118.11);

                string prefix = textPrefix.Text;

                ChangeSize($"1{prefix}主视图.jpg", lengthPix, heightPix, outPath);
                ChangeSize($"2{prefix}后视图.jpg", lengthPix, heightPix, outPath);
                ChangeSize($"3{prefix}左视图.jpg", widthPix, heightPix, outPath);
                ChangeSize($"4{prefix}右视图.jpg", widthPix, heightPix, outPath);
                ChangeSize($"5{prefix}俯视图.jpg", lengthPix, widthPix, outPath);
                ChangeSize($"6{prefix}仰视图.jpg", lengthPix, widthPix, outPath);

                MessageBox.Show("完成");
            }
            catch
            {
                MessageBox.Show("操作有误");
            }
        }

        private void ChangeSize(string fileName, int newWidth, int newHeight, string outPath)
        {
            string srcFile = Path.Combine(_workPath, fileName);
            using (Bitmap src = new Bitmap(srcFile), dest = new Bitmap(newWidth, newHeight))
            {
                using (Graphics g = Graphics.FromImage(dest))
                {
                    g.DrawImage(src, new Rectangle(0, 0, newWidth, newHeight));
                }

                string outFile = Path.Combine(outPath, fileName);
                dest.SetResolution(300, 300);
                dest.Save(outFile, ImageFormat.Jpeg);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // 如果有文件被拖入，改变拖放效果
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length != 1)
            {
                labWorkPath.Text = "拖入不是文件夹";
                return;
            }

            string file = files[0];
            if (!Directory.Exists(file))
            {
                labWorkPath.Text = "拖入不是文件夹";
                return;
            }

            _workPath = file;
            labWorkPath.Text =
                _workPath.Length < 30 ? _workPath : _workPath.Substring(0, 10) + "……" + _workPath.Substring(_workPath.Length - 10);
        }
    }
}
