<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tbDatabaseName = New System.Windows.Forms.TextBox
        Me.tbServerName = New System.Windows.Forms.TextBox
        Me.tbPassword = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbUserName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbLogin = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tbDatabaseName)
        Me.Panel1.Controls.Add(Me.tbServerName)
        Me.Panel1.Controls.Add(Me.tbPassword)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.tbUserName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbLogin)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(226, 263)
        '
        'tbDatabaseName
        '
        Me.tbDatabaseName.Location = New System.Drawing.Point(5, 169)
        Me.tbDatabaseName.Name = "tbDatabaseName"
        Me.tbDatabaseName.Size = New System.Drawing.Size(215, 23)
        Me.tbDatabaseName.TabIndex = 19
        Me.tbDatabaseName.Text = "Suheb"
        '
        'tbServerName
        '
        Me.tbServerName.Location = New System.Drawing.Point(3, 140)
        Me.tbServerName.Name = "tbServerName"
        Me.tbServerName.Size = New System.Drawing.Size(217, 23)
        Me.tbServerName.TabIndex = 18
        Me.tbServerName.Text = "Suheb"
        '
        'tbPassword
        '
        Me.tbPassword.Location = New System.Drawing.Point(87, 57)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbPassword.Size = New System.Drawing.Size(142, 23)
        Me.tbPassword.TabIndex = 14
        Me.tbPassword.Text = "9009"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(5, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 23)
        Me.Label3.Text = "Password :-"
        '
        'tbUserName
        '
        Me.tbUserName.Location = New System.Drawing.Point(87, 28)
        Me.tbUserName.Name = "tbUserName"
        Me.tbUserName.Size = New System.Drawing.Size(142, 23)
        Me.tbUserName.TabIndex = 13
        Me.tbUserName.Text = "Suheb"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(5, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 23)
        Me.Label1.Text = "User Name :-"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(230, 25)
        Me.Label2.Text = "LOGIN ACCESS INFO"
        '
        'cbExit
        '
        Me.cbExit.Location = New System.Drawing.Point(147, 237)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(75, 23)
        Me.cbExit.TabIndex = 8
        Me.cbExit.Text = "Exit"
        '
        'cbLogin
        '
        Me.cbLogin.Location = New System.Drawing.Point(3, 237)
        Me.cbLogin.Name = "cbLogin"
        Me.cbLogin.Size = New System.Drawing.Size(75, 23)
        Me.cbLogin.TabIndex = 7
        Me.cbLogin.Text = "Login"
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(232, 269)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Menu = Me.mainMenu1
        Me.Name = "frmLogin"
        Me.Text = "frmLogin"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbLogin As System.Windows.Forms.Button
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbServerName As System.Windows.Forms.TextBox
    Friend WithEvents tbDatabaseName As System.Windows.Forms.TextBox
End Class
