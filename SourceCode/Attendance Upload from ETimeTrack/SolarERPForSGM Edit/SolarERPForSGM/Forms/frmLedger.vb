Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle


'Imports PCComm

Public Class frmLedger

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard

    





    Dim nOpeningStock, nArrival, nIssue, nClosingStock, nTotalClosingStock As Decimal
    Dim nRowcount As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Try
            Dim dadelLedger As New SqlDataAdapter("Delete from TempLedgerTable Where SystemIp = '" & sIpAddress & "'", sConstr)
            Dim dsDelLedger As New DataSet
            dadelLedger.Fill(dsDelLedger)
            dsDelLedger.AcceptChanges()

            nOpeningStock = 0 : nArrival = 0 : nIssue = 0 : nClosingStock = 0

            Dim daSelOpnStk As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As OpnStk From StockByMonth Where  MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                       "' And Location = '" & Trim(tbLocation.Text) & _
                                                       "' And Size = '" & sSize & _
                                                       "' And Cast(StockDate As Date) = '" & Format((DateAdd(DateInterval.Day, -1, dpFrom.Value)), "dd-MMM-yyyy") & "'", sConstr)

            Dim dsSelOpnStk As New DataSet
            daSelOpnStk.Fill(dsSelOpnStk)

            nOpeningStock = Val(dsSelOpnStk.Tables(0).Rows(0).Item("OpnStk").ToString)

            If nOpeningStock <> 0 Then

                nClosingStock = nOpeningStock + nArrival - nIssue
                Dim daInsTransaction2 As New SqlDataAdapter("Insert Into TempLedgerTable Values ('" & sIpAddress & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                           "','" & Format(dpto.Value, "dd-MMM-yyyy") & "','" & Trim(tbLocation.Text) & _
                                                           "','" & Trim(tbMaterial.Text) & "','" & sMaterialDescription & _
                                                           "','" & Trim(tbSize.Text) & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                           "','OPENING STOCK','','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                                                           "','" & nClosingStock & "')", sConstr)
                Dim dsInsTransaction2 As New DataSet
                daInsTransaction2.Fill(dsInsTransaction2)
                dsInsTransaction2.AcceptChanges()

                nOpeningStock = nClosingStock
                nArrival = 0
                nIssue = 0
                nClosingStock = 0
            End If

            Dim daSelTransaction As New SqlDataAdapter("Select IssueDate,VoucherNo,MaterialCode,PurchaseOrderNo,TransactionType,FromLocation,ToLocation,IssueQuantity From MaterialIssues Where  MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                       "' And (FromLocation = '" & Trim(tbLocation.Text) & _
                                                       "' or ToLocation = '" & Trim(tbLocation.Text) & _
                                                       "') And Size = '" & sSize & _
                                                       "' And IssueDate >= '" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                       "' And IssueDate <= '" & Format(dpto.Value, "dd-MMM-yyyy") & _
                                                       "' And TransactionType Not in ('NEW PURCHASE','FULLSHOE PRODUCTION CONSUME','InternalMaterialTransfer','STOCK MATERIAL TRANSFER') Order by IssueDate", sConstr)
            Dim dsSelTransaction As New DataSet
            daSelTransaction.Fill(dsSelTransaction)

            nRowcount = dsSelTransaction.Tables(0).Rows.Count



            Dim i As Integer = 0

            For i = 0 To nRowcount - 1
                'If i = 0 Then
                '    nOpeningStock = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                'Else
                If dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString Then

                    If dsSelTransaction.Tables(0).Rows(i).Item("TransactionType") = "MaterialInspected" Then
                        nArrival = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                    Else
                        MsgBox("Same From & To Location")
                    End If

                ElseIf dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = Trim(tbLocation.Text) Then
                    nIssue = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                ElseIf dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString = Trim(tbLocation.Text) Then
                    nArrival = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                End If
                'End If

                nClosingStock = nOpeningStock + nArrival - nIssue
                Dim daInsTransaction As New SqlDataAdapter("Insert Into TempLedgerTable Values ('" & sIpAddress & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                           "','" & Format(dpto.Value, "dd-MMM-yyyy") & "','" & Trim(tbLocation.Text) & _
                                                           "','" & Trim(tbMaterial.Text) & "','" & sMaterialDescription & _
                                                           "','" & Trim(tbSize.Text) & "','" & Format(dsSelTransaction.Tables(0).Rows(i).Item("IssueDate"), "dd-MMM-yyyy") & _
                                                           "','" & dsSelTransaction.Tables(0).Rows(i).Item("TransactionType") & _
                                                           "','" & dsSelTransaction.Tables(0).Rows(i).Item("VoucherNo") & _
                                                           "','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                                                           "','" & nClosingStock & "')", sConstr)
                Dim dsInsTransaction As New DataSet
                daInsTransaction.Fill(dsInsTransaction)
                dsInsTransaction.AcceptChanges()

                nOpeningStock = nClosingStock
                nArrival = 0
                nIssue = 0
                nClosingStock = 0
            Next

            Dim daSelStock As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity from Stock Where MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                 "' And Stage = 'INSTK' And Location = '" & Trim(tbLocation.Text) & _
                                                 "' And Size = '" & Trim(tbSize.Text) & "'", sConstr)
            Dim dsSelStock As New DataSet
            daSelStock.Fill(dsSelStock)

            nClosingStock = dsSelStock.Tables(0).Rows(0).Item("Quantity")

            If nOpeningStock <> nClosingStock Then
                Dim daInsTransaction1 As New SqlDataAdapter("Insert Into TempLedgerTable1 Values ('" & sIpAddress & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                           "','" & Format(dpto.Value, "dd-MMM-yyyy") & "','" & Trim(tbLocation.Text) & _
                                                           "','" & Trim(tbMaterial.Text) & "','" & sMaterialDescription & _
                                                           "','" & Trim(tbSize.Text) & "','" & Format(Date.Now, "dd-MMM-yyyy") & "','','','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                                                           "','" & nClosingStock & "')", sConstr)
                Dim dsInsTransaction1 As New DataSet
                daInsTransaction1.Fill(dsInsTransaction1)
                dsInsTransaction1.AcceptChanges()
            End If

            Me.TempLedgerTableTableAdapter.Fill(Me.DsLedger.TempLedgerTable, sIpAddress, Trim(tbMaterial.Text))
            MsgBox("Completed")

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
        'Me.Hide()
        'Form1.Show()
    End Sub

    Dim ngrdRowCount As Integer
    Dim sIpAddress As String
    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DsLedger.TempLedgerTable' table. You can move, or remove it, as needed.

        'TODO: This line of code loads data into the 'DsLocation.Location' table. You can move, or remove it, as needed.

        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)

        sIpAddress = h.AddressList.GetValue(0).ToString

        Me.TempLedgerTableTableAdapter.Fill(Me.DsLedger.TempLedgerTable, sIpAddress, Trim(tbMaterial.Text))
    End Sub

    

    Dim keyascii As Integer
    
    
  
  




    Private Sub Loadgrdinfo()
        ''Try

        
        ''grdLedger.DataSource = myccKHLIOutstandingWithJobcard.LoadPackedInfo(0)

        With grdLedgerV1

            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1
            .Columns(4).VisibleIndex = -1
            .Columns(5).VisibleIndex = -1
            '.Columns(6).VisibleIndex = -1
            '.Columns(7).VisibleIndex = -1
            .Columns(9).VisibleIndex = -1
            .Columns(10).VisibleIndex = -1
            .Columns(14).VisibleIndex = -1
            .Columns(15).VisibleIndex = -1
            .Columns(16).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1
            .Columns(18).VisibleIndex = -1
            .Columns(19).VisibleIndex = -1


            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

        End With


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub





   
    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

   
    Private Sub cbLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLocation.Click
        grdLocation.Visible = True
        grdLocation.BringToFront()
        tbLocation.Clear()
        sLocation = ""
        Me.LocationTableAdapter.Fill(Me.DsLocation.Location)

    End Sub

    Dim ngrdRowNo As Integer
    Dim sLocation, sMaterialCode, sMaterialDescription, sSize As String
    Private Sub grdLocationV1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLocationV1.DoubleClick
        Try
            ngrdRowNo = grdLocationV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Location Available", MsgBoxStyle.Information)
                Exit Sub
            Else
                sLocation = grdLocationV1.GetDataRow(ngrdRowNo).Item("LocationCode")
                tbLocation.Text = sLocation
                grdLocation.Visible = False
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.Click
        grdMaterials.Visible = True
        grdMaterials.BringToFront()
        tbMaterial.Clear()
        sMaterialCode = ""
        Me.MaterialsTableAdapter.Fill(Me.DsMaterials.Materials, Trim(tbLocation.Text))
    End Sub

    Private Sub grdMaterialsV1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMaterialsV1.DoubleClick
        Try
            ngrdRowNo = grdMaterialsV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Materials Available", MsgBoxStyle.Information)
                Exit Sub
            Else
                sMaterialCode = grdMaterialsV1.GetDataRow(ngrdRowNo).Item("MaterialCode")
                sMaterialDescription = grdMaterialsV1.GetDataRow(ngrdRowNo).Item("Description")
                tbMaterial.Text = sMaterialCode
                grdMaterials.Visible = False
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSize.Click
        grdSize.Visible = True
        grdSize.BringToFront()
        tbSize.Clear()
        sSize = ""
        Me.MaterialIssuesTableAdapter.Fill(Me.DsSize.MaterialIssues, Trim(tbMaterial.Text))
    End Sub

    Private Sub grdSizeV1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSizeV1.DoubleClick
        Try
            ngrdRowNo = grdSizeV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Size Available", MsgBoxStyle.Information)
                Exit Sub
            Else
                sSize = grdSizeV1.GetDataRow(ngrdRowNo).Item("Size").ToString
                tbSize.Text = sSize
                grdSize.Visible = False
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

   
    Private Sub cbExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExporttoExcel.Click
        grdLedger.ExportToXls("D:\Ledger.xls")
        MsgBox("Export Completed")
    End Sub

    Dim sTransactionNo As String
    
    Private Sub cbAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAll.Click
        Try
            Dim dadelLedger As New SqlDataAdapter("Delete from TempLedgerTable1 Where SystemIp = '" & sIpAddress & "'", sConstr)
            Dim dsDelLedger As New DataSet
            dadelLedger.Fill(dsDelLedger)
            dsDelLedger.AcceptChanges()

            Dim daSelTotalStock As New SqlDataAdapter("Select Location,MaterialCode,MaterialSize As Size,Sum(Quantity) As Quantity from Stock Where Stage = 'INSTK' And Quantity > 0 And Location <> '' Group by Location,MaterialCode,MaterialSize Order by Location,MaterialCode,MaterialSize", sConstr)
            Dim dsSelTotalStock As New DataSet
            daSelTotalStock.Fill(dsSelTotalStock)

            Dim j As Integer = 0

            For j = 0 To dsSelTotalStock.Tables(0).Rows.Count - 1
                sMaterialCode = dsSelTotalStock.Tables(0).Rows(j).Item("MaterialCode")
                sLocation = dsSelTotalStock.Tables(0).Rows(j).Item("Location")
                sSize = dsSelTotalStock.Tables(0).Rows(j).Item("Size").ToString
                nTotalClosingStock = dsSelTotalStock.Tables(0).Rows(j).Item("Quantity")
                sTransactionNo = j

                Dim daSelTransaction As New SqlDataAdapter("Select IssueDate,VoucherNo,MaterialCode,PurchaseOrderNo,TransactionType,FromLocation,ToLocation,IssueQuantity From MaterialIssues Where  MaterialCode = '" & sMaterialCode & _
                                                           "' And (FromLocation = '" & sLocation & _
                                                           "' or ToLocation = '" & sLocation & _
                                                           "') And Size = '" & sSize & _
                                                           "' And TransactionType Not in ('NEW PURCHASE','FULLSHOE PRODUCTION CONSUME','InternalMaterialTransfer','STOCK MATERIAL TRANSFER','Production Entry Tannery') Order by IssueDate", sConstr)
                Dim dsSelTransaction As New DataSet
                daSelTransaction.Fill(dsSelTransaction)

                nRowcount = dsSelTransaction.Tables(0).Rows.Count

                nOpeningStock = 0 : nArrival = 0 : nIssue = 0 : nClosingStock = 0

                Dim i As Integer = 0

                For i = 0 To nRowcount - 1
                    'If i = 0 Then
                    '    nOpeningStock = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                    'Else
                    If dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString Then

                        If dsSelTransaction.Tables(0).Rows(i).Item("TransactionType") = "MaterialInspected" Then
                            nArrival = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                        Else
                            MsgBox("Same From & To Location")
                        End If

                    ElseIf dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = Trim(tbLocation.Text) Then
                        nIssue = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                    ElseIf dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString = Trim(tbLocation.Text) Then
                        nArrival = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                    End If
                    'End If

                    nClosingStock = nOpeningStock + nArrival - nIssue
                    'Dim daInsTransaction As New SqlDataAdapter("Insert Into TempLedgerTable Values ('" & sIpAddress & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                    '                                           "','" & Format(dpto.Value, "dd-MMM-yyyy") & "','" & Trim(tbLocation.Text) & _
                    '                                           "','" & Trim(tbMaterial.Text) & "','" & sMaterialDescription & _
                    '                                           "','" & Trim(tbSize.Text) & "','" & Format(dsSelTransaction.Tables(0).Rows(i).Item("IssueDate"), "dd-MMM-yyyy") & _
                    '                                           "','" & dsSelTransaction.Tables(0).Rows(i).Item("TransactionType") & _
                    '                                           "','" & dsSelTransaction.Tables(0).Rows(i).Item("VoucherNo") & _
                    '                                           "','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                    '                                           "','" & nClosingStock & "')", sConstr)
                    'Dim dsInsTransaction As New DataSet
                    'daInsTransaction.Fill(dsInsTransaction)
                    'dsInsTransaction.AcceptChanges()

                    nOpeningStock = nClosingStock
                    nArrival = 0
                    nIssue = 0
                    nClosingStock = 0
                Next


                Dim sTransactionType As String
                If nOpeningStock = nTotalClosingStock Then
                    sTransactionType = "Matching"
                Else
                    sTransactionType = "Not Matching"
                End If


                nClosingStock = nTotalClosingStock

                Dim daSelAuditStock As New SqlDataAdapter("Select Location,MaterialCode,MaterialSize As Size,IsNull(Sum(Quantity),0) As Quantity from AHGroupAudit.dbo.Stock Where MaterialCode = '" & sMaterialCode & _
                                                          "' And Location = '" & sLocation & _
                                                          "' And MaterialSize = '" & sSize & _
                                                          "' And UpdateMode = 'Deleted' And Stage = 'INSTK' And Quantity > 0 And Location <> '' Group by Location,MaterialCode,MaterialSize Order by Location,MaterialCode,MaterialSize", sConstr)
                Dim dsSelAuditStock As New DataSet
                daSelAuditStock.Fill(dsSelAuditStock)

                If dsSelAuditStock.Tables(0).Rows.Count > 0 Then
                    nArrival = dsSelAuditStock.Tables(0).Rows(0).Item("Quantity")
                End If


                Dim daInsTransaction1 As New SqlDataAdapter("Insert Into TempLedgerTable1 Values ('" & sIpAddress & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                           "','" & Format(dpto.Value, "dd-MMM-yyyy") & "','" & sLocation & _
                                                           "','" & sMaterialCode & "','" & sMaterialDescription & _
                                                           "','" & sSize & "','" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                           "','" & sTransactionType & "','" & sTransactionNo & "','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                                                           "','" & nClosingStock & "')", sConstr)
                Dim dsInsTransaction1 As New DataSet
                daInsTransaction1.Fill(dsInsTransaction1)
                dsInsTransaction1.AcceptChanges()

            Next

            Me.TempLedgerTableTableAdapter.Fill(Me.DsLedger.TempLedgerTable, sIpAddress, Trim(tbMaterial.Text))
            MsgBox("Completed")

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

End Class