<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
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
    private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.AbbrevTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.Button1 = New System.Windows.Forms.Button
        Me.AHGroup_SSPLDataSet = New SmartDeviceProject3inVB.AHGroup_SSPLDataSet
        Me.AbbrevTableTableAdapter = New SmartDeviceProject3inVB.AHGroup_SSPLDataSetTableAdapters.AbbrevTableTableAdapter
        CType(Me.AbbrevTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AHGroup_SSPLDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AbbrevTableBindingSource
        '
        Me.AbbrevTableBindingSource.DataMember = "AbbrevTable"
        Me.AbbrevTableBindingSource.DataSource = Me.AHGroup_SSPLDataSet
        '
        'DataGrid1
        '
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DataGrid1.DataSource = Me.AbbrevTableBindingSource
        Me.DataGrid1.Location = New System.Drawing.Point(3, 3)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(226, 195)
        Me.DataGrid1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 204)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 20)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        '
        'AHGroup_SSPLDataSet
        '
        Me.AHGroup_SSPLDataSet.DataSetName = "AHGroup_SSPLDataSet"
        Me.AHGroup_SSPLDataSet.Prefix = ""
        Me.AHGroup_SSPLDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'AbbrevTableTableAdapter
        '
        Me.AbbrevTableTableAdapter.ClearBeforeFill = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(232, 235)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGrid1)
        Me.Menu = Me.mainMenu1
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.AbbrevTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AHGroup_SSPLDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents AHGroup_SSPLDataSet As SmartDeviceProject3inVB.AHGroup_SSPLDataSet
    Friend WithEvents AbbrevTableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AbbrevTableTableAdapter As SmartDeviceProject3inVB.AHGroup_SSPLDataSetTableAdapters.AbbrevTableTableAdapter

End Class
