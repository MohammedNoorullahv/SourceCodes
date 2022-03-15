<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Me.crVeiwer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.cbQuit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'crVeiwer
        '
        Me.crVeiwer.ActiveViewIndex = -1
        Me.crVeiwer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crVeiwer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crVeiwer.Location = New System.Drawing.Point(0, 0)
        Me.crVeiwer.Name = "crVeiwer"
        Me.crVeiwer.SelectionFormula = ""
        Me.crVeiwer.Size = New System.Drawing.Size(1170, 454)
        Me.crVeiwer.TabIndex = 0
        Me.crVeiwer.ViewTimeSelectionFormula = ""
        '
        'cbQuit
        '
        Me.cbQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cbQuit.Location = New System.Drawing.Point(8, 8)
        Me.cbQuit.Name = "cbQuit"
        Me.cbQuit.Size = New System.Drawing.Size(75, 23)
        Me.cbQuit.TabIndex = 1
        Me.cbQuit.Text = "Button1"
        Me.cbQuit.UseVisualStyleBackColor = True
        '
        'frmReport
        '
        Me.CancelButton = Me.cbQuit
        Me.ClientSize = New System.Drawing.Size(1170, 454)
        Me.Controls.Add(Me.crVeiwer)
        Me.Controls.Add(Me.cbQuit)
        Me.Name = "frmReport"
        Me.Text = "frmReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rptViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents crVeiwer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cbQuit As System.Windows.Forms.Button
End Class
