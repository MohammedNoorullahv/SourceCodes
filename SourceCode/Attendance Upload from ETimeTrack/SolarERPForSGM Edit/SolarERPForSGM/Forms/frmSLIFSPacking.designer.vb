<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSLIFSPacking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSLIFSPacking))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Dim StyleFormatCondition2 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer
        Me.tbPacked = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.tbTotalBoxes = New System.Windows.Forms.TextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.grdCartonDtls = New DevExpress.XtraGrid.GridControl
        Me.grdCartonDtlsV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.tbJobcardNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdArticleMaster = New DevExpress.XtraGrid.GridControl
        Me.grdArticleMasterV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCartonDtls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCartonDtlsV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.grdArticleMaster)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1015, 640)
        Me.Panel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(1075, 563)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(108, 66)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(555, 581)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(361, 16)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "12. 30-Jun-17 Ecco Scanning - With Weight Updates"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(306, 563)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 74)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Upper Short Dispatch Report"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Export2Excel
        '
        Me.Export2Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Export2Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export2Excel.Image = CType(resources.GetObject("Export2Excel.Image"), System.Drawing.Image)
        Me.Export2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Export2Excel.Location = New System.Drawing.Point(144, 563)
        Me.Export2Excel.Name = "Export2Excel"
        Me.Export2Excel.Size = New System.Drawing.Size(156, 74)
        Me.Export2Excel.TabIndex = 4
        Me.Export2Excel.Text = "Expot to Excel"
        Me.Export2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export2Excel.UseVisualStyleBackColor = True
        Me.Export2Excel.Visible = False
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(884, 561)
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
        Me.cbReferesh.Location = New System.Drawing.Point(10, 563)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        Me.cbReferesh.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.Panel2.Controls.Add(Me.tbPacked)
        Me.Panel2.Controls.Add(Me.Label36)
        Me.Panel2.Controls.Add(Me.tbTotalBoxes)
        Me.Panel2.Controls.Add(Me.Label35)
        Me.Panel2.Controls.Add(Me.grdCartonDtls)
        Me.Panel2.Controls.Add(Me.tbJobcardNo)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(10, 15)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(993, 542)
        Me.Panel2.TabIndex = 0
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(479, 250)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(35, 43)
        Me.AxWindowsMediaPlayer1.TabIndex = 122
        Me.AxWindowsMediaPlayer1.Visible = False
        '
        'tbPacked
        '
        Me.tbPacked.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPacked.ForeColor = System.Drawing.Color.Green
        Me.tbPacked.Location = New System.Drawing.Point(548, 5)
        Me.tbPacked.Name = "tbPacked"
        Me.tbPacked.Size = New System.Drawing.Size(74, 23)
        Me.tbPacked.TabIndex = 118
        Me.tbPacked.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(451, 6)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(93, 20)
        Me.Label36.TabIndex = 117
        Me.Label36.Text = "Toral Packed :-"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbTotalBoxes
        '
        Me.tbTotalBoxes.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbTotalBoxes.Location = New System.Drawing.Point(371, 5)
        Me.tbTotalBoxes.Name = "tbTotalBoxes"
        Me.tbTotalBoxes.Size = New System.Drawing.Size(74, 23)
        Me.tbTotalBoxes.TabIndex = 116
        Me.tbTotalBoxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(274, 6)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(93, 20)
        Me.Label35.TabIndex = 115
        Me.Label35.Text = "Toral Boxes :-"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdCartonDtls
        '
        Me.grdCartonDtls.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCartonDtls.Location = New System.Drawing.Point(9, 37)
        Me.grdCartonDtls.MainView = Me.grdCartonDtlsV1
        Me.grdCartonDtls.Name = "grdCartonDtls"
        Me.grdCartonDtls.Size = New System.Drawing.Size(981, 505)
        Me.grdCartonDtls.TabIndex = 113
        Me.grdCartonDtls.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdCartonDtlsV1})
        '
        'grdCartonDtlsV1
        '
        StyleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue
        StyleFormatCondition1.Appearance.Options.UseForeColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater
        StyleFormatCondition1.Value1 = New Decimal(New Integer() {0, 0, 0, 0})
        Me.grdCartonDtlsV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.grdCartonDtlsV1.GridControl = Me.grdCartonDtls
        Me.grdCartonDtlsV1.Name = "grdCartonDtlsV1"
        Me.grdCartonDtlsV1.OptionsView.ColumnAutoWidth = False
        Me.grdCartonDtlsV1.OptionsView.ShowAutoFilterRow = True
        Me.grdCartonDtlsV1.OptionsView.ShowFooter = True
        '
        'tbJobcardNo
        '
        Me.tbJobcardNo.Location = New System.Drawing.Point(62, 6)
        Me.tbJobcardNo.Name = "tbJobcardNo"
        Me.tbJobcardNo.Size = New System.Drawing.Size(192, 20)
        Me.tbJobcardNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Box No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdArticleMaster
        '
        Me.grdArticleMaster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdArticleMaster.Location = New System.Drawing.Point(20, 308)
        Me.grdArticleMaster.MainView = Me.grdArticleMasterV1
        Me.grdArticleMaster.Name = "grdArticleMaster"
        Me.grdArticleMaster.Size = New System.Drawing.Size(687, 87)
        Me.grdArticleMaster.TabIndex = 3
        Me.grdArticleMaster.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdArticleMasterV1})
        Me.grdArticleMaster.Visible = False
        '
        'grdArticleMasterV1
        '
        StyleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.White
        StyleFormatCondition2.Appearance.Options.UseForeColor = True
        StyleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition2.Value1 = CType(0, Short)
        Me.grdArticleMasterV1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition2})
        Me.grdArticleMasterV1.GridControl = Me.grdArticleMaster
        Me.grdArticleMasterV1.Name = "grdArticleMasterV1"
        Me.grdArticleMasterV1.OptionsView.ColumnAutoWidth = False
        Me.grdArticleMasterV1.OptionsView.ShowAutoFilterRow = True
        Me.grdArticleMasterV1.OptionsView.ShowFooter = True
        Me.grdArticleMasterV1.OptionsView.ShowGroupPanel = False
        '
        'frmSLIFSPacking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cbExit
        Me.ClientSize = New System.Drawing.Size(1020, 663)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmSLIFSPacking"
        Me.Text = "ECCO Inner Box Packing According to Outer Carton"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCartonDtls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCartonDtlsV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents grdArticleMaster As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdArticleMasterV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbJobcardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grdCartonDtls As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdCartonDtlsV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents tbPacked As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents tbTotalBoxes As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer

End Class
