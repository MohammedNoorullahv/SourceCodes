<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmdiSolarERP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmmdiSolarERP))
        Me.plHeader = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblTime = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.plInfo = New System.Windows.Forms.Panel
        Me.lblUserDesignation = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblYear = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblUnitType = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.plMain = New System.Windows.Forms.Panel
        Me.tb1 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cbHideMessage = New System.Windows.Forms.Button
        Me.cbViewMessage = New System.Windows.Forms.Button
        Me.cbNextMessage = New System.Windows.Forms.Button
        Me.cbPreviousMessage = New System.Windows.Forms.Button
        Me.cbSendMessage = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.plERP = New System.Windows.Forms.Panel
        Me.cbFRM = New System.Windows.Forms.Button
        Me.cbERP = New System.Windows.Forms.Button
        Me.cbHRM = New System.Windows.Forms.Button
        Me.cbMRP = New System.Windows.Forms.Button
        Me.cbSCM = New System.Windows.Forms.Button
        Me.cbPPC = New System.Windows.Forms.Button
        Me.cbCRM = New System.Windows.Forms.Button
        Me.tbDatasource = New System.Windows.Forms.TextBox
        Me.cbComposeMessage = New System.Windows.Forms.Button
        Me.tbDatabase = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbExit = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tbIPAddress = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.tbSystem = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.pl1 = New System.Windows.Forms.Panel
        Me.lblImageERP = New System.Windows.Forms.Panel
        Me.lblImageFRM = New System.Windows.Forms.Panel
        Me.lblImageHRM = New System.Windows.Forms.Panel
        Me.lblImageMRP = New System.Windows.Forms.Panel
        Me.lblImagePPC = New System.Windows.Forms.Panel
        Me.lblImageCRM = New System.Windows.Forms.Panel
        Me.lblImageSCM = New System.Windows.Forms.Panel
        Me.lblMRP = New System.Windows.Forms.Label
        Me.lblSCM = New System.Windows.Forms.Label
        Me.lblCRM = New System.Windows.Forms.Label
        Me.lblPPC = New System.Windows.Forms.Label
        Me.lblERP = New System.Windows.Forms.Label
        Me.lblFRM = New System.Windows.Forms.Label
        Me.lblHRM = New System.Windows.Forms.Label
        Me.plMessage = New System.Windows.Forms.Panel
        Me.tbSubject = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.dpDate = New System.Windows.Forms.DateTimePicker
        Me.Label13 = New System.Windows.Forms.Label
        Me.tbMessageNo = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.tbMessage = New System.Windows.Forms.TextBox
        Me.ERPMessageHeaderBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tmrHour = New System.Windows.Forms.Timer(Me.components)
        Me.tmrMin = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSec = New System.Windows.Forms.Timer(Me.components)
        Me.plHeader.SuspendLayout()
        Me.plInfo.SuspendLayout()
        Me.plMain.SuspendLayout()
        Me.plERP.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pl1.SuspendLayout()
        Me.plMessage.SuspendLayout()
        CType(Me.ERPMessageHeaderBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'plHeader
        '
        Me.plHeader.BackColor = System.Drawing.Color.Snow
        Me.plHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plHeader.Controls.Add(Me.Panel3)
        Me.plHeader.Controls.Add(Me.lblTime)
        Me.plHeader.Controls.Add(Me.Label2)
        Me.plHeader.Controls.Add(Me.Panel2)
        Me.plHeader.Controls.Add(Me.Panel1)
        Me.plHeader.Location = New System.Drawing.Point(1, 1)
        Me.plHeader.Name = "plHeader"
        Me.plHeader.Size = New System.Drawing.Size(1014, 100)
        Me.plHeader.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Location = New System.Drawing.Point(840, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(75, 95)
        Me.Panel3.TabIndex = 5
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.ForeColor = System.Drawing.Color.Red
        Me.lblTime.Location = New System.Drawing.Point(737, 82)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(35, 17)
        Me.lblTime.TabIndex = 4
        Me.lblTime.Text = "Time"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Book", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(206, -1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(633, 45)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "SOLAR SOLES PRIVATE LIMITED.,"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(95, 95)
        Me.Panel2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(98, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(95, 95)
        Me.Panel1.TabIndex = 1
        '
        'plInfo
        '
        Me.plInfo.BackColor = System.Drawing.Color.Bisque
        Me.plInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plInfo.Controls.Add(Me.lblUserDesignation)
        Me.plInfo.Controls.Add(Me.Label12)
        Me.plInfo.Controls.Add(Me.lblUserName)
        Me.plInfo.Controls.Add(Me.Label10)
        Me.plInfo.Controls.Add(Me.lblDate)
        Me.plInfo.Controls.Add(Me.Label8)
        Me.plInfo.Controls.Add(Me.lblYear)
        Me.plInfo.Controls.Add(Me.Label6)
        Me.plInfo.Controls.Add(Me.lblUnitType)
        Me.plInfo.Controls.Add(Me.Label1)
        Me.plInfo.Location = New System.Drawing.Point(1, 99)
        Me.plInfo.Name = "plInfo"
        Me.plInfo.Size = New System.Drawing.Size(1014, 25)
        Me.plInfo.TabIndex = 1
        '
        'lblUserDesignation
        '
        Me.lblUserDesignation.AutoSize = True
        Me.lblUserDesignation.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUserDesignation.Location = New System.Drawing.Point(900, 3)
        Me.lblUserDesignation.Name = "lblUserDesignation"
        Me.lblUserDesignation.Size = New System.Drawing.Size(84, 17)
        Me.lblUserDesignation.TabIndex = 9
        Me.lblUserDesignation.Text = "Assistant UPO"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Tomato
        Me.Label12.Location = New System.Drawing.Point(800, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 17)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "Designation :-"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUserName.Location = New System.Drawing.Point(640, 3)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(114, 17)
        Me.lblUserName.TabIndex = 7
        Me.lblUserName.Text = "Thanveer Ahmed .P"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Tomato
        Me.Label10.Location = New System.Drawing.Point(550, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 17)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "User Name :-"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblDate.Location = New System.Drawing.Point(370, 3)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(132, 17)
        Me.lblDate.TabIndex = 5
        Me.lblDate.Text = "09 - November - 2009"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Tomato
        Me.Label8.Location = New System.Drawing.Point(320, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 17)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Date :-"
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblYear.Location = New System.Drawing.Point(220, 3)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(81, 17)
        Me.lblYear.TabIndex = 3
        Me.lblYear.Text = "2009 - 2010"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Tomato
        Me.Label6.Location = New System.Drawing.Point(170, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 17)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Year :-"
        '
        'lblUnitType
        '
        Me.lblUnitType.AutoSize = True
        Me.lblUnitType.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUnitType.Location = New System.Drawing.Point(80, 3)
        Me.lblUnitType.Name = "lblUnitType"
        Me.lblUnitType.Size = New System.Drawing.Size(38, 17)
        Me.lblUnitType.TabIndex = 1
        Me.lblUnitType.Text = "Soles"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Tomato
        Me.Label1.Location = New System.Drawing.Point(0, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Unit Type :-"
        '
        'plMain
        '
        Me.plMain.BackColor = System.Drawing.Color.FloralWhite
        Me.plMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plMain.Controls.Add(Me.tb1)
        Me.plMain.Controls.Add(Me.Label15)
        Me.plMain.Controls.Add(Me.cbHideMessage)
        Me.plMain.Controls.Add(Me.cbViewMessage)
        Me.plMain.Controls.Add(Me.cbNextMessage)
        Me.plMain.Controls.Add(Me.cbPreviousMessage)
        Me.plMain.Controls.Add(Me.cbSendMessage)
        Me.plMain.Controls.Add(Me.Label9)
        Me.plMain.Controls.Add(Me.plERP)
        Me.plMain.Controls.Add(Me.tbDatasource)
        Me.plMain.Controls.Add(Me.cbComposeMessage)
        Me.plMain.Controls.Add(Me.tbDatabase)
        Me.plMain.Controls.Add(Me.Label5)
        Me.plMain.Controls.Add(Me.cbExit)
        Me.plMain.Controls.Add(Me.Label7)
        Me.plMain.Controls.Add(Me.Panel4)
        Me.plMain.Controls.Add(Me.pl1)
        Me.plMain.Controls.Add(Me.plMessage)
        Me.plMain.Location = New System.Drawing.Point(1, 123)
        Me.plMain.Name = "plMain"
        Me.plMain.Size = New System.Drawing.Size(1014, 579)
        Me.plMain.TabIndex = 2
        '
        'tb1
        '
        Me.tb1.Location = New System.Drawing.Point(269, 4)
        Me.tb1.Name = "tb1"
        Me.tb1.Size = New System.Drawing.Size(118, 22)
        Me.tb1.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 37)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(322, 22)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Current Version :  8 - 08-06-12"
        '
        'cbHideMessage
        '
        Me.cbHideMessage.Enabled = False
        Me.cbHideMessage.Image = CType(resources.GetObject("cbHideMessage.Image"), System.Drawing.Image)
        Me.cbHideMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbHideMessage.Location = New System.Drawing.Point(495, 519)
        Me.cbHideMessage.Name = "cbHideMessage"
        Me.cbHideMessage.Size = New System.Drawing.Size(120, 55)
        Me.cbHideMessage.TabIndex = 19
        Me.cbHideMessage.Text = "Hide" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Message"
        Me.cbHideMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbHideMessage.UseVisualStyleBackColor = True
        Me.cbHideMessage.Visible = False
        '
        'cbViewMessage
        '
        Me.cbViewMessage.Image = CType(resources.GetObject("cbViewMessage.Image"), System.Drawing.Image)
        Me.cbViewMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbViewMessage.Location = New System.Drawing.Point(372, 519)
        Me.cbViewMessage.Name = "cbViewMessage"
        Me.cbViewMessage.Size = New System.Drawing.Size(120, 55)
        Me.cbViewMessage.TabIndex = 18
        Me.cbViewMessage.Text = "View" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Message"
        Me.cbViewMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbViewMessage.UseVisualStyleBackColor = True
        Me.cbViewMessage.Visible = False
        '
        'cbNextMessage
        '
        Me.cbNextMessage.Enabled = False
        Me.cbNextMessage.Image = CType(resources.GetObject("cbNextMessage.Image"), System.Drawing.Image)
        Me.cbNextMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbNextMessage.Location = New System.Drawing.Point(618, 519)
        Me.cbNextMessage.Name = "cbNextMessage"
        Me.cbNextMessage.Size = New System.Drawing.Size(120, 55)
        Me.cbNextMessage.TabIndex = 17
        Me.cbNextMessage.Text = "&Next" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Message"
        Me.cbNextMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbNextMessage.UseVisualStyleBackColor = True
        Me.cbNextMessage.Visible = False
        '
        'cbPreviousMessage
        '
        Me.cbPreviousMessage.Enabled = False
        Me.cbPreviousMessage.Image = CType(resources.GetObject("cbPreviousMessage.Image"), System.Drawing.Image)
        Me.cbPreviousMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPreviousMessage.Location = New System.Drawing.Point(249, 518)
        Me.cbPreviousMessage.Name = "cbPreviousMessage"
        Me.cbPreviousMessage.Size = New System.Drawing.Size(120, 55)
        Me.cbPreviousMessage.TabIndex = 16
        Me.cbPreviousMessage.Text = "P&revious" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Message"
        Me.cbPreviousMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPreviousMessage.UseVisualStyleBackColor = True
        Me.cbPreviousMessage.Visible = False
        '
        'cbSendMessage
        '
        Me.cbSendMessage.Enabled = False
        Me.cbSendMessage.Image = CType(resources.GetObject("cbSendMessage.Image"), System.Drawing.Image)
        Me.cbSendMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbSendMessage.Location = New System.Drawing.Point(126, 518)
        Me.cbSendMessage.Name = "cbSendMessage"
        Me.cbSendMessage.Size = New System.Drawing.Size(120, 55)
        Me.cbSendMessage.TabIndex = 15
        Me.cbSendMessage.Text = "Sen&d " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Message"
        Me.cbSendMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbSendMessage.UseVisualStyleBackColor = True
        Me.cbSendMessage.Visible = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(242, 22)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Server Name"
        '
        'plERP
        '
        Me.plERP.BackgroundImage = CType(resources.GetObject("plERP.BackgroundImage"), System.Drawing.Image)
        Me.plERP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plERP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plERP.Controls.Add(Me.cbFRM)
        Me.plERP.Controls.Add(Me.cbERP)
        Me.plERP.Controls.Add(Me.cbHRM)
        Me.plERP.Controls.Add(Me.cbMRP)
        Me.plERP.Controls.Add(Me.cbSCM)
        Me.plERP.Controls.Add(Me.cbPPC)
        Me.plERP.Controls.Add(Me.cbCRM)
        Me.plERP.Location = New System.Drawing.Point(558, 4)
        Me.plERP.Name = "plERP"
        Me.plERP.Size = New System.Drawing.Size(450, 450)
        Me.plERP.TabIndex = 0
        '
        'cbFRM
        '
        Me.cbFRM.BackColor = System.Drawing.Color.Transparent
        Me.cbFRM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbFRM.FlatAppearance.BorderSize = 0
        Me.cbFRM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbFRM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbFRM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbFRM.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFRM.ForeColor = System.Drawing.Color.Transparent
        Me.cbFRM.Location = New System.Drawing.Point(317, 50)
        Me.cbFRM.Name = "cbFRM"
        Me.cbFRM.Size = New System.Drawing.Size(99, 86)
        Me.cbFRM.TabIndex = 6
        Me.cbFRM.Text = "&F"
        Me.cbFRM.UseVisualStyleBackColor = False
        '
        'cbERP
        '
        Me.cbERP.BackColor = System.Drawing.Color.Transparent
        Me.cbERP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbERP.FlatAppearance.BorderSize = 0
        Me.cbERP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbERP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbERP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbERP.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbERP.ForeColor = System.Drawing.Color.Transparent
        Me.cbERP.Location = New System.Drawing.Point(175, 160)
        Me.cbERP.Name = "cbERP"
        Me.cbERP.Size = New System.Drawing.Size(91, 129)
        Me.cbERP.TabIndex = 7
        Me.cbERP.UseVisualStyleBackColor = False
        '
        'cbHRM
        '
        Me.cbHRM.BackColor = System.Drawing.Color.Transparent
        Me.cbHRM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbHRM.FlatAppearance.BorderSize = 0
        Me.cbHRM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbHRM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbHRM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbHRM.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHRM.ForeColor = System.Drawing.Color.Transparent
        Me.cbHRM.Location = New System.Drawing.Point(317, 179)
        Me.cbHRM.Name = "cbHRM"
        Me.cbHRM.Size = New System.Drawing.Size(99, 86)
        Me.cbHRM.TabIndex = 5
        Me.cbHRM.Text = "&H"
        Me.cbHRM.UseVisualStyleBackColor = False
        '
        'cbMRP
        '
        Me.cbMRP.BackColor = System.Drawing.Color.Transparent
        Me.cbMRP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbMRP.FlatAppearance.BorderSize = 0
        Me.cbMRP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbMRP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbMRP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbMRP.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMRP.ForeColor = System.Drawing.Color.Transparent
        Me.cbMRP.Location = New System.Drawing.Point(317, 306)
        Me.cbMRP.Name = "cbMRP"
        Me.cbMRP.Size = New System.Drawing.Size(99, 86)
        Me.cbMRP.TabIndex = 4
        Me.cbMRP.Text = "&M"
        Me.cbMRP.UseVisualStyleBackColor = False
        '
        'cbSCM
        '
        Me.cbSCM.BackColor = System.Drawing.Color.Transparent
        Me.cbSCM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbSCM.FlatAppearance.BorderSize = 0
        Me.cbSCM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbSCM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbSCM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbSCM.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSCM.ForeColor = System.Drawing.Color.Transparent
        Me.cbSCM.Location = New System.Drawing.Point(34, 305)
        Me.cbSCM.Name = "cbSCM"
        Me.cbSCM.Size = New System.Drawing.Size(99, 86)
        Me.cbSCM.TabIndex = 3
        Me.cbSCM.Text = "&S"
        Me.cbSCM.UseVisualStyleBackColor = False
        '
        'cbPPC
        '
        Me.cbPPC.BackColor = System.Drawing.Color.Transparent
        Me.cbPPC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbPPC.FlatAppearance.BorderSize = 0
        Me.cbPPC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbPPC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbPPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbPPC.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPPC.ForeColor = System.Drawing.Color.Transparent
        Me.cbPPC.Location = New System.Drawing.Point(36, 179)
        Me.cbPPC.Name = "cbPPC"
        Me.cbPPC.Size = New System.Drawing.Size(99, 86)
        Me.cbPPC.TabIndex = 2
        Me.cbPPC.Text = "&P"
        Me.cbPPC.UseVisualStyleBackColor = False
        '
        'cbCRM
        '
        Me.cbCRM.BackColor = System.Drawing.Color.Transparent
        Me.cbCRM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbCRM.FlatAppearance.BorderSize = 0
        Me.cbCRM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.cbCRM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.cbCRM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbCRM.Font = New System.Drawing.Font("Franklin Gothic Book", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCRM.ForeColor = System.Drawing.Color.Transparent
        Me.cbCRM.Location = New System.Drawing.Point(36, 51)
        Me.cbCRM.Name = "cbCRM"
        Me.cbCRM.Size = New System.Drawing.Size(99, 86)
        Me.cbCRM.TabIndex = 1
        Me.cbCRM.Text = "&C"
        Me.cbCRM.UseVisualStyleBackColor = False
        '
        'tbDatasource
        '
        Me.tbDatasource.BackColor = System.Drawing.Color.White
        Me.tbDatasource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbDatasource.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDatasource.ForeColor = System.Drawing.Color.Blue
        Me.tbDatasource.Location = New System.Drawing.Point(855, 458)
        Me.tbDatasource.Name = "tbDatasource"
        Me.tbDatasource.ReadOnly = True
        Me.tbDatasource.Size = New System.Drawing.Size(150, 22)
        Me.tbDatasource.TabIndex = 7
        Me.tbDatasource.TabStop = False
        '
        'cbComposeMessage
        '
        Me.cbComposeMessage.Image = CType(resources.GetObject("cbComposeMessage.Image"), System.Drawing.Image)
        Me.cbComposeMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbComposeMessage.Location = New System.Drawing.Point(4, 518)
        Me.cbComposeMessage.Name = "cbComposeMessage"
        Me.cbComposeMessage.Size = New System.Drawing.Size(120, 55)
        Me.cbComposeMessage.TabIndex = 1
        Me.cbComposeMessage.Text = "C&ompose Message"
        Me.cbComposeMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbComposeMessage.UseVisualStyleBackColor = True
        Me.cbComposeMessage.Visible = False
        '
        'tbDatabase
        '
        Me.tbDatabase.BackColor = System.Drawing.Color.White
        Me.tbDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbDatabase.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDatabase.ForeColor = System.Drawing.Color.Blue
        Me.tbDatabase.Location = New System.Drawing.Point(856, 485)
        Me.tbDatabase.Name = "tbDatabase"
        Me.tbDatabase.ReadOnly = True
        Me.tbDatabase.Size = New System.Drawing.Size(150, 22)
        Me.tbDatabase.TabIndex = 6
        Me.tbDatabase.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(736, 458)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 22)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Server Name"
        '
        'cbExit
        '
        Me.cbExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(888, 518)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(120, 55)
        Me.cbExit.TabIndex = 1
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(737, 485)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 22)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Database Name"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.NavajoWhite
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.tbIPAddress)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.tbSystem)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Location = New System.Drawing.Point(558, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(273, 55)
        Me.Panel4.TabIndex = 2
        '
        'tbIPAddress
        '
        Me.tbIPAddress.BackColor = System.Drawing.Color.White
        Me.tbIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbIPAddress.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbIPAddress.ForeColor = System.Drawing.Color.Blue
        Me.tbIPAddress.Location = New System.Drawing.Point(123, 27)
        Me.tbIPAddress.Name = "tbIPAddress"
        Me.tbIPAddress.ReadOnly = True
        Me.tbIPAddress.Size = New System.Drawing.Size(150, 22)
        Me.tbIPAddress.TabIndex = 5
        Me.tbIPAddress.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 22)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "I.P Address"
        '
        'tbSystem
        '
        Me.tbSystem.BackColor = System.Drawing.Color.White
        Me.tbSystem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbSystem.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSystem.ForeColor = System.Drawing.Color.Blue
        Me.tbSystem.Location = New System.Drawing.Point(123, 5)
        Me.tbSystem.Name = "tbSystem"
        Me.tbSystem.ReadOnly = True
        Me.tbSystem.Size = New System.Drawing.Size(150, 22)
        Me.tbSystem.TabIndex = 1
        Me.tbSystem.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.AntiqueWhite
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 22)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "System Name"
        '
        'pl1
        '
        Me.pl1.Controls.Add(Me.lblImageERP)
        Me.pl1.Controls.Add(Me.lblImageFRM)
        Me.pl1.Controls.Add(Me.lblImageHRM)
        Me.pl1.Controls.Add(Me.lblImageMRP)
        Me.pl1.Controls.Add(Me.lblImagePPC)
        Me.pl1.Controls.Add(Me.lblImageCRM)
        Me.pl1.Controls.Add(Me.lblImageSCM)
        Me.pl1.Controls.Add(Me.lblMRP)
        Me.pl1.Controls.Add(Me.lblSCM)
        Me.pl1.Controls.Add(Me.lblCRM)
        Me.pl1.Controls.Add(Me.lblPPC)
        Me.pl1.Controls.Add(Me.lblERP)
        Me.pl1.Controls.Add(Me.lblFRM)
        Me.pl1.Controls.Add(Me.lblHRM)
        Me.pl1.Location = New System.Drawing.Point(737, 63)
        Me.pl1.Name = "pl1"
        Me.pl1.Size = New System.Drawing.Size(265, 373)
        Me.pl1.TabIndex = 5
        Me.pl1.Visible = False
        '
        'lblImageERP
        '
        Me.lblImageERP.BackgroundImage = CType(resources.GetObject("lblImageERP.BackgroundImage"), System.Drawing.Image)
        Me.lblImageERP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImageERP.Location = New System.Drawing.Point(4, 4)
        Me.lblImageERP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImageERP.Name = "lblImageERP"
        Me.lblImageERP.Size = New System.Drawing.Size(255, 255)
        Me.lblImageERP.TabIndex = 16
        '
        'lblImageFRM
        '
        Me.lblImageFRM.BackgroundImage = CType(resources.GetObject("lblImageFRM.BackgroundImage"), System.Drawing.Image)
        Me.lblImageFRM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImageFRM.Location = New System.Drawing.Point(4, 4)
        Me.lblImageFRM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImageFRM.Name = "lblImageFRM"
        Me.lblImageFRM.Size = New System.Drawing.Size(255, 255)
        Me.lblImageFRM.TabIndex = 14
        '
        'lblImageHRM
        '
        Me.lblImageHRM.BackgroundImage = CType(resources.GetObject("lblImageHRM.BackgroundImage"), System.Drawing.Image)
        Me.lblImageHRM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImageHRM.Location = New System.Drawing.Point(4, 4)
        Me.lblImageHRM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImageHRM.Name = "lblImageHRM"
        Me.lblImageHRM.Size = New System.Drawing.Size(255, 255)
        Me.lblImageHRM.TabIndex = 12
        '
        'lblImageMRP
        '
        Me.lblImageMRP.BackgroundImage = CType(resources.GetObject("lblImageMRP.BackgroundImage"), System.Drawing.Image)
        Me.lblImageMRP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImageMRP.Location = New System.Drawing.Point(4, 4)
        Me.lblImageMRP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImageMRP.Name = "lblImageMRP"
        Me.lblImageMRP.Size = New System.Drawing.Size(255, 255)
        Me.lblImageMRP.TabIndex = 10
        '
        'lblImagePPC
        '
        Me.lblImagePPC.BackgroundImage = CType(resources.GetObject("lblImagePPC.BackgroundImage"), System.Drawing.Image)
        Me.lblImagePPC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImagePPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblImagePPC.Location = New System.Drawing.Point(4, 4)
        Me.lblImagePPC.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImagePPC.Name = "lblImagePPC"
        Me.lblImagePPC.Size = New System.Drawing.Size(255, 255)
        Me.lblImagePPC.TabIndex = 8
        '
        'lblImageCRM
        '
        Me.lblImageCRM.BackgroundImage = CType(resources.GetObject("lblImageCRM.BackgroundImage"), System.Drawing.Image)
        Me.lblImageCRM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImageCRM.Location = New System.Drawing.Point(4, 4)
        Me.lblImageCRM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImageCRM.Name = "lblImageCRM"
        Me.lblImageCRM.Size = New System.Drawing.Size(255, 255)
        Me.lblImageCRM.TabIndex = 1
        '
        'lblImageSCM
        '
        Me.lblImageSCM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lblImageSCM.Location = New System.Drawing.Point(4, 4)
        Me.lblImageSCM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblImageSCM.Name = "lblImageSCM"
        Me.lblImageSCM.Size = New System.Drawing.Size(255, 256)
        Me.lblImageSCM.TabIndex = 7
        '
        'lblMRP
        '
        Me.lblMRP.BackColor = System.Drawing.Color.Transparent
        Me.lblMRP.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRP.ForeColor = System.Drawing.Color.Purple
        Me.lblMRP.Location = New System.Drawing.Point(4, 264)
        Me.lblMRP.Name = "lblMRP"
        Me.lblMRP.Size = New System.Drawing.Size(255, 180)
        Me.lblMRP.TabIndex = 9
        Me.lblMRP.Text = "Almost all quality improvement comes via simplification of design, manufacturing." & _
            ".. layout, processes, and procedures."
        '
        'lblSCM
        '
        Me.lblSCM.BackColor = System.Drawing.Color.Transparent
        Me.lblSCM.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSCM.ForeColor = System.Drawing.Color.Blue
        Me.lblSCM.Location = New System.Drawing.Point(4, 264)
        Me.lblSCM.Name = "lblSCM"
        Me.lblSCM.Size = New System.Drawing.Size(255, 180)
        Me.lblSCM.TabIndex = 4
        Me.lblSCM.Text = "The best supply chains aren't just fast and cost-effective. They are also agile a" & _
            "nd adaptable, and they ensure that all their companies' interests stay aligned" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
            ""
        '
        'lblCRM
        '
        Me.lblCRM.BackColor = System.Drawing.Color.Transparent
        Me.lblCRM.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRM.ForeColor = System.Drawing.Color.Green
        Me.lblCRM.Location = New System.Drawing.Point(4, 264)
        Me.lblCRM.Name = "lblCRM"
        Me.lblCRM.Size = New System.Drawing.Size(255, 180)
        Me.lblCRM.TabIndex = 3
        Me.lblCRM.Text = "We see our customers as invited guests to a party, and we are the hosts." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It's ou" & _
            "r job every day to make every important aspect of the customer experience a litt" & _
            "le bit better."
        '
        'lblPPC
        '
        Me.lblPPC.BackColor = System.Drawing.Color.Transparent
        Me.lblPPC.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPPC.ForeColor = System.Drawing.Color.DarkCyan
        Me.lblPPC.Location = New System.Drawing.Point(4, 264)
        Me.lblPPC.Name = "lblPPC"
        Me.lblPPC.Size = New System.Drawing.Size(255, 180)
        Me.lblPPC.TabIndex = 5
        '
        'lblERP
        '
        Me.lblERP.BackColor = System.Drawing.Color.Transparent
        Me.lblERP.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblERP.ForeColor = System.Drawing.Color.Black
        Me.lblERP.Location = New System.Drawing.Point(4, 264)
        Me.lblERP.Name = "lblERP"
        Me.lblERP.Size = New System.Drawing.Size(255, 180)
        Me.lblERP.TabIndex = 15
        '
        'lblFRM
        '
        Me.lblFRM.BackColor = System.Drawing.Color.Transparent
        Me.lblFRM.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFRM.ForeColor = System.Drawing.Color.Maroon
        Me.lblFRM.Location = New System.Drawing.Point(4, 264)
        Me.lblFRM.Name = "lblFRM"
        Me.lblFRM.Size = New System.Drawing.Size(255, 180)
        Me.lblFRM.TabIndex = 13
        Me.lblFRM.Text = "Business is not financial science, it's about trading.. buying and selling. It's " & _
            "about creating a product or service so good that people will pay for it."
        '
        'lblHRM
        '
        Me.lblHRM.BackColor = System.Drawing.Color.Transparent
        Me.lblHRM.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHRM.ForeColor = System.Drawing.Color.Chocolate
        Me.lblHRM.Location = New System.Drawing.Point(4, 264)
        Me.lblHRM.Name = "lblHRM"
        Me.lblHRM.Size = New System.Drawing.Size(255, 180)
        Me.lblHRM.TabIndex = 11
        Me.lblHRM.Text = "Working together does not only brings out the best in all of us: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "it brings out " & _
            "the best in each one of us."
        '
        'plMessage
        '
        Me.plMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plMessage.Controls.Add(Me.tbSubject)
        Me.plMessage.Controls.Add(Me.Label14)
        Me.plMessage.Controls.Add(Me.dpDate)
        Me.plMessage.Controls.Add(Me.Label13)
        Me.plMessage.Controls.Add(Me.tbMessageNo)
        Me.plMessage.Controls.Add(Me.Label11)
        Me.plMessage.Controls.Add(Me.tbMessage)
        Me.plMessage.Location = New System.Drawing.Point(477, 4)
        Me.plMessage.Name = "plMessage"
        Me.plMessage.Size = New System.Drawing.Size(531, 512)
        Me.plMessage.TabIndex = 20
        Me.plMessage.Visible = False
        '
        'tbSubject
        '
        Me.tbSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbSubject.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSubject.ForeColor = System.Drawing.Color.Blue
        Me.tbSubject.Location = New System.Drawing.Point(84, 30)
        Me.tbSubject.MaxLength = 50
        Me.tbSubject.Name = "tbSubject"
        Me.tbSubject.Size = New System.Drawing.Size(441, 22)
        Me.tbSubject.TabIndex = 19
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(4, 30)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(49, 17)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Subject"
        '
        'dpDate
        '
        Me.dpDate.CustomFormat = "dddd - dd - MMMM - yyyy"
        Me.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpDate.Location = New System.Drawing.Point(211, 4)
        Me.dpDate.Name = "dpDate"
        Me.dpDate.Size = New System.Drawing.Size(225, 22)
        Me.dpDate.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(162, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 17)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Date :-"
        '
        'tbMessageNo
        '
        Me.tbMessageNo.BackColor = System.Drawing.Color.White
        Me.tbMessageNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbMessageNo.ForeColor = System.Drawing.Color.Red
        Me.tbMessageNo.Location = New System.Drawing.Point(84, 4)
        Me.tbMessageNo.Name = "tbMessageNo"
        Me.tbMessageNo.ReadOnly = True
        Me.tbMessageNo.Size = New System.Drawing.Size(76, 22)
        Me.tbMessageNo.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 17)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Message No."
        '
        'tbMessage
        '
        Me.tbMessage.Location = New System.Drawing.Point(4, 56)
        Me.tbMessage.MaxLength = 1600
        Me.tbMessage.Multiline = True
        Me.tbMessage.Name = "tbMessage"
        Me.tbMessage.Size = New System.Drawing.Size(521, 450)
        Me.tbMessage.TabIndex = 13
        '
        'ERPMessageHeaderBindingSource
        '
        Me.ERPMessageHeaderBindingSource.DataMember = "ERPMessageHeader"
        '
        'Timer1
        '
        '
        'tmrHour
        '
        Me.tmrHour.Interval = 3600000
        '
        'tmrMin
        '
        Me.tmrMin.Interval = 6000
        '
        'tmrSec
        '
        Me.tmrSec.Interval = 1000
        '
        'frmmdiSolarERP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Beige
        Me.CancelButton = Me.cbExit
        Me.ClientSize = New System.Drawing.Size(1016, 703)
        Me.Controls.Add(Me.plInfo)
        Me.Controls.Add(Me.plHeader)
        Me.Controls.Add(Me.plMain)
        Me.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsMdiContainer = True
        Me.Name = "frmmdiSolarERP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Solar Enterprises Resources Planning (SERP)"
        Me.plHeader.ResumeLayout(False)
        Me.plHeader.PerformLayout()
        Me.plInfo.ResumeLayout(False)
        Me.plInfo.PerformLayout()
        Me.plMain.ResumeLayout(False)
        Me.plMain.PerformLayout()
        Me.plERP.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pl1.ResumeLayout(False)
        Me.plMessage.ResumeLayout(False)
        Me.plMessage.PerformLayout()
        CType(Me.ERPMessageHeaderBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents plHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents plInfo As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUnitType As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblUserDesignation As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents plMain As System.Windows.Forms.Panel
    Friend WithEvents plERP As System.Windows.Forms.Panel
    Friend WithEvents cbCRM As System.Windows.Forms.Button
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbFRM As System.Windows.Forms.Button
    Friend WithEvents cbHRM As System.Windows.Forms.Button
    Friend WithEvents cbMRP As System.Windows.Forms.Button
    Friend WithEvents cbSCM As System.Windows.Forms.Button
    Friend WithEvents cbPPC As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cbERP As System.Windows.Forms.Button
    Friend WithEvents tmrHour As System.Windows.Forms.Timer
    Friend WithEvents tmrMin As System.Windows.Forms.Timer
    Friend WithEvents tmrSec As System.Windows.Forms.Timer
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbSystem As System.Windows.Forms.TextBox
    Friend WithEvents tbDatasource As System.Windows.Forms.TextBox
    Friend WithEvents tbDatabase As System.Windows.Forms.TextBox
    Friend WithEvents tbIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pl1 As System.Windows.Forms.Panel
    Friend WithEvents lblCRM As System.Windows.Forms.Label
    Friend WithEvents lblSCM As System.Windows.Forms.Label
    Friend WithEvents lblPPC As System.Windows.Forms.Label
    Friend WithEvents lblImageSCM As System.Windows.Forms.Panel
    Friend WithEvents lblImageCRM As System.Windows.Forms.Panel
    Friend WithEvents lblImagePPC As System.Windows.Forms.Panel
    Friend WithEvents lblMRP As System.Windows.Forms.Label
    Friend WithEvents lblImageMRP As System.Windows.Forms.Panel
    Friend WithEvents lblHRM As System.Windows.Forms.Label
    Friend WithEvents lblImageHRM As System.Windows.Forms.Panel
    Friend WithEvents lblFRM As System.Windows.Forms.Label
    Friend WithEvents lblImageFRM As System.Windows.Forms.Panel
    Friend WithEvents lblERP As System.Windows.Forms.Label
    Friend WithEvents lblImageERP As System.Windows.Forms.Panel
    Friend WithEvents cbComposeMessage As System.Windows.Forms.Button
    Friend WithEvents tbMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbSendMessage As System.Windows.Forms.Button
    Friend WithEvents cbNextMessage As System.Windows.Forms.Button
    Friend WithEvents cbPreviousMessage As System.Windows.Forms.Button
    Friend WithEvents cbHideMessage As System.Windows.Forms.Button
    Friend WithEvents cbViewMessage As System.Windows.Forms.Button
    Friend WithEvents plMessage As System.Windows.Forms.Panel
    Friend WithEvents tbMessageNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tbSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ERPMessageHeaderBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tb1 As System.Windows.Forms.TextBox

End Class
