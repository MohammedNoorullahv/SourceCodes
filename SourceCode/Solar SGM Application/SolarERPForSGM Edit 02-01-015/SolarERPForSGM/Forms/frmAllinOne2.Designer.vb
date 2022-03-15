<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllinOne2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAllinOne2))
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdPurchaseOrder = New DevExpress.XtraGrid.GridControl
        Me.TmptblPurchaseOrderforSGMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DstmpPurchaseOrder = New SolarERPForSGM.DstmpPurchaseOrder
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colID1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderNo1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderDate1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPartyName1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCurrencyCode1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderType1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialCode1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialDescription1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialSize1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialColorDescription1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colUnit1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colQuantity1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPrice1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialValue1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialTypeDescription1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colReceivedQuantity1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colBalanceQuantity1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModuleName1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCreatedBy1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCreatedDate1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModifiedBy1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colExeVersionNo1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colModifiedDate1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colEnteredOnMachineID1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.grdPurchaseOrderV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPartyName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colCurrencyCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPurchaseOrderType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialSize = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialColorDescription = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colUnit = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colQuantity = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colPrice = New DevExpress.XtraGrid.Columns.GridColumn
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Tmptbl_PurchaseOrderforSGMTableAdapter = New SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderforSGMTableAdapter
        Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter = New SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderDetailsforSGMTableAdapter
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TmptblPurchaseOrderforSGMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DstmpPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseOrderV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdPurchaseOrder
        Me.GridView1.Name = "GridView1"
        '
        'grdPurchaseOrder
        '
        Me.grdPurchaseOrder.DataSource = Me.TmptblPurchaseOrderforSGMBindingSource
        Me.grdPurchaseOrder.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPurchaseOrder.Location = New System.Drawing.Point(12, 7)
        Me.grdPurchaseOrder.LookAndFeel.SkinName = "Office 2013"
        Me.grdPurchaseOrder.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdPurchaseOrder.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdPurchaseOrder.MainView = Me.GridView2
        Me.grdPurchaseOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPurchaseOrder.Name = "grdPurchaseOrder"
        Me.grdPurchaseOrder.Size = New System.Drawing.Size(1182, 613)
        Me.grdPurchaseOrder.TabIndex = 14
        Me.grdPurchaseOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2, Me.grdPurchaseOrderV1, Me.GridView1})
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
        'GridView2
        '
        Me.GridView2.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.GridView2.Appearance.FilterCloseButton.Options.UseFont = True
        Me.GridView2.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.GridView2.Appearance.FilterPanel.Options.UseFont = True
        Me.GridView2.Appearance.ViewCaption.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.GridView2.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID1, Me.colPurchaseOrderNo1, Me.colPurchaseOrderDate1, Me.colPartyName1, Me.colCurrencyCode1, Me.colPurchaseOrderType1, Me.colMaterialCode1, Me.colMaterialDescription1, Me.colMaterialSize1, Me.colMaterialColorDescription1, Me.colUnit1, Me.colQuantity1, Me.colPrice1, Me.colMaterialValue1, Me.colMaterialTypeDescription1, Me.colReceivedQuantity1, Me.colBalanceQuantity1, Me.colModuleName1, Me.colCreatedBy1, Me.colCreatedDate1, Me.colModifiedBy1, Me.colExeVersionNo1, Me.colModifiedDate1, Me.colEnteredOnMachineID1})
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.GridView2.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.GridView2.GridControl = Me.grdPurchaseOrder
        Me.GridView2.Name = "GridView2"
        '
        'colID1
        '
        Me.colID1.FieldName = "ID"
        Me.colID1.Name = "colID1"
        Me.colID1.Visible = True
        Me.colID1.VisibleIndex = 0
        '
        'colPurchaseOrderNo1
        '
        Me.colPurchaseOrderNo1.FieldName = "PurchaseOrderNo"
        Me.colPurchaseOrderNo1.Name = "colPurchaseOrderNo1"
        Me.colPurchaseOrderNo1.Visible = True
        Me.colPurchaseOrderNo1.VisibleIndex = 1
        '
        'colPurchaseOrderDate1
        '
        Me.colPurchaseOrderDate1.FieldName = "PurchaseOrderDate"
        Me.colPurchaseOrderDate1.Name = "colPurchaseOrderDate1"
        Me.colPurchaseOrderDate1.Visible = True
        Me.colPurchaseOrderDate1.VisibleIndex = 2
        '
        'colPartyName1
        '
        Me.colPartyName1.FieldName = "PartyName"
        Me.colPartyName1.Name = "colPartyName1"
        Me.colPartyName1.Visible = True
        Me.colPartyName1.VisibleIndex = 3
        '
        'colCurrencyCode1
        '
        Me.colCurrencyCode1.FieldName = "CurrencyCode"
        Me.colCurrencyCode1.Name = "colCurrencyCode1"
        Me.colCurrencyCode1.Visible = True
        Me.colCurrencyCode1.VisibleIndex = 4
        '
        'colPurchaseOrderType1
        '
        Me.colPurchaseOrderType1.FieldName = "PurchaseOrderType"
        Me.colPurchaseOrderType1.Name = "colPurchaseOrderType1"
        Me.colPurchaseOrderType1.Visible = True
        Me.colPurchaseOrderType1.VisibleIndex = 5
        '
        'colMaterialCode1
        '
        Me.colMaterialCode1.FieldName = "MaterialCode"
        Me.colMaterialCode1.Name = "colMaterialCode1"
        Me.colMaterialCode1.Visible = True
        Me.colMaterialCode1.VisibleIndex = 6
        '
        'colMaterialDescription1
        '
        Me.colMaterialDescription1.FieldName = "MaterialDescription"
        Me.colMaterialDescription1.Name = "colMaterialDescription1"
        Me.colMaterialDescription1.Visible = True
        Me.colMaterialDescription1.VisibleIndex = 7
        '
        'colMaterialSize1
        '
        Me.colMaterialSize1.FieldName = "MaterialSize"
        Me.colMaterialSize1.Name = "colMaterialSize1"
        Me.colMaterialSize1.Visible = True
        Me.colMaterialSize1.VisibleIndex = 8
        '
        'colMaterialColorDescription1
        '
        Me.colMaterialColorDescription1.FieldName = "MaterialColorDescription"
        Me.colMaterialColorDescription1.Name = "colMaterialColorDescription1"
        Me.colMaterialColorDescription1.Visible = True
        Me.colMaterialColorDescription1.VisibleIndex = 9
        '
        'colUnit1
        '
        Me.colUnit1.FieldName = "Unit"
        Me.colUnit1.Name = "colUnit1"
        Me.colUnit1.Visible = True
        Me.colUnit1.VisibleIndex = 10
        '
        'colQuantity1
        '
        Me.colQuantity1.FieldName = "Quantity"
        Me.colQuantity1.Name = "colQuantity1"
        Me.colQuantity1.Visible = True
        Me.colQuantity1.VisibleIndex = 11
        '
        'colPrice1
        '
        Me.colPrice1.FieldName = "Price"
        Me.colPrice1.Name = "colPrice1"
        Me.colPrice1.Visible = True
        Me.colPrice1.VisibleIndex = 12
        '
        'colMaterialValue1
        '
        Me.colMaterialValue1.FieldName = "MaterialValue"
        Me.colMaterialValue1.Name = "colMaterialValue1"
        Me.colMaterialValue1.Visible = True
        Me.colMaterialValue1.VisibleIndex = 13
        '
        'colMaterialTypeDescription1
        '
        Me.colMaterialTypeDescription1.FieldName = "MaterialTypeDescription"
        Me.colMaterialTypeDescription1.Name = "colMaterialTypeDescription1"
        Me.colMaterialTypeDescription1.Visible = True
        Me.colMaterialTypeDescription1.VisibleIndex = 14
        '
        'colReceivedQuantity1
        '
        Me.colReceivedQuantity1.FieldName = "ReceivedQuantity"
        Me.colReceivedQuantity1.Name = "colReceivedQuantity1"
        Me.colReceivedQuantity1.Visible = True
        Me.colReceivedQuantity1.VisibleIndex = 15
        '
        'colBalanceQuantity1
        '
        Me.colBalanceQuantity1.FieldName = "BalanceQuantity"
        Me.colBalanceQuantity1.Name = "colBalanceQuantity1"
        Me.colBalanceQuantity1.Visible = True
        Me.colBalanceQuantity1.VisibleIndex = 16
        '
        'colModuleName1
        '
        Me.colModuleName1.FieldName = "ModuleName"
        Me.colModuleName1.Name = "colModuleName1"
        Me.colModuleName1.Visible = True
        Me.colModuleName1.VisibleIndex = 17
        '
        'colCreatedBy1
        '
        Me.colCreatedBy1.FieldName = "CreatedBy"
        Me.colCreatedBy1.Name = "colCreatedBy1"
        Me.colCreatedBy1.Visible = True
        Me.colCreatedBy1.VisibleIndex = 18
        '
        'colCreatedDate1
        '
        Me.colCreatedDate1.FieldName = "CreatedDate"
        Me.colCreatedDate1.Name = "colCreatedDate1"
        Me.colCreatedDate1.Visible = True
        Me.colCreatedDate1.VisibleIndex = 19
        '
        'colModifiedBy1
        '
        Me.colModifiedBy1.FieldName = "ModifiedBy"
        Me.colModifiedBy1.Name = "colModifiedBy1"
        Me.colModifiedBy1.Visible = True
        Me.colModifiedBy1.VisibleIndex = 20
        '
        'colExeVersionNo1
        '
        Me.colExeVersionNo1.FieldName = "ExeVersionNo"
        Me.colExeVersionNo1.Name = "colExeVersionNo1"
        Me.colExeVersionNo1.Visible = True
        Me.colExeVersionNo1.VisibleIndex = 21
        '
        'colModifiedDate1
        '
        Me.colModifiedDate1.FieldName = "ModifiedDate"
        Me.colModifiedDate1.Name = "colModifiedDate1"
        Me.colModifiedDate1.Visible = True
        Me.colModifiedDate1.VisibleIndex = 22
        '
        'colEnteredOnMachineID1
        '
        Me.colEnteredOnMachineID1.FieldName = "EnteredOnMachineID"
        Me.colEnteredOnMachineID1.Name = "colEnteredOnMachineID1"
        Me.colEnteredOnMachineID1.Visible = True
        Me.colEnteredOnMachineID1.VisibleIndex = 23
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
        Me.grdPurchaseOrderV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.colPurchaseOrderNo, Me.colPurchaseOrderDate, Me.colPartyName, Me.colCurrencyCode, Me.colPurchaseOrderType, Me.colMaterialCode, Me.colMaterialDescription, Me.colMaterialSize, Me.colMaterialColorDescription, Me.colUnit, Me.colQuantity, Me.colPrice, Me.colMaterialValue, Me.colMaterialTypeDescription, Me.colReceivedQuantity, Me.colBalanceQuantity, Me.colModuleName, Me.colCreatedBy, Me.colCreatedDate, Me.colModifiedBy, Me.colExeVersionNo, Me.colModifiedDate, Me.colEnteredOnMachineID})
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
        'colID
        '
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        Me.colID.Visible = True
        Me.colID.VisibleIndex = 0
        '
        'colPurchaseOrderNo
        '
        Me.colPurchaseOrderNo.FieldName = "PurchaseOrderNo"
        Me.colPurchaseOrderNo.Name = "colPurchaseOrderNo"
        Me.colPurchaseOrderNo.Visible = True
        Me.colPurchaseOrderNo.VisibleIndex = 1
        '
        'colPurchaseOrderDate
        '
        Me.colPurchaseOrderDate.FieldName = "PurchaseOrderDate"
        Me.colPurchaseOrderDate.Name = "colPurchaseOrderDate"
        Me.colPurchaseOrderDate.Visible = True
        Me.colPurchaseOrderDate.VisibleIndex = 2
        '
        'colPartyName
        '
        Me.colPartyName.FieldName = "PartyName"
        Me.colPartyName.Name = "colPartyName"
        Me.colPartyName.Visible = True
        Me.colPartyName.VisibleIndex = 3
        '
        'colCurrencyCode
        '
        Me.colCurrencyCode.FieldName = "CurrencyCode"
        Me.colCurrencyCode.Name = "colCurrencyCode"
        Me.colCurrencyCode.Visible = True
        Me.colCurrencyCode.VisibleIndex = 4
        '
        'colPurchaseOrderType
        '
        Me.colPurchaseOrderType.FieldName = "PurchaseOrderType"
        Me.colPurchaseOrderType.Name = "colPurchaseOrderType"
        Me.colPurchaseOrderType.Visible = True
        Me.colPurchaseOrderType.VisibleIndex = 5
        '
        'colMaterialCode
        '
        Me.colMaterialCode.FieldName = "MaterialCode"
        Me.colMaterialCode.Name = "colMaterialCode"
        Me.colMaterialCode.Visible = True
        Me.colMaterialCode.VisibleIndex = 6
        '
        'colMaterialDescription
        '
        Me.colMaterialDescription.FieldName = "MaterialDescription"
        Me.colMaterialDescription.Name = "colMaterialDescription"
        Me.colMaterialDescription.Visible = True
        Me.colMaterialDescription.VisibleIndex = 7
        '
        'colMaterialSize
        '
        Me.colMaterialSize.FieldName = "MaterialSize"
        Me.colMaterialSize.Name = "colMaterialSize"
        Me.colMaterialSize.Visible = True
        Me.colMaterialSize.VisibleIndex = 8
        '
        'colMaterialColorDescription
        '
        Me.colMaterialColorDescription.FieldName = "MaterialColorDescription"
        Me.colMaterialColorDescription.Name = "colMaterialColorDescription"
        Me.colMaterialColorDescription.Visible = True
        Me.colMaterialColorDescription.VisibleIndex = 9
        '
        'colUnit
        '
        Me.colUnit.FieldName = "Unit"
        Me.colUnit.Name = "colUnit"
        Me.colUnit.Visible = True
        Me.colUnit.VisibleIndex = 10
        '
        'colQuantity
        '
        Me.colQuantity.FieldName = "Quantity"
        Me.colQuantity.Name = "colQuantity"
        Me.colQuantity.Visible = True
        Me.colQuantity.VisibleIndex = 11
        '
        'colPrice
        '
        Me.colPrice.FieldName = "Price"
        Me.colPrice.Name = "colPrice"
        Me.colPrice.Visible = True
        Me.colPrice.VisibleIndex = 12
        '
        'colMaterialValue
        '
        Me.colMaterialValue.FieldName = "MaterialValue"
        Me.colMaterialValue.Name = "colMaterialValue"
        Me.colMaterialValue.Visible = True
        Me.colMaterialValue.VisibleIndex = 13
        '
        'colMaterialTypeDescription
        '
        Me.colMaterialTypeDescription.FieldName = "MaterialTypeDescription"
        Me.colMaterialTypeDescription.Name = "colMaterialTypeDescription"
        Me.colMaterialTypeDescription.Visible = True
        Me.colMaterialTypeDescription.VisibleIndex = 14
        '
        'colReceivedQuantity
        '
        Me.colReceivedQuantity.FieldName = "ReceivedQuantity"
        Me.colReceivedQuantity.Name = "colReceivedQuantity"
        Me.colReceivedQuantity.Visible = True
        Me.colReceivedQuantity.VisibleIndex = 15
        '
        'colBalanceQuantity
        '
        Me.colBalanceQuantity.FieldName = "BalanceQuantity"
        Me.colBalanceQuantity.Name = "colBalanceQuantity"
        Me.colBalanceQuantity.Visible = True
        Me.colBalanceQuantity.VisibleIndex = 16
        '
        'colModuleName
        '
        Me.colModuleName.FieldName = "ModuleName"
        Me.colModuleName.Name = "colModuleName"
        Me.colModuleName.Visible = True
        Me.colModuleName.VisibleIndex = 17
        '
        'colCreatedBy
        '
        Me.colCreatedBy.FieldName = "CreatedBy"
        Me.colCreatedBy.Name = "colCreatedBy"
        Me.colCreatedBy.Visible = True
        Me.colCreatedBy.VisibleIndex = 18
        '
        'colCreatedDate
        '
        Me.colCreatedDate.FieldName = "CreatedDate"
        Me.colCreatedDate.Name = "colCreatedDate"
        Me.colCreatedDate.Visible = True
        Me.colCreatedDate.VisibleIndex = 19
        '
        'colModifiedBy
        '
        Me.colModifiedBy.FieldName = "ModifiedBy"
        Me.colModifiedBy.Name = "colModifiedBy"
        Me.colModifiedBy.Visible = True
        Me.colModifiedBy.VisibleIndex = 20
        '
        'colExeVersionNo
        '
        Me.colExeVersionNo.FieldName = "ExeVersionNo"
        Me.colExeVersionNo.Name = "colExeVersionNo"
        Me.colExeVersionNo.Visible = True
        Me.colExeVersionNo.VisibleIndex = 21
        '
        'colModifiedDate
        '
        Me.colModifiedDate.FieldName = "ModifiedDate"
        Me.colModifiedDate.Name = "colModifiedDate"
        Me.colModifiedDate.Visible = True
        Me.colModifiedDate.VisibleIndex = 22
        '
        'colEnteredOnMachineID
        '
        Me.colEnteredOnMachineID.FieldName = "EnteredOnMachineID"
        Me.colEnteredOnMachineID.Name = "colEnteredOnMachineID"
        Me.colEnteredOnMachineID.Visible = True
        Me.colEnteredOnMachineID.VisibleIndex = 23
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.grdPurchaseOrder)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
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
        'Tmptbl_PurchaseOrderforSGMTableAdapter
        '
        Me.Tmptbl_PurchaseOrderforSGMTableAdapter.ClearBeforeFill = True
        '
        'Tmptbl_PurchaseOrderDetailsforSGMTableAdapter
        '
        Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.ClearBeforeFill = True
        '
        'frmAllinOne2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAllinOne2"
        Me.Text = "frmInvoice"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TmptblPurchaseOrderforSGMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DstmpPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseOrderV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdPurchaseOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPurchaseOrderV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DstmpPurchaseOrder As SolarERPForSGM.DstmpPurchaseOrder
    Friend WithEvents TmptblPurchaseOrderforSGMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Tmptbl_PurchaseOrderforSGMTableAdapter As SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderforSGMTableAdapter
    Friend WithEvents Tmptbl_PurchaseOrderDetailsforSGMTableAdapter As SolarERPForSGM.DstmpPurchaseOrderTableAdapters.tmptbl_PurchaseOrderDetailsforSGMTableAdapter
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPartyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCurrencyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialColorDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrice As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colID1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderNo1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderDate1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPartyName1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCurrencyCode1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPurchaseOrderType1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialCode1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialDescription1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialSize1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialColorDescription1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colUnit1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQuantity1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPrice1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialValue1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialTypeDescription1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colReceivedQuantity1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBalanceQuantity1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModuleName1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedBy1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colCreatedDate1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModifiedBy1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colExeVersionNo1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colModifiedDate1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEnteredOnMachineID1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
