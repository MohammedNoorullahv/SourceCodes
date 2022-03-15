<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaraCProductionEntries
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition3 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSaraCProductionEntries))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.grdConveyorProduction = New DevExpress.XtraGrid.GridControl
        Me.grdConveyorProductionV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdJobCardProduction = New DevExpress.XtraGrid.GridControl
        Me.grdJobCardProductionV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdBarcodeStatus = New DevExpress.XtraGrid.GridControl
        Me.grdBarCodeStatusV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.tbStatus = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.tbFaultDescription = New System.Windows.Forms.TextBox
        Me.cbClearBuffer = New System.Windows.Forms.Button
        Me.tbWrongQty = New System.Windows.Forms.TextBox
        Me.tbCorrectQty = New System.Windows.Forms.TextBox
        Me.tbTotalScanned = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbMachineNo = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.tbStage = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.tbShift = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbLastScannedBarcode = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.tbBarcode = New System.Windows.Forms.TextBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.Panel1.SuspendLayout()
        Me.pl.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.grdConveyorProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdConveyorProductionV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdJobCardProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdJobCardProductionV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBarcodeStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBarCodeStatusV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel2.SuspendLayout()
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
        Me.Panel1.Controls.Add(Me.Label12)
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
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(402, 681)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(307, 16)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "V.6 29-Nov-2019 - Upper Per Pair Production"
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
        Me.pl.Controls.Add(Me.Panel10)
        Me.pl.Controls.Add(Me.Panel9)
        Me.pl.Controls.Add(Me.Panel8)
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 615)
        Me.pl.TabIndex = 0
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Lavender
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.grdConveyorProduction)
        Me.Panel10.Controls.Add(Me.grdJobCardProduction)
        Me.Panel10.Controls.Add(Me.grdBarcodeStatus)
        Me.Panel10.Location = New System.Drawing.Point(7, 279)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1184, 333)
        Me.Panel10.TabIndex = 18
        '
        'grdConveyorProduction
        '
        Me.grdConveyorProduction.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdConveyorProduction.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdConveyorProduction.Location = New System.Drawing.Point(7, 4)
        Me.grdConveyorProduction.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdConveyorProduction.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdConveyorProduction.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdConveyorProduction.MainView = Me.grdConveyorProductionV1
        Me.grdConveyorProduction.Margin = New System.Windows.Forms.Padding(4)
        Me.grdConveyorProduction.Name = "grdConveyorProduction"
        Me.grdConveyorProduction.Size = New System.Drawing.Size(920, 323)
        Me.grdConveyorProduction.TabIndex = 117
        Me.grdConveyorProduction.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdConveyorProductionV1})
        '
        'grdConveyorProductionV1
        '
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.Empty.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.EvenRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.EvenRow.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FixedLine.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupButton.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.GroupRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.GroupRow.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.HideSelectionRow.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.HorzLine.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.OddRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.OddRow.Options.UseBorderColor = True
        Me.grdConveyorProductionV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.grdConveyorProductionV1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.Preview.Options.UseFont = True
        Me.grdConveyorProductionV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdConveyorProductionV1.Appearance.Row.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.Row.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grdConveyorProductionV1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.grdConveyorProductionV1.Appearance.TopNewRow.Options.UseBackColor = True
        Me.grdConveyorProductionV1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdConveyorProductionV1.Appearance.VertLine.Options.UseBackColor = True
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition1.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdConveyorProductionV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdConveyorProductionV1.GridControl = Me.grdConveyorProduction
        Me.grdConveyorProductionV1.Name = "grdConveyorProductionV1"
        Me.grdConveyorProductionV1.OptionsView.EnableAppearanceEvenRow = True
        Me.grdConveyorProductionV1.OptionsView.EnableAppearanceOddRow = True
        Me.grdConveyorProductionV1.OptionsView.ShowAutoFilterRow = True
        Me.grdConveyorProductionV1.OptionsView.ShowFooter = True
        Me.grdConveyorProductionV1.OptionsView.ShowGroupPanel = False
        '
        'grdJobCardProduction
        '
        Me.grdJobCardProduction.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdJobCardProduction.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdJobCardProduction.Location = New System.Drawing.Point(684, 2)
        Me.grdJobCardProduction.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdJobCardProduction.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdJobCardProduction.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdJobCardProduction.MainView = Me.grdJobCardProductionV1
        Me.grdJobCardProduction.Margin = New System.Windows.Forms.Padding(4)
        Me.grdJobCardProduction.Name = "grdJobCardProduction"
        Me.grdJobCardProduction.Size = New System.Drawing.Size(243, 323)
        Me.grdJobCardProduction.TabIndex = 118
        Me.grdJobCardProduction.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdJobCardProductionV1})
        Me.grdJobCardProduction.Visible = False
        '
        'grdJobCardProductionV1
        '
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.Empty.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.EvenRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.EvenRow.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FixedLine.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupButton.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.GroupRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.GroupRow.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.HideSelectionRow.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.HorzLine.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.OddRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.OddRow.Options.UseBorderColor = True
        Me.grdJobCardProductionV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.grdJobCardProductionV1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.Preview.Options.UseFont = True
        Me.grdJobCardProductionV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdJobCardProductionV1.Appearance.Row.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.Row.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grdJobCardProductionV1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.grdJobCardProductionV1.Appearance.TopNewRow.Options.UseBackColor = True
        Me.grdJobCardProductionV1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdJobCardProductionV1.Appearance.VertLine.Options.UseBackColor = True
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.ApplyToRow = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdJobCardProductionV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdJobCardProductionV1.GridControl = Me.grdJobCardProduction
        Me.grdJobCardProductionV1.Name = "grdJobCardProductionV1"
        Me.grdJobCardProductionV1.OptionsView.EnableAppearanceEvenRow = True
        Me.grdJobCardProductionV1.OptionsView.EnableAppearanceOddRow = True
        Me.grdJobCardProductionV1.OptionsView.ShowAutoFilterRow = True
        Me.grdJobCardProductionV1.OptionsView.ShowFooter = True
        Me.grdJobCardProductionV1.OptionsView.ShowGroupPanel = False
        '
        'grdBarcodeStatus
        '
        Me.grdBarcodeStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBarcodeStatus.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdBarcodeStatus.Location = New System.Drawing.Point(935, 2)
        Me.grdBarcodeStatus.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdBarcodeStatus.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdBarcodeStatus.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdBarcodeStatus.MainView = Me.grdBarCodeStatusV1
        Me.grdBarcodeStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.grdBarcodeStatus.Name = "grdBarcodeStatus"
        Me.grdBarcodeStatus.Size = New System.Drawing.Size(243, 323)
        Me.grdBarcodeStatus.TabIndex = 119
        Me.grdBarcodeStatus.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdBarCodeStatusV1})
        '
        'grdBarCodeStatusV1
        '
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.Empty.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.EvenRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.EvenRow.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(195, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FixedLine.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupButton.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.GroupRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupRow.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.HideSelectionRow.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.HorzLine.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.OddRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.OddRow.Options.UseBorderColor = True
        Me.grdBarCodeStatusV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.grdBarCodeStatusV1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.Preview.Options.UseFont = True
        Me.grdBarCodeStatusV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdBarCodeStatusV1.Appearance.Row.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.Row.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grdBarCodeStatusV1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.grdBarCodeStatusV1.Appearance.TopNewRow.Options.UseBackColor = True
        Me.grdBarCodeStatusV1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.grdBarCodeStatusV1.Appearance.VertLine.Options.UseBackColor = True
        StyleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition3.Appearance.Options.UseForeColor = True
        StyleFormatCondition3.ApplyToRow = True
        StyleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition3.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdBarCodeStatusV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition3})
        Me.grdBarCodeStatusV1.GridControl = Me.grdBarcodeStatus
        Me.grdBarCodeStatusV1.Name = "grdBarCodeStatusV1"
        Me.grdBarCodeStatusV1.OptionsView.EnableAppearanceEvenRow = True
        Me.grdBarCodeStatusV1.OptionsView.EnableAppearanceOddRow = True
        Me.grdBarCodeStatusV1.OptionsView.ShowAutoFilterRow = True
        Me.grdBarCodeStatusV1.OptionsView.ShowFooter = True
        Me.grdBarCodeStatusV1.OptionsView.ShowGroupPanel = False
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.tbStatus)
        Me.Panel9.Location = New System.Drawing.Point(7, 153)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1184, 122)
        Me.Panel9.TabIndex = 17
        '
        'tbStatus
        '
        Me.tbStatus.BackColor = System.Drawing.Color.White
        Me.tbStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tbStatus.Font = New System.Drawing.Font("Cambria", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.tbStatus.Location = New System.Drawing.Point(7, 5)
        Me.tbStatus.Name = "tbStatus"
        Me.tbStatus.Size = New System.Drawing.Size(1162, 105)
        Me.tbStatus.TabIndex = 60
        Me.tbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.tbFaultDescription)
        Me.Panel8.Controls.Add(Me.cbClearBuffer)
        Me.Panel8.Controls.Add(Me.tbWrongQty)
        Me.Panel8.Controls.Add(Me.tbCorrectQty)
        Me.Panel8.Controls.Add(Me.tbTotalScanned)
        Me.Panel8.Controls.Add(Me.Label5)
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Controls.Add(Me.Label3)
        Me.Panel8.Controls.Add(Me.tbMachineNo)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.tbStage)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.tbShift)
        Me.Panel8.Controls.Add(Me.Label34)
        Me.Panel8.Location = New System.Drawing.Point(7, 62)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1184, 85)
        Me.Panel8.TabIndex = 16
        '
        'tbFaultDescription
        '
        Me.tbFaultDescription.BackColor = System.Drawing.Color.White
        Me.tbFaultDescription.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFaultDescription.Location = New System.Drawing.Point(1040, 5)
        Me.tbFaultDescription.Multiline = True
        Me.tbFaultDescription.Name = "tbFaultDescription"
        Me.tbFaultDescription.ReadOnly = True
        Me.tbFaultDescription.Size = New System.Drawing.Size(137, 66)
        Me.tbFaultDescription.TabIndex = 48
        Me.tbFaultDescription.TabStop = False
        Me.tbFaultDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbClearBuffer
        '
        Me.cbClearBuffer.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbClearBuffer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbClearBuffer.Location = New System.Drawing.Point(974, 5)
        Me.cbClearBuffer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbClearBuffer.Name = "cbClearBuffer"
        Me.cbClearBuffer.Size = New System.Drawing.Size(59, 66)
        Me.cbClearBuffer.TabIndex = 47
        Me.cbClearBuffer.Text = "Clear Buffer"
        Me.cbClearBuffer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbClearBuffer.UseVisualStyleBackColor = True
        '
        'tbWrongQty
        '
        Me.tbWrongQty.BackColor = System.Drawing.Color.White
        Me.tbWrongQty.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbWrongQty.ForeColor = System.Drawing.Color.Red
        Me.tbWrongQty.Location = New System.Drawing.Point(848, 39)
        Me.tbWrongQty.Name = "tbWrongQty"
        Me.tbWrongQty.ReadOnly = True
        Me.tbWrongQty.Size = New System.Drawing.Size(119, 32)
        Me.tbWrongQty.TabIndex = 46
        Me.tbWrongQty.TabStop = False
        Me.tbWrongQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbCorrectQty
        '
        Me.tbCorrectQty.BackColor = System.Drawing.Color.White
        Me.tbCorrectQty.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCorrectQty.ForeColor = System.Drawing.Color.Green
        Me.tbCorrectQty.Location = New System.Drawing.Point(725, 39)
        Me.tbCorrectQty.Name = "tbCorrectQty"
        Me.tbCorrectQty.ReadOnly = True
        Me.tbCorrectQty.Size = New System.Drawing.Size(119, 32)
        Me.tbCorrectQty.TabIndex = 45
        Me.tbCorrectQty.TabStop = False
        Me.tbCorrectQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbTotalScanned
        '
        Me.tbTotalScanned.BackColor = System.Drawing.Color.White
        Me.tbTotalScanned.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalScanned.Location = New System.Drawing.Point(583, 39)
        Me.tbTotalScanned.Name = "tbTotalScanned"
        Me.tbTotalScanned.ReadOnly = True
        Me.tbTotalScanned.Size = New System.Drawing.Size(138, 32)
        Me.tbTotalScanned.TabIndex = 44
        Me.tbTotalScanned.TabStop = False
        Me.tbTotalScanned.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(848, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 30)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Wrong Qty :-"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(725, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 30)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Correct Qty :-"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(583, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 30)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Total Scanned :-"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbMachineNo
        '
        Me.tbMachineNo.BackColor = System.Drawing.Color.White
        Me.tbMachineNo.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMachineNo.Location = New System.Drawing.Point(5, 39)
        Me.tbMachineNo.Name = "tbMachineNo"
        Me.tbMachineNo.ReadOnly = True
        Me.tbMachineNo.Size = New System.Drawing.Size(285, 32)
        Me.tbMachineNo.TabIndex = 40
        Me.tbMachineNo.TabStop = False
        Me.tbMachineNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(5, 5)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(285, 30)
        Me.Label22.TabIndex = 39
        Me.Label22.Text = "CONVEYOUR NO. :-"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbStage
        '
        Me.tbStage.BackColor = System.Drawing.Color.White
        Me.tbStage.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbStage.Location = New System.Drawing.Point(294, 39)
        Me.tbStage.Name = "tbStage"
        Me.tbStage.ReadOnly = True
        Me.tbStage.Size = New System.Drawing.Size(285, 32)
        Me.tbStage.TabIndex = 38
        Me.tbStage.TabStop = False
        Me.tbStage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(294, 5)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(285, 30)
        Me.Label20.TabIndex = 37
        Me.Label20.Text = "STAGE :-"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbShift
        '
        Me.tbShift.BackColor = System.Drawing.Color.White
        Me.tbShift.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbShift.Location = New System.Drawing.Point(5, 39)
        Me.tbShift.Name = "tbShift"
        Me.tbShift.ReadOnly = True
        Me.tbShift.Size = New System.Drawing.Size(285, 32)
        Me.tbShift.TabIndex = 36
        Me.tbShift.TabStop = False
        Me.tbShift.Text = "GSKHLI"
        Me.tbShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbShift.Visible = False
        '
        'Label34
        '
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(5, 5)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(285, 30)
        Me.Label34.TabIndex = 35
        Me.Label34.Text = "SHIFT :-"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label34.Visible = False
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
        Me.Panel2.Size = New System.Drawing.Size(1184, 49)
        Me.Panel2.TabIndex = 12
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Honeydew
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.tbLastScannedBarcode)
        Me.Panel5.Location = New System.Drawing.Point(716, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(422, 40)
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
        Me.tbLastScannedBarcode.Size = New System.Drawing.Size(230, 23)
        Me.tbLastScannedBarcode.TabIndex = 35
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Honeydew
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.tbBarcode)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(332, 40)
        Me.Panel4.TabIndex = 14
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
        'tbBarcode
        '
        Me.tbBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbBarcode.Location = New System.Drawing.Point(89, 8)
        Me.tbBarcode.Name = "tbBarcode"
        Me.tbBarcode.Size = New System.Drawing.Size(230, 23)
        Me.tbBarcode.TabIndex = 24
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
        Me.Export2Excel.Visible = False
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
        'frmSaraCProductionEntries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSaraCProductionEntries"
        Me.Text = "frmSaraCProductionEntries"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pl.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        CType(Me.grdConveyorProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdConveyorProductionV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdJobCardProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdJobCardProductionV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBarcodeStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBarCodeStatusV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel2.ResumeLayout(False)
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
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents tbMachineNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tbStage As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents tbShift As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents tbStatus As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbLastScannedBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents grdJobCardProduction As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdJobCardProductionV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdConveyorProduction As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdConveyorProductionV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdBarcodeStatus As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdBarCodeStatusV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbWrongQty As System.Windows.Forms.TextBox
    Friend WithEvents tbCorrectQty As System.Windows.Forms.TextBox
    Friend WithEvents tbTotalScanned As System.Windows.Forms.TextBox
    Friend WithEvents tbFaultDescription As System.Windows.Forms.TextBox
    Friend WithEvents cbClearBuffer As System.Windows.Forms.Button
End Class
