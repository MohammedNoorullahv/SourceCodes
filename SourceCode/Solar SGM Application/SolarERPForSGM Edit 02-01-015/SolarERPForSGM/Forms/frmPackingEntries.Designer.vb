<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingEntries
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode5 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPackingEntries))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkbxShow = New System.Windows.Forms.CheckBox
        Me.cbFetch = New System.Windows.Forms.Button
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.plMCData = New System.Windows.Forms.Panel
        Me.grdMCDataDtls = New DevExpress.XtraGrid.GridControl
        Me.grdMCDataDtlsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdMCData = New DevExpress.XtraGrid.GridControl
        Me.grdMCDataV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.grdRTDDetails = New DevExpress.XtraGrid.GridControl
        Me.grdRTDDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.grdRTDSummary = New DevExpress.XtraGrid.GridControl
        Me.grdRTDSummaryV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.grdBuyers = New DevExpress.XtraGrid.GridControl
        Me.grdBuyersV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.tbJobcardNo = New System.Windows.Forms.TextBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbLastScannedBarcode = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tbBarcode = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Panel1.SuspendLayout()
        Me.pl.SuspendLayout()
        Me.plMCData.SuspendLayout()
        CType(Me.grdMCDataDtls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMCDataDtlsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMCData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMCDataV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        CType(Me.grdRTDDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRTDDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        CType(Me.grdRTDSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRTDSummaryV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        CType(Me.grdBuyers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBuyersV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
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
        Me.Panel1.Controls.Add(Me.chkbxShow)
        Me.Panel1.Controls.Add(Me.cbFetch)
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
        'chkbxShow
        '
        Me.chkbxShow.AutoSize = True
        Me.chkbxShow.Location = New System.Drawing.Point(595, 646)
        Me.chkbxShow.Name = "chkbxShow"
        Me.chkbxShow.Size = New System.Drawing.Size(213, 20)
        Me.chkbxShow.TabIndex = 17
        Me.chkbxShow.Text = "Show Mobile Computer Data"
        Me.chkbxShow.UseVisualStyleBackColor = True
        '
        'cbFetch
        '
        Me.cbFetch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFetch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbFetch.Location = New System.Drawing.Point(403, 628)
        Me.cbFetch.Margin = New System.Windows.Forms.Padding(4)
        Me.cbFetch.Name = "cbFetch"
        Me.cbFetch.Size = New System.Drawing.Size(128, 74)
        Me.cbFetch.TabIndex = 16
        Me.cbFetch.Text = "Fetch Data"
        Me.cbFetch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbFetch.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(832, 628)
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(232, 51)
        Me.pgbar.TabIndex = 15
        Me.pgbar.Visible = False
        '
        'pl
        '
        Me.pl.Controls.Add(Me.plMCData)
        Me.pl.Controls.Add(Me.Panel9)
        Me.pl.Controls.Add(Me.Panel6)
        Me.pl.Controls.Add(Me.Panel8)
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 615)
        Me.pl.TabIndex = 0
        '
        'plMCData
        '
        Me.plMCData.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.plMCData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plMCData.Controls.Add(Me.grdMCDataDtls)
        Me.plMCData.Controls.Add(Me.grdMCData)
        Me.plMCData.Location = New System.Drawing.Point(7, 62)
        Me.plMCData.Name = "plMCData"
        Me.plMCData.Size = New System.Drawing.Size(1184, 553)
        Me.plMCData.TabIndex = 19
        Me.plMCData.Visible = False
        '
        'grdMCDataDtls
        '
        Me.grdMCDataDtls.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMCDataDtls.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdMCDataDtls.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdMCDataDtls.Location = New System.Drawing.Point(563, 6)
        Me.grdMCDataDtls.MainView = Me.grdMCDataDtlsV1
        Me.grdMCDataDtls.Margin = New System.Windows.Forms.Padding(4)
        Me.grdMCDataDtls.Name = "grdMCDataDtls"
        Me.grdMCDataDtls.Size = New System.Drawing.Size(548, 543)
        Me.grdMCDataDtls.TabIndex = 15
        Me.grdMCDataDtls.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdMCDataDtlsV1})
        '
        'grdMCDataDtlsV1
        '
        Me.grdMCDataDtlsV1.GridControl = Me.grdMCDataDtls
        Me.grdMCDataDtlsV1.Name = "grdMCDataDtlsV1"
        Me.grdMCDataDtlsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdMCDataDtlsV1.OptionsView.ShowFooter = True
        Me.grdMCDataDtlsV1.OptionsView.ShowGroupPanel = False
        '
        'grdMCData
        '
        Me.grdMCData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMCData.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdMCData.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdMCData.Location = New System.Drawing.Point(7, 4)
        Me.grdMCData.MainView = Me.grdMCDataV1
        Me.grdMCData.Margin = New System.Windows.Forms.Padding(4)
        Me.grdMCData.Name = "grdMCData"
        Me.grdMCData.Size = New System.Drawing.Size(548, 543)
        Me.grdMCData.TabIndex = 14
        Me.grdMCData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdMCDataV1})
        '
        'grdMCDataV1
        '
        Me.grdMCDataV1.GridControl = Me.grdMCData
        Me.grdMCDataV1.Name = "grdMCDataV1"
        Me.grdMCDataV1.OptionsView.ShowAutoFilterRow = True
        Me.grdMCDataV1.OptionsView.ShowFooter = True
        Me.grdMCDataV1.OptionsView.ShowGroupPanel = False
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.grdRTDDetails)
        Me.Panel9.Location = New System.Drawing.Point(7, 318)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1184, 294)
        Me.Panel9.TabIndex = 18
        '
        'grdRTDDetails
        '
        Me.grdRTDDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRTDDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode3.RelationName = "Level1"
        Me.grdRTDDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.grdRTDDetails.Location = New System.Drawing.Point(4, 4)
        Me.grdRTDDetails.MainView = Me.grdRTDDetailsV1
        Me.grdRTDDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdRTDDetails.Name = "grdRTDDetails"
        Me.grdRTDDetails.Size = New System.Drawing.Size(1174, 284)
        Me.grdRTDDetails.TabIndex = 14
        Me.grdRTDDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdRTDDetailsV1})
        '
        'grdRTDDetailsV1
        '
        Me.grdRTDDetailsV1.GridControl = Me.grdRTDDetails
        Me.grdRTDDetailsV1.Name = "grdRTDDetailsV1"
        Me.grdRTDDetailsV1.OptionsView.ColumnAutoWidth = False
        Me.grdRTDDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdRTDDetailsV1.OptionsView.ShowFooter = True
        Me.grdRTDDetailsV1.OptionsView.ShowGroupPanel = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.grdRTDSummary)
        Me.Panel6.Location = New System.Drawing.Point(537, 62)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(654, 250)
        Me.Panel6.TabIndex = 17
        '
        'grdRTDSummary
        '
        Me.grdRTDSummary.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode4.RelationName = "Level1"
        Me.grdRTDSummary.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4})
        Me.grdRTDSummary.Location = New System.Drawing.Point(4, 4)
        Me.grdRTDSummary.MainView = Me.grdRTDSummaryV1
        Me.grdRTDSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.grdRTDSummary.Name = "grdRTDSummary"
        Me.grdRTDSummary.Size = New System.Drawing.Size(642, 240)
        Me.grdRTDSummary.TabIndex = 14
        Me.grdRTDSummary.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdRTDSummaryV1})
        '
        'grdRTDSummaryV1
        '
        Me.grdRTDSummaryV1.GridControl = Me.grdRTDSummary
        Me.grdRTDSummaryV1.Name = "grdRTDSummaryV1"
        Me.grdRTDSummaryV1.OptionsView.ShowAutoFilterRow = True
        Me.grdRTDSummaryV1.OptionsView.ShowFooter = True
        Me.grdRTDSummaryV1.OptionsView.ShowGroupPanel = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.grdBuyers)
        Me.Panel8.Location = New System.Drawing.Point(7, 62)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(528, 250)
        Me.Panel8.TabIndex = 16
        '
        'grdBuyers
        '
        Me.grdBuyers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBuyers.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode5.RelationName = "Level1"
        Me.grdBuyers.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode5})
        Me.grdBuyers.Location = New System.Drawing.Point(7, 4)
        Me.grdBuyers.MainView = Me.grdBuyersV1
        Me.grdBuyers.Margin = New System.Windows.Forms.Padding(4)
        Me.grdBuyers.Name = "grdBuyers"
        Me.grdBuyers.Size = New System.Drawing.Size(515, 240)
        Me.grdBuyers.TabIndex = 13
        Me.grdBuyers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdBuyersV1})
        '
        'grdBuyersV1
        '
        Me.grdBuyersV1.GridControl = Me.grdBuyers
        Me.grdBuyersV1.Name = "grdBuyersV1"
        Me.grdBuyersV1.OptionsView.ShowAutoFilterRow = True
        Me.grdBuyersV1.OptionsView.ShowFooter = True
        Me.grdBuyersV1.OptionsView.ShowGroupPanel = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(7, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1184, 49)
        Me.Panel2.TabIndex = 12
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Honeydew
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Controls.Add(Me.tbJobcardNo)
        Me.Panel7.Location = New System.Drawing.Point(881, -1)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(297, 40)
        Me.Panel7.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 23)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "JC. No :-"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJobcardNo
        '
        Me.tbJobcardNo.BackColor = System.Drawing.Color.White
        Me.tbJobcardNo.Location = New System.Drawing.Point(89, 8)
        Me.tbJobcardNo.Name = "tbJobcardNo"
        Me.tbJobcardNo.ReadOnly = True
        Me.tbJobcardNo.Size = New System.Drawing.Size(200, 23)
        Me.tbJobcardNo.TabIndex = 24
        Me.tbJobcardNo.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Honeydew
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.tbLastScannedBarcode)
        Me.Panel5.Location = New System.Drawing.Point(529, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(348, 40)
        Me.Panel5.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(174, 23)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Last Scanned Barcode :-"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbLastScannedBarcode
        '
        Me.tbLastScannedBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbLastScannedBarcode.Location = New System.Drawing.Point(184, 8)
        Me.tbLastScannedBarcode.Name = "tbLastScannedBarcode"
        Me.tbLastScannedBarcode.Size = New System.Drawing.Size(159, 23)
        Me.tbLastScannedBarcode.TabIndex = 35
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Honeydew
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.tbBarcode)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(297, 40)
        Me.Panel4.TabIndex = 14
        '
        'tbBarcode
        '
        Me.tbBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbBarcode.Location = New System.Drawing.Point(89, 8)
        Me.tbBarcode.Name = "tbBarcode"
        Me.tbBarcode.Size = New System.Drawing.Size(200, 23)
        Me.tbBarcode.TabIndex = 24
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(4, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 23)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Barcode :-"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Ivory
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 40)
        Me.Panel3.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date :-"
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
        Me.dpFrom.TabStop = False
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
        Me.cbPrint.Visible = False
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
        'PrintDocument1
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmPackingEntries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPackingEntries"
        Me.Text = "Packing Entries"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pl.ResumeLayout(False)
        Me.plMCData.ResumeLayout(False)
        CType(Me.grdMCDataDtls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMCDataDtlsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMCData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMCDataV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        CType(Me.grdRTDDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRTDDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        CType(Me.grdRTDSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRTDSummaryV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        CType(Me.grdBuyers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBuyersV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
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
    Friend WithEvents tbBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbJobcardNo As System.Windows.Forms.TextBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbLastScannedBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents grdRTDSummary As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdRTDSummaryV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdBuyers As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdBuyersV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents grdRTDDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdRTDDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbFetch As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents plMCData As System.Windows.Forms.Panel
    Friend WithEvents grdMCData As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdMCDataV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkbxShow As System.Windows.Forms.CheckBox
    Friend WithEvents grdMCDataDtls As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdMCDataDtlsV1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
