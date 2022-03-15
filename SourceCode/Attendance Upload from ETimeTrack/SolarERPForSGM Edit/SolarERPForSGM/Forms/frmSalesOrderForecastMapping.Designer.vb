<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesOrderForecastMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesOrderForecastMapping))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cbxForecastOrder = New System.Windows.Forms.ComboBox
        Me.chkbxLoadForecastORderNo = New System.Windows.Forms.CheckBox
        Me.chkbxLoadCustomer = New System.Windows.Forms.CheckBox
        Me.cbxSalesOrder = New System.Windows.Forms.ComboBox
        Me.cbxCustomer = New System.Windows.Forms.ComboBox
        Me.cbxSeason = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkbxLoadSalesORderNo = New System.Windows.Forms.CheckBox
        Me.cbUpdate = New System.Windows.Forms.Button
        Me.cbExit = New System.Windows.Forms.Button
        Me.cbReferesh = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.cbTmp2MaterislIssues4Adjustments = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbxForecastOrder)
        Me.GroupBox1.Controls.Add(Me.chkbxLoadForecastORderNo)
        Me.GroupBox1.Controls.Add(Me.chkbxLoadCustomer)
        Me.GroupBox1.Controls.Add(Me.cbxSalesOrder)
        Me.GroupBox1.Controls.Add(Me.cbxCustomer)
        Me.GroupBox1.Controls.Add(Me.cbxSeason)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkbxLoadSalesORderNo)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(460, 171)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sales Order Info :-"
        '
        'cbxForecastOrder
        '
        Me.cbxForecastOrder.FormattingEnabled = True
        Me.cbxForecastOrder.Location = New System.Drawing.Point(227, 110)
        Me.cbxForecastOrder.Name = "cbxForecastOrder"
        Me.cbxForecastOrder.Size = New System.Drawing.Size(227, 24)
        Me.cbxForecastOrder.TabIndex = 9
        '
        'chkbxLoadForecastORderNo
        '
        Me.chkbxLoadForecastORderNo.AutoSize = True
        Me.chkbxLoadForecastORderNo.Location = New System.Drawing.Point(7, 110)
        Me.chkbxLoadForecastORderNo.Name = "chkbxLoadForecastORderNo"
        Me.chkbxLoadForecastORderNo.Size = New System.Drawing.Size(214, 20)
        Me.chkbxLoadForecastORderNo.TabIndex = 8
        Me.chkbxLoadForecastORderNo.Text = "Load Forecast Order Nos. :-"
        Me.chkbxLoadForecastORderNo.UseVisualStyleBackColor = True
        '
        'chkbxLoadCustomer
        '
        Me.chkbxLoadCustomer.AutoSize = True
        Me.chkbxLoadCustomer.Location = New System.Drawing.Point(7, 54)
        Me.chkbxLoadCustomer.Name = "chkbxLoadCustomer"
        Me.chkbxLoadCustomer.Size = New System.Drawing.Size(143, 20)
        Me.chkbxLoadCustomer.TabIndex = 6
        Me.chkbxLoadCustomer.Text = "Load Customer :-"
        Me.chkbxLoadCustomer.UseVisualStyleBackColor = True
        '
        'cbxSalesOrder
        '
        Me.cbxSalesOrder.FormattingEnabled = True
        Me.cbxSalesOrder.Location = New System.Drawing.Point(203, 80)
        Me.cbxSalesOrder.Name = "cbxSalesOrder"
        Me.cbxSalesOrder.Size = New System.Drawing.Size(250, 24)
        Me.cbxSalesOrder.TabIndex = 5
        '
        'cbxCustomer
        '
        Me.cbxCustomer.FormattingEnabled = True
        Me.cbxCustomer.Location = New System.Drawing.Point(203, 52)
        Me.cbxCustomer.Name = "cbxCustomer"
        Me.cbxCustomer.Size = New System.Drawing.Size(250, 24)
        Me.cbxCustomer.TabIndex = 3
        '
        'cbxSeason
        '
        Me.cbxSeason.FormattingEnabled = True
        Me.cbxSeason.Location = New System.Drawing.Point(203, 24)
        Me.cbxSeason.Name = "cbxSeason"
        Me.cbxSeason.Size = New System.Drawing.Size(250, 24)
        Me.cbxSeason.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(7, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(191, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Season :-"
        '
        'chkbxLoadSalesORderNo
        '
        Me.chkbxLoadSalesORderNo.AutoSize = True
        Me.chkbxLoadSalesORderNo.Location = New System.Drawing.Point(7, 82)
        Me.chkbxLoadSalesORderNo.Name = "chkbxLoadSalesORderNo"
        Me.chkbxLoadSalesORderNo.Size = New System.Drawing.Size(191, 20)
        Me.chkbxLoadSalesORderNo.TabIndex = 7
        Me.chkbxLoadSalesORderNo.Text = "Load Sales Order Nos. :-"
        Me.chkbxLoadSalesORderNo.UseVisualStyleBackColor = True
        '
        'cbUpdate
        '
        Me.cbUpdate.Location = New System.Drawing.Point(12, 189)
        Me.cbUpdate.Name = "cbUpdate"
        Me.cbUpdate.Size = New System.Drawing.Size(460, 40)
        Me.cbUpdate.TabIndex = 1
        Me.cbUpdate.Text = "Update Demand Based on Forecast of the Selected Order"
        Me.cbUpdate.UseVisualStyleBackColor = True
        '
        'cbExit
        '
        Me.cbExit.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbExit.Image = CType(resources.GetObject("cbExit.Image"), System.Drawing.Image)
        Me.cbExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbExit.Location = New System.Drawing.Point(1039, 501)
        Me.cbExit.Name = "cbExit"
        Me.cbExit.Size = New System.Drawing.Size(117, 74)
        Me.cbExit.TabIndex = 4
        Me.cbExit.Text = "E&xit"
        Me.cbExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbExit.UseVisualStyleBackColor = True
        '
        'cbReferesh
        '
        Me.cbReferesh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbReferesh.Image = CType(resources.GetObject("cbReferesh.Image"), System.Drawing.Image)
        Me.cbReferesh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbReferesh.Location = New System.Drawing.Point(215, 501)
        Me.cbReferesh.Name = "cbReferesh"
        Me.cbReferesh.Size = New System.Drawing.Size(128, 74)
        Me.cbReferesh.TabIndex = 3
        Me.cbReferesh.Text = "Refresh"
        Me.cbReferesh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbReferesh.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(538, 109)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 74)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Imported Stock Information Verification"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cbTmp2MaterislIssues4Adjustments
        '
        Me.cbTmp2MaterislIssues4Adjustments.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTmp2MaterislIssues4Adjustments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cbTmp2MaterislIssues4Adjustments.Location = New System.Drawing.Point(12, 245)
        Me.cbTmp2MaterislIssues4Adjustments.Name = "cbTmp2MaterislIssues4Adjustments"
        Me.cbTmp2MaterislIssues4Adjustments.Size = New System.Drawing.Size(460, 74)
        Me.cbTmp2MaterislIssues4Adjustments.TabIndex = 6
        Me.cbTmp2MaterislIssues4Adjustments.Text = "Transfer of Stock from tmptable to MaterialIssues4Adjustments"
        Me.cbTmp2MaterislIssues4Adjustments.UseVisualStyleBackColor = True
        '
        'frmSalesOrderForecastMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1363, 587)
        Me.Controls.Add(Me.cbTmp2MaterislIssues4Adjustments)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cbExit)
        Me.Controls.Add(Me.cbReferesh)
        Me.Controls.Add(Me.cbUpdate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSalesOrderForecastMapping"
        Me.Text = "frmSalesOrderForecastMapping"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbxSeason As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkbxLoadSalesORderNo As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxLoadCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents cbxSalesOrder As System.Windows.Forms.ComboBox
    Friend WithEvents cbxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cbUpdate As System.Windows.Forms.Button
    Friend WithEvents cbxForecastOrder As System.Windows.Forms.ComboBox
    Friend WithEvents chkbxLoadForecastORderNo As System.Windows.Forms.CheckBox
    Friend WithEvents cbExit As System.Windows.Forms.Button
    Friend WithEvents cbReferesh As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cbTmp2MaterislIssues4Adjustments As System.Windows.Forms.Button
End Class
