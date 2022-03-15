Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'AHGroup_SSPLDataSet.AbbrevTable' table. You can move, or remove it, as needed.
        Me.AbbrevTableTableAdapter.Fill(Me.AHGroup_SSPLDataSet.AbbrevTable)

    End Sub
End Class
