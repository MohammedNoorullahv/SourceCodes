<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainWinForm
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition3 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition4 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainWinForm))
        Me.bntStart = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdActiveUser = New DevExpress.XtraGrid.GridControl
        Me.grdActiveUserV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdSelectedUser = New DevExpress.XtraGrid.GridControl
        Me.grdSelectedUserV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbSelectSingle = New System.Windows.Forms.Button
        Me.cbSelectAll = New System.Windows.Forms.Button
        Me.cbRemoveAll = New System.Windows.Forms.Button
        Me.cbRemoveSingle = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkbxAcknowledge = New System.Windows.Forms.CheckBox
        Me.cbSend = New System.Windows.Forms.Button
        Me.rtbMessage = New System.Windows.Forms.RichTextBox
        Me.cbExit = New System.Windows.Forms.Button
        Me.plMessageInfo = New System.Windows.Forms.GroupBox
        Me.cbAcknowledgement = New System.Windows.Forms.Button
        Me.tbSystem = New System.Windows.Forms.TextBox
        Me.tbFrom = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbOK = New System.Windows.Forms.Button
        Me.rtbReceivedMessage = New System.Windows.Forms.RichTextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cbRefresh = New System.Windows.Forms.Button
        Me.grdPendingAcknowledgement = New DevExpress.XtraGrid.GridControl
        Me.grdPendingAcknowledgementV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbHistory = New System.Windows.Forms.Button
        Me.plHistory = New System.Windows.Forms.GroupBox
        Me.cbExport2Excel = New System.Windows.Forms.Button
        Me.cbRefreshHistory = New System.Windows.Forms.Button
        Me.dpToDate = New System.Windows.Forms.DateTimePicker
        Me.dpFromDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.rbReceivedHistory = New System.Windows.Forms.RadioButton
        Me.rbSentHistory = New System.Windows.Forms.RadioButton
        Me.grdHistory = New DevExpress.XtraGrid.GridControl
        Me.grdHistoryV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbExitHistory = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.tbUserName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tbLoginName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbSystemName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.tbSentOn = New System.Windows.Forms.TextBox
        Me.cbReply = New System.Windows.Forms.Button
        Me.cbReplyAll = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdActiveUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdActiveUserV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdSelectedUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSelectedUserV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.plMessageInfo.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdPendingAcknowledgement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPendingAcknowledgementV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plHistory.SuspendLayout()
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdHistoryV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'bntStart
        '
        Me.bntStart.Location = New System.Drawing.Point(876, 521)
        Me.bntStart.Margin = New System.Windows.Forms.Padding(4)
        Me.bntStart.Name = "bntStart"
        Me.bntStart.Size = New System.Drawing.Size(100, 28)
        Me.bntStart.TabIndex = 0
        Me.bntStart.Text = "Start"
        Me.bntStart.UseVisualStyleBackColor = True
        Me.bntStart.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.grdActiveUser)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(650, 300)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "List of Active Users / Select User to Whom Message(S) has to be sent :-"
        '
        'grdActiveUser
        '
        Me.grdActiveUser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdActiveUser.Location = New System.Drawing.Point(7, 23)
        Me.grdActiveUser.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdActiveUser.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.grdActiveUser.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdActiveUser.MainView = Me.grdActiveUserV1
        Me.grdActiveUser.Name = "grdActiveUser"
        Me.grdActiveUser.Size = New System.Drawing.Size(636, 270)
        Me.grdActiveUser.TabIndex = 114
        Me.grdActiveUser.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdActiveUserV1})
        '
        'grdActiveUserV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition1.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdActiveUserV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdActiveUserV1.GridControl = Me.grdActiveUser
        Me.grdActiveUserV1.Name = "grdActiveUserV1"
        Me.grdActiveUserV1.OptionsView.ShowAutoFilterRow = True
        Me.grdActiveUserV1.OptionsView.ShowFooter = True
        Me.grdActiveUserV1.OptionsView.ShowGroupPanel = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.grdSelectedUser)
        Me.GroupBox2.Location = New System.Drawing.Point(714, 15)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(262, 300)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Selected User(S) :-"
        '
        'grdSelectedUser
        '
        Me.grdSelectedUser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSelectedUser.Location = New System.Drawing.Point(7, 23)
        Me.grdSelectedUser.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdSelectedUser.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.grdSelectedUser.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdSelectedUser.MainView = Me.grdSelectedUserV1
        Me.grdSelectedUser.Name = "grdSelectedUser"
        Me.grdSelectedUser.Size = New System.Drawing.Size(253, 270)
        Me.grdSelectedUser.TabIndex = 115
        Me.grdSelectedUser.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSelectedUserV1})
        '
        'grdSelectedUserV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.ApplyToRow = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdSelectedUserV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdSelectedUserV1.GridControl = Me.grdSelectedUser
        Me.grdSelectedUserV1.Name = "grdSelectedUserV1"
        Me.grdSelectedUserV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSelectedUserV1.OptionsView.ShowFooter = True
        Me.grdSelectedUserV1.OptionsView.ShowGroupPanel = False
        '
        'cbSelectSingle
        '
        Me.cbSelectSingle.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSelectSingle.ForeColor = System.Drawing.Color.Green
        Me.cbSelectSingle.Location = New System.Drawing.Point(661, 100)
        Me.cbSelectSingle.Name = "cbSelectSingle"
        Me.cbSelectSingle.Size = New System.Drawing.Size(50, 31)
        Me.cbSelectSingle.TabIndex = 3
        Me.cbSelectSingle.Text = ">"
        Me.cbSelectSingle.UseVisualStyleBackColor = True
        '
        'cbSelectAll
        '
        Me.cbSelectAll.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSelectAll.ForeColor = System.Drawing.Color.Green
        Me.cbSelectAll.Location = New System.Drawing.Point(661, 139)
        Me.cbSelectAll.Name = "cbSelectAll"
        Me.cbSelectAll.Size = New System.Drawing.Size(50, 31)
        Me.cbSelectAll.TabIndex = 4
        Me.cbSelectAll.Text = ">>"
        Me.cbSelectAll.UseVisualStyleBackColor = True
        '
        'cbRemoveAll
        '
        Me.cbRemoveAll.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRemoveAll.ForeColor = System.Drawing.Color.Red
        Me.cbRemoveAll.Location = New System.Drawing.Point(661, 175)
        Me.cbRemoveAll.Name = "cbRemoveAll"
        Me.cbRemoveAll.Size = New System.Drawing.Size(50, 31)
        Me.cbRemoveAll.TabIndex = 6
        Me.cbRemoveAll.Text = "<<"
        Me.cbRemoveAll.UseVisualStyleBackColor = True
        '
        'cbRemoveSingle
        '
        Me.cbRemoveSingle.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRemoveSingle.ForeColor = System.Drawing.Color.Red
        Me.cbRemoveSingle.Location = New System.Drawing.Point(661, 211)
        Me.cbRemoveSingle.Name = "cbRemoveSingle"
        Me.cbRemoveSingle.Size = New System.Drawing.Size(50, 31)
        Me.cbRemoveSingle.TabIndex = 5
        Me.cbRemoveSingle.Text = "<"
        Me.cbRemoveSingle.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox3.Controls.Add(Me.chkbxAcknowledge)
        Me.GroupBox3.Controls.Add(Me.cbSend)
        Me.GroupBox3.Controls.Add(Me.rtbMessage)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 323)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(650, 226)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Message / Instruction :-"
        '
        'chkbxAcknowledge
        '
        Me.chkbxAcknowledge.AutoSize = True
        Me.chkbxAcknowledge.Location = New System.Drawing.Point(499, 25)
        Me.chkbxAcknowledge.Name = "chkbxAcknowledge"
        Me.chkbxAcknowledge.Size = New System.Drawing.Size(113, 20)
        Me.chkbxAcknowledge.TabIndex = 2
        Me.chkbxAcknowledge.Text = "Acknowledge"
        Me.chkbxAcknowledge.UseVisualStyleBackColor = True
        '
        'cbSend
        '
        Me.cbSend.Location = New System.Drawing.Point(490, 196)
        Me.cbSend.Name = "cbSend"
        Me.cbSend.Size = New System.Drawing.Size(75, 23)
        Me.cbSend.TabIndex = 1
        Me.cbSend.Text = "Send"
        Me.cbSend.UseVisualStyleBackColor = True
        '
        'rtbMessage
        '
        Me.rtbMessage.Location = New System.Drawing.Point(7, 23)
        Me.rtbMessage.MaxLength = 500
        Me.rtbMessage.Name = "rtbMessage"
        Me.rtbMessage.Size = New System.Drawing.Size(477, 196)
        Me.rtbMessage.TabIndex = 0
        Me.rtbMessage.Text = ""
        '
        'cbExit
        '
        Me.cbExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExit.Location = New System.Drawing.Point(876, 521)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(100, 28)
        Me.cbExit.TabIndex = 8
        Me.cbExit.Text = "E&xit"
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'plMessageInfo
        '
        Me.plMessageInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plMessageInfo.Controls.Add(Me.cbReplyAll)
        Me.plMessageInfo.Controls.Add(Me.cbReply)
        Me.plMessageInfo.Controls.Add(Me.tbSentOn)
        Me.plMessageInfo.Controls.Add(Me.Label8)
        Me.plMessageInfo.Controls.Add(Me.tbSystem)
        Me.plMessageInfo.Controls.Add(Me.tbFrom)
        Me.plMessageInfo.Controls.Add(Me.Label2)
        Me.plMessageInfo.Controls.Add(Me.Label1)
        Me.plMessageInfo.Controls.Add(Me.cbOK)
        Me.plMessageInfo.Controls.Add(Me.cbAcknowledgement)
        Me.plMessageInfo.Controls.Add(Me.rtbReceivedMessage)
        Me.plMessageInfo.Location = New System.Drawing.Point(92, 158)
        Me.plMessageInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.plMessageInfo.Name = "plMessageInfo"
        Me.plMessageInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.plMessageInfo.Size = New System.Drawing.Size(800, 300)
        Me.plMessageInfo.TabIndex = 9
        Me.plMessageInfo.TabStop = False
        Me.plMessageInfo.Text = "Received Message"
        Me.plMessageInfo.Visible = False
        '
        'cbAcknowledgement
        '
        Me.cbAcknowledgement.Font = New System.Drawing.Font("Verdana", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAcknowledgement.ForeColor = System.Drawing.Color.Red
        Me.cbAcknowledgement.Location = New System.Drawing.Point(7, 52)
        Me.cbAcknowledgement.Name = "cbAcknowledgement"
        Me.cbAcknowledgement.Size = New System.Drawing.Size(786, 241)
        Me.cbAcknowledgement.TabIndex = 6
        Me.cbAcknowledgement.Text = "Acknowledgement" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Requested" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "By" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Sender"
        Me.cbAcknowledgement.UseVisualStyleBackColor = True
        Me.cbAcknowledgement.Visible = False
        '
        'tbSystem
        '
        Me.tbSystem.Location = New System.Drawing.Point(327, 20)
        Me.tbSystem.Name = "tbSystem"
        Me.tbSystem.Size = New System.Drawing.Size(150, 23)
        Me.tbSystem.TabIndex = 5
        '
        'tbFrom
        '
        Me.tbFrom.Location = New System.Drawing.Point(71, 20)
        Me.tbFrom.Name = "tbFrom"
        Me.tbFrom.Size = New System.Drawing.Size(150, 23)
        Me.tbFrom.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(240, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "System :-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From :-"
        '
        'cbOK
        '
        Me.cbOK.Location = New System.Drawing.Point(487, 270)
        Me.cbOK.Name = "cbOK"
        Me.cbOK.Size = New System.Drawing.Size(100, 23)
        Me.cbOK.TabIndex = 1
        Me.cbOK.Text = "O K"
        Me.cbOK.UseVisualStyleBackColor = True
        '
        'rtbReceivedMessage
        '
        Me.rtbReceivedMessage.Location = New System.Drawing.Point(7, 52)
        Me.rtbReceivedMessage.Name = "rtbReceivedMessage"
        Me.rtbReceivedMessage.ReadOnly = True
        Me.rtbReceivedMessage.Size = New System.Drawing.Size(470, 241)
        Me.rtbReceivedMessage.TabIndex = 0
        Me.rtbReceivedMessage.Text = ""
        '
        'Timer1
        '
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox4.Controls.Add(Me.cbRefresh)
        Me.GroupBox4.Controls.Add(Me.grdPendingAcknowledgement)
        Me.GroupBox4.Location = New System.Drawing.Point(661, 323)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(315, 193)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Acknowledgement(s) Pending From :-"
        '
        'cbRefresh
        '
        Me.cbRefresh.Location = New System.Drawing.Point(7, 163)
        Me.cbRefresh.Name = "cbRefresh"
        Me.cbRefresh.Size = New System.Drawing.Size(301, 23)
        Me.cbRefresh.TabIndex = 116
        Me.cbRefresh.Text = "Refresh to View for Pending Acknowlegement "
        Me.cbRefresh.UseVisualStyleBackColor = True
        '
        'grdPendingAcknowledgement
        '
        Me.grdPendingAcknowledgement.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPendingAcknowledgement.Location = New System.Drawing.Point(7, 23)
        Me.grdPendingAcknowledgement.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdPendingAcknowledgement.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.grdPendingAcknowledgement.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdPendingAcknowledgement.MainView = Me.grdPendingAcknowledgementV1
        Me.grdPendingAcknowledgement.Name = "grdPendingAcknowledgement"
        Me.grdPendingAcknowledgement.Size = New System.Drawing.Size(301, 135)
        Me.grdPendingAcknowledgement.TabIndex = 115
        Me.grdPendingAcknowledgement.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdPendingAcknowledgementV1})
        '
        'grdPendingAcknowledgementV1
        '
        StyleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition3.Appearance.Options.UseForeColor = True
        StyleFormatCondition3.ApplyToRow = True
        StyleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition3.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdPendingAcknowledgementV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition3})
        Me.grdPendingAcknowledgementV1.GridControl = Me.grdPendingAcknowledgement
        Me.grdPendingAcknowledgementV1.Name = "grdPendingAcknowledgementV1"
        Me.grdPendingAcknowledgementV1.OptionsView.ColumnAutoWidth = False
        Me.grdPendingAcknowledgementV1.OptionsView.ShowAutoFilterRow = True
        Me.grdPendingAcknowledgementV1.OptionsView.ShowFooter = True
        Me.grdPendingAcknowledgementV1.OptionsView.ShowGroupPanel = False
        '
        'cbHistory
        '
        Me.cbHistory.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbHistory.Location = New System.Drawing.Point(662, 519)
        Me.cbHistory.Margin = New System.Windows.Forms.Padding(4)
        Me.cbHistory.Name = "cbHistory"
        Me.cbHistory.Size = New System.Drawing.Size(100, 28)
        Me.cbHistory.TabIndex = 12
        Me.cbHistory.Text = "&History"
        Me.cbHistory.UseVisualStyleBackColor = True
        '
        'plHistory
        '
        Me.plHistory.BackColor = System.Drawing.Color.Silver
        Me.plHistory.Controls.Add(Me.cbExport2Excel)
        Me.plHistory.Controls.Add(Me.cbRefreshHistory)
        Me.plHistory.Controls.Add(Me.dpToDate)
        Me.plHistory.Controls.Add(Me.dpFromDate)
        Me.plHistory.Controls.Add(Me.Label5)
        Me.plHistory.Controls.Add(Me.Label4)
        Me.plHistory.Controls.Add(Me.rbReceivedHistory)
        Me.plHistory.Controls.Add(Me.rbSentHistory)
        Me.plHistory.Controls.Add(Me.grdHistory)
        Me.plHistory.Controls.Add(Me.cbExitHistory)
        Me.plHistory.Location = New System.Drawing.Point(4, 2)
        Me.plHistory.Name = "plHistory"
        Me.plHistory.Size = New System.Drawing.Size(972, 548)
        Me.plHistory.TabIndex = 13
        Me.plHistory.TabStop = False
        Me.plHistory.Text = "History"
        Me.plHistory.Visible = False
        '
        'cbExport2Excel
        '
        Me.cbExport2Excel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExport2Excel.Location = New System.Drawing.Point(112, 514)
        Me.cbExport2Excel.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExport2Excel.Name = "cbExport2Excel"
        Me.cbExport2Excel.Size = New System.Drawing.Size(113, 28)
        Me.cbExport2Excel.TabIndex = 123
        Me.cbExport2Excel.Text = "Export 2 Excel"
        Me.cbExport2Excel.UseVisualStyleBackColor = True
        '
        'cbRefreshHistory
        '
        Me.cbRefreshHistory.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbRefreshHistory.Location = New System.Drawing.Point(9, 514)
        Me.cbRefreshHistory.Margin = New System.Windows.Forms.Padding(4)
        Me.cbRefreshHistory.Name = "cbRefreshHistory"
        Me.cbRefreshHistory.Size = New System.Drawing.Size(100, 28)
        Me.cbRefreshHistory.TabIndex = 122
        Me.cbRefreshHistory.Text = "Refresh"
        Me.cbRefreshHistory.UseVisualStyleBackColor = True
        '
        'dpToDate
        '
        Me.dpToDate.CustomFormat = "dd-MMMM-yyyy"
        Me.dpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpToDate.Location = New System.Drawing.Point(763, 22)
        Me.dpToDate.Name = "dpToDate"
        Me.dpToDate.Size = New System.Drawing.Size(200, 23)
        Me.dpToDate.TabIndex = 121
        '
        'dpFromDate
        '
        Me.dpFromDate.CustomFormat = "dd-MMMM-yyyy"
        Me.dpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFromDate.Location = New System.Drawing.Point(526, 22)
        Me.dpFromDate.Name = "dpFromDate"
        Me.dpFromDate.Size = New System.Drawing.Size(200, 23)
        Me.dpFromDate.TabIndex = 120
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(732, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 16)
        Me.Label5.TabIndex = 119
        Me.Label5.Text = "To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(480, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 16)
        Me.Label4.TabIndex = 118
        Me.Label4.Text = "From"
        '
        'rbReceivedHistory
        '
        Me.rbReceivedHistory.AutoSize = True
        Me.rbReceivedHistory.Location = New System.Drawing.Point(140, 22)
        Me.rbReceivedHistory.Name = "rbReceivedHistory"
        Me.rbReceivedHistory.Size = New System.Drawing.Size(154, 20)
        Me.rbReceivedHistory.TabIndex = 117
        Me.rbReceivedHistory.Text = "Received Messages"
        Me.rbReceivedHistory.UseVisualStyleBackColor = True
        '
        'rbSentHistory
        '
        Me.rbSentHistory.AutoSize = True
        Me.rbSentHistory.Checked = True
        Me.rbSentHistory.Location = New System.Drawing.Point(8, 22)
        Me.rbSentHistory.Name = "rbSentHistory"
        Me.rbSentHistory.Size = New System.Drawing.Size(126, 20)
        Me.rbSentHistory.TabIndex = 116
        Me.rbSentHistory.TabStop = True
        Me.rbSentHistory.Text = "Sent Messages"
        Me.rbSentHistory.UseVisualStyleBackColor = True
        '
        'grdHistory
        '
        Me.grdHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridLevelNode1.RelationName = "Level1"
        Me.grdHistory.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdHistory.Location = New System.Drawing.Point(8, 57)
        Me.grdHistory.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdHistory.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D
        Me.grdHistory.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdHistory.MainView = Me.grdHistoryV1
        Me.grdHistory.Name = "grdHistory"
        Me.grdHistory.Size = New System.Drawing.Size(957, 453)
        Me.grdHistory.TabIndex = 115
        Me.grdHistory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdHistoryV1})
        '
        'grdHistoryV1
        '
        StyleFormatCondition4.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition4.Appearance.Options.UseForeColor = True
        StyleFormatCondition4.ApplyToRow = True
        StyleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition4.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdHistoryV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition4})
        Me.grdHistoryV1.GridControl = Me.grdHistory
        Me.grdHistoryV1.Name = "grdHistoryV1"
        Me.grdHistoryV1.OptionsView.ShowAutoFilterRow = True
        Me.grdHistoryV1.OptionsView.ShowFooter = True
        Me.grdHistoryV1.OptionsView.ShowGroupPanel = False
        '
        'cbExitHistory
        '
        Me.cbExitHistory.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExitHistory.Location = New System.Drawing.Point(865, 513)
        Me.cbExitHistory.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExitHistory.Name = "cbExitHistory"
        Me.cbExitHistory.Size = New System.Drawing.Size(100, 28)
        Me.cbExitHistory.TabIndex = 9
        Me.cbExitHistory.Text = "Exit &History"
        Me.cbExitHistory.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.tbUserName)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.tbLoginName)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.tbSystemName)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Location = New System.Drawing.Point(4, 556)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(972, 54)
        Me.GroupBox5.TabIndex = 14
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "System Info :-"
        '
        'tbUserName
        '
        Me.tbUserName.BackColor = System.Drawing.Color.White
        Me.tbUserName.Location = New System.Drawing.Point(710, 22)
        Me.tbUserName.Name = "tbUserName"
        Me.tbUserName.ReadOnly = True
        Me.tbUserName.Size = New System.Drawing.Size(253, 23)
        Me.tbUserName.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(605, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "User Name :-"
        '
        'tbLoginName
        '
        Me.tbLoginName.BackColor = System.Drawing.Color.White
        Me.tbLoginName.Location = New System.Drawing.Point(446, 22)
        Me.tbLoginName.Name = "tbLoginName"
        Me.tbLoginName.ReadOnly = True
        Me.tbLoginName.Size = New System.Drawing.Size(150, 23)
        Me.tbLoginName.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(336, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 16)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Login Name :-"
        '
        'tbSystemName
        '
        Me.tbSystemName.BackColor = System.Drawing.Color.White
        Me.tbSystemName.Location = New System.Drawing.Point(130, 19)
        Me.tbSystemName.Name = "tbSystemName"
        Me.tbSystemName.ReadOnly = True
        Me.tbSystemName.Size = New System.Drawing.Size(200, 23)
        Me.tbSystemName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "System Name :-"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(483, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Sent On :-"
        '
        'tbSentOn
        '
        Me.tbSentOn.Location = New System.Drawing.Point(576, 20)
        Me.tbSentOn.Name = "tbSentOn"
        Me.tbSentOn.Size = New System.Drawing.Size(217, 23)
        Me.tbSentOn.TabIndex = 8
        '
        'cbReply
        '
        Me.cbReply.Location = New System.Drawing.Point(590, 270)
        Me.cbReply.Name = "cbReply"
        Me.cbReply.Size = New System.Drawing.Size(100, 23)
        Me.cbReply.TabIndex = 9
        Me.cbReply.Text = "Reply"
        Me.cbReply.UseVisualStyleBackColor = True
        '
        'cbReplyAll
        '
        Me.cbReplyAll.Location = New System.Drawing.Point(693, 270)
        Me.cbReplyAll.Name = "cbReplyAll"
        Me.cbReplyAll.Size = New System.Drawing.Size(100, 23)
        Me.cbReplyAll.TabIndex = 10
        Me.cbReplyAll.Text = "Reply All"
        Me.cbReplyAll.UseVisualStyleBackColor = True
        '
        'mainWinForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 617)
        Me.Controls.Add(Me.plMessageInfo)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.cbHistory)
        Me.Controls.Add(Me.cbExit)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cbRemoveAll)
        Me.Controls.Add(Me.cbRemoveSingle)
        Me.Controls.Add(Me.cbSelectAll)
        Me.Controls.Add(Me.cbSelectSingle)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.bntStart)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.plHistory)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "mainWinForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Internal Messaging Ver 05.24-06-15"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdActiveUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdActiveUserV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdSelectedUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSelectedUserV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.plMessageInfo.ResumeLayout(False)
        Me.plMessageInfo.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdPendingAcknowledgement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPendingAcknowledgementV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plHistory.ResumeLayout(False)
        Me.plHistory.PerformLayout()
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdHistoryV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bntStart As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbSelectSingle As System.Windows.Forms.Button
    Friend WithEvents cbSelectAll As System.Windows.Forms.Button
    Friend WithEvents cbRemoveAll As System.Windows.Forms.Button
    Friend WithEvents cbRemoveSingle As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rtbMessage As System.Windows.Forms.RichTextBox
    Friend WithEvents chkbxAcknowledge As System.Windows.Forms.CheckBox
    Friend WithEvents cbSend As System.Windows.Forms.Button
    Friend WithEvents grdActiveUser As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdActiveUserV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents grdSelectedUser As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSelectedUserV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents plMessageInfo As System.Windows.Forms.GroupBox
    Friend WithEvents cbOK As System.Windows.Forms.Button
    Friend WithEvents rtbReceivedMessage As System.Windows.Forms.RichTextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents tbSystem As System.Windows.Forms.TextBox
    Friend WithEvents tbFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbAcknowledgement As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cbRefresh As System.Windows.Forms.Button
    Friend WithEvents grdPendingAcknowledgement As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPendingAcknowledgementV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbHistory As System.Windows.Forms.Button
    Friend WithEvents plHistory As System.Windows.Forms.GroupBox
    Friend WithEvents cbExitHistory As System.Windows.Forms.Button
    Friend WithEvents grdHistory As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdHistoryV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbRefreshHistory As System.Windows.Forms.Button
    Friend WithEvents dpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbReceivedHistory As System.Windows.Forms.RadioButton
    Friend WithEvents rbSentHistory As System.Windows.Forms.RadioButton
    Friend WithEvents cbExport2Excel As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents tbLoginName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbSystemName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbReplyAll As System.Windows.Forms.Button
    Friend WithEvents cbReply As System.Windows.Forms.Button
    Friend WithEvents tbSentOn As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
