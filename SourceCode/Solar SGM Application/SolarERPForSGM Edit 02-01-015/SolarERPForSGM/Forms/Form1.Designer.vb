<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbAllinoneForm = New System.Windows.Forms.Button
        Me.cbAllMaterials = New System.Windows.Forms.Button
        Me.cbRejection = New System.Windows.Forms.Button
        Me.cbRejection2 = New System.Windows.Forms.Button
        Me.cbRejectionMainReport = New System.Windows.Forms.Button
        Me.cbOrderPlanningReport = New System.Windows.Forms.Button
        Me.cbSalesAnalysisReport = New System.Windows.Forms.Button
        Me.cbERPTracking1 = New System.Windows.Forms.Button
        Me.cbERPTracking2 = New System.Windows.Forms.Button
        Me.cbProductionEntries = New System.Windows.Forms.Button
        Me.cbPackingEntries = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.cbSpoolManageTools = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.tbIP = New System.Windows.Forms.TextBox
        Me.cbInvoiceGeneration = New System.Windows.Forms.Button
        Me.plProductionTools = New System.Windows.Forms.Panel
        Me.Button6 = New System.Windows.Forms.Button
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.cbProductStock = New System.Windows.Forms.Button
        Me.cbProductStockIH = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plProductionTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(153, 132)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(187, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(594, 38)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "SOLAR SOLES  PRIVATE LIMITED "
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(12, 150)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(292, 60)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "&1. Article Details"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(12, 222)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(292, 60)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "&2. Invoice Details"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(838, 617)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(142, 74)
        Me.cbExit.TabIndex = 10
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(12, 295)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(292, 60)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "&3. OutStanding"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(230, 675)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(323, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Version :- SGM Module 90 06-JuL-21 - Updates"
        '
        'cbAllinoneForm
        '
        Me.cbAllinoneForm.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAllinoneForm.Location = New System.Drawing.Point(12, 429)
        Me.cbAllinoneForm.Name = "cbAllinoneForm"
        Me.cbAllinoneForm.Size = New System.Drawing.Size(292, 60)
        Me.cbAllinoneForm.TabIndex = 4
        Me.cbAllinoneForm.Text = "&5. All in One Form (Beta)"
        Me.cbAllinoneForm.UseVisualStyleBackColor = True
        '
        'cbAllMaterials
        '
        Me.cbAllMaterials.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAllMaterials.Location = New System.Drawing.Point(12, 363)
        Me.cbAllMaterials.Name = "cbAllMaterials"
        Me.cbAllMaterials.Size = New System.Drawing.Size(292, 60)
        Me.cbAllMaterials.TabIndex = 3
        Me.cbAllMaterials.Text = "&4. All Materials"
        Me.cbAllMaterials.UseVisualStyleBackColor = True
        '
        'cbRejection
        '
        Me.cbRejection.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRejection.Location = New System.Drawing.Point(310, 363)
        Me.cbRejection.Name = "cbRejection"
        Me.cbRejection.Size = New System.Drawing.Size(292, 60)
        Me.cbRejection.TabIndex = 8
        Me.cbRejection.Text = "&9. Rejection With MaterialType Summary"
        Me.cbRejection.UseVisualStyleBackColor = True
        '
        'cbRejection2
        '
        Me.cbRejection2.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRejection2.Location = New System.Drawing.Point(224, 617)
        Me.cbRejection2.Name = "cbRejection2"
        Me.cbRejection2.Size = New System.Drawing.Size(71, 41)
        Me.cbRejection2.TabIndex = 10
        Me.cbRejection2.Text = "&7. Rejection [ 2 ]"
        Me.cbRejection2.UseVisualStyleBackColor = True
        Me.cbRejection2.Visible = False
        '
        'cbRejectionMainReport
        '
        Me.cbRejectionMainReport.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRejectionMainReport.Location = New System.Drawing.Point(310, 429)
        Me.cbRejectionMainReport.Name = "cbRejectionMainReport"
        Me.cbRejectionMainReport.Size = New System.Drawing.Size(292, 60)
        Me.cbRejectionMainReport.TabIndex = 9
        Me.cbRejectionMainReport.Text = "10. Rejection Main Report (Beta)"
        Me.cbRejectionMainReport.UseVisualStyleBackColor = True
        '
        'cbOrderPlanningReport
        '
        Me.cbOrderPlanningReport.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOrderPlanningReport.Location = New System.Drawing.Point(310, 295)
        Me.cbOrderPlanningReport.Name = "cbOrderPlanningReport"
        Me.cbOrderPlanningReport.Size = New System.Drawing.Size(292, 60)
        Me.cbOrderPlanningReport.TabIndex = 7
        Me.cbOrderPlanningReport.Text = "&8. Order Planning Report (Beta)"
        Me.cbOrderPlanningReport.UseVisualStyleBackColor = True
        '
        'cbSalesAnalysisReport
        '
        Me.cbSalesAnalysisReport.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSalesAnalysisReport.Location = New System.Drawing.Point(310, 150)
        Me.cbSalesAnalysisReport.Name = "cbSalesAnalysisReport"
        Me.cbSalesAnalysisReport.Size = New System.Drawing.Size(292, 60)
        Me.cbSalesAnalysisReport.TabIndex = 5
        Me.cbSalesAnalysisReport.Text = "&6. Sales Analysis Report (Beta)"
        Me.cbSalesAnalysisReport.UseVisualStyleBackColor = True
        '
        'cbERPTracking1
        '
        Me.cbERPTracking1.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbERPTracking1.Location = New System.Drawing.Point(310, 222)
        Me.cbERPTracking1.Name = "cbERPTracking1"
        Me.cbERPTracking1.Size = New System.Drawing.Size(292, 60)
        Me.cbERPTracking1.TabIndex = 6
        Me.cbERPTracking1.Text = "&7. ERP Tracking  (Beta)"
        Me.cbERPTracking1.UseVisualStyleBackColor = True
        '
        'cbERPTracking2
        '
        Me.cbERPTracking2.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbERPTracking2.Location = New System.Drawing.Point(171, 617)
        Me.cbERPTracking2.Name = "cbERPTracking2"
        Me.cbERPTracking2.Size = New System.Drawing.Size(47, 41)
        Me.cbERPTracking2.TabIndex = 15
        Me.cbERPTracking2.Text = "12. ERP Tracking [ 2 ] (Beta)"
        Me.cbERPTracking2.UseVisualStyleBackColor = True
        Me.cbERPTracking2.Visible = False
        '
        'cbProductionEntries
        '
        Me.cbProductionEntries.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProductionEntries.Location = New System.Drawing.Point(13, 12)
        Me.cbProductionEntries.Name = "cbProductionEntries"
        Me.cbProductionEntries.Size = New System.Drawing.Size(292, 60)
        Me.cbProductionEntries.TabIndex = 16
        Me.cbProductionEntries.Text = "11. Production Entries (Beta)"
        Me.cbProductionEntries.UseVisualStyleBackColor = True
        '
        'cbPackingEntries
        '
        Me.cbPackingEntries.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPackingEntries.Location = New System.Drawing.Point(13, 84)
        Me.cbPackingEntries.Name = "cbPackingEntries"
        Me.cbPackingEntries.Size = New System.Drawing.Size(292, 60)
        Me.cbPackingEntries.TabIndex = 17
        Me.cbPackingEntries.Text = "12. Packing Entries (Beta)"
        Me.cbPackingEntries.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(12, 617)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(72, 41)
        Me.Button4.TabIndex = 18
        Me.Button4.Text = "12. ERP Tracking [ 2 ] (Beta)"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'cbSpoolManageTools
        '
        Me.cbSpoolManageTools.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSpoolManageTools.Location = New System.Drawing.Point(13, 157)
        Me.cbSpoolManageTools.Name = "cbSpoolManageTools"
        Me.cbSpoolManageTools.Size = New System.Drawing.Size(292, 60)
        Me.cbSpoolManageTools.TabIndex = 19
        Me.cbSpoolManageTools.Text = "13. Spool Manage Tools (Beta)"
        Me.cbSpoolManageTools.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(93, 617)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(72, 41)
        Me.Button5.TabIndex = 20
        Me.Button5.Text = "12. ERP Tracking [ 2 ] (Beta)"
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'tbIP
        '
        Me.tbIP.BackColor = System.Drawing.Color.White
        Me.tbIP.ForeColor = System.Drawing.Color.Blue
        Me.tbIP.Location = New System.Drawing.Point(12, 671)
        Me.tbIP.Name = "tbIP"
        Me.tbIP.ReadOnly = True
        Me.tbIP.Size = New System.Drawing.Size(171, 20)
        Me.tbIP.TabIndex = 21
        '
        'cbInvoiceGeneration
        '
        Me.cbInvoiceGeneration.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbInvoiceGeneration.Location = New System.Drawing.Point(13, 228)
        Me.cbInvoiceGeneration.Name = "cbInvoiceGeneration"
        Me.cbInvoiceGeneration.Size = New System.Drawing.Size(292, 60)
        Me.cbInvoiceGeneration.TabIndex = 22
        Me.cbInvoiceGeneration.Text = "14. Invoice Generation"
        Me.cbInvoiceGeneration.UseVisualStyleBackColor = True
        '
        'plProductionTools
        '
        Me.plProductionTools.Controls.Add(Me.cbProductionEntries)
        Me.plProductionTools.Controls.Add(Me.cbInvoiceGeneration)
        Me.plProductionTools.Controls.Add(Me.cbPackingEntries)
        Me.plProductionTools.Controls.Add(Me.cbSpoolManageTools)
        Me.plProductionTools.Location = New System.Drawing.Point(608, 150)
        Me.plProductionTools.Name = "plProductionTools"
        Me.plProductionTools.Size = New System.Drawing.Size(315, 293)
        Me.plProductionTools.TabIndex = 23
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(393, 571)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(170, 73)
        Me.Button6.TabIndex = 24
        Me.Button6.Text = "Full Shoe Excess Pruduction Updates"
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'PrintDocument1
        '
        '
        'cbProductStock
        '
        Me.cbProductStock.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProductStock.Location = New System.Drawing.Point(621, 449)
        Me.cbProductStock.Name = "cbProductStock"
        Me.cbProductStock.Size = New System.Drawing.Size(292, 60)
        Me.cbProductStock.TabIndex = 25
        Me.cbProductStock.Text = "15. Product Stock"
        Me.cbProductStock.UseVisualStyleBackColor = True
        '
        'cbProductStockIH
        '
        Me.cbProductStockIH.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProductStockIH.Location = New System.Drawing.Point(621, 512)
        Me.cbProductStockIH.Name = "cbProductStockIH"
        Me.cbProductStockIH.Size = New System.Drawing.Size(292, 60)
        Me.cbProductStockIH.TabIndex = 26
        Me.cbProductStockIH.Text = "16. Product Stock {IH View}"
        Me.cbProductStockIH.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 703)
        Me.ControlBox = False
        Me.Controls.Add(Me.cbProductStockIH)
        Me.Controls.Add(Me.cbProductStock)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.plProductionTools)
        Me.Controls.Add(Me.tbIP)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.cbERPTracking2)
        Me.Controls.Add(Me.cbERPTracking1)
        Me.Controls.Add(Me.cbSalesAnalysisReport)
        Me.Controls.Add(Me.cbOrderPlanningReport)
        Me.Controls.Add(Me.cbRejectionMainReport)
        Me.Controls.Add(Me.cbRejection2)
        Me.Controls.Add(Me.cbRejection)
        Me.Controls.Add(Me.cbAllMaterials)
        Me.Controls.Add(Me.cbAllinoneForm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.cbExit)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plProductionTools.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbAllinoneForm As System.Windows.Forms.Button
    Friend WithEvents cbAllMaterials As System.Windows.Forms.Button
    Friend WithEvents cbRejection As System.Windows.Forms.Button
    Friend WithEvents cbRejection2 As System.Windows.Forms.Button
    Friend WithEvents cbRejectionMainReport As System.Windows.Forms.Button
    Friend WithEvents cbOrderPlanningReport As System.Windows.Forms.Button
    Friend WithEvents cbSalesAnalysisReport As System.Windows.Forms.Button
    Friend WithEvents cbERPTracking1 As System.Windows.Forms.Button
    Friend WithEvents cbERPTracking2 As System.Windows.Forms.Button
    Friend WithEvents cbProductionEntries As System.Windows.Forms.Button
    Friend WithEvents cbPackingEntries As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents cbSpoolManageTools As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents tbIP As System.Windows.Forms.TextBox
    Friend WithEvents cbInvoiceGeneration As System.Windows.Forms.Button
    Friend WithEvents plProductionTools As System.Windows.Forms.Panel
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents cbProductStock As System.Windows.Forms.Button
    Friend WithEvents cbProductStockIH As System.Windows.Forms.Button

End Class
