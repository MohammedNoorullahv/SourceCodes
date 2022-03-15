<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cbCancel = New System.Windows.Forms.Button
        Me.cbOK = New System.Windows.Forms.Button
        Me.tbPassword = New System.Windows.Forms.TextBox
        Me.tbUserName = New System.Windows.Forms.TextBox
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.plPasswordCorrect = New System.Windows.Forms.Panel
        Me.pbLogin = New System.Windows.Forms.ProgressBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbDatabase = New System.Windows.Forms.TextBox
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.lblVersionInfo = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.lblTime = New System.Windows.Forms.Label
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plPasswordCorrect.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'cbCancel
        '
        Me.cbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbCancel.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbCancel.Location = New System.Drawing.Point(438, 139)
        Me.cbCancel.Name = "cbCancel"
        Me.cbCancel.Size = New System.Drawing.Size(110, 31)
        Me.cbCancel.TabIndex = 14
        Me.cbCancel.Text = "&Cancel"
        '
        'cbOK
        '
        Me.cbOK.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbOK.Location = New System.Drawing.Point(306, 139)
        Me.cbOK.Name = "cbOK"
        Me.cbOK.Size = New System.Drawing.Size(110, 31)
        Me.cbOK.TabIndex = 13
        Me.cbOK.Text = "&OK"
        '
        'tbPassword
        '
        Me.tbPassword.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPassword.Location = New System.Drawing.Point(293, 63)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbPassword.Size = New System.Drawing.Size(256, 22)
        Me.tbPassword.TabIndex = 12
        '
        'tbUserName
        '
        Me.tbUserName.Location = New System.Drawing.Point(293, 35)
        Me.tbUserName.Name = "tbUserName"
        Me.tbUserName.Size = New System.Drawing.Size(256, 22)
        Me.tbUserName.TabIndex = 10
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.PasswordLabel.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PasswordLabel.Location = New System.Drawing.Point(218, 66)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(69, 17)
        Me.PasswordLabel.TabIndex = 11
        Me.PasswordLabel.Text = "Password"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.BackColor = System.Drawing.Color.Transparent
        Me.UsernameLabel.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UsernameLabel.Location = New System.Drawing.Point(203, 37)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(84, 17)
        Me.UsernameLabel.TabIndex = 8
        Me.UsernameLabel.Text = "Login Name"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'plPasswordCorrect
        '
        Me.plPasswordCorrect.Controls.Add(Me.pbLogin)
        Me.plPasswordCorrect.Controls.Add(Me.Label1)
        Me.plPasswordCorrect.Location = New System.Drawing.Point(27, 9)
        Me.plPasswordCorrect.Name = "plPasswordCorrect"
        Me.plPasswordCorrect.Size = New System.Drawing.Size(74, 36)
        Me.plPasswordCorrect.TabIndex = 19
        Me.plPasswordCorrect.Visible = False
        '
        'pbLogin
        '
        Me.pbLogin.Location = New System.Drawing.Point(5, 15)
        Me.pbLogin.Name = "pbLogin"
        Me.pbLogin.Size = New System.Drawing.Size(468, 21)
        Me.pbLogin.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(261, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Correct Password! Connection to Database......"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(213, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "DataBase "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbDatabase
        '
        Me.tbDatabase.BackColor = System.Drawing.Color.White
        Me.tbDatabase.Font = New System.Drawing.Font("Copperplate Gothic Light", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDatabase.ForeColor = System.Drawing.Color.Blue
        Me.tbDatabase.Location = New System.Drawing.Point(293, 94)
        Me.tbDatabase.Name = "tbDatabase"
        Me.tbDatabase.ReadOnly = True
        Me.tbDatabase.Size = New System.Drawing.Size(256, 37)
        Me.tbDatabase.TabIndex = 78
        Me.tbDatabase.TabStop = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Honeydew
        Me.PanelControl1.Appearance.BackColor2 = System.Drawing.Color.YellowGreen
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.Controls.Add(Me.lblVersionInfo)
        Me.PanelControl1.Controls.Add(Me.PictureBox3)
        Me.PanelControl1.Controls.Add(Me.PictureBox2)
        Me.PanelControl1.Controls.Add(Me.PictureBox4)
        Me.PanelControl1.Controls.Add(Me.lblTime)
        Me.PanelControl1.Controls.Add(Me.tbDatabase)
        Me.PanelControl1.Controls.Add(Me.UsernameLabel)
        Me.PanelControl1.Controls.Add(Me.tbPassword)
        Me.PanelControl1.Controls.Add(Me.cbCancel)
        Me.PanelControl1.Controls.Add(Me.tbUserName)
        Me.PanelControl1.Controls.Add(Me.plPasswordCorrect)
        Me.PanelControl1.Controls.Add(Me.cbOK)
        Me.PanelControl1.Controls.Add(Me.Label2)
        Me.PanelControl1.Controls.Add(Me.PasswordLabel)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(568, 185)
        Me.PanelControl1.TabIndex = 79
        '
        'lblVersionInfo
        '
        Me.lblVersionInfo.AutoSize = True
        Me.lblVersionInfo.BackColor = System.Drawing.Color.Transparent
        Me.lblVersionInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVersionInfo.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersionInfo.Location = New System.Drawing.Point(91, 160)
        Me.lblVersionInfo.Name = "lblVersionInfo"
        Me.lblVersionInfo.Size = New System.Drawing.Size(107, 16)
        Me.lblVersionInfo.TabIndex = 80
        Me.lblVersionInfo.Text = "306 30.01.2014"
        Me.lblVersionInfo.Visible = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureBox3.Location = New System.Drawing.Point(168, 67)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(33, 34)
        Me.PictureBox3.TabIndex = 81
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureBox2.Location = New System.Drawing.Point(167, 27)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(33, 34)
        Me.PictureBox2.TabIndex = 80
        Me.PictureBox2.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureBox4.Location = New System.Drawing.Point(7, 11)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(132, 139)
        Me.PictureBox4.TabIndex = 79
        Me.PictureBox4.TabStop = False
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.BackColor = System.Drawing.Color.Transparent
        Me.lblTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTime.Location = New System.Drawing.Point(373, 8)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(46, 17)
        Me.lblTime.TabIndex = 17
        Me.lblTime.Text = "Label1"
        '
        'frmLoginForm
        '
        Me.AcceptButton = Me.cbOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FloralWhite
        Me.CancelButton = Me.cbCancel
        Me.ClientSize = New System.Drawing.Size(568, 185)
        Me.Controls.Add(Me.PanelControl1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmLoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plPasswordCorrect.ResumeLayout(False)
        Me.plPasswordCorrect.PerformLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cbCancel As System.Windows.Forms.Button
    Friend WithEvents cbOK As System.Windows.Forms.Button
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents tbUserName As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents plPasswordCorrect As System.Windows.Forms.Panel
    Friend WithEvents pbLogin As System.Windows.Forms.ProgressBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbDatabase As System.Windows.Forms.TextBox
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblVersionInfo As System.Windows.Forms.Label
End Class
