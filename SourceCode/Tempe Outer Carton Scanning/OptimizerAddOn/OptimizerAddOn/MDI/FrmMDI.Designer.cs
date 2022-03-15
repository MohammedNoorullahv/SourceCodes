
namespace OptimizerAddOn.MDI
{
    partial class FrmMDI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nBarPacking = new DevExpress.XtraNavBar.NavBarControl();
            this.MMDashBoard = new DevExpress.XtraNavBar.NavBarGroup();
            this.SMOrderOutstanding = new DevExpress.XtraNavBar.NavBarItem();
            this.MMPacking = new DevExpress.XtraNavBar.NavBarGroup();
            this.smGeneratePackingList = new DevExpress.XtraNavBar.NavBarItem();
            this.smTempeScanning = new DevExpress.XtraNavBar.NavBarItem();
            this.MMHR = new DevExpress.XtraNavBar.NavBarGroup();
            this.SMImportSalary = new DevExpress.XtraNavBar.NavBarItem();
            this.nBarMain = new DevExpress.XtraNavBar.NavBarControl();
            this.MMTransfer = new DevExpress.XtraNavBar.NavBarGroup();
            this.SMUnAssignedStck = new DevExpress.XtraNavBar.NavBarItem();
            this.SMSalesOrder = new DevExpress.XtraNavBar.NavBarItem();
            this.MMIssues = new DevExpress.XtraNavBar.NavBarGroup();
            this.SMJobcardIssue = new DevExpress.XtraNavBar.NavBarItem();
            this.cbExit = new System.Windows.Forms.Button();
            this.plSystemConfiguration = new System.Windows.Forms.Panel();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.tbStoreCode = new System.Windows.Forms.TextBox();
            this.tbAPLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSystemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.plMdi = new System.Windows.Forms.Panel();
            this.pnlMdi = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBarPacking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBarMain)).BeginInit();
            this.plSystemConfiguration.SuspendLayout();
            this.plMdi.SuspendLayout();
            this.pnlMdi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 100);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::OptimizerAddOn.Properties.Resources.AH_GROUP;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.nBarPacking);
            this.panel2.Controls.Add(this.nBarMain);
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 459);
            this.panel2.TabIndex = 5;
            // 
            // nBarPacking
            // 
            this.nBarPacking.ActiveGroup = this.MMDashBoard;
            this.nBarPacking.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.nBarPacking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nBarPacking.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.MMPacking,
            this.MMHR,
            this.MMDashBoard});
            this.nBarPacking.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.smTempeScanning,
            this.smGeneratePackingList,
            this.SMImportSalary,
            this.SMOrderOutstanding});
            this.nBarPacking.Location = new System.Drawing.Point(0, 0);
            this.nBarPacking.LookAndFeel.SkinName = "Visual Studio 2013 Dark";
            this.nBarPacking.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.nBarPacking.LookAndFeel.UseDefaultLookAndFeel = false;
            this.nBarPacking.LookAndFeel.UseWindowsXPTheme = true;
            this.nBarPacking.Name = "nBarPacking";
            this.nBarPacking.OptionsNavPane.ExpandedWidth = 196;
            this.nBarPacking.Size = new System.Drawing.Size(196, 455);
            this.nBarPacking.TabIndex = 8;
            this.nBarPacking.Text = "navBarControl1";
            this.nBarPacking.View = new DevExpress.XtraNavBar.ViewInfo.NavigationPaneViewInfoRegistrator();
            // 
            // MMDashBoard
            // 
            this.MMDashBoard.Caption = "DashBoard";
            this.MMDashBoard.Expanded = true;
            this.MMDashBoard.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.SMOrderOutstanding)});
            this.MMDashBoard.Name = "MMDashBoard";
            // 
            // SMOrderOutstanding
            // 
            this.SMOrderOutstanding.Caption = "Order Outstanding";
            this.SMOrderOutstanding.Name = "SMOrderOutstanding";
            this.SMOrderOutstanding.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.SMOrderOutstanding_LinkClicked);
            // 
            // MMPacking
            // 
            this.MMPacking.Caption = "Packing";
            this.MMPacking.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.smGeneratePackingList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.smTempeScanning)});
            this.MMPacking.Name = "MMPacking";
            // 
            // smGeneratePackingList
            // 
            this.smGeneratePackingList.Caption = "Generate Packing List";
            this.smGeneratePackingList.Name = "smGeneratePackingList";
            this.smGeneratePackingList.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.smGeneratePackingList_LinkClicked);
            // 
            // smTempeScanning
            // 
            this.smTempeScanning.Caption = "TEMPE Scanning";
            this.smTempeScanning.Name = "smTempeScanning";
            this.smTempeScanning.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.smTempeScanning_LinkClicked);
            // 
            // MMHR
            // 
            this.MMHR.Caption = "HR";
            this.MMHR.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.SMImportSalary)});
            this.MMHR.Name = "MMHR";
            // 
            // SMImportSalary
            // 
            this.SMImportSalary.Caption = "Import Salary";
            this.SMImportSalary.Name = "SMImportSalary";
            this.SMImportSalary.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.SMImportSalary_LinkClicked);
            // 
            // nBarMain
            // 
            this.nBarMain.ActiveGroup = this.MMTransfer;
            this.nBarMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nBarMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.MMTransfer,
            this.MMIssues});
            this.nBarMain.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.SMUnAssignedStck,
            this.SMSalesOrder,
            this.SMJobcardIssue});
            this.nBarMain.Location = new System.Drawing.Point(0, 0);
            this.nBarMain.LookAndFeel.SkinName = "Visual Studio 2013 Dark";
            this.nBarMain.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.nBarMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.nBarMain.LookAndFeel.UseWindowsXPTheme = true;
            this.nBarMain.Name = "nBarMain";
            this.nBarMain.OptionsNavPane.ExpandedWidth = 196;
            this.nBarMain.Size = new System.Drawing.Size(196, 455);
            this.nBarMain.TabIndex = 7;
            this.nBarMain.Text = "navBarControl1";
            this.nBarMain.View = new DevExpress.XtraNavBar.ViewInfo.AdvExplorerBarViewInfoRegistrator();
            // 
            // MMTransfer
            // 
            this.MMTransfer.Caption = "Transfer\'s";
            this.MMTransfer.Expanded = true;
            this.MMTransfer.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.SMUnAssignedStck),
            new DevExpress.XtraNavBar.NavBarItemLink(this.SMSalesOrder)});
            this.MMTransfer.Name = "MMTransfer";
            // 
            // SMUnAssignedStck
            // 
            this.SMUnAssignedStck.Caption = "Unassiged Stock to Sales Order";
            this.SMUnAssignedStck.Name = "SMUnAssignedStck";
            this.SMUnAssignedStck.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.SMUnAssignedStck_LinkClicked);
            // 
            // SMSalesOrder
            // 
            this.SMSalesOrder.Caption = "Sales Order to Sales Order";
            this.SMSalesOrder.Name = "SMSalesOrder";
            this.SMSalesOrder.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.SMSalesOrder_LinkClicked);
            // 
            // MMIssues
            // 
            this.MMIssues.Caption = "Issue\'s";
            this.MMIssues.Expanded = true;
            this.MMIssues.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.SMJobcardIssue)});
            this.MMIssues.Name = "MMIssues";
            // 
            // SMJobcardIssue
            // 
            this.SMJobcardIssue.Caption = "Jobcard Issue";
            this.SMJobcardIssue.Name = "SMJobcardIssue";
            this.SMJobcardIssue.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.SMJobcardIssue_LinkClicked);
            // 
            // cbExit
            // 
            this.cbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExit.Location = new System.Drawing.Point(863, 503);
            this.cbExit.Name = "cbExit";
            this.cbExit.Size = new System.Drawing.Size(89, 44);
            this.cbExit.TabIndex = 6;
            this.cbExit.Text = "E&xit";
            this.cbExit.UseVisualStyleBackColor = true;
            this.cbExit.Click += new System.EventHandler(this.cbExit_Click);
            // 
            // plSystemConfiguration
            // 
            this.plSystemConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plSystemConfiguration.Controls.Add(this.tbIPAddress);
            this.plSystemConfiguration.Controls.Add(this.tbStoreCode);
            this.plSystemConfiguration.Controls.Add(this.tbAPLogin);
            this.plSystemConfiguration.Controls.Add(this.label4);
            this.plSystemConfiguration.Controls.Add(this.label3);
            this.plSystemConfiguration.Controls.Add(this.label1);
            this.plSystemConfiguration.Controls.Add(this.tbSystemName);
            this.plSystemConfiguration.Controls.Add(this.label2);
            this.plSystemConfiguration.Location = new System.Drawing.Point(205, 477);
            this.plSystemConfiguration.Name = "plSystemConfiguration";
            this.plSystemConfiguration.Size = new System.Drawing.Size(652, 72);
            this.plSystemConfiguration.TabIndex = 8;
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(124, 37);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.ReadOnly = true;
            this.tbIPAddress.Size = new System.Drawing.Size(200, 23);
            this.tbIPAddress.TabIndex = 8;
            // 
            // tbStoreCode
            // 
            this.tbStoreCode.Location = new System.Drawing.Point(444, 39);
            this.tbStoreCode.Name = "tbStoreCode";
            this.tbStoreCode.ReadOnly = true;
            this.tbStoreCode.Size = new System.Drawing.Size(200, 23);
            this.tbStoreCode.TabIndex = 7;
            // 
            // tbAPLogin
            // 
            this.tbAPLogin.Location = new System.Drawing.Point(444, 12);
            this.tbAPLogin.Name = "tbAPLogin";
            this.tbAPLogin.ReadOnly = true;
            this.tbAPLogin.Size = new System.Drawing.Size(200, 23);
            this.tbAPLogin.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(330, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Store Code";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(330, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "AP Login";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(10, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP Address";
            // 
            // tbSystemName
            // 
            this.tbSystemName.Location = new System.Drawing.Point(124, 10);
            this.tbSystemName.Name = "tbSystemName";
            this.tbSystemName.ReadOnly = true;
            this.tbSystemName.Size = new System.Drawing.Size(200, 23);
            this.tbSystemName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "System Name";
            // 
            // plMdi
            // 
            this.plMdi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plMdi.Controls.Add(this.pnlMdi);
            this.plMdi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMdi.Location = new System.Drawing.Point(0, 0);
            this.plMdi.Name = "plMdi";
            this.plMdi.Size = new System.Drawing.Size(968, 558);
            this.plMdi.TabIndex = 10;
            this.plMdi.Paint += new System.Windows.Forms.PaintEventHandler(this.plMdi_Paint);
            // 
            // pnlMdi
            // 
            this.pnlMdi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMdi.BackColor = System.Drawing.Color.White;
            this.pnlMdi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMdi.Controls.Add(this.label5);
            this.pnlMdi.Controls.Add(this.panel1);
            this.pnlMdi.Controls.Add(this.panel2);
            this.pnlMdi.Controls.Add(this.cbExit);
            this.pnlMdi.Controls.Add(this.plSystemConfiguration);
            this.pnlMdi.Controls.Add(this.pictureBox2);
            this.pnlMdi.Location = new System.Drawing.Point(0, 0);
            this.pnlMdi.Name = "pnlMdi";
            this.pnlMdi.Size = new System.Drawing.Size(972, 597);
            this.pnlMdi.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(205, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "HR Upload V2.0 - 05-Jan-2021";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::OptimizerAddOn.Properties.Resources.AH_GROUP;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 100);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // FrmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 558);
            this.Controls.Add(this.plMdi);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMDI";
            this.Text = "FrmMDI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMDI_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nBarPacking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBarMain)).EndInit();
            this.plSystemConfiguration.ResumeLayout(false);
            this.plSystemConfiguration.PerformLayout();
            this.plMdi.ResumeLayout(false);
            this.pnlMdi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cbExit;
        private DevExpress.XtraNavBar.NavBarControl nBarMain;
        private DevExpress.XtraNavBar.NavBarGroup MMTransfer;
        private DevExpress.XtraNavBar.NavBarItem SMUnAssignedStck;
        private DevExpress.XtraNavBar.NavBarItem SMSalesOrder;
        private DevExpress.XtraNavBar.NavBarGroup MMIssues;
        private DevExpress.XtraNavBar.NavBarItem SMJobcardIssue;
        private System.Windows.Forms.Panel plSystemConfiguration;
        private System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.TextBox tbStoreCode;
        private System.Windows.Forms.TextBox tbAPLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSystemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel plMdi;
        private System.Windows.Forms.Panel pnlMdi;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraNavBar.NavBarControl nBarPacking;
        private DevExpress.XtraNavBar.NavBarGroup MMPacking;
        private DevExpress.XtraNavBar.NavBarItem smTempeScanning;
        private DevExpress.XtraNavBar.NavBarItem smGeneratePackingList;
        private DevExpress.XtraNavBar.NavBarGroup MMHR;
        private DevExpress.XtraNavBar.NavBarItem SMImportSalary;
        private DevExpress.XtraNavBar.NavBarGroup MMDashBoard;
        private DevExpress.XtraNavBar.NavBarItem SMOrderOutstanding;
    }
}



