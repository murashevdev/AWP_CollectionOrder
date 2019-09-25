using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AWP_CollectionOrder
{
    public partial class PreviewDlg : Form
    {
        private bool buttondown = false;
        private Point saveloc;
        private Point ImageClientLocation = new Point(0,0);
        public Bitmap ImageSource;
        public Bitmap ImagePreview;
        public MemoryStream dataforprint = new MemoryStream();
        public MemoryStream dataforcard = new MemoryStream();
        
        public PreviewDlg()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            buttondown = true;
            saveloc = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (buttondown)
            {
                int sLeft = ImageClientLocation.X;
                int sTop = ImageClientLocation.Y;
                ImageClientLocation.X += (e.X - saveloc.X);
                ImageClientLocation.Y += (e.Y - saveloc.Y);
                //Полверки на выход отображения за границы изображения
                if (ImageClientLocation.X > 0)
                    ImageClientLocation.X = 0;
                if (ImageClientLocation.Y > 0)
                    ImageClientLocation.Y = 0;
                if (ImageClientLocation.X - pictureBox1.Width < -ImagePreview.Width)
                    ImageClientLocation.X = sLeft;
                if (ImageClientLocation.Y - pictureBox1.Height < -ImagePreview.Height)
                    ImageClientLocation.Y = sTop;
                saveloc = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            buttondown = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(ImagePreview != null)
                e.Graphics.DrawImage(ImagePreview, ImageClientLocation.X, ImageClientLocation.Y, ImagePreview.Width, ImagePreview.Height);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ImagePreview = new Bitmap(ImageSource, new Size(ImageSource.Width / 2, ImageSource.Height / 2));
            ImagePreview.SetResolution(ImageSource.HorizontalResolution, ImageSource.VerticalResolution);
            //Отцентрировать изображение внутри области просомтра
            ImageClientLocation.X = -((ImagePreview.Width / 2) - (pictureBox1.Width / 2));
            ImageClientLocation.Y = -((ImagePreview.Height / 2) - (pictureBox1.Height / 2));
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void PreviewDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Сохраняем изображение попавшее в области просомтра
            Bitmap imageforprint = new Bitmap(pictureBox1.Width*2, pictureBox1.Height*2);
            imageforprint.SetResolution(ImageSource.HorizontalResolution, ImageSource.VerticalResolution);
            Graphics g = Graphics.FromImage(imageforprint);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(ImageSource, 0, 0, new Rectangle(Math.Abs(ImageClientLocation.X)*2, Math.Abs(ImageClientLocation.Y)*2, pictureBox1.Width*2, pictureBox1.Height*2), GraphicsUnit.Pixel);
            g.Flush();
            dataforprint.SetLength(0);
            imageforprint.Save(dataforprint, ImageFormat.Jpeg);
            Bitmap imageforcard = new Bitmap(310, 420);
            Graphics g2 = Graphics.FromImage(imageforcard);
            g2.CompositingQuality = CompositingQuality.HighSpeed;
            g2.DrawImage(imageforprint, new Rectangle(0, 0, imageforcard.Width, imageforcard.Height));
            g2.Flush();
            dataforcard.SetLength(0);
            imageforcard.Save(dataforcard, ImageFormat.Jpeg);
        }
    }
}
