Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle


'Imports PCComm

Public Class frmStockByMonthLedger

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard

    Dim nOpeningStock, nArrival, nIssue, nClosingStock, nTotalClosingStock, nCurrentStock As Decimal
    Dim nRowcount As Integer
    Dim sMaterialcode, sSize As String
    Dim dFromDate, dToDate, dCurrentDate, dClosingDate As Date
    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Try
            Dim dadelLedger As New SqlDataAdapter("Delete from TempLedgerTable1 Where SystemIp = '" & sIpAddress & "'", sConstr)
            Dim dsDelLedger As New DataSet
            dadelLedger.Fill(dsDelLedger)
            dsDelLedger.AcceptChanges()

            dCurrentDate = Format(dpFrom.Value, "dd-MMM-yyyy")
            dFromDate = DateAdd(DateInterval.Day, -(Format(dpFrom.Value, "dd") - 1), dCurrentDate)
            dClosingDate = DateAdd(DateInterval.Day, -1, dFromDate)
            dToDate = DateAdd(DateInterval.Month, 1, dFromDate)
            'dToDate = Format(Date.Now, "dd-MMM-yyyy")
            dToDate = DateAdd(DateInterval.Day, -1, dToDate)
            'MsgBox(dCurrentDate)
            'MsgBox(dClosingDate)
            'MsgBox(dFromDate)
            'MsgBox(dToDate)

            Dim daSelMaterials As New SqlDataAdapter("Select Distinct MaterialCode from StockByMonth  where Location = '" & Trim(tbLocation.Text) & _
                                                     "' And StockDate >= '" & Format(dClosingDate.Date, "dd-MMM-yyyy") & _
                                                     "' And StockDate < '" & Format(dFromDate.Date, "dd-MMM-yyyy") & _
                                                     "' Union Select Distinct MaterialCode from MaterialIssues  where (FromLocation = '" & Trim(tbLocation.Text) & _
                                                     "' or ToLocation = '" & Trim(tbLocation.Text) & "') And IssueDate > '" & Format(dClosingDate.Date, "dd-MMM-yyyy") & _
                                                     "' And IssueDate < '" & Format(dToDate.Date, "dd-MMM-yyyy") & _
                                                     "' And MaterialCode in (Select MaterialCode from Materials) Order by MaterialCode", sConstr)
            Dim dsSelMaterials As New DataSet
            daSelMaterials.Fill(dsSelMaterials)

            Dim i As Integer = 0

            For i = 0 To dsSelMaterials.Tables(0).Rows.Count - 1
                sMaterialcode = dsSelMaterials.Tables(0).Rows(i).Item("MaterialCode").ToString

                
                Dim daSelMaterialDescription As New SqlDataAdapter("Select * from Materials Where MaterialCode = '" & sMaterialcode & "'", sConstr)
                Dim dsSelMaterialDescription As New DataSet
                daSelMaterialDescription.Fill(dsSelMaterialDescription)

                sMaterialDescription = dsSelMaterialDescription.Tables(0).Rows(0).Item("Description")

                Dim daSelSizeType As New SqlDataAdapter("Select * from MaterialType Where MaterialTypeCode = '" & Microsoft.VisualBasic.Left(sMaterialcode, 10) & "'", sConstr)
                Dim dsSelSizeType As New DataSet
                daSelSizeType.Fill(dsSelSizeType)

                If dsSelSizeType.Tables(0).Rows(0).Item("SizeType").ToString = "SG" Or dsSelSizeType.Tables(0).Rows(0).Item("SizeType").ToString = "SS" Then
                    'UpdateLedgerWithSize()
                Else
                    UpdateLedgerWithoutSize()
                End If
            Next


            MsgBox("Completed")

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub UpdateLedgerWithoutSize()
        Try
            nOpeningStock = 0 : nArrival = 0 : nIssue = 0 : nClosingStock = 0

            Dim daSelOpnStk As New SqlDataAdapter("Select IsNull(SUM(Quantity),0) AS OpeningStock from StockByMonth where MaterialCode = '" & sMaterialcode & _
                                                  "' And Location = '" & Trim(tbLocation.Text) & "' And (cast ([StockDate] as date) = '" & Format(dClosingDate.Date, "dd-MMM-yyyy") & "')", sConstr)
            Dim dsSelOpnStk As New DataSet
            daSelOpnStk.Fill(dsSelOpnStk)

            nOpeningStock = Val(dsSelOpnStk.Tables(0).Rows(0).Item("OpeningStock"))
            
            Dim daSelArrival As New SqlDataAdapter("Select ISNULL(SUM(IssueQuantity),0) As Arrival from Materialissues Where MaterialCode = '" & sMaterialcode _
                                                   & "' And cast ([IssueDate] as date) > '" & Format(dClosingDate.Date, "dd-MMM-yyyy") & _
                                                   "' And cast ([IssueDate] as date) < '" & Format(DateAdd(DateInterval.Day, 1, (dToDate.Date)), "dd-MMM-yyyy") & _
                                                   "' And ToLocation = '" & Trim(tbLocation.Text) & "' And IsNull(FromLocation,'') <> ToLocation", sConstr)
            Dim dsSelArrival As New DataSet
            daSelArrival.Fill(dsSelArrival)

            nArrival = Val(dsSelArrival.Tables(0).Rows(0).Item("Arrival"))

          

            Dim daSelIssue As New SqlDataAdapter("Select ISNULL(SUM(IssueQuantity),0) As Issue from Materialissues Where MaterialCode = '" & sMaterialcode _
                                                   & "' And cast ([IssueDate] as date) > '" & Format(dClosingDate.Date, "dd-MMM-yyyy") & _
                                                   "' And cast ([IssueDate] as date) < '" & Format(DateAdd(DateInterval.Day, 1, (dToDate.Date)), "dd-MMM-yyyy") & _
                                                   "' And FromLocation = '" & Trim(tbLocation.Text) & "' And FromLocation <> ToLocation", sConstr)
            Dim dsSelIssue As New DataSet
            daSelIssue.Fill(dsSelIssue)

            nIssue = Val(dsSelIssue.Tables(0).Rows(0).Item("Issue"))

            nClosingStock = nOpeningStock + nArrival - nIssue
            Dim dsSelStock As New DataSet

           
            'If dToDate > Date.Now Then
            Dim daSelStock As New SqlDataAdapter("Select ISNULL(Sum(Quantity),0) As CurrentStock from Stock where MaterialCode = '" & sMaterialcode & _
                                                 "' And Stage in ('INSTK','GIN','INSP') And Location = '" & Trim(tbLocation.Text) & "'", sConstr)
            daSelStock.Fill(dsSelStock)
            'Else

            '    Dim daSelStock As New SqlDataAdapter("Select IsNull(SUM(Quantity),0) AS CurrentStock from StockByMonth where MaterialCode = '" & sMaterialcode & _
            '                                          "' And Location = '" & Trim(tbLocation.Text) & "' And (StockDate >= '" & Format(dToDate.Date, "dd-MMM-yyyy") & _
            '                                          "' And StockDate < '" & Format(DateAdd(DateInterval.Day, 1, (dToDate.Date)), "dd-MMM-yyyy") & "')", sConstr)
            '    daSelStock.Fill(dsSelStock)
            'End If



            nCurrentStock = Val(dsSelStock.Tables(0).Rows(0).Item("CurrentStock").ToString)

            Dim daInsTransaction As New SqlDataAdapter("Insert Into TempLedgerTable1 Values ('" & sIpAddress & "','" & Format(dFromDate.Date, "dd-MMM-yyyy") & _
                                                           "','" & Format(dToDate.Date, "dd-MMM-yyyy") & "','" & Trim(tbLocation.Text) & _
                                                           "','" & sMaterialcode & "','" & sMaterialDescription & _
                                                           "','','" & Format(dToDate.Date, "dd-MMM-yyyy") & _
                                                           "','','" & Val(nCurrentStock) & "','" & nOpeningStock & "','" & nArrival & "','" & nIssue & _
                                                           "','" & nClosingStock & "')", sConstr)
            Dim dsInsTransaction As New DataSet
            daInsTransaction.Fill(dsInsTransaction)
            dsInsTransaction.AcceptChanges()

            nOpeningStock = 0 : nArrival = 0 : nIssue = 0 : nClosingStock = 0
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

        'Me.TempLedgerTableTableAdapter.Fill(Me.DsLedger.TempLedgerTable, sIpAddress, sMaterialcode)
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






    


    Private Sub cbLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLocation.Click
        grdLocation.Visible = True
        grdLocation.BringToFront()
        tbLocation.Clear()
        sLocation = ""
        Me.LocationTableAdapter.Fill(Me.DsLocation.Location)

    End Sub

    Dim ngrdRowNo As Integer
    Dim sLocation, sMaterialDescription As String
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


 
 
   

    Private Sub cbExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExporttoExcel.Click
        grdLedger.ExportToXls("D:\Ledger.xls")
        MsgBox("Export Completed")
    End Sub

    Dim sTransactionNo As String

 
End Class