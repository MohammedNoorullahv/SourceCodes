<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArticleList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArticleList))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.grdArticleMaster = New DevExpress.XtraGrid.GridControl
        Me.grdArticleMasterV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.plSelectionCriteria = New System.Windows.Forms.GroupBox
        Me.cbxCodification = New System.Windows.Forms.ComboBox
        Me.cbxArticleName = New System.Windows.Forms.ComboBox
        Me.rbCodification = New System.Windows.Forms.RadioButton
        Me.rbArticleName = New System.Windows.Forms.RadioButton
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
        Me.plSelectionCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
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
        'grdArticleMaster
        '
        Me.grdArticleMaster.Location = New System.Drawing.Point(10, 117)
        Me.grdArticleMaster.MainView = Me.grdArticleMasterV1
        Me.grdArticleMaster.Name = "grdArticleMaster"
        Me.grdArticleMaster.Size = New System.Drawing.Size(957, 400)
        Me.grdArticleMaster.TabIndex = 3
        Me.grdArticleMaster.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdArticleMasterV1})
        '
        'grdArticleMasterV1
        '
        Me.grdArticleMasterV1.GridControl = Me.grdArticleMaster
        Me.grdArticleMasterV1.Name = "grdArticleMasterV1"
        Me.grdArticleMasterV1.OptionsView.ColumnAutoWidth = False
        Me.grdArticleMasterV1.OptionsView.ShowAutoFilterRow = True
        Me.grdArticleMasterV1.OptionsView.ShowFooter = True
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
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cbxCustomer)
        Me.Panel2.Controls.Add(Me.plSelectionCriteria)
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
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(495, 17)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(325, 21)
        Me.cbxCustomer.TabIndex = 7
        '
        'plSelectionCriteria
        '
        Me.plSelectionCriteria.Controls.Add(Me.cbxCodification)
        Me.plSelectionCriteria.Controls.Add(Me.cbxArticleName)
        Me.plSelectionCriteria.Controls.Add(Me.rbCodification)
        Me.plSelectionCriteria.Controls.Add(Me.rbArticleName)
        Me.plSelectionCriteria.Enabled = False
        Me.plSelectionCriteria.Location = New System.Drawing.Point(165, 40)
        Me.plSelectionCriteria.Name = "plSelectionCriteria"
        Me.plSelectionCriteria.Size = New System.Drawing.Size(655, 61)
        Me.plSelectionCriteria.TabIndex = 6
        Me.plSelectionCriteria.TabStop = False
        Me.plSelectionCriteria.Text = "Selection Critirea :-"
        '
        'cbxCodification
        '
        Me.cbxCodification.Enabled = False
        Me.cbxCodification.FormattingEnabled = True
        Me.cbxCodification.Location = New System.Drawing.Point(486, 20)
        Me.cbxCodification.Name = "cbxCodification"
        Me.cbxCodification.Size = New System.Drawing.Size(123, 21)
        Me.cbxCodification.TabIndex = 9
        '
        'cbxArticleName
        '
        Me.cbxArticleName.FormattingEnabled = True
        Me.cbxArticleName.Location = New System.Drawing.Point(131, 20)
        Me.cbxArticleName.Name = "cbxArticleName"
        Me.cbxArticleName.Size = New System.Drawing.Size(239, 21)
        Me.cbxArticleName.TabIndex = 8
        '
        'rbCodification
        '
        Me.rbCodification.AutoSize = True
        Me.rbCodification.Location = New System.Drawing.Point(376, 19)
        Me.rbCodification.Name = "rbCodification"
        Me.rbCodification.Size = New System.Drawing.Size(104, 17)
        Me.rbCodification.TabIndex = 1
        Me.rbCodification.TabStop = True
        Me.rbCodification.Text = "By Codification :-"
        Me.rbCodification.UseVisualStyleBackColor = True
        '
        'rbArticleName
        '
        Me.rbArticleName.AutoSize = True
        Me.rbArticleName.Location = New System.Drawing.Point(16, 21)
        Me.rbArticleName.Name = "rbArticleName"
        Me.rbArticleName.Size = New System.Drawing.Size(109, 17)
        Me.rbArticleName.TabIndex = 0
        Me.rbArticleName.TabStop = True
        Me.rbArticleName.Text = "By Article Name :-"
        Me.rbArticleName.UseVisualStyleBackColor = True
        '
        'chkbxFilter
        '
        Me.chkbxFilter.AutoSize = True
        Me.chkbxFilter.Enabled = False
        Me.chkbxFilter.Location = New System.Drawing.Point(16, 62)
        Me.chkbxFilter.Name = "chkbxFilter"
        Me.chkbxFilter.Size = New System.Drawing.Size(143, 17)
        Me.chkbxFilter.TabIndex = 5
        Me.chkbxFilter.Text = "Filter Based on Selection"
        Me.chkbxFilter.UseVisualStyleBackColor = True
        '
        'chkbxSelectCustomer
        '
        Me.chkbxSelectCustomer.AutoSize = True
        Me.chkbxSelectCustomer.Location = New System.Drawing.Point(324, 17)
        Me.chkbxSelectCustomer.Name = "chkbxSelectCustomer"
        Me.chkbxSelectCustomer.Size = New System.Drawing.Size(164, 17)
        Me.chkbxSelectCustomer.TabIndex = 4
        Me.chkbxSelectCustomer.Text = "Articles of Selected Customer"
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
        'frmArticleList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 623)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmArticleList"
        Me.Text = "frmArticleList"
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.plSelectionCriteria.ResumeLayout(False)
        Me.plSelectionCriteria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkbxSelectCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents plSelectionCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents rbCodification As System.Windows.Forms.RadioButton
    Friend WithEvents rbArticleName As System.Windows.Forms.RadioButton
    Friend WithEvents chkbxFilter As System.Windows.Forms.CheckBox
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cbxCodification As System.Windows.Forms.ComboBox
    Friend WithEvents cbxArticleName As System.Windows.Forms.ComboBox
    Friend WithEvents grdArticleMaster As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdArticleMasterV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
End Class
