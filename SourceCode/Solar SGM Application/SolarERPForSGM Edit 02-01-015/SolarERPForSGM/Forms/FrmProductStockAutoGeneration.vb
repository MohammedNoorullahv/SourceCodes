Public Class FrmProductStockAutoGeneration

    Dim myccProductStock As New ccProductStock

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
    End Sub

    Dim nCount As Integer = 0
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Format(Date.Now, "HH") = "23" And Format(Date.Now, "mm") = "00" Then
            If nCount = 0 Then
                InsertProductStock()
            End If
            nCount = nCount + 1
        End If

        If Format(Date.Now, "HH") = "03" And Format(Date.Now, "mm") = "00" Then
            nCount = 0
        End If
    End Sub
    Private Sub InsertProductStock()
        myccProductStock.InsertProductStock()
        myccProductStock.UpdateStockDaysCount()
    End Sub
End Class