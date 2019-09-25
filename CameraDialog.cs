using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Camera.Device.Common;
using Camera.Canon.PSReC;
using System.IO;


namespace AWP_CollectionOrder
{
    public partial class CameraDialog : Form
    {
        private MemoryStream _ms;
        private ICameraManager _cm;
        private ICamera _camera;
        private Pen PenFaceBourder = new Pen(Color.Green);
        public Image RelaeseImage;
        public Rectangle RectangleForRelease;

        private void RefreshCameras()
        {
            if (_cm != null)
                _cm.Dispose();
            _camera = null;
            _cm = new CanonCameraManager();
            if (_cm.Count == 0) return;

            _camera = _cm[0];
            _camera.ReleasedImageBlockReceived += Camera_ReleasedImageBlockReceived;
            _camera.ViewFinderImageReceived += Camera_ViewFinderImageReceived;
        }
        private void Camera_ReleasedImageBlockReceived(object sender, ReleasedImageBlockReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.ReleaseprogressBar.Value = e.Percent;
                _ms.Write(e.ImageBlock, 0, e.ImageBlock.Length);
            });
        }

        private void Camera_ViewFinderImageReceived(object sender, ViewFinderImageReceivedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                using (var ms = new MemoryStream(e.Image))
                    this.pbViewFinder.Image = Image.FromStream(ms);
            });
        }
        public void Connect()
        {
            try
            {
                _camera.Connect();
            }
            catch
            {
                MessageBox.Show("Камера не подключена, либо находится в недопустимом для работы режиме.\nДальнейшая работа невозможна!\nВыполните действия по подключению и включению камеры", "Камера не подключена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        public CameraDialog()
        {
            InitializeComponent();
            RectangleForRelease = new Rectangle(0, 0, 420, 420);
            RectangleForRelease.X = (pbViewFinder.Width/2) - (RectangleForRelease.Width/2);
            RectangleForRelease.Y = (pbViewFinder.Height/2) - (RectangleForRelease.Height/2);
            RefreshCameras();
            PenFaceBourder.DashStyle = DashStyle.Dash;
            PenFaceBourder.Width = 2;
        }
        public void Disconnect()
        {
            try
            {
                _camera.Disconnect();
            }
            catch
            {
            }
            
        }
        private void StartViewFinder()
        {
            try
            {
                _camera.StartViewFinder();
            }
            catch
            {
                MessageBox.Show("Невозможно начать работу видоискателя камеры.\nДальнейшая работа невозможна!\nВыполните действия по повторному включению камеры", "Ошибка 	видоискателя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void StopViewFinder()
        {
            try
            {
                _camera.StopViewFinder();
            }
            catch
            {
            }
        }
        private void Release()
        {
            try
            {
                _ms = new MemoryStream();
                _camera.Release();
                _ms.Flush();
                this.RelaeseImage = Image.FromStream(_ms);
                _ms.Close();
                _ms = null;
            }
            catch
            {
                MessageBox.Show("Невозможно получить изображение с камеры.\nДальнейшая работа невозможна!\nВыполните действия по повторному включению камеры", "Ошибка 	получения изображения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        /// <summary>
        /// Событие возникает при вызове метода отображения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CameraDialog_Shown(object sender, EventArgs e)
        {
            //Получить список допустимых занчений маштибирования
            for (int i = _camera.DeviceProperties.Zoom.Min+1; i < _camera.DeviceProperties.Zoom.Max; i++)
            {
                zoomcomboBox1.Items.Add(string.Format("{0}x",i));
            }
            zoomcomboBox1.SelectedIndex = Properties.Settings.Default.CameraZoomIndex;
            this.ReleaseprogressBar.Value = 0;
            //Начать процесс предватительного просмотра изображений с камеры
            StartViewFinder();
        }

        private void CameraDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopViewFinder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Release();
        }

        private void pbViewFinder_Paint(object sender, PaintEventArgs e)
        {
            //Добавить в предварительный просмотр прямоугольник с перекрестием для захвата области лица
            e.Graphics.DrawRectangle(PenFaceBourder, RectangleForRelease);
            e.Graphics.DrawLine(Pens.Red, new Point(RectangleForRelease.X, RectangleForRelease.Y+(RectangleForRelease.Height / 2)), 
                new Point(RectangleForRelease.Right, RectangleForRelease.Y+(RectangleForRelease.Height / 2)));
            e.Graphics.DrawLine(Pens.Red, new Point(RectangleForRelease.X + (RectangleForRelease.Width / 2), RectangleForRelease.Y),
                new Point(RectangleForRelease.X + (RectangleForRelease.Width / 2), RectangleForRelease.Bottom));
            e.Graphics.DrawRectangle(Pens.Red, new Rectangle(new Point(RectangleForRelease.X + (RectangleForRelease.Width / 2)-50,RectangleForRelease.Y+(RectangleForRelease.Height / 2)-50), new Size(50*2, 50*2)));
        }

        private void zoomcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _camera.DeviceProperties.Zoom.Current = (ushort)zoomcomboBox1.SelectedIndex;
        }
    }
}
