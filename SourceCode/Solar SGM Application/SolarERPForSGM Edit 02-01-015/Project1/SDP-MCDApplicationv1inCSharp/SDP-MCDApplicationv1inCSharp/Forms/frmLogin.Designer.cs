namespace SDP_MCDApplicationv1inCSharp
{
    partial class frmLogin
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDatabaseName = new System.Windows.Forms.TextBox();
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.cbExit = new System.Windows.Forms.Button();
            this.cbLogin = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.White;
            this.Panel1.Controls.Add(this.lblVersion);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.lblDate);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.tbDatabaseName);
            this.Panel1.Controls.Add(this.tbServerName);
            this.Panel1.Controls.Add(this.tbPassword);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.tbUserName);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.cbExit);
            this.Panel1.Controls.Add(this.cbLogin);
            this.Panel1.Location = new System.Drawing.Point(3, 27);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(229, 238);
            this.Panel1.GotFocus += new System.EventHandler(this.Panel1_GotFocus);
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(145, 166);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(75, 20);
            this.lblVersion.Text = "16";
            this.lblVersion.Visible = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(145, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.Text = "Version";
            this.label5.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(6, 143);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(116, 23);
            this.lblDate.Text = "v2.21-Jan-20";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 23);
            this.label4.Text = "v16.03-Aug-20";
            // 
            // tbDatabaseName
            // 
            this.tbDatabaseName.Location = new System.Drawing.Point(6, 117);
            this.tbDatabaseName.Name = "tbDatabaseName";
            this.tbDatabaseName.ReadOnly = true;
            this.tbDatabaseName.Size = new System.Drawing.Size(215, 23);
            this.tbDatabaseName.TabIndex = 19;
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(4, 88);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.ReadOnly = true;
            this.tbServerName.Size = new System.Drawing.Size(217, 23);
            this.tbServerName.TabIndex = 18;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(87, 57);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(134, 23);
            this.tbPassword.TabIndex = 14;
            this.tbPassword.Text = "9900";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(5, 57);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 23);
            this.Label3.Text = "Password :-";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(87, 28);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(136, 23);
            this.tbUserName.TabIndex = 13;
            this.tbUserName.Text = "Suheb";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(5, 28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 23);
            this.Label1.Text = "User Name :-";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label2.Location = new System.Drawing.Point(0, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(226, 20);
            this.Label2.Text = "LOGIN ACCESS INFO";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbExit
            // 
            this.cbExit.Location = new System.Drawing.Point(145, 198);
            this.cbExit.Name = "cbExit";
            this.cbExit.Size = new System.Drawing.Size(75, 23);
            this.cbExit.TabIndex = 8;
            this.cbExit.Text = "Exit";
            this.cbExit.Click += new System.EventHandler(this.cbExit_Click);
            // 
            // cbLogin
            // 
            this.cbLogin.Location = new System.Drawing.Point(6, 198);
            this.cbLogin.Name = "cbLogin";
            this.cbLogin.Size = new System.Drawing.Size(75, 23);
            this.cbLogin.TabIndex = 7;
            this.cbLogin.Text = "Login";
            this.cbLogin.Click += new System.EventHandler(this.cbLogin_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(237, 272);
            this.Controls.Add(this.Panel1);
            this.Menu = this.mainMenu1;
            this.Name = "frmLogin";
            this.Text = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TextBox tbDatabaseName;
        internal System.Windows.Forms.TextBox tbServerName;
        internal System.Windows.Forms.TextBox tbPassword;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox tbUserName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button cbExit;
        internal System.Windows.Forms.Button cbLogin;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVersion;
    }
}

