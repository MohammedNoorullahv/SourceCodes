<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSelection
    Inherits System.Windows.Forms.Form

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
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.plSelection = New System.Windows.Forms.Panel
        Me.cbxMachine = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.cbDispatch = New System.Windows.Forms.Button
        Me.cbPacking = New System.Windows.Forms.Button
        Me.cbFinishing = New System.Windows.Forms.Button
        Me.cbMoulding = New System.Windows.Forms.Button
        Me.cbExitSelection = New System.Windows.Forms.Button
        Me.cbNext = New System.Windows.Forms.Button
        Me.cbxShift = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.plSelection.SuspendLayout()
        Me.SuspendLayout()
        '
        'plSelection
        '
        Me.plSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plSelection.Controls.Add(Me.cbxMachine)
        Me.plSelection.Controls.Add(Me.Label13)
        Me.plSelection.Controls.Add(Me.cbDispatch)
        Me.plSelection.Controls.Add(Me.cbPacking)
        Me.plSelection.Controls.Add(Me.cbFinishing)
        Me.plSelection.Controls.Add(Me.cbMoulding)
        Me.plSelection.Controls.Add(Me.cbExitSelection)
        Me.plSelection.Controls.Add(Me.cbNext)
        Me.plSelection.Controls.Add(Me.cbxShift)
        Me.plSelection.Controls.Add(Me.Label6)
        Me.plSelection.Controls.Add(Me.Label4)
        Me.plSelection.Location = New System.Drawing.Point(-3, -4)
        Me.plSelection.Name = "plSelection"
        Me.plSelection.Size = New System.Drawing.Size(234, 260)
        '
        'cbxMachine
        '
        Me.cbxMachine.Location = New System.Drawing.Point(86, 62)
        Me.cbxMachine.Name = "cbxMachine"
        Me.cbxMachine.Size = New System.Drawing.Size(142, 23)
        Me.cbxMachine.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(4, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 23)
        Me.Label13.Text = "Machine / Conv"
        '
        'cbDispatch
        '
        Me.cbDispatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbDispatch.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cbDispatch.Location = New System.Drawing.Point(121, 165)
        Me.cbDispatch.Name = "cbDispatch"
        Me.cbDispatch.Size = New System.Drawing.Size(108, 64)
        Me.cbDispatch.TabIndex = 15
        Me.cbDispatch.Text = "DISPATCH"
        '
        'cbPacking
        '
        Me.cbPacking.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbPacking.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cbPacking.Location = New System.Drawing.Point(7, 165)
        Me.cbPacking.Name = "cbPacking"
        Me.cbPacking.Size = New System.Drawing.Size(108, 64)
        Me.cbPacking.TabIndex = 14
        Me.cbPacking.Text = "PACKING"
        '
        'cbFinishing
        '
        Me.cbFinishing.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cbFinishing.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cbFinishing.Location = New System.Drawing.Point(121, 95)
        Me.cbFinishing.Name = "cbFinishing"
        Me.cbFinishing.Size = New System.Drawing.Size(108, 64)
        Me.cbFinishing.TabIndex = 13
        Me.cbFinishing.Text = "FINISHING"
        '
        'cbMoulding
        '
        Me.cbMoulding.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cbMoulding.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cbMoulding.Location = New System.Drawing.Point(7, 95)
        Me.cbMoulding.Name = "cbMoulding"
        Me.cbMoulding.Size = New System.Drawing.Size(108, 64)
        Me.cbMoulding.TabIndex = 12
        Me.cbMoulding.Text = "MOULDING"
        '
        'cbExitSelection
        '
        Me.cbExitSelection.Location = New System.Drawing.Point(154, 232)
        Me.cbExitSelection.Name = "cbExitSelection"
        Me.cbExitSelection.Size = New System.Drawing.Size(75, 23)
        Me.cbExitSelection.TabIndex = 11
        Me.cbExitSelection.Text = "Exit"
        '
        'cbNext
        '
        Me.cbNext.Location = New System.Drawing.Point(4, 232)
        Me.cbNext.Name = "cbNext"
        Me.cbNext.Size = New System.Drawing.Size(75, 23)
        Me.cbNext.TabIndex = 10
        Me.cbNext.Text = "Next"
        '
        'cbxShift
        '
        Me.cbxShift.Location = New System.Drawing.Point(86, 34)
        Me.cbxShift.Name = "cbxShift"
        Me.cbxShift.Size = New System.Drawing.Size(142, 23)
        Me.cbxShift.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 23)
        Me.Label6.Text = "Shift :-"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(230, 25)
        Me.Label4.Text = "SELECTION"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(232, 269)
        Me.Controls.Add(Me.plSelection)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Menu = Me.mainMenu1
        Me.Name = "frmSelection"
        Me.Text = "frmSelection"
        Me.plSelection.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents plSelection As System.Windows.Forms.Panel
    Friend WithEvents cbxMachine As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbDispatch As System.Windows.Forms.Button
    Friend WithEvents cbPacking As System.Windows.Forms.Button
    Friend WithEvents cbFinishing As System.Windows.Forms.Button
    Friend WithEvents cbMoulding As System.Windows.Forms.Button
    Friend WithEvents cbExitSelection As System.Windows.Forms.Button
    Friend WithEvents cbNext As System.Windows.Forms.Button
    Friend WithEvents cbxShift As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
