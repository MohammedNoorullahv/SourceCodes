<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FeedingPassingSlip
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
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.tbJobcardNo = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'cbExit
        '
        Me.cbExit.Location = New System.Drawing.Point(209, 54)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(100, 28)
        Me.cbExit.TabIndex = 0
        Me.cbExit.Text = "E&xit"
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbPrint
        '
        Me.cbPrint.Location = New System.Drawing.Point(101, 54)
        Me.cbPrint.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(100, 28)
        Me.cbPrint.TabIndex = 1
        Me.cbPrint.Text = "&Print"
        Me.cbPrint.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Jobcard No"
        '
        'tbJobcardNo
        '
        Me.tbJobcardNo.Location = New System.Drawing.Point(109, 23)
        Me.tbJobcardNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbJobcardNo.Name = "tbJobcardNo"
        Me.tbJobcardNo.Size = New System.Drawing.Size(200, 23)
        Me.tbJobcardNo.TabIndex = 3
        Me.tbJobcardNo.Text = "K-F-ST17-011-01-001-01"
        '
        'FeedingPassingSlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 553)
        Me.Controls.Add(Me.tbJobcardNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbPrint)
        Me.Controls.Add(Me.cbExit)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FeedingPassingSlip"
        Me.Text = "FeedingPassingSlip"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbJobcardNo As System.Windows.Forms.TextBox
End Class
