<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmScanning
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
        Me.plScanning = New System.Windows.Forms.Panel
        Me.cbManualSave = New System.Windows.Forms.Button
        Me.tbSize = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tbLastBarcode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.chkbxNoBoxAdd = New System.Windows.Forms.CheckBox
        Me.chkbxManualSave = New System.Windows.Forms.CheckBox
        Me.cbExitBarcodeScanning = New System.Windows.Forms.Button
        Me.cbCancel = New System.Windows.Forms.Button
        Me.cbBack = New System.Windows.Forms.Button
        Me.cbSaveBarcode = New System.Windows.Forms.Button
        Me.tbQuantity = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.tbBarcode = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblScanning = New System.Windows.Forms.Label
        Me.plScanning.SuspendLayout()
        Me.SuspendLayout()
        '
        'plScanning
        '
        Me.plScanning.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plScanning.Controls.Add(Me.cbManualSave)
        Me.plScanning.Controls.Add(Me.tbSize)
        Me.plScanning.Controls.Add(Me.Label7)
        Me.plScanning.Controls.Add(Me.tbLastBarcode)
        Me.plScanning.Controls.Add(Me.Label5)
        Me.plScanning.Controls.Add(Me.chkbxNoBoxAdd)
        Me.plScanning.Controls.Add(Me.chkbxManualSave)
        Me.plScanning.Controls.Add(Me.cbExitBarcodeScanning)
        Me.plScanning.Controls.Add(Me.cbCancel)
        Me.plScanning.Controls.Add(Me.cbBack)
        Me.plScanning.Controls.Add(Me.cbSaveBarcode)
        Me.plScanning.Controls.Add(Me.tbQuantity)
        Me.plScanning.Controls.Add(Me.Label8)
        Me.plScanning.Controls.Add(Me.tbBarcode)
        Me.plScanning.Controls.Add(Me.Label9)
        Me.plScanning.Controls.Add(Me.lblScanning)
        Me.plScanning.Location = New System.Drawing.Point(-1, -4)
        Me.plScanning.Name = "plScanning"
        Me.plScanning.Size = New System.Drawing.Size(234, 260)
        '
        'cbManualSave
        '
        Me.cbManualSave.Location = New System.Drawing.Point(4, 206)
        Me.cbManualSave.Name = "cbManualSave"
        Me.cbManualSave.Size = New System.Drawing.Size(147, 23)
        Me.cbManualSave.TabIndex = 29
        Me.cbManualSave.Text = "Manual Save"
        '
        'tbSize
        '
        Me.tbSize.Location = New System.Drawing.Point(6, 146)
        Me.tbSize.Name = "tbSize"
        Me.tbSize.Size = New System.Drawing.Size(84, 23)
        Me.tbSize.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(4, 126)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 23)
        Me.Label7.Text = "Size :-"
        '
        'tbLastBarcode
        '
        Me.tbLastBarcode.Location = New System.Drawing.Point(4, 100)
        Me.tbLastBarcode.Name = "tbLastBarcode"
        Me.tbLastBarcode.Size = New System.Drawing.Size(223, 23)
        Me.tbLastBarcode.TabIndex = 26
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(4, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 23)
        Me.Label5.Text = "LAST BARCODE :-"
        '
        'chkbxNoBoxAdd
        '
        Me.chkbxNoBoxAdd.Location = New System.Drawing.Point(7, 180)
        Me.chkbxNoBoxAdd.Name = "chkbxNoBoxAdd"
        Me.chkbxNoBoxAdd.Size = New System.Drawing.Size(90, 20)
        Me.chkbxNoBoxAdd.TabIndex = 24
        Me.chkbxNoBoxAdd.Text = "No Box Add"
        '
        'chkbxManualSave
        '
        Me.chkbxManualSave.Location = New System.Drawing.Point(134, 181)
        Me.chkbxManualSave.Name = "chkbxManualSave"
        Me.chkbxManualSave.Size = New System.Drawing.Size(93, 20)
        Me.chkbxManualSave.TabIndex = 23
        Me.chkbxManualSave.Text = "Manual Save"
        '
        'cbExitBarcodeScanning
        '
        Me.cbExitBarcodeScanning.Location = New System.Drawing.Point(154, 232)
        Me.cbExitBarcodeScanning.Name = "cbExitBarcodeScanning"
        Me.cbExitBarcodeScanning.Size = New System.Drawing.Size(75, 23)
        Me.cbExitBarcodeScanning.TabIndex = 22
        Me.cbExitBarcodeScanning.Text = "Exit"
        '
        'cbCancel
        '
        Me.cbCancel.Location = New System.Drawing.Point(82, 232)
        Me.cbCancel.Name = "cbCancel"
        Me.cbCancel.Size = New System.Drawing.Size(69, 23)
        Me.cbCancel.TabIndex = 21
        Me.cbCancel.Text = "Cancel"
        '
        'cbBack
        '
        Me.cbBack.Location = New System.Drawing.Point(154, 206)
        Me.cbBack.Name = "cbBack"
        Me.cbBack.Size = New System.Drawing.Size(75, 23)
        Me.cbBack.TabIndex = 20
        Me.cbBack.Text = "Back"
        '
        'cbSaveBarcode
        '
        Me.cbSaveBarcode.Location = New System.Drawing.Point(4, 232)
        Me.cbSaveBarcode.Name = "cbSaveBarcode"
        Me.cbSaveBarcode.Size = New System.Drawing.Size(75, 23)
        Me.cbSaveBarcode.TabIndex = 19
        Me.cbSaveBarcode.Text = "Save"
        '
        'tbQuantity
        '
        Me.tbQuantity.Location = New System.Drawing.Point(143, 144)
        Me.tbQuantity.Name = "tbQuantity"
        Me.tbQuantity.Size = New System.Drawing.Size(84, 23)
        Me.tbQuantity.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(141, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 23)
        Me.Label8.Text = "Quantity :-"
        '
        'tbBarcode
        '
        Me.tbBarcode.Location = New System.Drawing.Point(4, 52)
        Me.tbBarcode.Name = "tbBarcode"
        Me.tbBarcode.Size = New System.Drawing.Size(223, 23)
        Me.tbBarcode.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(4, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 23)
        Me.Label9.Text = "BARCODE :-"
        '
        'lblScanning
        '
        Me.lblScanning.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblScanning.ForeColor = System.Drawing.Color.Red
        Me.lblScanning.Location = New System.Drawing.Point(1, 1)
        Me.lblScanning.Name = "lblScanning"
        Me.lblScanning.Size = New System.Drawing.Size(230, 25)
        Me.lblScanning.Text = "BARCODE SCANNING"
        Me.lblScanning.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmScanning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(232, 269)
        Me.Controls.Add(Me.plScanning)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Menu = Me.mainMenu1
        Me.Name = "frmScanning"
        Me.Text = "frmScanning"
        Me.plScanning.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents plScanning As System.Windows.Forms.Panel
    Friend WithEvents cbManualSave As System.Windows.Forms.Button
    Friend WithEvents tbSize As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbLastBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkbxNoBoxAdd As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxManualSave As System.Windows.Forms.CheckBox
    Friend WithEvents cbExitBarcodeScanning As System.Windows.Forms.Button
    Friend WithEvents cbCancel As System.Windows.Forms.Button
    Friend WithEvents cbBack As System.Windows.Forms.Button
    Friend WithEvents cbSaveBarcode As System.Windows.Forms.Button
    Friend WithEvents tbQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblScanning As System.Windows.Forms.Label
End Class
