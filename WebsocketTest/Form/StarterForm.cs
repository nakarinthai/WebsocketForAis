using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using ThaiNationalIDCard;
using WebsocketMock.Model;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace WebsocketMock
{
    public partial class StarterForm : MetroFramework.Forms.MetroForm
    {
        private readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
        string selectIdCard_Id, selectPassport_Id;
        JArray jsonIdCardVal, jsonPassportVal, jsonAddIdCardVal;
        string jsonIdCardSelected, jsonPassportSelected, jsonIdCardReader;

        // thaiIDCard
        ThaiIDCard thaiIDCard;
        bool isHaveData = false;
        bool idCardSelectChange = false;
        bool passPortSelectChange = false;
        string AtrString = "";

        public StarterForm()
        {
            InitializeComponent();
            CheckDuplicateProcessRun();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDeviceIdCard();

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
            Primary.Orange500, Primary.BlueGrey900,
            Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);


            RunWebSocket();
            notifyIconForm.BalloonTipIcon = ToolTipIcon.Info;
            notifyIconForm.BalloonTipText = "WebSocket Mock Data ver 1.0.0";
            notifyIconForm.BalloonTipTitle = "Welcome to Websocket";
            notifyIconForm.ShowBalloonTip(1000);
            LoadJson();


        }

        private void CheckDuplicateProcessRun()
        {
            Process process = Process.GetCurrentProcess();
            var dupl = (Process.GetProcessesByName(process.ProcessName));
            if (dupl.Length > 1 && MessageBox.Show("รัน Websocket ซ้ำกัน ?", "Kill duplicates Websocket?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var p in dupl)
                {
                    if (p.Id != process.Id)
                        p.Kill();
                }
            }
        }

        private static void RunWebSocket()
        {
            var webSocketServer = new WebSocketServer(8088, true);
            webSocketServer.SslConfiguration.ServerCertificate = new X509Certificate2("localhost.pfx", "111111");
            webSocketServer.AddWebSocketService<SignaturePadWebSocket>("/SignaturePad");
            webSocketServer.AddWebSocketService<OnscreenPadWebSocket>("/OnscreenSignpad");
            webSocketServer.AddWebSocketService<IDCardReaderWebSocket>("/ReadIDCard");
            webSocketServer.AddWebSocketService<PassportReaderWebSocket>("/ReadPassport");
            try
            {
                webSocketServer.Start();
            }
            catch (Exception e)
            {
                if (MessageBox.Show("Error " + e, "Kill duplicates Websocket?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    webSocketServer.Stop();
                }

            }

            //wssv.Stop();
        }

        private void LoadJson()
        {
            // ReadData IdCard
            using (System.IO.StreamReader idcard = new System.IO.StreamReader("mockData\\id_card_mock_list.json"))
            {

                var json = idcard.ReadToEnd();
                var idCardList = JsonConvert.DeserializeObject(json);
                jsonIdCardVal = JArray.Parse(idCardList.ToString()) as JArray;
                idCardCbx.DataSource = jsonIdCardVal;
                idCardCbx.DisplayMember = "NationalID";
                idCardCbx.ValueMember = "NationalID";
            }

            // ReadData Passport
            using (System.IO.StreamReader passport = new System.IO.StreamReader("mockData\\passport_mock_list.json"))
            {

                var json = passport.ReadToEnd();
                var passportList = JsonConvert.DeserializeObject(json);
                jsonPassportVal = JArray.Parse(passportList.ToString()) as JArray;
                passportCbx.DataSource = jsonPassportVal;
                passportCbx.DisplayMember = "GivenName";
                passportCbx.ValueMember = "PassportNumber";
            }

        }

        void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == "Exit")
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }



        private void WriteJsonToFile()
        {
            if (idCardSelectChange)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("mockData\\id_card_mock.json"))
                {
                    file.WriteLine(JObject.Parse(jsonIdCardSelected));
                }
            }

            if (passPortSelectChange)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("mockData\\passport_mock.json"))
                {
                    file.WriteLine(JObject.Parse(jsonPassportSelected));
                }
            }
        }

        private void idCardCbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void passportCbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void idCardCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectIdCard_Id = idCardCbx.GetItemText(idCardCbx.SelectedItem);
            foreach (var json in jsonIdCardVal)
            {
                if (json["NationalID"].ToString() == selectIdCard_Id)
                {
                    txtIdcardSelect.Text = json["EnglishFirstName"] + " " + json["EnglishLastName"];
                    jsonIdCardSelected = json.ToString();
                    break;
                }
            }
        }

        async private void btnLoadCard_Click(object sender, EventArgs e)
        {
            if (isHaveData)
            {
                using (System.IO.StreamReader idcard = new System.IO.StreamReader("mockData\\id_card_mock_list.json"))
                {
                    var json = idcard.ReadToEnd();
                    var idCardList = JsonConvert.DeserializeObject(json);
                    jsonAddIdCardVal = JArray.Parse(idCardList.ToString()) as JArray;
                    jsonAddIdCardVal.Add(JsonConvert.DeserializeObject(jsonIdCardReader));

                }

                await WaitFormAsync(2000);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("mockData\\id_card_mock_list.json"))
                {
                    file.WriteLine(jsonAddIdCardVal);
                    btnLoadCard.Text = "Save Success";
                }
                await WaitFormAsync(1000);
                LoadJson();


            }
            else
            {
                LoadIDCard();
            }

        }

        // LoadCard
        private void LoadIDCard()
        {

            thaiIDCard = new ThaiIDCard();
            thaiIDCard.eventPhotoProgress += new handlePhotoProgress(photoProgress);

            try
            {
                Personal personal = thaiIDCard.readAllPhoto();
                if (personal != null)
                {
                    IDCardModel readFormIdcard = new IDCardModel();

                    readFormIdcard.NationalID = personal.Citizenid;
                    readFormIdcard.CardType = "01";
                    readFormIdcard.ThaiFirstName = personal.Th_Firstname;
                    readFormIdcard.ThaiLastName = personal.Th_Lastname;
                    readFormIdcard.ThaiMiddleName = personal.Th_Middlename;
                    readFormIdcard.ThaiTitleName = personal.Th_Prefix;
                    readFormIdcard.EnglishTitleName = personal.En_Prefix;
                    readFormIdcard.EnglishFirstName = personal.En_Firstname;
                    readFormIdcard.EnglishLastName = personal.En_Lastname;
                    readFormIdcard.Address = personal.addrHouseNo;
                    readFormIdcard.Amphur = personal.addrAmphur;
                    readFormIdcard.AtrString = AtrString;
                    readFormIdcard.Birthdate = personal.Birthday.ToString("dd/MM/yyyy");
                    readFormIdcard.ChipID = "";
                    readFormIdcard.EnglishMiddleName = personal.En_Middlename;
                    readFormIdcard.ExpireDate = personal.Expire.ToString("dd/MM/yyyy"); ;
                    readFormIdcard.FormatVersion = "0003";
                    readFormIdcard.IssueDate = personal.Issue.ToString("dd/MM/yyyy");
                    readFormIdcard.IssuePlace = personal.Expire.ToString("dd/MM/yyyy");
                    readFormIdcard.IssuerCode = "";
                    readFormIdcard.LaserID = personal.En_Middlename;
                    readFormIdcard.Moo = personal.addrVillageNo;
                    readFormIdcard.PhotoRefNo = "";
                    readFormIdcard.Province = personal.addrProvince;
                    readFormIdcard.RequestNo = personal.En_Middlename;
                    readFormIdcard.Sex = personal.Sex;
                    readFormIdcard.Soi = personal.addrLane;
                    readFormIdcard.Thanon = personal.addrRoad;
                    readFormIdcard.Trok = personal.En_Middlename;
                    readFormIdcard.Tumbol = personal.addrTambol;

                    // readFormIdcard
                    lbIdcard.Text = personal.Citizenid;
                    lbFullname.Text = personal.En_Firstname + " " + personal.En_Lastname;

                    bitmapCard.Image = personal.PhotoBitmap;
                    var jsonConverter = JsonConvert.SerializeObject(readFormIdcard);
                    btnLoadCard.Text = "Save to Mock";
                    isHaveData = true;

                    jsonIdCardReader = jsonConverter.ToString();

                    Debug.WriteLine(JsonConvert.DeserializeObject(personal.ToString()));


                }
                else if (thaiIDCard.ErrorCode() > 0)
                {
                    MessageBox.Show("Error :" + thaiIDCard.Error());
                    MessageBox.Show("Error :" + thaiIDCard.ErrorCode());
                    Console.WriteLine(thaiIDCard.Error());
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            
        }

        private void photoProgress(int value, int maximum)
        {
            if (progressReadCard.Maximum != maximum)
            {
                progressReadCard.Maximum = maximum;
            }

            // fix progress bar sync.
            if (progressReadCard.Maximum > value)
            {
                progressReadCard.Value = value + 1;
            }
            progressReadCard.Value = value;

        }

        private void LoadDeviceIdCard()
        {
            cbo_DeviceName.Items.Clear();
            cbo_DeviceName.SelectedText = String.Empty;
            cbo_DeviceName.Text = string.Empty;
            cbo_DeviceName.Refresh();

            var contextFactory = ContextFactory.Instance;
            using (var context = contextFactory.Establish(SCardScope.System))
            {
                Console.WriteLine("Currently connected readers: ");
                var readerNames = context.GetReaders();
                foreach (var readerName in readerNames)
                {
                    try
                    {
                        using (var reader = context.ConnectReader(readerName, SCardShareMode.Shared, SCardProtocol.Any))
                        {
                            var status = reader.GetStatus();
                            Debug.WriteLine(JsonConvert.SerializeObject(status));
                            AtrString = BitConverter.ToString(status.GetAtr()).Replace("-", "");
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(
                            "No card inserted or reader '{0}' is reserved exclusively by another application.", readerName);
                        Debug.WriteLine("Error message: {0} ({1})\n", exception.Message, exception.GetType());
                    }
                }

            }

            try
            {
                ThaiIDCard idcard = new ThaiIDCard();
                string[] readers = idcard.GetReaders();

                if (readers == null)
                {
                    btnLoadCard.Enabled = false;
                    return;
                }

                btnLoadCard.Enabled = true;

                foreach (string r in readers)
                {
                    cbo_DeviceName.Items.Add(r);
                }

                Debug.WriteLine("Count", cbo_DeviceName.Items.Count.ToString());

                if (cbo_DeviceName.Items.Count > 0)
                {
                    cbo_DeviceName.SelectedIndex = 0;
                }
                else
                {
                    cbo_DeviceName.Items.Add("None");
                }

                cbo_DeviceName.DroppedDown = true;

            }
            catch (Exception ex)
            {
                cbo_DeviceName.Items.Add("None");
                cbo_DeviceName.SelectedIndex = 0;
                Debug.WriteLine(ex.ToString());
            }
        }



        private void idCardCbx_SelectedValueChanged(object sender, EventArgs e)
        {
            idCardSelectChange = true;
        }

        private void passportCbx_SelectedValueChanged(object sender, EventArgs e)
        {
            passPortSelectChange = true;
        }

        private void cbo_DeviceName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            LoadDeviceIdCard();
        }

        private void passportCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectPassport_Id = passportCbx.GetItemText(passportCbx.SelectedItem);
            foreach (var json in jsonPassportVal)
            {
                if (json["GivenName"].ToString() == selectPassport_Id)
                {
                    jsonPassportSelected = json.ToString();
                    break;
                }
            }
            Debug.WriteLine(jsonPassportSelected);
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            WriteJsonToFile();
            this.Hide();
        }

        private async System.Threading.Tasks.Task WaitFormAsync(int wait)
        {
            await System.Threading.Tasks.Task.Delay(wait);
        }
    }
}