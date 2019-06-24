using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WebsocketMock.Model;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebsocketMock
{

    public class SignaturePadWebSocket : WebSocketBehavior
    {

        ResponseData responseData = new ResponseData();
        DataSignaturePad dataSignaturePad = new DataSignaturePad();
        DataOnscreenPad dataOnscreenPad = new DataOnscreenPad();
        SignpadDrawForm signpadDrawForm;
        OnscreenDrawForm onscreenDrawForm;

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

        public SignaturePadWebSocket()
        {
        }

        protected override void OnOpen()
        {
            Debug.Write("OnOpen");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            string getEventDataFormClient = e.Data;
            if (getEventDataFormClient == "{Who:Customer,Why:Purchase}")
            {
                signpadDrawForm = new SignpadDrawForm();
                User32.SetForegroundWindow(signpadDrawForm.Handle);
                signpadDrawForm.Focus();
                signpadDrawForm.TopMost = true;
                signpadDrawForm.ShowDialog();
                if (signpadDrawForm.IsDisposed)
                {
                    dataSignaturePad.Event = responseData.EventStatus; // "OnSigPadCompleted";
                    dataSignaturePad.SigImage = responseData.SignatureBase64;
                    dataSignaturePad.Message = "Success";
                    var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataSignaturePad);
                    Send(jsondata);
                }
            } else
            {
                onscreenDrawForm = new OnscreenDrawForm();
                User32.SetForegroundWindow(onscreenDrawForm.Handle);
                onscreenDrawForm.Focus();
                onscreenDrawForm.TopMost = true;
                onscreenDrawForm.ShowDialog();
                if (onscreenDrawForm.IsDisposed)
                {
                    dataOnscreenPad.Event = responseData.EventStatus; // "OnSignCompleted";
                    dataOnscreenPad.Base64Image = responseData.SignatureBase64;
                    dataOnscreenPad.Message = "Success";
                    var jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(dataOnscreenPad);
                    Send(jsondata);
                }
            }

            //Sessions.Broadcast(e.Data);

        }

        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Write("OnClose" + responseData.IsDrawSignature);
            CloseForm();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Debug.Write("OnError" + e);
        }

        private void CloseForm()
        {
            try
            {
                if (signpadDrawForm.InvokeRequired)
                {
                    signpadDrawForm.Invoke(new Action(CloseForm));
                    return;
                } else
                {
                    signpadDrawForm.Dispose();
                }

                if (onscreenDrawForm.InvokeRequired)
                {
                    onscreenDrawForm.Invoke(new Action(CloseForm));
                    return;
                } else
                {
                    onscreenDrawForm.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }
    }

}