Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle


'Imports PCComm

Public Class frmStockLedger

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    'Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard







    Dim nOpeningStock, nArrival, nIssue, nClosingStock, nTotalClosingStock As Decimal
    Dim nRowcount As Integer
    Dim dTransactionDate As Date
    Dim sTransactionType, sVoucherNo, sMaterialType As String


    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Try
            Dim dadelLedger As New SqlDataAdapter("Delete from TempLedgerTable Where SystemIp = '" & sIpAddress & "'", sConstr)
            Dim dsDelLedger As New DataSet
            dadelLedger.Fill(dsDelLedger)
            dsDelLedger.AcceptChanges()

            Dim daSelSizeType As New SqlDataAdapter("Select * from MaterialType Where MaterialTypeCode = '" & sMaterialType & "'", sConstr)
            Dim dsSelSizeType As New DataSet
            daSelSizeType.Fill(dsSelSizeType)

            'If dsSelSizeType.Tables(0).Rows(0).Item("SizeType").ToString = "SG" Or dsSelSizeType.Tables(0).Rows(0).Item("SizeType").ToString = "SS" Then
            '    UpdateLedgerWithSize()
            'Else
            UpdateLedgerWithoutSize()
            'End If

            'Me.TempLedgerTableTableAdapter.Fill(Me.DsLedger.TempLedgerTable, sIpAddress, Trim(tbMaterial.Text))
            MsgBox("Completed")

        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
        End Try

    End Sub

    Private Sub InsertTransaction()
        Dim daInsTransaction As New SqlDataAdapter("Insert Into TempLedgerTable Values ('" & sIpAddress & "','" & Format(dpFrom.Value, "dd-MMM-yyyy") & _
                                                           "','" & Format(dpto.Value, "dd-MMM-yyyy") & "','" & Trim(tbLocation.Text) & _
                                                           "','" & Trim(tbMaterial.Text) & "','" & sMaterialDescription & _
                                                           "','" & Trim(tbSize.Text) & "','" & Format(dTransactionDate.Date, "dd-MMM-yyyy") & _
                                                           "','" & sTransactionType & _
                                                           "','" & sVoucherNo & _
                                                           "','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                                                           "','" & nClosingStock & "')", sConstr)
        Dim dsInsTransaction As New DataSet
        daInsTransaction.Fill(dsInsTransaction)
        dsInsTransaction.AcceptChanges()
    End Sub

    Private Sub UpdateLedgerWithoutSize()
        Try
            nOpeningStock = 0 : nArrival = 0 : nIssue = 0 : nClosingStock = 0

            Dim daSelOpnStk As New SqlDataAdapter("Select IsNull(SUM(Quantity),0) AS OpeningStock from StockByMonth where MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                  "' And Location = '" & Trim(tbLocation.Text) & "' And (StockDate >= '2018-03-31' And StockDate < '2018-04-01')", sConstr)
            Dim dsSelOpnStk As New DataSet
            daSelOpnStk.Fill(dsSelOpnStk)

            nOpeningStock = Val(dsSelOpnStk.Tables(0).Rows(0).Item("OpeningStock"))
            nClosingStock = Val(dsSelOpnStk.Tables(0).Rows(0).Item("OpeningStock"))

            dTransactionDate = Format(dpFrom.Value, "dd-MMM-yyyy")
            sTransactionType = "Opening Stock"
            sVoucherNo = ""

            InsertTransaction()

            nOpeningStock = nClosingStock : nArrival = 0 : nIssue = 0 : nClosingStock = 0

            Dim daSelTransaction As New SqlDataAdapter("Select IssueDate,VoucherNo,MaterialCode,PurchaseOrderNo,TransactionType,FromLocation,ToLocation,IssueQuantity From MaterialIssues Where  MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                       "' And (FromLocation = '" & Trim(tbLocation.Text) & _
                                                       "' or ToLocation = '" & Trim(tbLocation.Text) & _
                                                       "') And IssueDate >= '" & Format(dpFrom.Value, "dd-MMM-yyyy") & "'Order by IssueDate", sConstr)
            Dim dsSelTransaction As New DataSet
            daSelTransaction.Fill(dsSelTransaction)

            nRowcount = dsSelTransaction.Tables(0).Rows.Count

            Dim i As Integer = 0

            For i = 0 To nRowcount - 1
                If dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString Then
                    GoTo Ab
                ElseIf dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = Trim(tbLocation.Text) Then
                    nIssue = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                ElseIf dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString = Trim(tbLocation.Text) Then
                    nArrival = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                End If

                nClosingStock = nOpeningStock + nArrival - nIssue

                dTransactionDate = Format(dsSelTransaction.Tables(0).Rows(i).Item("IssueDate"), "dd-MMM-yyyy")
                sTransactionType = Trim(dsSelTransaction.Tables(0).Rows(i).Item("TransactionType")).ToString
                sVoucherNo = Trim(dsSelTransaction.Tables(0).Rows(i).Item("VoucherNo")).ToString

                InsertTransaction()

                nOpeningStock = nClosingStock
                nArrival = 0
                nIssue = 0
                nClosingStock = 0
Ab:
            Next

            Dim daSelStock As New SqlDataAdapter("Select Stage,SUM(Quantity) AS ClosingStock from Stock where MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                 "' And Location = '" & Trim(tbLocation.Text) & _
                                                 "' And Stage IN ('INSTK','GIN','INSP') Group By Stage", sConstr)
            Dim dsSelStock As New DataSet
            daSelStock.Fill(dsSelStock)

            nRowcount = dsSelStock.Tables(0).Rows.Count

            i = 0

            For i = 0 To nRowcount - 1
                dTransactionDate = Format(Date.Now, "dd-MMM-yyyy")
                sTransactionType = "Closing Stock - " + Trim(dsSelStock.Tables(0).Rows(i).Item("Stage")).ToString
                sVoucherNo = ""

                nOpeningStock = 0
                nArrival = 0
                nIssue = 0
                nClosingStock = Val(dsSelStock.Tables(0).Rows(i).Item("ClosingStock").ToString)

                InsertTransaction()

            Next
        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub UpdateLedgerWithSize()
        Try
            nOpeningStock = 0 : nArrival = 0 : nIssue = 0 : nClosingStock = 0

            Dim daSelOpnStk As New SqlDataAdapter("Select IsNull(SUM(Quantity),0) AS OpeningStock from StockByMonth where MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                  "' And Location = '" & Trim(tbLocation.Text) & _
                                                  "' And MaterialSize = '" & Trim(tbSize.Text) & _
                                                  "' And (StockDate >= '2018-03-31' And StockDate < '2018-04-01')", sConstr)
            Dim dsSelOpnStk As New DataSet
            daSelOpnStk.Fill(dsSelOpnStk)

            nOpeningStock = Val(dsSelOpnStk.Tables(0).Rows(0).Item("OpeningStock"))
            nClosingStock = Val(dsSelOpnStk.Tables(0).Rows(0).Item("OpeningStock"))

            dTransactionDate = Format(dpFrom.Value, "dd-MMM-yyyy")
            sTransactionType = "Opening Stock"
            sVoucherNo = ""

            InsertTransaction()

            nOpeningStock = nClosingStock : nArrival = 0 : nIssue = 0 : nClosingStock = 0

            Dim daSelTransaction As New SqlDataAdapter("Select IssueDate,VoucherNo,MaterialCode,PurchaseOrderNo,TransactionType,FromLocation,ToLocation,IssueQuantity From MaterialIssues Where  MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                       "' And (FromLocation = '" & Trim(tbLocation.Text) & _
                                                       "' or ToLocation = '" & Trim(tbLocation.Text) & _
                                                       "' And Size = '" & Trim(tbSize.Text) & _
                                                       "') And IssueDate >= '" & Format(dpFrom.Value, "dd-MMM-yyyy") & "'Order by IssueDate", sConstr)
            Dim dsSelTransaction As New DataSet
            daSelTransaction.Fill(dsSelTransaction)

            nRowcount = dsSelTransaction.Tables(0).Rows.Count

            Dim i As Integer = 0

            For i = 0 To nRowcount - 1
                If dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString Then
                    GoTo Ab
                ElseIf dsSelTransaction.Tables(0).Rows(i).Item("FromLocation").ToString = Trim(tbLocation.Text) Then
                    nIssue = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                ElseIf dsSelTransaction.Tables(0).Rows(i).Item("ToLocation").ToString = Trim(tbLocation.Text) Then
                    nArrival = dsSelTransaction.Tables(0).Rows(i).Item("IssueQuantity")
                End If

                nClosingStock = nOpeningStock + nArrival - nIssue

                dTransactionDate = Format(dsSelTransaction.Tables(0).Rows(i).Item("IssueDate"), "dd-MMM-yyyy")
                sTransactionType = Trim(dsSelTransaction.Tables(0).Rows(i).Item("TransactionType")).ToString
                sVoucherNo = Trim(dsSelTransaction.Tables(0).Rows(i).Item("VoucherNo")).ToString

                InsertTransaction()

                nOpeningStock = nClosingStock
                nArrival = 0
                nIssue = 0
                nClosingStock = 0
Ab:
            Next

            Dim daSelStock As New SqlDataAdapter("Select Stage,SUM(Quantity) AS ClosingStock from Stock where MaterialCode = '" & Trim(tbMaterial.Text) & _
                                                 "' And Location = '" & Trim(tbLocation.Text) & _
                                                 "' And MaterialSize = '" & Trim(tbSize.Text) & _
                                                 "' And Stage IN ('INSTK','GIN','INSP') Group By Stage", sConstr)
            Dim dsSelStock As New DataSet
            daSelStock.Fill(dsSelStock)

            nRowcount = dsSelStock.Tables(0).Rows.Count

            i = 0

            For i = 0 To nRowcount - 1
                dTransactionDate = Format(Date.Now, "dd-MMM-yyyy")
                sTransactionType = "Closing Stock - " + Trim(dsSelStock.Tables(0).Rows(i).Item("Stage")).ToString
                sVoucherNo = ""

                nOpeningStock = 0
                nArrival = 0
                nIssue = 0
                nClosingStock = Val(dsSelStock.Tables(0).Rows(i).Item("ClosingStock").ToString)

                InsertTransaction()

            Next
        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
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

        'Me.TempLedgerTableTableAdapter.Fill(Me.DsLedger.TempLedgerTable, sIpAddress, Trim(tbMaterial.Text))
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
        'Me.LocationTableAdapter.Fill(Me.DsLocation.Location)

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
            'HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.Click
        grdMaterials.Visible = True
        grdMaterials.BringToFront()
        tbMaterial.Clear()
        sMaterialCode = ""
        'Me.MaterialsTableAdapter.Fill(Me.DsMaterials.Materials, Trim(tbLocation.Text))
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
                sMaterialType = grdMaterialsV1.GetDataRow(ngrdRowNo).Item("MaterialTypeCode").ToString
                tbMaterial.Text = sMaterialCode
                grdMaterials.Visible = False
            End If
        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSize.Click
        grdSize.Visible = True
        grdSize.BringToFront()
        tbSize.Clear()
        sSize = ""
        'Me.MaterialIssuesTableAdapter.Fill(Me.DsSize.MaterialIssues, Trim(tbMaterial.Text))
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
            'HandleException(Me.Name, Exp)
        End Try
    End Sub


    Private Sub cbExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExporttoExcel.Click
        grdLedger.ExportToXls("D:\Ledger.xls")
        MsgBox("Export Completed")
    End Sub

    Dim sTransactionNo As String

 
    Private Sub grdMaterials_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMaterials.Click

    End Sub

    Private Sub grdMaterials_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMaterials.DoubleClick

    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click

    End Sub
End Class