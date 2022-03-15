<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBulkProductionEntries
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBulkProductionEntries))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pgbar = New System.Windows.Forms.ProgressBar
        Me.pl = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbMouldCompleted = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tbMouldEndDate = New System.Windows.Forms.TextBox
        Me.tbMouldPending = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.tbPackEndDate = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.tbFinishingEndDate = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.tbPackPending = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.tbPackCompleted = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.tbPackStartDate = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.tbFinishingOutQty = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.tbWIPQty = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.tbFinishingPending = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.tbFinishingCompleted = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.tbFinishingStartDate = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.tbJobcardQty = New System.Windows.Forms.TextBox
        Me.tbMouldStartDate = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.tbStationNo = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.tbMachineNo = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.tbSection = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.tbShift = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.tbJobcardNo = New System.Windows.Forms.TextBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbLastScannedBarcode = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.tbBarcode = New System.Windows.Forms.TextBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.dpFrom = New System.Windows.Forms.DateTimePicker
        Me.cbPrint = New System.Windows.Forms.Button
        Me.Export2Excel = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.grdArticleMaster = New DevExpress.XtraGrid.GridControl
        Me.grdArticleMasterV1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Panel1.SuspendLayout()
        Me.pl.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.pgbar)
        Me.Panel1.Controls.Add(Me.pl)
        Me.Panel1.Controls.Add(Me.cbPrint)
        Me.Panel1.Controls.Add(Me.Export2Excel)
        Me.Panel1.Controls.Add(Me.cbExit)
        Me.Panel1.Controls.Add(Me.cbReferesh)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1203, 735)
        Me.Panel1.TabIndex = 0
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(401, 628)
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(663, 51)
        Me.pgbar.TabIndex = 15
        Me.pgbar.Visible = False
        '
        'pl
        '
        Me.pl.Controls.Add(Me.Panel6)
        Me.pl.Controls.Add(Me.Panel9)
        Me.pl.Controls.Add(Me.Panel8)
        Me.pl.Controls.Add(Me.Panel2)
        Me.pl.Location = New System.Drawing.Point(5, 5)
        Me.pl.Margin = New System.Windows.Forms.Padding(4)
        Me.pl.Name = "pl"
        Me.pl.Size = New System.Drawing.Size(1198, 615)
        Me.pl.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Controls.Add(Me.tbMouldCompleted)
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Controls.Add(Me.tbMouldEndDate)
        Me.Panel6.Controls.Add(Me.tbMouldPending)
        Me.Panel6.Controls.Add(Me.Label19)
        Me.Panel6.Controls.Add(Me.tbPackEndDate)
        Me.Panel6.Controls.Add(Me.Label17)
        Me.Panel6.Controls.Add(Me.tbFinishingEndDate)
        Me.Panel6.Controls.Add(Me.Label18)
        Me.Panel6.Controls.Add(Me.tbPackPending)
        Me.Panel6.Controls.Add(Me.Label14)
        Me.Panel6.Controls.Add(Me.tbPackCompleted)
        Me.Panel6.Controls.Add(Me.Label15)
        Me.Panel6.Controls.Add(Me.tbPackStartDate)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Controls.Add(Me.tbFinishingOutQty)
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Controls.Add(Me.tbWIPQty)
        Me.Panel6.Controls.Add(Me.Label13)
        Me.Panel6.Controls.Add(Me.tbFinishingPending)
        Me.Panel6.Controls.Add(Me.Label8)
        Me.Panel6.Controls.Add(Me.tbFinishingCompleted)
        Me.Panel6.Controls.Add(Me.Label9)
        Me.Panel6.Controls.Add(Me.tbFinishingStartDate)
        Me.Panel6.Controls.Add(Me.Label10)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.tbJobcardQty)
        Me.Panel6.Controls.Add(Me.tbMouldStartDate)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Location = New System.Drawing.Point(7, 157)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1184, 126)
        Me.Panel6.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Location = New System.Drawing.Point(208, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(141, 23)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "Mould Completed :-"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbMouldCompleted
        '
        Me.tbMouldCompleted.BackColor = System.Drawing.Color.White
        Me.tbMouldCompleted.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbMouldCompleted.Location = New System.Drawing.Point(353, 33)
        Me.tbMouldCompleted.Name = "tbMouldCompleted"
        Me.tbMouldCompleted.ReadOnly = True
        Me.tbMouldCompleted.Size = New System.Drawing.Size(88, 23)
        Me.tbMouldCompleted.TabIndex = 66
        Me.tbMouldCompleted.TabStop = False
        Me.tbMouldCompleted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Location = New System.Drawing.Point(208, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 23)
        Me.Label7.TabIndex = 67
        Me.Label7.Text = "Mould Pending :-"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbMouldEndDate
        '
        Me.tbMouldEndDate.BackColor = System.Drawing.Color.White
        Me.tbMouldEndDate.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbMouldEndDate.Location = New System.Drawing.Point(353, 87)
        Me.tbMouldEndDate.Name = "tbMouldEndDate"
        Me.tbMouldEndDate.ReadOnly = True
        Me.tbMouldEndDate.Size = New System.Drawing.Size(88, 23)
        Me.tbMouldEndDate.TabIndex = 70
        Me.tbMouldEndDate.TabStop = False
        Me.tbMouldEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbMouldPending
        '
        Me.tbMouldPending.BackColor = System.Drawing.Color.White
        Me.tbMouldPending.ForeColor = System.Drawing.Color.DarkMagenta
        Me.tbMouldPending.Location = New System.Drawing.Point(353, 60)
        Me.tbMouldPending.Name = "tbMouldPending"
        Me.tbMouldPending.ReadOnly = True
        Me.tbMouldPending.Size = New System.Drawing.Size(88, 23)
        Me.tbMouldPending.TabIndex = 68
        Me.tbMouldPending.TabStop = False
        Me.tbMouldPending.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label19.Location = New System.Drawing.Point(208, 87)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(141, 23)
        Me.Label19.TabIndex = 69
        Me.Label19.Text = "Mould End Date :-"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbPackEndDate
        '
        Me.tbPackEndDate.BackColor = System.Drawing.Color.White
        Me.tbPackEndDate.Location = New System.Drawing.Point(830, 87)
        Me.tbPackEndDate.Name = "tbPackEndDate"
        Me.tbPackEndDate.ReadOnly = True
        Me.tbPackEndDate.Size = New System.Drawing.Size(88, 23)
        Me.tbPackEndDate.TabIndex = 64
        Me.tbPackEndDate.TabStop = False
        '
        'Label17
        '
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label17.Location = New System.Drawing.Point(685, 87)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(141, 23)
        Me.Label17.TabIndex = 63
        Me.Label17.Text = "Pack End Date :-"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbFinishingEndDate
        '
        Me.tbFinishingEndDate.BackColor = System.Drawing.Color.White
        Me.tbFinishingEndDate.Location = New System.Drawing.Point(592, 87)
        Me.tbFinishingEndDate.Name = "tbFinishingEndDate"
        Me.tbFinishingEndDate.ReadOnly = True
        Me.tbFinishingEndDate.Size = New System.Drawing.Size(88, 23)
        Me.tbFinishingEndDate.TabIndex = 62
        Me.tbFinishingEndDate.TabStop = False
        '
        'Label18
        '
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label18.Location = New System.Drawing.Point(447, 87)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(141, 23)
        Me.Label18.TabIndex = 61
        Me.Label18.Text = "Fin' End Date :-"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbPackPending
        '
        Me.tbPackPending.BackColor = System.Drawing.Color.White
        Me.tbPackPending.Location = New System.Drawing.Point(830, 60)
        Me.tbPackPending.Name = "tbPackPending"
        Me.tbPackPending.ReadOnly = True
        Me.tbPackPending.Size = New System.Drawing.Size(88, 23)
        Me.tbPackPending.TabIndex = 58
        Me.tbPackPending.TabStop = False
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.Location = New System.Drawing.Point(685, 60)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(141, 23)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Pack Pending :-"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbPackCompleted
        '
        Me.tbPackCompleted.BackColor = System.Drawing.Color.White
        Me.tbPackCompleted.Location = New System.Drawing.Point(830, 33)
        Me.tbPackCompleted.Name = "tbPackCompleted"
        Me.tbPackCompleted.ReadOnly = True
        Me.tbPackCompleted.Size = New System.Drawing.Size(88, 23)
        Me.tbPackCompleted.TabIndex = 56
        Me.tbPackCompleted.TabStop = False
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label15.Location = New System.Drawing.Point(685, 33)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(141, 23)
        Me.Label15.TabIndex = 55
        Me.Label15.Text = "Pack Completed :-"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbPackStartDate
        '
        Me.tbPackStartDate.BackColor = System.Drawing.Color.White
        Me.tbPackStartDate.Location = New System.Drawing.Point(830, 4)
        Me.tbPackStartDate.Name = "tbPackStartDate"
        Me.tbPackStartDate.ReadOnly = True
        Me.tbPackStartDate.Size = New System.Drawing.Size(88, 23)
        Me.tbPackStartDate.TabIndex = 54
        Me.tbPackStartDate.TabStop = False
        '
        'Label16
        '
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label16.Location = New System.Drawing.Point(685, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(141, 23)
        Me.Label16.TabIndex = 53
        Me.Label16.Text = "Pack Start Date :-"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbFinishingOutQty
        '
        Me.tbFinishingOutQty.BackColor = System.Drawing.Color.White
        Me.tbFinishingOutQty.Location = New System.Drawing.Point(592, 87)
        Me.tbFinishingOutQty.Name = "tbFinishingOutQty"
        Me.tbFinishingOutQty.ReadOnly = True
        Me.tbFinishingOutQty.Size = New System.Drawing.Size(88, 23)
        Me.tbFinishingOutQty.TabIndex = 52
        Me.tbFinishingOutQty.TabStop = False
        Me.tbFinishingOutQty.Visible = False
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.Location = New System.Drawing.Point(447, 87)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(141, 23)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Fin' Out Qty :-"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label12.Visible = False
        '
        'tbWIPQty
        '
        Me.tbWIPQty.BackColor = System.Drawing.Color.White
        Me.tbWIPQty.Location = New System.Drawing.Point(592, 87)
        Me.tbWIPQty.Name = "tbWIPQty"
        Me.tbWIPQty.ReadOnly = True
        Me.tbWIPQty.Size = New System.Drawing.Size(88, 23)
        Me.tbWIPQty.TabIndex = 50
        Me.tbWIPQty.TabStop = False
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Location = New System.Drawing.Point(447, 87)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(141, 23)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = " W I P Quantity:-"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbFinishingPending
        '
        Me.tbFinishingPending.BackColor = System.Drawing.Color.White
        Me.tbFinishingPending.Location = New System.Drawing.Point(592, 60)
        Me.tbFinishingPending.Name = "tbFinishingPending"
        Me.tbFinishingPending.ReadOnly = True
        Me.tbFinishingPending.Size = New System.Drawing.Size(88, 23)
        Me.tbFinishingPending.TabIndex = 48
        Me.tbFinishingPending.TabStop = False
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Location = New System.Drawing.Point(447, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(141, 23)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Fin' Pending :-"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbFinishingCompleted
        '
        Me.tbFinishingCompleted.BackColor = System.Drawing.Color.White
        Me.tbFinishingCompleted.Location = New System.Drawing.Point(592, 33)
        Me.tbFinishingCompleted.Name = "tbFinishingCompleted"
        Me.tbFinishingCompleted.ReadOnly = True
        Me.tbFinishingCompleted.Size = New System.Drawing.Size(88, 23)
        Me.tbFinishingCompleted.TabIndex = 46
        Me.tbFinishingCompleted.TabStop = False
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Location = New System.Drawing.Point(447, 33)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(141, 23)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "Fin' Completed :-"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbFinishingStartDate
        '
        Me.tbFinishingStartDate.BackColor = System.Drawing.Color.White
        Me.tbFinishingStartDate.Location = New System.Drawing.Point(592, 4)
        Me.tbFinishingStartDate.Name = "tbFinishingStartDate"
        Me.tbFinishingStartDate.ReadOnly = True
        Me.tbFinishingStartDate.Size = New System.Drawing.Size(88, 23)
        Me.tbFinishingStartDate.TabIndex = 44
        Me.tbFinishingStartDate.TabStop = False
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Location = New System.Drawing.Point(447, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(141, 23)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Fin' Start Date :-"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(4, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(200, 23)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Jobcard Quantity :-"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbJobcardQty
        '
        Me.tbJobcardQty.BackColor = System.Drawing.Color.White
        Me.tbJobcardQty.Font = New System.Drawing.Font("Verdana", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbJobcardQty.Location = New System.Drawing.Point(7, 33)
        Me.tbJobcardQty.Name = "tbJobcardQty"
        Me.tbJobcardQty.ReadOnly = True
        Me.tbJobcardQty.Size = New System.Drawing.Size(200, 85)
        Me.tbJobcardQty.TabIndex = 37
        Me.tbJobcardQty.TabStop = False
        '
        'tbMouldStartDate
        '
        Me.tbMouldStartDate.BackColor = System.Drawing.Color.White
        Me.tbMouldStartDate.Location = New System.Drawing.Point(353, 4)
        Me.tbMouldStartDate.Name = "tbMouldStartDate"
        Me.tbMouldStartDate.ReadOnly = True
        Me.tbMouldStartDate.Size = New System.Drawing.Size(88, 23)
        Me.tbMouldStartDate.TabIndex = 36
        Me.tbMouldStartDate.TabStop = False
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Location = New System.Drawing.Point(208, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 23)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Mould Start Date :-"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Controls.Add(Me.grdArticleMaster)
        Me.Panel9.Location = New System.Drawing.Point(7, 287)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(526, 322)
        Me.Panel9.TabIndex = 17
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Controls.Add(Me.tbStationNo)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.tbMachineNo)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.tbSection)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.tbShift)
        Me.Panel8.Controls.Add(Me.Label34)
        Me.Panel8.Location = New System.Drawing.Point(7, 62)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1184, 85)
        Me.Panel8.TabIndex = 16
        '
        'tbStationNo
        '
        Me.tbStationNo.BackColor = System.Drawing.Color.White
        Me.tbStationNo.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbStationNo.Location = New System.Drawing.Point(872, 39)
        Me.tbStationNo.Name = "tbStationNo"
        Me.tbStationNo.ReadOnly = True
        Me.tbStationNo.Size = New System.Drawing.Size(285, 32)
        Me.tbStationNo.TabIndex = 42
        Me.tbStationNo.TabStop = False
        Me.tbStationNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label21
        '
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(872, 6)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(285, 30)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "STATION NO. :-"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbMachineNo
        '
        Me.tbMachineNo.BackColor = System.Drawing.Color.White
        Me.tbMachineNo.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMachineNo.Location = New System.Drawing.Point(583, 39)
        Me.tbMachineNo.Name = "tbMachineNo"
        Me.tbMachineNo.ReadOnly = True
        Me.tbMachineNo.Size = New System.Drawing.Size(285, 32)
        Me.tbMachineNo.TabIndex = 40
        Me.tbMachineNo.TabStop = False
        Me.tbMachineNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(583, 5)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(285, 30)
        Me.Label22.TabIndex = 39
        Me.Label22.Text = "MACHINE / CONVEYOUR NO. :-"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbSection
        '
        Me.tbSection.BackColor = System.Drawing.Color.White
        Me.tbSection.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbSection.Location = New System.Drawing.Point(294, 39)
        Me.tbSection.Name = "tbSection"
        Me.tbSection.ReadOnly = True
        Me.tbSection.Size = New System.Drawing.Size(285, 32)
        Me.tbSection.TabIndex = 38
        Me.tbSection.TabStop = False
        Me.tbSection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(294, 5)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(285, 30)
        Me.Label20.TabIndex = 37
        Me.Label20.Text = "SECTION :-"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbShift
        '
        Me.tbShift.BackColor = System.Drawing.Color.White
        Me.tbShift.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbShift.Location = New System.Drawing.Point(5, 39)
        Me.tbShift.Name = "tbShift"
        Me.tbShift.ReadOnly = True
        Me.tbShift.Size = New System.Drawing.Size(285, 32)
        Me.tbShift.TabIndex = 36
        Me.tbShift.TabStop = False
        Me.tbShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label34
        '
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(5, 5)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(285, 30)
        Me.Label34.TabIndex = 35
        Me.Label34.Text = "SHIFT :-"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(7, 7)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1184, 49)
        Me.Panel2.TabIndex = 12
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Honeydew
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Controls.Add(Me.tbJobcardNo)
        Me.Panel7.Location = New System.Drawing.Point(881, -1)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(297, 40)
        Me.Panel7.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 23)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "JC. No :-"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbJobcardNo
        '
        Me.tbJobcardNo.BackColor = System.Drawing.Color.White
        Me.tbJobcardNo.Location = New System.Drawing.Point(89, 8)
        Me.tbJobcardNo.Name = "tbJobcardNo"
        Me.tbJobcardNo.Size = New System.Drawing.Size(200, 23)
        Me.tbJobcardNo.TabIndex = 24
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Honeydew
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.tbLastScannedBarcode)
        Me.Panel5.Location = New System.Drawing.Point(529, -1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(348, 40)
        Me.Panel5.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(174, 23)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Last Scanned Barcode :-"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbLastScannedBarcode
        '
        Me.tbLastScannedBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbLastScannedBarcode.Location = New System.Drawing.Point(184, 8)
        Me.tbLastScannedBarcode.Name = "tbLastScannedBarcode"
        Me.tbLastScannedBarcode.Size = New System.Drawing.Size(159, 23)
        Me.tbLastScannedBarcode.TabIndex = 35
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Honeydew
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.tbBarcode)
        Me.Panel4.Location = New System.Drawing.Point(228, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(297, 40)
        Me.Panel4.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(4, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 23)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Barcode :-"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbBarcode
        '
        Me.tbBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tbBarcode.Location = New System.Drawing.Point(89, 8)
        Me.tbBarcode.Name = "tbBarcode"
        Me.tbBarcode.Size = New System.Drawing.Size(200, 23)
        Me.tbBarcode.TabIndex = 24
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Ivory
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.dpFrom)
        Me.Panel3.Location = New System.Drawing.Point(-1, -1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(225, 40)
        Me.Panel3.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date :-"
        '
        'dpFrom
        '
        Me.dpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpFrom.Location = New System.Drawing.Point(75, 8)
        Me.dpFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.dpFrom.Name = "dpFrom"
        Me.dpFrom.Size = New System.Drawing.Size(139, 23)
        Me.dpFrom.TabIndex = 2
        Me.dpFrom.TabStop = False
        '
        'cbPrint
        '
        Me.cbPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbPrint.Location = New System.Drawing.Point(266, 628)
        Me.cbPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(128, 74)
        Me.cbPrint.TabIndex = 7
        Me.cbPrint.Text = "Print"
        Me.cbPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbPrint.UseVisualStyleBackColor = True
        Me.cbPrint.Visible = False
        '
        'Export2Excel
        '
        Me.Export2Excel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export2Excel.Image = CType(resources.GetObject("Export2Excel.Image"), System.Drawing.Image)
        Me.Export2Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Export2Excel.Location = New System.Drawing.Point(135, 628)
        Me.Export2Excel.Margin = New System.Windows.Forms.Padding(4)
        Me.Export2Excel.Name = "Export2Excel"
        Me.Export2Excel.Size = New System.Drawing.Size(128, 74)
        Me.Export2Excel.TabIndex = 4
        Me.Export2Excel.Text = "Expot" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to   " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Excel"
        Me.Export2Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export2Excel.UseVisualStyleBackColor = True
        Me.Export2Excel.Visible = False
        '
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(1071, 628)
        Me.cbExit.Margin = New System.Windows.Forms.Padding(4)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(128, 74)
        Me.cbExit.TabIndex = 2
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbReferesh
        '
        Me.cbReferesh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReferesh.Image = CType(resources.GetObject("cbReferesh.Image"), System.Drawing.Image)
        Me.cbReferesh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbReferesh.Location = New System.Drawing.Point(4, 628)
        Me.cbReferesh.Margin = New System.Windows.Forms.Padding(4)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 1
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'grdArticleMaster
        '
        Me.grdArticleMaster.Location = New System.Drawing.Point(8, 9)
        Me.grdArticleMaster.MainView = Me.grdArticleMasterV1
        Me.grdArticleMaster.Name = "grdArticleMaster"
        Me.grdArticleMaster.Size = New System.Drawing.Size(510, 308)
        Me.grdArticleMaster.TabIndex = 4
        Me.grdArticleMaster.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdArticleMasterV1})
        '
        'grdArticleMasterV1
        '
        Me.grdArticleMasterV1.GridControl = Me.grdArticleMaster
        Me.grdArticleMasterV1.Name = "grdArticleMasterV1"
        Me.grdArticleMasterV1.OptionsView.ColumnAutoWidth = False
        Me.grdArticleMasterV1.OptionsView.ShowAutoFilterRow = True
        Me.grdArticleMasterV1.OptionsView.ShowFooter = True
        '
        'frmBulkProductionEntries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 712)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBulkProductionEntries"
        Me.Text = "frmOrderPlanning"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.pl.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grdArticleMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdArticleMasterV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pl As System.Windows.Forms.Panel
    Friend WithEvents dpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Export2Excel As System.Windows.Forms.Button
    Friend WithEvents cbPrint As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents tbBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents tbMouldStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbJobcardQty As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbJobcardNo As System.Windows.Forms.TextBox
    Friend WithEvents tbFinishingPending As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbFinishingCompleted As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbFinishingStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tbFinishingOutQty As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tbWIPQty As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tbPackPending As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents tbPackCompleted As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tbPackStartDate As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tbPackEndDate As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tbFinishingEndDate As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents tbStationNo As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents tbMachineNo As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tbSection As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents tbShift As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbLastScannedBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tbMouldCompleted As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tbMouldEndDate As System.Windows.Forms.TextBox
    Friend WithEvents tbMouldPending As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents grdArticleMaster As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdArticleMasterV1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
