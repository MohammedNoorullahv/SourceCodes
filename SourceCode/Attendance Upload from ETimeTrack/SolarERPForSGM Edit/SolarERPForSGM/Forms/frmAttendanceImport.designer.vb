<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendanceImport
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition3 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttendanceImport))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pgbdepartment = New System.Windows.Forms.ProgressBar
        Me.tbUnitName = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.pgb1 = New System.Windows.Forms.ProgressBar
        Me.dpImportedTill = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cbUpdateStatusDept = New System.Windows.Forms.Button
        Me.chkbxSingleDay = New System.Windows.Forms.CheckBox
        Me.cbUpdateStatus = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbSummaryYear = New System.Windows.Forms.TextBox
        Me.tbSummaryMonth = New System.Windows.Forms.TextBox
        Me.dpSumaryDate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdAttSummeryDtls = New DevExpress.XtraGrid.GridControl
        Me.grdAttSummeryDtlsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdAttendaceInfoineTimeTrack = New DevExpress.XtraGrid.GridControl
        Me.grdAttendaceInfoineTimeTrackV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbxUnitName = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbImport = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdAttendaceInfoinAP = New DevExpress.XtraGrid.GridControl
        Me.grdAttendaceInfoinAPv1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdAttSummeryDtls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAttSummeryDtlsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAttendaceInfoineTimeTrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAttendaceInfoineTimeTrackV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAttendaceInfoinAP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAttendaceInfoinAPv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.pgbdepartment)
        Me.Panel1.Controls.Add(Me.tbUnitName)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.pgb1)
        Me.Panel1.Controls.Add(Me.dpImportedTill)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.dpFrom)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.cbxUnitName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dpTo)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cbImport)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(967, 600)
        Me.Panel1.TabIndex = 0
        '
        'pgbdepartment
        '
        Me.pgbdepartment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbdepartment.Location = New System.Drawing.Point(468, 550)
        Me.pgbdepartment.Name = "pgbdepartment"
        Me.pgbdepartment.Size = New System.Drawing.Size(360, 23)
        Me.pgbdepartment.TabIndex = 25
        '
        'tbUnitName
        '
        Me.tbUnitName.BackColor = System.Drawing.Color.White
        Me.tbUnitName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbUnitName.ForeColor = System.Drawing.Color.Purple
        Me.tbUnitName.Location = New System.Drawing.Point(144, 212)
        Me.tbUnitName.Name = "tbUnitName"
        Me.tbUnitName.ReadOnly = True
        Me.tbUnitName.Size = New System.Drawing.Size(283, 20)
        Me.tbUnitName.TabIndex = 24
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(507, 531)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(186, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "v3.4 Attendance Import - 24-Jan-2022"
        '
        'pgb1
        '
        Me.pgb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgb1.Location = New System.Drawing.Point(468, 574)
        Me.pgb1.Name = "pgb1"
        Me.pgb1.Size = New System.Drawing.Size(360, 23)
        Me.pgb1.TabIndex = 21
        '
        'dpImportedTill
        '
        Me.dpImportedTill.Location = New System.Drawing.Point(140, 241)
        Me.dpImportedTill.MinDate = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.dpImportedTill.Name = "dpImportedTill"
        Me.dpImportedTill.Size = New System.Drawing.Size(121, 20)
        Me.dpImportedTill.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(7, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 20)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Data Imported till"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbUpdateStatusDept)
        Me.GroupBox3.Controls.Add(Me.chkbxSingleDay)
        Me.GroupBox3.Controls.Add(Me.cbUpdateStatus)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.tbSummaryYear)
        Me.GroupBox3.Controls.Add(Me.tbSummaryMonth)
        Me.GroupBox3.Controls.Add(Me.dpSumaryDate)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Location = New System.Drawing.Point(446, 222)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(362, 96)
        Me.GroupBox3.TabIndex = 16
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Attendance Summary Generation :-"
        '
        'cbUpdateStatusDept
        '
        Me.cbUpdateStatusDept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUpdateStatusDept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbUpdateStatusDept.Location = New System.Drawing.Point(161, 44)
        Me.cbUpdateStatusDept.Name = "cbUpdateStatusDept"
        Me.cbUpdateStatusDept.Size = New System.Drawing.Size(157, 38)
        Me.cbUpdateStatusDept.TabIndex = 25
        Me.cbUpdateStatusDept.Text = "Update Status Dept"
        Me.cbUpdateStatusDept.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbUpdateStatusDept.UseVisualStyleBackColor = True
        '
        'chkbxSingleDay
        '
        Me.chkbxSingleDay.AutoSize = True
        Me.chkbxSingleDay.Checked = True
        Me.chkbxSingleDay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxSingleDay.Location = New System.Drawing.Point(211, 16)
        Me.chkbxSingleDay.Name = "chkbxSingleDay"
        Me.chkbxSingleDay.Size = New System.Drawing.Size(142, 17)
        Me.chkbxSingleDay.TabIndex = 23
        Me.chkbxSingleDay.Text = "Import Single Day Status"
        Me.chkbxSingleDay.UseVisualStyleBackColor = True
        '
        'cbUpdateStatus
        '
        Me.cbUpdateStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUpdateStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbUpdateStatus.Location = New System.Drawing.Point(161, 44)
        Me.cbUpdateStatus.Name = "cbUpdateStatus"
        Me.cbUpdateStatus.Size = New System.Drawing.Size(157, 38)
        Me.cbUpdateStatus.TabIndex = 22
        Me.cbUpdateStatus.Text = "Update Status"
        Me.cbUpdateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbUpdateStatus.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(6, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 20)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Year"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(6, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 20)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Month"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbSummaryYear
        '
        Me.tbSummaryYear.Location = New System.Drawing.Point(84, 68)
        Me.tbSummaryYear.Name = "tbSummaryYear"
        Me.tbSummaryYear.Size = New System.Drawing.Size(50, 20)
        Me.tbSummaryYear.TabIndex = 19
        '
        'tbSummaryMonth
        '
        Me.tbSummaryMonth.Location = New System.Drawing.Point(84, 42)
        Me.tbSummaryMonth.Name = "tbSummaryMonth"
        Me.tbSummaryMonth.Size = New System.Drawing.Size(50, 20)
        Me.tbSummaryMonth.TabIndex = 18
        '
        'dpSumaryDate
        '
        Me.dpSumaryDate.Location = New System.Drawing.Point(84, 16)
        Me.dpSumaryDate.MinDate = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.dpSumaryDate.Name = "dpSumaryDate"
        Me.dpSumaryDate.Size = New System.Drawing.Size(121, 20)
        Me.dpSumaryDate.TabIndex = 17
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dpFrom
        '
        Me.dpFrom.Location = New System.Drawing.Point(141, 264)
        Me.dpFrom.MinDate = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(121, 20)
        Me.dpFrom.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(8, 261)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 20)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Import From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdAttSummeryDtls)
        Me.GroupBox2.Controls.Add(Me.grdAttendaceInfoineTimeTrack)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 317)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(961, 211)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datas to be Imported from eTimeTrackv1"
        '
        'grdAttSummeryDtls
        '
        Me.grdAttSummeryDtls.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.grdAttSummeryDtls.Location = New System.Drawing.Point(8, 19)
        Me.grdAttSummeryDtls.MainView = Me.grdAttSummeryDtlsV1
        Me.grdAttSummeryDtls.Name = "grdAttSummeryDtls"
        Me.grdAttSummeryDtls.Size = New System.Drawing.Size(947, 186)
        Me.grdAttSummeryDtls.TabIndex = 25
        Me.grdAttSummeryDtls.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAttSummeryDtlsV1})
        '
        'grdAttSummeryDtlsV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdAttSummeryDtlsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdAttSummeryDtlsV1.GridControl = Me.grdAttSummeryDtls
        Me.grdAttSummeryDtlsV1.Name = "grdAttSummeryDtlsV1"
        Me.grdAttSummeryDtlsV1.OptionsView.ColumnAutoWidth = False
        Me.grdAttSummeryDtlsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdAttSummeryDtlsV1.OptionsView.ShowFooter = True
        Me.grdAttSummeryDtlsV1.OptionsView.ShowGroupPanel = False
        '
        'grdAttendaceInfoineTimeTrack
        '
        Me.grdAttendaceInfoineTimeTrack.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.grdAttendaceInfoineTimeTrack.Location = New System.Drawing.Point(8, 19)
        Me.grdAttendaceInfoineTimeTrack.MainView = Me.grdAttendaceInfoineTimeTrackV1
        Me.grdAttendaceInfoineTimeTrack.Name = "grdAttendaceInfoineTimeTrack"
        Me.grdAttendaceInfoineTimeTrack.Size = New System.Drawing.Size(947, 186)
        Me.grdAttendaceInfoineTimeTrack.TabIndex = 3
        Me.grdAttendaceInfoineTimeTrack.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAttendaceInfoineTimeTrackV1})
        '
        'grdAttendaceInfoineTimeTrackV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdAttendaceInfoineTimeTrackV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdAttendaceInfoineTimeTrackV1.GridControl = Me.grdAttendaceInfoineTimeTrack
        Me.grdAttendaceInfoineTimeTrackV1.Name = "grdAttendaceInfoineTimeTrackV1"
        Me.grdAttendaceInfoineTimeTrackV1.OptionsView.ColumnAutoWidth = False
        Me.grdAttendaceInfoineTimeTrackV1.OptionsView.ShowAutoFilterRow = True
        Me.grdAttendaceInfoineTimeTrackV1.OptionsView.ShowFooter = True
        Me.grdAttendaceInfoineTimeTrackV1.OptionsView.ShowGroupPanel = False
        '
        'cbxUnitName
        '
        Me.cbxUnitName.FormattingEnabled = True
        Me.cbxUnitName.Location = New System.Drawing.Point(144, 214)
        Me.cbxUnitName.Name = "cbxUnitName"
        Me.cbxUnitName.Size = New System.Drawing.Size(121, 21)
        Me.cbxUnitName.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(7, 214)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 21)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Unit Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dpTo
        '
        Me.dpTo.Location = New System.Drawing.Point(141, 287)
        Me.dpTo.MinDate = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.dpTo.Name = "dpTo"
        Me.dpTo.Size = New System.Drawing.Size(121, 20)
        Me.dpTo.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(8, 284)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Import To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbImport
        '
        Me.cbImport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbImport.Location = New System.Drawing.Point(271, 238)
        Me.cbImport.Name = "cbImport"
        Me.cbImport.Size = New System.Drawing.Size(156, 66)
        Me.cbImport.TabIndex = 7
        Me.cbImport.Text = "Import to AP"
        Me.cbImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbImport.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grdAttendaceInfoinAP)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(961, 211)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datas Available in AP Wiz"
        '
        'grdAttendaceInfoinAP
        '
        Me.grdAttendaceInfoinAP.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.grdAttendaceInfoinAP.Location = New System.Drawing.Point(8, 19)
        Me.grdAttendaceInfoinAP.MainView = Me.grdAttendaceInfoinAPv1
        Me.grdAttendaceInfoinAP.Name = "grdAttendaceInfoinAP"
        Me.grdAttendaceInfoinAP.Size = New System.Drawing.Size(947, 186)
        Me.grdAttendaceInfoinAP.TabIndex = 3
        Me.grdAttendaceInfoinAP.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdAttendaceInfoinAPv1})
        '
        'grdAttendaceInfoinAPv1
        '
        StyleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition3.Appearance.Options.UseForeColor = True
        StyleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition3.Value1 = CType(0, Short)
        Me.grdAttendaceInfoinAPv1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition3})
        Me.grdAttendaceInfoinAPv1.GridControl = Me.grdAttendaceInfoinAP
        Me.grdAttendaceInfoinAPv1.Name = "grdAttendaceInfoinAPv1"
        Me.grdAttendaceInfoinAPv1.OptionsView.ColumnAutoWidth = False
        Me.grdAttendaceInfoinAPv1.OptionsView.ShowAutoFilterRow = True
        Me.grdAttendaceInfoinAPv1.OptionsView.ShowFooter = True
        Me.grdAttendaceInfoinAPv1.OptionsView.ShowGroupPanel = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(306, 523)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 74)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Upper Short Dispatch Report"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Export2Excel
        '
        Me.Export2Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Export2Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export2Excel.Image = CType(resources.GetObject("Export2Excel.Image"), System.Drawing.Image)
        Me.Export2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Export2Excel.Location = New System.Drawing.Point(144, 523)
        Me.Export2Excel.Name = "Export2Excel"
        Me.Export2Excel.Size = New System.Drawing.Size(156, 74)
        Me.Export2Excel.TabIndex = 4
        Me.Export2Excel.Text = "Expot to Excel"
        Me.Export2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export2Excel.UseVisualStyleBackColor = True
        Me.Export2Excel.Visible = False
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(834, 523)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(117, 74)
        Me.cbExit.TabIndex = 2
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbReferesh
        '
        Me.cbReferesh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbReferesh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReferesh.Image = CType(resources.GetObject("cbReferesh.Image"), System.Drawing.Image)
        Me.cbReferesh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbReferesh.Location = New System.Drawing.Point(10, 523)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'frmAttendanceImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 623)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmAttendanceImport"
        Me.Text = "Upper & Full Shoe Outstanding"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdAttSummeryDtls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAttSummeryDtlsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAttendaceInfoineTimeTrack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAttendaceInfoineTimeTrackV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdAttendaceInfoinAP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAttendaceInfoinAPv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents grdAttendaceInfoinAP As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAttendaceInfoinAPv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdAttendaceInfoineTimeTrack As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAttendaceInfoineTimeTrackV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbxUnitName As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbImport As System.Windows.Forms.Button
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbUpdateStatus As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbSummaryYear As System.Windows.Forms.TextBox
    Friend WithEvents tbSummaryMonth As System.Windows.Forms.TextBox
    Friend WithEvents dpSumaryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dpImportedTill As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkbxSingleDay As System.Windows.Forms.CheckBox
    Friend WithEvents pgb1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tbUnitName As System.Windows.Forms.TextBox
    Friend WithEvents grdAttSummeryDtls As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdAttSummeryDtlsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbUpdateStatusDept As System.Windows.Forms.Button
    Friend WithEvents pgbdepartment As System.Windows.Forms.ProgressBar
End Class
