Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle


'Imports PCComm

Public Class frmKHLIOCSAllCustomerJW

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard

    Private comm As New CommManager()
    Private transType As String = String.Empty





    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try


        ''Catch ex As Exception

        ''End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
        'Me.Hide()
        'Form1.Show()
    End Sub

    Dim ngrdRowCount As Integer

    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sAgainstSalesOrderDetail = "N"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        
        grdArticleMaster.ExportToXls("D:\ArticleMaster.xls")
        MsgBox("Export Completed")

    End Sub

    Dim keyascii As Integer
    Dim sJobcardNo, sOrderNo, sWeightAvailable, sArticleGroup, sArticleName, sArticleColour, sArticleId As String
    Dim nSize, nQuantityinSize As Integer
    Dim sFKJobcardId As String

    '09840808969 - Saravanan
    '08870454393

    Private Sub tbJobcardNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbJobcardNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then

            If Trim(tbJobcardNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Jobcard")
                Exit Sub
            End If

            Dim daSelJobcard As New SqlDataAdapter("Select * from jobcardDetail where Barcode = '" & Trim(tbJobcardNo.Text) & "'", sCnn)
            Dim dsSelJobcard As New DataSet
            daSelJobcard.Fill(dsSelJobcard)

            If dsSelJobcard.Tables(0).Rows.Count <> 1 Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Jobcard")
            Else
                sFKJobcardId = dsSelJobcard.Tables(0).Rows(0).Item("ID")
                sJobcardNo = dsSelJobcard.Tables(0).Rows(0).Item("JobcardNo")
                sOrderNo = dsSelJobcard.Tables(0).Rows(0).Item("CustomerOrderNo")

                Dim daSelArticleInfo As New SqlDataAdapter("Select * from SalesOrderDetails where CustomerOrderNo = '" & sOrderNo & _
                                                           "' And Shipper <> 'SSPL'", sCnn)
                Dim dsSelArticleInfo As New DataSet
                daSelArticleInfo.Fill(dsSelArticleInfo)

                sArticleId = dsSelArticleInfo.Tables(0).Rows(0).Item("ArticleDetailID")
                sArticleGroup = dsSelArticleInfo.Tables(0).Rows(0).Item("ArticleGroup")
                sArticleName = dsSelArticleInfo.Tables(0).Rows(0).Item("Article")
                sArticleColour = dsSelArticleInfo.Tables(0).Rows(0).Item("ColorName")

            End If
            tbOuterCartonNoforViewing.Clear() : tbOuterCartonNoforViewing.Focus()
            'MsgBox("V:2")
            sAgainstSalesOrderDetail = "N"
            Loadgrdinfo()
        End If

        'Catch ex As Exception

        'End Try
    End Sub

    Dim nCartonNo As Integer
    Dim sCartonNo As String
    Dim sImageFileName, sAgainstSalesOrderDetail As String

    Private Sub tbOuterCartonNoforViewing_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbOuterCartonNoforViewing.KeyPress
        'Try


        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then


            keyascii = 0

            If Trim(tbOuterCartonNoforViewing.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Outer CartonNo - tbOuterCartonNoforViewing-1")
                Exit Sub
            End If



            Dim daSelPackingListInfo As New SqlDataAdapter("Select * from PackingDetail where packingListNo = (Select ID from Packing where CUSTWORKORDERnO = '" & Microsoft.VisualBasic.Left(sJobcardNo, 19) & _
                                                           "') And Barcode = '" & Trim(tbOuterCartonNoforViewing.Text) & "' order by cartonNo", sCnn)
            Dim dsSelPackingListInfo As New DataSet
            daSelPackingListInfo.Fill(dsSelPackingListInfo)

            Dim nRowCount As Integer

            nRowCount = dsSelPackingListInfo.Tables(0).Rows.Count

            If nRowCount <> 1 Then
                MsgBox("Invalid Outer Box Barcode No.", MsgBoxStyle.Critical)
            Else
                If Val(dsSelPackingListInfo.Tables(0).Rows(0).Item("IsPacked").ToString) <> "0" Then
                    tbOuterCartonNo.Clear()
                    tbOuterCartonNo.Focus()
                    'MsgBox("4")
                    MsgBox("This Box is Already Scanned / Packed. Double Entry!", MsgBoxStyle.Critical)
                Else
                    Dim daSelPackingInfo As New SqlDataAdapter("Select * from PackingInfo Where JobcardNo = '" & Trim(tbJobcardNo.Text) & _
                                                               "' And OuterCartonNo = '" & Trim(tbOuterCartonNoforViewing.Text) & _
                                                               "' And IsUpdatedinAP <> 1", sCnn)
                    Dim dsSelPackingInfo As New DataSet
                    daSelPackingInfo.Fill(dsSelPackingInfo)

                    If dsSelPackingInfo.Tables(0).Rows.Count > 0 Then
                        MsgBox("This Box is Already Scanned / Packed. Double Entry!", MsgBoxStyle.Critical)
                        tbOuterCartonNo.Clear()
                        tbOuterCartonNo.Focus()
                        'MsgBox("5")
                        Exit Sub
                    End If


                    tbOS01.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size01")
                    tbOS02.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size02")
                    tbOS03.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size03")
                    tbOS04.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size04")
                    tbOS05.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size05")
                    tbOS06.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size06")
                    tbOS07.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size07")
                    tbOS08.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size08")
                    tbOS09.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size09")
                    tbOS10.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size10")
                    tbOS11.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size11")
                    tbOS12.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size12")
                    tbOS13.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size13")
                    tbOS14.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size14")
                    tbOS15.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size15")
                    tbOS16.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size16")
                    tbOS17.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size17")
                    tbOS18.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size18")

                    tbOQ01.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity01")
                    tbOQ02.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity02")
                    tbOQ03.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity03")
                    tbOQ04.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity04")
                    tbOQ05.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity05")
                    tbOQ06.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity06")
                    tbOQ07.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity07")
                    tbOQ08.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity08")
                    tbOQ09.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity09")
                    tbOQ10.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity10")
                    tbOQ11.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity11")
                    tbOQ12.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity12")
                    tbOQ13.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity13")
                    tbOQ14.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity14")
                    tbOQ15.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity15")
                    tbOQ16.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity16")
                    tbOQ17.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity17")
                    tbOQ18.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity18")

                    If Val(tbOS01.Text) = 0 Then : tbOS01.Clear() : End If
                    If Val(tbOS02.Text) = 0 Then : tbOS02.Clear() : End If
                    If Val(tbOS03.Text) = 0 Then : tbOS03.Clear() : End If
                    If Val(tbOS04.Text) = 0 Then : tbOS04.Clear() : End If
                    If Val(tbOS05.Text) = 0 Then : tbOS05.Clear() : End If
                    If Val(tbOS06.Text) = 0 Then : tbOS06.Clear() : End If
                    If Val(tbOS07.Text) = 0 Then : tbOS07.Clear() : End If
                    If Val(tbOS08.Text) = 0 Then : tbOS08.Clear() : End If
                    If Val(tbOS09.Text) = 0 Then : tbOS09.Clear() : End If
                    If Val(tbOS10.Text) = 0 Then : tbOS10.Clear() : End If
                    If Val(tbOS11.Text) = 0 Then : tbOS11.Clear() : End If
                    If Val(tbOS12.Text) = 0 Then : tbOS12.Clear() : End If
                    If Val(tbOS13.Text) = 0 Then : tbOS13.Clear() : End If
                    If Val(tbOS14.Text) = 0 Then : tbOS14.Clear() : End If
                    If Val(tbOS15.Text) = 0 Then : tbOS15.Clear() : End If
                    If Val(tbOS16.Text) = 0 Then : tbOS16.Clear() : End If
                    If Val(tbOS17.Text) = 0 Then : tbOS17.Clear() : End If
                    If Val(tbOS18.Text) = 0 Then : tbOS18.Clear() : End If


                    If Val(tbOQ01.Text) = 0 Then : tbOQ01.Clear() : End If
                    If Val(tbOQ02.Text) = 0 Then : tbOQ02.Clear() : End If
                    If Val(tbOQ03.Text) = 0 Then : tbOQ03.Clear() : End If
                    If Val(tbOQ04.Text) = 0 Then : tbOQ04.Clear() : End If
                    If Val(tbOQ05.Text) = 0 Then : tbOQ05.Clear() : End If
                    If Val(tbOQ06.Text) = 0 Then : tbOQ06.Clear() : End If
                    If Val(tbOQ07.Text) = 0 Then : tbOQ07.Clear() : End If
                    If Val(tbOQ08.Text) = 0 Then : tbOQ08.Clear() : End If
                    If Val(tbOQ09.Text) = 0 Then : tbOQ09.Clear() : End If
                    If Val(tbOQ10.Text) = 0 Then : tbOQ10.Clear() : End If
                    If Val(tbOQ11.Text) = 0 Then : tbOQ11.Clear() : End If
                    If Val(tbOQ12.Text) = 0 Then : tbOQ12.Clear() : End If
                    If Val(tbOQ13.Text) = 0 Then : tbOQ13.Clear() : End If
                    If Val(tbOQ14.Text) = 0 Then : tbOQ14.Clear() : End If
                    If Val(tbOQ15.Text) = 0 Then : tbOQ15.Clear() : End If
                    If Val(tbOQ16.Text) = 0 Then : tbOQ16.Clear() : End If
                    If Val(tbOQ17.Text) = 0 Then : tbOQ17.Clear() : End If
                    If Val(tbOQ18.Text) = 0 Then : tbOQ18.Clear() : End If
                    keyascii = 0


                    tbOuterCartonNo.Focus()
                    'MsgBox("6")
                End If

            End If




        End If


        'Catch ex As Exception

        'End Try
    End Sub


    Private Sub tbOuterCartonNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbOuterCartonNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        'sTransferFocus = "N"
        If keyascii = 13 Then
            keyascii = 0



            If Trim(tbOuterCartonNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Outer CartonNo :: tbOuterCartonNo - 2")
                Exit Sub
            End If



            Dim daSelPackingListInfo As New SqlDataAdapter("Select * from PackingDetail where packingListNo = (Select ID from Packing where CUSTWORKORDERnO = '" & Microsoft.VisualBasic.Left(sJobcardNo, 19) & _
                                                           "') And Barcode = '" & Trim(tbOuterCartonNo.Text) & "' order by cartonNo", sCnn)
            Dim dsSelPackingListInfo As New DataSet
            daSelPackingListInfo.Fill(dsSelPackingListInfo)

            Dim nRowCount As Integer

            nRowCount = dsSelPackingListInfo.Tables(0).Rows.Count

            If nRowCount <> 1 Then
                MsgBox("Invalid Outer Box Barcode No.", MsgBoxStyle.Critical)
            Else
                If Val(dsSelPackingListInfo.Tables(0).Rows(0).Item("IsPacked").ToString) <> "0" Then
                    tbOuterCartonNo.Clear()
                    tbOuterCartonNo.Focus()
                    'MsgBox("1")
                    MsgBox("This Box is Already Scanned / Packed. Double Entry!", MsgBoxStyle.Critical)
                Else
                    Dim daSelPackingInfo As New SqlDataAdapter("Select * from PackingInfo Where JobcardNo = '" & Trim(tbJobcardNo.Text) & _
                                                               "' And OuterCartonNo = '" & Trim(tbOuterCartonNo.Text) & _
                                                               "' And IsUpdatedinAP <> 1", sCnn)
                    Dim dsSelPackingInfo As New DataSet
                    daSelPackingInfo.Fill(dsSelPackingInfo)

                    If dsSelPackingInfo.Tables(0).Rows.Count > 0 Then
                        MsgBox("This Box is Already Scanned / Packed. Double Entry!", MsgBoxStyle.Critical)
                        tbOuterCartonNo.Clear()
                        tbOuterCartonNo.Focus()
                        'MsgBox("2")
                        Exit Sub
                    End If
                    Dim sId As String
                    sId = System.Guid.NewGuid.ToString()
                    Dim daInsPackingInfo As New SqlDataAdapter("Insert Into PackingInfo(ID,JobcardNo,OuterCartonNo,IsUpdatedinAP,PackedOn,PackedDt) Values ('" & sId & _
                                                               "','" & Trim(tbJobcardNo.Text) & "','" & Trim(tbOuterCartonNo.Text) & "','0','" & Format(Date.Now, "dd-MMM-yy") & _
                                                               "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "')", sCnn)
                    Dim dsInsPackingInfo As New DataSet
                    daInsPackingInfo.Fill(dsInsPackingInfo)
                    dsInsPackingInfo.AcceptChanges()

                    sCartonNo = Trim(tbOuterCartonNo.Text)
                    tbOuterCartonNo.Clear()
                    tbOuterCartonNoforViewing.Clear() : tbOuterCartonNoforViewing.Focus()
                    'MsgBox("V:1")

                    ClearSizesAndQuantity()
                    sAgainstSalesOrderDetail = "Y"

                End If

            End If




        End If
        Loadgrdinfo()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Clear()
        keyascii = 0
        tbOuterCartonNo.Clear()

        tbOuterCartonNo.Focus()
        'MsgBox("3")

    End Sub

    Private Sub ClearSizesAndQuantity()
        tbOS01.Clear() : tbOS02.Clear() : tbOS03.Clear() : tbOS04.Clear() : tbOS05.Clear() : tbOS06.Clear() : tbOS07.Clear() : tbOS08.Clear() : tbOS09.Clear()
        tbOS10.Clear() : tbOS11.Clear() : tbOS12.Clear() : tbOS13.Clear() : tbOS14.Clear() : tbOS15.Clear() : tbOS16.Clear() : tbOS17.Clear() : tbOS18.Clear()

        tbOQ01.Clear() : tbOQ02.Clear() : tbOQ03.Clear() : tbOQ04.Clear() : tbOQ05.Clear() : tbOQ06.Clear() : tbOQ07.Clear() : tbOQ08.Clear() : tbOQ09.Clear()
        tbOQ10.Clear() : tbOQ11.Clear() : tbOQ12.Clear() : tbOQ13.Clear() : tbOQ14.Clear() : tbOQ15.Clear() : tbOQ16.Clear() : tbOQ17.Clear() : tbOQ18.Clear()
    End Sub


    Dim nJCSize, nJCQty As Integer
    Dim nWeight As Decimal

    Dim nTotalCartons, nPackedCartons As Integer





    Private Sub Loadgrdinfo()
        ''Try

        Dim dsSelPackingListInfo As New DataSet
        If sAgainstSalesOrderDetail = "N" Then
            Dim daSelPackingListInfo As New SqlDataAdapter("Select * from PackingDetail where packingListNo = (Select ID from Packing where CUSTWORKORDERnO = '" & Microsoft.VisualBasic.Left(sJobcardNo, 19) & _
                                                           "') order by cartonNo", sCnn)
            daSelPackingListInfo.Fill(dsSelPackingListInfo)
        Else
            Dim daSelPackingListInfo As New SqlDataAdapter("Select * from PackingDetail where packingListNo = (Select ID from Packing where CUSTWORKORDERnO = '" & Microsoft.VisualBasic.Left(sJobcardNo, 19) & _
                                                           "') And SalesOrderDetailId = (Select SalesOrderDetailId From PackingDetail Where Barcode = '" & sCartonNo & "') order by cartonNo", sCnn)
            daSelPackingListInfo.Fill(dsSelPackingListInfo)
        End If

        nTotalCartons = dsSelPackingListInfo.Tables(0).Rows.Count
        nPackedCartons = 0

        Dim i As Integer = 0

        For i = 0 To nTotalCartons - 1
            If Val(dsSelPackingListInfo.Tables(0).Rows(i).Item("IsPacked").ToString) * -1 = 1 Then
                nPackedCartons = nPackedCartons + 1
            End If
        Next
        i = 0

        Dim daSelPackingInfo As New SqlDataAdapter("Select * from PackingInfo Where JobcardNo = '" & Trim(tbJobcardNo.Text) & "' And IsUpdatedinAP <> 1", sCnn)
        Dim dsSelPackingInfo As New DataSet
        daSelPackingInfo.Fill(dsSelPackingInfo)

        If dsSelPackingInfo.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsSelPackingInfo.Tables(0).Rows.Count - 1
                nPackedCartons = nPackedCartons + 1
            Next
        End If

        i = 0
        tbTotalBoxes.Text = nTotalCartons
        tbPacked.Text = nPackedCartons
        tbBal2Pack.Text = nTotalCartons - nPackedCartons

Ab:
        ngrdRowCount = grdCartonDtlsV1.RowCount
        For i = 0 To ngrdRowCount
            grdCartonDtlsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdCartonDtlsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        grdCartonDtls.DataSource = myccKHLIOutstandingWithJobcard.LoadPackedInfo(sFKJobcardId)

        With grdCartonDtlsV1

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

        myccKHLIOutstandingWithJobcard.LoadPackedStatus(sFKJobcardId)
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

End Class