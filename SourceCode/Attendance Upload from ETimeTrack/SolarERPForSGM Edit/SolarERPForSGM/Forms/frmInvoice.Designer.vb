<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoice))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.grdInvoices = New DevExpress.XtraGrid.GridControl
        Me.grdInvoicesV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.pl = New System.Windows.Forms.Panel
        Me.plTypeofInvoice = New System.Windows.Forms.GroupBox
        Me.chkbxFormH = New System.Windows.Forms.CheckBox
        Me.chkbxExport = New System.Windows.Forms.CheckBox
        Me.chkbxFormC = New System.Windows.Forms.CheckBox
        Me.chkbxCT3 = New System.Windows.Forms.CheckBox
        Me.chkbxAll = New System.Windows.Forms.CheckBox
        Me.plTypeofOrder = New System.Windows.Forms.GroupBox
        Me.chkbxJobWorkOrder = New System.Windows.Forms.CheckBox
        Me.chkbxSalesOrder = New System.Windows.Forms.CheckBox
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
        CType(Me.grdInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInvoicesV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.plTypeofInvoice.SuspendLayout()
        Me.plTypeofOrder.SuspendLayout()
        Me.plSelectionCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.grdInvoices)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(967, 696)
        Me.Panel1.TabIndex = 0
        '
        'Export2Excel
        '
        Me.Export2Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export2Excel.Image = CType(resources.GetObject("Export2Excel.Image"), System.Drawing.Image)
        Me.Export2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Export2Excel.Location = New System.Drawing.Point(144, 618)
        Me.Export2Excel.Name = "Export2Excel"
        Me.Export2Excel.Size = New System.Drawing.Size(156, 74)
        Me.Export2Excel.TabIndex = 4
        Me.Export2Excel.Text = "Expot to Excel"
        Me.Export2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export2Excel.UseVisualStyleBackColor = True
        '
        'grdInvoices
        '
        Me.grdInvoices.Location = New System.Drawing.Point(9, 212)
        Me.grdInvoices.MainView = Me.grdInvoicesV1
        Me.grdInvoices.Name = "grdInvoices"
        Me.grdInvoices.Size = New System.Drawing.Size(957, 400)
        Me.grdInvoices.TabIndex = 3
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
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(834, 618)
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
        Me.cbReferesh.Location = New System.Drawing.Point(10, 618)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'pl
        '
        Me.pl.Controls.Add(Me.plTypeofInvoice)
        Me.pl.Controls.Add(Me.plTypeofOrder)
        Me.pl.Controls.Add(Me.cbxCustomer)
        Me.pl.Controls.Add(Me.plSelectionCriteria)
        Me.pl.Controls.Add(Me.chkbxFilter)
        Me.pl.Controls.Add(Me.chkbxSelectCustomer)
        Me.pl.Controls.Add(Me.dpTo)
        Me.pl.Controls.Add(Me.dpFrom)
        Me.pl.Controls.Add(Me.Label2)
        Me.pl.Controls.Add(Me.Label1)
        Me.pl.Location = New System.Drawing.Point(10, 10)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(954, 189)
        Me.pl.TabIndex = 0
        '
        'plTypeofInvoice
        '
        Me.plTypeofInvoice.Controls.Add(Me.chkbxFormH)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxExport)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxFormC)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxCT3)
        Me.plTypeofInvoice.Controls.Add(Me.chkbxAll)
        Me.plTypeofInvoice.Location = New System.Drawing.Point(716, 75)
        Me.plTypeofInvoice.Name = "plTypeofInvoice"
        Me.plTypeofInvoice.Size = New System.Drawing.Size(225, 99)
        Me.plTypeofInvoice.TabIndex = 9
        Me.plTypeofInvoice.TabStop = False
        Me.plTypeofInvoice.Text = "Type Of Invoice"
        '
        'chkbxFormH
        '
        Me.chkbxFormH.AutoSize = True
        Me.chkbxFormH.Location = New System.Drawing.Point(18, 71)
        Me.chkbxFormH.Name = "chkbxFormH"
        Me.chkbxFormH.Size = New System.Drawing.Size(60, 17)
        Me.chkbxFormH.TabIndex = 4
        Me.chkbxFormH.Text = "Form H"
        Me.chkbxFormH.UseVisualStyleBackColor = True
        '
        'chkbxExport
        '
        Me.chkbxExport.AutoSize = True
        Me.chkbxExport.Location = New System.Drawing.Point(108, 48)
        Me.chkbxExport.Name = "chkbxExport"
        Me.chkbxExport.Size = New System.Drawing.Size(56, 17)
        Me.chkbxExport.TabIndex = 3
        Me.chkbxExport.Text = "Export"
        Me.chkbxExport.UseVisualStyleBackColor = True
        '
        'chkbxFormC
        '
        Me.chkbxFormC.AutoSize = True
        Me.chkbxFormC.Location = New System.Drawing.Point(108, 20)
        Me.chkbxFormC.Name = "chkbxFormC"
        Me.chkbxFormC.Size = New System.Drawing.Size(59, 17)
        Me.chkbxFormC.TabIndex = 2
        Me.chkbxFormC.Text = "Form C"
        Me.chkbxFormC.UseVisualStyleBackColor = True
        '
        'chkbxCT3
        '
        Me.chkbxCT3.AutoSize = True
        Me.chkbxCT3.Location = New System.Drawing.Point(18, 47)
        Me.chkbxCT3.Name = "chkbxCT3"
        Me.chkbxCT3.Size = New System.Drawing.Size(46, 17)
        Me.chkbxCT3.TabIndex = 1
        Me.chkbxCT3.Text = "CT3"
        Me.chkbxCT3.UseVisualStyleBackColor = True
        '
        'chkbxAll
        '
        Me.chkbxAll.AutoSize = True
        Me.chkbxAll.Location = New System.Drawing.Point(18, 22)
        Me.chkbxAll.Name = "chkbxAll"
        Me.chkbxAll.Size = New System.Drawing.Size(37, 17)
        Me.chkbxAll.TabIndex = 0
        Me.chkbxAll.Text = "All"
        Me.chkbxAll.UseVisualStyleBackColor = True
        '
        'plTypeofOrder
        '
        Me.plTypeofOrder.Controls.Add(Me.chkbxJobWorkOrder)
        Me.plTypeofOrder.Controls.Add(Me.chkbxSalesOrder)
        Me.plTypeofOrder.Location = New System.Drawing.Point(561, 75)
        Me.plTypeofOrder.Name = "plTypeofOrder"
        Me.plTypeofOrder.Size = New System.Drawing.Size(148, 85)
        Me.plTypeofOrder.TabIndex = 8
        Me.plTypeofOrder.TabStop = False
        Me.plTypeofOrder.Text = "Type Of Order"
        '
        'chkbxJobWorkOrder
        '
        Me.chkbxJobWorkOrder.AutoSize = True
        Me.chkbxJobWorkOrder.Location = New System.Drawing.Point(7, 49)
        Me.chkbxJobWorkOrder.Name = "chkbxJobWorkOrder"
        Me.chkbxJobWorkOrder.Size = New System.Drawing.Size(98, 17)
        Me.chkbxJobWorkOrder.TabIndex = 1
        Me.chkbxJobWorkOrder.Text = "JobWork Order"
        Me.chkbxJobWorkOrder.UseVisualStyleBackColor = True
        '
        'chkbxSalesOrder
        '
        Me.chkbxSalesOrder.AutoSize = True
        Me.chkbxSalesOrder.Location = New System.Drawing.Point(7, 23)
        Me.chkbxSalesOrder.Name = "chkbxSalesOrder"
        Me.chkbxSalesOrder.Size = New System.Drawing.Size(81, 17)
        Me.chkbxSalesOrder.TabIndex = 0
        Me.chkbxSalesOrder.Text = "Slaes Order"
        Me.chkbxSalesOrder.UseVisualStyleBackColor = True
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(353, 26)
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
        Me.plSelectionCriteria.Location = New System.Drawing.Point(165, 75)
        Me.plSelectionCriteria.Name = "plSelectionCriteria"
        Me.plSelectionCriteria.Size = New System.Drawing.Size(390, 85)
        Me.plSelectionCriteria.TabIndex = 6
        Me.plSelectionCriteria.TabStop = False
        Me.plSelectionCriteria.Text = "Selection Critirea :-"
        '
        'cbxCodification
        '
        Me.cbxCodification.Enabled = False
        Me.cbxCodification.FormattingEnabled = True
        Me.cbxCodification.Location = New System.Drawing.Point(131, 45)
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
        Me.rbCodification.Location = New System.Drawing.Point(21, 44)
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
        Me.chkbxFilter.Location = New System.Drawing.Point(16, 75)
        Me.chkbxFilter.Name = "chkbxFilter"
        Me.chkbxFilter.Size = New System.Drawing.Size(143, 17)
        Me.chkbxFilter.TabIndex = 5
        Me.chkbxFilter.Text = "Filter Based on Selection"
        Me.chkbxFilter.UseVisualStyleBackColor = True
        '
        'chkbxSelectCustomer
        '
        Me.chkbxSelectCustomer.AutoSize = True
        Me.chkbxSelectCustomer.Location = New System.Drawing.Point(182, 26)
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
        Me.dpTo.Location = New System.Drawing.Point(67, 34)
        Me.dpTo.Name = "dpTo"
        Me.dpTo.Size = New System.Drawing.Size(105, 20)
        Me.dpTo.TabIndex = 3
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(67, 8)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(105, 20)
        Me.dpFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To :-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From :-"
        '
        'frmInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 700)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmInvoice"
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pl As System.Windows.Forms.Panel
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
    Friend WithEvents grdInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdInvoicesV1 As DevExpress.XtraGrid.Views.Grid.GridView
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
End Class
