namespace BarcodeScann
{
    partial class Scanning
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
            this.lblSection = new System.Windows.Forms.Label();
            this.plScanning = new System.Windows.Forms.Panel();
            this.cbManualSave = new System.Windows.Forms.Button();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.tbLastBarcode = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.chkbxNoBoxAdd = new System.Windows.Forms.CheckBox();
            this.chkbxManualSave = new System.Windows.Forms.CheckBox();
            this.cbExitBarcodeScanning = new System.Windows.Forms.Button();
            this.cbCancel = new System.Windows.Forms.Button();
            this.cbBack = new System.Windows.Forms.Button();
            this.cbSaveBarcode = new System.Windows.Forms.Button();
            this.tbQuantity = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.plScanning.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSection
            // 
            this.lblSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular);
            this.lblSection.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSection.Location = new System.Drawing.Point(0, 0);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(234, 23);
            this.lblSection.Text = "BARCODE SCANNING";
            // 
            // plScanning
            // 
            this.plScanning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.plScanning.Controls.Add(this.cbManualSave);
            this.plScanning.Controls.Add(this.lblSection);
            this.plScanning.Controls.Add(this.tbSize);
            this.plScanning.Controls.Add(this.Label7);
            this.plScanning.Controls.Add(this.tbLastBarcode);
            this.plScanning.Controls.Add(this.Label5);
            this.plScanning.Controls.Add(this.chkbxNoBoxAdd);
            this.plScanning.Controls.Add(this.chkbxManualSave);
            this.plScanning.Controls.Add(this.cbExitBarcodeScanning);
            this.plScanning.Controls.Add(this.cbCancel);
            this.plScanning.Controls.Add(this.cbBack);
            this.plScanning.Controls.Add(this.cbSaveBarcode);
            this.plScanning.Controls.Add(this.tbQuantity);
            this.plScanning.Controls.Add(this.Label8);
            this.plScanning.Controls.Add(this.tbBarcode);
            this.plScanning.Controls.Add(this.Label9);
            this.plScanning.Location = new System.Drawing.Point(0, 0);
            this.plScanning.Name = "plScanning";
            this.plScanning.Size = new System.Drawing.Size(234, 260);
            // 
            // cbManualSave
            // 
            this.cbManualSave.Location = new System.Drawing.Point(4, 206);
            this.cbManualSave.Name = "cbManualSave";
            this.cbManualSave.Size = new System.Drawing.Size(147, 23);
            this.cbManualSave.TabIndex = 29;
            this.cbManualSave.Text = "Manual Save";
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(6, 146);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(84, 23);
            this.tbSize.TabIndex = 28;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(4, 126);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(76, 23);
            this.Label7.Text = "Size :-";
            // 
            // tbLastBarcode
            // 
            this.tbLastBarcode.Location = new System.Drawing.Point(4, 100);
            this.tbLastBarcode.Name = "tbLastBarcode";
            this.tbLastBarcode.Size = new System.Drawing.Size(223, 23);
            this.tbLastBarcode.TabIndex = 26;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(4, 78);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(128, 23);
            this.Label5.Text = "LAST BARCODE :-";
            // 
            // chkbxNoBoxAdd
            // 
            this.chkbxNoBoxAdd.Location = new System.Drawing.Point(7, 180);
            this.chkbxNoBoxAdd.Name = "chkbxNoBoxAdd";
            this.chkbxNoBoxAdd.Size = new System.Drawing.Size(90, 20);
            this.chkbxNoBoxAdd.TabIndex = 24;
            this.chkbxNoBoxAdd.Text = "No Box Add";
            // 
            // chkbxManualSave
            // 
            this.chkbxManualSave.Location = new System.Drawing.Point(134, 181);
            this.chkbxManualSave.Name = "chkbxManualSave";
            this.chkbxManualSave.Size = new System.Drawing.Size(93, 20);
            this.chkbxManualSave.TabIndex = 23;
            this.chkbxManualSave.Text = "Manual Save";
            // 
            // cbExitBarcodeScanning
            // 
            this.cbExitBarcodeScanning.Location = new System.Drawing.Point(154, 232);
            this.cbExitBarcodeScanning.Name = "cbExitBarcodeScanning";
            this.cbExitBarcodeScanning.Size = new System.Drawing.Size(75, 23);
            this.cbExitBarcodeScanning.TabIndex = 22;
            this.cbExitBarcodeScanning.Text = "Exit";
            this.cbExitBarcodeScanning.Click += new System.EventHandler(this.cbExitBarcodeScanning_Click_1);
            // 
            // cbCancel
            // 
            this.cbCancel.Location = new System.Drawing.Point(82, 232);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(69, 23);
            this.cbCancel.TabIndex = 21;
            this.cbCancel.Text = "Cancel";
            // 
            // cbBack
            // 
            this.cbBack.Location = new System.Drawing.Point(154, 206);
            this.cbBack.Name = "cbBack";
            this.cbBack.Size = new System.Drawing.Size(75, 23);
            this.cbBack.TabIndex = 20;
            this.cbBack.Text = "Back";
            this.cbBack.Click += new System.EventHandler(this.cbBack_Click_1);
            // 
            // cbSaveBarcode
            // 
            this.cbSaveBarcode.Location = new System.Drawing.Point(4, 232);
            this.cbSaveBarcode.Name = "cbSaveBarcode";
            this.cbSaveBarcode.Size = new System.Drawing.Size(75, 23);
            this.cbSaveBarcode.TabIndex = 19;
            this.cbSaveBarcode.Text = "Save";
            this.cbSaveBarcode.Click += new System.EventHandler(this.cbSaveBarcode_Click);
            // 
            // tbQuantity
            // 
            this.tbQuantity.Location = new System.Drawing.Point(143, 144);
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(84, 23);
            this.tbQuantity.TabIndex = 18;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(141, 124);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(76, 23);
            this.Label8.Text = "Quantity :-";
            // 
            // tbBarcode
            // 
            this.tbBarcode.Location = new System.Drawing.Point(4, 52);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.Size = new System.Drawing.Size(223, 23);
            this.tbBarcode.TabIndex = 16;
            this.tbBarcode.Text = "S-19-3119-023.1";
            this.tbBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBarcode_KeyPress);
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(4, 34);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(76, 23);
            this.Label9.Text = "BARCODE :-";
            // 
            // Scanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.plScanning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Scanning";
            this.Text = "Scanning";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.plScanning.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSection;
        internal System.Windows.Forms.Panel plScanning;
        internal System.Windows.Forms.Button cbManualSave;
        internal System.Windows.Forms.TextBox tbSize;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox tbLastBarcode;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.CheckBox chkbxNoBoxAdd;
        internal System.Windows.Forms.CheckBox chkbxManualSave;
        internal System.Windows.Forms.Button cbExitBarcodeScanning;
        internal System.Windows.Forms.Button cbCancel;
        internal System.Windows.Forms.Button cbBack;
        internal System.Windows.Forms.Button cbSaveBarcode;
        internal System.Windows.Forms.TextBox tbQuantity;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox tbBarcode;
        internal System.Windows.Forms.Label Label9;
    }
}