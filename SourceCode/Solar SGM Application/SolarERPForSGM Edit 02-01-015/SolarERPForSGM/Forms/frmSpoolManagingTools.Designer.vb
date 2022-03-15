<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpoolManagingTools
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
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition3 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpoolManagingTools))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.plPartQtyInfo = New System.Windows.Forms.Panel
        Me.lblSize18 = New System.Windows.Forms.Label
        Me.tbJCQty18 = New System.Windows.Forms.TextBox
        Me.lblSize17 = New System.Windows.Forms.Label
        Me.tbJCQty17 = New System.Windows.Forms.TextBox
        Me.lblSize16 = New System.Windows.Forms.Label
        Me.tbJCQty16 = New System.Windows.Forms.TextBox
        Me.lblSize15 = New System.Windows.Forms.Label
        Me.tbJCQty15 = New System.Windows.Forms.TextBox
        Me.lblSize14 = New System.Windows.Forms.Label
        Me.tbJCQty14 = New System.Windows.Forms.TextBox
        Me.lblSize13 = New System.Windows.Forms.Label
        Me.tbJCQty13 = New System.Windows.Forms.TextBox
        Me.lblSize12 = New System.Windows.Forms.Label
        Me.tbJCQty12 = New System.Windows.Forms.TextBox
        Me.lblSize11 = New System.Windows.Forms.Label
        Me.tbJCQty11 = New System.Windows.Forms.TextBox
        Me.lblSize10 = New System.Windows.Forms.Label
        Me.tbJCQty10 = New System.Windows.Forms.TextBox
        Me.lblSize09 = New System.Windows.Forms.Label
        Me.tbJCQty09 = New System.Windows.Forms.TextBox
        Me.lblSize08 = New System.Windows.Forms.Label
        Me.tbJCQty08 = New System.Windows.Forms.TextBox
        Me.lblSize07 = New System.Windows.Forms.Label
        Me.tbJCQty07 = New System.Windows.Forms.TextBox
        Me.lblSize06 = New System.Windows.Forms.Label
        Me.tbJCQty06 = New System.Windows.Forms.TextBox
        Me.lblSize05 = New System.Windows.Forms.Label
        Me.tbJCQty05 = New System.Windows.Forms.TextBox
        Me.lblSize04 = New System.Windows.Forms.Label
        Me.tbJCQty04 = New System.Windows.Forms.TextBox
        Me.lblSize03 = New System.Windows.Forms.Label
        Me.tbJCQty03 = New System.Windows.Forms.TextBox
        Me.lblSize02 = New System.Windows.Forms.Label
        Me.tbJCQty02 = New System.Windows.Forms.TextBox
        Me.lblSize01 = New System.Windows.Forms.Label
        Me.Label48 = New System.Windows.Forms.Label
        Me.tbJCQty01 = New System.Windows.Forms.TextBox
        Me.Label62 = New System.Windows.Forms.Label
        Me.chkbxSpoolInfo = New System.Windows.Forms.CheckBox
        Me.grdWIPDetails = New DevExpress.XtraGrid.GridControl
        Me.grdWIPDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdWIPSummary = New DevExpress.XtraGrid.GridControl
        Me.grdWIPSummaryV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rbA4 = New System.Windows.Forms.RadioButton
        Me.rbA5 = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbxTypeofSpoon = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.plPartQtyInfo.SuspendLayout()
        CType(Me.grdWIPDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWIPDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWIPSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWIPSummaryV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.plPartQtyInfo)
        Me.Panel1.Controls.Add(Me.chkbxSpoolInfo)
        Me.Panel1.Controls.Add(Me.grdWIPDetails)
        Me.Panel1.Controls.Add(Me.grdWIPSummary)
        Me.Panel1.Controls.Add(Me.pgbar)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'plPartQtyInfo
        '
        Me.plPartQtyInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.plPartQtyInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plPartQtyInfo.Controls.Add(Me.lblSize18)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty18)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize17)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty17)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize16)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty16)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize15)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty15)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize14)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty14)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize13)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty13)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize12)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty12)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize11)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty11)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize10)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty10)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize09)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty09)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize08)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty08)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize07)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty07)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize06)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty06)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize05)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty05)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize04)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty04)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize03)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty03)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize02)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty02)
        Me.plPartQtyInfo.Controls.Add(Me.lblSize01)
        Me.plPartQtyInfo.Controls.Add(Me.Label48)
        Me.plPartQtyInfo.Controls.Add(Me.tbJCQty01)
        Me.plPartQtyInfo.Controls.Add(Me.Label62)
        Me.plPartQtyInfo.Location = New System.Drawing.Point(329, 554)
        Me.plPartQtyInfo.Name = "plPartQtyInfo"
        Me.plPartQtyInfo.Size = New System.Drawing.Size(819, 66)
        Me.plPartQtyInfo.TabIndex = 24
        '
        'lblSize18
        '
        Me.lblSize18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize18.Location = New System.Drawing.Point(771, 4)
        Me.lblSize18.Name = "lblSize18"
        Me.lblSize18.Size = New System.Drawing.Size(39, 23)
        Me.lblSize18.TabIndex = 105
        Me.lblSize18.Text = "39.5"
        Me.lblSize18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty18
        '
        Me.tbJCQty18.BackColor = System.Drawing.Color.White
        Me.tbJCQty18.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty18.Location = New System.Drawing.Point(771, 33)
        Me.tbJCQty18.Name = "tbJCQty18"
        Me.tbJCQty18.ReadOnly = True
        Me.tbJCQty18.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty18.TabIndex = 104
        Me.tbJCQty18.TabStop = False
        Me.tbJCQty18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize17
        '
        Me.lblSize17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize17.Location = New System.Drawing.Point(730, 4)
        Me.lblSize17.Name = "lblSize17"
        Me.lblSize17.Size = New System.Drawing.Size(39, 23)
        Me.lblSize17.TabIndex = 103
        Me.lblSize17.Text = "39.5"
        Me.lblSize17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty17
        '
        Me.tbJCQty17.BackColor = System.Drawing.Color.White
        Me.tbJCQty17.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty17.Location = New System.Drawing.Point(730, 33)
        Me.tbJCQty17.Name = "tbJCQty17"
        Me.tbJCQty17.ReadOnly = True
        Me.tbJCQty17.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty17.TabIndex = 102
        Me.tbJCQty17.TabStop = False
        Me.tbJCQty17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize16
        '
        Me.lblSize16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize16.Location = New System.Drawing.Point(689, 4)
        Me.lblSize16.Name = "lblSize16"
        Me.lblSize16.Size = New System.Drawing.Size(39, 23)
        Me.lblSize16.TabIndex = 101
        Me.lblSize16.Text = "39.5"
        Me.lblSize16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty16
        '
        Me.tbJCQty16.BackColor = System.Drawing.Color.White
        Me.tbJCQty16.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty16.Location = New System.Drawing.Point(689, 33)
        Me.tbJCQty16.Name = "tbJCQty16"
        Me.tbJCQty16.ReadOnly = True
        Me.tbJCQty16.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty16.TabIndex = 100
        Me.tbJCQty16.TabStop = False
        Me.tbJCQty16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize15
        '
        Me.lblSize15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize15.Location = New System.Drawing.Point(648, 4)
        Me.lblSize15.Name = "lblSize15"
        Me.lblSize15.Size = New System.Drawing.Size(39, 23)
        Me.lblSize15.TabIndex = 99
        Me.lblSize15.Text = "39.5"
        Me.lblSize15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty15
        '
        Me.tbJCQty15.BackColor = System.Drawing.Color.White
        Me.tbJCQty15.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty15.Location = New System.Drawing.Point(648, 33)
        Me.tbJCQty15.Name = "tbJCQty15"
        Me.tbJCQty15.ReadOnly = True
        Me.tbJCQty15.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty15.TabIndex = 98
        Me.tbJCQty15.TabStop = False
        Me.tbJCQty15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize14
        '
        Me.lblSize14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize14.Location = New System.Drawing.Point(607, 4)
        Me.lblSize14.Name = "lblSize14"
        Me.lblSize14.Size = New System.Drawing.Size(39, 23)
        Me.lblSize14.TabIndex = 97
        Me.lblSize14.Text = "39.5"
        Me.lblSize14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty14
        '
        Me.tbJCQty14.BackColor = System.Drawing.Color.White
        Me.tbJCQty14.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty14.Location = New System.Drawing.Point(607, 33)
        Me.tbJCQty14.Name = "tbJCQty14"
        Me.tbJCQty14.ReadOnly = True
        Me.tbJCQty14.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty14.TabIndex = 96
        Me.tbJCQty14.TabStop = False
        Me.tbJCQty14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize13
        '
        Me.lblSize13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize13.Location = New System.Drawing.Point(566, 4)
        Me.lblSize13.Name = "lblSize13"
        Me.lblSize13.Size = New System.Drawing.Size(39, 23)
        Me.lblSize13.TabIndex = 95
        Me.lblSize13.Text = "39.5"
        Me.lblSize13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty13
        '
        Me.tbJCQty13.BackColor = System.Drawing.Color.White
        Me.tbJCQty13.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty13.Location = New System.Drawing.Point(566, 33)
        Me.tbJCQty13.Name = "tbJCQty13"
        Me.tbJCQty13.ReadOnly = True
        Me.tbJCQty13.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty13.TabIndex = 94
        Me.tbJCQty13.TabStop = False
        Me.tbJCQty13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize12
        '
        Me.lblSize12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize12.Location = New System.Drawing.Point(525, 4)
        Me.lblSize12.Name = "lblSize12"
        Me.lblSize12.Size = New System.Drawing.Size(39, 23)
        Me.lblSize12.TabIndex = 93
        Me.lblSize12.Text = "39.5"
        Me.lblSize12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty12
        '
        Me.tbJCQty12.BackColor = System.Drawing.Color.White
        Me.tbJCQty12.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty12.Location = New System.Drawing.Point(525, 33)
        Me.tbJCQty12.Name = "tbJCQty12"
        Me.tbJCQty12.ReadOnly = True
        Me.tbJCQty12.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty12.TabIndex = 92
        Me.tbJCQty12.TabStop = False
        Me.tbJCQty12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize11
        '
        Me.lblSize11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize11.Location = New System.Drawing.Point(484, 4)
        Me.lblSize11.Name = "lblSize11"
        Me.lblSize11.Size = New System.Drawing.Size(39, 23)
        Me.lblSize11.TabIndex = 91
        Me.lblSize11.Text = "39.5"
        Me.lblSize11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty11
        '
        Me.tbJCQty11.BackColor = System.Drawing.Color.White
        Me.tbJCQty11.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty11.Location = New System.Drawing.Point(484, 33)
        Me.tbJCQty11.Name = "tbJCQty11"
        Me.tbJCQty11.ReadOnly = True
        Me.tbJCQty11.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty11.TabIndex = 90
        Me.tbJCQty11.TabStop = False
        Me.tbJCQty11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize10
        '
        Me.lblSize10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize10.Location = New System.Drawing.Point(443, 4)
        Me.lblSize10.Name = "lblSize10"
        Me.lblSize10.Size = New System.Drawing.Size(39, 23)
        Me.lblSize10.TabIndex = 89
        Me.lblSize10.Text = "39.5"
        Me.lblSize10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty10
        '
        Me.tbJCQty10.BackColor = System.Drawing.Color.White
        Me.tbJCQty10.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty10.Location = New System.Drawing.Point(443, 33)
        Me.tbJCQty10.Name = "tbJCQty10"
        Me.tbJCQty10.ReadOnly = True
        Me.tbJCQty10.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty10.TabIndex = 88
        Me.tbJCQty10.TabStop = False
        Me.tbJCQty10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize09
        '
        Me.lblSize09.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize09.Location = New System.Drawing.Point(402, 4)
        Me.lblSize09.Name = "lblSize09"
        Me.lblSize09.Size = New System.Drawing.Size(39, 23)
        Me.lblSize09.TabIndex = 87
        Me.lblSize09.Text = "39.5"
        Me.lblSize09.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty09
        '
        Me.tbJCQty09.BackColor = System.Drawing.Color.White
        Me.tbJCQty09.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty09.Location = New System.Drawing.Point(402, 33)
        Me.tbJCQty09.Name = "tbJCQty09"
        Me.tbJCQty09.ReadOnly = True
        Me.tbJCQty09.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty09.TabIndex = 86
        Me.tbJCQty09.TabStop = False
        Me.tbJCQty09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize08
        '
        Me.lblSize08.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize08.Location = New System.Drawing.Point(361, 4)
        Me.lblSize08.Name = "lblSize08"
        Me.lblSize08.Size = New System.Drawing.Size(39, 23)
        Me.lblSize08.TabIndex = 85
        Me.lblSize08.Text = "39.5"
        Me.lblSize08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty08
        '
        Me.tbJCQty08.BackColor = System.Drawing.Color.White
        Me.tbJCQty08.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty08.Location = New System.Drawing.Point(361, 33)
        Me.tbJCQty08.Name = "tbJCQty08"
        Me.tbJCQty08.ReadOnly = True
        Me.tbJCQty08.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty08.TabIndex = 84
        Me.tbJCQty08.TabStop = False
        Me.tbJCQty08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize07
        '
        Me.lblSize07.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize07.Location = New System.Drawing.Point(320, 4)
        Me.lblSize07.Name = "lblSize07"
        Me.lblSize07.Size = New System.Drawing.Size(39, 23)
        Me.lblSize07.TabIndex = 83
        Me.lblSize07.Text = "39.5"
        Me.lblSize07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty07
        '
        Me.tbJCQty07.BackColor = System.Drawing.Color.White
        Me.tbJCQty07.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty07.Location = New System.Drawing.Point(320, 33)
        Me.tbJCQty07.Name = "tbJCQty07"
        Me.tbJCQty07.ReadOnly = True
        Me.tbJCQty07.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty07.TabIndex = 82
        Me.tbJCQty07.TabStop = False
        Me.tbJCQty07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize06
        '
        Me.lblSize06.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize06.Location = New System.Drawing.Point(279, 4)
        Me.lblSize06.Name = "lblSize06"
        Me.lblSize06.Size = New System.Drawing.Size(39, 23)
        Me.lblSize06.TabIndex = 81
        Me.lblSize06.Text = "39.5"
        Me.lblSize06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty06
        '
        Me.tbJCQty06.BackColor = System.Drawing.Color.White
        Me.tbJCQty06.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty06.Location = New System.Drawing.Point(279, 33)
        Me.tbJCQty06.Name = "tbJCQty06"
        Me.tbJCQty06.ReadOnly = True
        Me.tbJCQty06.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty06.TabIndex = 80
        Me.tbJCQty06.TabStop = False
        Me.tbJCQty06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize05
        '
        Me.lblSize05.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize05.Location = New System.Drawing.Point(238, 4)
        Me.lblSize05.Name = "lblSize05"
        Me.lblSize05.Size = New System.Drawing.Size(39, 23)
        Me.lblSize05.TabIndex = 79
        Me.lblSize05.Text = "39.5"
        Me.lblSize05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty05
        '
        Me.tbJCQty05.BackColor = System.Drawing.Color.White
        Me.tbJCQty05.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty05.Location = New System.Drawing.Point(238, 33)
        Me.tbJCQty05.Name = "tbJCQty05"
        Me.tbJCQty05.ReadOnly = True
        Me.tbJCQty05.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty05.TabIndex = 78
        Me.tbJCQty05.TabStop = False
        Me.tbJCQty05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize04
        '
        Me.lblSize04.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize04.Location = New System.Drawing.Point(197, 4)
        Me.lblSize04.Name = "lblSize04"
        Me.lblSize04.Size = New System.Drawing.Size(39, 23)
        Me.lblSize04.TabIndex = 77
        Me.lblSize04.Text = "39.5"
        Me.lblSize04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty04
        '
        Me.tbJCQty04.BackColor = System.Drawing.Color.White
        Me.tbJCQty04.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty04.Location = New System.Drawing.Point(197, 33)
        Me.tbJCQty04.Name = "tbJCQty04"
        Me.tbJCQty04.ReadOnly = True
        Me.tbJCQty04.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty04.TabIndex = 76
        Me.tbJCQty04.TabStop = False
        Me.tbJCQty04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize03
        '
        Me.lblSize03.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize03.Location = New System.Drawing.Point(156, 4)
        Me.lblSize03.Name = "lblSize03"
        Me.lblSize03.Size = New System.Drawing.Size(39, 23)
        Me.lblSize03.TabIndex = 75
        Me.lblSize03.Text = "39.5"
        Me.lblSize03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty03
        '
        Me.tbJCQty03.BackColor = System.Drawing.Color.White
        Me.tbJCQty03.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty03.Location = New System.Drawing.Point(156, 33)
        Me.tbJCQty03.Name = "tbJCQty03"
        Me.tbJCQty03.ReadOnly = True
        Me.tbJCQty03.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty03.TabIndex = 74
        Me.tbJCQty03.TabStop = False
        Me.tbJCQty03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize02
        '
        Me.lblSize02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize02.Location = New System.Drawing.Point(115, 4)
        Me.lblSize02.Name = "lblSize02"
        Me.lblSize02.Size = New System.Drawing.Size(39, 23)
        Me.lblSize02.TabIndex = 73
        Me.lblSize02.Text = "39.5"
        Me.lblSize02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty02
        '
        Me.tbJCQty02.BackColor = System.Drawing.Color.White
        Me.tbJCQty02.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty02.Location = New System.Drawing.Point(115, 33)
        Me.tbJCQty02.Name = "tbJCQty02"
        Me.tbJCQty02.ReadOnly = True
        Me.tbJCQty02.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty02.TabIndex = 72
        Me.tbJCQty02.TabStop = False
        Me.tbJCQty02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSize01
        '
        Me.lblSize01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSize01.Location = New System.Drawing.Point(74, 4)
        Me.lblSize01.Name = "lblSize01"
        Me.lblSize01.Size = New System.Drawing.Size(39, 23)
        Me.lblSize01.TabIndex = 71
        Me.lblSize01.Text = "39.5"
        Me.lblSize01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label48
        '
        Me.Label48.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label48.Location = New System.Drawing.Point(7, 33)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(62, 23)
        Me.Label48.TabIndex = 65
        Me.Label48.Text = "Qty :-"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJCQty01
        '
        Me.tbJCQty01.BackColor = System.Drawing.Color.White
        Me.tbJCQty01.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbJCQty01.Location = New System.Drawing.Point(74, 33)
        Me.tbJCQty01.Name = "tbJCQty01"
        Me.tbJCQty01.ReadOnly = True
        Me.tbJCQty01.Size = New System.Drawing.Size(39, 23)
        Me.tbJCQty01.TabIndex = 66
        Me.tbJCQty01.TabStop = False
        Me.tbJCQty01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label62
        '
        Me.Label62.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label62.Location = New System.Drawing.Point(7, 4)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(62, 23)
        Me.Label62.TabIndex = 35
        Me.Label62.Text = "Sizes :-"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkbxSpoolInfo
        '
        Me.chkbxSpoolInfo.AutoSize = True
        Me.chkbxSpoolInfo.Location = New System.Drawing.Point(401, 682)
        Me.chkbxSpoolInfo.Name = "chkbxSpoolInfo"
        Me.chkbxSpoolInfo.Size = New System.Drawing.Size(129, 20)
        Me.chkbxSpoolInfo.TabIndex = 23
        Me.chkbxSpoolInfo.Text = "Print Spool Info"
        Me.chkbxSpoolInfo.UseVisualStyleBackColor = True
        '
        'grdWIPDetails
        '
        Me.grdWIPDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdWIPDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode3.RelationName = "Level1"
        Me.grdWIPDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.grdWIPDetails.Location = New System.Drawing.Point(329, 108)
        Me.grdWIPDetails.MainView = Me.grdWIPDetailsV1
        Me.grdWIPDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdWIPDetails.Name = "grdWIPDetails"
        Me.grdWIPDetails.Size = New System.Drawing.Size(865, 442)
        Me.grdWIPDetails.TabIndex = 22
        Me.grdWIPDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdWIPDetailsV1})
        '
        'grdWIPDetailsV1
        '
        StyleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition3.Appearance.Options.UseForeColor = True
        StyleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition3.Value1 = CType(0, Short)
        Me.grdWIPDetailsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition3})
        Me.grdWIPDetailsV1.GridControl = Me.grdWIPDetails
        Me.grdWIPDetailsV1.Name = "grdWIPDetailsV1"
        Me.grdWIPDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdWIPDetailsV1.OptionsView.ShowFooter = True
        '
        'grdWIPSummary
        '
        Me.grdWIPSummary.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdWIPSummary.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdWIPSummary.Location = New System.Drawing.Point(12, 108)
        Me.grdWIPSummary.MainView = Me.grdWIPSummaryV1
        Me.grdWIPSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.grdWIPSummary.Name = "grdWIPSummary"
        Me.grdWIPSummary.Size = New System.Drawing.Size(309, 512)
        Me.grdWIPSummary.TabIndex = 21
        Me.grdWIPSummary.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdWIPSummaryV1})
        '
        'grdWIPSummaryV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdWIPSummaryV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdWIPSummaryV1.GridControl = Me.grdWIPSummary
        Me.grdWIPSummaryV1.Name = "grdWIPSummaryV1"
        Me.grdWIPSummaryV1.OptionsView.ColumnAutoWidth = False
        Me.grdWIPSummaryV1.OptionsView.ShowAutoFilterRow = True
        Me.grdWIPSummaryV1.OptionsView.ShowFooter = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(401, 628)
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(663, 51)
        Me.pgbar.TabIndex = 15
        '
        'pl
        '
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 95)
        Me.pl.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(7, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1184, 82)
        Me.Panel2.TabIndex = 12
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Honeydew
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.rbA4)
        Me.Panel5.Controls.Add(Me.rbA5)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Location = New System.Drawing.Point(657, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(109, 76)
        Me.Panel5.TabIndex = 15
        '
        'rbA4
        '
        Me.rbA4.AutoSize = True
        Me.rbA4.Location = New System.Drawing.Point(56, 43)
        Me.rbA4.Name = "rbA4"
        Me.rbA4.Size = New System.Drawing.Size(43, 20)
        Me.rbA4.TabIndex = 22
        Me.rbA4.Text = "A4"
        Me.rbA4.UseVisualStyleBackColor = True
        '
        'rbA5
        '
        Me.rbA5.AutoSize = True
        Me.rbA5.Checked = True
        Me.rbA5.Location = New System.Drawing.Point(7, 42)
        Me.rbA5.Name = "rbA5"
        Me.rbA5.Size = New System.Drawing.Size(43, 20)
        Me.rbA5.TabIndex = 21
        Me.rbA5.TabStop = True
        Me.rbA5.Text = "A5"
        Me.rbA5.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 23)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Paper Size"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Honeydew
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.cbxTypeofSpoon)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.cbxCustomer)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(413, 76)
        Me.Panel4.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(4, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(138, 23)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Customer"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxTypeofSpoon
        '
        Me.cbxTypeofSpoon.FormattingEnabled = True
        Me.cbxTypeofSpoon.Items.AddRange(New Object() {"All", "Mould", "Finish", "Pack"})
        Me.cbxTypeofSpoon.Location = New System.Drawing.Point(145, 11)
        Me.cbxTypeofSpoon.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxTypeofSpoon.Name = "cbxTypeofSpoon"
        Me.cbxTypeofSpoon.Size = New System.Drawing.Size(262, 24)
        Me.cbxTypeofSpoon.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 23)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Type of Spoon "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(145, 38)
        Me.cbxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(262, 24)
        Me.cbxCustomer.TabIndex = 12
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Ivory
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.dpTo)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 76)
        Me.Panel3.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From :-"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 43)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To :-"
        '
        'dpTo
        '
        Me.dpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpTo.Location = New System.Drawing.Point(75, 40)
        Me.dpTo.Margin = New System.Windows.Forms.Padding(4)
        Me.dpTo.Name = "dpTo"
        Me.dpTo.Size = New System.Drawing.Size(139, 23)
        Me.dpTo.TabIndex = 3
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(75, 8)
        Me.dpFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(139, 23)
        Me.dpFrom.TabIndex = 2
        '
        'cbPrint
        '
        Me.cbPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPrint.Location = New System.Drawing.Point(266, 628)
        Me.cbPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(128, 74)
        Me.cbPrint.TabIndex = 7
        Me.cbPrint.Text = "Print"
        Me.cbPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPrint.UseVisualStyleBackColor = True
        '
        'Export2Excel
        '
        Me.Export2Excel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export2Excel.Image = CType(resources.GetObject("Export2Excel.Image"), System.Drawing.Image)
        Me.Export2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Export2Excel.Location = New System.Drawing.Point(135, 628)
        Me.Export2Excel.Margin = New System.Windows.Forms.Padding(4)
        Me.Export2Excel.Name = "Export2Excel"
        Me.Export2Excel.Size = New System.Drawing.Size(128, 74)
        Me.Export2Excel.TabIndex = 4
        Me.Export2Excel.Text = "Expot" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to   " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Excel"
        Me.Export2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export2Excel.UseVisualStyleBackColor = True
        '
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(1071, 628)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(128, 74)
        Me.cbExit.TabIndex = 2
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbReferesh
        '
        Me.cbReferesh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReferesh.Image = CType(resources.GetObject("cbReferesh.Image"), System.Drawing.Image)
        Me.cbReferesh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbReferesh.Location = New System.Drawing.Point(4, 628)
        Me.cbReferesh.Margin = New System.Windows.Forms.Padding(4)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'frmSpoolManagingTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSpoolManagingTools"
        Me.Text = "ERP Tracking System v2"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.plPartQtyInfo.ResumeLayout(False)
        Me.plPartQtyInfo.PerformLayout()
        CType(Me.grdWIPDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWIPDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWIPSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWIPSummaryV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbxTypeofSpoon As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents grdWIPSummary As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdWIPSummaryV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdWIPDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdWIPDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkbxSpoolInfo As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rbA4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbA5 As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents plPartQtyInfo As System.Windows.Forms.Panel
    Friend WithEvents lblSize18 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty18 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize17 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty17 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize16 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty16 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize15 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty15 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize14 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty14 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize13 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty13 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize12 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty12 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize11 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty11 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize10 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty10 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize09 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty09 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize08 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty08 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize07 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty07 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize06 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty06 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize05 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty05 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize04 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty04 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize03 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty03 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize02 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty02 As System.Windows.Forms.TextBox
    Friend WithEvents lblSize01 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents tbJCQty01 As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
End Class
