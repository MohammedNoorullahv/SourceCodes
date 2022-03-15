<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSave
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
        Me.plSave = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.tbSpoolNo = New System.Windows.Forms.TextBox
        Me.lbScannedBoxes = New System.Windows.Forms.ListBox
        Me.cbExitSave = New System.Windows.Forms.Button
        Me.cbSaveBack = New System.Windows.Forms.Button
        Me.cbSaveAll = New System.Windows.Forms.Button
        Me.tbTotalQty = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.tbTotalCartons = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.plwrongBoxStatus = New System.Windows.Forms.Panel
        Me.tbWrongBox = New System.Windows.Forms.TextBox
        Me.cbPrint = New System.Windows.Forms.Button
        Me.lbWrongBox = New System.Windows.Forms.ListBox
        Me.cbExitSplDtls = New System.Windows.Forms.Button
        Me.tbCorrectBox = New System.Windows.Forms.TextBox
        Me.tbTotalCartonScanned = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbBackSplDtls = New System.Windows.Forms.Button
        Me.plSave.SuspendLayout()
        Me.plwrongBoxStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'plSave
        '
        Me.plSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.plSave.Controls.Add(Me.Label1)
        Me.plSave.Controls.Add(Me.tbSpoolNo)
        Me.plSave.Controls.Add(Me.lbScannedBoxes)
        Me.plSave.Controls.Add(Me.cbExitSave)
        Me.plSave.Controls.Add(Me.cbSaveBack)
        Me.plSave.Controls.Add(Me.cbSaveAll)
        Me.plSave.Controls.Add(Me.tbTotalQty)
        Me.plSave.Controls.Add(Me.Label11)
        Me.plSave.Controls.Add(Me.tbTotalCartons)
        Me.plSave.Controls.Add(Me.Label12)
        Me.plSave.Controls.Add(Me.Label10)
        Me.plSave.Location = New System.Drawing.Point(-4, -4)
        Me.plSave.Name = "plSave"
        Me.plSave.Size = New System.Drawing.Size(234, 260)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 23)
        Me.Label1.Text = "Spool No. :-"
        '
        'tbSpoolNo
        '
        Me.tbSpoolNo.Location = New System.Drawing.Point(94, 177)
        Me.tbSpoolNo.Name = "tbSpoolNo"
        Me.tbSpoolNo.Size = New System.Drawing.Size(129, 23)
        Me.tbSpoolNo.TabIndex = 31
        '
        'lbScannedBoxes
        '
        Me.lbScannedBoxes.Location = New System.Drawing.Point(4, 95)
        Me.lbScannedBoxes.Name = "lbScannedBoxes"
        Me.lbScannedBoxes.Size = New System.Drawing.Size(224, 82)
        Me.lbScannedBoxes.TabIndex = 27
        '
        'cbExitSave
        '
        Me.cbExitSave.Location = New System.Drawing.Point(153, 232)
        Me.cbExitSave.Name = "cbExitSave"
        Me.cbExitSave.Size = New System.Drawing.Size(75, 23)
        Me.cbExitSave.TabIndex = 26
        Me.cbExitSave.Text = "Exit"
        '
        'cbSaveBack
        '
        Me.cbSaveBack.Location = New System.Drawing.Point(153, 206)
        Me.cbSaveBack.Name = "cbSaveBack"
        Me.cbSaveBack.Size = New System.Drawing.Size(75, 23)
        Me.cbSaveBack.TabIndex = 24
        Me.cbSaveBack.Text = "Back"
        '
        'cbSaveAll
        '
        Me.cbSaveAll.Location = New System.Drawing.Point(4, 232)
        Me.cbSaveAll.Name = "cbSaveAll"
        Me.cbSaveAll.Size = New System.Drawing.Size(75, 23)
        Me.cbSaveAll.TabIndex = 23
        Me.cbSaveAll.Text = "Save"
        '
        'tbTotalQty
        '
        Me.tbTotalQty.Location = New System.Drawing.Point(94, 66)
        Me.tbTotalQty.Name = "tbTotalQty"
        Me.tbTotalQty.Size = New System.Drawing.Size(129, 23)
        Me.tbTotalQty.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(-1, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 23)
        Me.Label11.Text = "Total Qty :-"
        '
        'tbTotalCartons
        '
        Me.tbTotalCartons.Location = New System.Drawing.Point(94, 37)
        Me.tbTotalCartons.Name = "tbTotalCartons"
        Me.tbTotalCartons.Size = New System.Drawing.Size(129, 23)
        Me.tbTotalCartons.TabIndex = 6
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(-1, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(103, 23)
        Me.Label12.Text = "Total Cartons :-"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(1, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(230, 25)
        Me.Label10.Text = "SAVE SELECTION"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'plwrongBoxStatus
        '
        Me.plwrongBoxStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plwrongBoxStatus.Controls.Add(Me.tbWrongBox)
        Me.plwrongBoxStatus.Controls.Add(Me.cbPrint)
        Me.plwrongBoxStatus.Controls.Add(Me.lbWrongBox)
        Me.plwrongBoxStatus.Controls.Add(Me.cbExitSplDtls)
        Me.plwrongBoxStatus.Controls.Add(Me.tbCorrectBox)
        Me.plwrongBoxStatus.Controls.Add(Me.tbTotalCartonScanned)
        Me.plwrongBoxStatus.Controls.Add(Me.Label4)
        Me.plwrongBoxStatus.Controls.Add(Me.Label5)
        Me.plwrongBoxStatus.Controls.Add(Me.cbBackSplDtls)
        Me.plwrongBoxStatus.Location = New System.Drawing.Point(0, 0)
        Me.plwrongBoxStatus.Name = "plwrongBoxStatus"
        Me.plwrongBoxStatus.Size = New System.Drawing.Size(234, 260)
        Me.plwrongBoxStatus.Visible = False
        '
        'tbWrongBox
        '
        Me.tbWrongBox.BackColor = System.Drawing.Color.Red
        Me.tbWrongBox.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.tbWrongBox.ForeColor = System.Drawing.Color.White
        Me.tbWrongBox.Location = New System.Drawing.Point(94, 63)
        Me.tbWrongBox.Name = "tbWrongBox"
        Me.tbWrongBox.Size = New System.Drawing.Size(80, 23)
        Me.tbWrongBox.TabIndex = 36
        Me.tbWrongBox.Text = "20"
        '
        'cbPrint
        '
        Me.cbPrint.Location = New System.Drawing.Point(80, 232)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(75, 23)
        Me.cbPrint.TabIndex = 35
        Me.cbPrint.Text = "Print"
        '
        'lbWrongBox
        '
        Me.lbWrongBox.Location = New System.Drawing.Point(4, 95)
        Me.lbWrongBox.Name = "lbWrongBox"
        Me.lbWrongBox.Size = New System.Drawing.Size(224, 130)
        Me.lbWrongBox.TabIndex = 27
        '
        'cbExitSplDtls
        '
        Me.cbExitSplDtls.Location = New System.Drawing.Point(153, 232)
        Me.cbExitSplDtls.Name = "cbExitSplDtls"
        Me.cbExitSplDtls.Size = New System.Drawing.Size(75, 23)
        Me.cbExitSplDtls.TabIndex = 26
        Me.cbExitSplDtls.Text = "Exit"
        '
        'tbCorrectBox
        '
        Me.tbCorrectBox.BackColor = System.Drawing.Color.Lime
        Me.tbCorrectBox.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.tbCorrectBox.ForeColor = System.Drawing.Color.Maroon
        Me.tbCorrectBox.Location = New System.Drawing.Point(4, 63)
        Me.tbCorrectBox.Name = "tbCorrectBox"
        Me.tbCorrectBox.Size = New System.Drawing.Size(80, 23)
        Me.tbCorrectBox.TabIndex = 8
        Me.tbCorrectBox.Text = "20"
        '
        'tbTotalCartonScanned
        '
        Me.tbTotalCartonScanned.Location = New System.Drawing.Point(94, 37)
        Me.tbTotalCartonScanned.Name = "tbTotalCartonScanned"
        Me.tbTotalCartonScanned.Size = New System.Drawing.Size(129, 23)
        Me.tbTotalCartonScanned.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(-1, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 23)
        Me.Label4.Text = "Total Cartons :-"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(230, 25)
        Me.Label5.Text = "SAVE SELECTION"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbBackSplDtls
        '
        Me.cbBackSplDtls.Location = New System.Drawing.Point(4, 232)
        Me.cbBackSplDtls.Name = "cbBackSplDtls"
        Me.cbBackSplDtls.Size = New System.Drawing.Size(75, 23)
        Me.cbBackSplDtls.TabIndex = 24
        Me.cbBackSplDtls.Text = "Back"
        '
        'frmSave
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(232, 269)
        Me.Controls.Add(Me.plSave)
        Me.Controls.Add(Me.plwrongBoxStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Menu = Me.mainMenu1
        Me.Name = "frmSave"
        Me.Text = "frmSave"
        Me.plSave.ResumeLayout(False)
        Me.plwrongBoxStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents plSave As System.Windows.Forms.Panel
    Friend WithEvents lbScannedBoxes As System.Windows.Forms.ListBox
    Friend WithEvents cbExitSave As System.Windows.Forms.Button
    Friend WithEvents cbSaveBack As System.Windows.Forms.Button
    Friend WithEvents cbSaveAll As System.Windows.Forms.Button
    Friend WithEvents tbTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tbTotalCartons As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbSpoolNo As System.Windows.Forms.TextBox
    Friend WithEvents plwrongBoxStatus As System.Windows.Forms.Panel
    Friend WithEvents lbWrongBox As System.Windows.Forms.ListBox
    Friend WithEvents cbExitSplDtls As System.Windows.Forms.Button
    Friend WithEvents tbCorrectBox As System.Windows.Forms.TextBox
    Friend WithEvents tbTotalCartonScanned As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbBackSplDtls As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents tbWrongBox As System.Windows.Forms.TextBox
End Class
