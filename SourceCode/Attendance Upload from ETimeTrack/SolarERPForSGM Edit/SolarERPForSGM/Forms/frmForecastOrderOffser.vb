Imports System.Data.SqlClient

Public Class frmForecastOrderOffser
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)


    Dim sForecastOrderNo, sMaterialCode, sSize, sID As String
    Dim nPOQty, nBalQty, nDemandQty As Decimal
    Private Sub cbUpdateStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUpdateStatus.Click
Aa:
        Dim daSelFPOM As New SqlDataAdapter("Select * from ForecastPOMaterials Where ForecastOrderNo in (Select Distinct ForecastOrderNo from SalesOrderDetails Where OrderNo = '" & Trim(tbOrderNo.Text) & _
                                            "') Order by ForecastOrderNo,materialCode,MaterialSize", sConstr)
        Dim dsSelFPOM As New DataSet
        daSelFPOM.Fill(dsSelFPOM)

        If dsSelFPOM.Tables(0).Rows.Count = 0 Then
            Dim daSelFCPOQty As New SqlDataAdapter("Select SalesOrderNo,MaterialCode,MaterialSize,Sum(Quantity) As POQty From PurchaseOrderDetails Where SalesOrderNo in (Select Distinct ForecastOrderNo from SalesOrderDetails Where OrderNo = '" & Trim(tbOrderNo.Text) & _
                                            "') Group By SalesOrderNo,MaterialCode,MaterialSize Order By SalesOrderNo,MaterialCode,MaterialSize", sConstr)
            Dim dsSelFCPOQty As New DataSet
            daSelFCPOQty.Fill(dsSelFCPOQty)

            Dim k As Integer = 0
            For k = 0 To dsSelFCPOQty.Tables(0).Rows.Count - 1
                sForecastOrderNo = dsSelFCPOQty.Tables(0).Rows(k).Item("SalesOrderNo").ToString()
                sMaterialCode = dsSelFCPOQty.Tables(0).Rows(k).Item("MaterialCode").ToString()
                sSize = dsSelFCPOQty.Tables(0).Rows(k).Item("MaterialSize").ToString()
                nPOQty = Val(dsSelFCPOQty.Tables(0).Rows(k).Item("POQty").ToString())

                sID = System.Guid.NewGuid.ToString()

                InsertIntoFPOM()
            Next
            GoTo Aa
        End If

        Dim i As Integer = 0
        For i = 0 To dsSelFPOM.Tables(0).Rows.Count - 1
            sMaterialCode = dsSelFPOM.Tables(0).Rows(i).Item("MaterialCode").ToString()
            sSize = dsSelFPOM.Tables(0).Rows(i).Item("MaterialSize").ToString()
            nPOQty = Val(dsSelFPOM.Tables(0).Rows(i).Item("BalPOQty").ToString())
            nBalQty = Val(dsSelFPOM.Tables(0).Rows(i).Item("BalPOQty").ToString())
            If nPOQty > 0 Then
                Dim daSelDemand As New SqlDataAdapter("Select * from Demand Where OrderNo = '" & Trim(tbOrderNo.Text) & _
                                                      "' And MaterialCode = '" & sMaterialCode & "' And Size = '" & sSize & _
                                                      "' Order By SalesorderNo", sConstr)
                Dim dsSelDemand As New DataSet
                daSelDemand.Fill(dsSelDemand)

                Dim j As Integer = 0
                For j = 0 To dsSelDemand.Tables(0).Rows.Count - 1
                    sID = dsSelDemand.Tables(0).Rows(j).Item("Id").ToString()
                    nDemandQty = Val(dsSelDemand.Tables(0).Rows(j).Item("Quantity").ToString())

                    If nBalQty <= nDemandQty Then
                        nPOQty = nBalQty
                        nBalQty = nBalQty - nPOQty
                    Else
                        nPOQty = nDemandQty
                        nBalQty = nBalQty - nPOQty
                    End If

                    Dim daUpdDemandQty As New SqlDataAdapter("Update Demand Set ForecastPOQty = '" & nPOQty & _
                                                             "' Where Id = '" & sID & "'", sConstr)
                    Dim dsUpdDemandQty As New DataSet
                    daUpdDemandQty.Fill(dsUpdDemandQty)
                    dsUpdDemandQty.AcceptChanges()
                    If nBalQty <= 0 Then
                        Exit For
                    End If
                Next
            End If
        Next
        MessageBox.Show("Completed")
    End Sub

    Private Sub InsertIntoFPOM()
        Dim daInsFPOM As New SqlDataAdapter("Insert Into ForecastPOMaterials(ID, ForecastOrderNo, MaterialCode, MaterialSize, POQty, IndAdjustedQty, BalPOQty) values ('" & sID & _
                                            "','" & sForecastOrderNo & "','" & sMaterialCode & "','" & sSize & "','" & nPOQty & _
                                            "','0','" & nPOQty & "')", sConstr)
        Dim dsInsFPOM As New DataSet
        daInsFPOM.Fill(dsInsFPOM)
        dsInsFPOM.AcceptChanges()
    End Sub
End Class