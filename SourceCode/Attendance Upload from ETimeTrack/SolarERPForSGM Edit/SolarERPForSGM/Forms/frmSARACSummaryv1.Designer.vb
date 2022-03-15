<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSARACSummaryv1
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
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.grdConveyorProduction = New DevExpress.XtraGrid.GridControl
        Me.grdConveyorProductionV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdWeeklyPlanV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel10.SuspendLayout()
        CType(Me.grdConveyorProduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdConveyorProductionV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWeeklyPlanV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Lavender
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Controls.Add(Me.grdConveyorProduction)
        Me.Panel10.Location = New System.Drawing.Point(12, 67)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(987, 609)
        Me.Panel10.TabIndex = 19
        '
        'grdConveyorProduction
        '
        Me.grdConveyorProduction.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.grdConveyorProduction.Location = New System.Drawing.Point(6, 4)
        Me.grdConveyorProduction.LookAndFeel.SkinName = "Office 2010 Blue"
        Me.grdConveyorProduction.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.grdConveyorProduction.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grdConveyorProduction.MainView = Me.grdConveyorProductionV1
        Me.grdConveyorProduction.Margin = New System.Windows.Forms.Padding(4)
        Me.grdConveyorProduction.Name = "grdConveyorProduction"
        Me.grdConveyorProduction.Size = New System.Drawing.Size(972, 599)
        Me.grdConveyorProduction.TabIndex = 118
        Me.grdConveyorProduction.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdConveyorProductionV1, Me.grdWeeklyPlanV1, Me.GridView1})
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
        'grdWeeklyPlanV1
        '
        Me.grdWeeklyPlanV1.GridControl = Me.grdConveyorProduction
        Me.grdWeeklyPlanV1.Name = "grdWeeklyPlanV1"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdConveyorProduction
        Me.GridView1.Name = "GridView1"
        '
        'frmSARACSummaryv1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1010, 742)
        Me.Controls.Add(Me.Panel10)
        Me.Name = "frmSARACSummaryv1"
        Me.Text = "frmSARACSummaryv1"
        Me.Panel10.ResumeLayout(False)
        CType(Me.grdConveyorProduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdConveyorProductionV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWeeklyPlanV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents grdConveyorProduction As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdConveyorProductionV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdWeeklyPlanV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
