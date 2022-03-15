<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmForecastOrderOffser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmForecastOrderOffser))
        Me.cbExit = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbOrderNo = New System.Windows.Forms.TextBox
        Me.cbUpdateStatus = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cbExit
        '
        Me.cbExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(155, 176)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(117, 74)
        Me.cbExit.TabIndex = 3
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 20)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Order No. :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbOrderNo
        '
        Me.tbOrderNo.Location = New System.Drawing.Point(90, 8)
        Me.tbOrderNo.Name = "tbOrderNo"
        Me.tbOrderNo.Size = New System.Drawing.Size(100, 20)
        Me.tbOrderNo.TabIndex = 21
        Me.tbOrderNo.Text = "S-F-SS21-014"
        '
        'cbUpdateStatus
        '
        Me.cbUpdateStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUpdateStatus.Image = CType(resources.GetObject("cbUpdateStatus.Image"), System.Drawing.Image)
        Me.cbUpdateStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbUpdateStatus.Location = New System.Drawing.Point(12, 173)
        Me.cbUpdateStatus.Name = "cbUpdateStatus"
        Me.cbUpdateStatus.Size = New System.Drawing.Size(126, 77)
        Me.cbUpdateStatus.TabIndex = 23
        Me.cbUpdateStatus.Text = "Generte"
        Me.cbUpdateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbUpdateStatus.UseVisualStyleBackColor = True
        '
        'frmForecastOrderOffser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.cbUpdateStatus)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbOrderNo)
        Me.Controls.Add(Me.cbExit)
        Me.Name = "frmForecastOrderOffser"
        Me.Text = "frmForecastOrderOffser"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents cbUpdateStatus As System.Windows.Forms.Button
End Class
