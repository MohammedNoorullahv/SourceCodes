<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSCPackingStatus
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
        Me.cbPackingStatus = New System.Windows.Forms.Button
        Me.tbLinkNo = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'cbPackingStatus
        '
        Me.cbPackingStatus.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPackingStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPackingStatus.Location = New System.Drawing.Point(257, 13)
        Me.cbPackingStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cbPackingStatus.Name = "cbPackingStatus"
        Me.cbPackingStatus.Size = New System.Drawing.Size(138, 30)
        Me.cbPackingStatus.TabIndex = 51
        Me.cbPackingStatus.Text = "PackingStatus"
        Me.cbPackingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPackingStatus.UseVisualStyleBackColor = True
        '
        'tbLinkNo
        '
        Me.tbLinkNo.BackColor = System.Drawing.Color.White
        Me.tbLinkNo.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbLinkNo.Location = New System.Drawing.Point(12, 12)
        Me.tbLinkNo.Name = "tbLinkNo"
        Me.tbLinkNo.Size = New System.Drawing.Size(238, 32)
        Me.tbLinkNo.TabIndex = 50
        Me.tbLinkNo.TabStop = False
        Me.tbLinkNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmSCPackingStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 462)
        Me.Controls.Add(Me.cbPackingStatus)
        Me.Controls.Add(Me.tbLinkNo)
        Me.Name = "frmSCPackingStatus"
        Me.Text = "frmSCPackingStatus"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbPackingStatus As System.Windows.Forms.Button
    Friend WithEvents tbLinkNo As System.Windows.Forms.TextBox
End Class
