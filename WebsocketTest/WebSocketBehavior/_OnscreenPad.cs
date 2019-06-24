using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebsocketMock.Model;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebsocketMock
{

    public class OnscreenPadWebSocket : WebSocketBehavior
    {

        DataSignaturePad dataSignaturePad = new DataSignaturePad();
        DataOnscreenPad dataOnscreenPad = new DataOnscreenPad();
        ResponseData responseData = new ResponseData();
        OnscreenDrawForm onscreenDrawForm;
        SignpadDrawForm signpadDrawForm;

        private static string getEventDataFormClient = "";

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

       

        protected override void OnOpen()
        {
            
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //Sessions.Broadcast(e.Data);
            getEventDataFormClient = e.Data;
            Debug.WriteLine(getEventDataFormClient, "OnMessage");

            if (getEventDataFormClient == "{Who:Customer,Why:Purchase}")
            {
                OpenSignpadDraw();
            }
            else if (getEventDataFormClient == "CaptureImage")
            {
                dataOnscreenPad.Event = "OnSignCompleted";
                dataOnscreenPad.Message = "Success";
                dataOnscreenPad.Base64Image = responseData.SignatureBase64;
                var jsondataObj = Newtonsoft.Json.JsonConvert.SerializeObject(dataOnscreenPad);
                Send(jsondataObj);
            }
            else
            {
                OpenOnScreenPad();
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Debug.WriteLine("OnClose" + e);
            CloseForm();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Debug.WriteLine("OnError" + e);
        }


        private void OpenSignpadDraw()
        {
            FormCollection formCollection = Application.OpenForms;
            if (Application.OpenForms["SignpadDrawForm"] == null)
            {
                new Thread(() =>
                {
                    signpadDrawForm = new SignpadDrawForm();
                    User32.SetForegroundWindow(signpadDrawForm.Handle);
                    signpadDrawForm.Focus();
                    signpadDrawForm.TopMost = true;
                    signpadDrawForm.ShowDialog();
                    if (signpadDrawForm.IsDisposed && responseData.SignatureBase64 != "")
                    {
                        ReturnMessageToClient("SignpadDrawForm");
                    }
                }
                ).Start();

            }
            else
            {
                Application.OpenForms["SignpadDrawForm"].BringToFront();
            }
        }

        public void OpenOnScreenPad()
        {

            FormCollection formCollection = Application.OpenForms;
            if (Application.OpenForms["OnscreenDrawForm"] == null)
            {
                new Thread(() =>
                {
                    onscreenDrawForm = new OnscreenDrawForm();
                    User32.SetForegroundWindow(onscreenDrawForm.Handle);
                    onscreenDrawForm.Focus();
                    onscreenDrawForm.TopMost = true;
                    onscreenDrawForm.ShowDialog();
                    if (onscreenDrawForm.IsDisposed)
                    {
                        ReturnMessageToClient("OnscreenDrawForm");
                    }
                }
                ).Start();

            }
            else
            {
                onscreenDrawForm.Focus();
                onscreenDrawForm.TopMost = true;
                onscreenDrawForm.BringToFront();
                onscreenDrawForm.Activate();
            }

        }

        public void ReturnMessageToClient(string formName)
        {
            if (formName == "OnscreenDrawForm")
            {
                dataOnscreenPad.Event = "OnSignCompleted";
                dataOnscreenPad.Message = "Success";
                dataOnscreenPad.Base64Image = responseData.SignatureBase64;
                var jsondataObj = Newtonsoft.Json.JsonConvert.SerializeObject(dataOnscreenPad);
                Send(jsondataObj);
            }
            else if (formName == "SignpadDrawForm")
            {
                dataSignaturePad.Event = "OnSignCompleted";
                dataSignaturePad.Message = "Success";
                dataSignaturePad.Base64Image = responseData.SignatureBase64;
                dataSignaturePad.SigImage = responseData.SignatureBase64;
                var jsondataObj = Newtonsoft.Json.JsonConvert.SerializeObject(dataSignaturePad);
                Send(jsondataObj);
            }


        }

        private void CloseForm()
        {
            try
            {
                if (onscreenDrawForm.InvokeRequired)
                {
                    onscreenDrawForm.Invoke(new Action(CloseForm));
                    return;
                }
                onscreenDrawForm.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }


    }

}
