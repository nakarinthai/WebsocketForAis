using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WebsocketMock;
using WebsocketMock.Model;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebsocketMock
{

    public class IDCardReaderWebSocket : WebSocketBehavior
    {
        
        private int progressNumber = 0;
        private int maxProgressNumber = 100;
        private string jsonModelEncode;
        private IDCardImage jsonIdcardImage;

        Timer timer;
        ResponseData responseData;
        DataReadCard dataIdCard;
        IDCardModel iDCardModelJson;
        IDCardReaderForm iDCardReaderForm;


        public static class User32
        {
            public const int SW_HIDE = 0;
            public const int SW_SHOW = 5;
            public const int SW_SHOWNORMAL = 1;
            public const int SW_SHOWMAXIMIZED = 3;
            public const int SW_RESTORE = 9;

            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern bool AllowSetForegroundWindow(uint dwProcessId);
            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        }

        public IDCardReaderWebSocket()
        {
            responseData = new ResponseData();
            dataIdCard = new DataReadCard();
        }

        protected override void OnOpen()
        {
            LoadJson();

            iDCardReaderForm = new IDCardReaderForm();
            User32.SetForegroundWindow(iDCardReaderForm.Handle);
            iDCardReaderForm.WindowState = FormWindowState.Normal;
            iDCardReaderForm.BringToFront();
            iDCardReaderForm.TopMost = true;
            iDCardReaderForm.ShowDialog();

        }

        // call when client send message
        protected override void OnMessage(MessageEventArgs e)
        {

        }

        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Write("OnClose" + e);
            CloseForm();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Debug.Write("OnError" + e);
        }

        private void LoadJson()
        {
            // ReadData IdCard
            using (System.IO.StreamReader r = new System.IO.StreamReader("mockData\\id_card_mock.json"))
            {
                iDCardModelJson = new IDCardModel();
                var json = r.ReadToEnd();
                var jsonModel = JsonConvert.DeserializeObject<IDCardModel>(json);
                if (jsonModel != null)
                {
                    jsonModelEncode = encodeData(JsonConvert.SerializeObject(jsonModel));
                    iDCardModelJson = jsonModel;
                    TimerDoing();
                }
            }


            // ReadData IdCard Image
            using (System.IO.StreamReader r = new System.IO.StreamReader("mockData\\id_card_mock_image.json"))
            {
                iDCardModelJson = new IDCardModel();
                var image = r.ReadToEnd();
                var jsonConvert = JsonConvert.DeserializeObject<IDCardImage>(image);
                jsonIdcardImage = jsonConvert;
            }
        }

        private void TimerDoing()
        {
            timer = new Timer
            {
                Interval = 200
            };
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(OnTimerEvent);
        }


        async public void OnTimerEvent(object source, EventArgs e)
        {
            Random random = new Random();

            if (progressNumber < 10)
            {
                dataIdCard.Event = "OnInitialized";
                dataIdCard.Error = "0";
                dataIdCard.Version = "Mock Websocket Test 1.0.0";
                dataIdCard.PhysicalAddress = "I DONT KNOW";
                var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataIdCard);
                Send(jsondata);
                await WaitFormAsync(2000);
            }
            else if (progressNumber < 20)
            {
                dataIdCard.Event = "OnCardInserted";
                var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataIdCard);
                Send(jsondata);

            }
            else if (progressNumber >= maxProgressNumber)
            {
                progressNumber = maxProgressNumber;
                if (dataIdCard.Event != "OnCardLoadCompleted")
                {
                    dataIdCard.Event = "OnCardLoadCompleted";
                    dataIdCard.CardImage = jsonIdcardImage.CardImage;
                    dataIdCard.PhotoImage = jsonIdcardImage.PhotoImage;
                    dataIdCard.Message = "กำลังอ่านบัตรประชาชน กรุณารอสักครู่";
                    dataIdCard.Progress = progressNumber.ToString();
                    dataIdCard.Data = jsonModelEncode;
                    var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataIdCard);
                    Send(jsondata);
                }
            }

            if (progressNumber != maxProgressNumber)
            {
                dataIdCard.Event = "OnCardLoadProgress";
                dataIdCard.Message = "กำลังอ่านบัตรประชาชน กรุณารอสักครู่";
                if (progressNumber > 90)
                {
                    progressNumber = maxProgressNumber;
                }
                dataIdCard.Progress = progressNumber.ToString();
                var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataIdCard);
                Send(jsondata);
            }

            if (progressNumber == maxProgressNumber)
            {
                iDCardReaderForm.lb_percen.Text = maxProgressNumber + " %";
                iDCardReaderForm.progressBar.Value = maxProgressNumber;
                await WaitFormAsync(2000);
                iDCardReaderForm.Close();
                timer.Enabled = false;
                timer.Stop();
            }
            else
            {
                iDCardReaderForm.lb_percen.Text = progressNumber + " %";
                iDCardReaderForm.progressBar.Value = progressNumber;
                progressNumber += random.Next(3, 8);
            }

        }

        private async System.Threading.Tasks.Task WaitFormAsync(int wait)
        {
            await System.Threading.Tasks.Task.Delay(wait);
        }


        public string encodeData(string text)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
            string returntext = System.Convert.ToBase64String(mybyte);
            return returntext;
        }

        private void CloseForm()
        {
            try
            {
                if (iDCardReaderForm.InvokeRequired)
                {
                    iDCardReaderForm.Invoke(new Action(CloseForm));
                    return;
                }
                iDCardReaderForm.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

    }

}
