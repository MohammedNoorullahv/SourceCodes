<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockByMonthLedger
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockByMonthLedger))
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.grdLocation = New DevExpress.XtraGrid.GridControl
        Me.LocationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsLocation = New SolarERPForSGM.dsLocation
        Me.grdLocationV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colCompanyCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLocationCode = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLocationName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.tbLocation = New System.Windows.Forms.TextBox
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbLocation = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.grdLedger = New DevExpress.XtraGrid.GridControl
        Me.grdLedgerV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.colPKID = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSystemIp = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colFromDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colToDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colLocation = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colMaterialCode1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colDescription1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colSize1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTranDate = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTransactionType = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colTransactionNo = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colOpeningStock = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colArrival = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colIssue = New DevExpress.XtraGrid.Columns.GridColumn
        Me.colClosingStock = New DevExpress.XtraGrid.Columns.GridColumn
        Me.cbExporttoExcel = New System.Windows.Forms.Button
        Me.LocationTableAdapter = New SolarERPForSGM.dsLocationTableAdapters.LocationTableAdapter
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LocationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLocationV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLedgerV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.cbExporttoExcel)
        Me.Panel1.Location = New System.Drawing.Point(7, 12)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1235, 719)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel3.Controls.Add(Me.grdLocation)
        Me.Panel3.Controls.Add(Me.tbLocation)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.cbLocation)
        Me.Panel3.Location = New System.Drawing.Point(6, 16)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(500, 551)
        Me.Panel3.TabIndex = 7
        '
        'grdLocation
        '
        Me.grdLocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLocation.DataSource = Me.LocationBindingSource
        Me.grdLocation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdLocation.Location = New System.Drawing.Point(5, 65)
        Me.grdLocation.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdLocation.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdLocation.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdLocation.MainView = Me.grdLocationV1
        Me.grdLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.Size = New System.Drawing.Size(491, 480)
        Me.grdLocation.TabIndex = 114
        Me.grdLocation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdLocationV1})
        Me.grdLocation.Visible = False
        '
        'LocationBindingSource
        '
        Me.LocationBindingSource.DataMember = "Location"
        Me.LocationBindingSource.DataSource = Me.DsLocation
        '
        'DsLocation
        '
        Me.DsLocation.DataSetName = "dsLocation"
        Me.DsLocation.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'grdLocationV1
        '
        Me.grdLocationV1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.grdLocationV1.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.Empty.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.grdLocationV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.EvenRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.grdLocationV1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.grdLocationV1.Appearance.FixedLine.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(91, Byte), Integer))
        Me.grdLocationV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupButton.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.GroupRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.GroupRow.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.grdLocationV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdLocationV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.grdLocationV1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.HorzLine.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.grdLocationV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.OddRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.grdLocationV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.grdLocationV1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.grdLocationV1.Appearance.Preview.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.Preview.Options.UseFont = True
        Me.grdLocationV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.grdLocationV1.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.grdLocationV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdLocationV1.Appearance.Row.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.Row.Options.UseBorderColor = True
        Me.grdLocationV1.Appearance.Row.Options.UseForeColor = True
        Me.grdLocationV1.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.grdLocationV1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.grdLocationV1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.grdLocationV1.Appearance.TopNewRow.Options.UseBackColor = True
        Me.grdLocationV1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.grdLocationV1.Appearance.VertLine.Options.UseBackColor = True
        Me.grdLocationV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colCompanyCode, Me.colLocationCode, Me.colLocationName})
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition1.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdLocationV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdLocationV1.GridControl = Me.grdLocation
        Me.grdLocationV1.Name = "grdLocationV1"
        Me.grdLocationV1.OptionsView.EnableAppearanceEvenRow = True
        Me.grdLocationV1.OptionsView.EnableAppearanceOddRow = True
        Me.grdLocationV1.OptionsView.ShowAutoFilterRow = True
        Me.grdLocationV1.OptionsView.ShowFooter = True
        Me.grdLocationV1.OptionsView.ShowGroupPanel = False
        '
        'colCompanyCode
        '
        Me.colCompanyCode.FieldName = "CompanyCode"
        Me.colCompanyCode.Name = "colCompanyCode"
        Me.colCompanyCode.Visible = True
        Me.colCompanyCode.VisibleIndex = 0
        '
        'colLocationCode
        '
        Me.colLocationCode.FieldName = "LocationCode"
        Me.colLocationCode.Name = "colLocationCode"
        Me.colLocationCode.Visible = True
        Me.colLocationCode.VisibleIndex = 1
        '
        'colLocationName
        '
        Me.colLocationName.FieldName = "LocationName"
        Me.colLocationName.Name = "colLocationName"
        Me.colLocationName.Visible = True
        Me.colLocationName.VisibleIndex = 2
        '
        'tbLocation
        '
        Me.tbLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbLocation.Location = New System.Drawing.Point(109, 34)
        Me.tbLocation.Name = "tbLocation"
        Me.tbLocation.Size = New System.Drawing.Size(164, 23)
        Me.tbLocation.TabIndex = 7
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "MMMM - yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(109, 5)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(164, 23)
        Me.dpFrom.TabIndex = 5
        Me.dpFrom.Value = New Date(2018, 5, 4, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(5, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Month :-"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbLocation
        '
        Me.cbLocation.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbLocation.Location = New System.Drawing.Point(5, 34)
        Me.cbLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.cbLocation.Name = "cbLocation"
        Me.cbLocation.Size = New System.Drawing.Size(100, 23)
        Me.cbLocation.TabIndex = 10
        Me.cbLocation.Text = "&Location"
        Me.cbLocation.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(537, 662)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(229, 16)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "01. 29-May-18 - Stock By Month"
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(1051, 578)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(171, 63)
        Me.cbExit.TabIndex = 2
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbReferesh
        '
        Me.cbReferesh.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbReferesh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReferesh.Image = CType(resources.GetObject("cbReferesh.Image"), System.Drawing.Image)
        Me.cbReferesh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbReferesh.Location = New System.Drawing.Point(13, 578)
        Me.cbReferesh.Margin = New System.Windows.Forms.Padding(4)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(171, 63)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel2.Controls.Add(Me.grdLedger)
        Me.Panel2.Location = New System.Drawing.Point(512, 16)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(717, 551)
        Me.Panel2.TabIndex = 0
        '
        'grdLedger
        '
        Me.grdLedger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLedger.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdLedger.Location = New System.Drawing.Point(4, 4)
        Me.grdLedger.MainView = Me.grdLedgerV1
        Me.grdLedger.Margin = New System.Windows.Forms.Padding(4)
        Me.grdLedger.Name = "grdLedger"
        Me.grdLedger.Size = New System.Drawing.Size(706, 540)
        Me.grdLedger.TabIndex = 113
        Me.grdLedger.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdLedgerV1})
        '
        'grdLedgerV1
        '
        Me.grdLedgerV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPKID, Me.colSystemIp, Me.colFromDate, Me.colToDate, Me.colLocation, Me.colMaterialCode1, Me.colDescription1, Me.colSize1, Me.colTranDate, Me.colTransactionType, Me.colTransactionNo, Me.colOpeningStock, Me.colArrival, Me.colIssue, Me.colClosingStock})
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.ApplyToRow = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdLedgerV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdLedgerV1.GridControl = Me.grdLedger
        Me.grdLedgerV1.Name = "grdLedgerV1"
        Me.grdLedgerV1.OptionsView.ColumnAutoWidth = False
        Me.grdLedgerV1.OptionsView.ShowAutoFilterRow = True
        Me.grdLedgerV1.OptionsView.ShowFooter = True
        '
        'colPKID
        '
        Me.colPKID.FieldName = "PKID"
        Me.colPKID.Name = "colPKID"
        Me.colPKID.OptionsColumn.ReadOnly = True
        Me.colPKID.Visible = True
        Me.colPKID.VisibleIndex = 0
        '
        'colSystemIp
        '
        Me.colSystemIp.FieldName = "SystemIp"
        Me.colSystemIp.Name = "colSystemIp"
        Me.colSystemIp.Visible = True
        Me.colSystemIp.VisibleIndex = 1
        '
        'colFromDate
        '
        Me.colFromDate.FieldName = "FromDate"
        Me.colFromDate.Name = "colFromDate"
        Me.colFromDate.Visible = True
        Me.colFromDate.VisibleIndex = 2
        '
        'colToDate
        '
        Me.colToDate.FieldName = "ToDate"
        Me.colToDate.Name = "colToDate"
        Me.colToDate.Visible = True
        Me.colToDate.VisibleIndex = 3
        '
        'colLocation
        '
        Me.colLocation.FieldName = "Location"
        Me.colLocation.Name = "colLocation"
        Me.colLocation.Visible = True
        Me.colLocation.VisibleIndex = 4
        '
        'colMaterialCode1
        '
        Me.colMaterialCode1.FieldName = "MaterialCode"
        Me.colMaterialCode1.Name = "colMaterialCode1"
        Me.colMaterialCode1.Visible = True
        Me.colMaterialCode1.VisibleIndex = 5
        '
        'colDescription1
        '
        Me.colDescription1.FieldName = "Description"
        Me.colDescription1.Name = "colDescription1"
        Me.colDescription1.Visible = True
        Me.colDescription1.VisibleIndex = 6
        '
        'colSize1
        '
        Me.colSize1.FieldName = "Size"
        Me.colSize1.Name = "colSize1"
        Me.colSize1.Visible = True
        Me.colSize1.VisibleIndex = 7
        '
        'colTranDate
        '
        Me.colTranDate.FieldName = "TranDate"
        Me.colTranDate.Name = "colTranDate"
        Me.colTranDate.Visible = True
        Me.colTranDate.VisibleIndex = 8
        '
        'colTransactionType
        '
        Me.colTransactionType.FieldName = "TransactionType"
        Me.colTransactionType.Name = "colTransactionType"
        Me.colTransactionType.Visible = True
        Me.colTransactionType.VisibleIndex = 9
        '
        'colTransactionNo
        '
        Me.colTransactionNo.FieldName = "TransactionNo"
        Me.colTransactionNo.Name = "colTransactionNo"
        Me.colTransactionNo.Visible = True
        Me.colTransactionNo.VisibleIndex = 10
        '
        'colOpeningStock
        '
        Me.colOpeningStock.FieldName = "OpeningStock"
        Me.colOpeningStock.Name = "colOpeningStock"
        Me.colOpeningStock.Visible = True
        Me.colOpeningStock.VisibleIndex = 11
        '
        'colArrival
        '
        Me.colArrival.FieldName = "Arrival"
        Me.colArrival.Name = "colArrival"
        Me.colArrival.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colArrival.Visible = True
        Me.colArrival.VisibleIndex = 12
        '
        'colIssue
        '
        Me.colIssue.FieldName = "Issue"
        Me.colIssue.Name = "colIssue"
        Me.colIssue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.colIssue.Visible = True
        Me.colIssue.VisibleIndex = 13
        '
        'colClosingStock
        '
        Me.colClosingStock.FieldName = "ClosingStock"
        Me.colClosingStock.Name = "colClosingStock"
        Me.colClosingStock.Visible = True
        Me.colClosingStock.VisibleIndex = 14
        '
        'cbExporttoExcel
        '
        Me.cbExporttoExcel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbExporttoExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExporttoExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExporttoExcel.Location = New System.Drawing.Point(516, 578)
        Me.cbExporttoExcel.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExporttoExcel.Name = "cbExporttoExcel"
        Me.cbExporttoExcel.Size = New System.Drawing.Size(171, 63)
        Me.cbExporttoExcel.TabIndex = 8
        Me.cbExporttoExcel.Text = "Export to Excel"
        Me.cbExporttoExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExporttoExcel.UseVisualStyleBackColor = True
        '
        'LocationTableAdapter
        '
        Me.LocationTableAdapter.ClearBeforeFill = True
        '
        'frmStockByMonthLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cbExit
        Me.ClientSize = New System.Drawing.Size(1242, 730)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmStockByMonthLedger"
        Me.Text = "Ledger"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LocationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLocationV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdLedger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLedgerV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grdLedger As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdLedgerV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbLocation As System.Windows.Forms.Button
    Friend WithEvents tbLocation As System.Windows.Forms.TextBox
    Friend WithEvents grdLocation As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdLocationV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DsLocation As SolarERPForSGM.dsLocation
    Friend WithEvents LocationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LocationTableAdapter As SolarERPForSGM.dsLocationTableAdapters.LocationTableAdapter
    Friend WithEvents colCompanyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLocationCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLocationName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cbExporttoExcel As System.Windows.Forms.Button
    Friend WithEvents colPKID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSystemIp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colFromDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colToDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLocation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMaterialCode1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDescription1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSize1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTranDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTransactionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTransactionNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colOpeningStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colArrival As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colIssue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colClosingStock As DevExpress.XtraGrid.Columns.GridColumn

End Class
