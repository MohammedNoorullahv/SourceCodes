Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle


'Imports PCComm

Public Class frmKHLIOuterCartonScanningforAllCustomer

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

    Private Sub tbJobcardNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbJobcardNo.KeyDown

    End Sub

    '09840808969 - Saravanan
    '08870454393
    Dim sPackingJobcardNo As String
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
                sPackingJobcardNo = dsSelJobcard.Tables(0).Rows(0).Item("JobcardNo")
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
            'Loadgrdinfo()
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
                    MsgBox("4")
                    MsgBox("This Box is Already Scanned / Packed. Double Entry!", MsgBoxStyle.Critical)
                Else
                    Dim daSelPackingInfo As New SqlDataAdapter("Select * from PackingInfo Where JobcardNo = '" & Trim(tbJobcardNo.Text) & _
                                                               "' And OuterCartonNo = '" & Trim(tbOuterCartonNoforViewing.Text) & _
                                                               "' And IsUpdatedinAP <> 1", sCnn)
                    Dim dsSelPackingInfo As New DataSet
                    daSelPackingInfo.Fill(dsSelPackingInfo)

                    If dsSelPackingInfo.Tables(0).Rows.Count > 0 Then
                        MsgBox("This Box is Already Scanned / Packed. Double Entry!", MsgBoxStyle.Critical)
                        tbOuterCartonNoforViewing.Clear()
                        tbOuterCartonNoforViewing.Focus()
                        Exit Sub
                    End If


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

                    tbOQTotal.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity")

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

                    If Trim(tbPackingJobcardNo.Text) <> Trim(tbJobcardNo.Text) Then
                        tbSameJCQty01.Text = tbOQ01.Text
                        tbSameJCQty02.Text = tbOQ02.Text
                        tbSameJCQty03.Text = tbOQ03.Text
                        tbSameJCQty04.Text = tbOQ04.Text
                        tbSameJCQty05.Text = tbOQ05.Text
                        tbSameJCQty06.Text = tbOQ06.Text
                        tbSameJCQty07.Text = tbOQ07.Text
                        tbSameJCQty08.Text = tbOQ08.Text
                        tbSameJCQty09.Text = tbOQ09.Text
                        tbSameJCQty10.Text = tbOQ10.Text
                        tbSameJCQty11.Text = tbOQ11.Text
                        tbSameJCQty12.Text = tbOQ12.Text
                        tbSameJCQty13.Text = tbOQ13.Text
                        tbSameJCQty14.Text = tbOQ14.Text
                        tbSameJCQty15.Text = tbOQ15.Text
                        tbSameJCQty16.Text = tbOQ16.Text
                        tbSameJCQty17.Text = tbOQ17.Text
                        tbSameJCQty18.Text = tbOQ18.Text
                        tbSameJCQtyTotal.Text = tbOQTotal.Text


                        tbSameJCQty01.Focus()
                    Else
                        tbOuterCartonNo.Focus()
                    End If

                End If

            End If


            Dim daSelTtlPkdABarcode1 As New SqlDataAdapter("Select IsNull(Sum(PKJCQty),0) As PKJCQty From JCPackInfoDetail WHERE PkgBarcode = '" & Trim(tbOuterCartonNoforViewing.Text) & "'", sCnn)
            Dim dsSelTtlPkdABarcode1 As New DataSet
            daSelTtlPkdABarcode1.Fill(dsSelTtlPkdABarcode1)

            tbAlreadyPkdQty.Text = dsSelTtlPkdABarcode1.Tables(0).Rows(0).Item("PKJCQty")

            LoadPackingInfo()

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

            If Trim(tbOuterCartonNoforViewing.Text) <> Trim(tbOuterCartonNo.Text) Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Scanned Barcode & Saving Barcode are different")
                tbOuterCartonNo.Clear()
                tbOuterCartonNoforViewing.Clear()
                tbOuterCartonNoforViewing.Focus()
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


                    If Trim(tbPackingJobcardNo.Text) <> Trim(tbJobcardNo.Text) Then
                        If Val(tbOtherJCQtyTotal.Text) = 0 Then
                            MsgBox("For Difference Jobcard Packing, Other Jobcard Quantity Should Not be Zero", MsgBoxStyle.Critical)
                            Exit Sub
                        End If

                        'If Val(tbOQTotal.Text) <> (Val(tbSameJCQtyTotal.Text) + Val(tbOtherJCQtyTotal.Text)) Then
                        '    MsgBox("Same Jobcard & Other Jobcard Quantities should match the Packing Quantity", MsgBoxStyle.Critical)
                        '    Exit Sub
                        'End If



                    End If

                    UpdateJCPackInfo()

                    If sUpdated = "Y" Then
                        Dim sId As String
                        sId = System.Guid.NewGuid.ToString()
                        Dim daInsPackingInfo As New SqlDataAdapter("Insert Into PackingInfo(ID,JobcardNo,OuterCartonNo,IsUpdatedinAP) Values ('" & sId & _
                                                                   "','" & Trim(tbJobcardNo.Text) & "','" & Trim(tbOuterCartonNo.Text) & "','0')", sCnn)
                        Dim dsInsPackingInfo As New DataSet
                        daInsPackingInfo.Fill(dsInsPackingInfo)
                        dsInsPackingInfo.AcceptChanges()

                        sCartonNo = Trim(tbOuterCartonNo.Text)
                    End If
                    'MsgBox("V:1")

                    'ClearSizesAndQuantity()
                    sAgainstSalesOrderDetail = "Y"

                End If

            End If



            ClearSizesAndQuantity()
            tbOuterCartonNo.Clear()
            tbOuterCartonNoforViewing.Clear() : tbOuterCartonNoforViewing.Focus()

            Loadgrdinfo()
        End If


        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Clear()
        keyascii = 0
        tbOuterCartonNo.Clear()

        tbOuterCartonNo.Focus()
        MsgBox("3")

    End Sub

    Private Sub ClearSizesAndQuantity()
        'tbOS01.Clear() : tbOS02.Clear() : tbOS03.Clear() : tbOS04.Clear() : tbOS05.Clear() : tbOS06.Clear() : tbOS07.Clear() : tbOS08.Clear() : tbOS09.Clear()
        'tbOS10.Clear() : tbOS11.Clear() : tbOS12.Clear() : tbOS13.Clear() : tbOS14.Clear() : tbOS15.Clear() : tbOS16.Clear() : tbOS17.Clear() : tbOS18.Clear()

        tbOQ01.Clear() : tbOQ02.Clear() : tbOQ03.Clear() : tbOQ04.Clear() : tbOQ05.Clear() : tbOQ06.Clear() : tbOQ07.Clear() : tbOQ08.Clear() : tbOQ09.Clear()
        tbOQ10.Clear() : tbOQ11.Clear() : tbOQ12.Clear() : tbOQ13.Clear() : tbOQ14.Clear() : tbOQ15.Clear() : tbOQ16.Clear() : tbOQ17.Clear() : tbOQ18.Clear()
        tbOQTotal.Clear()
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




    
    Private Sub tbOuterCartonNoforViewing_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbOuterCartonNoforViewing.TextChanged

    End Sub

    Private Sub tbJobcardNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbJobcardNo.TextChanged

    End Sub

    Dim sSalesOrderDetailsId As String

    Private Sub tbPackingJobcardNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbPackingJobcardNo.KeyPress

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then


            keyascii = 0

            If Trim(tbPackingJobcardNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Jobcard No")
                Exit Sub
            End If

            ''----------
            Dim daSelJobcard As New SqlDataAdapter("Select * from jobcardDetail where Barcode = '" & Trim(tbPackingJobcardNo.Text) & "'", sCnn)
            Dim dsSelJobcard As New DataSet
            daSelJobcard.Fill(dsSelJobcard)

            If dsSelJobcard.Tables(0).Rows.Count <> 1 Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Jobcard")
            Else
                sFKJobcardId = dsSelJobcard.Tables(0).Rows(0).Item("ID")
                sJobcardNo = dsSelJobcard.Tables(0).Rows(0).Item("JobcardNo")
                sOrderNo = dsSelJobcard.Tables(0).Rows(0).Item("CustomerOrderNo")
                sSalesOrderDetailsId = dsSelJobcard.Tables(0).Rows(0).Item("SalesOrderDetailId")

                tbJCQty01.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity01").ToString
                tbJCQty02.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity02").ToString
                tbJCQty03.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity03").ToString
                tbJCQty04.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity04").ToString
                tbJCQty05.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity05").ToString
                tbJCQty06.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity06").ToString
                tbJCQty07.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity07").ToString
                tbJCQty08.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity08").ToString
                tbJCQty09.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity09").ToString
                tbJCQty10.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity10").ToString
                tbJCQty11.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity11").ToString
                tbJCQty12.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity12").ToString
                tbJCQty13.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity13").ToString
                tbJCQty14.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity14").ToString
                tbJCQty15.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity15").ToString
                tbJCQty16.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity16").ToString
                tbJCQty17.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity17").ToString
                tbJCQty18.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity18").ToString
                tbJCQtyTotal.Text = dsSelJobcard.Tables(0).Rows(0).Item("Quantity").ToString

                LoadPackingInfo()

                'adsfdasf()

                If sSalesOrderDetailsId <> "" Then
                    LoadNonAssortedOrderSODDetails()
                End If

            End If
            ''----------

            tbJobcardNo.Text = Trim(tbPackingJobcardNo.Text)
            tbJobcardNo.Focus()

        End If


        'Catch ex As Exception

        'End Try


    End Sub

    Private Sub LoadNonAssortedOrderSODDetails()
        'Try

        Dim daSelArticleInfo As New SqlDataAdapter("Select * from SalesOrderDetails where ID = '" & sSalesOrderDetailsId & "'", sCnn)
        Dim dsSelArticleInfo As New DataSet
        daSelArticleInfo.Fill(dsSelArticleInfo)

        sArticleId = dsSelArticleInfo.Tables(0).Rows(0).Item("ArticleDetailID")
        sArticleGroup = dsSelArticleInfo.Tables(0).Rows(0).Item("ArticleGroup")
        sArticleName = dsSelArticleInfo.Tables(0).Rows(0).Item("Article")
        sArticleColour = dsSelArticleInfo.Tables(0).Rows(0).Item("ColorName")

        tbOS01.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size01")
        tbOS02.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size02")
        tbOS03.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size03")
        tbOS04.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size04")
        tbOS05.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size05")
        tbOS06.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size06")
        tbOS07.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size07")
        tbOS08.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size08")
        tbOS09.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size09")
        tbOS10.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size10")
        tbOS11.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size11")
        tbOS12.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size12")
        tbOS13.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size13")
        tbOS14.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size14")
        tbOS15.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size15")
        tbOS16.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size16")
        tbOS17.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size17")
        tbOS18.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Size18")

        tbSODQty01.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity01")
        tbSODQty02.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity02")
        tbSODQty03.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity03")
        tbSODQty04.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity04")
        tbSODQty05.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity05")
        tbSODQty06.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity06")
        tbSODQty07.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity07")
        tbSODQty08.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity08")
        tbSODQty09.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity09")
        tbSODQty10.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity10")
        tbSODQty11.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity11")
        tbSODQty12.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity12")
        tbSODQty13.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity13")
        tbSODQty14.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity14")
        tbSODQty15.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity15")
        tbSODQty16.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity16")
        tbSODQty17.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity17")
        tbSODQty18.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("Quantity18")
        tbSODTotal.Text = dsSelArticleInfo.Tables(0).Rows(0).Item("OrderQuantity")


        If Val(tbSODQty01.Text) = 0 Then : tbSODQty01.Clear() : End If
        If Val(tbSODQty02.Text) = 0 Then : tbSODQty02.Clear() : End If
        If Val(tbSODQty03.Text) = 0 Then : tbSODQty03.Clear() : End If
        If Val(tbSODQty04.Text) = 0 Then : tbSODQty04.Clear() : End If
        If Val(tbSODQty05.Text) = 0 Then : tbSODQty05.Clear() : End If
        If Val(tbSODQty06.Text) = 0 Then : tbSODQty06.Clear() : End If
        If Val(tbSODQty07.Text) = 0 Then : tbSODQty07.Clear() : End If
        If Val(tbSODQty08.Text) = 0 Then : tbSODQty08.Clear() : End If
        If Val(tbSODQty09.Text) = 0 Then : tbSODQty09.Clear() : End If
        If Val(tbSODQty10.Text) = 0 Then : tbSODQty10.Clear() : End If
        If Val(tbSODQty11.Text) = 0 Then : tbSODQty11.Clear() : End If
        If Val(tbSODQty12.Text) = 0 Then : tbSODQty12.Clear() : End If
        If Val(tbSODQty13.Text) = 0 Then : tbSODQty13.Clear() : End If
        If Val(tbSODQty14.Text) = 0 Then : tbSODQty14.Clear() : End If
        If Val(tbSODQty15.Text) = 0 Then : tbSODQty15.Clear() : End If
        If Val(tbSODQty16.Text) = 0 Then : tbSODQty16.Clear() : End If
        If Val(tbSODQty17.Text) = 0 Then : tbSODQty17.Clear() : End If
        If Val(tbSODQty18.Text) = 0 Then : tbSODQty18.Clear() : End If

        'Catch ex As Exception

        'End Try
    End Sub

    
    Dim nSlNo, nTotalPkd As Integer
    Dim sId, sUpdated As String

    Private Sub UpdateJCPackInfo()
        'Try
        
        sUpdated = "N"
        If Trim(tbPackingJobcardNo.Text) = Trim(tbJobcardNo.Text) Then
            'If Val(tbOQ01.Text) > Val(tbJCOPBalQty01.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ02.Text) > Val(tbJCOPBalQty02.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ03.Text) > Val(tbJCOPBalQty03.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ04.Text) > Val(tbJCOPBalQty04.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ05.Text) > Val(tbJCOPBalQty05.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ06.Text) > Val(tbJCOPBalQty06.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ07.Text) > Val(tbJCOPBalQty07.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ08.Text) > Val(tbJCOPBalQty08.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ09.Text) > Val(tbJCOPBalQty09.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS09.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ10.Text) > Val(tbJCOPBalQty10.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS10.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If

            'If Val(tbOQ11.Text) > Val(tbJCOPBalQty11.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ12.Text) > Val(tbJCOPBalQty12.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ13.Text) > Val(tbJCOPBalQty13.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ14.Text) > Val(tbJCOPBalQty14.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ15.Text) > Val(tbJCOPBalQty15.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ16.Text) > Val(tbJCOPBalQty16.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ17.Text) > Val(tbJCOPBalQty17.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbOQ18.Text) > Val(tbJCOPBalQty18.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If

            If Val(tbOQ01.Text) > 0 And Val(tbOQ01.Text) > (Val(tbJCQty01.Text) - (Val(tbJCQPty01.Text) + Val(tbJCOPQty01.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ02.Text) > 0 And Val(tbOQ02.Text) > (Val(tbJCQty02.Text) - (Val(tbJCQPty02.Text) + Val(tbJCOPQty02.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ03.Text) > 0 And Val(tbOQ03.Text) > (Val(tbJCQty03.Text) - (Val(tbJCQPty03.Text) + Val(tbJCOPQty03.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ04.Text) > 0 And Val(tbOQ04.Text) > (Val(tbJCQty04.Text) - (Val(tbJCQPty04.Text) + Val(tbJCOPQty04.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ05.Text) > 0 And Val(tbOQ05.Text) > (Val(tbJCQty05.Text) - (Val(tbJCQPty05.Text) + Val(tbJCOPQty05.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ06.Text) > 0 And Val(tbOQ06.Text) > (Val(tbJCQty06.Text) - (Val(tbJCQPty06.Text) + Val(tbJCOPQty06.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ07.Text) > 0 And Val(tbOQ07.Text) > (Val(tbJCQty07.Text) - (Val(tbJCQPty07.Text) + Val(tbJCOPQty07.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ08.Text) > 0 And Val(tbOQ08.Text) > (Val(tbJCQty08.Text) - (Val(tbJCQPty08.Text) + Val(tbJCOPQty08.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ09.Text) > 0 And Val(tbOQ09.Text) > (Val(tbJCQty09.Text) - (Val(tbJCQPty09.Text) + Val(tbJCOPQty09.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS09.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ10.Text) > 0 And Val(tbOQ10.Text) > (Val(tbJCQty10.Text) - (Val(tbJCQPty10.Text) + Val(tbJCOPQty10.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS10.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If

            If Val(tbOQ11.Text) > 0 And Val(tbOQ11.Text) > (Val(tbJCQty11.Text) - (Val(tbJCQPty11.Text) + Val(tbJCOPQty11.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ12.Text) > 0 And Val(tbOQ12.Text) > (Val(tbJCQty12.Text) - (Val(tbJCQPty12.Text) + Val(tbJCOPQty12.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ13.Text) > 0 And Val(tbOQ13.Text) > (Val(tbJCQty13.Text) - (Val(tbJCQPty13.Text) + Val(tbJCOPQty13.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ14.Text) > 0 And Val(tbOQ14.Text) > (Val(tbJCQty14.Text) - (Val(tbJCQPty14.Text) + Val(tbJCOPQty14.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ15.Text) > 0 And Val(tbOQ15.Text) > (Val(tbJCQty15.Text) - (Val(tbJCQPty15.Text) + Val(tbJCOPQty15.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ16.Text) > 0 And Val(tbOQ16.Text) > (Val(tbJCQty16.Text) - (Val(tbJCQPty16.Text) + Val(tbJCOPQty16.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ17.Text) > 0 And Val(tbOQ17.Text) > (Val(tbJCQty17.Text) - (Val(tbJCQPty17.Text) + Val(tbJCOPQty17.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            If Val(tbOQ18.Text) > 0 And Val(tbOQ18.Text) > (Val(tbJCQty18.Text) - (Val(tbJCQPty18.Text) + Val(tbJCOPQty18.Text))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If

        Else
            If (Val(tbSameJCQty01.Text) + Val(tbOtherJCQty01.Text) > 0) And (Val(tbSameJCQty01.Text) + Val(tbOtherJCQty01.Text) > (Val(tbJCQty01.Text) - (Val(tbJCQPty01.Text) + Val(tbJCOPQty01.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty02.Text) + Val(tbOtherJCQty02.Text) > 0) And (Val(tbSameJCQty02.Text) + Val(tbOtherJCQty02.Text) > (Val(tbJCQty02.Text) - (Val(tbJCQPty02.Text) + Val(tbJCOPQty02.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty03.Text) + Val(tbOtherJCQty03.Text) > 0) And (Val(tbSameJCQty03.Text) + Val(tbOtherJCQty03.Text) > (Val(tbJCQty03.Text) - (Val(tbJCQPty03.Text) + Val(tbJCOPQty03.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty04.Text) + Val(tbOtherJCQty04.Text) > 0) And (Val(tbSameJCQty04.Text) + Val(tbOtherJCQty04.Text) > (Val(tbJCQty04.Text) - (Val(tbJCQPty04.Text) + Val(tbJCOPQty04.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty05.Text) + Val(tbOtherJCQty05.Text) > 0) And (Val(tbSameJCQty05.Text) + Val(tbOtherJCQty05.Text) > (Val(tbJCQty05.Text) - (Val(tbJCQPty05.Text) + Val(tbJCOPQty05.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty06.Text) + Val(tbOtherJCQty06.Text) > 0) And (Val(tbSameJCQty06.Text) + Val(tbOtherJCQty06.Text) > (Val(tbJCQty06.Text) - (Val(tbJCQPty06.Text) + Val(tbJCOPQty06.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty07.Text) + Val(tbOtherJCQty07.Text) > 0) And (Val(tbSameJCQty07.Text) + Val(tbOtherJCQty07.Text) > (Val(tbJCQty07.Text) - (Val(tbJCQPty07.Text) + Val(tbJCOPQty07.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty08.Text) + Val(tbOtherJCQty08.Text) > 0) And (Val(tbSameJCQty08.Text) + Val(tbOtherJCQty08.Text) > (Val(tbJCQty08.Text) - (Val(tbJCQPty08.Text) + Val(tbJCOPQty08.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty09.Text) + Val(tbOtherJCQty09.Text) > 0) And (Val(tbSameJCQty09.Text) + Val(tbOtherJCQty09.Text) > (Val(tbJCQty09.Text) - (Val(tbJCQPty09.Text) + Val(tbJCOPQty09.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS09.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty10.Text) + Val(tbOtherJCQty10.Text) > 0) And (Val(tbSameJCQty10.Text) + Val(tbOtherJCQty10.Text) > (Val(tbJCQty10.Text) - (Val(tbJCQPty10.Text) + Val(tbJCOPQty10.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS10.Text) + "", MsgBoxStyle.Critical) : End If

            If (Val(tbSameJCQty11.Text) + Val(tbOtherJCQty11.Text) > 0) And (Val(tbSameJCQty11.Text) + Val(tbOtherJCQty11.Text) > (Val(tbJCQty11.Text) - (Val(tbJCQPty11.Text) + Val(tbJCOPQty11.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS11.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty12.Text) + Val(tbOtherJCQty12.Text) > 0) And (Val(tbSameJCQty12.Text) + Val(tbOtherJCQty12.Text) > (Val(tbJCQty12.Text) - (Val(tbJCQPty12.Text) + Val(tbJCOPQty12.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS12.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty13.Text) + Val(tbOtherJCQty13.Text) > 0) And (Val(tbSameJCQty13.Text) + Val(tbOtherJCQty13.Text) > (Val(tbJCQty13.Text) - (Val(tbJCQPty13.Text) + Val(tbJCOPQty13.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS13.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty14.Text) + Val(tbOtherJCQty14.Text) > 0) And (Val(tbSameJCQty14.Text) + Val(tbOtherJCQty14.Text) > (Val(tbJCQty14.Text) - (Val(tbJCQPty14.Text) + Val(tbJCOPQty14.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS14.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty15.Text) + Val(tbOtherJCQty15.Text) > 0) And (Val(tbSameJCQty15.Text) + Val(tbOtherJCQty15.Text) > (Val(tbJCQty15.Text) - (Val(tbJCQPty15.Text) + Val(tbJCOPQty15.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS15.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty16.Text) + Val(tbOtherJCQty16.Text) > 0) And (Val(tbSameJCQty16.Text) + Val(tbOtherJCQty16.Text) > (Val(tbJCQty16.Text) - (Val(tbJCQPty16.Text) + Val(tbJCOPQty16.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS16.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty17.Text) + Val(tbOtherJCQty17.Text) > 0) And (Val(tbSameJCQty17.Text) + Val(tbOtherJCQty17.Text) > (Val(tbJCQty17.Text) - (Val(tbJCQPty17.Text) + Val(tbJCOPQty17.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS17.Text) + "", MsgBoxStyle.Critical) : End If
            If (Val(tbSameJCQty18.Text) + Val(tbOtherJCQty18.Text) > 0) And (Val(tbSameJCQty18.Text) + Val(tbOtherJCQty18.Text) > (Val(tbJCQty18.Text) - (Val(tbJCQPty18.Text) + Val(tbJCOPQty18.Text)))) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS18.Text) + "", MsgBoxStyle.Critical) : End If


            'If Val(tbSameJCQty01.Text) > Val(tbJCOPBalQty01.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty02.Text) > Val(tbJCOPBalQty02.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty03.Text) > Val(tbJCOPBalQty03.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty04.Text) > Val(tbJCOPBalQty04.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty05.Text) > Val(tbJCOPBalQty05.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty06.Text) > Val(tbJCOPBalQty06.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty07.Text) > Val(tbJCOPBalQty07.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty08.Text) > Val(tbJCOPBalQty08.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty09.Text) > Val(tbJCOPBalQty09.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS09.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty10.Text) > Val(tbJCOPBalQty10.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS10.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If

            'If Val(tbSameJCQty11.Text) > Val(tbJCOPBalQty11.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS01.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty12.Text) > Val(tbJCOPBalQty12.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS02.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty13.Text) > Val(tbJCOPBalQty13.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS03.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty14.Text) > Val(tbJCOPBalQty14.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS04.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty15.Text) > Val(tbJCOPBalQty15.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS05.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty16.Text) > Val(tbJCOPBalQty16.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS06.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty17.Text) > Val(tbJCOPBalQty17.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS07.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If
            'If Val(tbSameJCQty18.Text) > Val(tbJCOPBalQty18.Text) Then : MsgBox("Quantity Mismatch in Size " + Trim(tbOS08.Text) + "", MsgBoxStyle.Critical) : Exit Sub : End If


        End If

        Dim daSelJCPackInfo As New SqlDataAdapter("Select * from JCPackInfoMain where JobcardNo = '" & sJobcardNo & "'", sCnn)
        Dim dsSelJCPackInfo As New DataSet
        daSelJCPackInfo.Fill(dsSelJCPackInfo)

        If dsSelJCPackInfo.Tables(0).Rows.Count = 0 Then
            Dim daNewJCPackInfo As New SqlDataAdapter("Select IsNull(Max(Slno),0) As SlNo From JCPackInfoMain", sCnn)
            Dim dsNewJCPackInfo As New DataSet
            daNewJCPackInfo.Fill(dsNewJCPackInfo)

            nSlNo = dsNewJCPackInfo.Tables(0).Rows(0).Item("SlNo") + 1

            sId = System.Guid.NewGuid.ToString()
            Dim daInsJCPackInfo As New SqlDataAdapter("Insert into JCPackInfoMain(ID,JobcardNo,Slno,DOP) Values ('" & sId & "','" & sJobcardNo & _
                                                      "','" & nSlNo & "','" & Format(Date.Now, "dd-MMM-yyyy") & "')", sCnn)
            Dim dsInsJCPackInfo As New DataSet
            daInsJCPackInfo.Fill(dsInsJCPackInfo)
            dsInsJCPackInfo.AcceptChanges()
        End If

        Dim dsNewJCPackInfoDtl As New DataSet
        Dim dsInsJCPackInfoDtl As New DataSet

        Dim daNewJCPackInfoDtl As New SqlDataAdapter("Select IsNull(Max(Slno),0) As SlNo From JCPackInfoDetail", sCnn)
        daNewJCPackInfoDtl.Fill(dsNewJCPackInfoDtl)

        nSlNo = dsNewJCPackInfoDtl.Tables(0).Rows(0).Item("SlNo") + 1


        If Trim(tbPackingJobcardNo.Text) = Trim(tbJobcardNo.Text) Then

            sId = System.Guid.NewGuid.ToString()
            Dim daInsJCPackInfoDtl As New SqlDataAdapter("Insert into JCPackInfoDetail Values ('" & sId & "','','','','','','','','','','" & sJobcardNo & _
                                                         "','" & sPackingJobcardNo & "','C','" & Val(tbOQTotal.Text) & _
                                                         "','" & Trim(tbOuterCartonNo.Text) & _
                                                         "','" & Val(tbOQ01.Text) & "','" & Val(tbOQ02.Text) & "','" & Val(tbOQ03.Text) & _
                                                         "','" & Val(tbOQ04.Text) & "','" & Val(tbOQ05.Text) & "','" & Val(tbOQ06.Text) & _
                                                         "','" & Val(tbOQ07.Text) & "','" & Val(tbOQ08.Text) & "','" & Val(tbOQ09.Text) & _
                                                         "','" & Val(tbOQ10.Text) & "','" & Val(tbOQ11.Text) & "','" & Val(tbOQ12.Text) & _
                                                         "','" & Val(tbOQ13.Text) & "','" & Val(tbOQ14.Text) & "','" & Val(tbOQ15.Text) & _
                                                         "','" & Val(tbOQ16.Text) & "','" & Val(tbOQ15.Text) & "','" & Val(tbOQ18.Text) & _
                                                         "','" & Format(Date.Now, "dd-MMM-yyyy") & "','" & nSlNo & "','1')", sCnn)
            daInsJCPackInfoDtl.Fill(dsInsJCPackInfoDtl)
            dsInsJCPackInfoDtl.AcceptChanges()

        Else

            If (Val(tbAlreadyPkdQty.Text) + Val(tbSameJCQtyTotal.Text) + Val(tbOtherJCQtyTotal.Text) > Val(tbOQTotal.Text)) Or (Val(tbSameJCQtyTotal.Text) + Val(tbOtherJCQtyTotal.Text) = 0) Then
                MsgBox("Part Quantity packing exceeds the Original Box Quantity", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If Val(tbSameJCQtyTotal.Text) > 0 Then

                sId = System.Guid.NewGuid.ToString()
                Dim daInsJCPackInfoDtl As New SqlDataAdapter("Insert into JCPackInfoDetail Values ('" & sId & "','','','','','','','','','','" & sJobcardNo & _
                                                             "','" & sJobcardNo & "','P','" & Val(tbSameJCQtyTotal.Text) & _
                                                             "','" & Trim(tbOuterCartonNo.Text) & _
                                                             "','" & Val(tbSameJCQty01.Text) & "','" & Val(tbSameJCQty02.Text) & "','" & Val(tbSameJCQty03.Text) & _
                                                             "','" & Val(tbSameJCQty04.Text) & "','" & Val(tbSameJCQty05.Text) & "','" & Val(tbSameJCQty06.Text) & _
                                                             "','" & Val(tbSameJCQty07.Text) & "','" & Val(tbSameJCQty08.Text) & "','" & Val(tbSameJCQty09.Text) & _
                                                             "','" & Val(tbSameJCQty10.Text) & "','" & Val(tbSameJCQty11.Text) & "','" & Val(tbSameJCQty12.Text) & _
                                                             "','" & Val(tbSameJCQty13.Text) & "','" & Val(tbSameJCQty14.Text) & "','" & Val(tbSameJCQty15.Text) & _
                                                             "','" & Val(tbSameJCQty16.Text) & "','" & Val(tbSameJCQty17.Text) & "','" & Val(tbSameJCQty18.Text) & _
                                                             "','" & Format(Date.Now, "dd-MMM-yyyy") & "','" & nSlNo & "','0')", sCnn)
                daInsJCPackInfoDtl.Fill(dsInsJCPackInfoDtl)
                dsInsJCPackInfoDtl.AcceptChanges()
            Else
                Dim daSelJCPackInfoDtl As New SqlDataAdapter("Select * from JCPackInfoDetail Where MainJCNo = '" & sJobcardNo & _
                                                             "' And PkgJCNo = '" & sJobcardNo & _
                                                             "' And PkgBarcode = '" & Trim(tbOuterCartonNo.Text) & "'", sCnn)
                Dim dsSelJCPackInfoDtl As New DataSet
                daSelJCPackInfoDtl.Fill(dsSelJCPackInfoDtl)

                If dsSelJCPackInfoDtl.Tables(0).Rows.Count = 0 Then
                    MsgBox("Minimum of 1 Pair of Same Jobcard should be Packed in the Box", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim daSelTtlPkdABarcode As New SqlDataAdapter("Select IsNull(Sum(PKJCQty),0) As PKJCQty From JCPackInfoDetail WHERE PkgBarcode = '" & Trim(tbOuterCartonNo.Text) & "'", sCnn)
                Dim dsSelTtlPkdABarcode As New DataSet
                daSelTtlPkdABarcode.Fill(dsSelTtlPkdABarcode)

                nTotalPkd = dsSelTtlPkdABarcode.Tables(0).Rows(0).Item("PKJCQty")

                If nTotalPkd + Val(tbOtherJCQtyTotal.Text) > Val(tbOQTotal.Text) Then
                    MsgBox("Part Quantity packing exceeds the Original Box Quantity", MsgBoxStyle.Critical)
                    Exit Sub
                End If

            End If
            Dim daNewJCPackInfoDtl1 As New SqlDataAdapter("Select IsNull(Max(Slno),0) As SlNo From JCPackInfoDetail", sCnn)
            Dim dsNewJCPackInfoDtl1 As New DataSet
            daNewJCPackInfoDtl1.Fill(dsNewJCPackInfoDtl1)

            nSlNo = dsNewJCPackInfoDtl1.Tables(0).Rows(0).Item("SlNo") + 1

            sId = System.Guid.NewGuid.ToString()
            Dim daInsJCPackInfoDtl1 As New SqlDataAdapter("Insert into JCPackInfoDetail Values ('" & sId & "','','','','','','','','','','" & sJobcardNo & _
                                                         "','" & sPackingJobcardNo & "','P','" & Val(tbOtherJCQtyTotal.Text) & _
                                                         "','" & Trim(tbOuterCartonNo.Text) & _
                                                         "','" & Val(tbOtherJCQty01.Text) & "','" & Val(tbOtherJCQty02.Text) & "','" & Val(tbOtherJCQty03.Text) & _
                                                         "','" & Val(tbOtherJCQty04.Text) & "','" & Val(tbOtherJCQty05.Text) & "','" & Val(tbOtherJCQty06.Text) & _
                                                         "','" & Val(tbOtherJCQty07.Text) & "','" & Val(tbOtherJCQty08.Text) & "','" & Val(tbOtherJCQty09.Text) & _
                                                         "','" & Val(tbOtherJCQty10.Text) & "','" & Val(tbOtherJCQty11.Text) & "','" & Val(tbOtherJCQty12.Text) & _
                                                         "','" & Val(tbOtherJCQty13.Text) & "','" & Val(tbOtherJCQty14.Text) & "','" & Val(tbOtherJCQty15.Text) & _
                                                         "','" & Val(tbOtherJCQty16.Text) & "','" & Val(tbOtherJCQty17.Text) & "','" & Val(tbOtherJCQty18.Text) & _
                                                         "','" & Format(Date.Now, "dd-MMM-yyyy") & "','" & nSlNo & "','0')", sCnn)
            Dim dsInsJCPackInfoDtl1 As New DataSet
            daInsJCPackInfoDtl1.Fill(dsInsJCPackInfoDtl1)
            dsInsJCPackInfoDtl1.AcceptChanges()

        End If

        Dim nTotalPkdQty, nSJCPkdQty, nOJCCount, nOJCPkdQty As Integer

        ''For Total Packed Quantity in JCPackInfo Main''
        Dim daSelTtlPkd As New SqlDataAdapter("Select IsNull(Sum(PKJCQty),0) As PKDQty from JCPackInfoDetail Where MainJCNo = '" & sJobcardNo & "'", sCnn)
        Dim dsSelTtlPkd As New DataSet
        daSelTtlPkd.Fill(dsSelTtlPkd)

        nTotalPkdQty = dsSelTtlPkd.Tables(0).Rows(0).Item("PKDQty")
        ''For Total Packed Quantity in JCPackInfo Main''

        ''For Single Jobcard Packed Quantity in JCPackInfo Main''
        Dim daSelSJCPkd As New SqlDataAdapter("Select IsNull(Sum(PKJCQty),0) As PKDQty from JCPackInfoDetail Where MainJCNo = '" & sJobcardNo & _
                                              "' And PkgJCNo = '" & sJobcardNo & "'", sCnn)
        Dim dsSelSJCPkd As New DataSet
        daSelSJCPkd.Fill(dsSelSJCPkd)

        nSJCPkdQty = dsSelSJCPkd.Tables(0).Rows(0).Item("PKDQty")
        ''For Single Jobcard Packed Quantity in JCPackInfo Main''

        ''For Other Jobcard Count & Packed Quantity in JCPackInfo Main''
        Dim daSelOJCPkd As New SqlDataAdapter("Select IsNull(Count(Distinct PkgJCNo),0) As OJCCount,IsNull(Sum(PKJCQty),0) As PKDQty from JCPackInfoDetail Where MainJCNo = '" & sJobcardNo & _
                                              "' And PkgJCNo <> '" & sJobcardNo & "' And Type <> 'C'", sCnn)
        Dim dsSelOJCPkd As New DataSet
        daSelOJCPkd.Fill(dsSelOJCPkd)

        nOJCCount = dsSelOJCPkd.Tables(0).Rows(0).Item("OJCCount")
        nOJCPkdQty = dsSelOJCPkd.Tables(0).Rows(0).Item("PKDQty")
        ''For Other Jobcard Count &  Packed Quantity in JCPackInfo Main''

        Dim daUpdJCMain As New SqlDataAdapter("Update JCPackInfoMain Set TotalPkdQty = '" & nTotalPkdQty & _
                                              "', SJCPkdQty = '" & nSJCPkdQty & _
                                              "', OJCCount = '" & nOJCCount & _
                                              "', OJCPkdQty = '" & nOJCPkdQty & _
                                              "' Where JobcardNo = '" & sJobcardNo & "'", sCnn)
        Dim dsUpdJCMain As New DataSet
        daUpdJCMain.Fill(dsUpdJCMain)
        dsUpdJCMain.AcceptChanges()


        Dim daSelTtlPkdABarcode1 As New SqlDataAdapter("Select IsNull(Sum(PKJCQty),0) As PKJCQty From JCPackInfoDetail WHERE PkgBarcode = '" & Trim(tbOuterCartonNo.Text) & "'", sCnn)
        Dim dsSelTtlPkdABarcode1 As New DataSet
        daSelTtlPkdABarcode1.Fill(dsSelTtlPkdABarcode1)

        nTotalPkd = dsSelTtlPkdABarcode1.Tables(0).Rows(0).Item("PKJCQty")

        If nTotalPkd = Val(tbOQTotal.Text) Then
            sUpdated = "Y"
            If Trim(tbPackingJobcardNo.Text) <> Trim(tbJobcardNo.Text) Then
                Dim daUpdJCPackInfoDtl As New SqlDataAdapter("Update JCPackInfoDetail Set IsCompleted = '1' Where MainJCNo = '" & sJobcardNo & _
                                                             "' And PkgJCNo = '" & sJobcardNo & "' And PkgBarcode = '" & Trim(tbOuterCartonNo.Text) & "'", sCnn)
                Dim dsUpdJCPackInfoDtl As New DataSet
                daUpdJCPackInfoDtl.Fill(dsUpdJCPackInfoDtl)
                dsUpdJCPackInfoDtl.AcceptChanges()
            End If

        Else
            sUpdated = "N"
        End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub tbSameJCQty01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSameJCQty01.TextChanged, tbSameJCQty02.TextChanged, tbSameJCQty03.TextChanged, tbSameJCQty04.TextChanged, tbSameJCQty05.TextChanged, tbSameJCQty06.TextChanged, tbSameJCQty07.TextChanged, tbSameJCQty08.TextChanged, tbSameJCQty09.TextChanged, tbSameJCQty10.TextChanged, tbSameJCQty11.TextChanged, tbSameJCQty12.TextChanged, tbSameJCQty13.TextChanged, tbSameJCQty14.TextChanged, tbSameJCQty15.TextChanged, tbSameJCQty16.TextChanged, tbSameJCQty17.TextChanged, tbSameJCQty18.TextAlignChanged
        tbSameJCQtyTotal.Text = Val(tbSameJCQty01.Text) + Val(tbSameJCQty02.Text) + Val(tbSameJCQty03.Text) + Val(tbSameJCQty04.Text) + Val(tbSameJCQty05.Text) + Val(tbSameJCQty06.Text) + Val(tbSameJCQty07.Text) + Val(tbSameJCQty08.Text) + Val(tbSameJCQty09.Text) + Val(tbSameJCQty10.Text) + Val(tbSameJCQty11.Text) + Val(tbSameJCQty12.Text) + Val(tbSameJCQty13.Text) + Val(tbSameJCQty14.Text) + Val(tbSameJCQty15.Text) + Val(tbSameJCQty16.Text) + Val(tbSameJCQty17.Text) + Val(tbSameJCQty18.Text)
    End Sub

    Private Sub tbotherJCQty01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbOtherJCQty01.TextChanged, tbOtherJCQty02.TextChanged, tbOtherJCQty03.TextChanged, tbOtherJCQty04.TextChanged, tbOtherJCQty05.TextChanged, tbOtherJCQty06.TextChanged, tbOtherJCQty07.TextChanged, tbOtherJCQty08.TextChanged, tbOtherJCQty09.TextChanged, tbOtherJCQty10.TextChanged, tbOtherJCQty11.TextChanged, tbOtherJCQty12.TextChanged, tbOtherJCQty13.TextChanged, tbOtherJCQty14.TextChanged, tbOtherJCQty15.TextChanged, tbOtherJCQty16.TextChanged, tbOtherJCQty17.TextChanged, tbOtherJCQty18.TextAlignChanged
        tbOtherJCQtyTotal.Text = Val(tbOtherJCQty01.Text) + Val(tbOtherJCQty02.Text) + Val(tbOtherJCQty03.Text) + Val(tbOtherJCQty04.Text) + Val(tbOtherJCQty05.Text) + Val(tbOtherJCQty06.Text) + Val(tbOtherJCQty07.Text) + Val(tbOtherJCQty08.Text) + Val(tbOtherJCQty09.Text) + Val(tbOtherJCQty10.Text) + Val(tbOtherJCQty11.Text) + Val(tbOtherJCQty12.Text) + Val(tbOtherJCQty13.Text) + Val(tbOtherJCQty14.Text) + Val(tbOtherJCQty15.Text) + Val(tbOtherJCQty16.Text) + Val(tbOtherJCQty17.Text) + Val(tbOtherJCQty18.Text)
    End Sub

    Private Sub LoadPackingInfo()

        'Try

        Dim daSelSJPKDInfo As New SqlDataAdapter("SELECT MainJCNo, SUM(Quantity01) AS Quantity01, SUM(Quantity02) AS Quantity02, SUM(Quantity03) AS Quantity03, SUM(Quantity04) AS Quantity04, SUM(Quantity05) AS Quantity05, SUM(Quantity06) AS Quantity06, Sum(Quantity07) As Quantity07, SUM(Quantity08) AS Quantity08, SUM(Quantity09) AS Quantity09, SUM(Quantity10) AS Quantity10, SUM(Quantity11) AS Quantity11, SUM(Quantity12) AS Quantity12, SUM(Quantity13) AS Quantity13, SUM(Quantity14) AS Quantity14, SUM(Quantity15) AS Quantity15, SUM(Quantity16) AS Quantity16, SUM(Quantity17) AS Quantity17, SUM(Quantity18) AS Quantity18, SUM(PKJCQty) AS TotalQuantity FROM dbo.JCPackInfoDetail Where MainjCNo = '" & sJobcardNo & _
                                                 "' GROUP BY MainJCNo", sConstr)

        Dim dsSelSJPKDInfo As New DataSet
        daSelSJPKDInfo.Fill(dsSelSJPKDInfo)

        If dsSelSJPKDInfo.Tables(0).Rows.Count = 1 Then
            tbJCQPty01.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity01").ToString)
            tbJCQPty02.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity02").ToString)
            tbJCQPty03.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity03").ToString)
            tbJCQPty04.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity04").ToString)
            tbJCQPty05.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity05").ToString)
            tbJCQPty06.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity06").ToString)
            tbJCQPty07.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity07").ToString)
            tbJCQPty08.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity08").ToString)
            tbJCQPty09.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity09").ToString)
            tbJCQPty10.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity10").ToString)
            tbJCQPty11.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity11").ToString)
            tbJCQPty12.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity12").ToString)
            tbJCQPty13.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity13").ToString)
            tbJCQPty14.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity14").ToString)
            tbJCQPty15.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity15").ToString)
            tbJCQPty16.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity16").ToString)
            tbJCQPty17.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity17").ToString)
            tbJCQPty18.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("Quantity18").ToString)
            tbJCQPtyTotal.Text = Val(dsSelSJPKDInfo.Tables(0).Rows(0).Item("TotalQuantity").ToString)

            If Val(tbJCQPty01.Text) = 0 Then : tbJCQPty01.Clear() : End If
            If Val(tbJCQPty02.Text) = 0 Then : tbJCQPty02.Clear() : End If
            If Val(tbJCQPty03.Text) = 0 Then : tbJCQPty03.Clear() : End If
            If Val(tbJCQPty04.Text) = 0 Then : tbJCQPty04.Clear() : End If
            If Val(tbJCQPty05.Text) = 0 Then : tbJCQPty05.Clear() : End If
            If Val(tbJCQPty06.Text) = 0 Then : tbJCQPty06.Clear() : End If
            If Val(tbJCQPty07.Text) = 0 Then : tbJCQPty07.Clear() : End If
            If Val(tbJCQPty08.Text) = 0 Then : tbJCQPty08.Clear() : End If
            If Val(tbJCQPty09.Text) = 0 Then : tbJCQPty09.Clear() : End If
            If Val(tbJCQPty10.Text) = 0 Then : tbJCQPty10.Clear() : End If
            If Val(tbJCQPty11.Text) = 0 Then : tbJCQPty11.Clear() : End If
            If Val(tbJCQPty12.Text) = 0 Then : tbJCQPty12.Clear() : End If
            If Val(tbJCQPty13.Text) = 0 Then : tbJCQPty13.Clear() : End If
            If Val(tbJCQPty14.Text) = 0 Then : tbJCQPty14.Clear() : End If
            If Val(tbJCQPty15.Text) = 0 Then : tbJCQPty15.Clear() : End If
            If Val(tbJCQPty16.Text) = 0 Then : tbJCQPty16.Clear() : End If
            If Val(tbJCQPty17.Text) = 0 Then : tbJCQPty17.Clear() : End If
            If Val(tbJCQPty18.Text) = 0 Then : tbJCQPty18.Clear() : End If

        Else
            tbJCQPty01.Clear() : tbJCQPty02.Clear() : tbJCQPty03.Clear() : tbJCQPty04.Clear() : tbJCQPty05.Clear() : tbJCQPty06.Clear()
            tbJCQPty07.Clear() : tbJCQPty08.Clear() : tbJCQPty09.Clear() : tbJCQPty10.Clear() : tbJCQPty11.Clear() : tbJCQPty12.Clear()
            tbJCQPty13.Clear() : tbJCQPty14.Clear() : tbJCQPty15.Clear() : tbJCQPty16.Clear() : tbJCQPty17.Clear() : tbJCQPty18.Clear()
        End If


        Dim daSelOJPKDInfo As New SqlDataAdapter("SELECT MainJCNo, SUM(Quantity01) AS Quantity01, SUM(Quantity02) AS Quantity02, SUM(Quantity03) AS Quantity03, SUM(Quantity04) AS Quantity04, SUM(Quantity05) AS Quantity05, SUM(Quantity06) AS Quantity06, Sum(Quantity07) As Quantity07, SUM(Quantity08) AS Quantity08, SUM(Quantity09) AS Quantity09, SUM(Quantity10) AS Quantity10, SUM(Quantity11) AS Quantity11, SUM(Quantity12) AS Quantity12, SUM(Quantity13) AS Quantity13, SUM(Quantity14) AS Quantity14, SUM(Quantity15) AS Quantity15, SUM(Quantity16) AS Quantity16, SUM(Quantity17) AS Quantity17, SUM(Quantity18) AS Quantity18, SUM(PKJCQty) AS TotalQuantity FROM dbo.JCPackInfoDetail Where PkgJCNo = '" & sJobcardNo & _
                                                "' And MainJCNo <> '" & sJobcardNo & "' GROUP BY MainJCNo", sConstr)

        Dim dsSelOJPKDInfo As New DataSet
        daSelOJPKDInfo.Fill(dsSelOJPKDInfo)

        If dsSelOJPKDInfo.Tables(0).Rows.Count = 1 Then
            tbJCOPQty01.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity01").ToString)
            tbJCOPQty02.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity02").ToString)
            tbJCOPQty03.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity03").ToString)
            tbJCOPQty04.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity04").ToString)
            tbJCOPQty05.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity05").ToString)
            tbJCOPQty06.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity06").ToString)
            tbJCOPQty07.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity07").ToString)
            tbJCOPQty08.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity08").ToString)
            tbJCOPQty09.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity09").ToString)
            tbJCOPQty10.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity10").ToString)
            tbJCOPQty11.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity11").ToString)
            tbJCOPQty12.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity12").ToString)
            tbJCOPQty13.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity13").ToString)
            tbJCOPQty14.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity14").ToString)
            tbJCOPQty15.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity15").ToString)
            tbJCOPQty16.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity16").ToString)
            tbJCOPQty17.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity17").ToString)
            tbJCOPQty18.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("Quantity18").ToString)
            tbJCOPQtyTotal.Text = Val(dsSelOJPKDInfo.Tables(0).Rows(0).Item("TotalQuantity").ToString)

            If Val(tbJCOPQty01.Text) = 0 Then : tbJCOPQty01.Clear() : End If
            If Val(tbJCOPQty02.Text) = 0 Then : tbJCOPQty02.Clear() : End If
            If Val(tbJCOPQty03.Text) = 0 Then : tbJCOPQty03.Clear() : End If
            If Val(tbJCOPQty04.Text) = 0 Then : tbJCOPQty04.Clear() : End If
            If Val(tbJCOPQty05.Text) = 0 Then : tbJCOPQty05.Clear() : End If
            If Val(tbJCOPQty06.Text) = 0 Then : tbJCOPQty06.Clear() : End If
            If Val(tbJCOPQty07.Text) = 0 Then : tbJCOPQty07.Clear() : End If
            If Val(tbJCOPQty08.Text) = 0 Then : tbJCOPQty08.Clear() : End If
            If Val(tbJCOPQty09.Text) = 0 Then : tbJCOPQty09.Clear() : End If
            If Val(tbJCOPQty10.Text) = 0 Then : tbJCOPQty10.Clear() : End If
            If Val(tbJCOPQty11.Text) = 0 Then : tbJCOPQty11.Clear() : End If
            If Val(tbJCOPQty12.Text) = 0 Then : tbJCOPQty12.Clear() : End If
            If Val(tbJCOPQty13.Text) = 0 Then : tbJCOPQty13.Clear() : End If
            If Val(tbJCOPQty14.Text) = 0 Then : tbJCOPQty14.Clear() : End If
            If Val(tbJCOPQty15.Text) = 0 Then : tbJCOPQty15.Clear() : End If
            If Val(tbJCOPQty16.Text) = 0 Then : tbJCOPQty16.Clear() : End If
            If Val(tbJCOPQty17.Text) = 0 Then : tbJCOPQty17.Clear() : End If
            If Val(tbJCOPQty18.Text) = 0 Then : tbJCOPQty18.Clear() : End If

        Else
            tbJCOPQty01.Clear() : tbJCOPQty02.Clear() : tbJCOPQty03.Clear() : tbJCOPQty04.Clear() : tbJCOPQty05.Clear() : tbJCOPQty06.Clear()
            tbJCOPQty07.Clear() : tbJCOPQty08.Clear() : tbJCOPQty09.Clear() : tbJCOPQty10.Clear() : tbJCOPQty11.Clear() : tbJCOPQty12.Clear()
            tbJCOPQty13.Clear() : tbJCOPQty14.Clear() : tbJCOPQty15.Clear() : tbJCOPQty16.Clear() : tbJCOPQty17.Clear() : tbJCOPQty18.Clear()
        End If


        tbJCOPBalQty01.Text = Val(tbJCQty01.Text) - Val(tbJCQPty01.Text) ''+ Val(tbJCOPQty01.Text))
        tbJCOPBalQty02.Text = Val(tbJCQty02.Text) - Val(tbJCQPty02.Text) ''+ Val(tbJCOPQty02.Text))
        tbJCOPBalQty03.Text = Val(tbJCQty03.Text) - Val(tbJCQPty03.Text) ''+ Val(tbJCOPQty03.Text))
        tbJCOPBalQty04.Text = Val(tbJCQty04.Text) - Val(tbJCQPty04.Text) ''+ Val(tbJCOPQty04.Text))
        tbJCOPBalQty05.Text = Val(tbJCQty05.Text) - Val(tbJCQPty05.Text) ''+ Val(tbJCOPQty05.Text))
        tbJCOPBalQty06.Text = Val(tbJCQty06.Text) - Val(tbJCQPty06.Text) ''+ Val(tbJCOPQty06.Text))
        tbJCOPBalQty07.Text = Val(tbJCQty07.Text) - Val(tbJCQPty07.Text) ''+ Val(tbJCOPQty07.Text))
        tbJCOPBalQty08.Text = Val(tbJCQty08.Text) - Val(tbJCQPty08.Text) ''+ Val(tbJCOPQty08.Text))
        tbJCOPBalQty09.Text = Val(tbJCQty09.Text) - Val(tbJCQPty09.Text) ''+ Val(tbJCOPQty09.Text))
        tbJCOPBalQty10.Text = Val(tbJCQty10.Text) - Val(tbJCQPty10.Text) ''+ Val(tbJCOPQty10.Text))
        tbJCOPBalQty11.Text = Val(tbJCQty11.Text) - Val(tbJCQPty11.Text) ''+ Val(tbJCOPQty11.Text))
        tbJCOPBalQty12.Text = Val(tbJCQty12.Text) - Val(tbJCQPty12.Text) ''+ Val(tbJCOPQty12.Text))
        tbJCOPBalQty13.Text = Val(tbJCQty13.Text) - Val(tbJCQPty13.Text) ''+ Val(tbJCOPQty13.Text))
        tbJCOPBalQty14.Text = Val(tbJCQty14.Text) - Val(tbJCQPty14.Text) ''+ Val(tbJCOPQty14.Text))
        tbJCOPBalQty15.Text = Val(tbJCQty15.Text) - Val(tbJCQPty15.Text) ''+ Val(tbJCOPQty15.Text))
        tbJCOPBalQty16.Text = Val(tbJCQty16.Text) - Val(tbJCQPty16.Text) ''+ Val(tbJCOPQty16.Text))
        tbJCOPBalQty17.Text = Val(tbJCQty17.Text) - Val(tbJCQPty17.Text) ''+ Val(tbJCOPQty17.Text))
        tbJCOPBalQty18.Text = Val(tbJCQty18.Text) - Val(tbJCQPty18.Text) ''+ Val(tbJCOPQty18.Text))

        tbJCOPBalQtyTotal.Text = Val(tbJCQPtyTotal.Text) + Val(tbJCOPQtyTotal.Text)

        If Val(tbJCOPBalQty01.Text) = 0 Then : tbJCOPBalQty01.Clear() : End If
        If Val(tbJCOPBalQty02.Text) = 0 Then : tbJCOPBalQty02.Clear() : End If
        If Val(tbJCOPBalQty03.Text) = 0 Then : tbJCOPBalQty03.Clear() : End If
        If Val(tbJCOPBalQty04.Text) = 0 Then : tbJCOPBalQty04.Clear() : End If
        If Val(tbJCOPBalQty05.Text) = 0 Then : tbJCOPBalQty05.Clear() : End If
        If Val(tbJCOPBalQty06.Text) = 0 Then : tbJCOPBalQty06.Clear() : End If
        If Val(tbJCOPBalQty07.Text) = 0 Then : tbJCOPBalQty07.Clear() : End If
        If Val(tbJCOPBalQty08.Text) = 0 Then : tbJCOPBalQty08.Clear() : End If
        If Val(tbJCOPBalQty09.Text) = 0 Then : tbJCOPBalQty09.Clear() : End If
        If Val(tbJCOPBalQty10.Text) = 0 Then : tbJCOPBalQty10.Clear() : End If
        If Val(tbJCOPBalQty11.Text) = 0 Then : tbJCOPBalQty11.Clear() : End If
        If Val(tbJCOPBalQty12.Text) = 0 Then : tbJCOPBalQty12.Clear() : End If
        If Val(tbJCOPBalQty13.Text) = 0 Then : tbJCOPBalQty13.Clear() : End If
        If Val(tbJCOPBalQty14.Text) = 0 Then : tbJCOPBalQty14.Clear() : End If
        If Val(tbJCOPBalQty15.Text) = 0 Then : tbJCOPBalQty15.Clear() : End If
        If Val(tbJCOPBalQty16.Text) = 0 Then : tbJCOPBalQty16.Clear() : End If
        If Val(tbJCOPBalQty17.Text) = 0 Then : tbJCOPBalQty17.Clear() : End If
        If Val(tbJCOPBalQty18.Text) = 0 Then : tbJCOPBalQty18.Clear() : End If

        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub tbPackingJobcardNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPackingJobcardNo.TextChanged

    End Sub

    Private Sub tbOuterCartonNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbOuterCartonNo.TextChanged

    End Sub
End Class