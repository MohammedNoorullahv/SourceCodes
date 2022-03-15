namespace SDP_MCDApplicationv1inCSharp.Forms
{
    partial class FrmStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.plScanning = new System.Windows.Forms.Panel();
            this.tbQuantity = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.lbScannedBoxes = new System.Windows.Forms.Label();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.tbLastBarcode = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cbExitBarcodeScanning = new System.Windows.Forms.Button();
            this.cbBack = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblScanning = new System.Windows.Forms.Label();
            this.rbFinishingSection = new System.Windows.Forms.RadioButton();
            this.rbPackingSection = new System.Windows.Forms.RadioButton();
            this.tbBoxCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.plScanning.SuspendLayout();
            this.SuspendLayout();
            // 
            // plScanning
            // 
            this.plScanning.BackColor = System.Drawing.Color.White;
            this.plScanning.Controls.Add(this.tbBoxCount);
            this.plScanning.Controls.Add(this.label1);
            this.plScanning.Controls.Add(this.rbPackingSection);
            this.plScanning.Controls.Add(this.rbFinishingSection);
            this.plScanning.Controls.Add(this.tbQuantity);
            this.plScanning.Controls.Add(this.Label8);
            this.plScanning.Controls.Add(this.tbBarcode);
            this.plScanning.Controls.Add(this.lbScannedBoxes);
            this.plScanning.Controls.Add(this.tbSize);
            this.plScanning.Controls.Add(this.Label7);
            this.plScanning.Controls.Add(this.tbLastBarcode);
            this.plScanning.Controls.Add(this.Label5);
            this.plScanning.Controls.Add(this.cbExitBarcodeScanning);
            this.plScanning.Controls.Add(this.cbBack);
            this.plScanning.Controls.Add(this.Label9);
            this.plScanning.Controls.Add(this.lblScanning);
            this.plScanning.Location = new System.Drawing.Point(0, 0);
            this.plScanning.Name = "plScanning";
            this.plScanning.Size = new System.Drawing.Size(234, 260);
            // 
            // tbQuantity
            // 
            this.tbQuantity.Location = new System.Drawing.Point(152, 203);
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(75, 23);
            this.tbQuantity.TabIndex = 18;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(152, 181);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(76, 23);
            this.Label8.Text = "Quantity :-";
            // 
            // tbBarcode
            // 
            this.tbBarcode.Location = new System.Drawing.Point(4, 109);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.Size = new System.Drawing.Size(223, 23);
            this.tbBarcode.TabIndex = 16;
            this.tbBarcode.TextChanged += new System.EventHandler(this.tbBarcode_TextChanged);
            this.tbBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBarcode_KeyPress);
            // 
            // lbScannedBoxes
            // 
            this.lbScannedBoxes.Location = new System.Drawing.Point(86, 91);
            this.lbScannedBoxes.Name = "lbScannedBoxes";
            this.lbScannedBoxes.Size = new System.Drawing.Size(141, 23);
            this.lbScannedBoxes.Text = "Scanned Barcode :-";
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(6, 203);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(75, 23);
            this.tbSize.TabIndex = 28;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(4, 183);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(76, 23);
            this.Label7.Text = "Size :-";
            // 
            // tbLastBarcode
            // 
            this.tbLastBarcode.Location = new System.Drawing.Point(4, 157);
            this.tbLastBarcode.Name = "tbLastBarcode";
            this.tbLastBarcode.Size = new System.Drawing.Size(223, 23);
            this.tbLastBarcode.TabIndex = 26;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(4, 135);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(128, 23);
            this.Label5.Text = "LAST BARCODE :-";
            // 
            // cbExitBarcodeScanning
            // 
            this.cbExitBarcodeScanning.Location = new System.Drawing.Point(154, 232);
            this.cbExitBarcodeScanning.Name = "cbExitBarcodeScanning";
            this.cbExitBarcodeScanning.Size = new System.Drawing.Size(75, 23);
            this.cbExitBarcodeScanning.TabIndex = 22;
            this.cbExitBarcodeScanning.Text = "Exit";
            this.cbExitBarcodeScanning.Click += new System.EventHandler(this.cbExitBarcodeScanning_Click);
            // 
            // cbBack
            // 
            this.cbBack.Enabled = false;
            this.cbBack.Location = new System.Drawing.Point(7, 232);
            this.cbBack.Name = "cbBack";
            this.cbBack.Size = new System.Drawing.Size(75, 23);
            this.cbBack.TabIndex = 20;
            this.cbBack.Text = "Back";
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(4, 91);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(76, 23);
            this.Label9.Text = "BARCODE :-";
            // 
            // lblScanning
            // 
            this.lblScanning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblScanning.ForeColor = System.Drawing.Color.Red;
            this.lblScanning.Location = new System.Drawing.Point(1, 1);
            this.lblScanning.Name = "lblScanning";
            this.lblScanning.Size = new System.Drawing.Size(230, 25);
            this.lblScanning.Text = "BARCODE SCANNING";
            this.lblScanning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rbFinishingSection
            // 
            this.rbFinishingSection.Checked = true;
            this.rbFinishingSection.Location = new System.Drawing.Point(7, 29);
            this.rbFinishingSection.Name = "rbFinishingSection";
            this.rbFinishingSection.Size = new System.Drawing.Size(132, 20);
            this.rbFinishingSection.TabIndex = 43;
            this.rbFinishingSection.Text = "Finishing Section";
            // 
            // rbPackingSection
            // 
            this.rbPackingSection.Location = new System.Drawing.Point(7, 55);
            this.rbPackingSection.Name = "rbPackingSection";
            this.rbPackingSection.Size = new System.Drawing.Size(132, 20);
            this.rbPackingSection.TabIndex = 44;
            this.rbPackingSection.TabStop = false;
            this.rbPackingSection.Text = "Packing Section";
            // 
            // tbBoxCount
            // 
            this.tbBoxCount.Location = new System.Drawing.Point(85, 203);
            this.tbBoxCount.Name = "tbBoxCount";
            this.tbBoxCount.Size = new System.Drawing.Size(64, 23);
            this.tbBoxCount.TabIndex = 46;
            this.tbBoxCount.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(84, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 23);
            this.label1.Text = "Box Count";
            // 
            // FrmStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(237, 272);
            this.Controls.Add(this.plScanning);
            this.Menu = this.mainMenu1;
            this.Name = "FrmStock";
            this.Text = "FrmScanning";
            this.plScanning.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel plScanning;
        internal System.Windows.Forms.TextBox tbSize;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox tbLastBarcode;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Button cbExitBarcodeScanning;
        internal System.Windows.Forms.Button cbBack;
        internal System.Windows.Forms.TextBox tbQuantity;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox tbBarcode;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label lblScanning;
        internal System.Windows.Forms.Label lbScannedBoxes;
        internal System.Windows.Forms.TextBox tbBoxCount;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPackingSection;
        private System.Windows.Forms.RadioButton rbFinishingSection;
    }
}