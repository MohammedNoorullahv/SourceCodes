<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoiceVer2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceVer2))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdInvoices = New DevExpress.XtraGrid.GridControl
        Me.grdInvoicesV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.pl = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbxArticleName = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.plTypeofInvoice = New System.Windows.Forms.GroupBox
        Me.chkbxFormH = New System.Windows.Forms.CheckBox
        Me.chkbxExport = New System.Windows.Forms.CheckBox
        Me.chkbxFormC = New System.Windows.Forms.CheckBox
        Me.chkbxCT3 = New System.Windows.Forms.CheckBox
        Me.chkbxAll = New System.Windows.Forms.CheckBox
        Me.plTypeofOrder = New System.Windows.Forms.GroupBox
        Me.chkbxAllOrder = New System.Windows.Forms.CheckBox
        Me.chkbxGeneral = New System.Windows.Forms.CheckBox
        Me.chkbxJobWorkOrder = New System.Windows.Forms.CheckBox
        Me.chkbxSalesOrder = New System.Windows.Forms.CheckBox
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.plSelectionCriteria = New System.Windows.Forms.GroupBox
        Me.cbxCodification = New System.Windows.Forms.ComboBox
        Me.rbCodification = New System.Windows.Forms.RadioButton
        Me.rbArticleName = New System.Windows.Forms.RadioButton
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdGeneralInvoices = New DevExpress.XtraGrid.GridControl
        Me.grdGeneralInvoicesV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        CType(Me.grdInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInvoicesV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.plTypeofInvoice.SuspendLayout()
        Me.plTypeofOrder.SuspendLayout()
        Me.plSelectionCriteria.SuspendLayout()
        CType(Me.grdGeneralInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdGeneralInvoicesV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdInvoices)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.grdGeneralInvoices)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'grdInvoices
        '
        Me.grdInvoices.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdInvoices.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdInvoices.Location = New System.Drawing.Point(12, 260)
        Me.grdInvoices.MainView = Me.grdInvoicesV1
        Me.grdInvoices.Margin = New System.Windows.Forms.Padding(4)
        Me.grdInvoices.Name = "grdInvoices"
        Me.grdInvoices.Size = New System.Drawing.Size(1182, 389)
        Me.grdInvoices.TabIndex = 9
        Me.grdInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdInvoicesV1})
        '
        'grdInvoicesV1
        '
        Me.grdInvoicesV1.GridControl = Me.grdInvoices
        Me.grdInvoicesV1.Name = "grdInvoicesV1"
        Me.grdInvoicesV1.OptionsView.ColumnAutoWidth = False
        Me.grdInvoicesV1.OptionsView.ShowAutoFilterRow = True
        Me.grdInvoicesV1.OptionsView.ShowFooter = True
        '
        'cbPrint
        '
        Me.cbPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPrint.Location = New System.Drawing.Point(266, 657)
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
        Me.Export2Excel.Location = New System.Drawing.Point(135, 657)
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
        Me.cbExit.Location = New System.Drawing.Point(1071, 657)
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
        Me.cbReferesh.Location = New System.Drawing.Point(4, 657)
        Me.cbReferesh.Margin = New System.Windows.Forms.Padding(4)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'pl
        '
        Me.pl.Controls.Add(Me.Label4)
        Me.pl.Controls.Add(Me.cbxArticleName)
        Me.pl.Controls.Add(Me.Label3)
        Me.pl.Controls.Add(Me.plTypeofInvoice)
        Me.pl.Controls.Add(Me.plTypeofOrder)
        Me.pl.Controls.Add(Me.cbxCustomer)
        Me.pl.Controls.Add(Me.plSelectionCriteria)
        Me.pl.Controls.Add(Me.dpTo)
        Me.pl.Controls.Add(Me.dpFrom)
        Me.pl.Controls.Add(Me.Label2)
        Me.pl.Controls.Add(Me.Label1)
        Me.pl.Location = New System.Drawing.Point(13, 12)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1272, 233)
        Me.pl.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(251, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Articles :-"
        '
        'cbxArticleName
        '
        Me.cbxArticleName.FormattingEnabled = True
        Me.cbxArticleName.Location = New System.Drawing.Point(356, 42)
        Me.cbxArticleName.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxArticleName.Name = "cbxArticleName"
        Me.cbxArticleName.Size = New System.Drawing.Size(317, 24)
        Me.cbxArticleName.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(251, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Customer :-"
        '
        'plTypeofInvoice
        '
        Me.plTypeofInvoice.Controls.Add(Me.chkbxFormH)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxExport)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxFormC)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxCT3)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxAll)
        Me.plTypeofInvoice.Location = New System.Drawing.Point(955, 92)
        Me.plTypeofInvoice.Margin = New System.Windows.Forms.Padding(4)
        Me.plTypeofInvoice.Name = "plTypeofInvoice"
        Me.plTypeofInvoice.Padding = New System.Windows.Forms.Padding(4)
        Me.plTypeofInvoice.Size = New System.Drawing.Size(300, 122)
        Me.plTypeofInvoice.TabIndex = 9
        Me.plTypeofInvoice.TabStop = False
        Me.plTypeofInvoice.Text = "Type Of Invoice"
        '
        'chkbxFormH
        '
        Me.chkbxFormH.AutoSize = True
        Me.chkbxFormH.Enabled = False
        Me.chkbxFormH.Location = New System.Drawing.Point(24, 87)
        Me.chkbxFormH.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxFormH.Name = "chkbxFormH"
        Me.chkbxFormH.Size = New System.Drawing.Size(73, 20)
        Me.chkbxFormH.TabIndex = 4
        Me.chkbxFormH.Text = "Form H"
        Me.chkbxFormH.UseVisualStyleBackColor = True
        '
        'chkbxExport
        '
        Me.chkbxExport.AutoSize = True
        Me.chkbxExport.Enabled = False
        Me.chkbxExport.Location = New System.Drawing.Point(144, 60)
        Me.chkbxExport.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxExport.Name = "chkbxExport"
        Me.chkbxExport.Size = New System.Drawing.Size(69, 20)
        Me.chkbxExport.TabIndex = 3
        Me.chkbxExport.Text = "Export"
        Me.chkbxExport.UseVisualStyleBackColor = True
        '
        'chkbxFormC
        '
        Me.chkbxFormC.AutoSize = True
        Me.chkbxFormC.Enabled = False
        Me.chkbxFormC.Location = New System.Drawing.Point(144, 25)
        Me.chkbxFormC.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxFormC.Name = "chkbxFormC"
        Me.chkbxFormC.Size = New System.Drawing.Size(73, 20)
        Me.chkbxFormC.TabIndex = 2
        Me.chkbxFormC.Text = "Form C"
        Me.chkbxFormC.UseVisualStyleBackColor = True
        '
        'chkbxCT3
        '
        Me.chkbxCT3.AutoSize = True
        Me.chkbxCT3.Enabled = False
        Me.chkbxCT3.Location = New System.Drawing.Point(24, 58)
        Me.chkbxCT3.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxCT3.Name = "chkbxCT3"
        Me.chkbxCT3.Size = New System.Drawing.Size(53, 20)
        Me.chkbxCT3.TabIndex = 1
        Me.chkbxCT3.Text = "CT3"
        Me.chkbxCT3.UseVisualStyleBackColor = True
        '
        'chkbxAll
        '
        Me.chkbxAll.AutoSize = True
        Me.chkbxAll.Checked = True
        Me.chkbxAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxAll.Location = New System.Drawing.Point(24, 28)
        Me.chkbxAll.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxAll.Name = "chkbxAll"
        Me.chkbxAll.Size = New System.Drawing.Size(42, 20)
        Me.chkbxAll.TabIndex = 0
        Me.chkbxAll.Text = "All"
        Me.chkbxAll.UseVisualStyleBackColor = True
        '
        'plTypeofOrder
        '
        Me.plTypeofOrder.Controls.Add(Me.chkbxAllOrder)
        Me.plTypeofOrder.Controls.Add(Me.chkbxGeneral)
        Me.plTypeofOrder.Controls.Add(Me.chkbxJobWorkOrder)
        Me.plTypeofOrder.Controls.Add(Me.chkbxSalesOrder)
        Me.plTypeofOrder.Location = New System.Drawing.Point(682, 92)
        Me.plTypeofOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.plTypeofOrder.Name = "plTypeofOrder"
        Me.plTypeofOrder.Padding = New System.Windows.Forms.Padding(4)
        Me.plTypeofOrder.Size = New System.Drawing.Size(265, 122)
        Me.plTypeofOrder.TabIndex = 8
        Me.plTypeofOrder.TabStop = False
        Me.plTypeofOrder.Text = "Type Of Order"
        '
        'chkbxAllOrder
        '
        Me.chkbxAllOrder.AutoSize = True
        Me.chkbxAllOrder.Checked = True
        Me.chkbxAllOrder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxAllOrder.Location = New System.Drawing.Point(17, 28)
        Me.chkbxAllOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxAllOrder.Name = "chkbxAllOrder"
        Me.chkbxAllOrder.Size = New System.Drawing.Size(90, 20)
        Me.chkbxAllOrder.TabIndex = 4
        Me.chkbxAllOrder.Text = "All Orders"
        Me.chkbxAllOrder.UseVisualStyleBackColor = True
        '
        'chkbxGeneral
        '
        Me.chkbxGeneral.AutoSize = True
        Me.chkbxGeneral.Enabled = False
        Me.chkbxGeneral.Location = New System.Drawing.Point(129, 87)
        Me.chkbxGeneral.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxGeneral.Name = "chkbxGeneral"
        Me.chkbxGeneral.Size = New System.Drawing.Size(129, 20)
        Me.chkbxGeneral.TabIndex = 3
        Me.chkbxGeneral.Text = "General Invoice"
        Me.chkbxGeneral.UseVisualStyleBackColor = True
        '
        'chkbxJobWorkOrder
        '
        Me.chkbxJobWorkOrder.AutoSize = True
        Me.chkbxJobWorkOrder.Enabled = False
        Me.chkbxJobWorkOrder.Location = New System.Drawing.Point(129, 58)
        Me.chkbxJobWorkOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxJobWorkOrder.Name = "chkbxJobWorkOrder"
        Me.chkbxJobWorkOrder.Size = New System.Drawing.Size(123, 20)
        Me.chkbxJobWorkOrder.TabIndex = 1
        Me.chkbxJobWorkOrder.Text = "JobWork Order"
        Me.chkbxJobWorkOrder.UseVisualStyleBackColor = True
        '
        'chkbxSalesOrder
        '
        Me.chkbxSalesOrder.AutoSize = True
        Me.chkbxSalesOrder.Enabled = False
        Me.chkbxSalesOrder.Location = New System.Drawing.Point(129, 28)
        Me.chkbxSalesOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbxSalesOrder.Name = "chkbxSalesOrder"
        Me.chkbxSalesOrder.Size = New System.Drawing.Size(103, 20)
        Me.chkbxSalesOrder.TabIndex = 0
        Me.chkbxSalesOrder.Text = "Sales Order"
        Me.chkbxSalesOrder.UseVisualStyleBackColor = True
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(356, 10)
        Me.cbxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(432, 24)
        Me.cbxCustomer.TabIndex = 7
        '
        'plSelectionCriteria
        '
        Me.plSelectionCriteria.Controls.Add(Me.cbxCodification)
        Me.plSelectionCriteria.Controls.Add(Me.rbCodification)
        Me.plSelectionCriteria.Controls.Add(Me.rbArticleName)
        Me.plSelectionCriteria.Enabled = False
        Me.plSelectionCriteria.Location = New System.Drawing.Point(21, 83)
        Me.plSelectionCriteria.Margin = New System.Windows.Forms.Padding(4)
        Me.plSelectionCriteria.Name = "plSelectionCriteria"
        Me.plSelectionCriteria.Padding = New System.Windows.Forms.Padding(4)
        Me.plSelectionCriteria.Size = New System.Drawing.Size(347, 105)
        Me.plSelectionCriteria.TabIndex = 6
        Me.plSelectionCriteria.TabStop = False
        Me.plSelectionCriteria.Text = "Selection Critirea :-"
        Me.plSelectionCriteria.Visible = False
        '
        'cbxCodification
        '
        Me.cbxCodification.Enabled = False
        Me.cbxCodification.FormattingEnabled = True
        Me.cbxCodification.Location = New System.Drawing.Point(174, 55)
        Me.cbxCodification.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCodification.Name = "cbxCodification"
        Me.cbxCodification.Size = New System.Drawing.Size(162, 24)
        Me.cbxCodification.TabIndex = 9
        '
        'rbCodification
        '
        Me.rbCodification.AutoSize = True
        Me.rbCodification.Location = New System.Drawing.Point(28, 54)
        Me.rbCodification.Margin = New System.Windows.Forms.Padding(4)
        Me.rbCodification.Name = "rbCodification"
        Me.rbCodification.Size = New System.Drawing.Size(142, 20)
        Me.rbCodification.TabIndex = 1
        Me.rbCodification.TabStop = True
        Me.rbCodification.Text = "By Codification :-"
        Me.rbCodification.UseVisualStyleBackColor = True
        '
        'rbArticleName
        '
        Me.rbArticleName.AutoSize = True
        Me.rbArticleName.Location = New System.Drawing.Point(21, 26)
        Me.rbArticleName.Margin = New System.Windows.Forms.Padding(4)
        Me.rbArticleName.Name = "rbArticleName"
        Me.rbArticleName.Size = New System.Drawing.Size(148, 20)
        Me.rbArticleName.TabIndex = 0
        Me.rbArticleName.TabStop = True
        Me.rbArticleName.Text = "By Article Name :-"
        Me.rbArticleName.UseVisualStyleBackColor = True
        '
        'dpTo
        '
        Me.dpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpTo.Location = New System.Drawing.Point(89, 42)
        Me.dpTo.Margin = New System.Windows.Forms.Padding(4)
        Me.dpTo.Name = "dpTo"
        Me.dpTo.Size = New System.Drawing.Size(139, 23)
        Me.dpTo.TabIndex = 3
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(89, 10)
        Me.dpFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(139, 23)
        Me.dpFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 45)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To :-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 13)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From :-"
        '
        'grdGeneralInvoices
        '
        Me.grdGeneralInvoices.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdGeneralInvoices.Location = New System.Drawing.Point(12, 260)
        Me.grdGeneralInvoices.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdGeneralInvoices.MainView = Me.grdGeneralInvoicesV1
        Me.grdGeneralInvoices.Margin = New System.Windows.Forms.Padding(4)
        Me.grdGeneralInvoices.Name = "grdGeneralInvoices"
        Me.grdGeneralInvoices.Size = New System.Drawing.Size(1182, 389)
        Me.grdGeneralInvoices.TabIndex = 8
        Me.grdGeneralInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdGeneralInvoicesV1})
        '
        'grdGeneralInvoicesV1
        '
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.Empty.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdGeneralInvoicesV1.Appearance.EvenRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FixedLine.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdGeneralInvoicesV1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupButton.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupButton.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdGeneralInvoicesV1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.GroupRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupRow.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grdGeneralInvoicesV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.HorzLine.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdGeneralInvoicesV1.Appearance.OddRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.grdGeneralInvoicesV1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.Preview.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.Preview.Options.UseFont = True
        Me.grdGeneralInvoicesV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdGeneralInvoicesV1.Appearance.Row.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.Row.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grdGeneralInvoicesV1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.grdGeneralInvoicesV1.Appearance.TopNewRow.Options.UseBackColor = True
        Me.grdGeneralInvoicesV1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.grdGeneralInvoicesV1.Appearance.VertLine.Options.UseBackColor = True
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdGeneralInvoicesV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdGeneralInvoicesV1.GridControl = Me.grdGeneralInvoices
        Me.grdGeneralInvoicesV1.Name = "grdGeneralInvoicesV1"
        Me.grdGeneralInvoicesV1.OptionsView.ColumnAutoWidth = False
        Me.grdGeneralInvoicesV1.OptionsView.EnableAppearanceEvenRow = True
        Me.grdGeneralInvoicesV1.OptionsView.EnableAppearanceOddRow = True
        Me.grdGeneralInvoicesV1.OptionsView.ShowAutoFilterRow = True
        Me.grdGeneralInvoicesV1.OptionsView.ShowFooter = True
        '
        'frmInvoiceVer2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 742)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInvoiceVer2"
        Me.Text = "frmInvoice"
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInvoicesV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl.ResumeLayout(False)
        Me.pl.PerformLayout()
        Me.plTypeofInvoice.ResumeLayout(False)
        Me.plTypeofInvoice.PerformLayout()
        Me.plTypeofOrder.ResumeLayout(False)
        Me.plTypeofOrder.PerformLayout()
        Me.plSelectionCriteria.ResumeLayout(False)
        Me.plSelectionCriteria.PerformLayout()
        CType(Me.grdGeneralInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdGeneralInvoicesV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents plSelectionCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents rbCodification As System.Windows.Forms.RadioButton
    Friend WithEvents rbArticleName As System.Windows.Forms.RadioButton
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cbxCodification As System.Windows.Forms.ComboBox
    Friend WithEvents cbxArticleName As System.Windows.Forms.ComboBox
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents plTypeofInvoice As System.Windows.Forms.GroupBox
    Friend WithEvents chkbxFormH As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxExport As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxFormC As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxCT3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxAll As System.Windows.Forms.CheckBox
    Friend WithEvents plTypeofOrder As System.Windows.Forms.GroupBox
    Friend WithEvents chkbxJobWorkOrder As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxSalesOrder As System.Windows.Forms.CheckBox
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents chkbxGeneral As System.Windows.Forms.CheckBox
    Friend WithEvents grdGeneralInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdGeneralInvoicesV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdInvoicesV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkbxAllOrder As System.Windows.Forms.CheckBox
End Class
