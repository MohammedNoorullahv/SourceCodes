<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllinOne
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAllinOne))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode5 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode6 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode7 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition3 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkbxDisplayDtl = New System.Windows.Forms.CheckBox
        Me.tbArticleDescription = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.tbArticleCode = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.rbDetailed = New System.Windows.Forms.RadioButton
        Me.rbSummary = New System.Windows.Forms.RadioButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbxArticleName = New System.Windows.Forms.ComboBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rbClose = New System.Windows.Forms.RadioButton
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
        Me.grdPurchaseOrder = New DevExpress.XtraGrid.GridControl
        Me.TmptblPurchaseOrderforSGMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DstmpPurchaseOrder = New SolarERPForSGM.DstmpPurchaseOrder
        Me.grdPurchaseOrderV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colID1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPartyName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCurrencyCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialSize = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialColorDescription1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colUnit = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPrice1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialValue = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialTypeDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colReceivedQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBalanceQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModuleName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCreatedBy = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCreatedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModifiedBy = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colExeVersionNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModifiedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colEnteredOnMachineID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.grdSalesOrderDetails = New DevExpress.XtraGrid.GridControl
        Me.TmptblSalesOrderHeaderforSGMBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DstmpSalesorder = New SolarERPForSGM.DstmpSalesorder
        Me.grdSalesOrderDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderRecivedDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBuyerName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSalesOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCustomerOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticle = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialColorDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCodificationNew = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArticleMould = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPrice = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOrderValue = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDispQty = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBal2disp = New DevExpress.XtraGrid.Columns.GridColumn
        Me.TmptblSalesOrderHeaderforSGMBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.TmptblSalesOrderHeaderforSGMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Tmptbl_SalesOrderHeaderforSGMTableAdapter = New SolarERPForSGM.DstmpSalesorderTableAdapters.tmptbl_SalesOrderHeaderforSGMTableAdapter
        Me.TmptblSalesOrderDetailforSGMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Tmptbl_SalesOrderDetailforSGMTableAdapter = New SolarERPForSGM.DstmpSalesorderTableAdapters.tmptbl_SalesOrderDetailforSGMTableAdapter
        Me.Tmptbl_PurchaseOrderforSGMTableAdapter = New SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderforSGMTableAdapter
        Me.TmptblPurchaseOrderDetailsforSGMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter = New SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderDetailsforSGMTableAdapter
        Me.Panel1.SuspendLayout()
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
        CType(Me.grdPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblPurchaseOrderforSGMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DstmpPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseOrderV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblSalesOrderHeaderforSGMBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DstmpSalesorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblSalesOrderHeaderforSGMBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblSalesOrderHeaderforSGMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblSalesOrderDetailforSGMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblPurchaseOrderDetailsforSGMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.pgbar)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.grdPurchaseInvoices)
        Me.Panel1.Controls.Add(Me.grdPurchaseOrders)
        Me.Panel1.Controls.Add(Me.grdSalesOrders)
        Me.Panel1.Controls.Add(Me.grdSalesInvoices)
        Me.Panel1.Controls.Add(Me.grdPurchaseOrder)
        Me.Panel1.Controls.Add(Me.grdSalesOrderDetails)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(401, 628)
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(663, 51)
        Me.pgbar.TabIndex = 15
        Me.pgbar.Visible = False
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
        Me.Panel6.Controls.Add(Me.chkbxDisplayDtl)
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
        'chkbxDisplayDtl
        '
        Me.chkbxDisplayDtl.AutoSize = True
        Me.chkbxDisplayDtl.Enabled = False
        Me.chkbxDisplayDtl.Location = New System.Drawing.Point(733, 62)
        Me.chkbxDisplayDtl.Name = "chkbxDisplayDtl"
        Me.chkbxDisplayDtl.Size = New System.Drawing.Size(431, 20)
        Me.chkbxDisplayDtl.TabIndex = 19
        Me.chkbxDisplayDtl.Text = "Display Invoice Details of Sales Order with Drop Down Option"
        Me.chkbxDisplayDtl.UseVisualStyleBackColor = True
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
        Me.Panel5.Controls.Add(Me.rbClose)
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
        'rbClose
        '
        Me.rbClose.AutoSize = True
        Me.rbClose.Location = New System.Drawing.Point(439, 74)
        Me.rbClose.Margin = New System.Windows.Forms.Padding(4)
        Me.rbClose.Name = "rbClose"
        Me.rbClose.Size = New System.Drawing.Size(61, 20)
        Me.rbClose.TabIndex = 15
        Me.rbClose.Text = "Close"
        Me.rbClose.UseVisualStyleBackColor = True
        '
        'rbPending
        '
        Me.rbPending.AutoSize = True
        Me.rbPending.Location = New System.Drawing.Point(439, 50)
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
        Me.rbCompleted.Location = New System.Drawing.Point(439, 26)
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
        Me.rbAll.Location = New System.Drawing.Point(439, 3)
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
        Me.grdPurchaseInvoices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.grdPurchaseOrders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.grdSalesOrders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdSalesOrdersV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdSalesOrdersV1.GridControl = Me.grdSalesOrders
        Me.grdSalesOrdersV1.Name = "grdSalesOrdersV1"
        Me.grdSalesOrdersV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrdersV1.OptionsView.ShowFooter = True
        Me.grdSalesOrdersV1.OptionsView.ShowGroupPanel = False
        '
        'grdSalesInvoices
        '
        Me.grdSalesInvoices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        'grdPurchaseOrder
        '
        Me.grdPurchaseOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPurchaseOrder.DataSource = Me.TmptblPurchaseOrderforSGMBindingSource
        Me.grdPurchaseOrder.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode5.RelationName = "tmptbl_PurchaseOrderforSGM_tmptbl_PurchaseOrderDetailsforSGM"
        GridLevelNode6.RelationName = "tmptbl_PurchaseOrderforSGM_tmptbl_PurchaseOrderDetailsforSGM1"
        Me.grdPurchaseOrder.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode5, GridLevelNode6})
        Me.grdPurchaseOrder.Location = New System.Drawing.Point(12, 228)
        Me.grdPurchaseOrder.LookAndFeel.SkinName = "Office 2013"
        Me.grdPurchaseOrder.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdPurchaseOrder.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdPurchaseOrder.MainView = Me.grdPurchaseOrderV1
        Me.grdPurchaseOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPurchaseOrder.Name = "grdPurchaseOrder"
        Me.grdPurchaseOrder.Size = New System.Drawing.Size(1182, 393)
        Me.grdPurchaseOrder.TabIndex = 14
        Me.grdPurchaseOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdPurchaseOrderV1})
        '
        'TmptblPurchaseOrderforSGMBindingSource
        '
        Me.TmptblPurchaseOrderforSGMBindingSource.DataMember = "tmptbl_PurchaseOrderforSGM"
        Me.TmptblPurchaseOrderforSGMBindingSource.DataSource = Me.DstmpPurchaseOrder
        '
        'DstmpPurchaseOrder
        '
        Me.DstmpPurchaseOrder.DataSetName = "DstmpPurchaseOrder"
        Me.DstmpPurchaseOrder.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'grdPurchaseOrderV1
        '
        Me.grdPurchaseOrderV1.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.DetailTip.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.DetailTip.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.Empty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.Empty.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.EvenRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.EvenRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.FilterCloseButton.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.FilterPanel.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.FixedLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.FixedLine.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.FocusedCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.FocusedCell.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.FocusedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.FocusedRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.FooterPanel.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.GroupButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.GroupButton.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.GroupFooter.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.GroupPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.GroupPanel.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.GroupRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.GroupRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.HorzLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.HorzLine.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.OddRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.OddRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.Preview.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.Row.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.RowSeparator.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.RowSeparator.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.SelectedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.SelectedRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.TopNewRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.TopNewRow.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.VertLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.VertLine.Options.UseFont = True
        Me.grdPurchaseOrderV1.Appearance.ViewCaption.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrderV1.Appearance.ViewCaption.Options.UseFont = True
        Me.grdPurchaseOrderV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID1, Me.colPurchaseOrderNo, Me.colPurchaseOrderDate, Me.colPartyName, Me.colCurrencyCode, Me.colPurchaseOrderType, Me.colMaterialCode, Me.colMaterialDescription, Me.colMaterialSize, Me.colMaterialColorDescription1, Me.colUnit, Me.colQuantity, Me.colPrice1, Me.colMaterialValue, Me.colMaterialTypeDescription, Me.colReceivedQuantity, Me.colBalanceQuantity, Me.colModuleName, Me.colCreatedBy, Me.colCreatedDate, Me.colModifiedBy, Me.colExeVersionNo, Me.colModifiedDate, Me.colEnteredOnMachineID})
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdPurchaseOrderV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdPurchaseOrderV1.GridControl = Me.grdPurchaseOrder
        Me.grdPurchaseOrderV1.Name = "grdPurchaseOrderV1"
        Me.grdPurchaseOrderV1.OptionsPrint.PrintDetails = True
        Me.grdPurchaseOrderV1.OptionsView.ShowAutoFilterRow = True
        Me.grdPurchaseOrderV1.OptionsView.ShowFooter = True
        Me.grdPurchaseOrderV1.OptionsView.ShowGroupPanel = False
        '
        'colID1
        '
        Me.colID1.FieldName = "ID"
        Me.colID1.Name = "colID1"
        '
        'colPurchaseOrderNo
        '
        Me.colPurchaseOrderNo.FieldName = "PurchaseOrderNo"
        Me.colPurchaseOrderNo.Name = "colPurchaseOrderNo"
        Me.colPurchaseOrderNo.Visible = True
        Me.colPurchaseOrderNo.VisibleIndex = 2
        '
        'colPurchaseOrderDate
        '
        Me.colPurchaseOrderDate.DisplayFormat.FormatString = "dd-MMM-yyyy"
        Me.colPurchaseOrderDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colPurchaseOrderDate.FieldName = "PurchaseOrderDate"
        Me.colPurchaseOrderDate.Name = "colPurchaseOrderDate"
        Me.colPurchaseOrderDate.Visible = True
        Me.colPurchaseOrderDate.VisibleIndex = 0
        '
        'colPartyName
        '
        Me.colPartyName.FieldName = "PartyName"
        Me.colPartyName.Name = "colPartyName"
        Me.colPartyName.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)})
        Me.colPartyName.Visible = True
        Me.colPartyName.VisibleIndex = 1
        '
        'colCurrencyCode
        '
        Me.colCurrencyCode.FieldName = "CurrencyCode"
        Me.colCurrencyCode.Name = "colCurrencyCode"
        Me.colCurrencyCode.Visible = True
        Me.colCurrencyCode.VisibleIndex = 7
        '
        'colPurchaseOrderType
        '
        Me.colPurchaseOrderType.FieldName = "PurchaseOrderType"
        Me.colPurchaseOrderType.Name = "colPurchaseOrderType"
        '
        'colMaterialCode
        '
        Me.colMaterialCode.FieldName = "MaterialCode"
        Me.colMaterialCode.Name = "colMaterialCode"
        Me.colMaterialCode.Visible = True
        Me.colMaterialCode.VisibleIndex = 3
        '
        'colMaterialDescription
        '
        Me.colMaterialDescription.FieldName = "MaterialDescription"
        Me.colMaterialDescription.Name = "colMaterialDescription"
        Me.colMaterialDescription.Visible = True
        Me.colMaterialDescription.VisibleIndex = 5
        '
        'colMaterialSize
        '
        Me.colMaterialSize.FieldName = "MaterialSize"
        Me.colMaterialSize.Name = "colMaterialSize"
        '
        'colMaterialColorDescription1
        '
        Me.colMaterialColorDescription1.FieldName = "MaterialColorDescription"
        Me.colMaterialColorDescription1.Name = "colMaterialColorDescription1"
        '
        'colUnit
        '
        Me.colUnit.FieldName = "Unit"
        Me.colUnit.Name = "colUnit"
        Me.colUnit.Visible = True
        Me.colUnit.VisibleIndex = 6
        '
        'colQuantity
        '
        Me.colQuantity.FieldName = "Quantity"
        Me.colQuantity.Name = "colQuantity"
        Me.colQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colQuantity.Visible = True
        Me.colQuantity.VisibleIndex = 8
        '
        'colPrice1
        '
        Me.colPrice1.FieldName = "Price"
        Me.colPrice1.Name = "colPrice1"
        Me.colPrice1.Visible = True
        Me.colPrice1.VisibleIndex = 9
        '
        'colMaterialValue
        '
        Me.colMaterialValue.FieldName = "MaterialValue"
        Me.colMaterialValue.Name = "colMaterialValue"
        Me.colMaterialValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colMaterialValue.Visible = True
        Me.colMaterialValue.VisibleIndex = 10
        '
        'colMaterialTypeDescription
        '
        Me.colMaterialTypeDescription.FieldName = "MaterialTypeDescription"
        Me.colMaterialTypeDescription.Name = "colMaterialTypeDescription"
        Me.colMaterialTypeDescription.Visible = True
        Me.colMaterialTypeDescription.VisibleIndex = 4
        '
        'colReceivedQuantity
        '
        Me.colReceivedQuantity.FieldName = "ReceivedQuantity"
        Me.colReceivedQuantity.Name = "colReceivedQuantity"
        Me.colReceivedQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colReceivedQuantity.Visible = True
        Me.colReceivedQuantity.VisibleIndex = 11
        '
        'colBalanceQuantity
        '
        Me.colBalanceQuantity.FieldName = "BalanceQuantity"
        Me.colBalanceQuantity.Name = "colBalanceQuantity"
        Me.colBalanceQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colBalanceQuantity.Visible = True
        Me.colBalanceQuantity.VisibleIndex = 12
        '
        'colModuleName
        '
        Me.colModuleName.FieldName = "ModuleName"
        Me.colModuleName.Name = "colModuleName"
        '
        'colCreatedBy
        '
        Me.colCreatedBy.FieldName = "CreatedBy"
        Me.colCreatedBy.Name = "colCreatedBy"
        '
        'colCreatedDate
        '
        Me.colCreatedDate.FieldName = "CreatedDate"
        Me.colCreatedDate.Name = "colCreatedDate"
        '
        'colModifiedBy
        '
        Me.colModifiedBy.FieldName = "ModifiedBy"
        Me.colModifiedBy.Name = "colModifiedBy"
        '
        'colExeVersionNo
        '
        Me.colExeVersionNo.FieldName = "ExeVersionNo"
        Me.colExeVersionNo.Name = "colExeVersionNo"
        '
        'colModifiedDate
        '
        Me.colModifiedDate.FieldName = "ModifiedDate"
        Me.colModifiedDate.Name = "colModifiedDate"
        '
        'colEnteredOnMachineID
        '
        Me.colEnteredOnMachineID.FieldName = "EnteredOnMachineID"
        Me.colEnteredOnMachineID.Name = "colEnteredOnMachineID"
        '
        'grdSalesOrderDetails
        '
        Me.grdSalesOrderDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderDetails.DataSource = Me.TmptblSalesOrderHeaderforSGMBindingSource2
        Me.grdSalesOrderDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode7.RelationName = "tmptbl_SalesOrderHeaderforSGM_tmptbl_SalesOrderDetailforSGM"
        Me.grdSalesOrderDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode7})
        Me.grdSalesOrderDetails.Location = New System.Drawing.Point(12, 228)
        Me.grdSalesOrderDetails.LookAndFeel.SkinName = "Office 2013"
        Me.grdSalesOrderDetails.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdSalesOrderDetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdSalesOrderDetails.MainView = Me.grdSalesOrderDetailsV1
        Me.grdSalesOrderDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderDetails.Name = "grdSalesOrderDetails"
        Me.grdSalesOrderDetails.Size = New System.Drawing.Size(1182, 393)
        Me.grdSalesOrderDetails.TabIndex = 13
        Me.grdSalesOrderDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderDetailsV1})
        '
        'TmptblSalesOrderHeaderforSGMBindingSource2
        '
        Me.TmptblSalesOrderHeaderforSGMBindingSource2.DataMember = "tmptbl_SalesOrderHeaderforSGM"
        Me.TmptblSalesOrderHeaderforSGMBindingSource2.DataSource = Me.DstmpSalesorder
        '
        'DstmpSalesorder
        '
        Me.DstmpSalesorder.DataSetName = "DstmpSalesorder"
        Me.DstmpSalesorder.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'grdSalesOrderDetailsV1
        '
        Me.grdSalesOrderDetailsV1.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.DetailTip.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.DetailTip.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.Empty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.Empty.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.EvenRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.EvenRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.FilterCloseButton.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.FilterPanel.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.FixedLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.FixedLine.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.FocusedCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.FocusedCell.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.FocusedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.FocusedRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.FooterPanel.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.GroupButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.GroupButton.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.GroupFooter.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.GroupPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.GroupPanel.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.GroupRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.GroupRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.HorzLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.HorzLine.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.OddRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.OddRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.Preview.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.Row.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.RowSeparator.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.RowSeparator.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.SelectedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.SelectedRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.TopNewRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.TopNewRow.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.VertLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.VertLine.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Appearance.ViewCaption.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalesOrderDetailsV1.Appearance.ViewCaption.Options.UseFont = True
        Me.grdSalesOrderDetailsV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.colOrderRecivedDate, Me.colBuyerName, Me.colSalesOrderNo, Me.colCustomerOrderNo, Me.colArticle, Me.colArticleName, Me.colDescription, Me.colMaterialColorDescription, Me.colCodificationNew, Me.colArticleMould, Me.colOrderType, Me.colOrderQuantity, Me.colPrice, Me.colOrderValue, Me.colDispQty, Me.colBal2disp})
        StyleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition3.Appearance.Options.UseForeColor = True
        StyleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition3.Value1 = CType(0, Short)
        Me.grdSalesOrderDetailsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition3})
        Me.grdSalesOrderDetailsV1.GridControl = Me.grdSalesOrderDetails
        Me.grdSalesOrderDetailsV1.Name = "grdSalesOrderDetailsV1"
        Me.grdSalesOrderDetailsV1.OptionsPrint.PrintDetails = True
        Me.grdSalesOrderDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderDetailsV1.OptionsView.ShowFooter = True
        Me.grdSalesOrderDetailsV1.OptionsView.ShowGroupPanel = False
        '
        'colID
        '
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        '
        'colOrderRecivedDate
        '
        Me.colOrderRecivedDate.DisplayFormat.FormatString = "dd-MMM-yyyy"
        Me.colOrderRecivedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.colOrderRecivedDate.FieldName = "OrderRecivedDate"
        Me.colOrderRecivedDate.Name = "colOrderRecivedDate"
        Me.colOrderRecivedDate.Visible = True
        Me.colOrderRecivedDate.VisibleIndex = 0
        '
        'colBuyerName
        '
        Me.colBuyerName.FieldName = "BuyerName"
        Me.colBuyerName.Name = "colBuyerName"
        Me.colBuyerName.Visible = True
        Me.colBuyerName.VisibleIndex = 1
        '
        'colSalesOrderNo
        '
        Me.colSalesOrderNo.FieldName = "SalesOrderNo"
        Me.colSalesOrderNo.Name = "colSalesOrderNo"
        Me.colSalesOrderNo.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)})
        Me.colSalesOrderNo.Visible = True
        Me.colSalesOrderNo.VisibleIndex = 2
        '
        'colCustomerOrderNo
        '
        Me.colCustomerOrderNo.FieldName = "CustomerOrderNo"
        Me.colCustomerOrderNo.Name = "colCustomerOrderNo"
        Me.colCustomerOrderNo.Visible = True
        Me.colCustomerOrderNo.VisibleIndex = 3
        '
        'colArticle
        '
        Me.colArticle.FieldName = "Article"
        Me.colArticle.Name = "colArticle"
        Me.colArticle.Visible = True
        Me.colArticle.VisibleIndex = 4
        '
        'colArticleName
        '
        Me.colArticleName.FieldName = "ArticleName"
        Me.colArticleName.Name = "colArticleName"
        Me.colArticleName.Visible = True
        Me.colArticleName.VisibleIndex = 5
        '
        'colDescription
        '
        Me.colDescription.FieldName = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Visible = True
        Me.colDescription.VisibleIndex = 6
        '
        'colMaterialColorDescription
        '
        Me.colMaterialColorDescription.FieldName = "MaterialColorDescription"
        Me.colMaterialColorDescription.Name = "colMaterialColorDescription"
        Me.colMaterialColorDescription.Visible = True
        Me.colMaterialColorDescription.VisibleIndex = 7
        '
        'colCodificationNew
        '
        Me.colCodificationNew.FieldName = "CodificationNew"
        Me.colCodificationNew.Name = "colCodificationNew"
        Me.colCodificationNew.Visible = True
        Me.colCodificationNew.VisibleIndex = 8
        '
        'colArticleMould
        '
        Me.colArticleMould.FieldName = "ArticleMould"
        Me.colArticleMould.Name = "colArticleMould"
        Me.colArticleMould.Visible = True
        Me.colArticleMould.VisibleIndex = 9
        '
        'colOrderType
        '
        Me.colOrderType.FieldName = "OrderType"
        Me.colOrderType.Name = "colOrderType"
        Me.colOrderType.Visible = True
        Me.colOrderType.VisibleIndex = 10
        '
        'colOrderQuantity
        '
        Me.colOrderQuantity.FieldName = "OrderQuantity"
        Me.colOrderQuantity.Name = "colOrderQuantity"
        Me.colOrderQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colOrderQuantity.Visible = True
        Me.colOrderQuantity.VisibleIndex = 11
        '
        'colPrice
        '
        Me.colPrice.FieldName = "Price"
        Me.colPrice.Name = "colPrice"
        Me.colPrice.Visible = True
        Me.colPrice.VisibleIndex = 12
        '
        'colOrderValue
        '
        Me.colOrderValue.FieldName = "OrderValue"
        Me.colOrderValue.Name = "colOrderValue"
        Me.colOrderValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colOrderValue.Visible = True
        Me.colOrderValue.VisibleIndex = 13
        '
        'colDispQty
        '
        Me.colDispQty.FieldName = "DispQty"
        Me.colDispQty.Name = "colDispQty"
        Me.colDispQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colDispQty.Visible = True
        Me.colDispQty.VisibleIndex = 14
        '
        'colBal2disp
        '
        Me.colBal2disp.FieldName = "Bal2disp"
        Me.colBal2disp.Name = "colBal2disp"
        Me.colBal2disp.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colBal2disp.Visible = True
        Me.colBal2disp.VisibleIndex = 15
        '
        'TmptblSalesOrderHeaderforSGMBindingSource1
        '
        Me.TmptblSalesOrderHeaderforSGMBindingSource1.DataMember = "tmptbl_SalesOrderHeaderforSGM"
        Me.TmptblSalesOrderHeaderforSGMBindingSource1.DataSource = Me.DstmpSalesorder
        '
        'TmptblSalesOrderHeaderforSGMBindingSource
        '
        Me.TmptblSalesOrderHeaderforSGMBindingSource.DataMember = "tmptbl_SalesOrderHeaderforSGM"
        Me.TmptblSalesOrderHeaderforSGMBindingSource.DataSource = Me.DstmpSalesorder
        '
        'Tmptbl_SalesOrderHeaderforSGMTableAdapter
        '
        Me.Tmptbl_SalesOrderHeaderforSGMTableAdapter.ClearBeforeFill = True
        '
        'TmptblSalesOrderDetailforSGMBindingSource
        '
        Me.TmptblSalesOrderDetailforSGMBindingSource.DataMember = "tmptbl_SalesOrderDetailforSGM"
        Me.TmptblSalesOrderDetailforSGMBindingSource.DataSource = Me.DstmpSalesorder
        '
        'Tmptbl_SalesOrderDetailforSGMTableAdapter
        '
        Me.Tmptbl_SalesOrderDetailforSGMTableAdapter.ClearBeforeFill = True
        '
        'Tmptbl_PurchaseOrderforSGMTableAdapter
        '
        Me.Tmptbl_PurchaseOrderforSGMTableAdapter.ClearBeforeFill = True
        '
        'TmptblPurchaseOrderDetailsforSGMBindingSource
        '
        Me.TmptblPurchaseOrderDetailsforSGMBindingSource.DataMember = "tmptbl_PurchaseOrderDetailsforSGM"
        Me.TmptblPurchaseOrderDetailsforSGMBindingSource.DataSource = Me.DstmpPurchaseOrder
        '
        'Tmptbl_PurchaseOrderDetailsforSGMTableAdapter
        '
        Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.ClearBeforeFill = True
        '
        'frmAllinOne
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAllinOne"
        Me.Text = "frmInvoice"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
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
        CType(Me.grdPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblPurchaseOrderforSGMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DstmpPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseOrderV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblSalesOrderHeaderforSGMBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DstmpSalesorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblSalesOrderHeaderforSGMBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblSalesOrderHeaderforSGMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblSalesOrderDetailforSGMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblPurchaseOrderDetailsforSGMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
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
    Friend WithEvents grdSalesOrderDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DstmpSalesorder As SolarERPForSGM.DstmpSalesorder
    Friend WithEvents TmptblSalesOrderHeaderforSGMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Tmptbl_SalesOrderHeaderforSGMTableAdapter As SolarERPForSGM.DstmpSalesorderTableAdapters.tmptbl_SalesOrderHeaderforSGMTableAdapter
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderRecivedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBuyerName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSalesOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCustomerOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialColorDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCodificationNew As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArticleMould As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOrderValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDispQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBal2disp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TmptblSalesOrderHeaderforSGMBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents TmptblSalesOrderDetailforSGMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Tmptbl_SalesOrderDetailforSGMTableAdapter As SolarERPForSGM.DstmpSalesorderTableAdapters.tmptbl_SalesOrderDetailforSGMTableAdapter
    Friend WithEvents TmptblSalesOrderHeaderforSGMBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents chkbxDisplayDtl As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents grdPurchaseOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPurchaseOrderV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DstmpPurchaseOrder As SolarERPForSGM.DstmpPurchaseOrder
    Friend WithEvents TmptblPurchaseOrderforSGMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Tmptbl_PurchaseOrderforSGMTableAdapter As SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderforSGMTableAdapter
    Friend WithEvents TmptblPurchaseOrderDetailsforSGMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Tmptbl_PurchaseOrderDetailsforSGMTableAdapter As SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderDetailsforSGMTableAdapter
    Friend WithEvents colID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPartyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCurrencyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialColorDescription1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrice1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialTypeDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReceivedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBalanceQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModuleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModifiedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colExeVersionNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModifiedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEnteredOnMachineID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents rbClose As System.Windows.Forms.RadioButton
End Class
