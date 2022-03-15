<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllinOne1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAllinOne1))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl
        Me.SalesOrderBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsSalesOrder = New SolarERPForSGM.dsSalesOrder
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSalesOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerBuy = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerGroupCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerCountry = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDestination = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerAccountCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colAgentCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colAgentName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCommissionPercentage = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderRecivedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderConfirmedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderQuality = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderStatus = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSeason = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShipper = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticle = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colVariant = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleGroup = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerArticleGroup = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colUnit = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCurrency = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCurrencyConversion = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colIsAssortedOrder = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTotalOrderQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTotalOrderValue = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTotalShippedQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTotalShippedValue = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTotalCancelQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTotalShortShippedQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDiscountPercentage = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDiscountValue = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModeOfShipment = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colRevisedModeOfShipment = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShipmentNature = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colRevisedShipmentNature = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModeOfPayment = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colRevisedModeOfPayment = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPictogram = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMadeInIndia = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShoeBoxBrand = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShoeBrand = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShoeBoxBarCodeLabels = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShoeBarCodeLabels = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colAdhesiveTape = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colParticularInstruction = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colStampingInformation = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colAdditionInformation = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLCDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLCStartDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLCNegotiationDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLCDiscount = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLCDetails = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colRSNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCartingDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colClaims = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colActionCalendarType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colActionCalendar = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colIsApproved = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colApprovedBy = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colApprovedOn = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModuleName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCreatedBy = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCreatedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModifiedBy = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModifiedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colEnteredOnMachineID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colExeVersionNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colShipperID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLCNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colUserCategory = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSalesOrderDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderSerialNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerOrderType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colStampingInformation1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colRemarks1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleColor = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderWeek = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colRate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLogo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerOrderQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDeliveryTo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colFinancialYear = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCustomerRefNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSizeName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSpecialCustomerCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPortOfDischarge = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colIsSampleOrder = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colisManualClosed = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCommitmentOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colForecastOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSalesOrderType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDeliveryLocation = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPIDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colChequeNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colGenerateProcessOrder = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colInternalSalesOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colInternalBuyer = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerOrderID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colIsDemandDirty = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colHOId = New DevExpress.XtraGrid.Columns.GridColumn
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.tbArticleDescription = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.tbArticleCode = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.rbDetailed = New System.Windows.Forms.RadioButton
        Me.rbSummary = New System.Windows.Forms.RadioButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbxArticleName = New System.Windows.Forms.ComboBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rbPending = New System.Windows.Forms.RadioButton
        Me.rbCompleted = New System.Windows.Forms.RadioButton
        Me.rbAll = New System.Windows.Forms.RadioButton
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbxTypeofOrder = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbxTypeofDocument = New System.Windows.Forms.ComboBox
        Me.rbPurchase = New System.Windows.Forms.RadioButton
        Me.rbSales = New System.Windows.Forms.RadioButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.grdPurchaseInvoices = New DevExpress.XtraGrid.GridControl
        Me.grdPurchaseInvoicesV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdPurchaseOrders = New DevExpress.XtraGrid.GridControl
        Me.grdPurchaseOrdersV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdSalesOrders = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrdersV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdSalesInvoices = New DevExpress.XtraGrid.GridControl
        Me.grdSalesInvoicesV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.SalesOrderTableAdapter = New SolarERPForSGM.dsSalesOrderTableAdapters.SalesOrderTableAdapter
        Me.SalesOrderDetailsTableAdapter1 = New SolarERPForSGM.dsSalesOrderTableAdapters.SalesOrderDetailsTableAdapter
        Me.Panel1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SalesOrderBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSalesOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.grdPurchaseInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseInvoicesV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseOrdersV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrdersV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesInvoicesV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GridControl1)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.grdPurchaseInvoices)
        Me.Panel1.Controls.Add(Me.grdPurchaseOrders)
        Me.Panel1.Controls.Add(Me.grdSalesOrders)
        Me.Panel1.Controls.Add(Me.grdSalesInvoices)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'GridControl1
        '
        Me.GridControl1.DataSource = Me.SalesOrderBindingSource
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Location = New System.Drawing.Point(10, 171)
        Me.GridControl1.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.GridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.GridControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(1182, 393)
        Me.GridControl1.TabIndex = 13
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'SalesOrderBindingSource
        '
        Me.SalesOrderBindingSource.DataMember = "SalesOrder"
        Me.SalesOrderBindingSource.DataSource = Me.DsSalesOrder
        '
        'DsSalesOrder
        '
        Me.DsSalesOrder.DataSetName = "dsSalesOrder"
        Me.DsSalesOrder.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.colSalesOrderNo, Me.colBuyerBuy, Me.colBuyerOrderNo, Me.colBuyerCode, Me.colBuyerName, Me.colBuyerGroupCode, Me.colBuyerCountry, Me.colDestination, Me.colBuyerAccountCode, Me.colAgentCode, Me.colAgentName, Me.colCommissionPercentage, Me.colOrderRecivedDate, Me.colOrderConfirmedDate, Me.colOrderType, Me.colOrderQuality, Me.colOrderStatus, Me.colSeason, Me.colShipper, Me.colArticleID, Me.colArticle, Me.colVariant, Me.colArticleName, Me.colArticleGroup, Me.colBuyerArticleGroup, Me.colUnit, Me.colCurrency, Me.colCurrencyConversion, Me.colIsAssortedOrder, Me.colTotalOrderQuantity, Me.colTotalOrderValue, Me.colTotalShippedQuantity, Me.colTotalShippedValue, Me.colTotalCancelQuantity, Me.colTotalShortShippedQuantity, Me.colDiscountPercentage, Me.colDiscountValue, Me.colModeOfShipment, Me.colRevisedModeOfShipment, Me.colShipmentNature, Me.colRevisedShipmentNature, Me.colModeOfPayment, Me.colRevisedModeOfPayment, Me.colPictogram, Me.colMadeInIndia, Me.colShoeBoxBrand, Me.colShoeBrand, Me.colShoeBoxBarCodeLabels, Me.colShoeBarCodeLabels, Me.colAdhesiveTape, Me.colParticularInstruction, Me.colStampingInformation, Me.colAdditionInformation, Me.colLCDate, Me.colLCStartDate, Me.colLCNegotiationDate, Me.colLCDiscount, Me.colLCDetails, Me.colRSNo, Me.colCartingDate, Me.colClaims, Me.colActionCalendarType, Me.colActionCalendar, Me.colIsApproved, Me.colApprovedBy, Me.colApprovedOn, Me.colModuleName, Me.colCreatedBy, Me.colCreatedDate, Me.colModifiedBy, Me.colModifiedDate, Me.colEnteredOnMachineID, Me.colExeVersionNo, Me.colBuyerID, Me.colShipperID, Me.colLCNo, Me.colUserCategory, Me.colSalesOrderDate, Me.colOrderSerialNo, Me.colBuyerOrderType, Me.colOrderNo, Me.colStampingInformation1, Me.colRemarks1, Me.colArticleColor, Me.colArticleDescription, Me.colOrderWeek, Me.colRate, Me.colLogo, Me.colBuyerOrderQuantity, Me.colDeliveryTo, Me.colFinancialYear, Me.colCustomerRefNo, Me.colSizeName, Me.colSpecialCustomerCode, Me.colPortOfDischarge, Me.colIsSampleOrder, Me.colisManualClosed, Me.colCommitmentOrderNo, Me.colForecastOrderNo, Me.colBuyerDeliveryDate, Me.colSalesOrderType, Me.colDeliveryLocation, Me.colPIDate, Me.colChequeNo, Me.colGenerateProcessOrder, Me.colInternalSalesOrderNo, Me.colInternalBuyer, Me.colType, Me.colBuyerOrderID, Me.colIsDemandDirty, Me.colHOId})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsPrint.PrintDetails = True
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'colID
        '
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        Me.colID.Visible = True
        Me.colID.VisibleIndex = 0
        '
        'colSalesOrderNo
        '
        Me.colSalesOrderNo.FieldName = "SalesOrderNo"
        Me.colSalesOrderNo.Name = "colSalesOrderNo"
        Me.colSalesOrderNo.Visible = True
        Me.colSalesOrderNo.VisibleIndex = 1
        '
        'colBuyerBuy
        '
        Me.colBuyerBuy.FieldName = "BuyerBuy"
        Me.colBuyerBuy.Name = "colBuyerBuy"
        Me.colBuyerBuy.Visible = True
        Me.colBuyerBuy.VisibleIndex = 2
        '
        'colBuyerOrderNo
        '
        Me.colBuyerOrderNo.FieldName = "BuyerOrderNo"
        Me.colBuyerOrderNo.Name = "colBuyerOrderNo"
        Me.colBuyerOrderNo.Visible = True
        Me.colBuyerOrderNo.VisibleIndex = 3
        '
        'colBuyerCode
        '
        Me.colBuyerCode.FieldName = "BuyerCode"
        Me.colBuyerCode.Name = "colBuyerCode"
        Me.colBuyerCode.Visible = True
        Me.colBuyerCode.VisibleIndex = 4
        '
        'colBuyerName
        '
        Me.colBuyerName.FieldName = "BuyerName"
        Me.colBuyerName.Name = "colBuyerName"
        Me.colBuyerName.Visible = True
        Me.colBuyerName.VisibleIndex = 5
        '
        'colBuyerGroupCode
        '
        Me.colBuyerGroupCode.FieldName = "BuyerGroupCode"
        Me.colBuyerGroupCode.Name = "colBuyerGroupCode"
        Me.colBuyerGroupCode.Visible = True
        Me.colBuyerGroupCode.VisibleIndex = 6
        '
        'colBuyerCountry
        '
        Me.colBuyerCountry.FieldName = "BuyerCountry"
        Me.colBuyerCountry.Name = "colBuyerCountry"
        Me.colBuyerCountry.Visible = True
        Me.colBuyerCountry.VisibleIndex = 7
        '
        'colDestination
        '
        Me.colDestination.FieldName = "Destination"
        Me.colDestination.Name = "colDestination"
        Me.colDestination.Visible = True
        Me.colDestination.VisibleIndex = 8
        '
        'colBuyerAccountCode
        '
        Me.colBuyerAccountCode.FieldName = "BuyerAccountCode"
        Me.colBuyerAccountCode.Name = "colBuyerAccountCode"
        Me.colBuyerAccountCode.Visible = True
        Me.colBuyerAccountCode.VisibleIndex = 9
        '
        'colAgentCode
        '
        Me.colAgentCode.FieldName = "AgentCode"
        Me.colAgentCode.Name = "colAgentCode"
        Me.colAgentCode.Visible = True
        Me.colAgentCode.VisibleIndex = 10
        '
        'colAgentName
        '
        Me.colAgentName.FieldName = "AgentName"
        Me.colAgentName.Name = "colAgentName"
        Me.colAgentName.Visible = True
        Me.colAgentName.VisibleIndex = 11
        '
        'colCommissionPercentage
        '
        Me.colCommissionPercentage.FieldName = "CommissionPercentage"
        Me.colCommissionPercentage.Name = "colCommissionPercentage"
        Me.colCommissionPercentage.Visible = True
        Me.colCommissionPercentage.VisibleIndex = 12
        '
        'colOrderRecivedDate
        '
        Me.colOrderRecivedDate.FieldName = "OrderRecivedDate"
        Me.colOrderRecivedDate.Name = "colOrderRecivedDate"
        Me.colOrderRecivedDate.Visible = True
        Me.colOrderRecivedDate.VisibleIndex = 13
        '
        'colOrderConfirmedDate
        '
        Me.colOrderConfirmedDate.FieldName = "OrderConfirmedDate"
        Me.colOrderConfirmedDate.Name = "colOrderConfirmedDate"
        Me.colOrderConfirmedDate.Visible = True
        Me.colOrderConfirmedDate.VisibleIndex = 14
        '
        'colOrderType
        '
        Me.colOrderType.FieldName = "OrderType"
        Me.colOrderType.Name = "colOrderType"
        Me.colOrderType.Visible = True
        Me.colOrderType.VisibleIndex = 15
        '
        'colOrderQuality
        '
        Me.colOrderQuality.FieldName = "OrderQuality"
        Me.colOrderQuality.Name = "colOrderQuality"
        Me.colOrderQuality.Visible = True
        Me.colOrderQuality.VisibleIndex = 16
        '
        'colOrderStatus
        '
        Me.colOrderStatus.FieldName = "OrderStatus"
        Me.colOrderStatus.Name = "colOrderStatus"
        Me.colOrderStatus.Visible = True
        Me.colOrderStatus.VisibleIndex = 17
        '
        'colSeason
        '
        Me.colSeason.FieldName = "Season"
        Me.colSeason.Name = "colSeason"
        Me.colSeason.Visible = True
        Me.colSeason.VisibleIndex = 18
        '
        'colShipper
        '
        Me.colShipper.FieldName = "Shipper"
        Me.colShipper.Name = "colShipper"
        Me.colShipper.Visible = True
        Me.colShipper.VisibleIndex = 19
        '
        'colArticleID
        '
        Me.colArticleID.FieldName = "ArticleID"
        Me.colArticleID.Name = "colArticleID"
        Me.colArticleID.Visible = True
        Me.colArticleID.VisibleIndex = 20
        '
        'colArticle
        '
        Me.colArticle.FieldName = "Article"
        Me.colArticle.Name = "colArticle"
        Me.colArticle.Visible = True
        Me.colArticle.VisibleIndex = 21
        '
        'colVariant
        '
        Me.colVariant.FieldName = "Variant"
        Me.colVariant.Name = "colVariant"
        Me.colVariant.Visible = True
        Me.colVariant.VisibleIndex = 22
        '
        'colArticleName
        '
        Me.colArticleName.FieldName = "ArticleName"
        Me.colArticleName.Name = "colArticleName"
        Me.colArticleName.Visible = True
        Me.colArticleName.VisibleIndex = 23
        '
        'colArticleGroup
        '
        Me.colArticleGroup.FieldName = "ArticleGroup"
        Me.colArticleGroup.Name = "colArticleGroup"
        Me.colArticleGroup.Visible = True
        Me.colArticleGroup.VisibleIndex = 24
        '
        'colBuyerArticleGroup
        '
        Me.colBuyerArticleGroup.FieldName = "BuyerArticleGroup"
        Me.colBuyerArticleGroup.Name = "colBuyerArticleGroup"
        Me.colBuyerArticleGroup.Visible = True
        Me.colBuyerArticleGroup.VisibleIndex = 25
        '
        'colUnit
        '
        Me.colUnit.FieldName = "Unit"
        Me.colUnit.Name = "colUnit"
        Me.colUnit.Visible = True
        Me.colUnit.VisibleIndex = 26
        '
        'colCurrency
        '
        Me.colCurrency.FieldName = "Currency"
        Me.colCurrency.Name = "colCurrency"
        Me.colCurrency.Visible = True
        Me.colCurrency.VisibleIndex = 27
        '
        'colCurrencyConversion
        '
        Me.colCurrencyConversion.FieldName = "CurrencyConversion"
        Me.colCurrencyConversion.Name = "colCurrencyConversion"
        Me.colCurrencyConversion.Visible = True
        Me.colCurrencyConversion.VisibleIndex = 28
        '
        'colIsAssortedOrder
        '
        Me.colIsAssortedOrder.FieldName = "IsAssortedOrder"
        Me.colIsAssortedOrder.Name = "colIsAssortedOrder"
        Me.colIsAssortedOrder.Visible = True
        Me.colIsAssortedOrder.VisibleIndex = 29
        '
        'colTotalOrderQuantity
        '
        Me.colTotalOrderQuantity.FieldName = "TotalOrderQuantity"
        Me.colTotalOrderQuantity.Name = "colTotalOrderQuantity"
        Me.colTotalOrderQuantity.Visible = True
        Me.colTotalOrderQuantity.VisibleIndex = 30
        '
        'colTotalOrderValue
        '
        Me.colTotalOrderValue.FieldName = "TotalOrderValue"
        Me.colTotalOrderValue.Name = "colTotalOrderValue"
        Me.colTotalOrderValue.Visible = True
        Me.colTotalOrderValue.VisibleIndex = 31
        '
        'colTotalShippedQuantity
        '
        Me.colTotalShippedQuantity.FieldName = "TotalShippedQuantity"
        Me.colTotalShippedQuantity.Name = "colTotalShippedQuantity"
        Me.colTotalShippedQuantity.Visible = True
        Me.colTotalShippedQuantity.VisibleIndex = 32
        '
        'colTotalShippedValue
        '
        Me.colTotalShippedValue.FieldName = "TotalShippedValue"
        Me.colTotalShippedValue.Name = "colTotalShippedValue"
        Me.colTotalShippedValue.Visible = True
        Me.colTotalShippedValue.VisibleIndex = 33
        '
        'colTotalCancelQuantity
        '
        Me.colTotalCancelQuantity.FieldName = "TotalCancelQuantity"
        Me.colTotalCancelQuantity.Name = "colTotalCancelQuantity"
        Me.colTotalCancelQuantity.Visible = True
        Me.colTotalCancelQuantity.VisibleIndex = 34
        '
        'colTotalShortShippedQuantity
        '
        Me.colTotalShortShippedQuantity.FieldName = "TotalShortShippedQuantity"
        Me.colTotalShortShippedQuantity.Name = "colTotalShortShippedQuantity"
        Me.colTotalShortShippedQuantity.Visible = True
        Me.colTotalShortShippedQuantity.VisibleIndex = 35
        '
        'colDiscountPercentage
        '
        Me.colDiscountPercentage.FieldName = "DiscountPercentage"
        Me.colDiscountPercentage.Name = "colDiscountPercentage"
        Me.colDiscountPercentage.Visible = True
        Me.colDiscountPercentage.VisibleIndex = 36
        '
        'colDiscountValue
        '
        Me.colDiscountValue.FieldName = "DiscountValue"
        Me.colDiscountValue.Name = "colDiscountValue"
        Me.colDiscountValue.Visible = True
        Me.colDiscountValue.VisibleIndex = 37
        '
        'colModeOfShipment
        '
        Me.colModeOfShipment.FieldName = "ModeOfShipment"
        Me.colModeOfShipment.Name = "colModeOfShipment"
        Me.colModeOfShipment.Visible = True
        Me.colModeOfShipment.VisibleIndex = 38
        '
        'colRevisedModeOfShipment
        '
        Me.colRevisedModeOfShipment.FieldName = "RevisedModeOfShipment"
        Me.colRevisedModeOfShipment.Name = "colRevisedModeOfShipment"
        Me.colRevisedModeOfShipment.Visible = True
        Me.colRevisedModeOfShipment.VisibleIndex = 39
        '
        'colShipmentNature
        '
        Me.colShipmentNature.FieldName = "ShipmentNature"
        Me.colShipmentNature.Name = "colShipmentNature"
        Me.colShipmentNature.Visible = True
        Me.colShipmentNature.VisibleIndex = 40
        '
        'colRevisedShipmentNature
        '
        Me.colRevisedShipmentNature.FieldName = "RevisedShipmentNature"
        Me.colRevisedShipmentNature.Name = "colRevisedShipmentNature"
        Me.colRevisedShipmentNature.Visible = True
        Me.colRevisedShipmentNature.VisibleIndex = 41
        '
        'colModeOfPayment
        '
        Me.colModeOfPayment.FieldName = "ModeOfPayment"
        Me.colModeOfPayment.Name = "colModeOfPayment"
        Me.colModeOfPayment.Visible = True
        Me.colModeOfPayment.VisibleIndex = 42
        '
        'colRevisedModeOfPayment
        '
        Me.colRevisedModeOfPayment.FieldName = "RevisedModeOfPayment"
        Me.colRevisedModeOfPayment.Name = "colRevisedModeOfPayment"
        Me.colRevisedModeOfPayment.Visible = True
        Me.colRevisedModeOfPayment.VisibleIndex = 43
        '
        'colPictogram
        '
        Me.colPictogram.FieldName = "Pictogram"
        Me.colPictogram.Name = "colPictogram"
        Me.colPictogram.Visible = True
        Me.colPictogram.VisibleIndex = 44
        '
        'colMadeInIndia
        '
        Me.colMadeInIndia.FieldName = "MadeInIndia"
        Me.colMadeInIndia.Name = "colMadeInIndia"
        Me.colMadeInIndia.Visible = True
        Me.colMadeInIndia.VisibleIndex = 45
        '
        'colShoeBoxBrand
        '
        Me.colShoeBoxBrand.FieldName = "ShoeBoxBrand"
        Me.colShoeBoxBrand.Name = "colShoeBoxBrand"
        Me.colShoeBoxBrand.Visible = True
        Me.colShoeBoxBrand.VisibleIndex = 46
        '
        'colShoeBrand
        '
        Me.colShoeBrand.FieldName = "ShoeBrand"
        Me.colShoeBrand.Name = "colShoeBrand"
        Me.colShoeBrand.Visible = True
        Me.colShoeBrand.VisibleIndex = 47
        '
        'colShoeBoxBarCodeLabels
        '
        Me.colShoeBoxBarCodeLabels.FieldName = "ShoeBoxBarCodeLabels"
        Me.colShoeBoxBarCodeLabels.Name = "colShoeBoxBarCodeLabels"
        Me.colShoeBoxBarCodeLabels.Visible = True
        Me.colShoeBoxBarCodeLabels.VisibleIndex = 48
        '
        'colShoeBarCodeLabels
        '
        Me.colShoeBarCodeLabels.FieldName = "ShoeBarCodeLabels"
        Me.colShoeBarCodeLabels.Name = "colShoeBarCodeLabels"
        Me.colShoeBarCodeLabels.Visible = True
        Me.colShoeBarCodeLabels.VisibleIndex = 49
        '
        'colAdhesiveTape
        '
        Me.colAdhesiveTape.FieldName = "AdhesiveTape"
        Me.colAdhesiveTape.Name = "colAdhesiveTape"
        Me.colAdhesiveTape.Visible = True
        Me.colAdhesiveTape.VisibleIndex = 50
        '
        'colParticularInstruction
        '
        Me.colParticularInstruction.FieldName = "ParticularInstruction"
        Me.colParticularInstruction.Name = "colParticularInstruction"
        Me.colParticularInstruction.Visible = True
        Me.colParticularInstruction.VisibleIndex = 51
        '
        'colStampingInformation
        '
        Me.colStampingInformation.FieldName = "StampingInformation"
        Me.colStampingInformation.Name = "colStampingInformation"
        Me.colStampingInformation.Visible = True
        Me.colStampingInformation.VisibleIndex = 52
        '
        'colAdditionInformation
        '
        Me.colAdditionInformation.FieldName = "AdditionInformation"
        Me.colAdditionInformation.Name = "colAdditionInformation"
        Me.colAdditionInformation.Visible = True
        Me.colAdditionInformation.VisibleIndex = 53
        '
        'colLCDate
        '
        Me.colLCDate.FieldName = "LCDate"
        Me.colLCDate.Name = "colLCDate"
        Me.colLCDate.Visible = True
        Me.colLCDate.VisibleIndex = 54
        '
        'colLCStartDate
        '
        Me.colLCStartDate.FieldName = "LCStartDate"
        Me.colLCStartDate.Name = "colLCStartDate"
        Me.colLCStartDate.Visible = True
        Me.colLCStartDate.VisibleIndex = 55
        '
        'colLCNegotiationDate
        '
        Me.colLCNegotiationDate.FieldName = "LCNegotiationDate"
        Me.colLCNegotiationDate.Name = "colLCNegotiationDate"
        Me.colLCNegotiationDate.Visible = True
        Me.colLCNegotiationDate.VisibleIndex = 56
        '
        'colLCDiscount
        '
        Me.colLCDiscount.FieldName = "LCDiscount"
        Me.colLCDiscount.Name = "colLCDiscount"
        Me.colLCDiscount.Visible = True
        Me.colLCDiscount.VisibleIndex = 57
        '
        'colLCDetails
        '
        Me.colLCDetails.FieldName = "LCDetails"
        Me.colLCDetails.Name = "colLCDetails"
        Me.colLCDetails.Visible = True
        Me.colLCDetails.VisibleIndex = 58
        '
        'colRSNo
        '
        Me.colRSNo.FieldName = "RSNo"
        Me.colRSNo.Name = "colRSNo"
        Me.colRSNo.Visible = True
        Me.colRSNo.VisibleIndex = 59
        '
        'colCartingDate
        '
        Me.colCartingDate.FieldName = "CartingDate"
        Me.colCartingDate.Name = "colCartingDate"
        Me.colCartingDate.Visible = True
        Me.colCartingDate.VisibleIndex = 60
        '
        'colClaims
        '
        Me.colClaims.FieldName = "Claims"
        Me.colClaims.Name = "colClaims"
        Me.colClaims.Visible = True
        Me.colClaims.VisibleIndex = 61
        '
        'colActionCalendarType
        '
        Me.colActionCalendarType.FieldName = "ActionCalendarType"
        Me.colActionCalendarType.Name = "colActionCalendarType"
        Me.colActionCalendarType.Visible = True
        Me.colActionCalendarType.VisibleIndex = 62
        '
        'colActionCalendar
        '
        Me.colActionCalendar.FieldName = "ActionCalendar"
        Me.colActionCalendar.Name = "colActionCalendar"
        Me.colActionCalendar.Visible = True
        Me.colActionCalendar.VisibleIndex = 63
        '
        'colIsApproved
        '
        Me.colIsApproved.FieldName = "IsApproved"
        Me.colIsApproved.Name = "colIsApproved"
        Me.colIsApproved.Visible = True
        Me.colIsApproved.VisibleIndex = 64
        '
        'colApprovedBy
        '
        Me.colApprovedBy.FieldName = "ApprovedBy"
        Me.colApprovedBy.Name = "colApprovedBy"
        Me.colApprovedBy.Visible = True
        Me.colApprovedBy.VisibleIndex = 65
        '
        'colApprovedOn
        '
        Me.colApprovedOn.FieldName = "ApprovedOn"
        Me.colApprovedOn.Name = "colApprovedOn"
        Me.colApprovedOn.Visible = True
        Me.colApprovedOn.VisibleIndex = 66
        '
        'colModuleName
        '
        Me.colModuleName.FieldName = "ModuleName"
        Me.colModuleName.Name = "colModuleName"
        Me.colModuleName.Visible = True
        Me.colModuleName.VisibleIndex = 67
        '
        'colCreatedBy
        '
        Me.colCreatedBy.FieldName = "CreatedBy"
        Me.colCreatedBy.Name = "colCreatedBy"
        Me.colCreatedBy.Visible = True
        Me.colCreatedBy.VisibleIndex = 68
        '
        'colCreatedDate
        '
        Me.colCreatedDate.FieldName = "CreatedDate"
        Me.colCreatedDate.Name = "colCreatedDate"
        Me.colCreatedDate.Visible = True
        Me.colCreatedDate.VisibleIndex = 69
        '
        'colModifiedBy
        '
        Me.colModifiedBy.FieldName = "ModifiedBy"
        Me.colModifiedBy.Name = "colModifiedBy"
        Me.colModifiedBy.Visible = True
        Me.colModifiedBy.VisibleIndex = 70
        '
        'colModifiedDate
        '
        Me.colModifiedDate.FieldName = "ModifiedDate"
        Me.colModifiedDate.Name = "colModifiedDate"
        Me.colModifiedDate.Visible = True
        Me.colModifiedDate.VisibleIndex = 71
        '
        'colEnteredOnMachineID
        '
        Me.colEnteredOnMachineID.FieldName = "EnteredOnMachineID"
        Me.colEnteredOnMachineID.Name = "colEnteredOnMachineID"
        Me.colEnteredOnMachineID.Visible = True
        Me.colEnteredOnMachineID.VisibleIndex = 72
        '
        'colExeVersionNo
        '
        Me.colExeVersionNo.FieldName = "ExeVersionNo"
        Me.colExeVersionNo.Name = "colExeVersionNo"
        Me.colExeVersionNo.Visible = True
        Me.colExeVersionNo.VisibleIndex = 73
        '
        'colBuyerID
        '
        Me.colBuyerID.FieldName = "BuyerID"
        Me.colBuyerID.Name = "colBuyerID"
        Me.colBuyerID.Visible = True
        Me.colBuyerID.VisibleIndex = 74
        '
        'colShipperID
        '
        Me.colShipperID.FieldName = "ShipperID"
        Me.colShipperID.Name = "colShipperID"
        Me.colShipperID.Visible = True
        Me.colShipperID.VisibleIndex = 75
        '
        'colLCNo
        '
        Me.colLCNo.FieldName = "LCNo"
        Me.colLCNo.Name = "colLCNo"
        Me.colLCNo.Visible = True
        Me.colLCNo.VisibleIndex = 76
        '
        'colUserCategory
        '
        Me.colUserCategory.FieldName = "UserCategory"
        Me.colUserCategory.Name = "colUserCategory"
        Me.colUserCategory.Visible = True
        Me.colUserCategory.VisibleIndex = 77
        '
        'colSalesOrderDate
        '
        Me.colSalesOrderDate.FieldName = "SalesOrderDate"
        Me.colSalesOrderDate.Name = "colSalesOrderDate"
        Me.colSalesOrderDate.Visible = True
        Me.colSalesOrderDate.VisibleIndex = 78
        '
        'colOrderSerialNo
        '
        Me.colOrderSerialNo.FieldName = "OrderSerialNo"
        Me.colOrderSerialNo.Name = "colOrderSerialNo"
        Me.colOrderSerialNo.Visible = True
        Me.colOrderSerialNo.VisibleIndex = 79
        '
        'colBuyerOrderType
        '
        Me.colBuyerOrderType.FieldName = "BuyerOrderType"
        Me.colBuyerOrderType.Name = "colBuyerOrderType"
        Me.colBuyerOrderType.Visible = True
        Me.colBuyerOrderType.VisibleIndex = 80
        '
        'colOrderNo
        '
        Me.colOrderNo.FieldName = "OrderNo"
        Me.colOrderNo.Name = "colOrderNo"
        Me.colOrderNo.Visible = True
        Me.colOrderNo.VisibleIndex = 81
        '
        'colStampingInformation1
        '
        Me.colStampingInformation1.FieldName = "StampingInformation1"
        Me.colStampingInformation1.Name = "colStampingInformation1"
        Me.colStampingInformation1.Visible = True
        Me.colStampingInformation1.VisibleIndex = 82
        '
        'colRemarks1
        '
        Me.colRemarks1.FieldName = "Remarks1"
        Me.colRemarks1.Name = "colRemarks1"
        Me.colRemarks1.Visible = True
        Me.colRemarks1.VisibleIndex = 83
        '
        'colArticleColor
        '
        Me.colArticleColor.FieldName = "ArticleColor"
        Me.colArticleColor.Name = "colArticleColor"
        Me.colArticleColor.Visible = True
        Me.colArticleColor.VisibleIndex = 84
        '
        'colArticleDescription
        '
        Me.colArticleDescription.FieldName = "ArticleDescription"
        Me.colArticleDescription.Name = "colArticleDescription"
        Me.colArticleDescription.Visible = True
        Me.colArticleDescription.VisibleIndex = 85
        '
        'colOrderWeek
        '
        Me.colOrderWeek.FieldName = "OrderWeek"
        Me.colOrderWeek.Name = "colOrderWeek"
        Me.colOrderWeek.Visible = True
        Me.colOrderWeek.VisibleIndex = 86
        '
        'colRate
        '
        Me.colRate.FieldName = "Rate"
        Me.colRate.Name = "colRate"
        Me.colRate.Visible = True
        Me.colRate.VisibleIndex = 87
        '
        'colLogo
        '
        Me.colLogo.FieldName = "Logo"
        Me.colLogo.Name = "colLogo"
        Me.colLogo.Visible = True
        Me.colLogo.VisibleIndex = 88
        '
        'colBuyerOrderQuantity
        '
        Me.colBuyerOrderQuantity.FieldName = "BuyerOrderQuantity"
        Me.colBuyerOrderQuantity.Name = "colBuyerOrderQuantity"
        Me.colBuyerOrderQuantity.Visible = True
        Me.colBuyerOrderQuantity.VisibleIndex = 89
        '
        'colDeliveryTo
        '
        Me.colDeliveryTo.FieldName = "DeliveryTo"
        Me.colDeliveryTo.Name = "colDeliveryTo"
        Me.colDeliveryTo.Visible = True
        Me.colDeliveryTo.VisibleIndex = 90
        '
        'colFinancialYear
        '
        Me.colFinancialYear.FieldName = "FinancialYear"
        Me.colFinancialYear.Name = "colFinancialYear"
        Me.colFinancialYear.Visible = True
        Me.colFinancialYear.VisibleIndex = 91
        '
        'colCustomerRefNo
        '
        Me.colCustomerRefNo.FieldName = "CustomerRefNo"
        Me.colCustomerRefNo.Name = "colCustomerRefNo"
        Me.colCustomerRefNo.Visible = True
        Me.colCustomerRefNo.VisibleIndex = 92
        '
        'colSizeName
        '
        Me.colSizeName.FieldName = "SizeName"
        Me.colSizeName.Name = "colSizeName"
        Me.colSizeName.Visible = True
        Me.colSizeName.VisibleIndex = 93
        '
        'colSpecialCustomerCode
        '
        Me.colSpecialCustomerCode.FieldName = "SpecialCustomerCode"
        Me.colSpecialCustomerCode.Name = "colSpecialCustomerCode"
        Me.colSpecialCustomerCode.Visible = True
        Me.colSpecialCustomerCode.VisibleIndex = 94
        '
        'colPortOfDischarge
        '
        Me.colPortOfDischarge.FieldName = "PortOfDischarge"
        Me.colPortOfDischarge.Name = "colPortOfDischarge"
        Me.colPortOfDischarge.Visible = True
        Me.colPortOfDischarge.VisibleIndex = 95
        '
        'colIsSampleOrder
        '
        Me.colIsSampleOrder.FieldName = "IsSampleOrder"
        Me.colIsSampleOrder.Name = "colIsSampleOrder"
        Me.colIsSampleOrder.Visible = True
        Me.colIsSampleOrder.VisibleIndex = 96
        '
        'colisManualClosed
        '
        Me.colisManualClosed.FieldName = "isManualClosed"
        Me.colisManualClosed.Name = "colisManualClosed"
        Me.colisManualClosed.Visible = True
        Me.colisManualClosed.VisibleIndex = 97
        '
        'colCommitmentOrderNo
        '
        Me.colCommitmentOrderNo.FieldName = "CommitmentOrderNo"
        Me.colCommitmentOrderNo.Name = "colCommitmentOrderNo"
        Me.colCommitmentOrderNo.Visible = True
        Me.colCommitmentOrderNo.VisibleIndex = 98
        '
        'colForecastOrderNo
        '
        Me.colForecastOrderNo.FieldName = "ForecastOrderNo"
        Me.colForecastOrderNo.Name = "colForecastOrderNo"
        Me.colForecastOrderNo.Visible = True
        Me.colForecastOrderNo.VisibleIndex = 99
        '
        'colBuyerDeliveryDate
        '
        Me.colBuyerDeliveryDate.FieldName = "BuyerDeliveryDate"
        Me.colBuyerDeliveryDate.Name = "colBuyerDeliveryDate"
        Me.colBuyerDeliveryDate.Visible = True
        Me.colBuyerDeliveryDate.VisibleIndex = 100
        '
        'colSalesOrderType
        '
        Me.colSalesOrderType.FieldName = "SalesOrderType"
        Me.colSalesOrderType.Name = "colSalesOrderType"
        Me.colSalesOrderType.Visible = True
        Me.colSalesOrderType.VisibleIndex = 101
        '
        'colDeliveryLocation
        '
        Me.colDeliveryLocation.FieldName = "DeliveryLocation"
        Me.colDeliveryLocation.Name = "colDeliveryLocation"
        Me.colDeliveryLocation.Visible = True
        Me.colDeliveryLocation.VisibleIndex = 102
        '
        'colPIDate
        '
        Me.colPIDate.FieldName = "PIDate"
        Me.colPIDate.Name = "colPIDate"
        Me.colPIDate.Visible = True
        Me.colPIDate.VisibleIndex = 103
        '
        'colChequeNo
        '
        Me.colChequeNo.FieldName = "ChequeNo"
        Me.colChequeNo.Name = "colChequeNo"
        Me.colChequeNo.Visible = True
        Me.colChequeNo.VisibleIndex = 104
        '
        'colGenerateProcessOrder
        '
        Me.colGenerateProcessOrder.FieldName = "GenerateProcessOrder"
        Me.colGenerateProcessOrder.Name = "colGenerateProcessOrder"
        Me.colGenerateProcessOrder.Visible = True
        Me.colGenerateProcessOrder.VisibleIndex = 105
        '
        'colInternalSalesOrderNo
        '
        Me.colInternalSalesOrderNo.FieldName = "InternalSalesOrderNo"
        Me.colInternalSalesOrderNo.Name = "colInternalSalesOrderNo"
        Me.colInternalSalesOrderNo.Visible = True
        Me.colInternalSalesOrderNo.VisibleIndex = 106
        '
        'colInternalBuyer
        '
        Me.colInternalBuyer.FieldName = "InternalBuyer"
        Me.colInternalBuyer.Name = "colInternalBuyer"
        Me.colInternalBuyer.Visible = True
        Me.colInternalBuyer.VisibleIndex = 107
        '
        'colType
        '
        Me.colType.FieldName = "Type"
        Me.colType.Name = "colType"
        Me.colType.Visible = True
        Me.colType.VisibleIndex = 108
        '
        'colBuyerOrderID
        '
        Me.colBuyerOrderID.FieldName = "BuyerOrderID"
        Me.colBuyerOrderID.Name = "colBuyerOrderID"
        Me.colBuyerOrderID.Visible = True
        Me.colBuyerOrderID.VisibleIndex = 109
        '
        'colIsDemandDirty
        '
        Me.colIsDemandDirty.FieldName = "IsDemandDirty"
        Me.colIsDemandDirty.Name = "colIsDemandDirty"
        Me.colIsDemandDirty.Visible = True
        Me.colIsDemandDirty.VisibleIndex = 110
        '
        'colHOId
        '
        Me.colHOId.FieldName = "HOId"
        Me.colHOId.Name = "colHOId"
        Me.colHOId.Visible = True
        Me.colHOId.VisibleIndex = 111
        '
        'pl
        '
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 215)
        Me.pl.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(7, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1184, 199)
        Me.Panel2.TabIndex = 12
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Ivory
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.tbArticleDescription)
        Me.Panel6.Controls.Add(Me.Label10)
        Me.Panel6.Controls.Add(Me.tbArticleCode)
        Me.Panel6.Controls.Add(Me.Label9)
        Me.Panel6.Controls.Add(Me.rbDetailed)
        Me.Panel6.Controls.Add(Me.rbSummary)
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Controls.Add(Me.cbxArticleName)
        Me.Panel6.Location = New System.Drawing.Point(-1, 103)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1184, 100)
        Me.Panel6.TabIndex = 16
        '
        'tbArticleDescription
        '
        Me.tbArticleDescription.Location = New System.Drawing.Point(455, 39)
        Me.tbArticleDescription.Name = "tbArticleDescription"
        Me.tbArticleDescription.Size = New System.Drawing.Size(243, 23)
        Me.tbArticleDescription.TabIndex = 18
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(456, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(220, 23)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Article / Material Description"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbArticleCode
        '
        Me.tbArticleCode.Location = New System.Drawing.Point(230, 39)
        Me.tbArticleCode.Name = "tbArticleCode"
        Me.tbArticleCode.Size = New System.Drawing.Size(220, 23)
        Me.tbArticleCode.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(230, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(220, 23)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Article / Material Code"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rbDetailed
        '
        Me.rbDetailed.AutoSize = True
        Me.rbDetailed.Location = New System.Drawing.Point(1085, 35)
        Me.rbDetailed.Margin = New System.Windows.Forms.Padding(4)
        Me.rbDetailed.Name = "rbDetailed"
        Me.rbDetailed.Size = New System.Drawing.Size(79, 20)
        Me.rbDetailed.TabIndex = 13
        Me.rbDetailed.Text = "Detailed"
        Me.rbDetailed.UseVisualStyleBackColor = True
        '
        'rbSummary
        '
        Me.rbSummary.AutoSize = True
        Me.rbSummary.Checked = True
        Me.rbSummary.Location = New System.Drawing.Point(1085, 11)
        Me.rbSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSummary.Name = "rbSummary"
        Me.rbSummary.Size = New System.Drawing.Size(86, 20)
        Me.rbSummary.TabIndex = 12
        Me.rbSummary.TabStop = True
        Me.rbSummary.Text = "Summary"
        Me.rbSummary.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(220, 23)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Article / Material Short Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxArticleName
        '
        Me.cbxArticleName.FormattingEnabled = True
        Me.cbxArticleName.Location = New System.Drawing.Point(10, 39)
        Me.cbxArticleName.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxArticleName.Name = "cbxArticleName"
        Me.cbxArticleName.Size = New System.Drawing.Size(214, 24)
        Me.cbxArticleName.TabIndex = 7
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Ivory
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.rbPending)
        Me.Panel5.Controls.Add(Me.rbCompleted)
        Me.Panel5.Controls.Add(Me.rbAll)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.cbxCustomer)
        Me.Panel5.Location = New System.Drawing.Point(645, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(538, 100)
        Me.Panel5.TabIndex = 15
        '
        'rbPending
        '
        Me.rbPending.AutoSize = True
        Me.rbPending.Location = New System.Drawing.Point(439, 72)
        Me.rbPending.Margin = New System.Windows.Forms.Padding(4)
        Me.rbPending.Name = "rbPending"
        Me.rbPending.Size = New System.Drawing.Size(77, 20)
        Me.rbPending.TabIndex = 14
        Me.rbPending.Text = "Pending"
        Me.rbPending.UseVisualStyleBackColor = True
        '
        'rbCompleted
        '
        Me.rbCompleted.AutoSize = True
        Me.rbCompleted.Location = New System.Drawing.Point(439, 43)
        Me.rbCompleted.Margin = New System.Windows.Forms.Padding(4)
        Me.rbCompleted.Name = "rbCompleted"
        Me.rbCompleted.Size = New System.Drawing.Size(95, 20)
        Me.rbCompleted.TabIndex = 13
        Me.rbCompleted.Text = "Completed"
        Me.rbCompleted.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Location = New System.Drawing.Point(439, 11)
        Me.rbAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(41, 20)
        Me.rbAll.TabIndex = 12
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(4, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(341, 23)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Customer / Supplier"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(10, 39)
        Me.cbxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(414, 24)
        Me.cbxCustomer.TabIndex = 7
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Ivory
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.cbxTypeofOrder)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.cbxTypeofDocument)
        Me.Panel4.Controls.Add(Me.rbPurchase)
        Me.Panel4.Controls.Add(Me.rbSales)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(413, 100)
        Me.Panel4.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(260, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 23)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Type of Order"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxTypeofOrder
        '
        Me.cbxTypeofOrder.FormattingEnabled = True
        Me.cbxTypeofOrder.Items.AddRange(New Object() {"All", "Job", "Sales"})
        Me.cbxTypeofOrder.Location = New System.Drawing.Point(260, 39)
        Me.cbxTypeofOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxTypeofOrder.Name = "cbxTypeofOrder"
        Me.cbxTypeofOrder.Size = New System.Drawing.Size(139, 24)
        Me.cbxTypeofOrder.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(113, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 23)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Type of Document"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxTypeofDocument
        '
        Me.cbxTypeofDocument.FormattingEnabled = True
        Me.cbxTypeofDocument.Items.AddRange(New Object() {"Order", "Invoice"})
        Me.cbxTypeofDocument.Location = New System.Drawing.Point(113, 40)
        Me.cbxTypeofDocument.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxTypeofDocument.Name = "cbxTypeofDocument"
        Me.cbxTypeofDocument.Size = New System.Drawing.Size(139, 24)
        Me.cbxTypeofDocument.TabIndex = 10
        '
        'rbPurchase
        '
        Me.rbPurchase.AutoSize = True
        Me.rbPurchase.Location = New System.Drawing.Point(14, 43)
        Me.rbPurchase.Margin = New System.Windows.Forms.Padding(4)
        Me.rbPurchase.Name = "rbPurchase"
        Me.rbPurchase.Size = New System.Drawing.Size(86, 20)
        Me.rbPurchase.TabIndex = 2
        Me.rbPurchase.Text = "Purchase"
        Me.rbPurchase.UseVisualStyleBackColor = True
        '
        'rbSales
        '
        Me.rbSales.AutoSize = True
        Me.rbSales.Checked = True
        Me.rbSales.Location = New System.Drawing.Point(14, 11)
        Me.rbSales.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSales.Name = "rbSales"
        Me.rbSales.Size = New System.Drawing.Size(61, 20)
        Me.rbSales.TabIndex = 1
        Me.rbSales.TabStop = True
        Me.rbSales.Text = "Sales"
        Me.rbSales.UseVisualStyleBackColor = True
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
        Me.Panel3.Size = New System.Drawing.Size(225, 100)
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
        'grdPurchaseInvoices
        '
        Me.grdPurchaseInvoices.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdPurchaseInvoices.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdPurchaseInvoices.Location = New System.Drawing.Point(12, 228)
        Me.grdPurchaseInvoices.MainView = Me.grdPurchaseInvoicesV1
        Me.grdPurchaseInvoices.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPurchaseInvoices.Name = "grdPurchaseInvoices"
        Me.grdPurchaseInvoices.Size = New System.Drawing.Size(1182, 393)
        Me.grdPurchaseInvoices.TabIndex = 12
        Me.grdPurchaseInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdPurchaseInvoicesV1})
        '
        'grdPurchaseInvoicesV1
        '
        Me.grdPurchaseInvoicesV1.GridControl = Me.grdPurchaseInvoices
        Me.grdPurchaseInvoicesV1.Name = "grdPurchaseInvoicesV1"
        Me.grdPurchaseInvoicesV1.OptionsView.ShowAutoFilterRow = True
        Me.grdPurchaseInvoicesV1.OptionsView.ShowFooter = True
        Me.grdPurchaseInvoicesV1.OptionsView.ShowGroupPanel = False
        '
        'grdPurchaseOrders
        '
        Me.grdPurchaseOrders.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdPurchaseOrders.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdPurchaseOrders.Location = New System.Drawing.Point(12, 228)
        Me.grdPurchaseOrders.MainView = Me.grdPurchaseOrdersV1
        Me.grdPurchaseOrders.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPurchaseOrders.Name = "grdPurchaseOrders"
        Me.grdPurchaseOrders.Size = New System.Drawing.Size(1182, 393)
        Me.grdPurchaseOrders.TabIndex = 11
        Me.grdPurchaseOrders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdPurchaseOrdersV1})
        '
        'grdPurchaseOrdersV1
        '
        Me.grdPurchaseOrdersV1.GridControl = Me.grdPurchaseOrders
        Me.grdPurchaseOrdersV1.Name = "grdPurchaseOrdersV1"
        Me.grdPurchaseOrdersV1.OptionsView.ShowAutoFilterRow = True
        Me.grdPurchaseOrdersV1.OptionsView.ShowFooter = True
        Me.grdPurchaseOrdersV1.OptionsView.ShowGroupPanel = False
        '
        'grdSalesOrders
        '
        Me.grdSalesOrders.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode3.RelationName = "Level1"
        Me.grdSalesOrders.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.grdSalesOrders.Location = New System.Drawing.Point(12, 228)
        Me.grdSalesOrders.MainView = Me.grdSalesOrdersV1
        Me.grdSalesOrders.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrders.Name = "grdSalesOrders"
        Me.grdSalesOrders.Size = New System.Drawing.Size(1182, 393)
        Me.grdSalesOrders.TabIndex = 9
        Me.grdSalesOrders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrdersV1})
        '
        'grdSalesOrdersV1
        '
        Me.grdSalesOrdersV1.GridControl = Me.grdSalesOrders
        Me.grdSalesOrdersV1.Name = "grdSalesOrdersV1"
        Me.grdSalesOrdersV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrdersV1.OptionsView.ShowFooter = True
        Me.grdSalesOrdersV1.OptionsView.ShowGroupPanel = False
        '
        'grdSalesInvoices
        '
        Me.grdSalesInvoices.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode4.RelationName = "Level1"
        Me.grdSalesInvoices.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4})
        Me.grdSalesInvoices.Location = New System.Drawing.Point(12, 228)
        Me.grdSalesInvoices.MainView = Me.grdSalesInvoicesV1
        Me.grdSalesInvoices.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesInvoices.Name = "grdSalesInvoices"
        Me.grdSalesInvoices.Size = New System.Drawing.Size(1182, 393)
        Me.grdSalesInvoices.TabIndex = 10
        Me.grdSalesInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesInvoicesV1})
        '
        'grdSalesInvoicesV1
        '
        Me.grdSalesInvoicesV1.GridControl = Me.grdSalesInvoices
        Me.grdSalesInvoicesV1.Name = "grdSalesInvoicesV1"
        Me.grdSalesInvoicesV1.OptionsView.ColumnAutoWidth = False
        Me.grdSalesInvoicesV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesInvoicesV1.OptionsView.ShowFooter = True
        Me.grdSalesInvoicesV1.OptionsView.ShowGroupPanel = False
        '
        'SalesOrderTableAdapter
        '
        Me.SalesOrderTableAdapter.ClearBeforeFill = True
        '
        'SalesOrderDetailsTableAdapter1
        '
        Me.SalesOrderDetailsTableAdapter1.ClearBeforeFill = True
        '
        'frmAllinOne1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAllinOne1"
        Me.Text = "frmInvoice"
        Me.Panel1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SalesOrderBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSalesOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grdPurchaseInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseInvoicesV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseOrdersV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrdersV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesInvoicesV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbxTypeofOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbxTypeofDocument As System.Windows.Forms.ComboBox
    Friend WithEvents rbPurchase As System.Windows.Forms.RadioButton
    Friend WithEvents rbSales As System.Windows.Forms.RadioButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rbCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rbPending As System.Windows.Forms.RadioButton
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents tbArticleCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents rbDetailed As System.Windows.Forms.RadioButton
    Friend WithEvents rbSummary As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbxArticleName As System.Windows.Forms.ComboBox
    Friend WithEvents tbArticleDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents grdSalesOrders As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrdersV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdSalesInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesInvoicesV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdPurchaseOrders As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPurchaseOrdersV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdPurchaseInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPurchaseInvoicesV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DsSalesOrder As SolarERPForSGM.dsSalesOrder
    Friend WithEvents SalesOrderBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SalesOrderTableAdapter As SolarERPForSGM.dsSalesOrderTableAdapters.SalesOrderTableAdapter
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerBuy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerGroupCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerCountry As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDestination As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerAccountCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAgentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAgentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCommissionPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderRecivedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderConfirmedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderQuality As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShipper As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colVariant As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerArticleGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCurrency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCurrencyConversion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsAssortedOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalOrderQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalOrderValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalShippedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalShippedValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalCancelQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTotalShortShippedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscountPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDiscountValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModeOfShipment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRevisedModeOfShipment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShipmentNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRevisedShipmentNature As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModeOfPayment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRevisedModeOfPayment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPictogram As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMadeInIndia As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShoeBoxBrand As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShoeBrand As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShoeBoxBarCodeLabels As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShoeBarCodeLabels As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAdhesiveTape As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colParticularInstruction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStampingInformation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colAdditionInformation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLCDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLCStartDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLCNegotiationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLCDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLCDetails As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRSNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCartingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colClaims As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActionCalendarType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colActionCalendar As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsApproved As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colApprovedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colApprovedOn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModuleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModifiedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModifiedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEnteredOnMachineID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colExeVersionNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colShipperID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLCNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUserCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderSerialNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colStampingInformation1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRemarks1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleColor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderWeek As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLogo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerOrderQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDeliveryTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFinancialYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCustomerRefNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSizeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSpecialCustomerCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPortOfDischarge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsSampleOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colisManualClosed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCommitmentOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colForecastOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDeliveryLocation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colChequeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGenerateProcessOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colInternalSalesOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colInternalBuyer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerOrderID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIsDemandDirty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHOId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SalesOrderDetailsTableAdapter1 As SolarERPForSGM.dsSalesOrderTableAdapters.SalesOrderDetailsTableAdapter
End Class
