<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaraCPRODSummaryFDDinBigScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSaraCPRODSummaryFDDinBigScreen))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.plProductionDetails = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblDataInfo = New System.Windows.Forms.Label
        Me.grdConveyorProduction = New DevExpress.XtraGrid.GridControl
        Me.grdConveyorProductionV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdWeeklyPlan = New DevExpress.XtraGrid.GridControl
        Me.grdWeeklyPlanV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbPause = New System.Windows.Forms.Button
        Me.lblSeconds = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.plVideos = New System.Windows.Forms.GroupBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer
        Me.cbxVideo = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblTrainingTimer = New System.Windows.Forms.Label
        Me.plDtlModeTimer = New System.Windows.Forms.GroupBox
        Me.chkbxAutoLoad = New System.Windows.Forms.CheckBox
        Me.lblDetailModeTimer = New System.Windows.Forms.Label
        Me.cbDetailMode = New System.Windows.Forms.Button
        Me.cbxOption = New System.Windows.Forms.ComboBox
        Me.plCapacity = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.tbBalanceHours = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblElapsedHour = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblTime = New System.Windows.Forms.Label
        Me.tbStatusHour = New System.Windows.Forms.TextBox
        Me.tbDifferenceHour = New System.Windows.Forms.TextBox
        Me.tbDifferenceDay = New System.Windows.Forms.TextBox
        Me.tbAchievedHour = New System.Windows.Forms.TextBox
        Me.tbAchievedDay = New System.Windows.Forms.TextBox
        Me.tbPlannedHour = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbPlannedDay = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblCapacity = New System.Windows.Forms.Label
        Me.tbToAchieveHour = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbSkip = New System.Windows.Forms.Button
        Me.plProductionDetails.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdConveyorProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdConveyorProductionV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWeeklyPlan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWeeklyPlanV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plVideos.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.plDtlModeTimer.SuspendLayout()
        Me.plCapacity.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'plProductionDetails
        '
        Me.plProductionDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plProductionDetails.BackColor = System.Drawing.Color.Lavender
        Me.plProductionDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plProductionDetails.Controls.Add(Me.Panel4)
        Me.plProductionDetails.Controls.Add(Me.grdConveyorProduction)
        Me.plProductionDetails.Controls.Add(Me.grdWeeklyPlan)
        Me.plProductionDetails.Location = New System.Drawing.Point(5, 5)
        Me.plProductionDetails.Name = "plProductionDetails"
        Me.plProductionDetails.Size = New System.Drawing.Size(1760, 591)
        Me.plProductionDetails.TabIndex = 18
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.lblDataInfo)
        Me.Panel4.Location = New System.Drawing.Point(2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1754, 62)
        Me.Panel4.TabIndex = 147
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.SolarERPForSGM.My.Resources.Resources.AH_Group_Logo
        Me.PictureBox2.Location = New System.Drawing.Point(7, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(77, 43)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 142
        Me.PictureBox2.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(93, 40)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(111, 16)
        Me.Label15.TabIndex = 144
        Me.Label15.Text = "Ranipet - INDIA"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Green
        Me.Label16.Location = New System.Drawing.Point(90, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(508, 32)
        Me.Label16.TabIndex = 143
        Me.Label16.Text = "SARA Leather Industries - C Unit"
        '
        'lblDataInfo
        '
        Me.lblDataInfo.AutoSize = True
        Me.lblDataInfo.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDataInfo.ForeColor = System.Drawing.Color.Purple
        Me.lblDataInfo.Location = New System.Drawing.Point(645, 19)
        Me.lblDataInfo.Name = "lblDataInfo"
        Me.lblDataInfo.Size = New System.Drawing.Size(115, 32)
        Me.lblDataInfo.TabIndex = 116
        Me.lblDataInfo.Text = "Label3"
        '
        'grdConveyorProduction
        '
        Me.grdConveyorProduction.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdConveyorProduction.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdConveyorProduction.Location = New System.Drawing.Point(5, 82)
        Me.grdConveyorProduction.MainView = Me.grdConveyorProductionV1
        Me.grdConveyorProduction.Name = "grdConveyorProduction"
        Me.grdConveyorProduction.Size = New System.Drawing.Size(1747, 493)
        Me.grdConveyorProduction.TabIndex = 115
        Me.grdConveyorProduction.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdConveyorProductionV1})
        '
        'grdConveyorProductionV1
        '
        Me.grdConveyorProductionV1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 20.25!)
        Me.grdConveyorProductionV1.Appearance.Row.Options.UseFont = True
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition1.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdConveyorProductionV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdConveyorProductionV1.GridControl = Me.grdConveyorProduction
        Me.grdConveyorProductionV1.Name = "grdConveyorProductionV1"
        Me.grdConveyorProductionV1.OptionsView.ShowAutoFilterRow = True
        Me.grdConveyorProductionV1.OptionsView.ShowFooter = True
        '
        'grdWeeklyPlan
        '
        Me.grdWeeklyPlan.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdWeeklyPlan.Location = New System.Drawing.Point(5, 82)
        Me.grdWeeklyPlan.MainView = Me.grdWeeklyPlanV1
        Me.grdWeeklyPlan.Name = "grdWeeklyPlan"
        Me.grdWeeklyPlan.Size = New System.Drawing.Size(1747, 493)
        Me.grdWeeklyPlan.TabIndex = 117
        Me.grdWeeklyPlan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdWeeklyPlanV1})
        '
        'grdWeeklyPlanV1
        '
        Me.grdWeeklyPlanV1.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.grdWeeklyPlanV1.Appearance.Row.Options.UseFont = True
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.ApplyToRow = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdWeeklyPlanV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdWeeklyPlanV1.GridControl = Me.grdWeeklyPlan
        Me.grdWeeklyPlanV1.Name = "grdWeeklyPlanV1"
        Me.grdWeeklyPlanV1.OptionsView.ShowAutoFilterRow = True
        Me.grdWeeklyPlanV1.OptionsView.ShowFooter = True
        '
        'cbPause
        '
        Me.cbPause.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPause.Location = New System.Drawing.Point(861, 31)
        Me.cbPause.Margin = New System.Windows.Forms.Padding(4)
        Me.cbPause.Name = "cbPause"
        Me.cbPause.Size = New System.Drawing.Size(108, 74)
        Me.cbPause.TabIndex = 20
        Me.cbPause.Text = "Pause"
        Me.cbPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPause.UseVisualStyleBackColor = True
        '
        'lblSeconds
        '
        Me.lblSeconds.AutoSize = True
        Me.lblSeconds.Font = New System.Drawing.Font("Digital-7", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeconds.Location = New System.Drawing.Point(785, 70)
        Me.lblSeconds.Name = "lblSeconds"
        Me.lblSeconds.Size = New System.Drawing.Size(80, 28)
        Me.lblSeconds.TabIndex = 21
        Me.lblSeconds.Text = "Label1"
        Me.lblSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Digital-7", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 28)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Label1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Digital-7", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 28)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Label2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(7, 23)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 74)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Play Video"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'plVideos
        '
        Me.plVideos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plVideos.Controls.Add(Me.Panel2)
        Me.plVideos.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.plVideos.Location = New System.Drawing.Point(5, 5)
        Me.plVideos.Name = "plVideos"
        Me.plVideos.Size = New System.Drawing.Size(1760, 591)
        Me.plVideos.TabIndex = 25
        Me.plVideos.TabStop = False
        Me.plVideos.Text = "Training Videos"
        Me.plVideos.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.Controls.Add(Me.PictureBox3)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Location = New System.Drawing.Point(6, 19)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1754, 62)
        Me.Panel2.TabIndex = 145
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.SolarERPForSGM.My.Resources.Resources.AH_Group_Logo
        Me.PictureBox3.Location = New System.Drawing.Point(7, 8)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(77, 43)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 142
        Me.PictureBox3.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(93, 40)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(111, 16)
        Me.Label17.TabIndex = 144
        Me.Label17.Text = "Ranipet - INDIA"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Green
        Me.Label18.Location = New System.Drawing.Point(90, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(508, 32)
        Me.Label18.TabIndex = 143
        Me.Label18.Text = "SARA Leather Industries - C Unit"
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(3, 87)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(1754, 504)
        Me.AxWindowsMediaPlayer1.TabIndex = 109
        '
        'cbxVideo
        '
        Me.cbxVideo.FormattingEnabled = True
        Me.cbxVideo.Items.AddRange(New Object() {"Marking 1", "Re Cutting", "Skiving 1", "Skiving 2", "Stitching Deco 1"})
        Me.cbxVideo.Location = New System.Drawing.Point(142, 22)
        Me.cbxVideo.Name = "cbxVideo"
        Me.cbxVideo.Size = New System.Drawing.Size(121, 24)
        Me.cbxVideo.TabIndex = 26
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.lblTrainingTimer)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.cbxVideo)
        Me.GroupBox1.Location = New System.Drawing.Point(138, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(291, 100)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Training Videos :-"
        '
        'lblTrainingTimer
        '
        Me.lblTrainingTimer.AutoSize = True
        Me.lblTrainingTimer.Font = New System.Drawing.Font("Digital-7", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrainingTimer.Location = New System.Drawing.Point(142, 61)
        Me.lblTrainingTimer.Name = "lblTrainingTimer"
        Me.lblTrainingTimer.Size = New System.Drawing.Size(80, 28)
        Me.lblTrainingTimer.TabIndex = 28
        Me.lblTrainingTimer.Text = "Label1"
        Me.lblTrainingTimer.Visible = False
        '
        'plDtlModeTimer
        '
        Me.plDtlModeTimer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plDtlModeTimer.Controls.Add(Me.chkbxAutoLoad)
        Me.plDtlModeTimer.Controls.Add(Me.lblDetailModeTimer)
        Me.plDtlModeTimer.Controls.Add(Me.cbDetailMode)
        Me.plDtlModeTimer.Controls.Add(Me.cbxOption)
        Me.plDtlModeTimer.Location = New System.Drawing.Point(435, 16)
        Me.plDtlModeTimer.Name = "plDtlModeTimer"
        Me.plDtlModeTimer.Size = New System.Drawing.Size(344, 100)
        Me.plDtlModeTimer.TabIndex = 28
        Me.plDtlModeTimer.TabStop = False
        Me.plDtlModeTimer.Text = "Data in Detail Mode"
        '
        'chkbxAutoLoad
        '
        Me.chkbxAutoLoad.AutoSize = True
        Me.chkbxAutoLoad.Location = New System.Drawing.Point(7, 23)
        Me.chkbxAutoLoad.Name = "chkbxAutoLoad"
        Me.chkbxAutoLoad.Size = New System.Drawing.Size(94, 20)
        Me.chkbxAutoLoad.TabIndex = 29
        Me.chkbxAutoLoad.Text = "Auto Load"
        Me.chkbxAutoLoad.UseVisualStyleBackColor = True
        '
        'lblDetailModeTimer
        '
        Me.lblDetailModeTimer.AutoSize = True
        Me.lblDetailModeTimer.Font = New System.Drawing.Font("Digital-7", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailModeTimer.Location = New System.Drawing.Point(142, 61)
        Me.lblDetailModeTimer.Name = "lblDetailModeTimer"
        Me.lblDetailModeTimer.Size = New System.Drawing.Size(80, 28)
        Me.lblDetailModeTimer.TabIndex = 28
        Me.lblDetailModeTimer.Text = "Label1"
        Me.lblDetailModeTimer.Visible = False
        '
        'cbDetailMode
        '
        Me.cbDetailMode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDetailMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbDetailMode.Location = New System.Drawing.Point(7, 47)
        Me.cbDetailMode.Margin = New System.Windows.Forms.Padding(4)
        Me.cbDetailMode.Name = "cbDetailMode"
        Me.cbDetailMode.Size = New System.Drawing.Size(128, 50)
        Me.cbDetailMode.TabIndex = 24
        Me.cbDetailMode.Text = "Detail Mode"
        Me.cbDetailMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbDetailMode.UseVisualStyleBackColor = True
        '
        'cbxOption
        '
        Me.cbxOption.FormattingEnabled = True
        Me.cbxOption.Items.AddRange(New Object() {"Video - 0 1", "Video - 0 2", "Video - 0 3", "Video - 0 4", "Video - 0 5", "Video - 0 6", "Video - 0 7", "Video - 0 8", "Video - 0 9", "Video - 10 "})
        Me.cbxOption.Location = New System.Drawing.Point(142, 22)
        Me.cbxOption.Name = "cbxOption"
        Me.cbxOption.Size = New System.Drawing.Size(196, 24)
        Me.cbxOption.TabIndex = 26
        '
        'plCapacity
        '
        Me.plCapacity.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plCapacity.BackColor = System.Drawing.Color.Lavender
        Me.plCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plCapacity.Controls.Add(Me.Panel3)
        Me.plCapacity.Controls.Add(Me.tbBalanceHours)
        Me.plCapacity.Controls.Add(Me.Label11)
        Me.plCapacity.Controls.Add(Me.lblElapsedHour)
        Me.plCapacity.Controls.Add(Me.Label10)
        Me.plCapacity.Controls.Add(Me.lblTime)
        Me.plCapacity.Controls.Add(Me.tbStatusHour)
        Me.plCapacity.Controls.Add(Me.tbDifferenceHour)
        Me.plCapacity.Controls.Add(Me.tbDifferenceDay)
        Me.plCapacity.Controls.Add(Me.tbAchievedHour)
        Me.plCapacity.Controls.Add(Me.tbAchievedDay)
        Me.plCapacity.Controls.Add(Me.tbPlannedHour)
        Me.plCapacity.Controls.Add(Me.Label9)
        Me.plCapacity.Controls.Add(Me.Label8)
        Me.plCapacity.Controls.Add(Me.Label7)
        Me.plCapacity.Controls.Add(Me.Label3)
        Me.plCapacity.Controls.Add(Me.tbPlannedDay)
        Me.plCapacity.Controls.Add(Me.Label6)
        Me.plCapacity.Controls.Add(Me.Label5)
        Me.plCapacity.Controls.Add(Me.Label4)
        Me.plCapacity.Controls.Add(Me.lblCapacity)
        Me.plCapacity.Controls.Add(Me.tbToAchieveHour)
        Me.plCapacity.Location = New System.Drawing.Point(5, 5)
        Me.plCapacity.Name = "plCapacity"
        Me.plCapacity.Size = New System.Drawing.Size(1760, 591)
        Me.plCapacity.TabIndex = 29
        Me.plCapacity.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel3.Controls.Add(Me.PictureBox4)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Location = New System.Drawing.Point(9, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1754, 62)
        Me.Panel3.TabIndex = 146
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.SolarERPForSGM.My.Resources.Resources.AH_Group_Logo
        Me.PictureBox4.Location = New System.Drawing.Point(7, 8)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(77, 43)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 142
        Me.PictureBox4.TabStop = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(93, 40)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(111, 16)
        Me.Label19.TabIndex = 144
        Me.Label19.Text = "Ranipet - INDIA"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Green
        Me.Label20.Location = New System.Drawing.Point(90, 8)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(508, 32)
        Me.Label20.TabIndex = 143
        Me.Label20.Text = "SARA Leather Industries - C Unit"
        '
        'tbBalanceHours
        '
        Me.tbBalanceHours.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalanceHours.Location = New System.Drawing.Point(267, 458)
        Me.tbBalanceHours.Name = "tbBalanceHours"
        Me.tbBalanceHours.Size = New System.Drawing.Size(85, 85)
        Me.tbBalanceHours.TabIndex = 138
        Me.tbBalanceHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Green
        Me.Label11.Location = New System.Drawing.Point(12, 458)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(250, 85)
        Me.Label11.TabIndex = 137
        Me.Label11.Text = "Balance Available Hour(s) :-"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblElapsedHour
        '
        Me.lblElapsedHour.AutoSize = True
        Me.lblElapsedHour.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElapsedHour.ForeColor = System.Drawing.Color.Black
        Me.lblElapsedHour.Location = New System.Drawing.Point(1298, 141)
        Me.lblElapsedHour.Name = "lblElapsedHour"
        Me.lblElapsedHour.Size = New System.Drawing.Size(34, 32)
        Me.lblElapsedHour.TabIndex = 136
        Me.lblElapsedHour.Text = "8"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(1000, 141)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(294, 32)
        Me.Label10.TabIndex = 135
        Me.Label10.Text = "Elapsed Hour(s) :-"
        '
        'lblTime
        '
        Me.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTime.Font = New System.Drawing.Font("Digital-7", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.Purple
        Me.lblTime.Location = New System.Drawing.Point(12, 232)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(250, 46)
        Me.lblTime.TabIndex = 134
        Me.lblTime.Text = "Label10"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbStatusHour
        '
        Me.tbStatusHour.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbStatusHour.Location = New System.Drawing.Point(267, 367)
        Me.tbStatusHour.Name = "tbStatusHour"
        Me.tbStatusHour.Size = New System.Drawing.Size(707, 85)
        Me.tbStatusHour.TabIndex = 131
        Me.tbStatusHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbDifferenceHour
        '
        Me.tbDifferenceHour.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDifferenceHour.Location = New System.Drawing.Point(526, 276)
        Me.tbDifferenceHour.Name = "tbDifferenceHour"
        Me.tbDifferenceHour.Size = New System.Drawing.Size(250, 85)
        Me.tbDifferenceHour.TabIndex = 129
        Me.tbDifferenceHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbDifferenceDay
        '
        Me.tbDifferenceDay.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDifferenceDay.Location = New System.Drawing.Point(267, 276)
        Me.tbDifferenceDay.Name = "tbDifferenceDay"
        Me.tbDifferenceDay.Size = New System.Drawing.Size(250, 85)
        Me.tbDifferenceDay.TabIndex = 128
        Me.tbDifferenceDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbAchievedHour
        '
        Me.tbAchievedHour.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAchievedHour.Location = New System.Drawing.Point(526, 185)
        Me.tbAchievedHour.Name = "tbAchievedHour"
        Me.tbAchievedHour.Size = New System.Drawing.Size(250, 85)
        Me.tbAchievedHour.TabIndex = 127
        Me.tbAchievedHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbAchievedDay
        '
        Me.tbAchievedDay.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAchievedDay.Location = New System.Drawing.Point(267, 185)
        Me.tbAchievedDay.Name = "tbAchievedDay"
        Me.tbAchievedDay.Size = New System.Drawing.Size(250, 85)
        Me.tbAchievedDay.TabIndex = 126
        Me.tbAchievedDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlannedHour
        '
        Me.tbPlannedHour.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlannedHour.Location = New System.Drawing.Point(526, 94)
        Me.tbPlannedHour.Name = "tbPlannedHour"
        Me.tbPlannedHour.Size = New System.Drawing.Size(250, 85)
        Me.tbPlannedHour.TabIndex = 125
        Me.tbPlannedHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Purple
        Me.Label9.Location = New System.Drawing.Point(358, 458)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(360, 85)
        Me.Label9.TabIndex = 124
        Me.Label9.Text = "Bal To Achieve / Hour"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Purple
        Me.Label8.Location = New System.Drawing.Point(12, 367)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(250, 85)
        Me.Label8.TabIndex = 123
        Me.Label8.Text = "Status"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Purple
        Me.Label7.Location = New System.Drawing.Point(599, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 32)
        Me.Label7.TabIndex = 122
        Me.Label7.Text = "HOUR"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Purple
        Me.Label3.Location = New System.Drawing.Point(353, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 32)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "DAY"
        '
        'tbPlannedDay
        '
        Me.tbPlannedDay.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlannedDay.Location = New System.Drawing.Point(267, 94)
        Me.tbPlannedDay.Name = "tbPlannedDay"
        Me.tbPlannedDay.Size = New System.Drawing.Size(250, 85)
        Me.tbPlannedDay.TabIndex = 120
        Me.tbPlannedDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Purple
        Me.Label6.Location = New System.Drawing.Point(12, 276)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(250, 85)
        Me.Label6.TabIndex = 119
        Me.Label6.Text = "Difference"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Purple
        Me.Label5.Location = New System.Drawing.Point(12, 185)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(250, 46)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "Achieved Qty"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Purple
        Me.Label4.Location = New System.Drawing.Point(12, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(250, 85)
        Me.Label4.TabIndex = 117
        Me.Label4.Text = "Planned Qty"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCapacity
        '
        Me.lblCapacity.AutoSize = True
        Me.lblCapacity.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCapacity.ForeColor = System.Drawing.Color.Purple
        Me.lblCapacity.Location = New System.Drawing.Point(1000, 82)
        Me.lblCapacity.Name = "lblCapacity"
        Me.lblCapacity.Size = New System.Drawing.Size(145, 32)
        Me.lblCapacity.TabIndex = 116
        Me.lblCapacity.Text = "Capacity"
        '
        'tbToAchieveHour
        '
        Me.tbToAchieveHour.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbToAchieveHour.Location = New System.Drawing.Point(724, 458)
        Me.tbToAchieveHour.Name = "tbToAchieveHour"
        Me.tbToAchieveHour.Size = New System.Drawing.Size(250, 85)
        Me.tbToAchieveHour.TabIndex = 133
        Me.tbToAchieveHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1012, 110)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(175, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Summary Only v4.07-Nov-19"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Lavender
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbSkip)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cbPause)
        Me.Panel1.Controls.Add(Me.lblSeconds)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.plDtlModeTimer)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(11, 602)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1754, 128)
        Me.Panel1.TabIndex = 31
        '
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(1218, 31)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(108, 74)
        Me.cbExit.TabIndex = 32
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbSkip
        '
        Me.cbSkip.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSkip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbSkip.Location = New System.Drawing.Point(6, 77)
        Me.cbSkip.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSkip.Name = "cbSkip"
        Me.cbSkip.Size = New System.Drawing.Size(128, 39)
        Me.cbSkip.TabIndex = 31
        Me.cbSkip.Text = "Skip this Window"
        Me.cbSkip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbSkip.UseVisualStyleBackColor = True
        '
        'frmSaraCPRODSummaryFDDinBigScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1435, 742)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.plVideos)
        Me.Controls.Add(Me.plCapacity)
        Me.Controls.Add(Me.plProductionDetails)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSaraCPRODSummaryFDDinBigScreen"
        Me.Text = "SUMMARY DISPLAY"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.plProductionDetails.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdConveyorProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdConveyorProductionV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWeeklyPlan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWeeklyPlanV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plVideos.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.plDtlModeTimer.ResumeLayout(False)
        Me.plDtlModeTimer.PerformLayout()
        Me.plCapacity.ResumeLayout(False)
        Me.plCapacity.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents plProductionDetails As System.Windows.Forms.Panel
    Friend WithEvents cbPause As System.Windows.Forms.Button
    Friend WithEvents lblSeconds As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdConveyorProduction As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdConveyorProductionV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents plVideos As System.Windows.Forms.GroupBox
    Friend WithEvents cbxVideo As System.Windows.Forms.ComboBox
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTrainingTimer As System.Windows.Forms.Label
    Friend WithEvents plDtlModeTimer As System.Windows.Forms.GroupBox
    Friend WithEvents lblDetailModeTimer As System.Windows.Forms.Label
    Friend WithEvents cbDetailMode As System.Windows.Forms.Button
    Friend WithEvents cbxOption As System.Windows.Forms.ComboBox
    Friend WithEvents chkbxAutoLoad As System.Windows.Forms.CheckBox
    Friend WithEvents lblDataInfo As System.Windows.Forms.Label
    Friend WithEvents plCapacity As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCapacity As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbPlannedDay As System.Windows.Forms.TextBox
    Friend WithEvents tbToAchieveHour As System.Windows.Forms.TextBox
    Friend WithEvents tbStatusHour As System.Windows.Forms.TextBox
    Friend WithEvents tbDifferenceHour As System.Windows.Forms.TextBox
    Friend WithEvents tbDifferenceDay As System.Windows.Forms.TextBox
    Friend WithEvents tbAchievedHour As System.Windows.Forms.TextBox
    Friend WithEvents tbAchievedDay As System.Windows.Forms.TextBox
    Friend WithEvents tbPlannedHour As System.Windows.Forms.TextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents lblElapsedHour As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tbBalanceHours As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdWeeklyPlan As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdWeeklyPlanV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbSkip As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
