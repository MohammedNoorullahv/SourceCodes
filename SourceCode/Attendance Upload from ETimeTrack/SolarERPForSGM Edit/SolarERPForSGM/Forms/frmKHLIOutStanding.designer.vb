<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKHLIOutstanding
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKHLIOutstanding))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkbxwoRegeneration = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.grdArticleMaster = New DevExpress.XtraGrid.GridControl
        Me.grdArticleMasterV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cbxSalesOrder = New System.Windows.Forms.ComboBox
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.chkbxFilter = New System.Windows.Forms.CheckBox
        Me.chkbxSelectCustomer = New System.Windows.Forms.CheckBox
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.chkbxwoRegeneration)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.grdArticleMaster)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(967, 600)
        Me.Panel1.TabIndex = 0
        '
        'chkbxwoRegeneration
        '
        Me.chkbxwoRegeneration.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxwoRegeneration.AutoSize = True
        Me.chkbxwoRegeneration.Location = New System.Drawing.Point(505, 544)
        Me.chkbxwoRegeneration.Name = "chkbxwoRegeneration"
        Me.chkbxwoRegeneration.Size = New System.Drawing.Size(229, 17)
        Me.chkbxwoRegeneration.TabIndex = 6
        Me.chkbxwoRegeneration.Text = "Display Without Outstanding ReGeneration"
        Me.chkbxwoRegeneration.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(306, 523)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 74)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Upper Short Dispatch Report"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Export2Excel
        '
        Me.Export2Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
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
        'grdArticleMaster
        '
        Me.grdArticleMaster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdArticleMaster.Location = New System.Drawing.Point(10, 117)
        Me.grdArticleMaster.MainView = Me.grdArticleMasterV1
        Me.grdArticleMaster.Name = "grdArticleMaster"
        Me.grdArticleMaster.Size = New System.Drawing.Size(957, 400)
        Me.grdArticleMaster.TabIndex = 3
        Me.grdArticleMaster.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdArticleMasterV1})
        '
        'grdArticleMasterV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdArticleMasterV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdArticleMasterV1.GridControl = Me.grdArticleMaster
        Me.grdArticleMasterV1.Name = "grdArticleMasterV1"
        Me.grdArticleMasterV1.OptionsView.ColumnAutoWidth = False
        Me.grdArticleMasterV1.OptionsView.ShowAutoFilterRow = True
        Me.grdArticleMasterV1.OptionsView.ShowFooter = True
        Me.grdArticleMasterV1.OptionsView.ShowGroupPanel = False
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.cbReferesh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
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
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cbxSalesOrder)
        Me.Panel2.Controls.Add(Me.cbxCustomer)
        Me.Panel2.Controls.Add(Me.chkbxFilter)
        Me.Panel2.Controls.Add(Me.chkbxSelectCustomer)
        Me.Panel2.Controls.Add(Me.dpTo)
        Me.Panel2.Controls.Add(Me.dpFrom)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(10, 10)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(954, 105)
        Me.Panel2.TabIndex = 0
        '
        'cbxSalesOrder
        '
        Me.cbxSalesOrder.FormattingEnabled = True
        Me.cbxSalesOrder.Location = New System.Drawing.Point(495, 44)
        Me.cbxSalesOrder.Name = "cbxSalesOrder"
        Me.cbxSalesOrder.Size = New System.Drawing.Size(325, 21)
        Me.cbxSalesOrder.TabIndex = 8
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(495, 17)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(325, 21)
        Me.cbxCustomer.TabIndex = 7
        '
        'chkbxFilter
        '
        Me.chkbxFilter.AutoSize = True
        Me.chkbxFilter.Enabled = False
        Me.chkbxFilter.Location = New System.Drawing.Point(324, 44)
        Me.chkbxFilter.Name = "chkbxFilter"
        Me.chkbxFilter.Size = New System.Drawing.Size(142, 17)
        Me.chkbxFilter.TabIndex = 5
        Me.chkbxFilter.Text = "Status of Selected Order"
        Me.chkbxFilter.UseVisualStyleBackColor = True
        '
        'chkbxSelectCustomer
        '
        Me.chkbxSelectCustomer.AutoSize = True
        Me.chkbxSelectCustomer.Location = New System.Drawing.Point(324, 17)
        Me.chkbxSelectCustomer.Name = "chkbxSelectCustomer"
        Me.chkbxSelectCustomer.Size = New System.Drawing.Size(160, 17)
        Me.chkbxSelectCustomer.TabIndex = 4
        Me.chkbxSelectCustomer.Text = "Status of Selected Customer"
        Me.chkbxSelectCustomer.UseVisualStyleBackColor = True
        '
        'dpTo
        '
        Me.dpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpTo.Location = New System.Drawing.Point(213, 14)
        Me.dpTo.Name = "dpTo"
        Me.dpTo.Size = New System.Drawing.Size(105, 20)
        Me.dpTo.TabIndex = 3
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(67, 14)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(105, 20)
        Me.dpFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(178, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To :-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From :-"
        '
        'frmKHLIOutstanding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 623)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmKHLIOutstanding"
        Me.Text = "Upper & Full Shoe Outstanding"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkbxSelectCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxFilter As System.Windows.Forms.CheckBox
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cbxSalesOrder As System.Windows.Forms.ComboBox
    Friend WithEvents grdArticleMaster As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdArticleMasterV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkbxwoRegeneration As System.Windows.Forms.CheckBox
End Class
