<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoiceGeneration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceGeneration))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cbxCancel = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbxSignatureBy = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.grdReadyToDispatch = New DevExpress.XtraGrid.GridControl
        Me.grdReadyToDispatchV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.cbEdit = New System.Windows.Forms.Button
        Me.cbDelete = New System.Windows.Forms.Button
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkbxArticleType = New System.Windows.Forms.CheckBox
        Me.chkbxOrderType = New System.Windows.Forms.CheckBox
        Me.cbGenerate = New System.Windows.Forms.Button
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.cbxType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.Panel1.SuspendLayout()
        Me.pl.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.grdReadyToDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdReadyToDispatchV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.cbxCancel)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cbxSignatureBy)
        Me.Panel1.Controls.Add(Me.Label4)
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
        'cbxCancel
        '
        Me.cbxCancel.FormattingEnabled = True
        Me.cbxCancel.Items.AddRange(New Object() {"All", "Processed", "To Be Processed"})
        Me.cbxCancel.Location = New System.Drawing.Point(828, 632)
        Me.cbxCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCancel.Name = "cbxCancel"
        Me.cbxCancel.Size = New System.Drawing.Size(232, 24)
        Me.cbxCancel.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(676, 634)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 23)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Reason for Cancel :-"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxSignatureBy
        '
        Me.cbxSignatureBy.FormattingEnabled = True
        Me.cbxSignatureBy.Items.AddRange(New Object() {"All", "Processed", "To Be Processed"})
        Me.cbxSignatureBy.Location = New System.Drawing.Point(828, 669)
        Me.cbxSignatureBy.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxSignatureBy.Name = "cbxSignatureBy"
        Me.cbxSignatureBy.Size = New System.Drawing.Size(232, 24)
        Me.cbxSignatureBy.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(699, 670)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 23)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Signature By :-"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pl
        '
        Me.pl.Controls.Add(Me.Panel9)
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 7)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 617)
        Me.pl.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.grdReadyToDispatch)
        Me.Panel9.Location = New System.Drawing.Point(7, 122)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1184, 491)
        Me.Panel9.TabIndex = 18
        '
        'grdReadyToDispatch
        '
        Me.grdReadyToDispatch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdReadyToDispatch.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdReadyToDispatch.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdReadyToDispatch.Location = New System.Drawing.Point(4, 4)
        Me.grdReadyToDispatch.MainView = Me.grdReadyToDispatchV1
        Me.grdReadyToDispatch.Margin = New System.Windows.Forms.Padding(4)
        Me.grdReadyToDispatch.Name = "grdReadyToDispatch"
        Me.grdReadyToDispatch.Size = New System.Drawing.Size(1174, 481)
        Me.grdReadyToDispatch.TabIndex = 0
        Me.grdReadyToDispatch.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdReadyToDispatchV1})
        '
        'grdReadyToDispatchV1
        '
        Me.grdReadyToDispatchV1.GridControl = Me.grdReadyToDispatch
        Me.grdReadyToDispatchV1.Name = "grdReadyToDispatchV1"
        Me.grdReadyToDispatchV1.OptionsView.ShowAutoFilterRow = True
        Me.grdReadyToDispatchV1.OptionsView.ShowFooter = True
        Me.grdReadyToDispatchV1.OptionsView.ShowGroupPanel = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(7, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1184, 109)
        Me.Panel2.TabIndex = 12
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.cbEdit)
        Me.Panel3.Controls.Add(Me.cbDelete)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.cbGenerate)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1184, 111)
        Me.Panel3.TabIndex = 13
        '
        'cbEdit
        '
        Me.cbEdit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbEdit.Location = New System.Drawing.Point(996, 37)
        Me.cbEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.cbEdit.Name = "cbEdit"
        Me.cbEdit.Size = New System.Drawing.Size(182, 30)
        Me.cbEdit.TabIndex = 19
        Me.cbEdit.Text = "Edit Invoice"
        Me.cbEdit.UseVisualStyleBackColor = True
        '
        'cbDelete
        '
        Me.cbDelete.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbDelete.Location = New System.Drawing.Point(996, 70)
        Me.cbDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.cbDelete.Name = "cbDelete"
        Me.cbDelete.Size = New System.Drawing.Size(182, 30)
        Me.cbDelete.TabIndex = 18
        Me.cbDelete.Text = "Cancel / Delete Invoice"
        Me.cbDelete.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Ivory
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.chkbxArticleType)
        Me.Panel4.Controls.Add(Me.chkbxOrderType)
        Me.Panel4.Location = New System.Drawing.Point(829, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(163, 100)
        Me.Panel4.TabIndex = 17
        '
        'chkbxArticleType
        '
        Me.chkbxArticleType.AutoSize = True
        Me.chkbxArticleType.Checked = True
        Me.chkbxArticleType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxArticleType.Location = New System.Drawing.Point(7, 35)
        Me.chkbxArticleType.Name = "chkbxArticleType"
        Me.chkbxArticleType.Size = New System.Drawing.Size(148, 20)
        Me.chkbxArticleType.TabIndex = 1
        Me.chkbxArticleType.Text = "Same Article Type"
        Me.chkbxArticleType.UseVisualStyleBackColor = True
        '
        'chkbxOrderType
        '
        Me.chkbxOrderType.AutoSize = True
        Me.chkbxOrderType.Checked = True
        Me.chkbxOrderType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxOrderType.Location = New System.Drawing.Point(7, 9)
        Me.chkbxOrderType.Name = "chkbxOrderType"
        Me.chkbxOrderType.Size = New System.Drawing.Size(142, 20)
        Me.chkbxOrderType.TabIndex = 0
        Me.chkbxOrderType.Text = "Same Order Type"
        Me.chkbxOrderType.UseVisualStyleBackColor = True
        '
        'cbGenerate
        '
        Me.cbGenerate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbGenerate.Location = New System.Drawing.Point(996, 4)
        Me.cbGenerate.Margin = New System.Windows.Forms.Padding(4)
        Me.cbGenerate.Name = "cbGenerate"
        Me.cbGenerate.Size = New System.Drawing.Size(182, 30)
        Me.cbGenerate.TabIndex = 16
        Me.cbGenerate.Text = "Generate Invoice"
        Me.cbGenerate.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Ivory
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.cbxType)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.cbxCustomer)
        Me.Panel5.Location = New System.Drawing.Point(228, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(596, 100)
        Me.Panel5.TabIndex = 15
        '
        'cbxType
        '
        Me.cbxType.FormattingEnabled = True
        Me.cbxType.Items.AddRange(New Object() {"All", "Processed", "To Be Processed"})
        Me.cbxType.Location = New System.Drawing.Point(445, 39)
        Me.cbxType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxType.Name = "cbxType"
        Me.cbxType.Size = New System.Drawing.Size(139, 24)
        Me.cbxType.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(465, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 23)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(4, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(341, 23)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Customer / Supplier"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(10, 39)
        Me.cbxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(414, 24)
        Me.cbxCustomer.TabIndex = 7
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Ivory
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Controls.Add(Me.dpTo)
        Me.Panel7.Controls.Add(Me.dpFrom)
        Me.Panel7.Location = New System.Drawing.Point(-1, -1)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(225, 100)
        Me.Panel7.TabIndex = 13
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
        'PrintDocument1
        '
        '
        'frmInvoiceGeneration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInvoiceGeneration"
        Me.Text = "Packing Entries"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.pl.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        CType(Me.grdReadyToDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdReadyToDispatchV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents grdReadyToDispatch As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdReadyToDispatchV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents cbxType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cbGenerate As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkbxArticleType As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxOrderType As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbxSignatureBy As System.Windows.Forms.ComboBox
    Friend WithEvents cbDelete As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbxCancel As System.Windows.Forms.ComboBox
    Friend WithEvents cbEdit As System.Windows.Forms.Button
End Class
