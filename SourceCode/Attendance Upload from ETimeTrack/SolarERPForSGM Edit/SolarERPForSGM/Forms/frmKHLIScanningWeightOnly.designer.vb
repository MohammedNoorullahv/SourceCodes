<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKHLIScanningWeightOnly
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKHLIScanningWeightOnly))
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.cbExit = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.tbOuterBoxNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.tbDifference = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.tbActualWeight = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.tbGrossWeight = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.tbOuterWeight = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.tbOuterBox = New System.Windows.Forms.TextBox
        Me.tbInnerWeight = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.grdArticleMaster = New DevExpress.XtraGrid.GridControl
        Me.grdArticleMasterV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.grdArticleMaster)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(627, 640)
        Me.Panel1.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(194, 594)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(270, 16)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "05.16-Apr-15 Ecco Scanning - Updates"
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(-203, 563)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(117, 74)
        Me.cbExit.TabIndex = 2
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Location = New System.Drawing.Point(10, 15)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(603, 542)
        Me.Panel2.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Pink
        Me.GroupBox2.Controls.Add(Me.tbOuterBoxNo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.tbDifference)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.tbActualWeight)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.tbGrossWeight)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.tbOuterWeight)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.tbOuterBox)
        Me.GroupBox2.Controls.Add(Me.tbInnerWeight)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(580, 528)
        Me.GroupBox2.TabIndex = 111
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Weight Info"
        '
        'tbOuterBoxNo
        '
        Me.tbOuterBoxNo.BackColor = System.Drawing.Color.White
        Me.tbOuterBoxNo.Font = New System.Drawing.Font("Verdana", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOuterBoxNo.ForeColor = System.Drawing.Color.Blue
        Me.tbOuterBoxNo.Location = New System.Drawing.Point(101, 16)
        Me.tbOuterBoxNo.Name = "tbOuterBoxNo"
        Me.tbOuterBoxNo.ReadOnly = True
        Me.tbOuterBoxNo.Size = New System.Drawing.Size(473, 64)
        Me.tbOuterBoxNo.TabIndex = 120
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 23)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Outer Box No.:-"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbDifference
        '
        Me.tbDifference.BackColor = System.Drawing.Color.White
        Me.tbDifference.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDifference.ForeColor = System.Drawing.Color.Blue
        Me.tbDifference.Location = New System.Drawing.Point(101, 406)
        Me.tbDifference.Name = "tbDifference"
        Me.tbDifference.ReadOnly = True
        Me.tbDifference.Size = New System.Drawing.Size(473, 116)
        Me.tbDifference.TabIndex = 118
        Me.tbDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(19, 406)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(76, 23)
        Me.Label29.TabIndex = 117
        Me.Label29.Text = "Difference :-"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbActualWeight
        '
        Me.tbActualWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbActualWeight.Location = New System.Drawing.Point(101, 284)
        Me.tbActualWeight.Name = "tbActualWeight"
        Me.tbActualWeight.Size = New System.Drawing.Size(473, 116)
        Me.tbActualWeight.TabIndex = 116
        Me.tbActualWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(17, 294)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(76, 58)
        Me.Label28.TabIndex = 115
        Me.Label28.Text = "Actual Weight ( From Weighing Machine ) :-"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbGrossWeight
        '
        Me.tbGrossWeight.BackColor = System.Drawing.Color.White
        Me.tbGrossWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbGrossWeight.ForeColor = System.Drawing.Color.Blue
        Me.tbGrossWeight.Location = New System.Drawing.Point(101, 162)
        Me.tbGrossWeight.Name = "tbGrossWeight"
        Me.tbGrossWeight.ReadOnly = True
        Me.tbGrossWeight.Size = New System.Drawing.Size(473, 116)
        Me.tbGrossWeight.TabIndex = 114
        Me.tbGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(19, 162)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(76, 23)
        Me.Label26.TabIndex = 113
        Me.Label26.Text = "Gross Weight :-"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbOuterWeight
        '
        Me.tbOuterWeight.BackColor = System.Drawing.Color.White
        Me.tbOuterWeight.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOuterWeight.ForeColor = System.Drawing.Color.Blue
        Me.tbOuterWeight.Location = New System.Drawing.Point(337, 135)
        Me.tbOuterWeight.Name = "tbOuterWeight"
        Me.tbOuterWeight.ReadOnly = True
        Me.tbOuterWeight.Size = New System.Drawing.Size(96, 23)
        Me.tbOuterWeight.TabIndex = 112
        Me.tbOuterWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(211, 135)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(122, 23)
        Me.Label24.TabIndex = 111
        Me.Label24.Text = "Outer Carton Weight :-"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbOuterBox
        '
        Me.tbOuterBox.BackColor = System.Drawing.Color.White
        Me.tbOuterBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbOuterBox.ForeColor = System.Drawing.Color.Blue
        Me.tbOuterBox.Location = New System.Drawing.Point(101, 108)
        Me.tbOuterBox.Name = "tbOuterBox"
        Me.tbOuterBox.ReadOnly = True
        Me.tbOuterBox.Size = New System.Drawing.Size(473, 23)
        Me.tbOuterBox.TabIndex = 110
        '
        'tbInnerWeight
        '
        Me.tbInnerWeight.BackColor = System.Drawing.Color.White
        Me.tbInnerWeight.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbInnerWeight.ForeColor = System.Drawing.Color.Blue
        Me.tbInnerWeight.Location = New System.Drawing.Point(101, 135)
        Me.tbInnerWeight.Name = "tbInnerWeight"
        Me.tbInnerWeight.ReadOnly = True
        Me.tbInnerWeight.Size = New System.Drawing.Size(96, 23)
        Me.tbInnerWeight.TabIndex = 109
        Me.tbInnerWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(19, 108)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(76, 23)
        Me.Label25.TabIndex = 108
        Me.Label25.Text = "Outer Box :-"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(19, 135)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(76, 23)
        Me.Label27.TabIndex = 107
        Me.Label27.Text = "Inner Box Weight :-"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdArticleMaster
        '
        Me.grdArticleMaster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdArticleMaster.Location = New System.Drawing.Point(20, 308)
        Me.grdArticleMaster.MainView = Me.grdArticleMasterV1
        Me.grdArticleMaster.Name = "grdArticleMaster"
        Me.grdArticleMaster.Size = New System.Drawing.Size(299, 87)
        Me.grdArticleMaster.TabIndex = 3
        Me.grdArticleMaster.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdArticleMasterV1})
        Me.grdArticleMaster.Visible = False
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
        'frmKHLIScanningWeightOnly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cbExit
        Me.ClientSize = New System.Drawing.Size(960, 663)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmKHLIScanningWeightOnly"
        Me.Text = "ECCO Inner Box Packing According to Outer Carton"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents grdArticleMaster As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdArticleMasterV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents tbOuterWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents tbDifference As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents tbActualWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents tbGrossWeight As System.Windows.Forms.TextBox
    Friend WithEvents tbOuterBox As System.Windows.Forms.TextBox
    Friend WithEvents tbInnerWeight As System.Windows.Forms.TextBox
    Friend WithEvents tbOuterBoxNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
