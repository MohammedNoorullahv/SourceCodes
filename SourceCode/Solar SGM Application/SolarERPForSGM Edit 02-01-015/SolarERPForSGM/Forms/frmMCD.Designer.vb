<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMCD
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
        Me.plLogin = New System.Windows.Forms.Panel
        Me.Button1 = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbLogin = New System.Windows.Forms.Button
        Me.tbPassword = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tbUserName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.plSave = New System.Windows.Forms.Panel
        Me.lbScannedBoxes = New System.Windows.Forms.ListBox
        Me.cbExitSave = New System.Windows.Forms.Button
        Me.cbSaveBack = New System.Windows.Forms.Button
        Me.cbSaveAll = New System.Windows.Forms.Button
        Me.tbTotalQty = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.tbTotalCartons = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.plLogin.SuspendLayout()
        Me.plSelection.SuspendLayout()
        Me.plScanning.SuspendLayout()
        Me.plSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'plLogin
        '
        Me.plLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plLogin.Controls.Add(Me.Button1)
        Me.plLogin.Controls.Add(Me.cbExit)
        Me.plLogin.Controls.Add(Me.cbLogin)
        Me.plLogin.Controls.Add(Me.tbPassword)
        Me.plLogin.Controls.Add(Me.Label3)
        Me.plLogin.Controls.Add(Me.tbUserName)
        Me.plLogin.Controls.Add(Me.Label2)
        Me.plLogin.Controls.Add(Me.Label1)
        Me.plLogin.Location = New System.Drawing.Point(12, 14)
        Me.plLogin.Name = "plLogin"
        Me.plLogin.Size = New System.Drawing.Size(234, 260)
        Me.plLogin.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(79, 118)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Login"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'cbExit
        '
        Me.cbExit.Location = New System.Drawing.Point(154, 232)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(75, 23)
        Me.cbExit.TabIndex = 6
        Me.cbExit.Text = "Exit"
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbLogin
        '
        Me.cbLogin.Location = New System.Drawing.Point(4, 232)
        Me.cbLogin.Name = "cbLogin"
        Me.cbLogin.Size = New System.Drawing.Size(75, 23)
        Me.cbLogin.TabIndex = 5
        Me.cbLogin.Text = "Login"
        Me.cbLogin.UseVisualStyleBackColor = True
        '
        'tbPassword
        '
        Me.tbPassword.Location = New System.Drawing.Point(86, 63)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbPassword.Size = New System.Drawing.Size(142, 23)
        Me.tbPassword.TabIndex = 4
        Me.tbPassword.Text = "9009"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(4, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 23)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password :-"
        '
        'tbUserName
        '
        Me.tbUserName.Location = New System.Drawing.Point(86, 34)
        Me.tbUserName.Name = "tbUserName"
        Me.tbUserName.Size = New System.Drawing.Size(142, 23)
        Me.tbUserName.TabIndex = 2
        Me.tbUserName.Text = "Suheb"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(230, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "LOGIN ACCESS INFO"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Name :-"
        '
        'plSelection
        '
        Me.plSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.plSelection.Enabled = False
        Me.plSelection.Location = New System.Drawing.Point(252, 14)
        Me.plSelection.Name = "plSelection"
        Me.plSelection.Size = New System.Drawing.Size(234, 260)
        Me.plSelection.TabIndex = 1
        '
        'cbxMachine
        '
        Me.cbxMachine.FormattingEnabled = True
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
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Machine / Conv"
        '
        'cbDispatch
        '
        Me.cbDispatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbDispatch.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDispatch.Location = New System.Drawing.Point(121, 165)
        Me.cbDispatch.Name = "cbDispatch"
        Me.cbDispatch.Size = New System.Drawing.Size(108, 64)
        Me.cbDispatch.TabIndex = 15
        Me.cbDispatch.Text = "DISPATCH"
        Me.cbDispatch.UseVisualStyleBackColor = False
        '
        'cbPacking
        '
        Me.cbPacking.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbPacking.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPacking.Location = New System.Drawing.Point(7, 165)
        Me.cbPacking.Name = "cbPacking"
        Me.cbPacking.Size = New System.Drawing.Size(108, 64)
        Me.cbPacking.TabIndex = 14
        Me.cbPacking.Text = "PACKING"
        Me.cbPacking.UseVisualStyleBackColor = False
        '
        'cbFinishing
        '
        Me.cbFinishing.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cbFinishing.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFinishing.Location = New System.Drawing.Point(121, 95)
        Me.cbFinishing.Name = "cbFinishing"
        Me.cbFinishing.Size = New System.Drawing.Size(108, 64)
        Me.cbFinishing.TabIndex = 13
        Me.cbFinishing.Text = "FINISHING"
        Me.cbFinishing.UseVisualStyleBackColor = False
        '
        'cbMoulding
        '
        Me.cbMoulding.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cbMoulding.Font = New System.Drawing.Font("Segoe Condensed", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMoulding.Location = New System.Drawing.Point(7, 95)
        Me.cbMoulding.Name = "cbMoulding"
        Me.cbMoulding.Size = New System.Drawing.Size(108, 64)
        Me.cbMoulding.TabIndex = 12
        Me.cbMoulding.Text = "MOULDING"
        Me.cbMoulding.UseVisualStyleBackColor = False
        '
        'cbExitSelection
        '
        Me.cbExitSelection.Location = New System.Drawing.Point(154, 232)
        Me.cbExitSelection.Name = "cbExitSelection"
        Me.cbExitSelection.Size = New System.Drawing.Size(75, 23)
        Me.cbExitSelection.TabIndex = 11
        Me.cbExitSelection.Text = "Exit"
        Me.cbExitSelection.UseVisualStyleBackColor = True
        '
        'cbNext
        '
        Me.cbNext.Location = New System.Drawing.Point(4, 232)
        Me.cbNext.Name = "cbNext"
        Me.cbNext.Size = New System.Drawing.Size(75, 23)
        Me.cbNext.TabIndex = 10
        Me.cbNext.Text = "Next"
        Me.cbNext.UseVisualStyleBackColor = True
        '
        'cbxShift
        '
        Me.cbxShift.FormattingEnabled = True
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
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Shift :-"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(230, 25)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "SELECTION"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plScanning
        '
        Me.plScanning.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plScanning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.plScanning.Enabled = False
        Me.plScanning.Location = New System.Drawing.Point(492, 14)
        Me.plScanning.Name = "plScanning"
        Me.plScanning.Size = New System.Drawing.Size(234, 260)
        Me.plScanning.TabIndex = 2
        '
        'cbManualSave
        '
        Me.cbManualSave.Location = New System.Drawing.Point(4, 206)
        Me.cbManualSave.Name = "cbManualSave"
        Me.cbManualSave.Size = New System.Drawing.Size(147, 23)
        Me.cbManualSave.TabIndex = 29
        Me.cbManualSave.Text = "Manual Save"
        Me.cbManualSave.UseVisualStyleBackColor = True
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
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Size :-"
        '
        'tbLastBarcode
        '
        Me.tbLastBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
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
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "LAST BARCODE :-"
        '
        'chkbxNoBoxAdd
        '
        Me.chkbxNoBoxAdd.AutoSize = True
        Me.chkbxNoBoxAdd.Location = New System.Drawing.Point(7, 180)
        Me.chkbxNoBoxAdd.Name = "chkbxNoBoxAdd"
        Me.chkbxNoBoxAdd.Size = New System.Drawing.Size(90, 20)
        Me.chkbxNoBoxAdd.TabIndex = 24
        Me.chkbxNoBoxAdd.Text = "No Box Add"
        Me.chkbxNoBoxAdd.UseVisualStyleBackColor = True
        '
        'chkbxManualSave
        '
        Me.chkbxManualSave.AutoSize = True
        Me.chkbxManualSave.Location = New System.Drawing.Point(134, 181)
        Me.chkbxManualSave.Name = "chkbxManualSave"
        Me.chkbxManualSave.Size = New System.Drawing.Size(93, 20)
        Me.chkbxManualSave.TabIndex = 23
        Me.chkbxManualSave.Text = "Manual Save"
        Me.chkbxManualSave.UseVisualStyleBackColor = True
        '
        'cbExitBarcodeScanning
        '
        Me.cbExitBarcodeScanning.Location = New System.Drawing.Point(154, 232)
        Me.cbExitBarcodeScanning.Name = "cbExitBarcodeScanning"
        Me.cbExitBarcodeScanning.Size = New System.Drawing.Size(75, 23)
        Me.cbExitBarcodeScanning.TabIndex = 22
        Me.cbExitBarcodeScanning.Text = "Exit"
        Me.cbExitBarcodeScanning.UseVisualStyleBackColor = True
        '
        'cbCancel
        '
        Me.cbCancel.Location = New System.Drawing.Point(82, 232)
        Me.cbCancel.Name = "cbCancel"
        Me.cbCancel.Size = New System.Drawing.Size(69, 23)
        Me.cbCancel.TabIndex = 21
        Me.cbCancel.Text = "Cancel"
        Me.cbCancel.UseVisualStyleBackColor = True
        '
        'cbBack
        '
        Me.cbBack.Location = New System.Drawing.Point(154, 206)
        Me.cbBack.Name = "cbBack"
        Me.cbBack.Size = New System.Drawing.Size(75, 23)
        Me.cbBack.TabIndex = 20
        Me.cbBack.Text = "Back"
        Me.cbBack.UseVisualStyleBackColor = True
        '
        'cbSaveBarcode
        '
        Me.cbSaveBarcode.Location = New System.Drawing.Point(4, 232)
        Me.cbSaveBarcode.Name = "cbSaveBarcode"
        Me.cbSaveBarcode.Size = New System.Drawing.Size(75, 23)
        Me.cbSaveBarcode.TabIndex = 19
        Me.cbSaveBarcode.Text = "Save"
        Me.cbSaveBarcode.UseVisualStyleBackColor = True
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
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Quantity :-"
        '
        'tbBarcode
        '
        Me.tbBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
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
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "BARCODE :-"
        '
        'lblScanning
        '
        Me.lblScanning.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblScanning.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblScanning.ForeColor = System.Drawing.Color.Red
        Me.lblScanning.Location = New System.Drawing.Point(1, 1)
        Me.lblScanning.Name = "lblScanning"
        Me.lblScanning.Size = New System.Drawing.Size(230, 25)
        Me.lblScanning.TabIndex = 3
        Me.lblScanning.Text = "BARCODE SCANNING"
        Me.lblScanning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plSave
        '
        Me.plSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.plSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.plSave.Controls.Add(Me.lbScannedBoxes)
        Me.plSave.Controls.Add(Me.cbExitSave)
        Me.plSave.Controls.Add(Me.cbSaveBack)
        Me.plSave.Controls.Add(Me.cbSaveAll)
        Me.plSave.Controls.Add(Me.tbTotalQty)
        Me.plSave.Controls.Add(Me.Label11)
        Me.plSave.Controls.Add(Me.tbTotalCartons)
        Me.plSave.Controls.Add(Me.Label12)
        Me.plSave.Controls.Add(Me.Label10)
        Me.plSave.Enabled = False
        Me.plSave.Location = New System.Drawing.Point(732, 14)
        Me.plSave.Name = "plSave"
        Me.plSave.Size = New System.Drawing.Size(234, 260)
        Me.plSave.TabIndex = 3
        '
        'lbScannedBoxes
        '
        Me.lbScannedBoxes.FormattingEnabled = True
        Me.lbScannedBoxes.ItemHeight = 15
        Me.lbScannedBoxes.Location = New System.Drawing.Point(4, 95)
        Me.lbScannedBoxes.Name = "lbScannedBoxes"
        Me.lbScannedBoxes.Size = New System.Drawing.Size(224, 94)
        Me.lbScannedBoxes.TabIndex = 27
        '
        'cbExitSave
        '
        Me.cbExitSave.Location = New System.Drawing.Point(153, 232)
        Me.cbExitSave.Name = "cbExitSave"
        Me.cbExitSave.Size = New System.Drawing.Size(75, 23)
        Me.cbExitSave.TabIndex = 26
        Me.cbExitSave.Text = "Exit"
        Me.cbExitSave.UseVisualStyleBackColor = True
        '
        'cbSaveBack
        '
        Me.cbSaveBack.Location = New System.Drawing.Point(153, 206)
        Me.cbSaveBack.Name = "cbSaveBack"
        Me.cbSaveBack.Size = New System.Drawing.Size(75, 23)
        Me.cbSaveBack.TabIndex = 24
        Me.cbSaveBack.Text = "Back"
        Me.cbSaveBack.UseVisualStyleBackColor = True
        '
        'cbSaveAll
        '
        Me.cbSaveAll.Location = New System.Drawing.Point(4, 232)
        Me.cbSaveAll.Name = "cbSaveAll"
        Me.cbSaveAll.Size = New System.Drawing.Size(75, 23)
        Me.cbSaveAll.TabIndex = 23
        Me.cbSaveAll.Text = "Save"
        Me.cbSaveAll.UseVisualStyleBackColor = True
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
        Me.Label11.TabIndex = 7
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
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Total Cartons :-"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(1, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(230, 25)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "SAVE SELECTION"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMCD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 284)
        Me.Controls.Add(Me.plSave)
        Me.Controls.Add(Me.plScanning)
        Me.Controls.Add(Me.plSelection)
        Me.Controls.Add(Me.plLogin)
        Me.Font = New System.Drawing.Font("Segoe Condensed", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Name = "frmMCD"
        Me.Text = "frmMCD"
        Me.plLogin.ResumeLayout(False)
        Me.plLogin.PerformLayout()
        Me.plSelection.ResumeLayout(False)
        Me.plScanning.ResumeLayout(False)
        Me.plScanning.PerformLayout()
        Me.plSave.ResumeLayout(False)
        Me.plSave.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents plLogin As System.Windows.Forms.Panel
    Friend WithEvents plSelection As System.Windows.Forms.Panel
    Friend WithEvents plScanning As System.Windows.Forms.Panel
    Friend WithEvents plSave As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbLogin As System.Windows.Forms.Button
    Friend WithEvents tbPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbxShift As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbExitSelection As System.Windows.Forms.Button
    Friend WithEvents cbNext As System.Windows.Forms.Button
    Friend WithEvents tbQuantity As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblScanning As System.Windows.Forms.Label
    Friend WithEvents cbExitBarcodeScanning As System.Windows.Forms.Button
    Friend WithEvents cbCancel As System.Windows.Forms.Button
    Friend WithEvents cbBack As System.Windows.Forms.Button
    Friend WithEvents cbSaveBarcode As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbExitSave As System.Windows.Forms.Button
    Friend WithEvents cbSaveBack As System.Windows.Forms.Button
    Friend WithEvents cbSaveAll As System.Windows.Forms.Button
    Friend WithEvents tbTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tbTotalCartons As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lbScannedBoxes As System.Windows.Forms.ListBox
    Friend WithEvents cbPacking As System.Windows.Forms.Button
    Friend WithEvents cbFinishing As System.Windows.Forms.Button
    Friend WithEvents cbMoulding As System.Windows.Forms.Button
    Friend WithEvents cbDispatch As System.Windows.Forms.Button
    Friend WithEvents chkbxNoBoxAdd As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxManualSave As System.Windows.Forms.CheckBox
    Friend WithEvents tbLastBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbSize As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbxMachine As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbManualSave As System.Windows.Forms.Button
End Class
