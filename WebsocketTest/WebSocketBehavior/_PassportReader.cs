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

    public class PassportReaderWebSocket : WebSocketBehavior
    {
        
        private string jsonModelEncode;

        ResponseData responseData;
        DataPassport dataPassport;
        PassportReaderForm passportReaderForm;
        PassportModel passportModelJson;
        PassportImage jsonPassportImage;


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

        public PassportReaderWebSocket()
        {
            responseData = new ResponseData();
            dataPassport = new DataPassport();
        }

        protected override void OnOpen()
        {
            LoadJson();

            passportReaderForm = new PassportReaderForm();
            User32.SetForegroundWindow(passportReaderForm.Handle);
            passportReaderForm.WindowState = FormWindowState.Normal;
            passportReaderForm.BringToFront();
            passportReaderForm.TopMost = true;
            passportReaderForm.ShowDialog();
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

        async private void LoadJson()
        {
            // ReadData IdCard
            using (System.IO.StreamReader r = new System.IO.StreamReader("mockData\\passport_mock.json"))
            {
                passportModelJson = new PassportModel();
                var json = r.ReadToEnd();
                var jsonModel = JsonConvert.DeserializeObject<PassportModel>(json);
                if (jsonModel != null)
                {
                    jsonModelEncode = encodeData(JsonConvert.SerializeObject(jsonModel));
                    passportModelJson = jsonModel;
                }
            }

            using (System.IO.StreamReader r = new System.IO.StreamReader("mockData\\passport_mock_image.json"))
            {
                jsonPassportImage = new PassportImage();
                var image = r.ReadToEnd();
                var jsonConvert = JsonConvert.DeserializeObject<PassportImage>(image);
                Debug.WriteLine(jsonConvert);
                jsonPassportImage = jsonConvert;
            }
            await WaitFormAsync(2000);
            ReturnMessageToClient();
            await WaitFormAsync(1000);
            CloseForm();
        }

        public string encodeData(string text)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
            string returntext = System.Convert.ToBase64String(mybyte);
            return returntext;
        }

        private void ReturnMessageToClient()
        {
            dataPassport.Event = "OnScanDocCompleted";
            dataPassport.DataPageImage = jsonPassportImage.CardImage;
            dataPassport.Message = "อ่านข้อมูลสำเร็จ";
            dataPassport.Data = jsonModelEncode;
            var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataPassport);
            Send(jsondata);
        }

        private void CloseForm()
        {
            try
            {
                if (passportReaderForm.InvokeRequired)
                {
                    passportReaderForm.Invoke(new Action(CloseForm));
                    return;
                }
                passportReaderForm.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private async System.Threading.Tasks.Task WaitFormAsync(int wait)
        {
            await System.Threading.Tasks.Task.Delay(wait);
        }

    }

}
