using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                imagePictureBox.Image = Image.FromFile(open_dialog.FileName);
            }
        }

        private void savePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:{ 
                        imagePictureBox.Image.Save(save_dialog.FileName, ImageFormat.Png);
                        break;
                    }
                    case 1:{
                        imagePictureBox.Image.Save(save_dialog.FileName, ImageFormat.Bmp);
                        break;
                    }    
                }
            }
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)imagePictureBox.Image;

            message.Text = new Steg().extractText(bmp);
            
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)imagePictureBox.Image;

            if (message.Text.Equals("")==true ) { 
                MessageBox.Show("The text you want to hide can't be empty", "Warning");
            }
            bmp = new Steg().hideText(message.Text, bmp);
        }
    }
}
