namespace BarcodeScann
{
    partial class Selection
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDispatch = new System.Windows.Forms.Button();
            this.btnPacking = new System.Windows.Forms.Button();
            this.btnFinishing = new System.Windows.Forms.Button();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.cmbShift = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMoulding = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClear.Location = new System.Drawing.Point(15, 261);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(101, 23);
            this.btnClear.TabIndex = 37;
            this.btnClear.Text = "Clear";
            // 
            // btnDispatch
            // 
            this.btnDispatch.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnDispatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnDispatch.Location = new System.Drawing.Point(122, 185);
            this.btnDispatch.Name = "btnDispatch";
            this.btnDispatch.Size = new System.Drawing.Size(101, 70);
            this.btnDispatch.TabIndex = 36;
            this.btnDispatch.Text = "DISPATCH";
            this.btnDispatch.Click += new System.EventHandler(this.btnDispatch_Click);
            // 
            // btnPacking
            // 
            this.btnPacking.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPacking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPacking.Location = new System.Drawing.Point(15, 185);
            this.btnPacking.Name = "btnPacking";
            this.btnPacking.Size = new System.Drawing.Size(101, 70);
            this.btnPacking.TabIndex = 35;
            this.btnPacking.Text = "PACKING";
            this.btnPacking.Click += new System.EventHandler(this.btnPacking_Click);
            // 
            // btnFinishing
            // 
            this.btnFinishing.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnFinishing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnFinishing.Location = new System.Drawing.Point(122, 109);
            this.btnFinishing.Name = "btnFinishing";
            this.btnFinishing.Size = new System.Drawing.Size(101, 70);
            this.btnFinishing.TabIndex = 34;
            this.btnFinishing.Text = "FINISHING";
            this.btnFinishing.Click += new System.EventHandler(this.btnFinishing_Click);
            // 
            // cmbMachine
            // 
            this.cmbMachine.Items.Add("M1");
            this.cmbMachine.Items.Add("M2");
            this.cmbMachine.Items.Add("M3");
            this.cmbMachine.Items.Add("M4");
            this.cmbMachine.Location = new System.Drawing.Point(108, 79);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(129, 23);
            this.cmbMachine.TabIndex = 33;
            // 
            // cmbShift
            // 
            this.cmbShift.Items.Add("4 AM - 12 PM");
            this.cmbShift.Items.Add("12 PM - 8 PM");
            this.cmbShift.Items.Add("8 PM - 4 AM");
            this.cmbShift.Location = new System.Drawing.Point(108, 52);
            this.cmbShift.Name = "cmbShift";
            this.cmbShift.Size = new System.Drawing.Size(129, 23);
            this.cmbShift.TabIndex = 32;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(122, 261);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 23);
            this.btnExit.TabIndex = 31;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMoulding
            // 
            this.btnMoulding.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnMoulding.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnMoulding.Location = new System.Drawing.Point(15, 109);
            this.btnMoulding.Name = "btnMoulding";
            this.btnMoulding.Size = new System.Drawing.Size(101, 70);
            this.btnMoulding.TabIndex = 30;
            this.btnMoulding.Text = "MOULDING";
            this.btnMoulding.Click += new System.EventHandler(this.btnMoulding_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular);
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(5, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 25);
            this.label6.Text = "Machine :";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(40, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 25);
            this.label5.Text = "Shift :";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(40, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 39);
            this.label4.Text = "Selection";
            // 
            // Selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDispatch);
            this.Controls.Add(this.btnPacking);
            this.Controls.Add(this.btnFinishing);
            this.Controls.Add(this.cmbMachine);
            this.Controls.Add(this.cmbShift);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMoulding);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Selection";
            this.Text = "frmSelection";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Selection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDispatch;
        private System.Windows.Forms.Button btnPacking;
        private System.Windows.Forms.Button btnFinishing;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.ComboBox cmbShift;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMoulding;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}