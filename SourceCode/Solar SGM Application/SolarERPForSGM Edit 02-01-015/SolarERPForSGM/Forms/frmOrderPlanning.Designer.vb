<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAOrderPlanning
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAOrderPlanning))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbOpenOrder = New System.Windows.Forms.Button
        Me.grdSalesOrderDetailsWKSummary = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderDetailsWKSummaryV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdSalesOrderDetails = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.cbxProductTypeMain = New System.Windows.Forms.ComboBox
        Me.rbAll = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.rbNo = New System.Windows.Forms.RadioButton
        Me.Label14 = New System.Windows.Forms.Label
        Me.rbYes = New System.Windows.Forms.RadioButton
        Me.tbWeekTo = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.cbxSortingType = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.cbxProductType = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.tbWeekFrom = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cbxYear = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.cbxShipmentStatus = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbxProductionStatus = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbArticleDescription = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cbxArticleMould = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rbDeliveryDate = New System.Windows.Forms.RadioButton
        Me.rbOrderDate = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.plSalesOrdDtl = New System.Windows.Forms.GroupBox
        Me.dpExpectedDlyDt = New System.Windows.Forms.DateTimePicker
        Me.dpBuyerDlyDt = New System.Windows.Forms.DateTimePicker
        Me.cbExitUpdateModule = New System.Windows.Forms.Button
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.tbWeekNo = New System.Windows.Forms.TextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.tbCustomerOrder = New System.Windows.Forms.TextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.tbTotalQty = New System.Windows.Forms.TextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.tbQty18 = New System.Windows.Forms.TextBox
        Me.lblS18 = New System.Windows.Forms.Label
        Me.tbQty17 = New System.Windows.Forms.TextBox
        Me.lblS17 = New System.Windows.Forms.Label
        Me.tbQty16 = New System.Windows.Forms.TextBox
        Me.lblS16 = New System.Windows.Forms.Label
        Me.tbQty15 = New System.Windows.Forms.TextBox
        Me.lblS15 = New System.Windows.Forms.Label
        Me.tbQty14 = New System.Windows.Forms.TextBox
        Me.lblS14 = New System.Windows.Forms.Label
        Me.tbQty13 = New System.Windows.Forms.TextBox
        Me.lblS13 = New System.Windows.Forms.Label
        Me.tbQty12 = New System.Windows.Forms.TextBox
        Me.lblS12 = New System.Windows.Forms.Label
        Me.tbQty11 = New System.Windows.Forms.TextBox
        Me.lblS11 = New System.Windows.Forms.Label
        Me.tbQty10 = New System.Windows.Forms.TextBox
        Me.lblS10 = New System.Windows.Forms.Label
        Me.tbQty09 = New System.Windows.Forms.TextBox
        Me.lblS09 = New System.Windows.Forms.Label
        Me.tbQty08 = New System.Windows.Forms.TextBox
        Me.lblS08 = New System.Windows.Forms.Label
        Me.tbQty07 = New System.Windows.Forms.TextBox
        Me.lblS07 = New System.Windows.Forms.Label
        Me.tbQty06 = New System.Windows.Forms.TextBox
        Me.lblS06 = New System.Windows.Forms.Label
        Me.tbQty05 = New System.Windows.Forms.TextBox
        Me.lblS05 = New System.Windows.Forms.Label
        Me.tbQty04 = New System.Windows.Forms.TextBox
        Me.lblS04 = New System.Windows.Forms.Label
        Me.tbQty03 = New System.Windows.Forms.TextBox
        Me.lblS03 = New System.Windows.Forms.Label
        Me.tbQty02 = New System.Windows.Forms.TextBox
        Me.lblS02 = New System.Windows.Forms.Label
        Me.tbQty01 = New System.Windows.Forms.TextBox
        Me.lblS01 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.tbArticleName = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.tbArticleCode = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.tbSalesOrderNo = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cbUpdate = New System.Windows.Forms.Button
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdSalesOrderDetailsWKSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetailsWKSummaryV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.plSalesOrdDtl.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.cbOpenOrder)
        Me.Panel1.Controls.Add(Me.grdSalesOrderDetailsWKSummary)
        Me.Panel1.Controls.Add(Me.grdSalesOrderDetails)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.plSalesOrdDtl)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'cbOpenOrder
        '
        Me.cbOpenOrder.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOpenOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbOpenOrder.Location = New System.Drawing.Point(409, 628)
        Me.cbOpenOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.cbOpenOrder.Name = "cbOpenOrder"
        Me.cbOpenOrder.Size = New System.Drawing.Size(128, 74)
        Me.cbOpenOrder.TabIndex = 20
        Me.cbOpenOrder.Text = "Open" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Order"
        Me.cbOpenOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbOpenOrder.UseVisualStyleBackColor = True
        '
        'grdSalesOrderDetailsWKSummary
        '
        Me.grdSalesOrderDetailsWKSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderDetailsWKSummary.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdSalesOrderDetailsWKSummary.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdSalesOrderDetailsWKSummary.Location = New System.Drawing.Point(921, 187)
        Me.grdSalesOrderDetailsWKSummary.MainView = Me.grdSalesOrderDetailsWKSummaryV1
        Me.grdSalesOrderDetailsWKSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderDetailsWKSummary.Name = "grdSalesOrderDetailsWKSummary"
        Me.grdSalesOrderDetailsWKSummary.Size = New System.Drawing.Size(273, 433)
        Me.grdSalesOrderDetailsWKSummary.TabIndex = 19
        Me.grdSalesOrderDetailsWKSummary.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderDetailsWKSummaryV1})
        '
        'grdSalesOrderDetailsWKSummaryV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdSalesOrderDetailsWKSummaryV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdSalesOrderDetailsWKSummaryV1.GridControl = Me.grdSalesOrderDetailsWKSummary
        Me.grdSalesOrderDetailsWKSummaryV1.Name = "grdSalesOrderDetailsWKSummaryV1"
        Me.grdSalesOrderDetailsWKSummaryV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderDetailsWKSummaryV1.OptionsView.ShowFooter = True
        '
        'grdSalesOrderDetails
        '
        Me.grdSalesOrderDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdSalesOrderDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdSalesOrderDetails.Location = New System.Drawing.Point(12, 187)
        Me.grdSalesOrderDetails.MainView = Me.grdSalesOrderDetailsV1
        Me.grdSalesOrderDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderDetails.Name = "grdSalesOrderDetails"
        Me.grdSalesOrderDetails.Size = New System.Drawing.Size(901, 433)
        Me.grdSalesOrderDetails.TabIndex = 18
        Me.grdSalesOrderDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderDetailsV1})
        '
        'grdSalesOrderDetailsV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdSalesOrderDetailsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdSalesOrderDetailsV1.GridControl = Me.grdSalesOrderDetails
        Me.grdSalesOrderDetailsV1.Name = "grdSalesOrderDetailsV1"
        Me.grdSalesOrderDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderDetailsV1.OptionsView.ShowFooter = True
        '
        'pl
        '
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 174)
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
        Me.Panel2.Size = New System.Drawing.Size(1184, 159)
        Me.Panel2.TabIndex = 12
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Ivory
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.cbxProductTypeMain)
        Me.Panel5.Controls.Add(Me.rbAll)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.rbNo)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.rbYes)
        Me.Panel5.Controls.Add(Me.tbWeekTo)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.cbxSortingType)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.cbxProductType)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.tbWeekFrom)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.cbxYear)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Location = New System.Drawing.Point(645, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(534, 153)
        Me.Panel5.TabIndex = 15
        '
        'cbxProductTypeMain
        '
        Me.cbxProductTypeMain.FormattingEnabled = True
        Me.cbxProductTypeMain.Location = New System.Drawing.Point(149, 92)
        Me.cbxProductTypeMain.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductTypeMain.Name = "cbxProductTypeMain"
        Me.cbxProductTypeMain.Size = New System.Drawing.Size(145, 24)
        Me.cbxProductTypeMain.TabIndex = 39
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Location = New System.Drawing.Point(149, 12)
        Me.rbAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(41, 20)
        Me.rbAll.TabIndex = 38
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(4, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 23)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Dly Dt Negotiable"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbNo
        '
        Me.rbNo.AutoSize = True
        Me.rbNo.Location = New System.Drawing.Point(313, 11)
        Me.rbNo.Margin = New System.Windows.Forms.Padding(4)
        Me.rbNo.Name = "rbNo"
        Me.rbNo.Size = New System.Drawing.Size(43, 20)
        Me.rbNo.TabIndex = 30
        Me.rbNo.Text = "No"
        Me.rbNo.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(228, 65)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 23)
        Me.Label14.TabIndex = 37
        Me.Label14.Text = "To"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbYes
        '
        Me.rbYes.AutoSize = True
        Me.rbYes.Location = New System.Drawing.Point(234, 11)
        Me.rbYes.Margin = New System.Windows.Forms.Padding(4)
        Me.rbYes.Name = "rbYes"
        Me.rbYes.Size = New System.Drawing.Size(50, 20)
        Me.rbYes.TabIndex = 29
        Me.rbYes.Text = "Yes"
        Me.rbYes.UseVisualStyleBackColor = True
        '
        'tbWeekTo
        '
        Me.tbWeekTo.Location = New System.Drawing.Point(300, 65)
        Me.tbWeekTo.Name = "tbWeekTo"
        Me.tbWeekTo.Size = New System.Drawing.Size(64, 23)
        Me.tbWeekTo.TabIndex = 36
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(76, 65)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 23)
        Me.Label13.TabIndex = 35
        Me.Label13.Text = "From"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxSortingType
        '
        Me.cbxSortingType.FormattingEnabled = True
        Me.cbxSortingType.Items.AddRange(New Object() {"WEEK / DELIVERY DATE / CUSTOMER ORDER DATE", "WEEK / CUSTOMER ORDER DATE / DELIVERY DATE"})
        Me.cbxSortingType.Location = New System.Drawing.Point(149, 119)
        Me.cbxSortingType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxSortingType.Name = "cbxSortingType"
        Me.cbxSortingType.Size = New System.Drawing.Size(214, 24)
        Me.cbxSortingType.TabIndex = 33
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(4, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(138, 23)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Sorting Type"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxProductType
        '
        Me.cbxProductType.FormattingEnabled = True
        Me.cbxProductType.Location = New System.Drawing.Point(307, 92)
        Me.cbxProductType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductType.Name = "cbxProductType"
        Me.cbxProductType.Size = New System.Drawing.Size(214, 24)
        Me.cbxProductType.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(4, 92)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(138, 23)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "Product Type"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbWeekFrom
        '
        Me.tbWeekFrom.Location = New System.Drawing.Point(149, 65)
        Me.tbWeekFrom.Name = "tbWeekFrom"
        Me.tbWeekFrom.Size = New System.Drawing.Size(64, 23)
        Me.tbWeekFrom.TabIndex = 30
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(4, 65)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 23)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Week"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxYear
        '
        Me.cbxYear.FormattingEnabled = True
        Me.cbxYear.Location = New System.Drawing.Point(149, 38)
        Me.cbxYear.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxYear.Name = "cbxYear"
        Me.cbxYear.Size = New System.Drawing.Size(214, 24)
        Me.cbxYear.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 23)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Year"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Honeydew
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cbxShipmentStatus)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.cbxProductionStatus)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.tbArticleDescription)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.cbxArticleMould)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.cbxCustomer)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(413, 153)
        Me.Panel4.TabIndex = 14
        '
        'cbxShipmentStatus
        '
        Me.cbxShipmentStatus.FormattingEnabled = True
        Me.cbxShipmentStatus.Items.AddRange(New Object() {"ALL DATA", "ON TIME", "DELAYED"})
        Me.cbxShipmentStatus.Location = New System.Drawing.Point(145, 119)
        Me.cbxShipmentStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxShipmentStatus.Name = "cbxShipmentStatus"
        Me.cbxShipmentStatus.Size = New System.Drawing.Size(262, 24)
        Me.cbxShipmentStatus.TabIndex = 27
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(138, 23)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Shipment Status"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxProductionStatus
        '
        Me.cbxProductionStatus.FormattingEnabled = True
        Me.cbxProductionStatus.Items.AddRange(New Object() {"ALL DATA", "COMPLETED", "PENDING", "PENDING [ ]", "CLOSED", "REPLACEMENT ORDER"})
        Me.cbxProductionStatus.Location = New System.Drawing.Point(145, 92)
        Me.cbxProductionStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductionStatus.Name = "cbxProductionStatus"
        Me.cbxProductionStatus.Size = New System.Drawing.Size(262, 24)
        Me.cbxProductionStatus.TabIndex = 25
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Production Status"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbArticleDescription
        '
        Me.tbArticleDescription.Location = New System.Drawing.Point(145, 65)
        Me.tbArticleDescription.Name = "tbArticleDescription"
        Me.tbArticleDescription.Size = New System.Drawing.Size(262, 23)
        Me.tbArticleDescription.TabIndex = 24
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(4, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(138, 23)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Customer"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(4, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(138, 23)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Article Description"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxArticleMould
        '
        Me.cbxArticleMould.FormattingEnabled = True
        Me.cbxArticleMould.Location = New System.Drawing.Point(145, 38)
        Me.cbxArticleMould.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxArticleMould.Name = "cbxArticleMould"
        Me.cbxArticleMould.Size = New System.Drawing.Size(262, 24)
        Me.cbxArticleMould.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 23)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Article Mould"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(145, 11)
        Me.cbxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(262, 24)
        Me.cbxCustomer.TabIndex = 12
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Ivory
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.rbDeliveryDate)
        Me.Panel3.Controls.Add(Me.rbOrderDate)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.dpTo)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 153)
        Me.Panel3.TabIndex = 13
        '
        'rbDeliveryDate
        '
        Me.rbDeliveryDate.AutoSize = True
        Me.rbDeliveryDate.Location = New System.Drawing.Point(8, 112)
        Me.rbDeliveryDate.Margin = New System.Windows.Forms.Padding(4)
        Me.rbDeliveryDate.Name = "rbDeliveryDate"
        Me.rbDeliveryDate.Size = New System.Drawing.Size(180, 20)
        Me.rbDeliveryDate.TabIndex = 32
        Me.rbDeliveryDate.Text = "Expected Delivery Date"
        Me.rbDeliveryDate.UseVisualStyleBackColor = True
        '
        'rbOrderDate
        '
        Me.rbOrderDate.AutoSize = True
        Me.rbOrderDate.Checked = True
        Me.rbOrderDate.Location = New System.Drawing.Point(9, 74)
        Me.rbOrderDate.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOrderDate.Name = "rbOrderDate"
        Me.rbOrderDate.Size = New System.Drawing.Size(98, 20)
        Me.rbOrderDate.TabIndex = 31
        Me.rbOrderDate.TabStop = True
        Me.rbOrderDate.Text = "Order Date"
        Me.rbOrderDate.UseVisualStyleBackColor = True
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
        'plSalesOrdDtl
        '
        Me.plSalesOrdDtl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plSalesOrdDtl.BackColor = System.Drawing.Color.Bisque
        Me.plSalesOrdDtl.Controls.Add(Me.dpExpectedDlyDt)
        Me.plSalesOrdDtl.Controls.Add(Me.dpBuyerDlyDt)
        Me.plSalesOrdDtl.Controls.Add(Me.cbExitUpdateModule)
        Me.plSalesOrdDtl.Controls.Add(Me.pgbar)
        Me.plSalesOrdDtl.Controls.Add(Me.tbWeekNo)
        Me.plSalesOrdDtl.Controls.Add(Me.Label40)
        Me.plSalesOrdDtl.Controls.Add(Me.tbCustomerOrder)
        Me.plSalesOrdDtl.Controls.Add(Me.Label39)
        Me.plSalesOrdDtl.Controls.Add(Me.tbTotalQty)
        Me.plSalesOrdDtl.Controls.Add(Me.Label38)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty18)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS18)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty17)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS17)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty16)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS16)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty15)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS15)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty14)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS14)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty13)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS13)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty12)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS12)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty11)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS11)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty10)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS10)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty09)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS09)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty08)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS08)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty07)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS07)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty06)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS06)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty05)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS05)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty04)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS04)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty03)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS03)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty02)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS02)
        Me.plSalesOrdDtl.Controls.Add(Me.tbQty01)
        Me.plSalesOrdDtl.Controls.Add(Me.lblS01)
        Me.plSalesOrdDtl.Controls.Add(Me.Label19)
        Me.plSalesOrdDtl.Controls.Add(Me.Label18)
        Me.plSalesOrdDtl.Controls.Add(Me.tbArticleName)
        Me.plSalesOrdDtl.Controls.Add(Me.Label17)
        Me.plSalesOrdDtl.Controls.Add(Me.tbArticleCode)
        Me.plSalesOrdDtl.Controls.Add(Me.Label16)
        Me.plSalesOrdDtl.Controls.Add(Me.tbSalesOrderNo)
        Me.plSalesOrdDtl.Controls.Add(Me.Label15)
        Me.plSalesOrdDtl.Controls.Add(Me.cbUpdate)
        Me.plSalesOrdDtl.Location = New System.Drawing.Point(12, 187)
        Me.plSalesOrdDtl.Name = "plSalesOrdDtl"
        Me.plSalesOrdDtl.Size = New System.Drawing.Size(1121, 515)
        Me.plSalesOrdDtl.TabIndex = 21
        Me.plSalesOrdDtl.TabStop = False
        Me.plSalesOrdDtl.Text = "Sales Order Details Update Mode :-"
        Me.plSalesOrdDtl.Visible = False
        '
        'dpExpectedDlyDt
        '
        Me.dpExpectedDlyDt.CustomFormat = "dd-MMM-yyyy"
        Me.dpExpectedDlyDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpExpectedDlyDt.Location = New System.Drawing.Point(499, 62)
        Me.dpExpectedDlyDt.Margin = New System.Windows.Forms.Padding(4)
        Me.dpExpectedDlyDt.Name = "dpExpectedDlyDt"
        Me.dpExpectedDlyDt.Size = New System.Drawing.Size(139, 23)
        Me.dpExpectedDlyDt.TabIndex = 80
        '
        'dpBuyerDlyDt
        '
        Me.dpBuyerDlyDt.CustomFormat = "dd-MMM-yyyy"
        Me.dpBuyerDlyDt.Enabled = False
        Me.dpBuyerDlyDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpBuyerDlyDt.Location = New System.Drawing.Point(499, 30)
        Me.dpBuyerDlyDt.Margin = New System.Windows.Forms.Padding(4)
        Me.dpBuyerDlyDt.Name = "dpBuyerDlyDt"
        Me.dpBuyerDlyDt.Size = New System.Drawing.Size(139, 23)
        Me.dpBuyerDlyDt.TabIndex = 79
        '
        'cbExitUpdateModule
        '
        Me.cbExitUpdateModule.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExitUpdateModule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExitUpdateModule.Location = New System.Drawing.Point(1059, 432)
        Me.cbExitUpdateModule.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExitUpdateModule.Name = "cbExitUpdateModule"
        Me.cbExitUpdateModule.Size = New System.Drawing.Size(128, 74)
        Me.cbExitUpdateModule.TabIndex = 78
        Me.cbExitUpdateModule.Text = "Exit"
        Me.cbExitUpdateModule.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExitUpdateModule.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(532, 441)
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(250, 51)
        Me.pgbar.TabIndex = 15
        Me.pgbar.Visible = False
        '
        'tbWeekNo
        '
        Me.tbWeekNo.BackColor = System.Drawing.Color.White
        Me.tbWeekNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbWeekNo.ForeColor = System.Drawing.Color.Blue
        Me.tbWeekNo.Location = New System.Drawing.Point(495, 102)
        Me.tbWeekNo.Name = "tbWeekNo"
        Me.tbWeekNo.ReadOnly = True
        Me.tbWeekNo.Size = New System.Drawing.Size(71, 23)
        Me.tbWeekNo.TabIndex = 76
        '
        'Label40
        '
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Location = New System.Drawing.Point(354, 102)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(138, 23)
        Me.Label40.TabIndex = 75
        Me.Label40.Text = "Week No.:"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbCustomerOrder
        '
        Me.tbCustomerOrder.BackColor = System.Drawing.Color.White
        Me.tbCustomerOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbCustomerOrder.ForeColor = System.Drawing.Color.Blue
        Me.tbCustomerOrder.Location = New System.Drawing.Point(153, 121)
        Me.tbCustomerOrder.Name = "tbCustomerOrder"
        Me.tbCustomerOrder.ReadOnly = True
        Me.tbCustomerOrder.Size = New System.Drawing.Size(182, 23)
        Me.tbCustomerOrder.TabIndex = 74
        '
        'Label39
        '
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Location = New System.Drawing.Point(12, 121)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(138, 23)
        Me.Label39.TabIndex = 73
        Me.Label39.Text = "Customer Order No. :-"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbTotalQty
        '
        Me.tbTotalQty.BackColor = System.Drawing.Color.White
        Me.tbTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbTotalQty.ForeColor = System.Drawing.Color.Blue
        Me.tbTotalQty.Location = New System.Drawing.Point(538, 257)
        Me.tbTotalQty.Name = "tbTotalQty"
        Me.tbTotalQty.ReadOnly = True
        Me.tbTotalQty.Size = New System.Drawing.Size(50, 23)
        Me.tbTotalQty.TabIndex = 72
        Me.tbTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label38.Location = New System.Drawing.Point(538, 230)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(50, 23)
        Me.Label38.TabIndex = 71
        Me.Label38.Text = "Total"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty18
        '
        Me.tbQty18.BackColor = System.Drawing.Color.White
        Me.tbQty18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty18.ForeColor = System.Drawing.Color.Blue
        Me.tbQty18.Location = New System.Drawing.Point(473, 257)
        Me.tbQty18.Name = "tbQty18"
        Me.tbQty18.ReadOnly = True
        Me.tbQty18.Size = New System.Drawing.Size(50, 23)
        Me.tbQty18.TabIndex = 70
        Me.tbQty18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS18
        '
        Me.lblS18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS18.Location = New System.Drawing.Point(473, 230)
        Me.lblS18.Name = "lblS18"
        Me.lblS18.Size = New System.Drawing.Size(50, 23)
        Me.lblS18.TabIndex = 69
        Me.lblS18.Text = "S-01"
        Me.lblS18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty17
        '
        Me.tbQty17.BackColor = System.Drawing.Color.White
        Me.tbQty17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty17.ForeColor = System.Drawing.Color.Blue
        Me.tbQty17.Location = New System.Drawing.Point(417, 257)
        Me.tbQty17.Name = "tbQty17"
        Me.tbQty17.ReadOnly = True
        Me.tbQty17.Size = New System.Drawing.Size(50, 23)
        Me.tbQty17.TabIndex = 68
        Me.tbQty17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS17
        '
        Me.lblS17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS17.Location = New System.Drawing.Point(417, 230)
        Me.lblS17.Name = "lblS17"
        Me.lblS17.Size = New System.Drawing.Size(50, 23)
        Me.lblS17.TabIndex = 67
        Me.lblS17.Text = "S-01"
        Me.lblS17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty16
        '
        Me.tbQty16.BackColor = System.Drawing.Color.White
        Me.tbQty16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty16.ForeColor = System.Drawing.Color.Blue
        Me.tbQty16.Location = New System.Drawing.Point(361, 257)
        Me.tbQty16.Name = "tbQty16"
        Me.tbQty16.ReadOnly = True
        Me.tbQty16.Size = New System.Drawing.Size(50, 23)
        Me.tbQty16.TabIndex = 66
        Me.tbQty16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS16
        '
        Me.lblS16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS16.Location = New System.Drawing.Point(361, 230)
        Me.lblS16.Name = "lblS16"
        Me.lblS16.Size = New System.Drawing.Size(50, 23)
        Me.lblS16.TabIndex = 65
        Me.lblS16.Text = "S-01"
        Me.lblS16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty15
        '
        Me.tbQty15.BackColor = System.Drawing.Color.White
        Me.tbQty15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty15.ForeColor = System.Drawing.Color.Blue
        Me.tbQty15.Location = New System.Drawing.Point(305, 257)
        Me.tbQty15.Name = "tbQty15"
        Me.tbQty15.ReadOnly = True
        Me.tbQty15.Size = New System.Drawing.Size(50, 23)
        Me.tbQty15.TabIndex = 64
        Me.tbQty15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS15
        '
        Me.lblS15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS15.Location = New System.Drawing.Point(305, 230)
        Me.lblS15.Name = "lblS15"
        Me.lblS15.Size = New System.Drawing.Size(50, 23)
        Me.lblS15.TabIndex = 63
        Me.lblS15.Text = "S-01"
        Me.lblS15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty14
        '
        Me.tbQty14.BackColor = System.Drawing.Color.White
        Me.tbQty14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty14.ForeColor = System.Drawing.Color.Blue
        Me.tbQty14.Location = New System.Drawing.Point(249, 257)
        Me.tbQty14.Name = "tbQty14"
        Me.tbQty14.ReadOnly = True
        Me.tbQty14.Size = New System.Drawing.Size(50, 23)
        Me.tbQty14.TabIndex = 62
        Me.tbQty14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS14
        '
        Me.lblS14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS14.Location = New System.Drawing.Point(249, 230)
        Me.lblS14.Name = "lblS14"
        Me.lblS14.Size = New System.Drawing.Size(50, 23)
        Me.lblS14.TabIndex = 61
        Me.lblS14.Text = "S-01"
        Me.lblS14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty13
        '
        Me.tbQty13.BackColor = System.Drawing.Color.White
        Me.tbQty13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty13.ForeColor = System.Drawing.Color.Blue
        Me.tbQty13.Location = New System.Drawing.Point(187, 257)
        Me.tbQty13.Name = "tbQty13"
        Me.tbQty13.ReadOnly = True
        Me.tbQty13.Size = New System.Drawing.Size(50, 23)
        Me.tbQty13.TabIndex = 60
        Me.tbQty13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS13
        '
        Me.lblS13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS13.Location = New System.Drawing.Point(187, 230)
        Me.lblS13.Name = "lblS13"
        Me.lblS13.Size = New System.Drawing.Size(50, 23)
        Me.lblS13.TabIndex = 59
        Me.lblS13.Text = "S-01"
        Me.lblS13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty12
        '
        Me.tbQty12.BackColor = System.Drawing.Color.White
        Me.tbQty12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty12.ForeColor = System.Drawing.Color.Blue
        Me.tbQty12.Location = New System.Drawing.Point(126, 257)
        Me.tbQty12.Name = "tbQty12"
        Me.tbQty12.ReadOnly = True
        Me.tbQty12.Size = New System.Drawing.Size(50, 23)
        Me.tbQty12.TabIndex = 58
        Me.tbQty12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS12
        '
        Me.lblS12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS12.Location = New System.Drawing.Point(126, 230)
        Me.lblS12.Name = "lblS12"
        Me.lblS12.Size = New System.Drawing.Size(50, 23)
        Me.lblS12.TabIndex = 57
        Me.lblS12.Text = "S-01"
        Me.lblS12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty11
        '
        Me.tbQty11.BackColor = System.Drawing.Color.White
        Me.tbQty11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty11.ForeColor = System.Drawing.Color.Blue
        Me.tbQty11.Location = New System.Drawing.Point(70, 257)
        Me.tbQty11.Name = "tbQty11"
        Me.tbQty11.ReadOnly = True
        Me.tbQty11.Size = New System.Drawing.Size(50, 23)
        Me.tbQty11.TabIndex = 56
        Me.tbQty11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS11
        '
        Me.lblS11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS11.Location = New System.Drawing.Point(70, 230)
        Me.lblS11.Name = "lblS11"
        Me.lblS11.Size = New System.Drawing.Size(50, 23)
        Me.lblS11.TabIndex = 55
        Me.lblS11.Text = "S-01"
        Me.lblS11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty10
        '
        Me.tbQty10.BackColor = System.Drawing.Color.White
        Me.tbQty10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty10.ForeColor = System.Drawing.Color.Blue
        Me.tbQty10.Location = New System.Drawing.Point(12, 257)
        Me.tbQty10.Name = "tbQty10"
        Me.tbQty10.ReadOnly = True
        Me.tbQty10.Size = New System.Drawing.Size(50, 23)
        Me.tbQty10.TabIndex = 54
        Me.tbQty10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS10
        '
        Me.lblS10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS10.Location = New System.Drawing.Point(12, 230)
        Me.lblS10.Name = "lblS10"
        Me.lblS10.Size = New System.Drawing.Size(50, 23)
        Me.lblS10.TabIndex = 53
        Me.lblS10.Text = "S-01"
        Me.lblS10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty09
        '
        Me.tbQty09.BackColor = System.Drawing.Color.White
        Me.tbQty09.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty09.ForeColor = System.Drawing.Color.Blue
        Me.tbQty09.Location = New System.Drawing.Point(473, 199)
        Me.tbQty09.Name = "tbQty09"
        Me.tbQty09.ReadOnly = True
        Me.tbQty09.Size = New System.Drawing.Size(50, 23)
        Me.tbQty09.TabIndex = 52
        Me.tbQty09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS09
        '
        Me.lblS09.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS09.Location = New System.Drawing.Point(473, 172)
        Me.lblS09.Name = "lblS09"
        Me.lblS09.Size = New System.Drawing.Size(50, 23)
        Me.lblS09.TabIndex = 51
        Me.lblS09.Text = "S-01"
        Me.lblS09.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty08
        '
        Me.tbQty08.BackColor = System.Drawing.Color.White
        Me.tbQty08.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty08.ForeColor = System.Drawing.Color.Blue
        Me.tbQty08.Location = New System.Drawing.Point(417, 199)
        Me.tbQty08.Name = "tbQty08"
        Me.tbQty08.ReadOnly = True
        Me.tbQty08.Size = New System.Drawing.Size(50, 23)
        Me.tbQty08.TabIndex = 50
        Me.tbQty08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS08
        '
        Me.lblS08.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS08.Location = New System.Drawing.Point(417, 172)
        Me.lblS08.Name = "lblS08"
        Me.lblS08.Size = New System.Drawing.Size(50, 23)
        Me.lblS08.TabIndex = 49
        Me.lblS08.Text = "S-01"
        Me.lblS08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty07
        '
        Me.tbQty07.BackColor = System.Drawing.Color.White
        Me.tbQty07.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty07.ForeColor = System.Drawing.Color.Blue
        Me.tbQty07.Location = New System.Drawing.Point(361, 199)
        Me.tbQty07.Name = "tbQty07"
        Me.tbQty07.ReadOnly = True
        Me.tbQty07.Size = New System.Drawing.Size(50, 23)
        Me.tbQty07.TabIndex = 48
        Me.tbQty07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS07
        '
        Me.lblS07.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS07.Location = New System.Drawing.Point(361, 172)
        Me.lblS07.Name = "lblS07"
        Me.lblS07.Size = New System.Drawing.Size(50, 23)
        Me.lblS07.TabIndex = 47
        Me.lblS07.Text = "S-01"
        Me.lblS07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty06
        '
        Me.tbQty06.BackColor = System.Drawing.Color.White
        Me.tbQty06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty06.ForeColor = System.Drawing.Color.Blue
        Me.tbQty06.Location = New System.Drawing.Point(305, 199)
        Me.tbQty06.Name = "tbQty06"
        Me.tbQty06.ReadOnly = True
        Me.tbQty06.Size = New System.Drawing.Size(50, 23)
        Me.tbQty06.TabIndex = 46
        Me.tbQty06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS06
        '
        Me.lblS06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS06.Location = New System.Drawing.Point(305, 172)
        Me.lblS06.Name = "lblS06"
        Me.lblS06.Size = New System.Drawing.Size(50, 23)
        Me.lblS06.TabIndex = 45
        Me.lblS06.Text = "S-01"
        Me.lblS06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty05
        '
        Me.tbQty05.BackColor = System.Drawing.Color.White
        Me.tbQty05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty05.ForeColor = System.Drawing.Color.Blue
        Me.tbQty05.Location = New System.Drawing.Point(249, 199)
        Me.tbQty05.Name = "tbQty05"
        Me.tbQty05.ReadOnly = True
        Me.tbQty05.Size = New System.Drawing.Size(50, 23)
        Me.tbQty05.TabIndex = 44
        Me.tbQty05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS05
        '
        Me.lblS05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS05.Location = New System.Drawing.Point(249, 172)
        Me.lblS05.Name = "lblS05"
        Me.lblS05.Size = New System.Drawing.Size(50, 23)
        Me.lblS05.TabIndex = 43
        Me.lblS05.Text = "S-01"
        Me.lblS05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty04
        '
        Me.tbQty04.BackColor = System.Drawing.Color.White
        Me.tbQty04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty04.ForeColor = System.Drawing.Color.Blue
        Me.tbQty04.Location = New System.Drawing.Point(187, 199)
        Me.tbQty04.Name = "tbQty04"
        Me.tbQty04.ReadOnly = True
        Me.tbQty04.Size = New System.Drawing.Size(50, 23)
        Me.tbQty04.TabIndex = 42
        Me.tbQty04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS04
        '
        Me.lblS04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS04.Location = New System.Drawing.Point(187, 172)
        Me.lblS04.Name = "lblS04"
        Me.lblS04.Size = New System.Drawing.Size(50, 23)
        Me.lblS04.TabIndex = 41
        Me.lblS04.Text = "S-01"
        Me.lblS04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty03
        '
        Me.tbQty03.BackColor = System.Drawing.Color.White
        Me.tbQty03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty03.ForeColor = System.Drawing.Color.Blue
        Me.tbQty03.Location = New System.Drawing.Point(126, 199)
        Me.tbQty03.Name = "tbQty03"
        Me.tbQty03.ReadOnly = True
        Me.tbQty03.Size = New System.Drawing.Size(50, 23)
        Me.tbQty03.TabIndex = 40
        Me.tbQty03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS03
        '
        Me.lblS03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS03.Location = New System.Drawing.Point(126, 172)
        Me.lblS03.Name = "lblS03"
        Me.lblS03.Size = New System.Drawing.Size(50, 23)
        Me.lblS03.TabIndex = 39
        Me.lblS03.Text = "S-01"
        Me.lblS03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty02
        '
        Me.tbQty02.BackColor = System.Drawing.Color.White
        Me.tbQty02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty02.ForeColor = System.Drawing.Color.Blue
        Me.tbQty02.Location = New System.Drawing.Point(70, 199)
        Me.tbQty02.Name = "tbQty02"
        Me.tbQty02.ReadOnly = True
        Me.tbQty02.Size = New System.Drawing.Size(50, 23)
        Me.tbQty02.TabIndex = 38
        Me.tbQty02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS02
        '
        Me.lblS02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS02.Location = New System.Drawing.Point(70, 172)
        Me.lblS02.Name = "lblS02"
        Me.lblS02.Size = New System.Drawing.Size(50, 23)
        Me.lblS02.TabIndex = 37
        Me.lblS02.Text = "S-01"
        Me.lblS02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbQty01
        '
        Me.tbQty01.BackColor = System.Drawing.Color.White
        Me.tbQty01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbQty01.ForeColor = System.Drawing.Color.Blue
        Me.tbQty01.Location = New System.Drawing.Point(12, 199)
        Me.tbQty01.Name = "tbQty01"
        Me.tbQty01.ReadOnly = True
        Me.tbQty01.Size = New System.Drawing.Size(50, 23)
        Me.tbQty01.TabIndex = 36
        Me.tbQty01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblS01
        '
        Me.lblS01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS01.Location = New System.Drawing.Point(12, 172)
        Me.lblS01.Name = "lblS01"
        Me.lblS01.Size = New System.Drawing.Size(50, 23)
        Me.lblS01.TabIndex = 35
        Me.lblS01.Text = "S-01"
        Me.lblS01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Location = New System.Drawing.Point(354, 62)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(138, 23)
        Me.Label19.TabIndex = 33
        Me.Label19.Text = "Expected Delivery Dt :-"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Location = New System.Drawing.Point(354, 30)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(138, 23)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Buyer Delivery Dt:-"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbArticleName
        '
        Me.tbArticleName.BackColor = System.Drawing.Color.White
        Me.tbArticleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbArticleName.ForeColor = System.Drawing.Color.Blue
        Me.tbArticleName.Location = New System.Drawing.Point(153, 91)
        Me.tbArticleName.Name = "tbArticleName"
        Me.tbArticleName.ReadOnly = True
        Me.tbArticleName.Size = New System.Drawing.Size(182, 23)
        Me.tbArticleName.TabIndex = 30
        '
        'Label17
        '
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Location = New System.Drawing.Point(12, 91)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(138, 23)
        Me.Label17.TabIndex = 29
        Me.Label17.Text = "Article Name :-"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbArticleCode
        '
        Me.tbArticleCode.BackColor = System.Drawing.Color.White
        Me.tbArticleCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbArticleCode.ForeColor = System.Drawing.Color.Blue
        Me.tbArticleCode.Location = New System.Drawing.Point(153, 62)
        Me.tbArticleCode.Name = "tbArticleCode"
        Me.tbArticleCode.ReadOnly = True
        Me.tbArticleCode.Size = New System.Drawing.Size(182, 23)
        Me.tbArticleCode.TabIndex = 28
        '
        'Label16
        '
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Location = New System.Drawing.Point(12, 62)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(138, 23)
        Me.Label16.TabIndex = 27
        Me.Label16.Text = "Article Code :-"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbSalesOrderNo
        '
        Me.tbSalesOrderNo.BackColor = System.Drawing.Color.White
        Me.tbSalesOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbSalesOrderNo.ForeColor = System.Drawing.Color.Blue
        Me.tbSalesOrderNo.Location = New System.Drawing.Point(153, 30)
        Me.tbSalesOrderNo.Name = "tbSalesOrderNo"
        Me.tbSalesOrderNo.ReadOnly = True
        Me.tbSalesOrderNo.Size = New System.Drawing.Size(182, 23)
        Me.tbSalesOrderNo.TabIndex = 26
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Location = New System.Drawing.Point(12, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(138, 23)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Sales Order No.:-"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbUpdate
        '
        Me.cbUpdate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbUpdate.Location = New System.Drawing.Point(33, 378)
        Me.cbUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.cbUpdate.Name = "cbUpdate"
        Me.cbUpdate.Size = New System.Drawing.Size(128, 74)
        Me.cbUpdate.TabIndex = 77
        Me.cbUpdate.Text = "Update"
        Me.cbUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbUpdate.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.Label22)
        Me.Panel6.Controls.Add(Me.Label21)
        Me.Panel6.Controls.Add(Me.Label20)
        Me.Panel6.Location = New System.Drawing.Point(382, 11)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(139, 58)
        Me.Panel6.TabIndex = 83
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.Red
        Me.Label22.Location = New System.Drawing.Point(3, 40)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 16)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "RED => Closed"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label21.Location = New System.Drawing.Point(3, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(123, 16)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "ORANGE => Open"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label20.Location = New System.Drawing.Point(3, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(120, 16)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "BLUE => Shipped"
        '
        'frmAOrderPlanning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAOrderPlanning"
        Me.Text = "frmOrderPlanning"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdSalesOrderDetailsWKSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetailsWKSummaryV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.plSalesOrdDtl.ResumeLayout(False)
        Me.plSalesOrdDtl.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
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
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents tbArticleDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbxArticleMould As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cbxShipmentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbxProductionStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbWeekTo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbxSortingType As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cbxProductType As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbWeekFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbxYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rbNo As System.Windows.Forms.RadioButton
    Friend WithEvents rbYes As System.Windows.Forms.RadioButton
    Friend WithEvents rbDeliveryDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrderDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents grdSalesOrderDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdSalesOrderDetailsWKSummary As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderDetailsWKSummaryV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbOpenOrder As System.Windows.Forms.Button
    Friend WithEvents plSalesOrdDtl As System.Windows.Forms.GroupBox
    Friend WithEvents tbQty01 As System.Windows.Forms.TextBox
    Friend WithEvents lblS01 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents tbArticleName As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tbArticleCode As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tbSalesOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbExitUpdateModule As System.Windows.Forms.Button
    Friend WithEvents cbUpdate As System.Windows.Forms.Button
    Friend WithEvents tbWeekNo As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents tbCustomerOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents tbTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents tbQty18 As System.Windows.Forms.TextBox
    Friend WithEvents lblS18 As System.Windows.Forms.Label
    Friend WithEvents tbQty17 As System.Windows.Forms.TextBox
    Friend WithEvents lblS17 As System.Windows.Forms.Label
    Friend WithEvents tbQty16 As System.Windows.Forms.TextBox
    Friend WithEvents lblS16 As System.Windows.Forms.Label
    Friend WithEvents tbQty15 As System.Windows.Forms.TextBox
    Friend WithEvents lblS15 As System.Windows.Forms.Label
    Friend WithEvents tbQty14 As System.Windows.Forms.TextBox
    Friend WithEvents lblS14 As System.Windows.Forms.Label
    Friend WithEvents tbQty13 As System.Windows.Forms.TextBox
    Friend WithEvents lblS13 As System.Windows.Forms.Label
    Friend WithEvents tbQty12 As System.Windows.Forms.TextBox
    Friend WithEvents lblS12 As System.Windows.Forms.Label
    Friend WithEvents tbQty11 As System.Windows.Forms.TextBox
    Friend WithEvents lblS11 As System.Windows.Forms.Label
    Friend WithEvents tbQty10 As System.Windows.Forms.TextBox
    Friend WithEvents lblS10 As System.Windows.Forms.Label
    Friend WithEvents tbQty09 As System.Windows.Forms.TextBox
    Friend WithEvents lblS09 As System.Windows.Forms.Label
    Friend WithEvents tbQty08 As System.Windows.Forms.TextBox
    Friend WithEvents lblS08 As System.Windows.Forms.Label
    Friend WithEvents tbQty07 As System.Windows.Forms.TextBox
    Friend WithEvents lblS07 As System.Windows.Forms.Label
    Friend WithEvents tbQty06 As System.Windows.Forms.TextBox
    Friend WithEvents lblS06 As System.Windows.Forms.Label
    Friend WithEvents tbQty05 As System.Windows.Forms.TextBox
    Friend WithEvents lblS05 As System.Windows.Forms.Label
    Friend WithEvents tbQty04 As System.Windows.Forms.TextBox
    Friend WithEvents lblS04 As System.Windows.Forms.Label
    Friend WithEvents tbQty03 As System.Windows.Forms.TextBox
    Friend WithEvents lblS03 As System.Windows.Forms.Label
    Friend WithEvents tbQty02 As System.Windows.Forms.TextBox
    Friend WithEvents lblS02 As System.Windows.Forms.Label
    Friend WithEvents dpExpectedDlyDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpBuyerDlyDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbxProductTypeMain As System.Windows.Forms.ComboBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
