<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRejectionMainReport
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRejectionMainReport))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdRejectionOut = New DevExpress.XtraGrid.GridControl
        Me.grdRejectionOutV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.grdRejectionIn = New DevExpress.XtraGrid.GridControl
        Me.grdRejectionInV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.plStatus = New System.Windows.Forms.GroupBox
        Me.rbOutStatusAll = New System.Windows.Forms.RadioButton
        Me.rbOutStatusScrap = New System.Windows.Forms.RadioButton
        Me.rbOutStatusReUsable = New System.Windows.Forms.RadioButton
        Me.plProcessed = New System.Windows.Forms.GroupBox
        Me.rbProcessAll = New System.Windows.Forms.RadioButton
        Me.rbProcessNo = New System.Windows.Forms.RadioButton
        Me.rbProcessYes = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbxType = New System.Windows.Forms.ComboBox
        Me.rbOut = New System.Windows.Forms.RadioButton
        Me.rbIn = New System.Windows.Forms.RadioButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dpTo = New System.Windows.Forms.DateTimePicker
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.grdRejectionOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRejectionOutV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRejectionIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRejectionInV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.plStatus.SuspendLayout()
        Me.plProcessed.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.grdRejectionOut)
        Me.Panel1.Controls.Add(Me.grdRejectionIn)
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
        'grdRejectionOut
        '
        Me.grdRejectionOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRejectionOut.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode1.RelationName = "Level1"
        Me.grdRejectionOut.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grdRejectionOut.Location = New System.Drawing.Point(12, 228)
        Me.grdRejectionOut.MainView = Me.grdRejectionOutV1
        Me.grdRejectionOut.Margin = New System.Windows.Forms.Padding(4)
        Me.grdRejectionOut.Name = "grdRejectionOut"
        Me.grdRejectionOut.Size = New System.Drawing.Size(1182, 393)
        Me.grdRejectionOut.TabIndex = 19
        Me.grdRejectionOut.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdRejectionOutV1})
        '
        'grdRejectionOutV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = CType(0, Short)
        Me.grdRejectionOutV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdRejectionOutV1.GridControl = Me.grdRejectionOut
        Me.grdRejectionOutV1.Name = "grdRejectionOutV1"
        Me.grdRejectionOutV1.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.grdRejectionOutV1.OptionsSelection.InvertSelection = True
        Me.grdRejectionOutV1.OptionsSelection.UseIndicatorForSelection = False
        Me.grdRejectionOutV1.OptionsView.ShowAutoFilterRow = True
        Me.grdRejectionOutV1.OptionsView.ShowFooter = True
        Me.grdRejectionOutV1.OptionsView.ShowGroupPanel = False
        '
        'grdRejectionIn
        '
        Me.grdRejectionIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRejectionIn.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        GridLevelNode2.RelationName = "Level1"
        Me.grdRejectionIn.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.grdRejectionIn.Location = New System.Drawing.Point(12, 228)
        Me.grdRejectionIn.MainView = Me.grdRejectionInV1
        Me.grdRejectionIn.Margin = New System.Windows.Forms.Padding(4)
        Me.grdRejectionIn.Name = "grdRejectionIn"
        Me.grdRejectionIn.Size = New System.Drawing.Size(1182, 393)
        Me.grdRejectionIn.TabIndex = 18
        Me.grdRejectionIn.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdRejectionInV1})
        '
        'grdRejectionInV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdRejectionInV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdRejectionInV1.GridControl = Me.grdRejectionIn
        Me.grdRejectionInV1.Name = "grdRejectionInV1"
        Me.grdRejectionInV1.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.grdRejectionInV1.OptionsSelection.InvertSelection = True
        Me.grdRejectionInV1.OptionsSelection.UseIndicatorForSelection = False
        Me.grdRejectionInV1.OptionsView.ShowAutoFilterRow = True
        Me.grdRejectionInV1.OptionsView.ShowFooter = True
        Me.grdRejectionInV1.OptionsView.ShowGroupPanel = False
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
        Me.pl.Size = New System.Drawing.Size(1198, 215)
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
        Me.Panel2.Size = New System.Drawing.Size(1184, 199)
        Me.Panel2.TabIndex = 12
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Ivory
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Location = New System.Drawing.Point(-1, 103)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1184, 100)
        Me.Panel6.TabIndex = 16
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Ivory
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Location = New System.Drawing.Point(942, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(241, 100)
        Me.Panel5.TabIndex = 15
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Ivory
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.plStatus)
        Me.Panel4.Controls.Add(Me.plProcessed)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.cbxType)
        Me.Panel4.Controls.Add(Me.rbOut)
        Me.Panel4.Controls.Add(Me.rbIn)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(640, 100)
        Me.Panel4.TabIndex = 14
        '
        'plStatus
        '
        Me.plStatus.Controls.Add(Me.rbOutStatusAll)
        Me.plStatus.Controls.Add(Me.rbOutStatusScrap)
        Me.plStatus.Controls.Add(Me.rbOutStatusReUsable)
        Me.plStatus.Location = New System.Drawing.Point(452, 5)
        Me.plStatus.Name = "plStatus"
        Me.plStatus.Size = New System.Drawing.Size(172, 89)
        Me.plStatus.TabIndex = 14
        Me.plStatus.TabStop = False
        Me.plStatus.Text = "Out Status :-"
        '
        'rbOutStatusAll
        '
        Me.rbOutStatusAll.AutoSize = True
        Me.rbOutStatusAll.Checked = True
        Me.rbOutStatusAll.Location = New System.Drawing.Point(14, 23)
        Me.rbOutStatusAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOutStatusAll.Name = "rbOutStatusAll"
        Me.rbOutStatusAll.Size = New System.Drawing.Size(41, 20)
        Me.rbOutStatusAll.TabIndex = 10
        Me.rbOutStatusAll.TabStop = True
        Me.rbOutStatusAll.Text = "All"
        Me.rbOutStatusAll.UseVisualStyleBackColor = True
        '
        'rbOutStatusScrap
        '
        Me.rbOutStatusScrap.AutoSize = True
        Me.rbOutStatusScrap.Location = New System.Drawing.Point(104, 50)
        Me.rbOutStatusScrap.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOutStatusScrap.Name = "rbOutStatusScrap"
        Me.rbOutStatusScrap.Size = New System.Drawing.Size(64, 20)
        Me.rbOutStatusScrap.TabIndex = 9
        Me.rbOutStatusScrap.Text = "Scrap"
        Me.rbOutStatusScrap.UseVisualStyleBackColor = True
        '
        'rbOutStatusReUsable
        '
        Me.rbOutStatusReUsable.AutoSize = True
        Me.rbOutStatusReUsable.Location = New System.Drawing.Point(14, 50)
        Me.rbOutStatusReUsable.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOutStatusReUsable.Name = "rbOutStatusReUsable"
        Me.rbOutStatusReUsable.Size = New System.Drawing.Size(90, 20)
        Me.rbOutStatusReUsable.TabIndex = 8
        Me.rbOutStatusReUsable.Text = "Re Usable"
        Me.rbOutStatusReUsable.UseVisualStyleBackColor = True
        '
        'plProcessed
        '
        Me.plProcessed.Controls.Add(Me.rbProcessAll)
        Me.plProcessed.Controls.Add(Me.rbProcessNo)
        Me.plProcessed.Controls.Add(Me.rbProcessYes)
        Me.plProcessed.Location = New System.Drawing.Point(283, 5)
        Me.plProcessed.Name = "plProcessed"
        Me.plProcessed.Size = New System.Drawing.Size(163, 89)
        Me.plProcessed.TabIndex = 13
        Me.plProcessed.TabStop = False
        Me.plProcessed.Text = "Processed :-"
        '
        'rbProcessAll
        '
        Me.rbProcessAll.AutoSize = True
        Me.rbProcessAll.Checked = True
        Me.rbProcessAll.Location = New System.Drawing.Point(14, 23)
        Me.rbProcessAll.Margin = New System.Windows.Forms.Padding(4)
        Me.rbProcessAll.Name = "rbProcessAll"
        Me.rbProcessAll.Size = New System.Drawing.Size(41, 20)
        Me.rbProcessAll.TabIndex = 7
        Me.rbProcessAll.TabStop = True
        Me.rbProcessAll.Text = "All"
        Me.rbProcessAll.UseVisualStyleBackColor = True
        '
        'rbProcessNo
        '
        Me.rbProcessNo.AutoSize = True
        Me.rbProcessNo.Location = New System.Drawing.Point(84, 50)
        Me.rbProcessNo.Margin = New System.Windows.Forms.Padding(4)
        Me.rbProcessNo.Name = "rbProcessNo"
        Me.rbProcessNo.Size = New System.Drawing.Size(43, 20)
        Me.rbProcessNo.TabIndex = 6
        Me.rbProcessNo.Text = "No"
        Me.rbProcessNo.UseVisualStyleBackColor = True
        '
        'rbProcessYes
        '
        Me.rbProcessYes.AutoSize = True
        Me.rbProcessYes.Location = New System.Drawing.Point(14, 50)
        Me.rbProcessYes.Margin = New System.Windows.Forms.Padding(4)
        Me.rbProcessYes.Name = "rbProcessYes"
        Me.rbProcessYes.Size = New System.Drawing.Size(50, 20)
        Me.rbProcessYes.TabIndex = 5
        Me.rbProcessYes.Text = "Yes"
        Me.rbProcessYes.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(113, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 23)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Type of Material"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxType
        '
        Me.cbxType.FormattingEnabled = True
        Me.cbxType.Location = New System.Drawing.Point(116, 38)
        Me.cbxType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxType.Name = "cbxType"
        Me.cbxType.Size = New System.Drawing.Size(139, 24)
        Me.cbxType.TabIndex = 11
        '
        'rbOut
        '
        Me.rbOut.AutoSize = True
        Me.rbOut.Location = New System.Drawing.Point(8, 55)
        Me.rbOut.Margin = New System.Windows.Forms.Padding(4)
        Me.rbOut.Name = "rbOut"
        Me.rbOut.Size = New System.Drawing.Size(50, 20)
        Me.rbOut.TabIndex = 4
        Me.rbOut.Text = "Out"
        Me.rbOut.UseVisualStyleBackColor = True
        '
        'rbIn
        '
        Me.rbIn.AutoSize = True
        Me.rbIn.Checked = True
        Me.rbIn.Location = New System.Drawing.Point(8, 24)
        Me.rbIn.Margin = New System.Windows.Forms.Padding(4)
        Me.rbIn.Name = "rbIn"
        Me.rbIn.Size = New System.Drawing.Size(39, 20)
        Me.rbIn.TabIndex = 3
        Me.rbIn.TabStop = True
        Me.rbIn.Text = "In"
        Me.rbIn.UseVisualStyleBackColor = True
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
        'frmRejectionMainReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmRejectionMainReport"
        Me.Text = "frmAllinOne V1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdRejectionOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRejectionOutV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRejectionIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRejectionInV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.plStatus.ResumeLayout(False)
        Me.plStatus.PerformLayout()
        Me.plProcessed.ResumeLayout(False)
        Me.plProcessed.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents rbOut As System.Windows.Forms.RadioButton
    Friend WithEvents rbIn As System.Windows.Forms.RadioButton
    Friend WithEvents cbxType As System.Windows.Forms.ComboBox
    Friend WithEvents plProcessed As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rbProcessAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbProcessNo As System.Windows.Forms.RadioButton
    Friend WithEvents rbProcessYes As System.Windows.Forms.RadioButton
    Friend WithEvents plStatus As System.Windows.Forms.GroupBox
    Friend WithEvents grdRejectionIn As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdRejectionInV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdRejectionOut As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdRejectionOutV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rbOutStatusAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbOutStatusScrap As System.Windows.Forms.RadioButton
    Friend WithEvents rbOutStatusReUsable As System.Windows.Forms.RadioButton
End Class
