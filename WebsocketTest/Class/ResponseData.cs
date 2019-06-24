using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebsocketMock
{
    class ResponseData
    {
        private static string signatureBase64;
        private static bool isDrawing;
        private static string eventStatus;

        public string SignatureBase64
        {
            get { return signatureBase64; }
            set { signatureBase64 = value; }
        }

        public string EventStatus
        {
            get { return eventStatus; }
            set { eventStatus = value; }
        }

        
        public bool IsDrawSignature
        {
            get { return isDrawing; }
            set { isDrawing = value; }
        }
    }
}
