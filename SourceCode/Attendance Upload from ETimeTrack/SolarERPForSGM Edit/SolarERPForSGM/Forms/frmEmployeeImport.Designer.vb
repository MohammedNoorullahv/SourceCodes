<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeImport
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
        Me.cbImport = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cbImport
        '
        Me.cbImport.Location = New System.Drawing.Point(80, 163)
        Me.cbImport.Name = "cbImport"
        Me.cbImport.Size = New System.Drawing.Size(257, 94)
        Me.cbImport.TabIndex = 0
        Me.cbImport.Text = "Import / Update Data"
        Me.cbImport.UseVisualStyleBackColor = True
        '
        'frmEmployeeImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 396)
        Me.Controls.Add(Me.cbImport)
        Me.Name = "frmEmployeeImport"
        Me.Text = "frmEmployeeImport"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbImport As System.Windows.Forms.Button
End Class
