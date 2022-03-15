<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExcessArrivalDtls
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
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExcessArrivalDtls))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdPendingExcessArrivals = New DevExpress.XtraGrid.GridControl
        Me.grdPendingExcessArrivalsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Label15 = New System.Windows.Forms.Label
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.cbExporttoExcel = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.grdPendingExcessArrivals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPendingExcessArrivalsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.grdPendingExcessArrivals)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.cbExporttoExcel)
        Me.Panel1.Location = New System.Drawing.Point(7, 12)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1235, 719)
        Me.Panel1.TabIndex = 0
        '
        'grdPendingExcessArrivals
        '
        Me.grdPendingExcessArrivals.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPendingExcessArrivals.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPendingExcessArrivals.Location = New System.Drawing.Point(6, 4)
        Me.grdPendingExcessArrivals.LookAndFeel.SkinName = "Office 2010 Black"
        Me.grdPendingExcessArrivals.MainView = Me.grdPendingExcessArrivalsV1
        Me.grdPendingExcessArrivals.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPendingExcessArrivals.Name = "grdPendingExcessArrivals"
        Me.grdPendingExcessArrivals.Size = New System.Drawing.Size(1225, 566)
        Me.grdPendingExcessArrivals.TabIndex = 113
        Me.grdPendingExcessArrivals.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdPendingExcessArrivalsV1})
        '
        'grdPendingExcessArrivalsV1
        '
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.CustomizationFormHint.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.DetailTip.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.DetailTip.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.DetailTip.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.DetailTip.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.Empty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.Empty.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.Empty.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.Empty.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.EvenRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.EvenRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.FilterCloseButton.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.FilterPanel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.FilterPanel.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.FixedLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.FixedLine.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.FixedLine.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedCell.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedCell.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.FooterPanel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.FooterPanel.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupButton.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.GroupButton.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupButton.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupFooter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.GroupFooter.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupPanel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.GroupPanel.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.GroupRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Purple
        Me.grdPendingExcessArrivalsV1.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.HorzLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.HorzLine.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.HorzLine.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.HorzLine.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.OddRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.OddRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.Preview.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.Preview.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.Row.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.Row.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.Row.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.RowSeparator.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.RowSeparator.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.RowSeparator.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.SelectedRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.SelectedRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.TopNewRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.TopNewRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.TopNewRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.TopNewRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.VertLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.VertLine.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.VertLine.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.VertLine.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.Appearance.ViewCaption.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.Appearance.ViewCaption.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.Appearance.ViewCaption.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.EvenRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.AppearancePrint.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.AppearancePrint.EvenRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.EvenRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.AppearancePrint.HeaderPanel.ForeColor = System.Drawing.Color.Purple
        Me.grdPendingExcessArrivalsV1.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.HeaderPanel.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.OddRow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.AppearancePrint.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.AppearancePrint.OddRow.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.OddRow.Options.UseForeColor = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.Row.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPendingExcessArrivalsV1.AppearancePrint.Row.ForeColor = System.Drawing.Color.Black
        Me.grdPendingExcessArrivalsV1.AppearancePrint.Row.Options.UseFont = True
        Me.grdPendingExcessArrivalsV1.AppearancePrint.Row.Options.UseForeColor = True
        StyleFormatCondition2.Appearance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseFont = True
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdPendingExcessArrivalsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdPendingExcessArrivalsV1.GridControl = Me.grdPendingExcessArrivals
        Me.grdPendingExcessArrivalsV1.Name = "grdPendingExcessArrivalsV1"
        Me.grdPendingExcessArrivalsV1.OptionsView.ColumnAutoWidth = False
        Me.grdPendingExcessArrivalsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdPendingExcessArrivalsV1.OptionsView.ShowFooter = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(537, 662)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(346, 16)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "01. 18-Oct-15 - Auto mail - Pending Excess Arrival"
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
        'Timer1
        '
        '
        'frmExcessArrivalDtls
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cbExit
        Me.ClientSize = New System.Drawing.Size(1242, 730)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmExcessArrivalDtls"
        Me.Text = "Auto Mail - Pending Excess Arrival"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdPendingExcessArrivals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPendingExcessArrivalsV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grdPendingExcessArrivals As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPendingExcessArrivalsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbExporttoExcel As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
