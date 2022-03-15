<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmERPTrackingSystemv1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmERPTrackingSystemv1))
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition3 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition4 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.plVersion2 = New System.Windows.Forms.Panel
        Me.cbHideDetails = New System.Windows.Forms.Button
        Me.grdSalesOrderGBOrder = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderGBOrderV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdSalesOrderGBArticle = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderGBArticleV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbTrack = New System.Windows.Forms.Button
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbAnalytic = New System.Windows.Forms.RadioButton
        Me.rbSynthetic = New System.Windows.Forms.RadioButton
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.rbGroupByOrder = New System.Windows.Forms.RadioButton
        Me.rbGroupByArticle = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.rbNo = New System.Windows.Forms.RadioButton
        Me.rbYes = New System.Windows.Forms.RadioButton
        Me.cbxProductType = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.cbxProductTypeMain = New System.Windows.Forms.ComboBox
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
        Me.grdSalesOrder = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderv1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdSalesOrderDetails = New DevExpress.XtraGrid.GridControl
        Me.grdSalesOrderDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        Me.plVersion2.SuspendLayout()
        CType(Me.grdSalesOrderGBOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderGBOrderV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderGBArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderGBArticleV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.grdSalesOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.plVersion2)
        Me.Panel1.Controls.Add(Me.cbTrack)
        Me.Panel1.Controls.Add(Me.pgbar)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.grdSalesOrder)
        Me.Panel1.Controls.Add(Me.grdSalesOrderDetails)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'plVersion2
        '
        Me.plVersion2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plVersion2.Controls.Add(Me.cbHideDetails)
        Me.plVersion2.Controls.Add(Me.grdSalesOrderGBOrder)
        Me.plVersion2.Controls.Add(Me.grdSalesOrderGBArticle)
        Me.plVersion2.Location = New System.Drawing.Point(12, 375)
        Me.plVersion2.Name = "plVersion2"
        Me.plVersion2.Size = New System.Drawing.Size(1175, 246)
        Me.plVersion2.TabIndex = 21
        Me.plVersion2.Visible = False
        '
        'cbHideDetails
        '
        Me.cbHideDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbHideDetails.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHideDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbHideDetails.Location = New System.Drawing.Point(1096, 5)
        Me.cbHideDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.cbHideDetails.Name = "cbHideDetails"
        Me.cbHideDetails.Size = New System.Drawing.Size(74, 29)
        Me.cbHideDetails.TabIndex = 21
        Me.cbHideDetails.Text = "Hide Dtls"
        Me.cbHideDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbHideDetails.UseVisualStyleBackColor = True
        '
        'grdSalesOrderGBOrder
        '
        Me.grdSalesOrderGBOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderGBOrder.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdSalesOrderGBOrder.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdSalesOrderGBOrder.Location = New System.Drawing.Point(10, 37)
        Me.grdSalesOrderGBOrder.MainView = Me.grdSalesOrderGBOrderV1
        Me.grdSalesOrderGBOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderGBOrder.Name = "grdSalesOrderGBOrder"
        Me.grdSalesOrderGBOrder.Size = New System.Drawing.Size(1161, 205)
        Me.grdSalesOrderGBOrder.TabIndex = 23
        Me.grdSalesOrderGBOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderGBOrderV1})
        '
        'grdSalesOrderGBOrderV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdSalesOrderGBOrderV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdSalesOrderGBOrderV1.GridControl = Me.grdSalesOrderGBOrder
        Me.grdSalesOrderGBOrderV1.Name = "grdSalesOrderGBOrderV1"
        Me.grdSalesOrderGBOrderV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderGBOrderV1.OptionsView.ShowFooter = True
        '
        'grdSalesOrderGBArticle
        '
        Me.grdSalesOrderGBArticle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderGBArticle.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdSalesOrderGBArticle.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdSalesOrderGBArticle.Location = New System.Drawing.Point(10, 37)
        Me.grdSalesOrderGBArticle.MainView = Me.grdSalesOrderGBArticleV1
        Me.grdSalesOrderGBArticle.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderGBArticle.Name = "grdSalesOrderGBArticle"
        Me.grdSalesOrderGBArticle.Size = New System.Drawing.Size(1161, 205)
        Me.grdSalesOrderGBArticle.TabIndex = 22
        Me.grdSalesOrderGBArticle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderGBArticleV1})
        '
        'grdSalesOrderGBArticleV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdSalesOrderGBArticleV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdSalesOrderGBArticleV1.GridControl = Me.grdSalesOrderGBArticle
        Me.grdSalesOrderGBArticleV1.Name = "grdSalesOrderGBArticleV1"
        Me.grdSalesOrderGBArticleV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderGBArticleV1.OptionsView.ShowFooter = True
        '
        'cbTrack
        '
        Me.cbTrack.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTrack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbTrack.Location = New System.Drawing.Point(464, 628)
        Me.cbTrack.Margin = New System.Windows.Forms.Padding(4)
        Me.cbTrack.Name = "cbTrack"
        Me.cbTrack.Size = New System.Drawing.Size(128, 74)
        Me.cbTrack.TabIndex = 20
        Me.cbTrack.Text = "Track"
        Me.cbTrack.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbTrack.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(599, 628)
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(465, 51)
        Me.pgbar.TabIndex = 15
        Me.pgbar.Visible = False
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
        Me.Panel5.Controls.Add(Me.Panel7)
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.rbNo)
        Me.Panel5.Controls.Add(Me.rbYes)
        Me.Panel5.Controls.Add(Me.cbxProductType)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.cbxProductTypeMain)
        Me.Panel5.Location = New System.Drawing.Point(645, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(413, 153)
        Me.Panel5.TabIndex = 15
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Ivory
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.rbAnalytic)
        Me.Panel7.Controls.Add(Me.rbSynthetic)
        Me.Panel7.Location = New System.Drawing.Point(7, 111)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(267, 31)
        Me.Panel7.TabIndex = 36
        '
        'rbAnalytic
        '
        Me.rbAnalytic.AutoSize = True
        Me.rbAnalytic.Location = New System.Drawing.Point(136, 5)
        Me.rbAnalytic.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAnalytic.Name = "rbAnalytic"
        Me.rbAnalytic.Size = New System.Drawing.Size(79, 20)
        Me.rbAnalytic.TabIndex = 32
        Me.rbAnalytic.Text = "Analytic"
        Me.rbAnalytic.UseVisualStyleBackColor = True
        '
        'rbSynthetic
        '
        Me.rbSynthetic.AutoSize = True
        Me.rbSynthetic.Checked = True
        Me.rbSynthetic.Location = New System.Drawing.Point(5, 5)
        Me.rbSynthetic.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSynthetic.Name = "rbSynthetic"
        Me.rbSynthetic.Size = New System.Drawing.Size(90, 20)
        Me.rbSynthetic.TabIndex = 31
        Me.rbSynthetic.TabStop = True
        Me.rbSynthetic.Text = "Synthetic"
        Me.rbSynthetic.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Ivory
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.rbGroupByOrder)
        Me.Panel6.Controls.Add(Me.rbGroupByArticle)
        Me.Panel6.Location = New System.Drawing.Point(7, 74)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(267, 31)
        Me.Panel6.TabIndex = 35
        '
        'rbGroupByOrder
        '
        Me.rbGroupByOrder.AutoSize = True
        Me.rbGroupByOrder.Location = New System.Drawing.Point(136, 5)
        Me.rbGroupByOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.rbGroupByOrder.Name = "rbGroupByOrder"
        Me.rbGroupByOrder.Size = New System.Drawing.Size(126, 20)
        Me.rbGroupByOrder.TabIndex = 32
        Me.rbGroupByOrder.Text = "Group By Order"
        Me.rbGroupByOrder.UseVisualStyleBackColor = True
        '
        'rbGroupByArticle
        '
        Me.rbGroupByArticle.AutoSize = True
        Me.rbGroupByArticle.Checked = True
        Me.rbGroupByArticle.Location = New System.Drawing.Point(5, 5)
        Me.rbGroupByArticle.Margin = New System.Windows.Forms.Padding(4)
        Me.rbGroupByArticle.Name = "rbGroupByArticle"
        Me.rbGroupByArticle.Size = New System.Drawing.Size(137, 20)
        Me.rbGroupByArticle.TabIndex = 31
        Me.rbGroupByArticle.TabStop = True
        Me.rbGroupByArticle.Text = "Group By Article "
        Me.rbGroupByArticle.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(280, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 28)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Detailed"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rbNo
        '
        Me.rbNo.AutoSize = True
        Me.rbNo.Checked = True
        Me.rbNo.Location = New System.Drawing.Point(362, 120)
        Me.rbNo.Margin = New System.Windows.Forms.Padding(4)
        Me.rbNo.Name = "rbNo"
        Me.rbNo.Size = New System.Drawing.Size(43, 20)
        Me.rbNo.TabIndex = 30
        Me.rbNo.TabStop = True
        Me.rbNo.Text = "No"
        Me.rbNo.UseVisualStyleBackColor = True
        '
        'rbYes
        '
        Me.rbYes.AutoSize = True
        Me.rbYes.Location = New System.Drawing.Point(280, 119)
        Me.rbYes.Margin = New System.Windows.Forms.Padding(4)
        Me.rbYes.Name = "rbYes"
        Me.rbYes.Size = New System.Drawing.Size(50, 20)
        Me.rbYes.TabIndex = 29
        Me.rbYes.Text = "Yes"
        Me.rbYes.UseVisualStyleBackColor = True
        '
        'cbxProductType
        '
        Me.cbxProductType.FormattingEnabled = True
        Me.cbxProductType.Location = New System.Drawing.Point(145, 38)
        Me.cbxProductType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductType.Name = "cbxProductType"
        Me.cbxProductType.Size = New System.Drawing.Size(214, 24)
        Me.cbxProductType.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(4, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(138, 23)
        Me.Label12.TabIndex = 32
        Me.Label12.Text = "Product Type"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxProductTypeMain
        '
        Me.cbxProductTypeMain.FormattingEnabled = True
        Me.cbxProductTypeMain.Location = New System.Drawing.Point(145, 11)
        Me.cbxProductTypeMain.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxProductTypeMain.Name = "cbxProductTypeMain"
        Me.cbxProductTypeMain.Size = New System.Drawing.Size(214, 24)
        Me.cbxProductTypeMain.TabIndex = 37
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
        Me.rbDeliveryDate.Enabled = False
        Me.rbDeliveryDate.Location = New System.Drawing.Point(8, 112)
        Me.rbDeliveryDate.Margin = New System.Windows.Forms.Padding(4)
        Me.rbDeliveryDate.Name = "rbDeliveryDate"
        Me.rbDeliveryDate.Size = New System.Drawing.Size(113, 20)
        Me.rbDeliveryDate.TabIndex = 32
        Me.rbDeliveryDate.Text = "Jobcard Date"
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
        'grdSalesOrder
        '
        Me.grdSalesOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrder.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode3.RelationName = "Level1"
        Me.grdSalesOrder.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.grdSalesOrder.Location = New System.Drawing.Point(12, 187)
        Me.grdSalesOrder.MainView = Me.grdSalesOrderv1
        Me.grdSalesOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrder.Name = "grdSalesOrder"
        Me.grdSalesOrder.Size = New System.Drawing.Size(1175, 433)
        Me.grdSalesOrder.TabIndex = 18
        Me.grdSalesOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderv1})
        '
        'grdSalesOrderv1
        '
        StyleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition3.Appearance.Options.UseForeColor = True
        StyleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition3.Value1 = CType(0, Short)
        Me.grdSalesOrderv1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition3})
        Me.grdSalesOrderv1.GridControl = Me.grdSalesOrder
        Me.grdSalesOrderv1.Name = "grdSalesOrderv1"
        Me.grdSalesOrderv1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderv1.OptionsView.ShowFooter = True
        '
        'grdSalesOrderDetails
        '
        Me.grdSalesOrderDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSalesOrderDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode4.RelationName = "Level1"
        Me.grdSalesOrderDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode4})
        Me.grdSalesOrderDetails.Location = New System.Drawing.Point(12, 187)
        Me.grdSalesOrderDetails.MainView = Me.grdSalesOrderDetailsV1
        Me.grdSalesOrderDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdSalesOrderDetails.Name = "grdSalesOrderDetails"
        Me.grdSalesOrderDetails.Size = New System.Drawing.Size(1175, 433)
        Me.grdSalesOrderDetails.TabIndex = 19
        Me.grdSalesOrderDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdSalesOrderDetailsV1})
        '
        'grdSalesOrderDetailsV1
        '
        StyleFormatCondition4.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition4.Appearance.Options.UseForeColor = True
        StyleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition4.Value1 = CType(0, Short)
        Me.grdSalesOrderDetailsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition4})
        Me.grdSalesOrderDetailsV1.GridControl = Me.grdSalesOrderDetails
        Me.grdSalesOrderDetailsV1.Name = "grdSalesOrderDetailsV1"
        Me.grdSalesOrderDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdSalesOrderDetailsV1.OptionsView.ShowFooter = True
        '
        'frmERPTrackingSystemv1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmERPTrackingSystemv1"
        Me.Text = "ERP Tracking System v1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.plVersion2.ResumeLayout(False)
        CType(Me.grdSalesOrderGBOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderGBOrderV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderGBArticle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderGBArticleV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grdSalesOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSalesOrderDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cbxProductType As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rbNo As System.Windows.Forms.RadioButton
    Friend WithEvents rbYes As System.Windows.Forms.RadioButton
    Friend WithEvents rbDeliveryDate As System.Windows.Forms.RadioButton
    Friend WithEvents rbOrderDate As System.Windows.Forms.RadioButton
    Friend WithEvents grdSalesOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdSalesOrderDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbAnalytic As System.Windows.Forms.RadioButton
    Friend WithEvents rbSynthetic As System.Windows.Forms.RadioButton
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rbGroupByOrder As System.Windows.Forms.RadioButton
    Friend WithEvents rbGroupByArticle As System.Windows.Forms.RadioButton
    Friend WithEvents cbTrack As System.Windows.Forms.Button
    Friend WithEvents plVersion2 As System.Windows.Forms.Panel
    Friend WithEvents cbHideDetails As System.Windows.Forms.Button
    Friend WithEvents grdSalesOrderGBArticle As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderGBArticleV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdSalesOrderGBOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdSalesOrderGBOrderV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbxProductTypeMain As System.Windows.Forms.ComboBox
End Class
