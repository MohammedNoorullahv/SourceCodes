
namespace OptimizerAddOn.Packing
{
    partial class FrmTempeScanning
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
            this.label5 = new System.Windows.Forms.Label();
            this.cbTransferStock = new System.Windows.Forms.Button();
            this.lblFrom = new System.Windows.Forms.Label();
            this.grdPackingStatus = new DevExpress.XtraGrid.GridControl();
            this.grdPackingStatusV1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbBoxBalQty = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbBoxScndQty = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbBoxQty = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbBalBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbScannedBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbTotalBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbInnerBarcode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbOuterBarcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBoxNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBalQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbScannedQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTotalQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSalesOrderNo = new System.Windows.Forms.TextBox();
            this.tbJobcardNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dpScanDate = new System.Windows.Forms.DateTimePicker();
            this.cbExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdPackingStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPackingStatusV1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(16, 617);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 28);
            this.label5.TabIndex = 20;
            this.label5.Text = "Remarks :-";
            this.label5.Visible = false;
            // 
            // cbTransferStock
            // 
            this.cbTransferStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTransferStock.Enabled = false;
            this.cbTransferStock.Location = new System.Drawing.Point(1120, 620);
            this.cbTransferStock.Margin = new System.Windows.Forms.Padding(4);
            this.cbTransferStock.Name = "cbTransferStock";
            this.cbTransferStock.Size = new System.Drawing.Size(119, 54);
            this.cbTransferStock.TabIndex = 19;
            this.cbTransferStock.Text = "Transfer Stock";
            this.cbTransferStock.UseVisualStyleBackColor = true;
            this.cbTransferStock.Visible = false;
            // 
            // lblFrom
            // 
            this.lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFrom.Location = new System.Drawing.Point(214, 22);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(96, 23);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "Jobcard No:-";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grdPackingStatus
            // 
            this.grdPackingStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPackingStatus.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grdPackingStatus.Location = new System.Drawing.Point(13, 109);
            this.grdPackingStatus.MainView = this.grdPackingStatusV1;
            this.grdPackingStatus.Margin = new System.Windows.Forms.Padding(4);
            this.grdPackingStatus.Name = "grdPackingStatus";
            this.grdPackingStatus.Size = new System.Drawing.Size(1325, 358);
            this.grdPackingStatus.TabIndex = 13;
            this.grdPackingStatus.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdPackingStatusV1});
            // 
            // grdPackingStatusV1
            // 
            this.grdPackingStatusV1.Appearance.Row.Options.UseTextOptions = true;
            this.grdPackingStatusV1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdPackingStatusV1.GridControl = this.grdPackingStatus;
            this.grdPackingStatusV1.Name = "grdPackingStatusV1";
            this.grdPackingStatusV1.OptionsView.EnableAppearanceEvenRow = true;
            this.grdPackingStatusV1.OptionsView.ShowAutoFilterRow = true;
            this.grdPackingStatusV1.OptionsView.ShowFooter = true;
            this.grdPackingStatusV1.OptionsView.ShowGroupPanel = false;
            this.grdPackingStatusV1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdPackingStatusV1_CustomDrawCell);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.tbBoxBalQty);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tbBoxScndQty);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.tbBoxQty);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbBalBox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbScannedBox);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tbTotalBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbInnerBarcode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbOuterBarcode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbBoxNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbBalQty);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbScannedQty);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbTotalQty);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSalesOrderNo);
            this.groupBox1.Controls.Add(this.lblFrom);
            this.groupBox1.Controls.Add(this.tbJobcardNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dpScanDate);
            this.groupBox1.Controls.Add(this.grdPackingStatus);
            this.groupBox1.Location = new System.Drawing.Point(6, 113);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1347, 475);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jobcard Info";
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(1117, 78);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 23);
            this.label13.TabIndex = 39;
            this.label13.Text = "Bal Qty :-";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBoxBalQty
            // 
            this.tbBoxBalQty.BackColor = System.Drawing.Color.White;
            this.tbBoxBalQty.Location = new System.Drawing.Point(1215, 78);
            this.tbBoxBalQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbBoxBalQty.MaxLength = 22;
            this.tbBoxBalQty.Name = "tbBoxBalQty";
            this.tbBoxBalQty.Size = new System.Drawing.Size(49, 23);
            this.tbBoxBalQty.TabIndex = 40;
            this.tbBoxBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.Location = new System.Drawing.Point(962, 78);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 23);
            this.label14.TabIndex = 37;
            this.label14.Text = "Scn\'d Qty :-";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBoxScndQty
            // 
            this.tbBoxScndQty.BackColor = System.Drawing.Color.White;
            this.tbBoxScndQty.Location = new System.Drawing.Point(1060, 78);
            this.tbBoxScndQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbBoxScndQty.MaxLength = 22;
            this.tbBoxScndQty.Name = "tbBoxScndQty";
            this.tbBoxScndQty.Size = new System.Drawing.Size(49, 23);
            this.tbBoxScndQty.TabIndex = 38;
            this.tbBoxScndQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.Location = new System.Drawing.Point(807, 78);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 23);
            this.label15.TabIndex = 35;
            this.label15.Text = "Box Qty:-";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBoxQty
            // 
            this.tbBoxQty.BackColor = System.Drawing.Color.White;
            this.tbBoxQty.Location = new System.Drawing.Point(905, 78);
            this.tbBoxQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbBoxQty.MaxLength = 22;
            this.tbBoxQty.Name = "tbBoxQty";
            this.tbBoxQty.Size = new System.Drawing.Size(49, 23);
            this.tbBoxQty.TabIndex = 36;
            this.tbBoxQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(1117, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "Bal Box :-";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBalBox
            // 
            this.tbBalBox.BackColor = System.Drawing.Color.White;
            this.tbBalBox.Location = new System.Drawing.Point(1215, 49);
            this.tbBalBox.Margin = new System.Windows.Forms.Padding(4);
            this.tbBalBox.MaxLength = 22;
            this.tbBalBox.Name = "tbBalBox";
            this.tbBalBox.Size = new System.Drawing.Size(49, 23);
            this.tbBalBox.TabIndex = 34;
            this.tbBalBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(962, 49);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 23);
            this.label11.TabIndex = 31;
            this.label11.Text = "Scn\'d Box :-";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbScannedBox
            // 
            this.tbScannedBox.BackColor = System.Drawing.Color.White;
            this.tbScannedBox.Location = new System.Drawing.Point(1060, 49);
            this.tbScannedBox.Margin = new System.Windows.Forms.Padding(4);
            this.tbScannedBox.MaxLength = 22;
            this.tbScannedBox.Name = "tbScannedBox";
            this.tbScannedBox.Size = new System.Drawing.Size(49, 23);
            this.tbScannedBox.TabIndex = 32;
            this.tbScannedBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(807, 49);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 23);
            this.label12.TabIndex = 29;
            this.label12.Text = "Total Box :-";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTotalBox
            // 
            this.tbTotalBox.BackColor = System.Drawing.Color.White;
            this.tbTotalBox.Location = new System.Drawing.Point(905, 49);
            this.tbTotalBox.Margin = new System.Windows.Forms.Padding(4);
            this.tbTotalBox.MaxLength = 22;
            this.tbTotalBox.Name = "tbTotalBox";
            this.tbTotalBox.Size = new System.Drawing.Size(49, 23);
            this.tbTotalBox.TabIndex = 30;
            this.tbTotalBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(499, 49);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 23);
            this.label9.TabIndex = 27;
            this.label9.Text = "Inner Barcode :-";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbInnerBarcode
            // 
            this.tbInnerBarcode.BackColor = System.Drawing.Color.White;
            this.tbInnerBarcode.Location = new System.Drawing.Point(631, 49);
            this.tbInnerBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.tbInnerBarcode.MaxLength = 22;
            this.tbInnerBarcode.Name = "tbInnerBarcode";
            this.tbInnerBarcode.Size = new System.Drawing.Size(168, 23);
            this.tbInnerBarcode.TabIndex = 28;
            this.tbInnerBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInnerBarcode_KeyPress);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(187, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 23);
            this.label8.TabIndex = 25;
            this.label8.Text = "Outer Barcode :-";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbOuterBarcode
            // 
            this.tbOuterBarcode.BackColor = System.Drawing.Color.White;
            this.tbOuterBarcode.Location = new System.Drawing.Point(314, 49);
            this.tbOuterBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.tbOuterBarcode.MaxLength = 22;
            this.tbOuterBarcode.Name = "tbOuterBarcode";
            this.tbOuterBarcode.Size = new System.Drawing.Size(181, 23);
            this.tbOuterBarcode.TabIndex = 26;
            this.tbOuterBarcode.TextChanged += new System.EventHandler(this.tbOuterBarcode_TextChanged);
            this.tbOuterBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOuterBarcode_KeyPress);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(13, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 23);
            this.label4.TabIndex = 23;
            this.label4.Text = "Box No.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBoxNo
            // 
            this.tbBoxNo.BackColor = System.Drawing.Color.White;
            this.tbBoxNo.Location = new System.Drawing.Point(81, 49);
            this.tbBoxNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbBoxNo.MaxLength = 22;
            this.tbBoxNo.Name = "tbBoxNo";
            this.tbBoxNo.Size = new System.Drawing.Size(49, 23);
            this.tbBoxNo.TabIndex = 24;
            this.tbBoxNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbBoxNo.TextChanged += new System.EventHandler(this.tbBoxNo_TextChanged);
            this.tbBoxNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBoxNo_KeyPress);
            this.tbBoxNo.Leave += new System.EventHandler(this.tbBoxNo_Leave);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(1117, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 23);
            this.label7.TabIndex = 21;
            this.label7.Text = "Bal Qty :-";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBalQty
            // 
            this.tbBalQty.BackColor = System.Drawing.Color.White;
            this.tbBalQty.Location = new System.Drawing.Point(1215, 20);
            this.tbBalQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbBalQty.MaxLength = 22;
            this.tbBalQty.Name = "tbBalQty";
            this.tbBalQty.Size = new System.Drawing.Size(49, 23);
            this.tbBalQty.TabIndex = 22;
            this.tbBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(962, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 23);
            this.label6.TabIndex = 19;
            this.label6.Text = "Scn\'d Qty :-";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbScannedQty
            // 
            this.tbScannedQty.BackColor = System.Drawing.Color.White;
            this.tbScannedQty.Location = new System.Drawing.Point(1060, 20);
            this.tbScannedQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbScannedQty.MaxLength = 22;
            this.tbScannedQty.Name = "tbScannedQty";
            this.tbScannedQty.Size = new System.Drawing.Size(49, 23);
            this.tbScannedQty.TabIndex = 20;
            this.tbScannedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(807, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 23);
            this.label3.TabIndex = 17;
            this.label3.Text = "Total Qty :-";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTotalQty
            // 
            this.tbTotalQty.BackColor = System.Drawing.Color.White;
            this.tbTotalQty.Location = new System.Drawing.Point(905, 20);
            this.tbTotalQty.Margin = new System.Windows.Forms.Padding(4);
            this.tbTotalQty.MaxLength = 22;
            this.tbTotalQty.Name = "tbTotalQty";
            this.tbTotalQty.Size = new System.Drawing.Size(49, 23);
            this.tbTotalQty.TabIndex = 18;
            this.tbTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(499, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Sales Order No :-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSalesOrderNo
            // 
            this.tbSalesOrderNo.BackColor = System.Drawing.Color.White;
            this.tbSalesOrderNo.Location = new System.Drawing.Point(631, 22);
            this.tbSalesOrderNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbSalesOrderNo.MaxLength = 22;
            this.tbSalesOrderNo.Name = "tbSalesOrderNo";
            this.tbSalesOrderNo.Size = new System.Drawing.Size(168, 23);
            this.tbSalesOrderNo.TabIndex = 16;
            // 
            // tbJobcardNo
            // 
            this.tbJobcardNo.BackColor = System.Drawing.Color.White;
            this.tbJobcardNo.Location = new System.Drawing.Point(314, 22);
            this.tbJobcardNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbJobcardNo.MaxLength = 22;
            this.tbJobcardNo.Name = "tbJobcardNo";
            this.tbJobcardNo.Size = new System.Drawing.Size(181, 23);
            this.tbJobcardNo.TabIndex = 7;
            this.tbJobcardNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbJobcardNo_KeyPress);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(13, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date :-";
            // 
            // dpScanDate
            // 
            this.dpScanDate.CustomFormat = "dd-MMM-yyyy";
            this.dpScanDate.Enabled = false;
            this.dpScanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpScanDate.Location = new System.Drawing.Point(78, 22);
            this.dpScanDate.Margin = new System.Windows.Forms.Padding(4);
            this.dpScanDate.Name = "dpScanDate";
            this.dpScanDate.Size = new System.Drawing.Size(132, 23);
            this.dpScanDate.TabIndex = 9;
            // 
            // cbExit
            // 
            this.cbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExit.Location = new System.Drawing.Point(1243, 620);
            this.cbExit.Margin = new System.Windows.Forms.Padding(4);
            this.cbExit.Name = "cbExit";
            this.cbExit.Size = new System.Drawing.Size(119, 54);
            this.cbExit.TabIndex = 17;
            this.cbExit.Text = "E&xit";
            this.cbExit.UseVisualStyleBackColor = true;
            this.cbExit.Click += new System.EventHandler(this.cbExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::OptimizerAddOn.Properties.Resources.AH_GROUP;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1368, 100);
            this.panel1.TabIndex = 16;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(457, 618);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(175, 16);
            this.label16.TabIndex = 21;
            this.label16.Text = "v1.0 Packing 26-10-2021";
            // 
            // FrmTempeScanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 682);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbTransferStock);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbExit);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmTempeScanning";
            this.Text = "frmTempeScanning";
            this.Load += new System.EventHandler(this.FrmTempeScanning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPackingStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPackingStatusV1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cbTransferStock;
        private System.Windows.Forms.Label lblFrom;
        private DevExpress.XtraGrid.GridControl grdPackingStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView grdPackingStatusV1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbBalBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbScannedBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbTotalBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbInnerBarcode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOuterBarcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBoxNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbBalQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbScannedQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTotalQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSalesOrderNo;
        private System.Windows.Forms.TextBox tbJobcardNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dpScanDate;
        private System.Windows.Forms.Button cbExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbBoxBalQty;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbBoxScndQty;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbBoxQty;
        private System.Windows.Forms.Label label16;
    }
}