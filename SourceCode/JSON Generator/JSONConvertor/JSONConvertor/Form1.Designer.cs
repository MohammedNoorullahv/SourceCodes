namespace JSONConvertor
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbInvoiceNo = new System.Windows.Forms.TextBox();
            this.cbGenerateJSON = new System.Windows.Forms.Button();
            this.cbExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.rbLocalInvoice = new System.Windows.Forms.RadioButton();
            this.rbExportInvoice = new System.Windows.Forms.RadioButton();
            this.cbImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbInvoiceNoLoc = new System.Windows.Forms.TextBox();
            this.tbAckLoc = new System.Windows.Forms.TextBox();
            this.tbIRNLoc = new System.Windows.Forms.TextBox();
            this.tbQRCodeLoc = new System.Windows.Forms.TextBox();
            this.cbUpdate = new System.Windows.Forms.Button();
            this.tbQRCodeLocBulk = new System.Windows.Forms.TextBox();
            this.tbIRNLocBulk = new System.Windows.Forms.TextBox();
            this.tbAckLocBulk = new System.Windows.Forms.TextBox();
            this.tbInvoiceNoLocBulk = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbBulk = new System.Windows.Forms.RadioButton();
            this.rbSingle = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbPixel3 = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.rbPixel2 = new System.Windows.Forms.RadioButton();
            this.rbPixel1 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice No.";
            // 
            // tbInvoiceNo
            // 
            this.tbInvoiceNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbInvoiceNo.Location = new System.Drawing.Point(122, 12);
            this.tbInvoiceNo.Name = "tbInvoiceNo";
            this.tbInvoiceNo.Size = new System.Drawing.Size(188, 22);
            this.tbInvoiceNo.TabIndex = 1;
            // 
            // cbGenerateJSON
            // 
            this.cbGenerateJSON.Location = new System.Drawing.Point(12, 39);
            this.cbGenerateJSON.Name = "cbGenerateJSON";
            this.cbGenerateJSON.Size = new System.Drawing.Size(296, 29);
            this.cbGenerateJSON.TabIndex = 2;
            this.cbGenerateJSON.Text = "Generate JSON";
            this.cbGenerateJSON.UseVisualStyleBackColor = true;
            this.cbGenerateJSON.Click += new System.EventHandler(this.cbGenerateJSON_Click);
            // 
            // cbExit
            // 
            this.cbExit.Location = new System.Drawing.Point(528, 318);
            this.cbExit.Name = "cbExit";
            this.cbExit.Size = new System.Drawing.Size(88, 30);
            this.cbExit.TabIndex = 3;
            this.cbExit.Text = "E&xit";
            this.cbExit.UseVisualStyleBackColor = true;
            this.cbExit.Click += new System.EventHandler(this.cbExit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate JSON";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbLocalInvoice
            // 
            this.rbLocalInvoice.AutoSize = true;
            this.rbLocalInvoice.Location = new System.Drawing.Point(12, 39);
            this.rbLocalInvoice.Name = "rbLocalInvoice";
            this.rbLocalInvoice.Size = new System.Drawing.Size(130, 20);
            this.rbLocalInvoice.TabIndex = 5;
            this.rbLocalInvoice.TabStop = true;
            this.rbLocalInvoice.Text = "Local Invoice";
            this.rbLocalInvoice.UseVisualStyleBackColor = true;
            this.rbLocalInvoice.Visible = false;
            // 
            // rbExportInvoice
            // 
            this.rbExportInvoice.AutoSize = true;
            this.rbExportInvoice.Checked = true;
            this.rbExportInvoice.Location = new System.Drawing.Point(148, 39);
            this.rbExportInvoice.Name = "rbExportInvoice";
            this.rbExportInvoice.Size = new System.Drawing.Size(138, 20);
            this.rbExportInvoice.TabIndex = 6;
            this.rbExportInvoice.TabStop = true;
            this.rbExportInvoice.Text = "Export Invoice";
            this.rbExportInvoice.UseVisualStyleBackColor = true;
            this.rbExportInvoice.Visible = false;
            // 
            // cbImport
            // 
            this.cbImport.Location = new System.Drawing.Point(3, 31);
            this.cbImport.Name = "cbImport";
            this.cbImport.Size = new System.Drawing.Size(293, 41);
            this.cbImport.TabIndex = 7;
            this.cbImport.Text = "Import Signed JSON && Convert QR Code";
            this.cbImport.UseVisualStyleBackColor = true;
            this.cbImport.Click += new System.EventHandler(this.cbImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(130, 331);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ver-10 - 28-Sep-21";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(12, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Column No. Locations in Excel File";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(12, 234);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "Invoice No.";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(12, 260);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 22);
            this.label5.TabIndex = 11;
            this.label5.Text = "ACK No.";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(12, 286);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 22);
            this.label6.TabIndex = 12;
            this.label6.Text = "IRN";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(12, 312);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 22);
            this.label7.TabIndex = 13;
            this.label7.Text = "Signed QR Code";
            // 
            // tbInvoiceNoLoc
            // 
            this.tbInvoiceNoLoc.Location = new System.Drawing.Point(141, 234);
            this.tbInvoiceNoLoc.Name = "tbInvoiceNoLoc";
            this.tbInvoiceNoLoc.Size = new System.Drawing.Size(72, 22);
            this.tbInvoiceNoLoc.TabIndex = 14;
            this.tbInvoiceNoLoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInvoiceNoLoc_KeyPress);
            // 
            // tbAckLoc
            // 
            this.tbAckLoc.Location = new System.Drawing.Point(141, 260);
            this.tbAckLoc.Name = "tbAckLoc";
            this.tbAckLoc.Size = new System.Drawing.Size(72, 22);
            this.tbAckLoc.TabIndex = 15;
            this.tbAckLoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAckLoc_KeyPress);
            // 
            // tbIRNLoc
            // 
            this.tbIRNLoc.Location = new System.Drawing.Point(141, 286);
            this.tbIRNLoc.Name = "tbIRNLoc";
            this.tbIRNLoc.Size = new System.Drawing.Size(72, 22);
            this.tbIRNLoc.TabIndex = 16;
            this.tbIRNLoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIRNLoc_KeyPress);
            // 
            // tbQRCodeLoc
            // 
            this.tbQRCodeLoc.Location = new System.Drawing.Point(141, 312);
            this.tbQRCodeLoc.Name = "tbQRCodeLoc";
            this.tbQRCodeLoc.Size = new System.Drawing.Size(72, 22);
            this.tbQRCodeLoc.TabIndex = 17;
            this.tbQRCodeLoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQRCodeLoc_KeyPress);
            // 
            // cbUpdate
            // 
            this.cbUpdate.Location = new System.Drawing.Point(316, 316);
            this.cbUpdate.Name = "cbUpdate";
            this.cbUpdate.Size = new System.Drawing.Size(206, 30);
            this.cbUpdate.TabIndex = 18;
            this.cbUpdate.Text = "Update Column Info";
            this.cbUpdate.UseVisualStyleBackColor = true;
            this.cbUpdate.Click += new System.EventHandler(this.cbUpdate_Click);
            // 
            // tbQRCodeLocBulk
            // 
            this.tbQRCodeLocBulk.Location = new System.Drawing.Point(219, 312);
            this.tbQRCodeLocBulk.Name = "tbQRCodeLocBulk";
            this.tbQRCodeLocBulk.Size = new System.Drawing.Size(72, 22);
            this.tbQRCodeLocBulk.TabIndex = 22;
            // 
            // tbIRNLocBulk
            // 
            this.tbIRNLocBulk.Location = new System.Drawing.Point(219, 286);
            this.tbIRNLocBulk.Name = "tbIRNLocBulk";
            this.tbIRNLocBulk.Size = new System.Drawing.Size(72, 22);
            this.tbIRNLocBulk.TabIndex = 21;
            // 
            // tbAckLocBulk
            // 
            this.tbAckLocBulk.Location = new System.Drawing.Point(219, 260);
            this.tbAckLocBulk.Name = "tbAckLocBulk";
            this.tbAckLocBulk.Size = new System.Drawing.Size(72, 22);
            this.tbAckLocBulk.TabIndex = 20;
            // 
            // tbInvoiceNoLocBulk
            // 
            this.tbInvoiceNoLocBulk.Location = new System.Drawing.Point(219, 234);
            this.tbInvoiceNoLocBulk.Name = "tbInvoiceNoLocBulk";
            this.tbInvoiceNoLocBulk.Size = new System.Drawing.Size(72, 22);
            this.tbInvoiceNoLocBulk.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(141, 215);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "Single";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(219, 215);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.TabIndex = 24;
            this.label9.Text = "Bulk";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbBulk);
            this.panel1.Controls.Add(this.rbSingle);
            this.panel1.Controls.Add(this.cbImport);
            this.panel1.Location = new System.Drawing.Point(11, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 79);
            this.panel1.TabIndex = 25;
            // 
            // rbBulk
            // 
            this.rbBulk.AutoSize = true;
            this.rbBulk.Location = new System.Drawing.Point(137, 6);
            this.rbBulk.Name = "rbBulk";
            this.rbBulk.Size = new System.Drawing.Size(58, 20);
            this.rbBulk.TabIndex = 9;
            this.rbBulk.Text = "Bulk";
            this.rbBulk.UseVisualStyleBackColor = true;
            // 
            // rbSingle
            // 
            this.rbSingle.AutoSize = true;
            this.rbSingle.Checked = true;
            this.rbSingle.Location = new System.Drawing.Point(7, 6);
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.Size = new System.Drawing.Size(74, 20);
            this.rbSingle.TabIndex = 8;
            this.rbSingle.TabStop = true;
            this.rbSingle.Text = "Single";
            this.rbSingle.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(316, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbPixel3);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.rbPixel2);
            this.panel2.Controls.Add(this.rbPixel1);
            this.panel2.Location = new System.Drawing.Point(11, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(299, 34);
            this.panel2.TabIndex = 29;
            // 
            // rbPixel3
            // 
            this.rbPixel3.AutoSize = true;
            this.rbPixel3.Location = new System.Drawing.Point(208, 11);
            this.rbPixel3.Name = "rbPixel3";
            this.rbPixel3.Size = new System.Drawing.Size(34, 20);
            this.rbPixel3.TabIndex = 12;
            this.rbPixel3.Text = "3";
            this.rbPixel3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(5, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 22);
            this.label10.TabIndex = 11;
            this.label10.Text = "Pixel Size :-";
            // 
            // rbPixel2
            // 
            this.rbPixel2.AutoSize = true;
            this.rbPixel2.Location = new System.Drawing.Point(170, 11);
            this.rbPixel2.Name = "rbPixel2";
            this.rbPixel2.Size = new System.Drawing.Size(34, 20);
            this.rbPixel2.TabIndex = 9;
            this.rbPixel2.Text = "2";
            this.rbPixel2.UseVisualStyleBackColor = true;
            // 
            // rbPixel1
            // 
            this.rbPixel1.AutoSize = true;
            this.rbPixel1.Checked = true;
            this.rbPixel1.Location = new System.Drawing.Point(130, 11);
            this.rbPixel1.Name = "rbPixel1";
            this.rbPixel1.Size = new System.Drawing.Size(34, 20);
            this.rbPixel1.TabIndex = 8;
            this.rbPixel1.TabStop = true;
            this.rbPixel1.Text = "1";
            this.rbPixel1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 354);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbGenerateJSON);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbQRCodeLocBulk);
            this.Controls.Add(this.tbIRNLocBulk);
            this.Controls.Add(this.tbAckLocBulk);
            this.Controls.Add(this.tbInvoiceNoLocBulk);
            this.Controls.Add(this.cbUpdate);
            this.Controls.Add(this.tbQRCodeLoc);
            this.Controls.Add(this.tbIRNLoc);
            this.Controls.Add(this.tbAckLoc);
            this.Controls.Add(this.tbInvoiceNoLoc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbExportInvoice);
            this.Controls.Add(this.rbLocalInvoice);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbExit);
            this.Controls.Add(this.tbInvoiceNo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSON Converstion Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInvoiceNo;
        private System.Windows.Forms.Button cbGenerateJSON;
        private System.Windows.Forms.Button cbExit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbLocalInvoice;
        private System.Windows.Forms.RadioButton rbExportInvoice;
        private System.Windows.Forms.Button cbImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbInvoiceNoLoc;
        private System.Windows.Forms.TextBox tbAckLoc;
        private System.Windows.Forms.TextBox tbIRNLoc;
        private System.Windows.Forms.TextBox tbQRCodeLoc;
        private System.Windows.Forms.Button cbUpdate;
        private System.Windows.Forms.TextBox tbQRCodeLocBulk;
        private System.Windows.Forms.TextBox tbIRNLocBulk;
        private System.Windows.Forms.TextBox tbAckLocBulk;
        private System.Windows.Forms.TextBox tbInvoiceNoLocBulk;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbBulk;
        private System.Windows.Forms.RadioButton rbSingle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbPixel2;
        private System.Windows.Forms.RadioButton rbPixel1;
        private System.Windows.Forms.RadioButton rbPixel3;
    }
}

