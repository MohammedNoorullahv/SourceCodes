<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImageCapture
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
        Me.Label6 = New System.Windows.Forms.Label
        Me.lMovements = New System.Windows.Forms.Label
        Me.pConverted = New System.Windows.Forms.PictureBox
        Me.chb1 = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.opt4 = New System.Windows.Forms.RadioButton
        Me.opt3 = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.opt2 = New System.Windows.Forms.RadioButton
        Me.opt1 = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmd3 = New System.Windows.Forms.Button
        Me.cmd2 = New System.Windows.Forms.Button
        Me.cmd1 = New System.Windows.Forms.Button
        Me.pView = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lst1 = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.sfdImages = New System.Windows.Forms.SaveFileDialog
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pConverted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(197, 387)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 20)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Movements"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lMovements
        '
        Me.lMovements.Location = New System.Drawing.Point(294, 387)
        Me.lMovements.Name = "lMovements"
        Me.lMovements.Size = New System.Drawing.Size(115, 20)
        Me.lMovements.TabIndex = 29
        Me.lMovements.Text = "Movements"
        '
        'pConverted
        '
        Me.pConverted.Location = New System.Drawing.Point(216, 216)
        Me.pConverted.Name = "pConverted"
        Me.pConverted.Size = New System.Drawing.Size(193, 168)
        Me.pConverted.TabIndex = 28
        Me.pConverted.TabStop = False
        '
        'chb1
        '
        Me.chb1.AutoSize = True
        Me.chb1.Location = New System.Drawing.Point(595, 314)
        Me.chb1.Name = "chb1"
        Me.chb1.Size = New System.Drawing.Size(121, 17)
        Me.chb1.TabIndex = 27
        Me.chb1.Text = "Add Motion Sensors"
        Me.chb1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.Label4.Location = New System.Drawing.Point(595, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(150, 19)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Tools"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(200, 416)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(547, 20)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Copyright (C) 2011, Albert Sandro Mamu"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'opt4
        '
        Me.opt4.AutoSize = True
        Me.opt4.Location = New System.Drawing.Point(662, 268)
        Me.opt4.Name = "opt4"
        Me.opt4.Size = New System.Drawing.Size(69, 17)
        Me.opt4.TabIndex = 23
        Me.opt4.TabStop = True
        Me.opt4.Text = "Infra &Red"
        Me.opt4.UseVisualStyleBackColor = True
        '
        'opt3
        '
        Me.opt3.AutoSize = True
        Me.opt3.Location = New System.Drawing.Point(662, 246)
        Me.opt3.Name = "opt3"
        Me.opt3.Size = New System.Drawing.Size(72, 17)
        Me.opt3.TabIndex = 21
        Me.opt3.TabStop = True
        Me.opt3.Text = "&Grayscale"
        Me.opt3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.Label2.Location = New System.Drawing.Point(595, 216)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 19)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Graphics Effect"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'opt2
        '
        Me.opt2.AutoSize = True
        Me.opt2.Location = New System.Drawing.Point(595, 246)
        Me.opt2.Name = "opt2"
        Me.opt2.Size = New System.Drawing.Size(58, 17)
        Me.opt2.TabIndex = 17
        Me.opt2.TabStop = True
        Me.opt2.Text = "&Normal"
        Me.opt2.UseVisualStyleBackColor = True
        '
        'opt1
        '
        Me.opt1.AutoSize = True
        Me.opt1.Location = New System.Drawing.Point(595, 268)
        Me.opt1.Name = "opt1"
        Me.opt1.Size = New System.Drawing.Size(52, 17)
        Me.opt1.TabIndex = 19
        Me.opt1.TabStop = True
        Me.opt1.Text = "&Invert"
        Me.opt1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmd3)
        Me.GroupBox2.Controls.Add(Me.cmd2)
        Me.GroupBox2.Controls.Add(Me.cmd1)
        Me.GroupBox2.Location = New System.Drawing.Point(415, 348)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(330, 65)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Main Control"
        '
        'cmd3
        '
        Me.cmd3.Location = New System.Drawing.Point(231, 20)
        Me.cmd3.Name = "cmd3"
        Me.cmd3.Size = New System.Drawing.Size(92, 36)
        Me.cmd3.TabIndex = 7
        Me.cmd3.Text = "S&ave"
        Me.cmd3.UseVisualStyleBackColor = True
        '
        'cmd2
        '
        Me.cmd2.Location = New System.Drawing.Point(120, 20)
        Me.cmd2.Name = "cmd2"
        Me.cmd2.Size = New System.Drawing.Size(92, 36)
        Me.cmd2.TabIndex = 6
        Me.cmd2.Text = "&Stop"
        Me.cmd2.UseVisualStyleBackColor = True
        '
        'cmd1
        '
        Me.cmd1.Location = New System.Drawing.Point(10, 20)
        Me.cmd1.Name = "cmd1"
        Me.cmd1.Size = New System.Drawing.Size(92, 36)
        Me.cmd1.TabIndex = 5
        Me.cmd1.Text = "&Capture"
        Me.cmd1.UseVisualStyleBackColor = True
        '
        'pView
        '
        Me.pView.Location = New System.Drawing.Point(425, 216)
        Me.pView.Name = "pView"
        Me.pView.Size = New System.Drawing.Size(146, 126)
        Me.pView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pView.TabIndex = 20
        Me.pView.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lst1)
        Me.GroupBox1.Location = New System.Drawing.Point(200, 117)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(545, 92)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Webcam"
        '
        'lst1
        '
        Me.lst1.FormattingEnabled = True
        Me.lst1.Location = New System.Drawing.Point(16, 24)
        Me.lst1.Name = "lst1"
        Me.lst1.Size = New System.Drawing.Size(522, 43)
        Me.lst1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.Label1.Location = New System.Drawing.Point(199, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(546, 41)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Mi.Ro SDK - Motion Sensors"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'Timer2
        '
        Me.Timer2.Interval = 10
        '
        'frmImageCapture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 508)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lMovements)
        Me.Controls.Add(Me.pConverted)
        Me.Controls.Add(Me.chb1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.opt4)
        Me.Controls.Add(Me.opt3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.opt2)
        Me.Controls.Add(Me.opt1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.pView)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmImageCapture"
        Me.Text = "frmImageCapture"
        CType(Me.pConverted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.pView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lMovements As System.Windows.Forms.Label
    Friend WithEvents pConverted As System.Windows.Forms.PictureBox
    Friend WithEvents chb1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents opt4 As System.Windows.Forms.RadioButton
    Friend WithEvents opt3 As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents opt2 As System.Windows.Forms.RadioButton
    Friend WithEvents opt1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmd3 As System.Windows.Forms.Button
    Friend WithEvents cmd2 As System.Windows.Forms.Button
    Friend WithEvents cmd1 As System.Windows.Forms.Button
    Friend WithEvents pView As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lst1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents sfdImages As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
End Class
