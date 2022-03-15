<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesAnalysis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesAnalysis))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.cbxBrand = New System.Windows.Forms.ComboBox
        Me.cbxProductTypeMain = New System.Windows.Forms.ComboBox
        Me.cbxGroupBy = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbxSampleOrderType = New System.Windows.Forms.ComboBox
        Me.rbAll = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.rbSample = New System.Windows.Forms.RadioButton
        Me.rbProduction = New System.Windows.Forms.RadioButton
        Me.cbxProductType = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cbxArticleMould = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.tbArticleDescription = New System.Windows.Forms.TextBox
        Me.cbxArticleCode = New System.Windows.Forms.ComboBox
        Me.cbxGranuleType = New System.Windows.Forms.ComboBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.cbxTypeofDocument = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.grdSalesOrderDetails = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdOrderDtls = New DevExpress.XtraGrid.GridControl
        Me.grdOrderDtlsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrderDtls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrderDtlsV1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.grdSalesOrderDetails)
        Me.Panel1.Controls.Add(Me.grdOrderDtls)
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
        Me.pl.Size = New System.Drawing.Size(1198, 206)
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
        Me.Panel2.Size = New System.Drawing.Size(1184, 188)
        Me.Panel2.TabIndex = 12
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Ivory
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.cbxBrand)
        Me.Panel5.Controls.Add(Me.cbxProductTypeMain)
        Me.Panel5.Controls.Add(Me.cbxGroupBy)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.cbxSampleOrderType)
        Me.Panel5.Controls.Add(Me.rbAll)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.rbSample)
        Me.Panel5.Controls.Add(Me.rbProduction)
        Me.Panel5.Controls.Add(Me.cbxProductType)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Location = New System.Drawing.Point(645, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(381, 183)
        Me.Panel5.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(4, 151)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(107, 23)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Brand"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxBrand
        '
        Me.cbxBrand.FormattingEnabled = True
        Me.cbxBrand.Location = New System.Drawing.Point(110, 151)
        Me.cbxBrand.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxBrand.Name = "cbxBrand"
        Me.cbxBrand.Size = New System.Drawing.Size(262, 24)
        Me.cbxBrand.TabIndex = 42
        '
        'cbxProductTypeMain
        '
        Me.cbxProductTypeMain.FormattingEnabled = True
        Me.cbxProductTypeMain.Location = New System.Drawing.Point(110, 65)
        Me.cbxProductTypeMain.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductTypeMain.Name = "cbxProductTypeMain"
        Me.cbxProductTypeMain.Size = New System.Drawing.Size(262, 24)
        Me.cbxProductTypeMain.TabIndex = 41
        '
        'cbxGroupBy
        '
        Me.cbxGroupBy.FormattingEnabled = True
        Me.cbxGroupBy.Items.AddRange(New Object() {"Customer", "Moulds", "Customer / Moulds", "Moulds / Customer"})
        Me.cbxGroupBy.Location = New System.Drawing.Point(110, 119)
        Me.cbxGroupBy.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxGroupBy.Name = "cbxGroupBy"
        Me.cbxGroupBy.Size = New System.Drawing.Size(262, 24)
        Me.cbxGroupBy.TabIndex = 39
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(3, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 23)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Group By"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxSampleOrderType
        '
        Me.cbxSampleOrderType.Enabled = False
        Me.cbxSampleOrderType.FormattingEnabled = True
        Me.cbxSampleOrderType.Location = New System.Drawing.Point(110, 38)
        Me.cbxSampleOrderType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxSampleOrderType.Name = "cbxSampleOrderType"
        Me.cbxSampleOrderType.Size = New System.Drawing.Size(262, 24)
        Me.cbxSampleOrderType.TabIndex = 27
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Location = New System.Drawing.Point(114, 12)
        Me.rbAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(41, 20)
        Me.rbAll.TabIndex = 38
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 23)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Sample Type"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(4, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 23)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Type of Order"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbSample
        '
        Me.rbSample.AutoSize = True
        Me.rbSample.Location = New System.Drawing.Point(278, 11)
        Me.rbSample.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSample.Name = "rbSample"
        Me.rbSample.Size = New System.Drawing.Size(73, 20)
        Me.rbSample.TabIndex = 30
        Me.rbSample.Text = "Sample"
        Me.rbSample.UseVisualStyleBackColor = True
        '
        'rbProduction
        '
        Me.rbProduction.AutoSize = True
        Me.rbProduction.Location = New System.Drawing.Point(175, 11)
        Me.rbProduction.Margin = New System.Windows.Forms.Padding(4)
        Me.rbProduction.Name = "rbProduction"
        Me.rbProduction.Size = New System.Drawing.Size(96, 20)
        Me.rbProduction.TabIndex = 29
        Me.rbProduction.Text = "Production"
        Me.rbProduction.UseVisualStyleBackColor = True
        '
        'cbxProductType
        '
        Me.cbxProductType.FormattingEnabled = True
        Me.cbxProductType.Location = New System.Drawing.Point(110, 92)
        Me.cbxProductType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductType.Name = "cbxProductType"
        Me.cbxProductType.Size = New System.Drawing.Size(262, 24)
        Me.cbxProductType.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(4, 65)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(107, 23)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "Product Type"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Honeydew
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.cbxArticleMould)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.cbxCustomer)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.tbArticleDescription)
        Me.Panel4.Controls.Add(Me.cbxArticleCode)
        Me.Panel4.Controls.Add(Me.cbxGranuleType)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(413, 183)
        Me.Panel4.TabIndex = 14
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
        Me.Label10.Location = New System.Drawing.Point(4, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(138, 23)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Article Description"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxArticleMould
        '
        Me.cbxArticleMould.FormattingEnabled = True
        Me.cbxArticleMould.Location = New System.Drawing.Point(145, 65)
        Me.cbxArticleMould.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxArticleMould.Name = "cbxArticleMould"
        Me.cbxArticleMould.Size = New System.Drawing.Size(262, 24)
        Me.cbxArticleMould.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 65)
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
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 23)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Granule Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(3, 92)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(138, 23)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Article Code"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbArticleDescription
        '
        Me.tbArticleDescription.Location = New System.Drawing.Point(145, 119)
        Me.tbArticleDescription.Name = "tbArticleDescription"
        Me.tbArticleDescription.Size = New System.Drawing.Size(262, 23)
        Me.tbArticleDescription.TabIndex = 24
        '
        'cbxArticleCode
        '
        Me.cbxArticleCode.FormattingEnabled = True
        Me.cbxArticleCode.Location = New System.Drawing.Point(145, 92)
        Me.cbxArticleCode.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxArticleCode.Name = "cbxArticleCode"
        Me.cbxArticleCode.Size = New System.Drawing.Size(262, 24)
        Me.cbxArticleCode.TabIndex = 33
        '
        'cbxGranuleType
        '
        Me.cbxGranuleType.FormattingEnabled = True
        Me.cbxGranuleType.Location = New System.Drawing.Point(145, 38)
        Me.cbxGranuleType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxGranuleType.Name = "cbxGranuleType"
        Me.cbxGranuleType.Size = New System.Drawing.Size(262, 24)
        Me.cbxGranuleType.TabIndex = 25
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Ivory
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.cbxTypeofDocument)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.dpTo)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 183)
        Me.Panel3.TabIndex = 13
        '
        'cbxTypeofDocument
        '
        Me.cbxTypeofDocument.FormattingEnabled = True
        Me.cbxTypeofDocument.Items.AddRange(New Object() {"INVOICE", "ORDER", "CREDIT NOTE"})
        Me.cbxTypeofDocument.Location = New System.Drawing.Point(7, 103)
        Me.cbxTypeofDocument.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxTypeofDocument.Name = "cbxTypeofDocument"
        Me.cbxTypeofDocument.Size = New System.Drawing.Size(207, 24)
        Me.cbxTypeofDocument.TabIndex = 40
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(41, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(138, 23)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Type of Document"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'grdSalesOrderDetails
        '
        Me.grdSalesOrderDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdSalesOrderDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdSalesOrderDetails.Location = New System.Drawing.Point(21, 219)
        Me.grdSalesOrderDetails.MainView = Me.grdSalesOrderDetailsV1
        Me.grdSalesOrderDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderDetails.Name = "grdSalesOrderDetails"
        Me.grdSalesOrderDetails.Size = New System.Drawing.Size(1182, 401)
        Me.grdSalesOrderDetails.TabIndex = 18
        Me.grdSalesOrderDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderDetailsV1})
        '
        'grdSalesOrderDetailsV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdSalesOrderDetailsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdSalesOrderDetailsV1.GridControl = Me.grdSalesOrderDetails
        Me.grdSalesOrderDetailsV1.Name = "grdSalesOrderDetailsV1"
        Me.grdSalesOrderDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderDetailsV1.OptionsView.ShowFooter = True
        '
        'grdOrderDtls
        '
        Me.grdOrderDtls.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOrderDtls.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdOrderDtls.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdOrderDtls.Location = New System.Drawing.Point(21, 219)
        Me.grdOrderDtls.MainView = Me.grdOrderDtlsV1
        Me.grdOrderDtls.Margin = New System.Windows.Forms.Padding(4)
        Me.grdOrderDtls.Name = "grdOrderDtls"
        Me.grdOrderDtls.Size = New System.Drawing.Size(1182, 402)
        Me.grdOrderDtls.TabIndex = 19
        Me.grdOrderDtls.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdOrderDtlsV1})
        Me.grdOrderDtls.Visible = False
        '
        'grdOrderDtlsV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdOrderDtlsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdOrderDtlsV1.GridControl = Me.grdOrderDtls
        Me.grdOrderDtlsV1.Name = "grdOrderDtlsV1"
        Me.grdOrderDtlsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdOrderDtlsV1.OptionsView.ShowFooter = True
        '
        'frmSalesAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSalesAnalysis"
        Me.Text = "frmSalesAnalysis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrderDtls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrderDtlsV1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cbxSampleOrderType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbxGranuleType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbxArticleCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cbxProductType As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rbSample As System.Windows.Forms.RadioButton
    Friend WithEvents rbProduction As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents grdSalesOrderDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbxGroupBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbxTypeofDocument As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdOrderDtls As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdOrderDtlsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbxProductTypeMain As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbxBrand As System.Windows.Forms.ComboBox
End Class
