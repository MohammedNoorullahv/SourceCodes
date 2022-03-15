namespace SDP_MCDApplicationv1inCSharp.Forms
{
    partial class FrmSelection
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
            this.plSelection = new System.Windows.Forms.Panel();
            this.cbPackingtoFinish = new System.Windows.Forms.Button();
            this.cbxMachine = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.cbDispatch = new System.Windows.Forms.Button();
            this.cbPacking = new System.Windows.Forms.Button();
            this.cbFinishing = new System.Windows.Forms.Button();
            this.cbMoulding = new System.Windows.Forms.Button();
            this.cbExitSelection = new System.Windows.Forms.Button();
            this.cbNext = new System.Windows.Forms.Button();
            this.cbxShift = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.cbStock = new System.Windows.Forms.Button();
            this.plSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // plSelection
            // 
            this.plSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.plSelection.Controls.Add(this.cbStock);
            this.plSelection.Controls.Add(this.cbPackingtoFinish);
            this.plSelection.Controls.Add(this.cbxMachine);
            this.plSelection.Controls.Add(this.Label13);
            this.plSelection.Controls.Add(this.cbDispatch);
            this.plSelection.Controls.Add(this.cbPacking);
            this.plSelection.Controls.Add(this.cbFinishing);
            this.plSelection.Controls.Add(this.cbMoulding);
            this.plSelection.Controls.Add(this.cbExitSelection);
            this.plSelection.Controls.Add(this.cbNext);
            this.plSelection.Controls.Add(this.cbxShift);
            this.plSelection.Controls.Add(this.Label6);
            this.plSelection.Controls.Add(this.Label4);
            this.plSelection.Location = new System.Drawing.Point(1, 6);
            this.plSelection.Name = "plSelection";
            this.plSelection.Size = new System.Drawing.Size(234, 263);
            // 
            // cbPackingtoFinish
            // 
            this.cbPackingtoFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.cbPackingtoFinish.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cbPackingtoFinish.ForeColor = System.Drawing.Color.Red;
            this.cbPackingtoFinish.Location = new System.Drawing.Point(7, 183);
            this.cbPackingtoFinish.Name = "cbPackingtoFinish";
            this.cbPackingtoFinish.Size = new System.Drawing.Size(221, 40);
            this.cbPackingtoFinish.TabIndex = 21;
            this.cbPackingtoFinish.Text = "PACKING to FINISHING\r\n[Rework / Replace ]";
            this.cbPackingtoFinish.Click += new System.EventHandler(this.cbPackingtoFinish_Click);
            // 
            // cbxMachine
            // 
            this.cbxMachine.Location = new System.Drawing.Point(86, 62);
            this.cbxMachine.Name = "cbxMachine";
            this.cbxMachine.Size = new System.Drawing.Size(142, 23);
            this.cbxMachine.TabIndex = 17;
            // 
            // Label13
            // 
            this.Label13.Location = new System.Drawing.Point(4, 62);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(76, 23);
            this.Label13.Text = "Machine / Conv";
            // 
            // cbDispatch
            // 
            this.cbDispatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cbDispatch.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.cbDispatch.Location = new System.Drawing.Point(121, 139);
            this.cbDispatch.Name = "cbDispatch";
            this.cbDispatch.Size = new System.Drawing.Size(108, 40);
            this.cbDispatch.TabIndex = 15;
            this.cbDispatch.Text = "DISPATCH";
            // 
            // cbPacking
            // 
            this.cbPacking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbPacking.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.cbPacking.Location = new System.Drawing.Point(7, 139);
            this.cbPacking.Name = "cbPacking";
            this.cbPacking.Size = new System.Drawing.Size(108, 40);
            this.cbPacking.TabIndex = 14;
            this.cbPacking.Text = "PACKING";
            this.cbPacking.Click += new System.EventHandler(this.cbPacking_Click);
            // 
            // cbFinishing
            // 
            this.cbFinishing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cbFinishing.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.cbFinishing.Location = new System.Drawing.Point(121, 95);
            this.cbFinishing.Name = "cbFinishing";
            this.cbFinishing.Size = new System.Drawing.Size(108, 40);
            this.cbFinishing.TabIndex = 13;
            this.cbFinishing.Text = "FINISHING";
            this.cbFinishing.Click += new System.EventHandler(this.cbFinishing_Click);
            // 
            // cbMoulding
            // 
            this.cbMoulding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cbMoulding.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.cbMoulding.Location = new System.Drawing.Point(7, 95);
            this.cbMoulding.Name = "cbMoulding";
            this.cbMoulding.Size = new System.Drawing.Size(108, 40);
            this.cbMoulding.TabIndex = 12;
            this.cbMoulding.Text = "MOULDING";
            this.cbMoulding.Click += new System.EventHandler(this.cbMoulding_Click);
            // 
            // cbExitSelection
            // 
            this.cbExitSelection.Location = new System.Drawing.Point(154, 232);
            this.cbExitSelection.Name = "cbExitSelection";
            this.cbExitSelection.Size = new System.Drawing.Size(75, 23);
            this.cbExitSelection.TabIndex = 11;
            this.cbExitSelection.Text = "Exit";
            this.cbExitSelection.Click += new System.EventHandler(this.cbExitSelection_Click);
            // 
            // cbNext
            // 
            this.cbNext.Location = new System.Drawing.Point(4, 232);
            this.cbNext.Name = "cbNext";
            this.cbNext.Size = new System.Drawing.Size(75, 23);
            this.cbNext.TabIndex = 10;
            this.cbNext.Text = "Next";
            // 
            // cbxShift
            // 
            this.cbxShift.Location = new System.Drawing.Point(86, 34);
            this.cbxShift.Name = "cbxShift";
            this.cbxShift.Size = new System.Drawing.Size(142, 23);
            this.cbxShift.TabIndex = 8;
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(4, 34);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(76, 23);
            this.Label6.Text = "Shift :-";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Label4.ForeColor = System.Drawing.Color.Red;
            this.Label4.Location = new System.Drawing.Point(1, 1);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(230, 25);
            this.Label4.Text = "SELECTION";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbStock
            // 
            this.cbStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cbStock.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.cbStock.Location = new System.Drawing.Point(121, 139);
            this.cbStock.Name = "cbStock";
            this.cbStock.Size = new System.Drawing.Size(108, 40);
            this.cbStock.TabIndex = 25;
            this.cbStock.Text = "STOCK";
            this.cbStock.Click += new System.EventHandler(this.cbStock_Click);
            // 
            // FrmSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(237, 272);
            this.Controls.Add(this.plSelection);
            this.Menu = this.mainMenu1;
            this.Name = "FrmSelection";
            this.Text = "FrmSelection";
            this.plSelection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel plSelection;
        internal System.Windows.Forms.ComboBox cbxMachine;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Button cbDispatch;
        internal System.Windows.Forms.Button cbPacking;
        internal System.Windows.Forms.Button cbFinishing;
        internal System.Windows.Forms.Button cbMoulding;
        internal System.Windows.Forms.Button cbExitSelection;
        internal System.Windows.Forms.Button cbNext;
        internal System.Windows.Forms.ComboBox cbxShift;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button cbPackingtoFinish;
        internal System.Windows.Forms.Button cbStock;
    }
}