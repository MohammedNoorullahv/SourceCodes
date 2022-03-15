<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterials
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaterials))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.grdMaterials = New DevExpress.XtraGrid.GridControl
        Me.grdMaterialsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        CType(Me.grdMaterials, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMaterialsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.grdMaterials)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(967, 600)
        Me.Panel1.TabIndex = 0
        '
        'cbPrint
        '
        Me.cbPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPrint.Location = New System.Drawing.Point(303, 523)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(156, 74)
        Me.cbPrint.TabIndex = 6
        Me.cbPrint.Text = "Print"
        Me.cbPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPrint.UseVisualStyleBackColor = True
        Me.cbPrint.Visible = False
        '
        'Export2Excel
        '
        Me.Export2Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export2Excel.Image = CType(resources.GetObject("Export2Excel.Image"), System.Drawing.Image)
        Me.Export2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Export2Excel.Location = New System.Drawing.Point(144, 523)
        Me.Export2Excel.Name = "Export2Excel"
        Me.Export2Excel.Size = New System.Drawing.Size(156, 74)
        Me.Export2Excel.TabIndex = 4
        Me.Export2Excel.Text = "Expot to Excel"
        Me.Export2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export2Excel.UseVisualStyleBackColor = True
        '
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(834, 523)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(117, 74)
        Me.cbExit.TabIndex = 2
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbReferesh
        '
        Me.cbReferesh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReferesh.Image = CType(resources.GetObject("cbReferesh.Image"), System.Drawing.Image)
        Me.cbReferesh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbReferesh.Location = New System.Drawing.Point(10, 523)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'grdMaterials
        '
        Me.grdMaterials.Location = New System.Drawing.Point(10, 7)
        Me.grdMaterials.MainView = Me.grdMaterialsV1
        Me.grdMaterials.Name = "grdMaterials"
        Me.grdMaterials.Size = New System.Drawing.Size(957, 510)
        Me.grdMaterials.TabIndex = 3
        Me.grdMaterials.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdMaterialsV1})
        '
        'grdMaterialsV1
        '
        Me.grdMaterialsV1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.Empty.BackColor2 = System.Drawing.Color.White
        Me.grdMaterialsV1.Appearance.Empty.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdMaterialsV1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.EvenRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.EvenRow.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
        Me.grdMaterialsV1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FixedLine.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.grdMaterialsV1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupButton.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grdMaterialsV1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.GroupRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.GroupRow.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.GroupRow.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.grdMaterialsV1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gainsboro
        Me.grdMaterialsV1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.grdMaterialsV1.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.grdMaterialsV1.Appearance.HorzLine.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.OddRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.OddRow.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.OddRow.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdMaterialsV1.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
        Me.grdMaterialsV1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.grdMaterialsV1.Appearance.Preview.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.Preview.Options.UseFont = True
        Me.grdMaterialsV1.Appearance.Preview.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.Row.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.Row.Options.UseBorderColor = True
        Me.grdMaterialsV1.Appearance.Row.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grdMaterialsV1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
        Me.grdMaterialsV1.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.grdMaterialsV1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
        Me.grdMaterialsV1.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grdMaterialsV1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
        Me.grdMaterialsV1.Appearance.TopNewRow.Options.UseBackColor = True
        Me.grdMaterialsV1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.grdMaterialsV1.Appearance.VertLine.Options.UseBackColor = True
        Me.grdMaterialsV1.GridControl = Me.grdMaterials
        Me.grdMaterialsV1.Name = "grdMaterialsV1"
        Me.grdMaterialsV1.OptionsView.EnableAppearanceEvenRow = True
        Me.grdMaterialsV1.OptionsView.EnableAppearanceOddRow = True
        Me.grdMaterialsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdMaterialsV1.OptionsView.ShowFooter = True
        '
        'frmMaterials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 623)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMaterials"
        Me.Text = "frmMaterials"
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdMaterials, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMaterialsV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents grdMaterials As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdMaterialsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
End Class
