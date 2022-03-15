<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class xtrafrmforStatusAgainstSalesOrder
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cbLoadDetails = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cbLoadDetails
        '
        Me.cbLoadDetails.Location = New System.Drawing.Point(30, 151)
        Me.cbLoadDetails.Name = "cbLoadDetails"
        Me.cbLoadDetails.Size = New System.Drawing.Size(75, 23)
        Me.cbLoadDetails.TabIndex = 0
        Me.cbLoadDetails.Text = "Load Details"
        Me.cbLoadDetails.UseVisualStyleBackColor = True
        '
        'xtrafrmforStatusAgainstSalesOrder
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 428)
        Me.Controls.Add(Me.cbLoadDetails)
        Me.Name = "xtrafrmforStatusAgainstSalesOrder"
        Me.Text = "xtrafrmforStatusAgainstSalesOrder"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbLoadDetails As System.Windows.Forms.Button
End Class
