using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WebsocketMock
{
    public partial class OnscreenDrawForm : Form
    {
        Graphics graphic, graphic_result;

        int PointX = 0;
        int PointY = 0;

        int LastX = 0;
        int LastY = 0;

        Bitmap bmp;

        ResponseData responseData = new ResponseData();


        string base64Signature;

        public OnscreenDrawForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Focus();
            // form corner
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphic_result = Graphics.FromImage(bmp);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            SmoothingMode smoothing = SmoothingMode.HighQuality;
            graphic = pictureBox.CreateGraphics();
            graphic.SmoothingMode = smoothing;
            graphic_result.SmoothingMode = smoothing;
            Pen pen = new Pen(Color.Blue, 2);
            graphic.DrawLine(pen, PointX, PointY, LastX, LastY);
            graphic_result.DrawLine(pen, PointX, PointY, LastX, LastY);
            LastX = PointX;
            LastY = PointY;
            ConverToBase64();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            LastX = e.X;
            LastY = e.Y;
            responseData.IsDrawSignature = true;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                responseData.IsDrawSignature = true;
                PointX = e.X;
                PointY = e.Y;
                pictureBox_Paint(this, null);
            }
        }


        private void btn_conf_Click(object sender, EventArgs e)
        {
            ConverToBase64();
            this.Dispose();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            graphic.Clear(Color.White);
            graphic_result.Clear(Color.White);
            ClearData();
        }

        private void ClearData()
        {
            responseData.IsDrawSignature = false;
            responseData.SignatureBase64 = "";
            responseData.EventStatus = "NotSign";
        }

        private void lb_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SignpadDraw_Load(object sender, EventArgs e)
        {
            responseData.IsDrawSignature = false;
            responseData.SignatureBase64 = "";
        }

        public void ConverToBase64()
        {
            using (MemoryStream m = new MemoryStream())
            {
                bmp.Save(m, ImageFormat.Png);
                byte[] imageBytes = m.ToArray();
                base64Signature = Convert.ToBase64String(imageBytes);
                if (responseData.IsDrawSignature == true)
                {
                    responseData.SignatureBase64 = base64Signature;
                }
                else
                {
                    responseData.SignatureBase64 = "";
                }
                responseData.EventStatus = "OnSigPadCompleted";
            }
        }

    }

}