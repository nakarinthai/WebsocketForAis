namespace WebsocketMock
{
    partial class StarterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StarterForm));
            this.notifyIconForm = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.idCardCbx = new System.Windows.Forms.ComboBox();
            this.btn_save = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.passportCbx = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.progressReadCard = new MetroFramework.Controls.MetroProgressBar();
            this.btn_Load = new System.Windows.Forms.Button();
            this.cbo_DeviceName = new System.Windows.Forms.ComboBox();
            this.bitmapCard = new System.Windows.Forms.PictureBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.lbIdcard = new System.Windows.Forms.Label();
            this.lbFullname = new System.Windows.Forms.Label();
            this.txtIdcardSelect = new System.Windows.Forms.Label();
            this.btnLoadCard = new MetroFramework.Controls.MetroButton();
            this.contextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapCard)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIconForm
            // 
            this.notifyIconForm.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notifyIconForm, "notifyIconForm");
            this.notifyIconForm.ContextMenuStrip = this.contextMenuStrip;
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::WebsocketMock.Properties.Resources.settings;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::WebsocketMock.Properties.Resources.exit_icon_3;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtIdcardSelect);
            this.groupBox1.Controls.Add(this.materialLabel1);
            this.groupBox1.Controls.Add(this.idCardCbx);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // idCardCbx
            // 
            resources.ApplyResources(this.idCardCbx, "idCardCbx");
            this.idCardCbx.FormattingEnabled = true;
            this.idCardCbx.Name = "idCardCbx";
            this.idCardCbx.Sorted = true;
            this.idCardCbx.SelectedIndexChanged += new System.EventHandler(this.idCardCbx_SelectedIndexChanged);
            this.idCardCbx.SelectedValueChanged += new System.EventHandler(this.idCardCbx_SelectedValueChanged);
            this.idCardCbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.idCardCbx_KeyPress);
            // 
            // btn_save
            // 
            this.btn_save.Depth = 0;
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_save.Name = "btn_save";
            this.btn_save.Primary = true;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.passportCbx);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // passportCbx
            // 
            resources.ApplyResources(this.passportCbx, "passportCbx");
            this.passportCbx.FormattingEnabled = true;
            this.passportCbx.Name = "passportCbx";
            this.passportCbx.Sorted = true;
            this.passportCbx.SelectedIndexChanged += new System.EventHandler(this.passportCbx_SelectedIndexChanged);
            this.passportCbx.SelectedValueChanged += new System.EventHandler(this.passportCbx_SelectedValueChanged);
            this.passportCbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passportCbx_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.btnLoadCard);
            this.groupBox3.Controls.Add(this.lbFullname);
            this.groupBox3.Controls.Add(this.lbIdcard);
            this.groupBox3.Controls.Add(this.materialLabel3);
            this.groupBox3.Controls.Add(this.materialLabel2);
            this.groupBox3.Controls.Add(this.progressReadCard);
            this.groupBox3.Controls.Add(this.btn_Load);
            this.groupBox3.Controls.Add(this.cbo_DeviceName);
            this.groupBox3.Controls.Add(this.bitmapCard);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            // 
            // materialLabel2
            // 
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.Depth = 0;
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            // 
            // progressReadCard
            // 
            resources.ApplyResources(this.progressReadCard, "progressReadCard");
            this.progressReadCard.Name = "progressReadCard";
            this.progressReadCard.Style = MetroFramework.MetroColorStyle.Orange;
            this.progressReadCard.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btn_Load
            // 
            this.btn_Load.BackColor = System.Drawing.Color.DarkOrange;
            this.btn_Load.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_Load, "btn_Load");
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.UseVisualStyleBackColor = false;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // cbo_DeviceName
            // 
            resources.ApplyResources(this.cbo_DeviceName, "cbo_DeviceName");
            this.cbo_DeviceName.FormattingEnabled = true;
            this.cbo_DeviceName.Name = "cbo_DeviceName";
            this.cbo_DeviceName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_DeviceName_KeyPress);
            // 
            // bitmapCard
            // 
            this.bitmapCard.BackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.bitmapCard, "bitmapCard");
            this.bitmapCard.Name = "bitmapCard";
            this.bitmapCard.TabStop = false;
            // 
            // materialLabel1
            // 
            resources.ApplyResources(this.materialLabel1, "materialLabel1");
            this.materialLabel1.Depth = 0;
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            // 
            // lbIdcard
            // 
            resources.ApplyResources(this.lbIdcard, "lbIdcard");
            this.lbIdcard.ForeColor = System.Drawing.Color.DimGray;
            this.lbIdcard.Name = "lbIdcard";
            // 
            // lbFullname
            // 
            resources.ApplyResources(this.lbFullname, "lbFullname");
            this.lbFullname.ForeColor = System.Drawing.Color.DimGray;
            this.lbFullname.Name = "lbFullname";
            // 
            // txtIdcardSelect
            // 
            resources.ApplyResources(this.txtIdcardSelect, "txtIdcardSelect");
            this.txtIdcardSelect.ForeColor = System.Drawing.Color.DimGray;
            this.txtIdcardSelect.Name = "txtIdcardSelect";
            // 
            // btnLoadCard
            // 
            this.btnLoadCard.BackColor = System.Drawing.Color.BlanchedAlmond;
            resources.ApplyResources(this.btnLoadCard, "btnLoadCard");
            this.btnLoadCard.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnLoadCard.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.btnLoadCard.Name = "btnLoadCard";
            this.btnLoadCard.Style = MetroFramework.MetroColorStyle.Teal;
            this.btnLoadCard.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnLoadCard.UseCustomBackColor = true;
            this.btnLoadCard.UseSelectable = true;
            this.btnLoadCard.Click += new System.EventHandler(this.btnLoadCard_Click);
            // 
            // StarterForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.Movable = false;
            this.Name = "StarterForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitmapCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIconForm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox idCardCbx;
        private MaterialSkin.Controls.MaterialRaisedButton btn_save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox passportCbx;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox bitmapCard;
        private System.Windows.Forms.ComboBox cbo_DeviceName;
        private System.Windows.Forms.Button btn_Load;
        private MetroFramework.Controls.MetroProgressBar progressReadCard;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.Label lbIdcard;
        private System.Windows.Forms.Label lbFullname;
        private System.Windows.Forms.Label txtIdcardSelect;
        private MetroFramework.Controls.MetroButton btnLoadCard;
    }
}

