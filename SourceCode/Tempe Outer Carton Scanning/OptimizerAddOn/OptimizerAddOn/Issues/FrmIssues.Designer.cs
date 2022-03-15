
namespace OptimizerAddOn.Issues
{
    partial class FrmIssues
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
            this.cbExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.grdAssignedStock = new DevExpress.XtraGrid.GridControl();
            this.grdAssignedStockV1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cbLoadDemand = new System.Windows.Forms.Button();
            this.cbLoadStock = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbJobcardNo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxStoreCode = new System.Windows.Forms.ComboBox();
            this.tbStoreCode = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.grdJobcardDemand = new DevExpress.XtraGrid.GridControl();
            this.grdJobcardDemandV1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cbIssuestock = new System.Windows.Forms.Button();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxLocations = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignedStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignedStockV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobcardDemand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobcardDemandV1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1066, 100);
            this.panel1.TabIndex = 5;
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
            // cbExit
            // 
            this.cbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExit.Location = new System.Drawing.Point(966, 498);
            this.cbExit.Name = "cbExit";
            this.cbExit.Size = new System.Drawing.Size(89, 44);
            this.cbExit.TabIndex = 7;
            this.cbExit.Text = "E&xit";
            this.cbExit.UseVisualStyleBackColor = true;
            this.cbExit.Click += new System.EventHandler(this.cbExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblFrom);
            this.groupBox1.Controls.Add(this.cbLoadDemand);
            this.groupBox1.Controls.Add(this.cbLoadStock);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbJobcardNo);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxStoreCode);
            this.groupBox1.Controls.Add(this.tbStoreCode);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.grdJobcardDemand);
            this.groupBox1.Controls.Add(this.grdAssignedStock);
            this.groupBox1.Controls.Add(this.cbxLocations);
            this.groupBox1.Location = new System.Drawing.Point(12, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1043, 386);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Material Issues";
            // 
            // lblFrom
            // 
            this.lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFrom.Location = new System.Drawing.Point(408, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(171, 23);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "Locations :-";
            // 
            // grdAssignedStock
            // 
            this.grdAssignedStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAssignedStock.Location = new System.Drawing.Point(10, 79);
            this.grdAssignedStock.MainView = this.grdAssignedStockV1;
            this.grdAssignedStock.Name = "grdAssignedStock";
            this.grdAssignedStock.Size = new System.Drawing.Size(1027, 301);
            this.grdAssignedStock.TabIndex = 13;
            this.grdAssignedStock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdAssignedStockV1});
            // 
            // grdAssignedStockV1
            // 
            this.grdAssignedStockV1.GridControl = this.grdAssignedStock;
            this.grdAssignedStockV1.Name = "grdAssignedStockV1";
            this.grdAssignedStockV1.OptionsView.EnableAppearanceEvenRow = true;
            this.grdAssignedStockV1.OptionsView.ShowAutoFilterRow = true;
            this.grdAssignedStockV1.OptionsView.ShowFooter = true;
            this.grdAssignedStockV1.OptionsView.ShowGroupPanel = false;
            // 
            // cbLoadDemand
            // 
            this.cbLoadDemand.Location = new System.Drawing.Point(793, 18);
            this.cbLoadDemand.Name = "cbLoadDemand";
            this.cbLoadDemand.Size = new System.Drawing.Size(202, 50);
            this.cbLoadDemand.TabIndex = 12;
            this.cbLoadDemand.Text = "Load Stock of Jobcard";
            this.cbLoadDemand.UseVisualStyleBackColor = true;
            this.cbLoadDemand.Click += new System.EventHandler(this.cbLoadDemand_Click);
            // 
            // cbLoadStock
            // 
            this.cbLoadStock.Location = new System.Drawing.Point(332, 18);
            this.cbLoadStock.Name = "cbLoadStock";
            this.cbLoadStock.Size = new System.Drawing.Size(70, 50);
            this.cbLoadStock.TabIndex = 11;
            this.cbLoadStock.Text = "Load Stock";
            this.cbLoadStock.UseVisualStyleBackColor = true;
            this.cbLoadStock.Click += new System.EventHandler(this.cbLoadStock_Click);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(10, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Store Code :-";
            // 
            // tbJobcardNo
            // 
            this.tbJobcardNo.BackColor = System.Drawing.Color.White;
            this.tbJobcardNo.Location = new System.Drawing.Point(587, 45);
            this.tbJobcardNo.MaxLength = 22;
            this.tbJobcardNo.Name = "tbJobcardNo";
            this.tbJobcardNo.Size = new System.Drawing.Size(200, 23);
            this.tbJobcardNo.TabIndex = 5;
            // 
            // lblTo
            // 
            this.lblTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTo.Location = new System.Drawing.Point(408, 45);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(171, 23);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "Jobcard No.";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date :-";
            // 
            // cbxStoreCode
            // 
            this.cbxStoreCode.FormattingEnabled = true;
            this.cbxStoreCode.Location = new System.Drawing.Point(126, 44);
            this.cbxStoreCode.Name = "cbxStoreCode";
            this.cbxStoreCode.Size = new System.Drawing.Size(200, 24);
            this.cbxStoreCode.TabIndex = 3;
            // 
            // tbStoreCode
            // 
            this.tbStoreCode.BackColor = System.Drawing.Color.White;
            this.tbStoreCode.Location = new System.Drawing.Point(126, 44);
            this.tbStoreCode.Name = "tbStoreCode";
            this.tbStoreCode.ReadOnly = true;
            this.tbStoreCode.Size = new System.Drawing.Size(200, 23);
            this.tbStoreCode.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(126, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // grdJobcardDemand
            // 
            this.grdJobcardDemand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdJobcardDemand.Location = new System.Drawing.Point(10, 79);
            this.grdJobcardDemand.MainView = this.grdJobcardDemandV1;
            this.grdJobcardDemand.Name = "grdJobcardDemand";
            this.grdJobcardDemand.Size = new System.Drawing.Size(1027, 301);
            this.grdJobcardDemand.TabIndex = 14;
            this.grdJobcardDemand.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdJobcardDemandV1});
            // 
            // grdJobcardDemandV1
            // 
            this.grdJobcardDemandV1.GridControl = this.grdJobcardDemand;
            this.grdJobcardDemandV1.Name = "grdJobcardDemandV1";
            this.grdJobcardDemandV1.OptionsView.EnableAppearanceEvenRow = true;
            this.grdJobcardDemandV1.OptionsView.ShowAutoFilterRow = true;
            this.grdJobcardDemandV1.OptionsView.ShowFooter = true;
            this.grdJobcardDemandV1.OptionsView.ShowGroupPanel = false;
            this.grdJobcardDemandV1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grdJobcardDemandV1_FocusedRowChanged);
            // 
            // cbIssuestock
            // 
            this.cbIssuestock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIssuestock.Enabled = false;
            this.cbIssuestock.Location = new System.Drawing.Point(874, 498);
            this.cbIssuestock.Name = "cbIssuestock";
            this.cbIssuestock.Size = new System.Drawing.Size(89, 44);
            this.cbIssuestock.TabIndex = 13;
            this.cbIssuestock.Text = "Issue";
            this.cbIssuestock.UseVisualStyleBackColor = true;
            this.cbIssuestock.Click += new System.EventHandler(this.cbIssuestock_Click);
            // 
            // tbRemarks
            // 
            this.tbRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbRemarks.BackColor = System.Drawing.Color.White;
            this.tbRemarks.Location = new System.Drawing.Point(108, 495);
            this.tbRemarks.MaxLength = 200;
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(748, 23);
            this.tbRemarks.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(12, 495);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 23);
            this.label5.TabIndex = 14;
            this.label5.Text = "Remarks :-";
            // 
            // cbxLocations
            // 
            this.cbxLocations.FormattingEnabled = true;
            this.cbxLocations.Location = new System.Drawing.Point(587, 18);
            this.cbxLocations.Name = "cbxLocations";
            this.cbxLocations.Size = new System.Drawing.Size(200, 24);
            this.cbxLocations.TabIndex = 15;
            // 
            // FrmIssues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tbRemarks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbIssuestock);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbExit);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmIssues";
            this.Text = "FrmIssues";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmIssues_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignedStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignedStockV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobcardDemand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobcardDemandV1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cbExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStoreCode;
        private System.Windows.Forms.ComboBox cbxStoreCode;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.TextBox tbJobcardNo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button cbLoadDemand;
        private System.Windows.Forms.Button cbLoadStock;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraGrid.GridControl grdAssignedStock;
        private DevExpress.XtraGrid.Views.Grid.GridView grdAssignedStockV1;
        private System.Windows.Forms.Button cbIssuestock;
        private DevExpress.XtraGrid.GridControl grdJobcardDemand;
        private DevExpress.XtraGrid.Views.Grid.GridView grdJobcardDemandV1;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxLocations;
    }
}