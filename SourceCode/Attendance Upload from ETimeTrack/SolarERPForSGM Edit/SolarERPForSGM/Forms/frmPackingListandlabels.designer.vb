<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingListandlabels
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPackingListandlabels))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.cbExport2Excel = New System.Windows.Forms.Button
        Me.cbPrint = New System.Windows.Forms.Button
        Me.cbDelete = New System.Windows.Forms.Button
        Me.cbUpdate = New System.Windows.Forms.Button
        Me.cbGenerate = New System.Windows.Forms.Button
        Me.cbRefresh = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.plInfo = New System.Windows.Forms.Panel
        Me.lblTime = New System.Windows.Forms.Label
        Me.lblUserDesignation = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblYear = New System.Windows.Forms.Label
        Me.plFooter = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblInsert = New System.Windows.Forms.Label
        Me.lblCapsLock = New System.Windows.Forms.Label
        Me.lblNumLock = New System.Windows.Forms.Label
        Me.lblTimeDifference = New System.Windows.Forms.Label
        Me.lblUnitType = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.plMain = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.rbBOXLabels = New System.Windows.Forms.RadioButton
        Me.rbProductionPackingList = New System.Windows.Forms.RadioButton
        Me.rbPairLabels = New System.Windows.Forms.RadioButton
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.Label46 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.rbManualPacking = New System.Windows.Forms.RadioButton
        Me.rbSingleSizePacking = New System.Windows.Forms.RadioButton
        Me.rbMultipleSizePacking = New System.Windows.Forms.RadioButton
        Me.tbBoxQuantity = New System.Windows.Forms.TextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.tbPPLWeek = New System.Windows.Forms.TextBox
        Me.tbProductionPackingListNo = New System.Windows.Forms.TextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.dpPPLDate = New System.Windows.Forms.DateTimePicker
        Me.Label41 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.Label71 = New System.Windows.Forms.Label
        Me.Label70 = New System.Windows.Forms.Label
        Me.dpDisplayDateTo = New System.Windows.Forms.DateTimePicker
        Me.dpDisplayDateFrom = New System.Windows.Forms.DateTimePicker
        Me.grdOrderHeader = New DevExpress.XtraGrid.GridControl
        Me.grdOrderHeaderV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.dpOrderDate = New System.Windows.Forms.DateTimePicker
        Me.plTypeofPacking = New System.Windows.Forms.GroupBox
        Me.rbAssortmentPacking = New System.Windows.Forms.RadioButton
        Me.rbNormalPacking = New System.Windows.Forms.RadioButton
        Me.Label14 = New System.Windows.Forms.Label
        Me.tbOrderNo = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.cbxSeason = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.plHeaderInfo = New System.Windows.Forms.GroupBox
        Me.tbTotalQuantity = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbxCurrency = New System.Windows.Forms.ComboBox
        Me.tbDiscountValue = New System.Windows.Forms.TextBox
        Me.tbDiscountPercentage = New System.Windows.Forms.TextBox
        Me.tbPayMode = New System.Windows.Forms.TextBox
        Me.tbPriceTerm = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.dpCustomerRefDt = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbCustomerRefNo = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.tbCustomer = New System.Windows.Forms.TextBox
        Me.tbSizeInfo = New System.Windows.Forms.TextBox
        Me.tbWeekNo = New System.Windows.Forms.TextBox
        Me.cbSave = New System.Windows.Forms.Button
        Me.cbCancel = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.tbManualBoxQty = New System.Windows.Forms.TextBox
        Me.tbTotalBoxes = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.rbMultipleBox = New System.Windows.Forms.RadioButton
        Me.rbSingleBox = New System.Windows.Forms.RadioButton
        Me.tbBalQtyTotal = New System.Windows.Forms.TextBox
        Me.tbBalQty17 = New System.Windows.Forms.TextBox
        Me.tbBalQty18 = New System.Windows.Forms.TextBox
        Me.tbBalQty19 = New System.Windows.Forms.TextBox
        Me.tbBalQty20 = New System.Windows.Forms.TextBox
        Me.tbBalQty02 = New System.Windows.Forms.TextBox
        Me.tbBalQty01 = New System.Windows.Forms.TextBox
        Me.tbBalQty03 = New System.Windows.Forms.TextBox
        Me.tbBalQty04 = New System.Windows.Forms.TextBox
        Me.tbBalQty16 = New System.Windows.Forms.TextBox
        Me.tbBalQty05 = New System.Windows.Forms.TextBox
        Me.tbBalQty06 = New System.Windows.Forms.TextBox
        Me.tbBalQty15 = New System.Windows.Forms.TextBox
        Me.tbBalQty07 = New System.Windows.Forms.TextBox
        Me.tbBalQty14 = New System.Windows.Forms.TextBox
        Me.tbBalQty08 = New System.Windows.Forms.TextBox
        Me.tbBalQty13 = New System.Windows.Forms.TextBox
        Me.tbBalQty09 = New System.Windows.Forms.TextBox
        Me.tbBalQty12 = New System.Windows.Forms.TextBox
        Me.tbBalQty10 = New System.Windows.Forms.TextBox
        Me.tbBalQty11 = New System.Windows.Forms.TextBox
        Me.tbPLQtyTotal = New System.Windows.Forms.TextBox
        Me.tbPLQty17 = New System.Windows.Forms.TextBox
        Me.tbPLQty18 = New System.Windows.Forms.TextBox
        Me.tbPLQty19 = New System.Windows.Forms.TextBox
        Me.tbPLQty20 = New System.Windows.Forms.TextBox
        Me.tbPLQty02 = New System.Windows.Forms.TextBox
        Me.tbPLQty01 = New System.Windows.Forms.TextBox
        Me.tbPLQty03 = New System.Windows.Forms.TextBox
        Me.tbPLQty04 = New System.Windows.Forms.TextBox
        Me.tbPLQty16 = New System.Windows.Forms.TextBox
        Me.tbPLQty05 = New System.Windows.Forms.TextBox
        Me.tbPLQty06 = New System.Windows.Forms.TextBox
        Me.tbPLQty15 = New System.Windows.Forms.TextBox
        Me.tbPLQty07 = New System.Windows.Forms.TextBox
        Me.tbPLQty14 = New System.Windows.Forms.TextBox
        Me.tbPLQty08 = New System.Windows.Forms.TextBox
        Me.tbPLQty13 = New System.Windows.Forms.TextBox
        Me.tbPLQty09 = New System.Windows.Forms.TextBox
        Me.tbPLQty12 = New System.Windows.Forms.TextBox
        Me.tbPLQty10 = New System.Windows.Forms.TextBox
        Me.tbPLQty11 = New System.Windows.Forms.TextBox
        Me.tbPLBalQtyTotal = New System.Windows.Forms.TextBox
        Me.tbPLBalQty17 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty18 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty19 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty20 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty02 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty01 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty03 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty04 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty16 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty05 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty06 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty15 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty07 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty14 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty08 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty13 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty09 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty12 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty10 = New System.Windows.Forms.TextBox
        Me.tbPLBalQty11 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQtyTotal = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty17 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty18 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty19 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty20 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty02 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty01 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty03 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty04 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty16 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty05 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty06 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty15 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty07 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty14 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty08 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty13 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty09 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty12 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty10 = New System.Windows.Forms.TextBox
        Me.tbPlGeneratedQty11 = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbRemove = New System.Windows.Forms.Button
        Me.cbInclude = New System.Windows.Forms.Button
        Me.tbSize17 = New System.Windows.Forms.TextBox
        Me.tbSize18 = New System.Windows.Forms.TextBox
        Me.tbSize19 = New System.Windows.Forms.TextBox
        Me.tbSize20 = New System.Windows.Forms.TextBox
        Me.tbSize02 = New System.Windows.Forms.TextBox
        Me.tbSize01 = New System.Windows.Forms.TextBox
        Me.tbSize03 = New System.Windows.Forms.TextBox
        Me.tbSize04 = New System.Windows.Forms.TextBox
        Me.tbSize16 = New System.Windows.Forms.TextBox
        Me.tbSize05 = New System.Windows.Forms.TextBox
        Me.tbSize06 = New System.Windows.Forms.TextBox
        Me.tbSize15 = New System.Windows.Forms.TextBox
        Me.tbSize07 = New System.Windows.Forms.TextBox
        Me.tbSize14 = New System.Windows.Forms.TextBox
        Me.tbSize08 = New System.Windows.Forms.TextBox
        Me.tbSize13 = New System.Windows.Forms.TextBox
        Me.tbSize09 = New System.Windows.Forms.TextBox
        Me.tbSize12 = New System.Windows.Forms.TextBox
        Me.tbSize10 = New System.Windows.Forms.TextBox
        Me.tbSize11 = New System.Windows.Forms.TextBox
        Me.tbPairsTotal = New System.Windows.Forms.TextBox
        Me.tbPairs17 = New System.Windows.Forms.TextBox
        Me.tbPairs18 = New System.Windows.Forms.TextBox
        Me.tbPairs19 = New System.Windows.Forms.TextBox
        Me.tbPairs20 = New System.Windows.Forms.TextBox
        Me.tbPairs02 = New System.Windows.Forms.TextBox
        Me.tbPairs01 = New System.Windows.Forms.TextBox
        Me.tbPairs03 = New System.Windows.Forms.TextBox
        Me.tbPairs04 = New System.Windows.Forms.TextBox
        Me.tbPairs16 = New System.Windows.Forms.TextBox
        Me.tbPairs05 = New System.Windows.Forms.TextBox
        Me.tbPairs06 = New System.Windows.Forms.TextBox
        Me.tbPairs15 = New System.Windows.Forms.TextBox
        Me.tbPairs07 = New System.Windows.Forms.TextBox
        Me.tbPairs14 = New System.Windows.Forms.TextBox
        Me.tbPairs08 = New System.Windows.Forms.TextBox
        Me.tbPairs13 = New System.Windows.Forms.TextBox
        Me.tbPairs09 = New System.Windows.Forms.TextBox
        Me.tbPairs12 = New System.Windows.Forms.TextBox
        Me.tbPairs10 = New System.Windows.Forms.TextBox
        Me.tbPairs11 = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label73 = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.grdTempPlQty = New DevExpress.XtraGrid.GridControl
        Me.grdTempPlQtyV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.CardView2 = New DevExpress.XtraGrid.Views.Card.CardView
        Me.grdOrderDetails = New DevExpress.XtraGrid.GridControl
        Me.grdOrderDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.CardView1 = New DevExpress.XtraGrid.Views.Card.CardView
        Me.Label19 = New System.Windows.Forms.Label
        Me.plDtls = New System.Windows.Forms.Panel
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.plInfo.SuspendLayout()
        Me.plFooter.SuspendLayout()
        Me.plMain.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrderHeaderV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plTypeofPacking.SuspendLayout()
        Me.plHeaderInfo.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.grdTempPlQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTempPlQtyV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrderDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plDtls.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbExport2Excel
        '
        Me.cbExport2Excel.ForeColor = System.Drawing.Color.Green
        Me.cbExport2Excel.Image = CType(resources.GetObject("cbExport2Excel.Image"), System.Drawing.Image)
        Me.cbExport2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExport2Excel.Location = New System.Drawing.Point(127, 4)
        Me.cbExport2Excel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbExport2Excel.Name = "cbExport2Excel"
        Me.cbExport2Excel.Size = New System.Drawing.Size(120, 55)
        Me.cbExport2Excel.TabIndex = 9
        Me.cbExport2Excel.Text = "Export &2 Excel"
        Me.cbExport2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExport2Excel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cbExport2Excel.UseVisualStyleBackColor = True
        '
        'cbPrint
        '
        Me.cbPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPrint.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.cbPrint.ForeColor = System.Drawing.Color.Green
        Me.cbPrint.Image = CType(resources.GetObject("cbPrint.Image"), System.Drawing.Image)
        Me.cbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPrint.Location = New System.Drawing.Point(757, 4)
        Me.cbPrint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(120, 55)
        Me.cbPrint.TabIndex = 8
        Me.cbPrint.Text = "&Print"
        Me.cbPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPrint.UseVisualStyleBackColor = True
        '
        'cbDelete
        '
        Me.cbDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbDelete.ForeColor = System.Drawing.Color.Green
        Me.cbDelete.Image = CType(resources.GetObject("cbDelete.Image"), System.Drawing.Image)
        Me.cbDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbDelete.Location = New System.Drawing.Point(634, 4)
        Me.cbDelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbDelete.Name = "cbDelete"
        Me.cbDelete.Size = New System.Drawing.Size(120, 55)
        Me.cbDelete.TabIndex = 7
        Me.cbDelete.Text = "&Delete"
        Me.cbDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbDelete.UseVisualStyleBackColor = True
        '
        'cbUpdate
        '
        Me.cbUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbUpdate.ForeColor = System.Drawing.Color.Green
        Me.cbUpdate.Image = CType(resources.GetObject("cbUpdate.Image"), System.Drawing.Image)
        Me.cbUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbUpdate.Location = New System.Drawing.Point(511, 4)
        Me.cbUpdate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbUpdate.Name = "cbUpdate"
        Me.cbUpdate.Size = New System.Drawing.Size(120, 55)
        Me.cbUpdate.TabIndex = 6
        Me.cbUpdate.Text = "&Update"
        Me.cbUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbUpdate.UseVisualStyleBackColor = True
        '
        'cbGenerate
        '
        Me.cbGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbGenerate.ForeColor = System.Drawing.Color.Green
        Me.cbGenerate.Image = CType(resources.GetObject("cbGenerate.Image"), System.Drawing.Image)
        Me.cbGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbGenerate.Location = New System.Drawing.Point(388, 4)
        Me.cbGenerate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbGenerate.Name = "cbGenerate"
        Me.cbGenerate.Size = New System.Drawing.Size(120, 55)
        Me.cbGenerate.TabIndex = 5
        Me.cbGenerate.Text = "&Generate"
        Me.cbGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbGenerate.UseVisualStyleBackColor = True
        '
        'cbRefresh
        '
        Me.cbRefresh.ForeColor = System.Drawing.Color.Green
        Me.cbRefresh.Image = CType(resources.GetObject("cbRefresh.Image"), System.Drawing.Image)
        Me.cbRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbRefresh.Location = New System.Drawing.Point(4, 4)
        Me.cbRefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbRefresh.Name = "cbRefresh"
        Me.cbRefresh.Size = New System.Drawing.Size(120, 55)
        Me.cbRefresh.TabIndex = 4
        Me.cbRefresh.Text = "Re&fresh"
        Me.cbRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbRefresh.UseVisualStyleBackColor = True
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExit.ForeColor = System.Drawing.Color.Green
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(880, 4)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(120, 55)
        Me.cbExit.TabIndex = 1
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Timer1
        '
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.cbPrint)
        Me.Panel2.Controls.Add(Me.cbRefresh)
        Me.Panel2.Controls.Add(Me.cbExport2Excel)
        Me.Panel2.Controls.Add(Me.cbExit)
        Me.Panel2.Controls.Add(Me.cbGenerate)
        Me.Panel2.Controls.Add(Me.cbDelete)
        Me.Panel2.Controls.Add(Me.cbUpdate)
        Me.Panel2.Location = New System.Drawing.Point(0, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1006, 64)
        Me.Panel2.TabIndex = 33
        '
        'plInfo
        '
        Me.plInfo.BackColor = System.Drawing.Color.Bisque
        Me.plInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plInfo.Controls.Add(Me.lblTime)
        Me.plInfo.Controls.Add(Me.lblUserDesignation)
        Me.plInfo.Controls.Add(Me.Label6)
        Me.plInfo.Controls.Add(Me.lblUserName)
        Me.plInfo.Controls.Add(Me.Label10)
        Me.plInfo.Controls.Add(Me.lblDate)
        Me.plInfo.Controls.Add(Me.Label1)
        Me.plInfo.Controls.Add(Me.Label20)
        Me.plInfo.Controls.Add(Me.lblYear)
        Me.plInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.plInfo.Location = New System.Drawing.Point(0, 0)
        Me.plInfo.Name = "plInfo"
        Me.plInfo.Size = New System.Drawing.Size(1006, 25)
        Me.plInfo.TabIndex = 47
        '
        'lblTime
        '
        Me.lblTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.BackColor = System.Drawing.Color.Transparent
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.Indigo
        Me.lblTime.Location = New System.Drawing.Point(871, 3)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(128, 22)
        Me.lblTime.TabIndex = 15
        Me.lblTime.Text = "Time"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblUserDesignation
        '
        Me.lblUserDesignation.AutoSize = True
        Me.lblUserDesignation.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUserDesignation.Location = New System.Drawing.Point(342, 3)
        Me.lblUserDesignation.Name = "lblUserDesignation"
        Me.lblUserDesignation.Size = New System.Drawing.Size(84, 17)
        Me.lblUserDesignation.TabIndex = 9
        Me.lblUserDesignation.Text = "Assistant UPO"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Tomato
        Me.Label6.Location = New System.Drawing.Point(242, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 17)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Designation :-"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUserName.Location = New System.Drawing.Point(93, 3)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(114, 17)
        Me.lblUserName.TabIndex = 7
        Me.lblUserName.Text = "Thanveer Ahmed .P"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Tomato
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 17)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "User Name :-"
        '
        'lblDate
        '
        Me.lblDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDate.AutoSize = True
        Me.lblDate.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblDate.Location = New System.Drawing.Point(683, 3)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(132, 17)
        Me.lblDate.TabIndex = 5
        Me.lblDate.Text = "09 - November - 2009"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Tomato
        Me.Label1.Location = New System.Drawing.Point(633, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Date :-"
        '
        'Label20
        '
        Me.Label20.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Tomato
        Me.Label20.Location = New System.Drawing.Point(480, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(49, 17)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Year :-"
        '
        'lblYear
        '
        Me.lblYear.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblYear.AutoSize = True
        Me.lblYear.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblYear.Location = New System.Drawing.Point(530, 3)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(81, 17)
        Me.lblYear.TabIndex = 3
        Me.lblYear.Text = "2009 - 2010"
        '
        'plFooter
        '
        Me.plFooter.BackColor = System.Drawing.Color.Bisque
        Me.plFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plFooter.Controls.Add(Me.Label2)
        Me.plFooter.Controls.Add(Me.lblInsert)
        Me.plFooter.Controls.Add(Me.lblCapsLock)
        Me.plFooter.Controls.Add(Me.lblNumLock)
        Me.plFooter.Controls.Add(Me.lblTimeDifference)
        Me.plFooter.Controls.Add(Me.lblUnitType)
        Me.plFooter.Controls.Add(Me.Label18)
        Me.plFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.plFooter.Location = New System.Drawing.Point(0, 636)
        Me.plFooter.Name = "plFooter"
        Me.plFooter.Size = New System.Drawing.Size(1006, 25)
        Me.plFooter.TabIndex = 48
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.PeachPuff
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(130, -1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(619, 25)
        Me.Label2.TabIndex = 20
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInsert
        '
        Me.lblInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInsert.BackColor = System.Drawing.Color.PeachPuff
        Me.lblInsert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInsert.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblInsert.Location = New System.Drawing.Point(751, -1)
        Me.lblInsert.Name = "lblInsert"
        Me.lblInsert.Size = New System.Drawing.Size(38, 25)
        Me.lblInsert.TabIndex = 19
        Me.lblInsert.Text = "Ins"
        Me.lblInsert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCapsLock
        '
        Me.lblCapsLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCapsLock.BackColor = System.Drawing.Color.PeachPuff
        Me.lblCapsLock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCapsLock.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblCapsLock.Location = New System.Drawing.Point(791, -1)
        Me.lblCapsLock.Name = "lblCapsLock"
        Me.lblCapsLock.Size = New System.Drawing.Size(38, 25)
        Me.lblCapsLock.TabIndex = 18
        Me.lblCapsLock.Text = "Caps"
        Me.lblCapsLock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumLock
        '
        Me.lblNumLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNumLock.BackColor = System.Drawing.Color.PeachPuff
        Me.lblNumLock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumLock.ForeColor = System.Drawing.Color.Blue
        Me.lblNumLock.Location = New System.Drawing.Point(831, -1)
        Me.lblNumLock.Name = "lblNumLock"
        Me.lblNumLock.Size = New System.Drawing.Size(38, 25)
        Me.lblNumLock.TabIndex = 17
        Me.lblNumLock.Text = "Num"
        Me.lblNumLock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTimeDifference
        '
        Me.lblTimeDifference.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTimeDifference.BackColor = System.Drawing.Color.PeachPuff
        Me.lblTimeDifference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTimeDifference.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeDifference.ForeColor = System.Drawing.Color.Indigo
        Me.lblTimeDifference.Location = New System.Drawing.Point(871, -1)
        Me.lblTimeDifference.Name = "lblTimeDifference"
        Me.lblTimeDifference.Size = New System.Drawing.Size(128, 25)
        Me.lblTimeDifference.TabIndex = 16
        Me.lblTimeDifference.Text = "Time"
        Me.lblTimeDifference.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUnitType
        '
        Me.lblUnitType.BackColor = System.Drawing.Color.PeachPuff
        Me.lblUnitType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnitType.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUnitType.Location = New System.Drawing.Point(88, -1)
        Me.lblUnitType.Name = "lblUnitType"
        Me.lblUnitType.Size = New System.Drawing.Size(40, 25)
        Me.lblUnitType.TabIndex = 1
        Me.lblUnitType.Text = "Soles"
        Me.lblUnitType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.PeachPuff
        Me.Label18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Tomato
        Me.Label18.Location = New System.Drawing.Point(5, -1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(79, 25)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Unit Type :-"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plMain
        '
        Me.plMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plMain.BackColor = System.Drawing.Color.Transparent
        Me.plMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plMain.Controls.Add(Me.GroupBox2)
        Me.plMain.Controls.Add(Me.Label71)
        Me.plMain.Controls.Add(Me.Label70)
        Me.plMain.Controls.Add(Me.dpDisplayDateTo)
        Me.plMain.Controls.Add(Me.dpDisplayDateFrom)
        Me.plMain.Controls.Add(Me.grdOrderHeader)
        Me.plMain.Location = New System.Drawing.Point(3, 92)
        Me.plMain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.plMain.Name = "plMain"
        Me.plMain.Size = New System.Drawing.Size(997, 540)
        Me.plMain.TabIndex = 49
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.rbBOXLabels)
        Me.GroupBox2.Controls.Add(Me.rbProductionPackingList)
        Me.GroupBox2.Controls.Add(Me.rbPairLabels)
        Me.GroupBox2.Controls.Add(Me.GroupBox6)
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 369)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(968, 158)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Packing List Generation Details :-"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(717, 122)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(291, 17)
        Me.Label15.TabIndex = 1018
        Me.Label15.Text = "08. 23-Oct-20 - Upper Packing List Last Box Merged"
        '
        'rbBOXLabels
        '
        Me.rbBOXLabels.AutoSize = True
        Me.rbBOXLabels.Enabled = False
        Me.rbBOXLabels.Location = New System.Drawing.Point(717, 85)
        Me.rbBOXLabels.Name = "rbBOXLabels"
        Me.rbBOXLabels.Size = New System.Drawing.Size(86, 21)
        Me.rbBOXLabels.TabIndex = 1017
        Me.rbBOXLabels.Text = "Box Labels"
        Me.rbBOXLabels.UseVisualStyleBackColor = True
        Me.rbBOXLabels.Visible = False
        '
        'rbProductionPackingList
        '
        Me.rbProductionPackingList.AutoSize = True
        Me.rbProductionPackingList.Checked = True
        Me.rbProductionPackingList.Location = New System.Drawing.Point(714, 58)
        Me.rbProductionPackingList.Name = "rbProductionPackingList"
        Me.rbProductionPackingList.Size = New System.Drawing.Size(91, 21)
        Me.rbProductionPackingList.TabIndex = 1016
        Me.rbProductionPackingList.TabStop = True
        Me.rbProductionPackingList.Text = "Packing List"
        Me.rbProductionPackingList.UseVisualStyleBackColor = True
        '
        'rbPairLabels
        '
        Me.rbPairLabels.AutoSize = True
        Me.rbPairLabels.Enabled = False
        Me.rbPairLabels.Location = New System.Drawing.Point(716, 31)
        Me.rbPairLabels.Name = "rbPairLabels"
        Me.rbPairLabels.Size = New System.Drawing.Size(86, 21)
        Me.rbPairLabels.TabIndex = 1015
        Me.rbPairLabels.Text = "Pair Labels"
        Me.rbPairLabels.UseVisualStyleBackColor = True
        Me.rbPairLabels.Visible = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TextBox4)
        Me.GroupBox6.Controls.Add(Me.Label43)
        Me.GroupBox6.Controls.Add(Me.Label44)
        Me.GroupBox6.Controls.Add(Me.TextBox5)
        Me.GroupBox6.Controls.Add(Me.TextBox6)
        Me.GroupBox6.Controls.Add(Me.Label45)
        Me.GroupBox6.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox6.Controls.Add(Me.Label46)
        Me.GroupBox6.Enabled = False
        Me.GroupBox6.Location = New System.Drawing.Point(535, 21)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(176, 131)
        Me.GroupBox6.TabIndex = 1014
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Export Depatment Info :-"
        Me.GroupBox6.Visible = False
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.ForeColor = System.Drawing.Color.Blue
        Me.TextBox4.Location = New System.Drawing.Point(64, 96)
        Me.TextBox4.MaxLength = 50
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(40, 22)
        Me.TextBox4.TabIndex = 1013
        '
        'Label43
        '
        Me.Label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label43.Location = New System.Drawing.Point(4, 96)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(56, 22)
        Me.Label43.TabIndex = 1012
        Me.Label43.Text = "Box No. :-"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label44
        '
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label44.Location = New System.Drawing.Point(4, 18)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(56, 22)
        Me.Label44.TabIndex = 1006
        Me.Label44.Text = "P.L No. :-"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.White
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.ForeColor = System.Drawing.Color.Blue
        Me.TextBox5.Location = New System.Drawing.Point(64, 70)
        Me.TextBox5.MaxLength = 50
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(40, 22)
        Me.TextBox5.TabIndex = 1011
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.White
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox6.ForeColor = System.Drawing.Color.Blue
        Me.TextBox6.Location = New System.Drawing.Point(64, 18)
        Me.TextBox6.MaxLength = 50
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(50, 22)
        Me.TextBox6.TabIndex = 1009
        '
        'Label45
        '
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Location = New System.Drawing.Point(4, 70)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(56, 22)
        Me.Label45.TabIndex = 1008
        Me.Label45.Text = "P.L Wk :-"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd-MMM-yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(64, 44)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(108, 22)
        Me.DateTimePicker2.TabIndex = 1010
        '
        'Label46
        '
        Me.Label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label46.Location = New System.Drawing.Point(4, 44)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(56, 22)
        Me.Label46.TabIndex = 1007
        Me.Label46.Text = "P.L Dt :-"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.tbBoxQuantity)
        Me.GroupBox4.Controls.Add(Me.Label42)
        Me.GroupBox4.Controls.Add(Me.Label40)
        Me.GroupBox4.Controls.Add(Me.tbPPLWeek)
        Me.GroupBox4.Controls.Add(Me.tbProductionPackingListNo)
        Me.GroupBox4.Controls.Add(Me.Label39)
        Me.GroupBox4.Controls.Add(Me.dpPPLDate)
        Me.GroupBox4.Controls.Add(Me.Label41)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 21)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(350, 131)
        Me.GroupBox4.TabIndex = 1013
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "P.P.C Department Info :-"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.rbManualPacking)
        Me.GroupBox5.Controls.Add(Me.rbSingleSizePacking)
        Me.GroupBox5.Controls.Add(Me.rbMultipleSizePacking)
        Me.GroupBox5.Location = New System.Drawing.Point(177, 12)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(166, 97)
        Me.GroupBox5.TabIndex = 1014
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Box Packing Type :-"
        '
        'rbManualPacking
        '
        Me.rbManualPacking.AutoSize = True
        Me.rbManualPacking.Enabled = False
        Me.rbManualPacking.Location = New System.Drawing.Point(9, 57)
        Me.rbManualPacking.Name = "rbManualPacking"
        Me.rbManualPacking.Size = New System.Drawing.Size(125, 21)
        Me.rbManualPacking.TabIndex = 1008
        Me.rbManualPacking.Text = "Manual (Optional)"
        Me.rbManualPacking.UseVisualStyleBackColor = True
        Me.rbManualPacking.Visible = False
        '
        'rbSingleSizePacking
        '
        Me.rbSingleSizePacking.AutoSize = True
        Me.rbSingleSizePacking.Enabled = False
        Me.rbSingleSizePacking.Location = New System.Drawing.Point(9, 37)
        Me.rbSingleSizePacking.Name = "rbSingleSizePacking"
        Me.rbSingleSizePacking.Size = New System.Drawing.Size(131, 21)
        Me.rbSingleSizePacking.TabIndex = 1006
        Me.rbSingleSizePacking.Text = "Single Size Packing"
        Me.rbSingleSizePacking.UseVisualStyleBackColor = True
        Me.rbSingleSizePacking.Visible = False
        '
        'rbMultipleSizePacking
        '
        Me.rbMultipleSizePacking.AutoSize = True
        Me.rbMultipleSizePacking.Checked = True
        Me.rbMultipleSizePacking.Location = New System.Drawing.Point(9, 16)
        Me.rbMultipleSizePacking.Name = "rbMultipleSizePacking"
        Me.rbMultipleSizePacking.Size = New System.Drawing.Size(143, 21)
        Me.rbMultipleSizePacking.TabIndex = 1007
        Me.rbMultipleSizePacking.TabStop = True
        Me.rbMultipleSizePacking.Text = "Multiple Size Packing"
        Me.rbMultipleSizePacking.UseVisualStyleBackColor = True
        '
        'tbBoxQuantity
        '
        Me.tbBoxQuantity.BackColor = System.Drawing.Color.White
        Me.tbBoxQuantity.ForeColor = System.Drawing.Color.Black
        Me.tbBoxQuantity.Location = New System.Drawing.Point(64, 96)
        Me.tbBoxQuantity.MaxLength = 50
        Me.tbBoxQuantity.Name = "tbBoxQuantity"
        Me.tbBoxQuantity.Size = New System.Drawing.Size(108, 22)
        Me.tbBoxQuantity.TabIndex = 1013
        '
        'Label42
        '
        Me.Label42.Location = New System.Drawing.Point(4, 96)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(56, 22)
        Me.Label42.TabIndex = 1012
        Me.Label42.Text = "Box Qty :-"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label40
        '
        Me.Label40.Location = New System.Drawing.Point(4, 18)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(56, 22)
        Me.Label40.TabIndex = 1006
        Me.Label40.Text = "P.L No. :-"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label40.Visible = False
        '
        'tbPPLWeek
        '
        Me.tbPPLWeek.BackColor = System.Drawing.Color.White
        Me.tbPPLWeek.Enabled = False
        Me.tbPPLWeek.ForeColor = System.Drawing.Color.Blue
        Me.tbPPLWeek.Location = New System.Drawing.Point(64, 70)
        Me.tbPPLWeek.MaxLength = 4
        Me.tbPPLWeek.Name = "tbPPLWeek"
        Me.tbPPLWeek.ReadOnly = True
        Me.tbPPLWeek.Size = New System.Drawing.Size(108, 22)
        Me.tbPPLWeek.TabIndex = 1011
        Me.tbPPLWeek.Visible = False
        '
        'tbProductionPackingListNo
        '
        Me.tbProductionPackingListNo.BackColor = System.Drawing.Color.White
        Me.tbProductionPackingListNo.Enabled = False
        Me.tbProductionPackingListNo.ForeColor = System.Drawing.Color.Blue
        Me.tbProductionPackingListNo.Location = New System.Drawing.Point(64, 18)
        Me.tbProductionPackingListNo.MaxLength = 50
        Me.tbProductionPackingListNo.Name = "tbProductionPackingListNo"
        Me.tbProductionPackingListNo.ReadOnly = True
        Me.tbProductionPackingListNo.Size = New System.Drawing.Size(108, 22)
        Me.tbProductionPackingListNo.TabIndex = 1009
        Me.tbProductionPackingListNo.Visible = False
        '
        'Label39
        '
        Me.Label39.Location = New System.Drawing.Point(4, 70)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(56, 22)
        Me.Label39.TabIndex = 1008
        Me.Label39.Text = "P.L Wk :-"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label39.Visible = False
        '
        'dpPPLDate
        '
        Me.dpPPLDate.CustomFormat = "dd-MMM-yyyy"
        Me.dpPPLDate.Enabled = False
        Me.dpPPLDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpPPLDate.Location = New System.Drawing.Point(64, 44)
        Me.dpPPLDate.Name = "dpPPLDate"
        Me.dpPPLDate.Size = New System.Drawing.Size(108, 22)
        Me.dpPPLDate.TabIndex = 1010
        Me.dpPPLDate.Visible = False
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(4, 44)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(56, 22)
        Me.Label41.TabIndex = 1007
        Me.Label41.Text = "P.L Dt :-"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label41.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButton1)
        Me.GroupBox3.Controls.Add(Me.RadioButton2)
        Me.GroupBox3.Location = New System.Drawing.Point(363, 21)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(166, 39)
        Me.GroupBox3.TabIndex = 1012
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Type of Packing :-"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Enabled = False
        Me.RadioButton1.Location = New System.Drawing.Point(75, 14)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(89, 21)
        Me.RadioButton1.TabIndex = 1007
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "&Assortment"
        Me.RadioButton1.UseVisualStyleBackColor = True
        Me.RadioButton1.Visible = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(9, 14)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(67, 21)
        Me.RadioButton2.TabIndex = 1006
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "&Normal"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Label71
        '
        Me.Label71.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label71.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.Label71.Location = New System.Drawing.Point(848, 4)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(39, 22)
        Me.Label71.TabIndex = 30
        Me.Label71.Text = "To :-"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label70
        '
        Me.Label70.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label70.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.Label70.Location = New System.Drawing.Point(694, 4)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(46, 22)
        Me.Label70.TabIndex = 29
        Me.Label70.Text = "From :-"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dpDisplayDateTo
        '
        Me.dpDisplayDateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dpDisplayDateTo.CustomFormat = "dd-MMM-yyyy"
        Me.dpDisplayDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpDisplayDateTo.Location = New System.Drawing.Point(891, 4)
        Me.dpDisplayDateTo.Name = "dpDisplayDateTo"
        Me.dpDisplayDateTo.Size = New System.Drawing.Size(100, 22)
        Me.dpDisplayDateTo.TabIndex = 28
        '
        'dpDisplayDateFrom
        '
        Me.dpDisplayDateFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dpDisplayDateFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dpDisplayDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpDisplayDateFrom.Location = New System.Drawing.Point(744, 3)
        Me.dpDisplayDateFrom.Name = "dpDisplayDateFrom"
        Me.dpDisplayDateFrom.Size = New System.Drawing.Size(100, 22)
        Me.dpDisplayDateFrom.TabIndex = 27
        Me.dpDisplayDateFrom.Value = New Date(2016, 4, 7, 15, 22, 0, 0)
        '
        'grdOrderHeader
        '
        Me.grdOrderHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOrderHeader.Location = New System.Drawing.Point(4, 32)
        Me.grdOrderHeader.LookAndFeel.SkinName = "Stardust"
        Me.grdOrderHeader.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdOrderHeader.MainView = Me.grdOrderHeaderV1
        Me.grdOrderHeader.Name = "grdOrderHeader"
        Me.grdOrderHeader.Size = New System.Drawing.Size(987, 331)
        Me.grdOrderHeader.TabIndex = 10
        Me.grdOrderHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdOrderHeaderV1})
        '
        'grdOrderHeaderV1
        '
        Me.grdOrderHeaderV1.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.DetailTip.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.DetailTip.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.Empty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.Empty.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.EvenRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.EvenRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.FilterCloseButton.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.FilterPanel.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.FixedLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.FixedLine.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.FocusedCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.FocusedCell.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.FocusedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.FocusedRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.FooterPanel.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.GroupButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.GroupButton.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.GroupFooter.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.GroupPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.GroupPanel.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.GroupRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.GroupRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.HorzLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.HorzLine.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.OddRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.OddRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.Preview.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.Row.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.RowSeparator.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.RowSeparator.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.SelectedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.SelectedRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.TopNewRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.TopNewRow.Options.UseFont = True
        Me.grdOrderHeaderV1.Appearance.VertLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderHeaderV1.Appearance.VertLine.Options.UseFont = True
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = 0
        StyleFormatCondition2.ApplyToRow = True
        Me.grdOrderHeaderV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1, StyleFormatCondition2})
        Me.grdOrderHeaderV1.GridControl = Me.grdOrderHeader
        Me.grdOrderHeaderV1.Name = "grdOrderHeaderV1"
        Me.grdOrderHeaderV1.OptionsBehavior.Editable = False
        Me.grdOrderHeaderV1.OptionsView.ColumnAutoWidth = False
        Me.grdOrderHeaderV1.OptionsView.ShowAutoFilterRow = True
        Me.grdOrderHeaderV1.OptionsView.ShowFooter = True
        Me.grdOrderHeaderV1.OptionsView.ShowGroupPanel = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Cancel.jpg")
        Me.ImageList1.Images.SetKeyName(1, "Save.jpg")
        Me.ImageList1.Images.SetKeyName(2, "Add More.jpg")
        Me.ImageList1.Images.SetKeyName(3, "Remove Item.jpg")
        Me.ImageList1.Images.SetKeyName(4, "Update.jpg")
        Me.ImageList1.Images.SetKeyName(5, "Exit.jpg")
        '
        'dpOrderDate
        '
        Me.dpOrderDate.CustomFormat = "dd-MMM-yyyy"
        Me.dpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpOrderDate.Location = New System.Drawing.Point(468, 4)
        Me.dpOrderDate.Name = "dpOrderDate"
        Me.dpOrderDate.Size = New System.Drawing.Size(108, 22)
        Me.dpOrderDate.TabIndex = 1003
        '
        'plTypeofPacking
        '
        Me.plTypeofPacking.Controls.Add(Me.rbAssortmentPacking)
        Me.plTypeofPacking.Controls.Add(Me.rbNormalPacking)
        Me.plTypeofPacking.Location = New System.Drawing.Point(581, 2)
        Me.plTypeofPacking.Name = "plTypeofPacking"
        Me.plTypeofPacking.Size = New System.Drawing.Size(166, 39)
        Me.plTypeofPacking.TabIndex = 1005
        Me.plTypeofPacking.TabStop = False
        Me.plTypeofPacking.Text = "Type of Packing :-"
        '
        'rbAssortmentPacking
        '
        Me.rbAssortmentPacking.AutoSize = True
        Me.rbAssortmentPacking.Location = New System.Drawing.Point(75, 14)
        Me.rbAssortmentPacking.Name = "rbAssortmentPacking"
        Me.rbAssortmentPacking.Size = New System.Drawing.Size(89, 21)
        Me.rbAssortmentPacking.TabIndex = 1007
        Me.rbAssortmentPacking.TabStop = True
        Me.rbAssortmentPacking.Text = "&Assortment"
        Me.rbAssortmentPacking.UseVisualStyleBackColor = True
        '
        'rbNormalPacking
        '
        Me.rbNormalPacking.AutoSize = True
        Me.rbNormalPacking.Checked = True
        Me.rbNormalPacking.Location = New System.Drawing.Point(9, 14)
        Me.rbNormalPacking.Name = "rbNormalPacking"
        Me.rbNormalPacking.Size = New System.Drawing.Size(67, 21)
        Me.rbNormalPacking.TabIndex = 1006
        Me.rbNormalPacking.TabStop = True
        Me.rbNormalPacking.Text = "&Normal"
        Me.rbNormalPacking.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(397, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(67, 22)
        Me.Label14.TabIndex = 833
        Me.Label14.Text = "Order Dt :-"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbOrderNo
        '
        Me.tbOrderNo.BackColor = System.Drawing.Color.White
        Me.tbOrderNo.ForeColor = System.Drawing.Color.Blue
        Me.tbOrderNo.Location = New System.Drawing.Point(249, 4)
        Me.tbOrderNo.MaxLength = 50
        Me.tbOrderNo.Name = "tbOrderNo"
        Me.tbOrderNo.ReadOnly = True
        Me.tbOrderNo.Size = New System.Drawing.Size(144, 22)
        Me.tbOrderNo.TabIndex = 1002
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(174, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 22)
        Me.Label13.TabIndex = 831
        Me.Label13.Text = "Order No. :-"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbxSeason
        '
        Me.cbxSeason.FormattingEnabled = True
        Me.cbxSeason.Location = New System.Drawing.Point(68, 3)
        Me.cbxSeason.Name = "cbxSeason"
        Me.cbxSeason.Size = New System.Drawing.Size(100, 25)
        Me.cbxSeason.TabIndex = 1001
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(4, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 22)
        Me.Label9.TabIndex = 829
        Me.Label9.Text = "Season :-"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'plHeaderInfo
        '
        Me.plHeaderInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plHeaderInfo.Controls.Add(Me.tbTotalQuantity)
        Me.plHeaderInfo.Controls.Add(Me.Label5)
        Me.plHeaderInfo.Controls.Add(Me.cbxCurrency)
        Me.plHeaderInfo.Controls.Add(Me.tbDiscountValue)
        Me.plHeaderInfo.Controls.Add(Me.tbDiscountPercentage)
        Me.plHeaderInfo.Controls.Add(Me.tbPayMode)
        Me.plHeaderInfo.Controls.Add(Me.tbPriceTerm)
        Me.plHeaderInfo.Controls.Add(Me.Label24)
        Me.plHeaderInfo.Controls.Add(Me.Label17)
        Me.plHeaderInfo.Controls.Add(Me.Label16)
        Me.plHeaderInfo.Controls.Add(Me.Label12)
        Me.plHeaderInfo.Controls.Add(Me.dpCustomerRefDt)
        Me.plHeaderInfo.Controls.Add(Me.Label3)
        Me.plHeaderInfo.Controls.Add(Me.tbCustomerRefNo)
        Me.plHeaderInfo.Controls.Add(Me.Label22)
        Me.plHeaderInfo.Controls.Add(Me.Label23)
        Me.plHeaderInfo.Controls.Add(Me.tbCustomer)
        Me.plHeaderInfo.Controls.Add(Me.tbSizeInfo)
        Me.plHeaderInfo.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plHeaderInfo.Location = New System.Drawing.Point(7, 41)
        Me.plHeaderInfo.Name = "plHeaderInfo"
        Me.plHeaderInfo.Size = New System.Drawing.Size(993, 45)
        Me.plHeaderInfo.TabIndex = 1008
        Me.plHeaderInfo.TabStop = False
        Me.plHeaderInfo.Text = "Order Header Info :-"
        '
        'tbTotalQuantity
        '
        Me.tbTotalQuantity.BackColor = System.Drawing.Color.White
        Me.tbTotalQuantity.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalQuantity.ForeColor = System.Drawing.Color.Black
        Me.tbTotalQuantity.Location = New System.Drawing.Point(917, 18)
        Me.tbTotalQuantity.MaxLength = 20
        Me.tbTotalQuantity.Name = "tbTotalQuantity"
        Me.tbTotalQuantity.Size = New System.Drawing.Size(65, 22)
        Me.tbTotalQuantity.TabIndex = 1012
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(847, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 22)
        Me.Label5.TabIndex = 845
        Me.Label5.Text = "Total Qty :-"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbxCurrency
        '
        Me.cbxCurrency.FormattingEnabled = True
        Me.cbxCurrency.Location = New System.Drawing.Point(623, 44)
        Me.cbxCurrency.Name = "cbxCurrency"
        Me.cbxCurrency.Size = New System.Drawing.Size(107, 25)
        Me.cbxCurrency.TabIndex = 1016
        '
        'tbDiscountValue
        '
        Me.tbDiscountValue.Location = New System.Drawing.Point(882, 44)
        Me.tbDiscountValue.MaxLength = 50
        Me.tbDiscountValue.Name = "tbDiscountValue"
        Me.tbDiscountValue.Size = New System.Drawing.Size(100, 22)
        Me.tbDiscountValue.TabIndex = 1018
        '
        'tbDiscountPercentage
        '
        Me.tbDiscountPercentage.Location = New System.Drawing.Point(838, 44)
        Me.tbDiscountPercentage.MaxLength = 50
        Me.tbDiscountPercentage.Name = "tbDiscountPercentage"
        Me.tbDiscountPercentage.Size = New System.Drawing.Size(40, 22)
        Me.tbDiscountPercentage.TabIndex = 1017
        '
        'tbPayMode
        '
        Me.tbPayMode.Location = New System.Drawing.Point(263, 44)
        Me.tbPayMode.MaxLength = 50
        Me.tbPayMode.Name = "tbPayMode"
        Me.tbPayMode.Size = New System.Drawing.Size(100, 22)
        Me.tbPayMode.TabIndex = 1014
        '
        'tbPriceTerm
        '
        Me.tbPriceTerm.Location = New System.Drawing.Point(448, 44)
        Me.tbPriceTerm.MaxLength = 50
        Me.tbPriceTerm.Name = "tbPriceTerm"
        Me.tbPriceTerm.Size = New System.Drawing.Size(100, 22)
        Me.tbPriceTerm.TabIndex = 1015
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(552, 44)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(67, 22)
        Me.Label24.TabIndex = 843
        Me.Label24.Text = "Currency :-"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(735, 44)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 22)
        Me.Label17.TabIndex = 841
        Me.Label17.Text = "Disc'nt % && Val :-"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(367, 44)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 22)
        Me.Label16.TabIndex = 840
        Me.Label16.Text = "Price Term :-"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(186, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 22)
        Me.Label12.TabIndex = 839
        Me.Label12.Text = "Pay Mode :-"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dpCustomerRefDt
        '
        Me.dpCustomerRefDt.CustomFormat = "dd-MMM-yyyy"
        Me.dpCustomerRefDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpCustomerRefDt.Location = New System.Drawing.Point(735, 18)
        Me.dpCustomerRefDt.Name = "dpCustomerRefDt"
        Me.dpCustomerRefDt.Size = New System.Drawing.Size(108, 22)
        Me.dpCustomerRefDt.TabIndex = 1011
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(448, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 22)
        Me.Label3.TabIndex = 836
        Me.Label3.Text = "Cust Ref No. && Date :-"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbCustomerRefNo
        '
        Me.tbCustomerRefNo.Location = New System.Drawing.Point(581, 18)
        Me.tbCustomerRefNo.MaxLength = 50
        Me.tbCustomerRefNo.Name = "tbCustomerRefNo"
        Me.tbCustomerRefNo.Size = New System.Drawing.Size(150, 22)
        Me.tbCustomerRefNo.TabIndex = 1010
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(5, 44)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(73, 22)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "Size Info :-"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(5, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(73, 22)
        Me.Label23.TabIndex = 11
        Me.Label23.Text = "Customer :-"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbCustomer
        '
        Me.tbCustomer.Location = New System.Drawing.Point(82, 18)
        Me.tbCustomer.MaxLength = 50
        Me.tbCustomer.Name = "tbCustomer"
        Me.tbCustomer.Size = New System.Drawing.Size(362, 22)
        Me.tbCustomer.TabIndex = 1009
        '
        'tbSizeInfo
        '
        Me.tbSizeInfo.Location = New System.Drawing.Point(82, 44)
        Me.tbSizeInfo.MaxLength = 50
        Me.tbSizeInfo.Name = "tbSizeInfo"
        Me.tbSizeInfo.Size = New System.Drawing.Size(100, 22)
        Me.tbSizeInfo.TabIndex = 1013
        '
        'tbWeekNo
        '
        Me.tbWeekNo.BackColor = System.Drawing.Color.White
        Me.tbWeekNo.ForeColor = System.Drawing.Color.Blue
        Me.tbWeekNo.Location = New System.Drawing.Point(468, 25)
        Me.tbWeekNo.MaxLength = 50
        Me.tbWeekNo.Name = "tbWeekNo"
        Me.tbWeekNo.ReadOnly = True
        Me.tbWeekNo.Size = New System.Drawing.Size(40, 22)
        Me.tbWeekNo.TabIndex = 1004
        '
        'cbSave
        '
        Me.cbSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSave.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.cbSave.ForeColor = System.Drawing.Color.Green
        Me.cbSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbSave.ImageIndex = 1
        Me.cbSave.ImageList = Me.ImageList1
        Me.cbSave.Location = New System.Drawing.Point(757, 4)
        Me.cbSave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbSave.Name = "cbSave"
        Me.cbSave.Size = New System.Drawing.Size(120, 40)
        Me.cbSave.TabIndex = 1064
        Me.cbSave.Text = "&Save"
        Me.cbSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbSave.UseVisualStyleBackColor = True
        '
        'cbCancel
        '
        Me.cbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbCancel.ForeColor = System.Drawing.Color.Green
        Me.cbCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbCancel.ImageIndex = 0
        Me.cbCancel.ImageList = Me.ImageList1
        Me.cbCancel.Location = New System.Drawing.Point(880, 4)
        Me.cbCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbCancel.Name = "cbCancel"
        Me.cbCancel.Size = New System.Drawing.Size(120, 40)
        Me.cbCancel.TabIndex = 1065
        Me.cbCancel.Text = "&Cancel"
        Me.cbCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GroupBox7)
        Me.GroupBox1.Controls.Add(Me.rbMultipleBox)
        Me.GroupBox1.Controls.Add(Me.rbSingleBox)
        Me.GroupBox1.Controls.Add(Me.tbBalQtyTotal)
        Me.GroupBox1.Controls.Add(Me.tbBalQty17)
        Me.GroupBox1.Controls.Add(Me.tbBalQty18)
        Me.GroupBox1.Controls.Add(Me.tbBalQty19)
        Me.GroupBox1.Controls.Add(Me.tbBalQty20)
        Me.GroupBox1.Controls.Add(Me.tbBalQty02)
        Me.GroupBox1.Controls.Add(Me.tbBalQty01)
        Me.GroupBox1.Controls.Add(Me.tbBalQty03)
        Me.GroupBox1.Controls.Add(Me.tbBalQty04)
        Me.GroupBox1.Controls.Add(Me.tbBalQty16)
        Me.GroupBox1.Controls.Add(Me.tbBalQty05)
        Me.GroupBox1.Controls.Add(Me.tbBalQty06)
        Me.GroupBox1.Controls.Add(Me.tbBalQty15)
        Me.GroupBox1.Controls.Add(Me.tbBalQty07)
        Me.GroupBox1.Controls.Add(Me.tbBalQty14)
        Me.GroupBox1.Controls.Add(Me.tbBalQty08)
        Me.GroupBox1.Controls.Add(Me.tbBalQty13)
        Me.GroupBox1.Controls.Add(Me.tbBalQty09)
        Me.GroupBox1.Controls.Add(Me.tbBalQty12)
        Me.GroupBox1.Controls.Add(Me.tbBalQty10)
        Me.GroupBox1.Controls.Add(Me.tbBalQty11)
        Me.GroupBox1.Controls.Add(Me.tbPLQtyTotal)
        Me.GroupBox1.Controls.Add(Me.tbPLQty17)
        Me.GroupBox1.Controls.Add(Me.tbPLQty18)
        Me.GroupBox1.Controls.Add(Me.tbPLQty19)
        Me.GroupBox1.Controls.Add(Me.tbPLQty20)
        Me.GroupBox1.Controls.Add(Me.tbPLQty02)
        Me.GroupBox1.Controls.Add(Me.tbPLQty01)
        Me.GroupBox1.Controls.Add(Me.tbPLQty03)
        Me.GroupBox1.Controls.Add(Me.tbPLQty04)
        Me.GroupBox1.Controls.Add(Me.tbPLQty16)
        Me.GroupBox1.Controls.Add(Me.tbPLQty05)
        Me.GroupBox1.Controls.Add(Me.tbPLQty06)
        Me.GroupBox1.Controls.Add(Me.tbPLQty15)
        Me.GroupBox1.Controls.Add(Me.tbPLQty07)
        Me.GroupBox1.Controls.Add(Me.tbPLQty14)
        Me.GroupBox1.Controls.Add(Me.tbPLQty08)
        Me.GroupBox1.Controls.Add(Me.tbPLQty13)
        Me.GroupBox1.Controls.Add(Me.tbPLQty09)
        Me.GroupBox1.Controls.Add(Me.tbPLQty12)
        Me.GroupBox1.Controls.Add(Me.tbPLQty10)
        Me.GroupBox1.Controls.Add(Me.tbPLQty11)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQtyTotal)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty17)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty18)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty19)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty20)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty02)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty01)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty03)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty04)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty16)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty05)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty06)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty15)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty07)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty14)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty08)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty13)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty09)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty12)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty10)
        Me.GroupBox1.Controls.Add(Me.tbPLBalQty11)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQtyTotal)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty17)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty18)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty19)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty20)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty02)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty01)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty03)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty04)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty16)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty05)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty06)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty15)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty07)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty14)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty08)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty13)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty09)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty12)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty10)
        Me.GroupBox1.Controls.Add(Me.tbPlGeneratedQty11)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbRemove)
        Me.GroupBox1.Controls.Add(Me.cbInclude)
        Me.GroupBox1.Controls.Add(Me.tbSize17)
        Me.GroupBox1.Controls.Add(Me.tbSize18)
        Me.GroupBox1.Controls.Add(Me.tbSize19)
        Me.GroupBox1.Controls.Add(Me.tbSize20)
        Me.GroupBox1.Controls.Add(Me.tbSize02)
        Me.GroupBox1.Controls.Add(Me.tbSize01)
        Me.GroupBox1.Controls.Add(Me.tbSize03)
        Me.GroupBox1.Controls.Add(Me.tbSize04)
        Me.GroupBox1.Controls.Add(Me.tbSize16)
        Me.GroupBox1.Controls.Add(Me.tbSize05)
        Me.GroupBox1.Controls.Add(Me.tbSize06)
        Me.GroupBox1.Controls.Add(Me.tbSize15)
        Me.GroupBox1.Controls.Add(Me.tbSize07)
        Me.GroupBox1.Controls.Add(Me.tbSize14)
        Me.GroupBox1.Controls.Add(Me.tbSize08)
        Me.GroupBox1.Controls.Add(Me.tbSize13)
        Me.GroupBox1.Controls.Add(Me.tbSize09)
        Me.GroupBox1.Controls.Add(Me.tbSize12)
        Me.GroupBox1.Controls.Add(Me.tbSize10)
        Me.GroupBox1.Controls.Add(Me.tbSize11)
        Me.GroupBox1.Controls.Add(Me.tbPairsTotal)
        Me.GroupBox1.Controls.Add(Me.tbPairs17)
        Me.GroupBox1.Controls.Add(Me.tbPairs18)
        Me.GroupBox1.Controls.Add(Me.tbPairs19)
        Me.GroupBox1.Controls.Add(Me.tbPairs20)
        Me.GroupBox1.Controls.Add(Me.tbPairs02)
        Me.GroupBox1.Controls.Add(Me.tbPairs01)
        Me.GroupBox1.Controls.Add(Me.tbPairs03)
        Me.GroupBox1.Controls.Add(Me.tbPairs04)
        Me.GroupBox1.Controls.Add(Me.tbPairs16)
        Me.GroupBox1.Controls.Add(Me.tbPairs05)
        Me.GroupBox1.Controls.Add(Me.tbPairs06)
        Me.GroupBox1.Controls.Add(Me.tbPairs15)
        Me.GroupBox1.Controls.Add(Me.tbPairs07)
        Me.GroupBox1.Controls.Add(Me.tbPairs14)
        Me.GroupBox1.Controls.Add(Me.tbPairs08)
        Me.GroupBox1.Controls.Add(Me.tbPairs13)
        Me.GroupBox1.Controls.Add(Me.tbPairs09)
        Me.GroupBox1.Controls.Add(Me.tbPairs12)
        Me.GroupBox1.Controls.Add(Me.tbPairs10)
        Me.GroupBox1.Controls.Add(Me.tbPairs11)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label73)
        Me.GroupBox1.Controls.Add(Me.Label52)
        Me.GroupBox1.Controls.Add(Me.grdTempPlQty)
        Me.GroupBox1.Controls.Add(Me.grdOrderDetails)
        Me.GroupBox1.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(7, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(993, 512)
        Me.GroupBox1.TabIndex = 1019
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Order Details Info :-"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label25)
        Me.GroupBox7.Controls.Add(Me.tbManualBoxQty)
        Me.GroupBox7.Controls.Add(Me.tbTotalBoxes)
        Me.GroupBox7.Controls.Add(Me.Label26)
        Me.GroupBox7.Location = New System.Drawing.Point(293, 148)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(296, 47)
        Me.GroupBox7.TabIndex = 1158
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Multiple Box Packing Information :-"
        '
        'Label25
        '
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(14, 20)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(60, 22)
        Me.Label25.TabIndex = 1152
        Me.Label25.Text = "Box Qty :-"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbManualBoxQty
        '
        Me.tbManualBoxQty.BackColor = System.Drawing.Color.White
        Me.tbManualBoxQty.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbManualBoxQty.ForeColor = System.Drawing.Color.Blue
        Me.tbManualBoxQty.Location = New System.Drawing.Point(78, 19)
        Me.tbManualBoxQty.MaxLength = 20
        Me.tbManualBoxQty.Name = "tbManualBoxQty"
        Me.tbManualBoxQty.Size = New System.Drawing.Size(43, 22)
        Me.tbManualBoxQty.TabIndex = 1153
        Me.tbManualBoxQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbTotalBoxes
        '
        Me.tbTotalBoxes.BackColor = System.Drawing.Color.White
        Me.tbTotalBoxes.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalBoxes.ForeColor = System.Drawing.Color.Blue
        Me.tbTotalBoxes.Location = New System.Drawing.Point(248, 19)
        Me.tbTotalBoxes.MaxLength = 20
        Me.tbTotalBoxes.Name = "tbTotalBoxes"
        Me.tbTotalBoxes.Size = New System.Drawing.Size(43, 22)
        Me.tbTotalBoxes.TabIndex = 1156
        Me.tbTotalBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(125, 19)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(119, 22)
        Me.Label26.TabIndex = 1154
        Me.Label26.Text = "Total No. of Boxes :-"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbMultipleBox
        '
        Me.rbMultipleBox.AutoSize = True
        Me.rbMultipleBox.Location = New System.Drawing.Point(146, 161)
        Me.rbMultipleBox.Name = "rbMultipleBox"
        Me.rbMultipleBox.Size = New System.Drawing.Size(141, 21)
        Me.rbMultipleBox.TabIndex = 1157
        Me.rbMultipleBox.Text = "Multiple Box Packing"
        Me.rbMultipleBox.UseVisualStyleBackColor = True
        '
        'rbSingleBox
        '
        Me.rbSingleBox.AutoSize = True
        Me.rbSingleBox.Checked = True
        Me.rbSingleBox.Location = New System.Drawing.Point(6, 161)
        Me.rbSingleBox.Name = "rbSingleBox"
        Me.rbSingleBox.Size = New System.Drawing.Size(129, 21)
        Me.rbSingleBox.TabIndex = 1155
        Me.rbSingleBox.TabStop = True
        Me.rbSingleBox.Text = "Single Box Packing"
        Me.rbSingleBox.UseVisualStyleBackColor = True
        '
        'tbBalQtyTotal
        '
        Me.tbBalQtyTotal.BackColor = System.Drawing.Color.White
        Me.tbBalQtyTotal.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQtyTotal.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQtyTotal.Location = New System.Drawing.Point(944, 122)
        Me.tbBalQtyTotal.MaxLength = 20
        Me.tbBalQtyTotal.Name = "tbBalQtyTotal"
        Me.tbBalQtyTotal.ReadOnly = True
        Me.tbBalQtyTotal.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQtyTotal.TabIndex = 1151
        Me.tbBalQtyTotal.TabStop = False
        Me.tbBalQtyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty17
        '
        Me.tbBalQty17.BackColor = System.Drawing.Color.White
        Me.tbBalQty17.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty17.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty17.Location = New System.Drawing.Point(776, 122)
        Me.tbBalQty17.MaxLength = 20
        Me.tbBalQty17.Name = "tbBalQty17"
        Me.tbBalQty17.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty17.TabIndex = 1147
        Me.tbBalQty17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty18
        '
        Me.tbBalQty18.BackColor = System.Drawing.Color.White
        Me.tbBalQty18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty18.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty18.Location = New System.Drawing.Point(818, 122)
        Me.tbBalQty18.MaxLength = 20
        Me.tbBalQty18.Name = "tbBalQty18"
        Me.tbBalQty18.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty18.TabIndex = 1148
        Me.tbBalQty18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty19
        '
        Me.tbBalQty19.BackColor = System.Drawing.Color.White
        Me.tbBalQty19.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty19.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty19.Location = New System.Drawing.Point(860, 122)
        Me.tbBalQty19.MaxLength = 20
        Me.tbBalQty19.Name = "tbBalQty19"
        Me.tbBalQty19.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty19.TabIndex = 1149
        Me.tbBalQty19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty20
        '
        Me.tbBalQty20.BackColor = System.Drawing.Color.White
        Me.tbBalQty20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold)
        Me.tbBalQty20.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty20.Location = New System.Drawing.Point(902, 122)
        Me.tbBalQty20.MaxLength = 20
        Me.tbBalQty20.Name = "tbBalQty20"
        Me.tbBalQty20.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty20.TabIndex = 1150
        Me.tbBalQty20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty02
        '
        Me.tbBalQty02.BackColor = System.Drawing.Color.White
        Me.tbBalQty02.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty02.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty02.Location = New System.Drawing.Point(146, 122)
        Me.tbBalQty02.MaxLength = 20
        Me.tbBalQty02.Name = "tbBalQty02"
        Me.tbBalQty02.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty02.TabIndex = 1132
        Me.tbBalQty02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty01
        '
        Me.tbBalQty01.BackColor = System.Drawing.Color.White
        Me.tbBalQty01.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty01.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty01.Location = New System.Drawing.Point(104, 122)
        Me.tbBalQty01.MaxLength = 20
        Me.tbBalQty01.Name = "tbBalQty01"
        Me.tbBalQty01.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty01.TabIndex = 1131
        Me.tbBalQty01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty03
        '
        Me.tbBalQty03.BackColor = System.Drawing.Color.White
        Me.tbBalQty03.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty03.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty03.Location = New System.Drawing.Point(188, 122)
        Me.tbBalQty03.MaxLength = 20
        Me.tbBalQty03.Name = "tbBalQty03"
        Me.tbBalQty03.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty03.TabIndex = 1133
        Me.tbBalQty03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty04
        '
        Me.tbBalQty04.BackColor = System.Drawing.Color.White
        Me.tbBalQty04.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty04.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty04.Location = New System.Drawing.Point(230, 122)
        Me.tbBalQty04.MaxLength = 20
        Me.tbBalQty04.Name = "tbBalQty04"
        Me.tbBalQty04.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty04.TabIndex = 1134
        Me.tbBalQty04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty16
        '
        Me.tbBalQty16.BackColor = System.Drawing.Color.White
        Me.tbBalQty16.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty16.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty16.Location = New System.Drawing.Point(734, 122)
        Me.tbBalQty16.MaxLength = 20
        Me.tbBalQty16.Name = "tbBalQty16"
        Me.tbBalQty16.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty16.TabIndex = 1146
        Me.tbBalQty16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty05
        '
        Me.tbBalQty05.BackColor = System.Drawing.Color.White
        Me.tbBalQty05.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty05.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty05.Location = New System.Drawing.Point(272, 122)
        Me.tbBalQty05.MaxLength = 20
        Me.tbBalQty05.Name = "tbBalQty05"
        Me.tbBalQty05.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty05.TabIndex = 1135
        Me.tbBalQty05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty06
        '
        Me.tbBalQty06.BackColor = System.Drawing.Color.White
        Me.tbBalQty06.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty06.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty06.Location = New System.Drawing.Point(314, 122)
        Me.tbBalQty06.MaxLength = 20
        Me.tbBalQty06.Name = "tbBalQty06"
        Me.tbBalQty06.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty06.TabIndex = 1136
        Me.tbBalQty06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty15
        '
        Me.tbBalQty15.BackColor = System.Drawing.Color.White
        Me.tbBalQty15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty15.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty15.Location = New System.Drawing.Point(692, 122)
        Me.tbBalQty15.MaxLength = 20
        Me.tbBalQty15.Name = "tbBalQty15"
        Me.tbBalQty15.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty15.TabIndex = 1145
        Me.tbBalQty15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty07
        '
        Me.tbBalQty07.BackColor = System.Drawing.Color.White
        Me.tbBalQty07.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty07.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty07.Location = New System.Drawing.Point(356, 122)
        Me.tbBalQty07.MaxLength = 20
        Me.tbBalQty07.Name = "tbBalQty07"
        Me.tbBalQty07.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty07.TabIndex = 1137
        Me.tbBalQty07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty14
        '
        Me.tbBalQty14.BackColor = System.Drawing.Color.White
        Me.tbBalQty14.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty14.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty14.Location = New System.Drawing.Point(650, 122)
        Me.tbBalQty14.MaxLength = 20
        Me.tbBalQty14.Name = "tbBalQty14"
        Me.tbBalQty14.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty14.TabIndex = 1144
        Me.tbBalQty14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty08
        '
        Me.tbBalQty08.BackColor = System.Drawing.Color.White
        Me.tbBalQty08.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty08.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty08.Location = New System.Drawing.Point(398, 122)
        Me.tbBalQty08.MaxLength = 20
        Me.tbBalQty08.Name = "tbBalQty08"
        Me.tbBalQty08.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty08.TabIndex = 1138
        Me.tbBalQty08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty13
        '
        Me.tbBalQty13.BackColor = System.Drawing.Color.White
        Me.tbBalQty13.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty13.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty13.Location = New System.Drawing.Point(608, 122)
        Me.tbBalQty13.MaxLength = 20
        Me.tbBalQty13.Name = "tbBalQty13"
        Me.tbBalQty13.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty13.TabIndex = 1143
        Me.tbBalQty13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty09
        '
        Me.tbBalQty09.BackColor = System.Drawing.Color.White
        Me.tbBalQty09.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty09.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty09.Location = New System.Drawing.Point(440, 122)
        Me.tbBalQty09.MaxLength = 20
        Me.tbBalQty09.Name = "tbBalQty09"
        Me.tbBalQty09.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty09.TabIndex = 1139
        Me.tbBalQty09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty12
        '
        Me.tbBalQty12.BackColor = System.Drawing.Color.White
        Me.tbBalQty12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty12.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty12.Location = New System.Drawing.Point(566, 122)
        Me.tbBalQty12.MaxLength = 20
        Me.tbBalQty12.Name = "tbBalQty12"
        Me.tbBalQty12.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty12.TabIndex = 1142
        Me.tbBalQty12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty10
        '
        Me.tbBalQty10.BackColor = System.Drawing.Color.White
        Me.tbBalQty10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty10.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty10.Location = New System.Drawing.Point(482, 122)
        Me.tbBalQty10.MaxLength = 20
        Me.tbBalQty10.Name = "tbBalQty10"
        Me.tbBalQty10.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty10.TabIndex = 1140
        Me.tbBalQty10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbBalQty11
        '
        Me.tbBalQty11.BackColor = System.Drawing.Color.White
        Me.tbBalQty11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBalQty11.ForeColor = System.Drawing.Color.Blue
        Me.tbBalQty11.Location = New System.Drawing.Point(524, 122)
        Me.tbBalQty11.MaxLength = 20
        Me.tbBalQty11.Name = "tbBalQty11"
        Me.tbBalQty11.Size = New System.Drawing.Size(43, 22)
        Me.tbBalQty11.TabIndex = 1141
        Me.tbBalQty11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQtyTotal
        '
        Me.tbPLQtyTotal.BackColor = System.Drawing.Color.White
        Me.tbPLQtyTotal.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQtyTotal.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQtyTotal.Location = New System.Drawing.Point(944, 101)
        Me.tbPLQtyTotal.MaxLength = 20
        Me.tbPLQtyTotal.Name = "tbPLQtyTotal"
        Me.tbPLQtyTotal.ReadOnly = True
        Me.tbPLQtyTotal.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQtyTotal.TabIndex = 1130
        Me.tbPLQtyTotal.TabStop = False
        Me.tbPLQtyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty17
        '
        Me.tbPLQty17.BackColor = System.Drawing.Color.White
        Me.tbPLQty17.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty17.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty17.Location = New System.Drawing.Point(776, 101)
        Me.tbPLQty17.MaxLength = 20
        Me.tbPLQty17.Name = "tbPLQty17"
        Me.tbPLQty17.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty17.TabIndex = 1126
        Me.tbPLQty17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty18
        '
        Me.tbPLQty18.BackColor = System.Drawing.Color.White
        Me.tbPLQty18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty18.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty18.Location = New System.Drawing.Point(818, 101)
        Me.tbPLQty18.MaxLength = 20
        Me.tbPLQty18.Name = "tbPLQty18"
        Me.tbPLQty18.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty18.TabIndex = 1127
        Me.tbPLQty18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty19
        '
        Me.tbPLQty19.BackColor = System.Drawing.Color.White
        Me.tbPLQty19.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty19.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty19.Location = New System.Drawing.Point(860, 101)
        Me.tbPLQty19.MaxLength = 20
        Me.tbPLQty19.Name = "tbPLQty19"
        Me.tbPLQty19.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty19.TabIndex = 1128
        Me.tbPLQty19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty20
        '
        Me.tbPLQty20.BackColor = System.Drawing.Color.White
        Me.tbPLQty20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold)
        Me.tbPLQty20.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty20.Location = New System.Drawing.Point(902, 101)
        Me.tbPLQty20.MaxLength = 20
        Me.tbPLQty20.Name = "tbPLQty20"
        Me.tbPLQty20.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty20.TabIndex = 1129
        Me.tbPLQty20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty02
        '
        Me.tbPLQty02.BackColor = System.Drawing.Color.White
        Me.tbPLQty02.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty02.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty02.Location = New System.Drawing.Point(146, 101)
        Me.tbPLQty02.MaxLength = 20
        Me.tbPLQty02.Name = "tbPLQty02"
        Me.tbPLQty02.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty02.TabIndex = 1111
        Me.tbPLQty02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty01
        '
        Me.tbPLQty01.BackColor = System.Drawing.Color.White
        Me.tbPLQty01.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty01.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty01.Location = New System.Drawing.Point(104, 101)
        Me.tbPLQty01.MaxLength = 20
        Me.tbPLQty01.Name = "tbPLQty01"
        Me.tbPLQty01.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty01.TabIndex = 1110
        Me.tbPLQty01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty03
        '
        Me.tbPLQty03.BackColor = System.Drawing.Color.White
        Me.tbPLQty03.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty03.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty03.Location = New System.Drawing.Point(188, 101)
        Me.tbPLQty03.MaxLength = 20
        Me.tbPLQty03.Name = "tbPLQty03"
        Me.tbPLQty03.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty03.TabIndex = 1112
        Me.tbPLQty03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty04
        '
        Me.tbPLQty04.BackColor = System.Drawing.Color.White
        Me.tbPLQty04.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty04.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty04.Location = New System.Drawing.Point(230, 101)
        Me.tbPLQty04.MaxLength = 20
        Me.tbPLQty04.Name = "tbPLQty04"
        Me.tbPLQty04.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty04.TabIndex = 1113
        Me.tbPLQty04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty16
        '
        Me.tbPLQty16.BackColor = System.Drawing.Color.White
        Me.tbPLQty16.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty16.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty16.Location = New System.Drawing.Point(734, 101)
        Me.tbPLQty16.MaxLength = 20
        Me.tbPLQty16.Name = "tbPLQty16"
        Me.tbPLQty16.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty16.TabIndex = 1125
        Me.tbPLQty16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty05
        '
        Me.tbPLQty05.BackColor = System.Drawing.Color.White
        Me.tbPLQty05.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty05.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty05.Location = New System.Drawing.Point(272, 101)
        Me.tbPLQty05.MaxLength = 20
        Me.tbPLQty05.Name = "tbPLQty05"
        Me.tbPLQty05.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty05.TabIndex = 1114
        Me.tbPLQty05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty06
        '
        Me.tbPLQty06.BackColor = System.Drawing.Color.White
        Me.tbPLQty06.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty06.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty06.Location = New System.Drawing.Point(314, 101)
        Me.tbPLQty06.MaxLength = 20
        Me.tbPLQty06.Name = "tbPLQty06"
        Me.tbPLQty06.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty06.TabIndex = 1115
        Me.tbPLQty06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty15
        '
        Me.tbPLQty15.BackColor = System.Drawing.Color.White
        Me.tbPLQty15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty15.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty15.Location = New System.Drawing.Point(692, 101)
        Me.tbPLQty15.MaxLength = 20
        Me.tbPLQty15.Name = "tbPLQty15"
        Me.tbPLQty15.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty15.TabIndex = 1124
        Me.tbPLQty15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty07
        '
        Me.tbPLQty07.BackColor = System.Drawing.Color.White
        Me.tbPLQty07.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty07.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty07.Location = New System.Drawing.Point(356, 101)
        Me.tbPLQty07.MaxLength = 20
        Me.tbPLQty07.Name = "tbPLQty07"
        Me.tbPLQty07.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty07.TabIndex = 1116
        Me.tbPLQty07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty14
        '
        Me.tbPLQty14.BackColor = System.Drawing.Color.White
        Me.tbPLQty14.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty14.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty14.Location = New System.Drawing.Point(650, 101)
        Me.tbPLQty14.MaxLength = 20
        Me.tbPLQty14.Name = "tbPLQty14"
        Me.tbPLQty14.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty14.TabIndex = 1123
        Me.tbPLQty14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty08
        '
        Me.tbPLQty08.BackColor = System.Drawing.Color.White
        Me.tbPLQty08.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty08.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty08.Location = New System.Drawing.Point(398, 101)
        Me.tbPLQty08.MaxLength = 20
        Me.tbPLQty08.Name = "tbPLQty08"
        Me.tbPLQty08.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty08.TabIndex = 1117
        Me.tbPLQty08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty13
        '
        Me.tbPLQty13.BackColor = System.Drawing.Color.White
        Me.tbPLQty13.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty13.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty13.Location = New System.Drawing.Point(608, 101)
        Me.tbPLQty13.MaxLength = 20
        Me.tbPLQty13.Name = "tbPLQty13"
        Me.tbPLQty13.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty13.TabIndex = 1122
        Me.tbPLQty13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty09
        '
        Me.tbPLQty09.BackColor = System.Drawing.Color.White
        Me.tbPLQty09.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty09.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty09.Location = New System.Drawing.Point(440, 101)
        Me.tbPLQty09.MaxLength = 20
        Me.tbPLQty09.Name = "tbPLQty09"
        Me.tbPLQty09.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty09.TabIndex = 1118
        Me.tbPLQty09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty12
        '
        Me.tbPLQty12.BackColor = System.Drawing.Color.White
        Me.tbPLQty12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty12.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty12.Location = New System.Drawing.Point(566, 101)
        Me.tbPLQty12.MaxLength = 20
        Me.tbPLQty12.Name = "tbPLQty12"
        Me.tbPLQty12.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty12.TabIndex = 1121
        Me.tbPLQty12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty10
        '
        Me.tbPLQty10.BackColor = System.Drawing.Color.White
        Me.tbPLQty10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty10.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty10.Location = New System.Drawing.Point(482, 101)
        Me.tbPLQty10.MaxLength = 20
        Me.tbPLQty10.Name = "tbPLQty10"
        Me.tbPLQty10.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty10.TabIndex = 1119
        Me.tbPLQty10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLQty11
        '
        Me.tbPLQty11.BackColor = System.Drawing.Color.White
        Me.tbPLQty11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLQty11.ForeColor = System.Drawing.Color.Blue
        Me.tbPLQty11.Location = New System.Drawing.Point(524, 101)
        Me.tbPLQty11.MaxLength = 20
        Me.tbPLQty11.Name = "tbPLQty11"
        Me.tbPLQty11.Size = New System.Drawing.Size(43, 22)
        Me.tbPLQty11.TabIndex = 1120
        Me.tbPLQty11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQtyTotal
        '
        Me.tbPLBalQtyTotal.BackColor = System.Drawing.Color.White
        Me.tbPLBalQtyTotal.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQtyTotal.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQtyTotal.Location = New System.Drawing.Point(944, 80)
        Me.tbPLBalQtyTotal.MaxLength = 20
        Me.tbPLBalQtyTotal.Name = "tbPLBalQtyTotal"
        Me.tbPLBalQtyTotal.ReadOnly = True
        Me.tbPLBalQtyTotal.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQtyTotal.TabIndex = 1109
        Me.tbPLBalQtyTotal.TabStop = False
        Me.tbPLBalQtyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty17
        '
        Me.tbPLBalQty17.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty17.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty17.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty17.Location = New System.Drawing.Point(776, 80)
        Me.tbPLBalQty17.MaxLength = 20
        Me.tbPLBalQty17.Name = "tbPLBalQty17"
        Me.tbPLBalQty17.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty17.TabIndex = 1105
        Me.tbPLBalQty17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty18
        '
        Me.tbPLBalQty18.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty18.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty18.Location = New System.Drawing.Point(818, 80)
        Me.tbPLBalQty18.MaxLength = 20
        Me.tbPLBalQty18.Name = "tbPLBalQty18"
        Me.tbPLBalQty18.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty18.TabIndex = 1106
        Me.tbPLBalQty18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty19
        '
        Me.tbPLBalQty19.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty19.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty19.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty19.Location = New System.Drawing.Point(860, 80)
        Me.tbPLBalQty19.MaxLength = 20
        Me.tbPLBalQty19.Name = "tbPLBalQty19"
        Me.tbPLBalQty19.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty19.TabIndex = 1107
        Me.tbPLBalQty19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty20
        '
        Me.tbPLBalQty20.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold)
        Me.tbPLBalQty20.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty20.Location = New System.Drawing.Point(902, 80)
        Me.tbPLBalQty20.MaxLength = 20
        Me.tbPLBalQty20.Name = "tbPLBalQty20"
        Me.tbPLBalQty20.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty20.TabIndex = 1108
        Me.tbPLBalQty20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty02
        '
        Me.tbPLBalQty02.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty02.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty02.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty02.Location = New System.Drawing.Point(146, 80)
        Me.tbPLBalQty02.MaxLength = 20
        Me.tbPLBalQty02.Name = "tbPLBalQty02"
        Me.tbPLBalQty02.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty02.TabIndex = 1090
        Me.tbPLBalQty02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty01
        '
        Me.tbPLBalQty01.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty01.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty01.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty01.Location = New System.Drawing.Point(104, 80)
        Me.tbPLBalQty01.MaxLength = 20
        Me.tbPLBalQty01.Name = "tbPLBalQty01"
        Me.tbPLBalQty01.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty01.TabIndex = 1089
        Me.tbPLBalQty01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty03
        '
        Me.tbPLBalQty03.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty03.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty03.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty03.Location = New System.Drawing.Point(188, 80)
        Me.tbPLBalQty03.MaxLength = 20
        Me.tbPLBalQty03.Name = "tbPLBalQty03"
        Me.tbPLBalQty03.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty03.TabIndex = 1091
        Me.tbPLBalQty03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty04
        '
        Me.tbPLBalQty04.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty04.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty04.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty04.Location = New System.Drawing.Point(230, 80)
        Me.tbPLBalQty04.MaxLength = 20
        Me.tbPLBalQty04.Name = "tbPLBalQty04"
        Me.tbPLBalQty04.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty04.TabIndex = 1092
        Me.tbPLBalQty04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty16
        '
        Me.tbPLBalQty16.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty16.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty16.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty16.Location = New System.Drawing.Point(734, 80)
        Me.tbPLBalQty16.MaxLength = 20
        Me.tbPLBalQty16.Name = "tbPLBalQty16"
        Me.tbPLBalQty16.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty16.TabIndex = 1104
        Me.tbPLBalQty16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty05
        '
        Me.tbPLBalQty05.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty05.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty05.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty05.Location = New System.Drawing.Point(272, 80)
        Me.tbPLBalQty05.MaxLength = 20
        Me.tbPLBalQty05.Name = "tbPLBalQty05"
        Me.tbPLBalQty05.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty05.TabIndex = 1093
        Me.tbPLBalQty05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty06
        '
        Me.tbPLBalQty06.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty06.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty06.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty06.Location = New System.Drawing.Point(314, 80)
        Me.tbPLBalQty06.MaxLength = 20
        Me.tbPLBalQty06.Name = "tbPLBalQty06"
        Me.tbPLBalQty06.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty06.TabIndex = 1094
        Me.tbPLBalQty06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty15
        '
        Me.tbPLBalQty15.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty15.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty15.Location = New System.Drawing.Point(692, 80)
        Me.tbPLBalQty15.MaxLength = 20
        Me.tbPLBalQty15.Name = "tbPLBalQty15"
        Me.tbPLBalQty15.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty15.TabIndex = 1103
        Me.tbPLBalQty15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty07
        '
        Me.tbPLBalQty07.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty07.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty07.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty07.Location = New System.Drawing.Point(356, 80)
        Me.tbPLBalQty07.MaxLength = 20
        Me.tbPLBalQty07.Name = "tbPLBalQty07"
        Me.tbPLBalQty07.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty07.TabIndex = 1095
        Me.tbPLBalQty07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty14
        '
        Me.tbPLBalQty14.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty14.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty14.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty14.Location = New System.Drawing.Point(650, 80)
        Me.tbPLBalQty14.MaxLength = 20
        Me.tbPLBalQty14.Name = "tbPLBalQty14"
        Me.tbPLBalQty14.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty14.TabIndex = 1102
        Me.tbPLBalQty14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty08
        '
        Me.tbPLBalQty08.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty08.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty08.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty08.Location = New System.Drawing.Point(398, 80)
        Me.tbPLBalQty08.MaxLength = 20
        Me.tbPLBalQty08.Name = "tbPLBalQty08"
        Me.tbPLBalQty08.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty08.TabIndex = 1096
        Me.tbPLBalQty08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty13
        '
        Me.tbPLBalQty13.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty13.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty13.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty13.Location = New System.Drawing.Point(608, 80)
        Me.tbPLBalQty13.MaxLength = 20
        Me.tbPLBalQty13.Name = "tbPLBalQty13"
        Me.tbPLBalQty13.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty13.TabIndex = 1101
        Me.tbPLBalQty13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty09
        '
        Me.tbPLBalQty09.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty09.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty09.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty09.Location = New System.Drawing.Point(440, 80)
        Me.tbPLBalQty09.MaxLength = 20
        Me.tbPLBalQty09.Name = "tbPLBalQty09"
        Me.tbPLBalQty09.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty09.TabIndex = 1097
        Me.tbPLBalQty09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty12
        '
        Me.tbPLBalQty12.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty12.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty12.Location = New System.Drawing.Point(566, 80)
        Me.tbPLBalQty12.MaxLength = 20
        Me.tbPLBalQty12.Name = "tbPLBalQty12"
        Me.tbPLBalQty12.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty12.TabIndex = 1100
        Me.tbPLBalQty12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty10
        '
        Me.tbPLBalQty10.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty10.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty10.Location = New System.Drawing.Point(482, 80)
        Me.tbPLBalQty10.MaxLength = 20
        Me.tbPLBalQty10.Name = "tbPLBalQty10"
        Me.tbPLBalQty10.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty10.TabIndex = 1098
        Me.tbPLBalQty10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPLBalQty11
        '
        Me.tbPLBalQty11.BackColor = System.Drawing.Color.White
        Me.tbPLBalQty11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPLBalQty11.ForeColor = System.Drawing.Color.Blue
        Me.tbPLBalQty11.Location = New System.Drawing.Point(524, 80)
        Me.tbPLBalQty11.MaxLength = 20
        Me.tbPLBalQty11.Name = "tbPLBalQty11"
        Me.tbPLBalQty11.Size = New System.Drawing.Size(43, 22)
        Me.tbPLBalQty11.TabIndex = 1099
        Me.tbPLBalQty11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQtyTotal
        '
        Me.tbPlGeneratedQtyTotal.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQtyTotal.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQtyTotal.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQtyTotal.Location = New System.Drawing.Point(944, 59)
        Me.tbPlGeneratedQtyTotal.MaxLength = 20
        Me.tbPlGeneratedQtyTotal.Name = "tbPlGeneratedQtyTotal"
        Me.tbPlGeneratedQtyTotal.ReadOnly = True
        Me.tbPlGeneratedQtyTotal.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQtyTotal.TabIndex = 1088
        Me.tbPlGeneratedQtyTotal.TabStop = False
        Me.tbPlGeneratedQtyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty17
        '
        Me.tbPlGeneratedQty17.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty17.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty17.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty17.Location = New System.Drawing.Point(776, 59)
        Me.tbPlGeneratedQty17.MaxLength = 20
        Me.tbPlGeneratedQty17.Name = "tbPlGeneratedQty17"
        Me.tbPlGeneratedQty17.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty17.TabIndex = 1084
        Me.tbPlGeneratedQty17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty18
        '
        Me.tbPlGeneratedQty18.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty18.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty18.Location = New System.Drawing.Point(818, 59)
        Me.tbPlGeneratedQty18.MaxLength = 20
        Me.tbPlGeneratedQty18.Name = "tbPlGeneratedQty18"
        Me.tbPlGeneratedQty18.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty18.TabIndex = 1085
        Me.tbPlGeneratedQty18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty19
        '
        Me.tbPlGeneratedQty19.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty19.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty19.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty19.Location = New System.Drawing.Point(860, 59)
        Me.tbPlGeneratedQty19.MaxLength = 20
        Me.tbPlGeneratedQty19.Name = "tbPlGeneratedQty19"
        Me.tbPlGeneratedQty19.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty19.TabIndex = 1086
        Me.tbPlGeneratedQty19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty20
        '
        Me.tbPlGeneratedQty20.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold)
        Me.tbPlGeneratedQty20.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty20.Location = New System.Drawing.Point(902, 59)
        Me.tbPlGeneratedQty20.MaxLength = 20
        Me.tbPlGeneratedQty20.Name = "tbPlGeneratedQty20"
        Me.tbPlGeneratedQty20.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty20.TabIndex = 1087
        Me.tbPlGeneratedQty20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty02
        '
        Me.tbPlGeneratedQty02.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty02.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty02.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty02.Location = New System.Drawing.Point(146, 59)
        Me.tbPlGeneratedQty02.MaxLength = 20
        Me.tbPlGeneratedQty02.Name = "tbPlGeneratedQty02"
        Me.tbPlGeneratedQty02.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty02.TabIndex = 1069
        Me.tbPlGeneratedQty02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty01
        '
        Me.tbPlGeneratedQty01.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty01.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty01.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty01.Location = New System.Drawing.Point(104, 59)
        Me.tbPlGeneratedQty01.MaxLength = 20
        Me.tbPlGeneratedQty01.Name = "tbPlGeneratedQty01"
        Me.tbPlGeneratedQty01.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty01.TabIndex = 1068
        Me.tbPlGeneratedQty01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty03
        '
        Me.tbPlGeneratedQty03.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty03.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty03.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty03.Location = New System.Drawing.Point(188, 59)
        Me.tbPlGeneratedQty03.MaxLength = 20
        Me.tbPlGeneratedQty03.Name = "tbPlGeneratedQty03"
        Me.tbPlGeneratedQty03.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty03.TabIndex = 1070
        Me.tbPlGeneratedQty03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty04
        '
        Me.tbPlGeneratedQty04.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty04.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty04.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty04.Location = New System.Drawing.Point(230, 59)
        Me.tbPlGeneratedQty04.MaxLength = 20
        Me.tbPlGeneratedQty04.Name = "tbPlGeneratedQty04"
        Me.tbPlGeneratedQty04.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty04.TabIndex = 1071
        Me.tbPlGeneratedQty04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty16
        '
        Me.tbPlGeneratedQty16.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty16.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty16.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty16.Location = New System.Drawing.Point(734, 59)
        Me.tbPlGeneratedQty16.MaxLength = 20
        Me.tbPlGeneratedQty16.Name = "tbPlGeneratedQty16"
        Me.tbPlGeneratedQty16.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty16.TabIndex = 1083
        Me.tbPlGeneratedQty16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty05
        '
        Me.tbPlGeneratedQty05.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty05.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty05.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty05.Location = New System.Drawing.Point(272, 59)
        Me.tbPlGeneratedQty05.MaxLength = 20
        Me.tbPlGeneratedQty05.Name = "tbPlGeneratedQty05"
        Me.tbPlGeneratedQty05.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty05.TabIndex = 1072
        Me.tbPlGeneratedQty05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty06
        '
        Me.tbPlGeneratedQty06.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty06.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty06.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty06.Location = New System.Drawing.Point(314, 59)
        Me.tbPlGeneratedQty06.MaxLength = 20
        Me.tbPlGeneratedQty06.Name = "tbPlGeneratedQty06"
        Me.tbPlGeneratedQty06.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty06.TabIndex = 1073
        Me.tbPlGeneratedQty06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty15
        '
        Me.tbPlGeneratedQty15.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty15.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty15.Location = New System.Drawing.Point(692, 59)
        Me.tbPlGeneratedQty15.MaxLength = 20
        Me.tbPlGeneratedQty15.Name = "tbPlGeneratedQty15"
        Me.tbPlGeneratedQty15.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty15.TabIndex = 1082
        Me.tbPlGeneratedQty15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty07
        '
        Me.tbPlGeneratedQty07.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty07.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty07.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty07.Location = New System.Drawing.Point(356, 59)
        Me.tbPlGeneratedQty07.MaxLength = 20
        Me.tbPlGeneratedQty07.Name = "tbPlGeneratedQty07"
        Me.tbPlGeneratedQty07.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty07.TabIndex = 1074
        Me.tbPlGeneratedQty07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty14
        '
        Me.tbPlGeneratedQty14.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty14.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty14.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty14.Location = New System.Drawing.Point(650, 59)
        Me.tbPlGeneratedQty14.MaxLength = 20
        Me.tbPlGeneratedQty14.Name = "tbPlGeneratedQty14"
        Me.tbPlGeneratedQty14.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty14.TabIndex = 1081
        Me.tbPlGeneratedQty14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty08
        '
        Me.tbPlGeneratedQty08.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty08.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty08.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty08.Location = New System.Drawing.Point(398, 59)
        Me.tbPlGeneratedQty08.MaxLength = 20
        Me.tbPlGeneratedQty08.Name = "tbPlGeneratedQty08"
        Me.tbPlGeneratedQty08.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty08.TabIndex = 1075
        Me.tbPlGeneratedQty08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty13
        '
        Me.tbPlGeneratedQty13.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty13.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty13.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty13.Location = New System.Drawing.Point(608, 59)
        Me.tbPlGeneratedQty13.MaxLength = 20
        Me.tbPlGeneratedQty13.Name = "tbPlGeneratedQty13"
        Me.tbPlGeneratedQty13.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty13.TabIndex = 1080
        Me.tbPlGeneratedQty13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty09
        '
        Me.tbPlGeneratedQty09.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty09.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty09.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty09.Location = New System.Drawing.Point(440, 59)
        Me.tbPlGeneratedQty09.MaxLength = 20
        Me.tbPlGeneratedQty09.Name = "tbPlGeneratedQty09"
        Me.tbPlGeneratedQty09.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty09.TabIndex = 1076
        Me.tbPlGeneratedQty09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty12
        '
        Me.tbPlGeneratedQty12.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty12.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty12.Location = New System.Drawing.Point(566, 59)
        Me.tbPlGeneratedQty12.MaxLength = 20
        Me.tbPlGeneratedQty12.Name = "tbPlGeneratedQty12"
        Me.tbPlGeneratedQty12.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty12.TabIndex = 1079
        Me.tbPlGeneratedQty12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty10
        '
        Me.tbPlGeneratedQty10.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty10.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty10.Location = New System.Drawing.Point(482, 59)
        Me.tbPlGeneratedQty10.MaxLength = 20
        Me.tbPlGeneratedQty10.Name = "tbPlGeneratedQty10"
        Me.tbPlGeneratedQty10.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty10.TabIndex = 1077
        Me.tbPlGeneratedQty10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPlGeneratedQty11
        '
        Me.tbPlGeneratedQty11.BackColor = System.Drawing.Color.White
        Me.tbPlGeneratedQty11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPlGeneratedQty11.ForeColor = System.Drawing.Color.Blue
        Me.tbPlGeneratedQty11.Location = New System.Drawing.Point(524, 59)
        Me.tbPlGeneratedQty11.MaxLength = 20
        Me.tbPlGeneratedQty11.Name = "tbPlGeneratedQty11"
        Me.tbPlGeneratedQty11.Size = New System.Drawing.Size(43, 22)
        Me.tbPlGeneratedQty11.TabIndex = 1078
        Me.tbPlGeneratedQty11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(5, 122)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 22)
        Me.Label11.TabIndex = 1067
        Me.Label11.Text = "Balance :-"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 101)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 22)
        Me.Label8.TabIndex = 1066
        Me.Label8.Text = "Curr PL Qty"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 22)
        Me.Label7.TabIndex = 1065
        Me.Label7.Text = "PL Bal :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 22)
        Me.Label4.TabIndex = 1064
        Me.Label4.Text = "PL. G'ated Qty :-"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbRemove
        '
        Me.cbRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRemove.ForeColor = System.Drawing.Color.DarkBlue
        Me.cbRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbRemove.ImageIndex = 3
        Me.cbRemove.ImageList = Me.ImageList1
        Me.cbRemove.Location = New System.Drawing.Point(890, 148)
        Me.cbRemove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbRemove.Name = "cbRemove"
        Me.cbRemove.Size = New System.Drawing.Size(96, 40)
        Me.cbRemove.TabIndex = 1061
        Me.cbRemove.Text = "&Remove"
        Me.cbRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbRemove.UseVisualStyleBackColor = True
        '
        'cbInclude
        '
        Me.cbInclude.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbInclude.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.cbInclude.ForeColor = System.Drawing.Color.DarkBlue
        Me.cbInclude.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbInclude.ImageIndex = 2
        Me.cbInclude.ImageList = Me.ImageList1
        Me.cbInclude.Location = New System.Drawing.Point(791, 148)
        Me.cbInclude.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbInclude.Name = "cbInclude"
        Me.cbInclude.Size = New System.Drawing.Size(96, 40)
        Me.cbInclude.TabIndex = 1060
        Me.cbInclude.Text = "I&nclude"
        Me.cbInclude.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbInclude.UseVisualStyleBackColor = True
        '
        'tbSize17
        '
        Me.tbSize17.BackColor = System.Drawing.Color.White
        Me.tbSize17.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize17.ForeColor = System.Drawing.Color.Brown
        Me.tbSize17.Location = New System.Drawing.Point(776, 17)
        Me.tbSize17.MaxLength = 20
        Me.tbSize17.Name = "tbSize17"
        Me.tbSize17.ReadOnly = True
        Me.tbSize17.Size = New System.Drawing.Size(43, 22)
        Me.tbSize17.TabIndex = 433
        Me.tbSize17.TabStop = False
        Me.tbSize17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize18
        '
        Me.tbSize18.BackColor = System.Drawing.Color.White
        Me.tbSize18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize18.ForeColor = System.Drawing.Color.Brown
        Me.tbSize18.Location = New System.Drawing.Point(818, 17)
        Me.tbSize18.MaxLength = 20
        Me.tbSize18.Name = "tbSize18"
        Me.tbSize18.ReadOnly = True
        Me.tbSize18.Size = New System.Drawing.Size(43, 22)
        Me.tbSize18.TabIndex = 434
        Me.tbSize18.TabStop = False
        Me.tbSize18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize19
        '
        Me.tbSize19.BackColor = System.Drawing.Color.White
        Me.tbSize19.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize19.ForeColor = System.Drawing.Color.Brown
        Me.tbSize19.Location = New System.Drawing.Point(860, 17)
        Me.tbSize19.MaxLength = 20
        Me.tbSize19.Name = "tbSize19"
        Me.tbSize19.ReadOnly = True
        Me.tbSize19.Size = New System.Drawing.Size(43, 22)
        Me.tbSize19.TabIndex = 435
        Me.tbSize19.TabStop = False
        Me.tbSize19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize20
        '
        Me.tbSize20.BackColor = System.Drawing.Color.White
        Me.tbSize20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize20.ForeColor = System.Drawing.Color.Brown
        Me.tbSize20.Location = New System.Drawing.Point(902, 17)
        Me.tbSize20.MaxLength = 20
        Me.tbSize20.Name = "tbSize20"
        Me.tbSize20.ReadOnly = True
        Me.tbSize20.Size = New System.Drawing.Size(43, 22)
        Me.tbSize20.TabIndex = 436
        Me.tbSize20.TabStop = False
        Me.tbSize20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize02
        '
        Me.tbSize02.BackColor = System.Drawing.Color.White
        Me.tbSize02.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize02.ForeColor = System.Drawing.Color.Brown
        Me.tbSize02.Location = New System.Drawing.Point(146, 17)
        Me.tbSize02.MaxLength = 20
        Me.tbSize02.Name = "tbSize02"
        Me.tbSize02.ReadOnly = True
        Me.tbSize02.Size = New System.Drawing.Size(43, 22)
        Me.tbSize02.TabIndex = 418
        Me.tbSize02.TabStop = False
        Me.tbSize02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize01
        '
        Me.tbSize01.BackColor = System.Drawing.Color.White
        Me.tbSize01.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize01.ForeColor = System.Drawing.Color.Brown
        Me.tbSize01.Location = New System.Drawing.Point(104, 17)
        Me.tbSize01.MaxLength = 20
        Me.tbSize01.Name = "tbSize01"
        Me.tbSize01.ReadOnly = True
        Me.tbSize01.Size = New System.Drawing.Size(43, 22)
        Me.tbSize01.TabIndex = 417
        Me.tbSize01.TabStop = False
        Me.tbSize01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize03
        '
        Me.tbSize03.BackColor = System.Drawing.Color.White
        Me.tbSize03.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize03.ForeColor = System.Drawing.Color.Brown
        Me.tbSize03.Location = New System.Drawing.Point(188, 17)
        Me.tbSize03.MaxLength = 20
        Me.tbSize03.Name = "tbSize03"
        Me.tbSize03.ReadOnly = True
        Me.tbSize03.Size = New System.Drawing.Size(43, 22)
        Me.tbSize03.TabIndex = 419
        Me.tbSize03.TabStop = False
        Me.tbSize03.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize04
        '
        Me.tbSize04.BackColor = System.Drawing.Color.White
        Me.tbSize04.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize04.ForeColor = System.Drawing.Color.Brown
        Me.tbSize04.Location = New System.Drawing.Point(230, 17)
        Me.tbSize04.MaxLength = 20
        Me.tbSize04.Name = "tbSize04"
        Me.tbSize04.ReadOnly = True
        Me.tbSize04.Size = New System.Drawing.Size(43, 22)
        Me.tbSize04.TabIndex = 420
        Me.tbSize04.TabStop = False
        Me.tbSize04.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize16
        '
        Me.tbSize16.BackColor = System.Drawing.Color.White
        Me.tbSize16.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize16.ForeColor = System.Drawing.Color.Brown
        Me.tbSize16.Location = New System.Drawing.Point(734, 17)
        Me.tbSize16.MaxLength = 20
        Me.tbSize16.Name = "tbSize16"
        Me.tbSize16.ReadOnly = True
        Me.tbSize16.Size = New System.Drawing.Size(43, 22)
        Me.tbSize16.TabIndex = 432
        Me.tbSize16.TabStop = False
        Me.tbSize16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize05
        '
        Me.tbSize05.BackColor = System.Drawing.Color.White
        Me.tbSize05.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize05.ForeColor = System.Drawing.Color.Brown
        Me.tbSize05.Location = New System.Drawing.Point(272, 17)
        Me.tbSize05.MaxLength = 20
        Me.tbSize05.Name = "tbSize05"
        Me.tbSize05.ReadOnly = True
        Me.tbSize05.Size = New System.Drawing.Size(43, 22)
        Me.tbSize05.TabIndex = 421
        Me.tbSize05.TabStop = False
        Me.tbSize05.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize06
        '
        Me.tbSize06.BackColor = System.Drawing.Color.White
        Me.tbSize06.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize06.ForeColor = System.Drawing.Color.Brown
        Me.tbSize06.Location = New System.Drawing.Point(314, 17)
        Me.tbSize06.MaxLength = 20
        Me.tbSize06.Name = "tbSize06"
        Me.tbSize06.ReadOnly = True
        Me.tbSize06.Size = New System.Drawing.Size(43, 22)
        Me.tbSize06.TabIndex = 422
        Me.tbSize06.TabStop = False
        Me.tbSize06.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize15
        '
        Me.tbSize15.BackColor = System.Drawing.Color.White
        Me.tbSize15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize15.ForeColor = System.Drawing.Color.Brown
        Me.tbSize15.Location = New System.Drawing.Point(692, 17)
        Me.tbSize15.MaxLength = 20
        Me.tbSize15.Name = "tbSize15"
        Me.tbSize15.ReadOnly = True
        Me.tbSize15.Size = New System.Drawing.Size(43, 22)
        Me.tbSize15.TabIndex = 431
        Me.tbSize15.TabStop = False
        Me.tbSize15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize07
        '
        Me.tbSize07.BackColor = System.Drawing.Color.White
        Me.tbSize07.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize07.ForeColor = System.Drawing.Color.Brown
        Me.tbSize07.Location = New System.Drawing.Point(356, 17)
        Me.tbSize07.MaxLength = 20
        Me.tbSize07.Name = "tbSize07"
        Me.tbSize07.ReadOnly = True
        Me.tbSize07.Size = New System.Drawing.Size(43, 22)
        Me.tbSize07.TabIndex = 423
        Me.tbSize07.TabStop = False
        Me.tbSize07.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize14
        '
        Me.tbSize14.BackColor = System.Drawing.Color.White
        Me.tbSize14.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize14.ForeColor = System.Drawing.Color.Brown
        Me.tbSize14.Location = New System.Drawing.Point(650, 17)
        Me.tbSize14.MaxLength = 20
        Me.tbSize14.Name = "tbSize14"
        Me.tbSize14.ReadOnly = True
        Me.tbSize14.Size = New System.Drawing.Size(43, 22)
        Me.tbSize14.TabIndex = 430
        Me.tbSize14.TabStop = False
        Me.tbSize14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize08
        '
        Me.tbSize08.BackColor = System.Drawing.Color.White
        Me.tbSize08.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize08.ForeColor = System.Drawing.Color.Brown
        Me.tbSize08.Location = New System.Drawing.Point(398, 17)
        Me.tbSize08.MaxLength = 20
        Me.tbSize08.Name = "tbSize08"
        Me.tbSize08.ReadOnly = True
        Me.tbSize08.Size = New System.Drawing.Size(43, 22)
        Me.tbSize08.TabIndex = 424
        Me.tbSize08.TabStop = False
        Me.tbSize08.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize13
        '
        Me.tbSize13.BackColor = System.Drawing.Color.White
        Me.tbSize13.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize13.ForeColor = System.Drawing.Color.Brown
        Me.tbSize13.Location = New System.Drawing.Point(608, 17)
        Me.tbSize13.MaxLength = 20
        Me.tbSize13.Name = "tbSize13"
        Me.tbSize13.ReadOnly = True
        Me.tbSize13.Size = New System.Drawing.Size(43, 22)
        Me.tbSize13.TabIndex = 429
        Me.tbSize13.TabStop = False
        Me.tbSize13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize09
        '
        Me.tbSize09.BackColor = System.Drawing.Color.White
        Me.tbSize09.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize09.ForeColor = System.Drawing.Color.Brown
        Me.tbSize09.Location = New System.Drawing.Point(440, 17)
        Me.tbSize09.MaxLength = 20
        Me.tbSize09.Name = "tbSize09"
        Me.tbSize09.ReadOnly = True
        Me.tbSize09.Size = New System.Drawing.Size(43, 22)
        Me.tbSize09.TabIndex = 425
        Me.tbSize09.TabStop = False
        Me.tbSize09.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize12
        '
        Me.tbSize12.BackColor = System.Drawing.Color.White
        Me.tbSize12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize12.ForeColor = System.Drawing.Color.Brown
        Me.tbSize12.Location = New System.Drawing.Point(566, 17)
        Me.tbSize12.MaxLength = 20
        Me.tbSize12.Name = "tbSize12"
        Me.tbSize12.ReadOnly = True
        Me.tbSize12.Size = New System.Drawing.Size(43, 22)
        Me.tbSize12.TabIndex = 428
        Me.tbSize12.TabStop = False
        Me.tbSize12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize10
        '
        Me.tbSize10.BackColor = System.Drawing.Color.White
        Me.tbSize10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize10.ForeColor = System.Drawing.Color.Brown
        Me.tbSize10.Location = New System.Drawing.Point(482, 17)
        Me.tbSize10.MaxLength = 20
        Me.tbSize10.Name = "tbSize10"
        Me.tbSize10.ReadOnly = True
        Me.tbSize10.Size = New System.Drawing.Size(43, 22)
        Me.tbSize10.TabIndex = 426
        Me.tbSize10.TabStop = False
        Me.tbSize10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbSize11
        '
        Me.tbSize11.BackColor = System.Drawing.Color.White
        Me.tbSize11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSize11.ForeColor = System.Drawing.Color.Brown
        Me.tbSize11.Location = New System.Drawing.Point(524, 17)
        Me.tbSize11.MaxLength = 20
        Me.tbSize11.Name = "tbSize11"
        Me.tbSize11.ReadOnly = True
        Me.tbSize11.Size = New System.Drawing.Size(43, 22)
        Me.tbSize11.TabIndex = 427
        Me.tbSize11.TabStop = False
        Me.tbSize11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbPairsTotal
        '
        Me.tbPairsTotal.BackColor = System.Drawing.Color.White
        Me.tbPairsTotal.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairsTotal.ForeColor = System.Drawing.Color.Blue
        Me.tbPairsTotal.Location = New System.Drawing.Point(944, 38)
        Me.tbPairsTotal.MaxLength = 20
        Me.tbPairsTotal.Name = "tbPairsTotal"
        Me.tbPairsTotal.ReadOnly = True
        Me.tbPairsTotal.Size = New System.Drawing.Size(43, 22)
        Me.tbPairsTotal.TabIndex = 1058
        Me.tbPairsTotal.TabStop = False
        Me.tbPairsTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs17
        '
        Me.tbPairs17.BackColor = System.Drawing.Color.White
        Me.tbPairs17.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs17.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs17.Location = New System.Drawing.Point(776, 38)
        Me.tbPairs17.MaxLength = 20
        Me.tbPairs17.Name = "tbPairs17"
        Me.tbPairs17.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs17.TabIndex = 1054
        Me.tbPairs17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs18
        '
        Me.tbPairs18.BackColor = System.Drawing.Color.White
        Me.tbPairs18.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs18.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs18.Location = New System.Drawing.Point(818, 38)
        Me.tbPairs18.MaxLength = 20
        Me.tbPairs18.Name = "tbPairs18"
        Me.tbPairs18.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs18.TabIndex = 1055
        Me.tbPairs18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs19
        '
        Me.tbPairs19.BackColor = System.Drawing.Color.White
        Me.tbPairs19.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs19.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs19.Location = New System.Drawing.Point(860, 38)
        Me.tbPairs19.MaxLength = 20
        Me.tbPairs19.Name = "tbPairs19"
        Me.tbPairs19.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs19.TabIndex = 1056
        Me.tbPairs19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs20
        '
        Me.tbPairs20.BackColor = System.Drawing.Color.White
        Me.tbPairs20.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold)
        Me.tbPairs20.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs20.Location = New System.Drawing.Point(902, 38)
        Me.tbPairs20.MaxLength = 20
        Me.tbPairs20.Name = "tbPairs20"
        Me.tbPairs20.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs20.TabIndex = 1057
        Me.tbPairs20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs02
        '
        Me.tbPairs02.BackColor = System.Drawing.Color.White
        Me.tbPairs02.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs02.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs02.Location = New System.Drawing.Point(146, 38)
        Me.tbPairs02.MaxLength = 20
        Me.tbPairs02.Name = "tbPairs02"
        Me.tbPairs02.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs02.TabIndex = 1039
        Me.tbPairs02.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs01
        '
        Me.tbPairs01.BackColor = System.Drawing.Color.White
        Me.tbPairs01.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs01.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs01.Location = New System.Drawing.Point(104, 38)
        Me.tbPairs01.MaxLength = 20
        Me.tbPairs01.Name = "tbPairs01"
        Me.tbPairs01.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs01.TabIndex = 1038
        Me.tbPairs01.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs03
        '
        Me.tbPairs03.BackColor = System.Drawing.Color.White
        Me.tbPairs03.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs03.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs03.Location = New System.Drawing.Point(188, 38)
        Me.tbPairs03.MaxLength = 20
        Me.tbPairs03.Name = "tbPairs03"
        Me.tbPairs03.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs03.TabIndex = 1040
        Me.tbPairs03.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs04
        '
        Me.tbPairs04.BackColor = System.Drawing.Color.White
        Me.tbPairs04.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs04.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs04.Location = New System.Drawing.Point(230, 38)
        Me.tbPairs04.MaxLength = 20
        Me.tbPairs04.Name = "tbPairs04"
        Me.tbPairs04.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs04.TabIndex = 1041
        Me.tbPairs04.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs16
        '
        Me.tbPairs16.BackColor = System.Drawing.Color.White
        Me.tbPairs16.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs16.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs16.Location = New System.Drawing.Point(734, 38)
        Me.tbPairs16.MaxLength = 20
        Me.tbPairs16.Name = "tbPairs16"
        Me.tbPairs16.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs16.TabIndex = 1053
        Me.tbPairs16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs05
        '
        Me.tbPairs05.BackColor = System.Drawing.Color.White
        Me.tbPairs05.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs05.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs05.Location = New System.Drawing.Point(272, 38)
        Me.tbPairs05.MaxLength = 20
        Me.tbPairs05.Name = "tbPairs05"
        Me.tbPairs05.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs05.TabIndex = 1042
        Me.tbPairs05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs06
        '
        Me.tbPairs06.BackColor = System.Drawing.Color.White
        Me.tbPairs06.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs06.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs06.Location = New System.Drawing.Point(314, 38)
        Me.tbPairs06.MaxLength = 20
        Me.tbPairs06.Name = "tbPairs06"
        Me.tbPairs06.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs06.TabIndex = 1043
        Me.tbPairs06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs15
        '
        Me.tbPairs15.BackColor = System.Drawing.Color.White
        Me.tbPairs15.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs15.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs15.Location = New System.Drawing.Point(692, 38)
        Me.tbPairs15.MaxLength = 20
        Me.tbPairs15.Name = "tbPairs15"
        Me.tbPairs15.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs15.TabIndex = 1052
        Me.tbPairs15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs07
        '
        Me.tbPairs07.BackColor = System.Drawing.Color.White
        Me.tbPairs07.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs07.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs07.Location = New System.Drawing.Point(356, 38)
        Me.tbPairs07.MaxLength = 20
        Me.tbPairs07.Name = "tbPairs07"
        Me.tbPairs07.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs07.TabIndex = 1044
        Me.tbPairs07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs14
        '
        Me.tbPairs14.BackColor = System.Drawing.Color.White
        Me.tbPairs14.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs14.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs14.Location = New System.Drawing.Point(650, 38)
        Me.tbPairs14.MaxLength = 20
        Me.tbPairs14.Name = "tbPairs14"
        Me.tbPairs14.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs14.TabIndex = 1051
        Me.tbPairs14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs08
        '
        Me.tbPairs08.BackColor = System.Drawing.Color.White
        Me.tbPairs08.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs08.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs08.Location = New System.Drawing.Point(398, 38)
        Me.tbPairs08.MaxLength = 20
        Me.tbPairs08.Name = "tbPairs08"
        Me.tbPairs08.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs08.TabIndex = 1045
        Me.tbPairs08.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs13
        '
        Me.tbPairs13.BackColor = System.Drawing.Color.White
        Me.tbPairs13.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs13.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs13.Location = New System.Drawing.Point(608, 38)
        Me.tbPairs13.MaxLength = 20
        Me.tbPairs13.Name = "tbPairs13"
        Me.tbPairs13.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs13.TabIndex = 1050
        Me.tbPairs13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs09
        '
        Me.tbPairs09.BackColor = System.Drawing.Color.White
        Me.tbPairs09.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs09.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs09.Location = New System.Drawing.Point(440, 38)
        Me.tbPairs09.MaxLength = 20
        Me.tbPairs09.Name = "tbPairs09"
        Me.tbPairs09.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs09.TabIndex = 1046
        Me.tbPairs09.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs12
        '
        Me.tbPairs12.BackColor = System.Drawing.Color.White
        Me.tbPairs12.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs12.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs12.Location = New System.Drawing.Point(566, 38)
        Me.tbPairs12.MaxLength = 20
        Me.tbPairs12.Name = "tbPairs12"
        Me.tbPairs12.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs12.TabIndex = 1049
        Me.tbPairs12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs10
        '
        Me.tbPairs10.BackColor = System.Drawing.Color.White
        Me.tbPairs10.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs10.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs10.Location = New System.Drawing.Point(482, 38)
        Me.tbPairs10.MaxLength = 20
        Me.tbPairs10.Name = "tbPairs10"
        Me.tbPairs10.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs10.TabIndex = 1047
        Me.tbPairs10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPairs11
        '
        Me.tbPairs11.BackColor = System.Drawing.Color.White
        Me.tbPairs11.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPairs11.ForeColor = System.Drawing.Color.Blue
        Me.tbPairs11.Location = New System.Drawing.Point(524, 38)
        Me.tbPairs11.MaxLength = 20
        Me.tbPairs11.Name = "tbPairs11"
        Me.tbPairs11.Size = New System.Drawing.Size(43, 22)
        Me.tbPairs11.TabIndex = 1048
        Me.tbPairs11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(944, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(43, 22)
        Me.Label21.TabIndex = 395
        Me.Label21.Text = "Total"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label73
        '
        Me.Label73.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label73.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(5, 17)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(95, 22)
        Me.Label73.TabIndex = 393
        Me.Label73.Text = "Sizes :-"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label52
        '
        Me.Label52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label52.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(5, 38)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(95, 22)
        Me.Label52.TabIndex = 394
        Me.Label52.Text = "Ord. Pairs :"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdTempPlQty
        '
        Me.grdTempPlQty.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTempPlQty.Location = New System.Drawing.Point(6, 200)
        Me.grdTempPlQty.LookAndFeel.SkinName = "The Asphalt World"
        Me.grdTempPlQty.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdTempPlQty.MainView = Me.grdTempPlQtyV1
        Me.grdTempPlQty.Name = "grdTempPlQty"
        Me.grdTempPlQty.Size = New System.Drawing.Size(981, 307)
        Me.grdTempPlQty.TabIndex = 1159
        Me.grdTempPlQty.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdTempPlQtyV1, Me.CardView2})
        '
        'grdTempPlQtyV1
        '
        Me.grdTempPlQtyV1.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.DetailTip.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.DetailTip.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.Empty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.Empty.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.EvenRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.EvenRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.FilterCloseButton.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.FilterPanel.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.FixedLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.FixedLine.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.FocusedCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.FocusedCell.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.FocusedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.FocusedRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.FooterPanel.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.GroupButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.GroupButton.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.GroupFooter.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.GroupPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.GroupPanel.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.GroupRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.GroupRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.HorzLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.HorzLine.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.OddRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.OddRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.Preview.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.Row.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.RowSeparator.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.RowSeparator.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.SelectedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.SelectedRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.TopNewRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.TopNewRow.Options.UseFont = True
        Me.grdTempPlQtyV1.Appearance.VertLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTempPlQtyV1.Appearance.VertLine.Options.UseFont = True
        Me.grdTempPlQtyV1.GridControl = Me.grdTempPlQty
        Me.grdTempPlQtyV1.Name = "grdTempPlQtyV1"
        Me.grdTempPlQtyV1.OptionsView.ColumnAutoWidth = False
        Me.grdTempPlQtyV1.OptionsView.ShowAutoFilterRow = True
        Me.grdTempPlQtyV1.OptionsView.ShowFooter = True
        Me.grdTempPlQtyV1.OptionsView.ShowGroupPanel = False
        '
        'CardView2
        '
        Me.CardView2.FocusedCardTopFieldIndex = 0
        Me.CardView2.GridControl = Me.grdTempPlQty
        Me.CardView2.Name = "CardView2"
        '
        'grdOrderDetails
        '
        Me.grdOrderDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOrderDetails.Location = New System.Drawing.Point(6, 18)
        Me.grdOrderDetails.LookAndFeel.SkinName = "The Asphalt World"
        Me.grdOrderDetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdOrderDetails.MainView = Me.grdOrderDetailsV1
        Me.grdOrderDetails.Name = "grdOrderDetails"
        Me.grdOrderDetails.Size = New System.Drawing.Size(981, 488)
        Me.grdOrderDetails.TabIndex = 1063
        Me.grdOrderDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdOrderDetailsV1, Me.CardView1})
        '
        'grdOrderDetailsV1
        '
        Me.grdOrderDetailsV1.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.DetailTip.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.DetailTip.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.Empty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.Empty.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.EvenRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.EvenRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.FilterCloseButton.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.FilterPanel.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.FixedLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.FixedLine.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.FocusedCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.FocusedCell.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.FocusedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.FocusedRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.FooterPanel.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.GroupButton.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.GroupButton.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.GroupFooter.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.GroupPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.GroupPanel.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.GroupRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.GroupRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.HorzLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.HorzLine.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.OddRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.OddRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.Preview.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.Row.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.RowSeparator.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.RowSeparator.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.SelectedRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.SelectedRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.TopNewRow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.TopNewRow.Options.UseFont = True
        Me.grdOrderDetailsV1.Appearance.VertLine.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOrderDetailsV1.Appearance.VertLine.Options.UseFont = True
        Me.grdOrderDetailsV1.GridControl = Me.grdOrderDetails
        Me.grdOrderDetailsV1.Name = "grdOrderDetailsV1"
        Me.grdOrderDetailsV1.OptionsView.ColumnAutoWidth = False
        Me.grdOrderDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdOrderDetailsV1.OptionsView.ShowFooter = True
        Me.grdOrderDetailsV1.OptionsView.ShowGroupPanel = False
        '
        'CardView1
        '
        Me.CardView1.FocusedCardTopFieldIndex = 0
        Me.CardView1.GridControl = Me.grdOrderDetails
        Me.CardView1.Name = "CardView1"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(397, 25)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(67, 22)
        Me.Label19.TabIndex = 849
        Me.Label19.Text = "Order Wk:-"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'plDtls
        '
        Me.plDtls.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plDtls.BackColor = System.Drawing.Color.Transparent
        Me.plDtls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.plDtls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plDtls.Controls.Add(Me.tbWeekNo)
        Me.plDtls.Controls.Add(Me.Label19)
        Me.plDtls.Controls.Add(Me.cbCancel)
        Me.plDtls.Controls.Add(Me.cbSave)
        Me.plDtls.Controls.Add(Me.Label9)
        Me.plDtls.Controls.Add(Me.cbxSeason)
        Me.plDtls.Controls.Add(Me.Label13)
        Me.plDtls.Controls.Add(Me.tbOrderNo)
        Me.plDtls.Controls.Add(Me.Label14)
        Me.plDtls.Controls.Add(Me.plTypeofPacking)
        Me.plDtls.Controls.Add(Me.dpOrderDate)
        Me.plDtls.Controls.Add(Me.plHeaderInfo)
        Me.plDtls.Controls.Add(Me.GroupBox1)
        Me.plDtls.Location = New System.Drawing.Point(0, 25)
        Me.plDtls.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.plDtls.Name = "plDtls"
        Me.plDtls.Size = New System.Drawing.Size(1006, 610)
        Me.plDtls.TabIndex = 0
        Me.plDtls.Visible = False
        '
        'frmPackingListandlabels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Honeydew
        Me.CancelButton = Me.cbExit
        Me.ClientSize = New System.Drawing.Size(1006, 661)
        Me.ControlBox = False
        Me.Controls.Add(Me.plFooter)
        Me.Controls.Add(Me.plInfo)
        Me.Controls.Add(Me.plMain)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.plDtls)
        Me.Font = New System.Drawing.Font("Franklin Gothic Book", 9.75!)
        Me.ForeColor = System.Drawing.Color.Indigo
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmPackingListandlabels"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Report - Packing List Generations"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.plInfo.ResumeLayout(False)
        Me.plInfo.PerformLayout()
        Me.plFooter.ResumeLayout(False)
        Me.plMain.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrderHeaderV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plTypeofPacking.ResumeLayout(False)
        Me.plTypeofPacking.PerformLayout()
        Me.plHeaderInfo.ResumeLayout(False)
        Me.plHeaderInfo.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.grdTempPlQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTempPlQtyV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrderDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plDtls.ResumeLayout(False)
        Me.plDtls.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbExport2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents cbDelete As System.Windows.Forms.Button
    Friend WithEvents cbUpdate As System.Windows.Forms.Button
    Friend WithEvents cbGenerate As System.Windows.Forms.Button
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbRefresh As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents plInfo As System.Windows.Forms.Panel
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents lblUserDesignation As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents plFooter As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblInsert As System.Windows.Forms.Label
    Friend WithEvents lblCapsLock As System.Windows.Forms.Label
    Friend WithEvents lblNumLock As System.Windows.Forms.Label
    Friend WithEvents lblTimeDifference As System.Windows.Forms.Label
    Friend WithEvents lblUnitType As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents plMain As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents plDtls As System.Windows.Forms.Panel
    Friend WithEvents tbWeekNo As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbSize17 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize18 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize19 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize20 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize02 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize01 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize03 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize04 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize16 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize05 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize06 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize15 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize07 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize14 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize08 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize13 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize09 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize12 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize10 As System.Windows.Forms.TextBox
    Friend WithEvents tbSize11 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairsTotal As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs17 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs18 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs19 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs20 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs02 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs01 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs03 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs04 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs16 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs05 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs06 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs15 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs07 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs14 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs08 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs13 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs09 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs12 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs10 As System.Windows.Forms.TextBox
    Friend WithEvents tbPairs11 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents cbCancel As System.Windows.Forms.Button
    Friend WithEvents cbSave As System.Windows.Forms.Button
    Friend WithEvents plHeaderInfo As System.Windows.Forms.GroupBox
    Friend WithEvents tbDiscountValue As System.Windows.Forms.TextBox
    Friend WithEvents tbDiscountPercentage As System.Windows.Forms.TextBox
    Friend WithEvents tbPayMode As System.Windows.Forms.TextBox
    Friend WithEvents tbPriceTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dpCustomerRefDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbCustomerRefNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents tbCustomer As System.Windows.Forms.TextBox
    Friend WithEvents tbSizeInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbxSeason As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tbOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents plTypeofPacking As System.Windows.Forms.GroupBox
    Friend WithEvents rbAssortmentPacking As System.Windows.Forms.RadioButton
    Friend WithEvents rbNormalPacking As System.Windows.Forms.RadioButton
    Friend WithEvents dpOrderDate As System.Windows.Forms.DateTimePicker
    'Friend WithEvents DsSuppliers As OptimizerERP_for_KHLI.DsSuppliers
    'Friend WithEvents CRMSCMTraderBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents CRMSCMTraderTableAdapter As OptimizerERP_for_KHLI.DsSuppliersTableAdapters.CRMSCMTraderTableAdapter
    'Friend WithEvents PPCSizesBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents DSSizesMaster As OptimizerERP_for_KHLI.DSSizesMaster
    'Friend WithEvents PPCSizesTableAdapter As OptimizerERP_for_KHLI.DSSizesMasterTableAdapters.PPCSizesTableAdapter
    Friend WithEvents cbxCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents tbTotalQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbRemove As System.Windows.Forms.Button
    Friend WithEvents cbInclude As System.Windows.Forms.Button
    Friend WithEvents PPCArticleMasterBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents DsCustomerArticle As OptimizerERP_for_KHLI.DsCustomerArticle
    'Friend WithEvents PPCArticleMasterTableAdapter As OptimizerERP_for_KHLI.DsCustomerArticleTableAdapters.PPCArticleMasterTableAdapter
    Friend WithEvents grdOrderDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdOrderDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CardView1 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents dpDisplayDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpDisplayDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents PPCSizeAssortmentBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents DsSizeAssortment As OptimizerERP_for_KHLI.DsSizeAssortment
    'Friend WithEvents PPCSizeAssortmentTableAdapter As OptimizerERP_for_KHLI.DsSizeAssortmentTableAdapters.PPCSizeAssortmentTableAdapter
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tbPPLWeek As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents tbProductionPackingListNo As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents dpPPLDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents rbMultipleSizePacking As System.Windows.Forms.RadioButton
    Friend WithEvents rbSingleSizePacking As System.Windows.Forms.RadioButton
    Friend WithEvents tbBoxQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents rbManualPacking As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents rbPairLabels As System.Windows.Forms.RadioButton
    Friend WithEvents rbProductionPackingList As System.Windows.Forms.RadioButton
    Friend WithEvents rbBOXLabels As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbBalQtyTotal As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty17 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty18 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty19 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty20 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty02 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty01 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty03 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty04 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty16 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty05 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty06 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty15 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty07 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty14 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty08 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty13 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty09 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty12 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty10 As System.Windows.Forms.TextBox
    Friend WithEvents tbBalQty11 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQtyTotal As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty17 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty18 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty19 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty20 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty02 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty01 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty03 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty04 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty16 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty05 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty06 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty15 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty07 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty14 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty08 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty13 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty09 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty12 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty10 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLQty11 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQtyTotal As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty17 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty18 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty19 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty20 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty02 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty01 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty03 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty04 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty16 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty05 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty06 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty15 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty07 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty14 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty08 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty13 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty09 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty12 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty10 As System.Windows.Forms.TextBox
    Friend WithEvents tbPLBalQty11 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQtyTotal As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty17 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty18 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty19 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty20 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty02 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty01 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty03 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty04 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty16 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty05 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty06 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty15 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty07 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty14 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty08 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty13 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty09 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty12 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty10 As System.Windows.Forms.TextBox
    Friend WithEvents tbPlGeneratedQty11 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rbSingleBox As System.Windows.Forms.RadioButton
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents tbManualBoxQty As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents tbTotalBoxes As System.Windows.Forms.TextBox
    Friend WithEvents rbMultipleBox As System.Windows.Forms.RadioButton
    Friend WithEvents grdTempPlQty As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdTempPlQtyV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CardView2 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grdOrderHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdOrderHeaderV1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
