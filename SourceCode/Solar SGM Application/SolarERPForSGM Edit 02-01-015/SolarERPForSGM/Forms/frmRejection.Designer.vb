<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRejection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRejection))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkbxDisplayDtl = New System.Windows.Forms.CheckBox
        Me.tbArticleDescription = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.tbArticleCode = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.rbDetailed = New System.Windows.Forms.RadioButton
        Me.rbSummary = New System.Windows.Forms.RadioButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.cbxArticleName = New System.Windows.Forms.ComboBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rbClose = New System.Windows.Forms.RadioButton
        Me.rbPending = New System.Windows.Forms.RadioButton
        Me.rbCompleted = New System.Windows.Forms.RadioButton
        Me.rbAll = New System.Windows.Forms.RadioButton
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rbInAndOut = New System.Windows.Forms.RadioButton
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbxTypeofOrder = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbxTypeofDocument = New System.Windows.Forms.ComboBox
        Me.rbOutJournal = New System.Windows.Forms.RadioButton
        Me.rbInJournal = New System.Windows.Forms.RadioButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.plRejectionDetails1 = New System.Windows.Forms.Panel
        Me.grdWastageDetails = New DevExpress.XtraGrid.GridControl
        Me.grdWastageDetailsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdWastage = New DevExpress.XtraGrid.GridControl
        Me.grdPurchaseInvoicesV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.plRejectionDetails1.SuspendLayout()
        CType(Me.grdWastageDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWastageDetailsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdWastage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchaseInvoicesV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.pgbar)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.plRejectionDetails1)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
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
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(438, 122)
        Me.pl.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(7, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(422, 108)
        Me.Panel2.TabIndex = 12
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Ivory
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.chkbxDisplayDtl)
        Me.Panel6.Controls.Add(Me.tbArticleDescription)
        Me.Panel6.Controls.Add(Me.Label10)
        Me.Panel6.Controls.Add(Me.tbArticleCode)
        Me.Panel6.Controls.Add(Me.Label9)
        Me.Panel6.Controls.Add(Me.rbDetailed)
        Me.Panel6.Controls.Add(Me.rbSummary)
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Controls.Add(Me.cbxArticleName)
        Me.Panel6.Location = New System.Drawing.Point(-1, 103)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1184, 100)
        Me.Panel6.TabIndex = 16
        Me.Panel6.Visible = False
        '
        'chkbxDisplayDtl
        '
        Me.chkbxDisplayDtl.AutoSize = True
        Me.chkbxDisplayDtl.Enabled = False
        Me.chkbxDisplayDtl.Location = New System.Drawing.Point(733, 62)
        Me.chkbxDisplayDtl.Name = "chkbxDisplayDtl"
        Me.chkbxDisplayDtl.Size = New System.Drawing.Size(431, 20)
        Me.chkbxDisplayDtl.TabIndex = 19
        Me.chkbxDisplayDtl.Text = "Display Invoice Details of Sales Order with Drop Down Option"
        Me.chkbxDisplayDtl.UseVisualStyleBackColor = True
        '
        'tbArticleDescription
        '
        Me.tbArticleDescription.Location = New System.Drawing.Point(455, 39)
        Me.tbArticleDescription.Name = "tbArticleDescription"
        Me.tbArticleDescription.Size = New System.Drawing.Size(243, 23)
        Me.tbArticleDescription.TabIndex = 18
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(456, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(220, 23)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Article / Material Description"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbArticleCode
        '
        Me.tbArticleCode.Location = New System.Drawing.Point(230, 39)
        Me.tbArticleCode.Name = "tbArticleCode"
        Me.tbArticleCode.Size = New System.Drawing.Size(220, 23)
        Me.tbArticleCode.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(230, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(220, 23)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Article / Material Code"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rbDetailed
        '
        Me.rbDetailed.AutoSize = True
        Me.rbDetailed.Location = New System.Drawing.Point(1085, 35)
        Me.rbDetailed.Margin = New System.Windows.Forms.Padding(4)
        Me.rbDetailed.Name = "rbDetailed"
        Me.rbDetailed.Size = New System.Drawing.Size(79, 20)
        Me.rbDetailed.TabIndex = 13
        Me.rbDetailed.Text = "Detailed"
        Me.rbDetailed.UseVisualStyleBackColor = True
        '
        'rbSummary
        '
        Me.rbSummary.AutoSize = True
        Me.rbSummary.Checked = True
        Me.rbSummary.Location = New System.Drawing.Point(1085, 11)
        Me.rbSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.rbSummary.Name = "rbSummary"
        Me.rbSummary.Size = New System.Drawing.Size(86, 20)
        Me.rbSummary.TabIndex = 12
        Me.rbSummary.TabStop = True
        Me.rbSummary.Text = "Summary"
        Me.rbSummary.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(220, 23)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Article / Material Short Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxArticleName
        '
        Me.cbxArticleName.FormattingEnabled = True
        Me.cbxArticleName.Location = New System.Drawing.Point(10, 39)
        Me.cbxArticleName.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxArticleName.Name = "cbxArticleName"
        Me.cbxArticleName.Size = New System.Drawing.Size(214, 24)
        Me.cbxArticleName.TabIndex = 7
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Ivory
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.rbClose)
        Me.Panel5.Controls.Add(Me.rbPending)
        Me.Panel5.Controls.Add(Me.rbCompleted)
        Me.Panel5.Controls.Add(Me.rbAll)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.cbxCustomer)
        Me.Panel5.Location = New System.Drawing.Point(645, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(538, 100)
        Me.Panel5.TabIndex = 15
        Me.Panel5.Visible = False
        '
        'rbClose
        '
        Me.rbClose.AutoSize = True
        Me.rbClose.Location = New System.Drawing.Point(439, 74)
        Me.rbClose.Margin = New System.Windows.Forms.Padding(4)
        Me.rbClose.Name = "rbClose"
        Me.rbClose.Size = New System.Drawing.Size(61, 20)
        Me.rbClose.TabIndex = 15
        Me.rbClose.Text = "Close"
        Me.rbClose.UseVisualStyleBackColor = True
        '
        'rbPending
        '
        Me.rbPending.AutoSize = True
        Me.rbPending.Location = New System.Drawing.Point(439, 50)
        Me.rbPending.Margin = New System.Windows.Forms.Padding(4)
        Me.rbPending.Name = "rbPending"
        Me.rbPending.Size = New System.Drawing.Size(77, 20)
        Me.rbPending.TabIndex = 14
        Me.rbPending.Text = "Pending"
        Me.rbPending.UseVisualStyleBackColor = True
        '
        'rbCompleted
        '
        Me.rbCompleted.AutoSize = True
        Me.rbCompleted.Location = New System.Drawing.Point(439, 26)
        Me.rbCompleted.Margin = New System.Windows.Forms.Padding(4)
        Me.rbCompleted.Name = "rbCompleted"
        Me.rbCompleted.Size = New System.Drawing.Size(95, 20)
        Me.rbCompleted.TabIndex = 13
        Me.rbCompleted.Text = "Completed"
        Me.rbCompleted.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Location = New System.Drawing.Point(439, 3)
        Me.rbAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(41, 20)
        Me.rbAll.TabIndex = 12
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
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
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Ivory
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.rbInAndOut)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.cbxTypeofOrder)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.cbxTypeofDocument)
        Me.Panel4.Controls.Add(Me.rbOutJournal)
        Me.Panel4.Controls.Add(Me.rbInJournal)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(413, 100)
        Me.Panel4.TabIndex = 14
        '
        'rbInAndOut
        '
        Me.rbInAndOut.AutoSize = True
        Me.rbInAndOut.Checked = True
        Me.rbInAndOut.Location = New System.Drawing.Point(14, 9)
        Me.rbInAndOut.Margin = New System.Windows.Forms.Padding(4)
        Me.rbInAndOut.Name = "rbInAndOut"
        Me.rbInAndOut.Size = New System.Drawing.Size(162, 20)
        Me.rbInAndOut.TabIndex = 14
        Me.rbInAndOut.TabStop = True
        Me.rbInAndOut.Text = "In && Out [Combined]"
        Me.rbInAndOut.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(260, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 23)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Type of Order"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label6.Visible = False
        '
        'cbxTypeofOrder
        '
        Me.cbxTypeofOrder.FormattingEnabled = True
        Me.cbxTypeofOrder.Items.AddRange(New Object() {"All", "Job", "Sales"})
        Me.cbxTypeofOrder.Location = New System.Drawing.Point(260, 39)
        Me.cbxTypeofOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxTypeofOrder.Name = "cbxTypeofOrder"
        Me.cbxTypeofOrder.Size = New System.Drawing.Size(139, 24)
        Me.cbxTypeofOrder.TabIndex = 12
        Me.cbxTypeofOrder.Visible = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(113, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 23)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Type of Material"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'cbxTypeofDocument
        '
        Me.cbxTypeofDocument.FormattingEnabled = True
        Me.cbxTypeofDocument.Items.AddRange(New Object() {"Order", "Invoice"})
        Me.cbxTypeofDocument.Location = New System.Drawing.Point(113, 40)
        Me.cbxTypeofDocument.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxTypeofDocument.Name = "cbxTypeofDocument"
        Me.cbxTypeofDocument.Size = New System.Drawing.Size(139, 24)
        Me.cbxTypeofDocument.TabIndex = 10
        Me.cbxTypeofDocument.Visible = False
        '
        'rbOutJournal
        '
        Me.rbOutJournal.AutoSize = True
        Me.rbOutJournal.Location = New System.Drawing.Point(14, 61)
        Me.rbOutJournal.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOutJournal.Name = "rbOutJournal"
        Me.rbOutJournal.Size = New System.Drawing.Size(101, 20)
        Me.rbOutJournal.TabIndex = 2
        Me.rbOutJournal.Text = "Out Journal"
        Me.rbOutJournal.UseVisualStyleBackColor = True
        '
        'rbInJournal
        '
        Me.rbInJournal.AutoSize = True
        Me.rbInJournal.Location = New System.Drawing.Point(14, 35)
        Me.rbInJournal.Margin = New System.Windows.Forms.Padding(4)
        Me.rbInJournal.Name = "rbInJournal"
        Me.rbInJournal.Size = New System.Drawing.Size(90, 20)
        Me.rbInJournal.TabIndex = 1
        Me.rbInJournal.Text = "In Journal"
        Me.rbInJournal.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Ivory
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.dpTo)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 100)
        Me.Panel3.TabIndex = 13
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
        'plRejectionDetails1
        '
        Me.plRejectionDetails1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.plRejectionDetails1.Controls.Add(Me.grdWastageDetails)
        Me.plRejectionDetails1.Controls.Add(Me.grdWastage)
        Me.plRejectionDetails1.Location = New System.Drawing.Point(12, 135)
        Me.plRejectionDetails1.Name = "plRejectionDetails1"
        Me.plRejectionDetails1.Size = New System.Drawing.Size(1184, 486)
        Me.plRejectionDetails1.TabIndex = 17
        '
        'grdWastageDetails
        '
        Me.grdWastageDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdWastageDetails.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdWastageDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdWastageDetails.Location = New System.Drawing.Point(296, 9)
        Me.grdWastageDetails.MainView = Me.grdWastageDetailsV1
        Me.grdWastageDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.grdWastageDetails.Name = "grdWastageDetails"
        Me.grdWastageDetails.Size = New System.Drawing.Size(883, 473)
        Me.grdWastageDetails.TabIndex = 16
        Me.grdWastageDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdWastageDetailsV1})
        '
        'grdWastageDetailsV1
        '
        Me.grdWastageDetailsV1.GridControl = Me.grdWastageDetails
        Me.grdWastageDetailsV1.Name = "grdWastageDetailsV1"
        Me.grdWastageDetailsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdWastageDetailsV1.OptionsView.ShowFooter = True
        Me.grdWastageDetailsV1.OptionsView.ShowGroupPanel = False
        '
        'grdWastage
        '
        Me.grdWastage.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdWastage.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdWastage.Location = New System.Drawing.Point(6, 9)
        Me.grdWastage.MainView = Me.grdPurchaseInvoicesV1
        Me.grdWastage.Margin = New System.Windows.Forms.Padding(4)
        Me.grdWastage.Name = "grdWastage"
        Me.grdWastage.Size = New System.Drawing.Size(282, 473)
        Me.grdWastage.TabIndex = 12
        Me.grdWastage.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdPurchaseInvoicesV1})
        '
        'grdPurchaseInvoicesV1
        '
        Me.grdPurchaseInvoicesV1.GridControl = Me.grdWastage
        Me.grdPurchaseInvoicesV1.Name = "grdPurchaseInvoicesV1"
        Me.grdPurchaseInvoicesV1.OptionsView.ShowAutoFilterRow = True
        Me.grdPurchaseInvoicesV1.OptionsView.ShowFooter = True
        Me.grdPurchaseInvoicesV1.OptionsView.ShowGroupPanel = False
        '
        'frmRejection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmRejection"
        Me.Text = "frmInvoice"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.plRejectionDetails1.ResumeLayout(False)
        CType(Me.grdWastageDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWastageDetailsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdWastage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchaseInvoicesV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbxTypeofOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbxTypeofDocument As System.Windows.Forms.ComboBox
    Friend WithEvents rbOutJournal As System.Windows.Forms.RadioButton
    Friend WithEvents rbInJournal As System.Windows.Forms.RadioButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rbCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rbPending As System.Windows.Forms.RadioButton
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents tbArticleCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents rbDetailed As System.Windows.Forms.RadioButton
    Friend WithEvents rbSummary As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbxArticleName As System.Windows.Forms.ComboBox
    Friend WithEvents tbArticleDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents grdWastage As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdPurchaseInvoicesV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents chkbxDisplayDtl As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents rbClose As System.Windows.Forms.RadioButton
    Friend WithEvents rbInAndOut As System.Windows.Forms.RadioButton
    Friend WithEvents grdWastageDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdWastageDetailsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents plRejectionDetails1 As System.Windows.Forms.Panel
End Class
