namespace SDP_MCDApplicationv1inCSharp.Forms
{
    partial class FrmSavePacktoFinish
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
            this.plSave = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.tbSpoolNo = new System.Windows.Forms.TextBox();
            this.lbScannedBoxes = new System.Windows.Forms.ListBox();
            this.cbExitSave = new System.Windows.Forms.Button();
            this.cbSaveBack = new System.Windows.Forms.Button();
            this.cbSaveAll = new System.Windows.Forms.Button();
            this.tbTotalQty = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.tbTotalCartons = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.plSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // plSave
            // 
            this.plSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.plSave.Controls.Add(this.Label1);
            this.plSave.Controls.Add(this.tbSpoolNo);
            this.plSave.Controls.Add(this.lbScannedBoxes);
            this.plSave.Controls.Add(this.cbExitSave);
            this.plSave.Controls.Add(this.cbSaveBack);
            this.plSave.Controls.Add(this.cbSaveAll);
            this.plSave.Controls.Add(this.tbTotalQty);
            this.plSave.Controls.Add(this.Label11);
            this.plSave.Controls.Add(this.tbTotalCartons);
            this.plSave.Controls.Add(this.Label12);
            this.plSave.Controls.Add(this.Label10);
            this.plSave.Location = new System.Drawing.Point(0, 0);
            this.plSave.Name = "plSave";
            this.plSave.Size = new System.Drawing.Size(234, 260);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(7, 180);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 23);
            this.Label1.Text = "Spool No. :-";
            // 
            // tbSpoolNo
            // 
            this.tbSpoolNo.Location = new System.Drawing.Point(94, 177);
            this.tbSpoolNo.Name = "tbSpoolNo";
            this.tbSpoolNo.Size = new System.Drawing.Size(129, 23);
            this.tbSpoolNo.TabIndex = 31;
            // 
            // lbScannedBoxes
            // 
            this.lbScannedBoxes.Location = new System.Drawing.Point(4, 95);
            this.lbScannedBoxes.Name = "lbScannedBoxes";
            this.lbScannedBoxes.Size = new System.Drawing.Size(224, 82);
            this.lbScannedBoxes.TabIndex = 27;
            // 
            // cbExitSave
            // 
            this.cbExitSave.Location = new System.Drawing.Point(153, 232);
            this.cbExitSave.Name = "cbExitSave";
            this.cbExitSave.Size = new System.Drawing.Size(75, 23);
            this.cbExitSave.TabIndex = 26;
            this.cbExitSave.Text = "Exit";
            // 
            // cbSaveBack
            // 
            this.cbSaveBack.Location = new System.Drawing.Point(153, 206);
            this.cbSaveBack.Name = "cbSaveBack";
            this.cbSaveBack.Size = new System.Drawing.Size(75, 23);
            this.cbSaveBack.TabIndex = 24;
            this.cbSaveBack.Text = "Back";
            // 
            // cbSaveAll
            // 
            this.cbSaveAll.Location = new System.Drawing.Point(4, 232);
            this.cbSaveAll.Name = "cbSaveAll";
            this.cbSaveAll.Size = new System.Drawing.Size(75, 23);
            this.cbSaveAll.TabIndex = 23;
            this.cbSaveAll.Text = "Save";
            this.cbSaveAll.Click += new System.EventHandler(this.cbSaveAll_Click);
            // 
            // tbTotalQty
            // 
            this.tbTotalQty.Location = new System.Drawing.Point(94, 66);
            this.tbTotalQty.Name = "tbTotalQty";
            this.tbTotalQty.Size = new System.Drawing.Size(129, 23);
            this.tbTotalQty.TabIndex = 8;
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(-1, 66);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(103, 23);
            this.Label11.Text = "Total Qty :-";
            // 
            // tbTotalCartons
            // 
            this.tbTotalCartons.Location = new System.Drawing.Point(94, 37);
            this.tbTotalCartons.Name = "tbTotalCartons";
            this.tbTotalCartons.Size = new System.Drawing.Size(129, 23);
            this.tbTotalCartons.TabIndex = 6;
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(-1, 37);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(103, 23);
            this.Label12.Text = "Total Cartons :-";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label10.ForeColor = System.Drawing.Color.Red;
            this.Label10.Location = new System.Drawing.Point(1, 1);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(230, 25);
            this.Label10.Text = "SAVE SELECTION";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FrmSavePacktoFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(235, 265);
            this.Controls.Add(this.plSave);
            this.Menu = this.mainMenu1;
            this.Name = "FrmSavePacktoFinish";
            this.Text = "FrmSavePacktoFinish";
            this.plSave.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel plSave;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox tbSpoolNo;
        internal System.Windows.Forms.ListBox lbScannedBoxes;
        internal System.Windows.Forms.Button cbExitSave;
        internal System.Windows.Forms.Button cbSaveBack;
        internal System.Windows.Forms.Button cbSaveAll;
        internal System.Windows.Forms.TextBox tbTotalQty;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox tbTotalCartons;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label10;
    }
}