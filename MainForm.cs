using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AWP_CollectionOrder
{
    public partial class MainForm : Form
    {

        CameraDialog cameradlg = new CameraDialog();
        PreviewDlg PreviewForm = new PreviewDlg();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Exitbutton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Savebutton1_Click(object sender, EventArgs e)
        {
            //Проверить на наличие данных о владельце
            if ((LastName_textBox.Text.Length == 0) || (FirstName_textBox.Text.Length == 0) || (PatrName_textBox.Text.Length == 0))
            {
                MessageBox.Show("В заявке не были заполнены необходимые поля, заполнити их.", "Не все поля заполнены", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LastName_textBox.Focus();
                return;
            }
            //Сформировать имя файла для сохранения
            string SaveFileName = Properties.Settings.Default.DemandPath;
            SaveFileName += LastName_textBox.Text + " " + FirstName_textBox.Text + " " + PatrName_textBox.Text;
            SaveFileName += ("-" + DateTime.Now.ToString("hhmmddMMyyyy") + ".xml");
            StringBuilder ChipPhoto = new StringBuilder();
            PreviewForm.dataforcard.Position = 0;
            for(int i=0;i < PreviewForm.dataforcard.Length;i++)
                ChipPhoto.Append(PreviewForm.dataforcard.ReadByte().ToString("X2"));

            StringBuilder PrintablePhoto = new StringBuilder();
            PreviewForm.dataforprint.Position = 0;
            for (int i = 0; i < PreviewForm.dataforprint.Length; i++)
                PrintablePhoto.Append(PreviewForm.dataforprint.ReadByte().ToString("X2"));
            //формируем файл формата XML для сохранения заявки
            XDocument docreq = new XDocument(new XElement("PropuskRequest",
                   new XElement("LastName",LastName_textBox.Text),
                   new XElement("FirstName",FirstName_textBox.Text),
                   new XElement("PatrName",PatrName_textBox.Text),
                   new XElement("ChipPhoto",ChipPhoto),
                   new XElement("PrintablePhoto",PrintablePhoto)
                   ));
            docreq.Save(SaveFileName);
            MessageBox.Show("Заявка успешно сохранена","Сохранение заявки",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        /// <summary>
        /// Событие при загрузке формы АРМа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Выполнить подключение к камере
            cameradlg.Connect();
        }

        /// <summary>
        /// Создать новую заявку и вызвать диалог получения изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neworederbutton1_Click(object sender, EventArgs e)
        {
            //Очистить поля ввода данных о владельце
            LastName_textBox.Clear();
            FirstName_textBox.Clear();
            PatrName_textBox.Clear();
            //Отобразить диалог получения фотографии
            if (cameradlg.ShowDialog() != DialogResult.Yes)
                return;
            int Mx = cameradlg.RelaeseImage.Width / cameradlg.pbViewFinder.Width;
            int My = cameradlg.RelaeseImage.Height / cameradlg.pbViewFinder.Height;

            Rectangle rect = new Rectangle(cameradlg.RectangleForRelease.X*Mx, cameradlg.RectangleForRelease.Y*My, cameradlg.RectangleForRelease.Width*Mx, cameradlg.RectangleForRelease.Height*My);
            Bitmap b = new Bitmap(rect.Width, rect.Height);
            b.SetResolution(cameradlg.RelaeseImage.HorizontalResolution, cameradlg.RelaeseImage.VerticalResolution); 
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(cameradlg.RelaeseImage,0,0,rect,GraphicsUnit.Pixel);
            g.Flush();
            PreviewForm.ImageSource = b;
            //Отобразить даилог Предварительного просмотра (позиционирование изображения)
            PreviewForm.ShowDialog();
            //Проверить размер изображения для записи в карту
            if (PreviewForm.dataforcard.Length > 25000)
            {

                MessageBox.Show("Размер полученной фотографии превышает допустимые значения,\n необходимо выполнить повторное фотографирование", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Вывести на форму изображение отпозиционированное оператором
            demopictureBox.Image = Image.FromStream(PreviewForm.dataforprint);
            LastName_textBox.Focus();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Выполнить отключение от камеры
            cameradlg.Disconnect();
        }
    }
}
