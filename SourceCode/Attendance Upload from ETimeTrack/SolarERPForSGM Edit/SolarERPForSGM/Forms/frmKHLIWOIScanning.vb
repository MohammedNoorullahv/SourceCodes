Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle


'Imports PCComm

Public Class frmKHLIWOIScanning

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

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToXls("D:\ArticleMaster.xls")
        MsgBox("Export Completed")

    End Sub

    Dim keyascii As Integer
    Dim sJobcardNo, sOrderNo, sWeightAvailable, sArticleGroup, sArticleName, sArticleColour, sArticleId As String
    Dim nQuantityinSize As Integer
    Dim nSize As Decimal
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
                Dim sSalesOrderDetailsId As String
                sSalesOrderDetailsId = dsSelJobcard.Tables(0).Rows(0).Item("SalesOrderDetailId")


                Dim daSelArticleInfo As New SqlDataAdapter("Select * from SalesOrderDetails where ID = '" & sSalesOrderDetailsId & _
                                                           "'", sCnn)
                Dim dsSelArticleInfo As New DataSet
                daSelArticleInfo.Fill(dsSelArticleInfo)

                sArticleId = dsSelArticleInfo.Tables(0).Rows(0).Item("ArticleDetailID")
                sArticleGroup = dsSelArticleInfo.Tables(0).Rows(0).Item("ArticleGroup")
                sArticleName = dsSelArticleInfo.Tables(0).Rows(0).Item("Article")
                sArticleColour = dsSelArticleInfo.Tables(0).Rows(0).Item("ColorName")



                CheckforSAPFiles()
                If sSAPAvailable = "N" Then
                    tbInnerBoxNo.Enabled = False
                    Exit Sub
                Else
                    tbInnerBoxNo.Enabled = True
                End If



                LoadPackedInformations()
                If sSAPAvailable = "N" Then
                    tbInnerBoxNo.Enabled = False
                    Exit Sub
                Else
                    tbInnerBoxNo.Enabled = True
                End If
                tbOuterCartonNo.Focus()
            End If
            Loadgrdinfo()
        End If

        'Catch ex As Exception

        'End Try
    End Sub

    Dim nCartonNo As Integer
    Dim sImageFileName As String

    Private Sub tbOuterCartonNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbOuterCartonNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then



            If Trim(tbOuterCartonNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Outer CartonNo")
                Exit Sub
            End If

            
            'nCartonNo = dsSelOuterCarton.Tables(0).Rows(0).Item("BoxSlNo")
            'tbBoxNo.Text = nCartonNo
            'tbBoxQty.Text = dsSelOuterCarton.Tables(0).Rows(0).Item("Quantity")

            Dim daSelPackingListInfo As New SqlDataAdapter("Select * from PackingDetail where Barcode = '" & Trim(tbOuterCartonNo.Text) & _
                                                           "'", sCnn)
            Dim dsSelPackingListInfo As New DataSet
            daSelPackingListInfo.Fill(dsSelPackingListInfo)

            If dsSelPackingListInfo.Tables(0).Rows.Count <> 1 Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Outer CartonNo")
                Exit Sub
            End If

            nCartonNo = dsSelPackingListInfo.Tables(0).Rows(0).Item("CartonNo")
            tbBoxNo.Text = nCartonNo
            tbBoxQty.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Quantity")

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

            tbIS01.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size01")
            tbIS02.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size02")
            tbIS03.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size03")
            tbIS04.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size04")
            tbIS05.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size05")
            tbIS06.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size06")
            tbIS07.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size07")
            tbIS08.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size08")
            tbIS09.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size09")
            tbIS10.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size10")
            tbIS11.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size11")
            tbIS12.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size12")
            tbIS13.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size13")
            tbIS14.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size14")
            tbIS15.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size15")
            tbIS16.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size16")
            tbIS17.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size17")
            tbIS18.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size18")

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


            tbIQ01.Clear() : tbIQ02.Clear() : tbIQ03.Clear() : tbIQ04.Clear() : tbIQ05.Clear() : tbIQ06.Clear() : tbIQ07.Clear() : tbIQ08.Clear() : tbIQ09.Clear() : tbIQ10.Clear()
            tbIQ11.Clear() : tbIQ12.Clear() : tbIQ13.Clear() : tbIQ14.Clear() : tbIQ15.Clear() : tbIQ16.Clear() : tbIQ17.Clear() : tbIQ18.Clear()

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


            Dim daSelPkdQty As New SqlDataAdapter("Select QuantityinSize,Count(QuantityinSize) As PkdQty From WeltedOuterCartonPackedInfo Where FKJobcardId = '" & sFKJobcardId & _
                                                  "' And CartonNo = '" & Trim(tbOuterCartonNo.Text) & _
                                                  "' And BoxSlNo = '" & Val(tbBoxNo.Text) & _
                                                  "' Group By QuantityinSize", sCnn)
            Dim dsSelPkdQty As New DataSet
            daSelPkdQty.Fill(dsSelPkdQty)

            Dim i As Integer = 0
            For i = 0 To dsSelPkdQty.Tables(0).Rows.Count - 1
                nQuantityinSize = dsSelPkdQty.Tables(0).Rows(i).Item(0)


                If nQuantityinSize = 1 Then : tbIQ01.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 2 Then : tbIQ02.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 3 Then : tbIQ03.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 4 Then : tbIQ04.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 5 Then : tbIQ05.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 6 Then : tbIQ06.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 7 Then : tbIQ07.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 8 Then : tbIQ08.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 9 Then : tbIQ09.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 10 Then : tbIQ10.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 11 Then : tbIQ11.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 12 Then : tbIQ12.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 13 Then : tbIQ13.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 14 Then : tbIQ14.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 15 Then : tbIQ15.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 16 Then : tbIQ16.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 17 Then : tbIQ17.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                ElseIf nQuantityinSize = 18 Then : tbIQ18.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
                End If
            Next


            tbInnerBoxNo.Focus()

        End If


        Loadgrdinfo()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub tbInnerBoxNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbInnerBoxNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then

            If Trim(tbOuterCartonNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("Invalid Inner Box No")
            End If

            'Dim daSelInnerBox As New SqlDataAdapter("Select * from InnerBoxToImport where UPCCode = '" & Trim(tbInnerBoxNo.Text) & _
            '                                        "' And SalesOrder = '" & sOrderNo & "'", sCnn)
            'Dim daSelInnerBox As New SqlDataAdapter("Select * from InnerBoxToImport where TracingId = '" & Trim(tbInnerBoxNo.Text) & _
            '                            "' And SalesOrder = '" & sOrderNo & "'", sCnn)
            Dim daSelInnerBox As New SqlDataAdapter("Select * from WeltedInnerBarcode where Style = '" & sArticleName & _
                                                    "' And Colour = '" & sArticleColour & _
                                                    "' And Barcode = '" & Trim(tbInnerBoxNo.Text) & "'", sCnn)
            Dim dsSelInnerBox As New DataSet
            daSelInnerBox.Fill(dsSelInnerBox)

            If dsSelInnerBox.Tables(0).Rows.Count <> 1 Then
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("Invalid Inner Box No")
            Else
                nSize = Val(dsSelInnerBox.Tables(0).Rows(0).Item("Size"))

                If Val(tbOS01.Text) = nSize And Val(tbOQ01.Text) > 0 Then
                    nQuantityinSize = 1
                ElseIf Val(tbOS02.Text) = nSize And Val(tbOQ02.Text) > 0 Then
                    nQuantityinSize = 2
                ElseIf Val(tbOS03.Text) = nSize And Val(tbOQ03.Text) > 0 Then
                    nQuantityinSize = 3
                ElseIf Val(tbOS04.Text) = nSize And Val(tbOQ04.Text) > 0 Then
                    nQuantityinSize = 4
                ElseIf Val(tbOS05.Text) = nSize And Val(tbOQ05.Text) > 0 Then
                    nQuantityinSize = 5
                ElseIf Val(tbOS06.Text) = nSize And Val(tbOQ06.Text) > 0 Then
                    nQuantityinSize = 6
                ElseIf Val(tbOS07.Text) = nSize And Val(tbOQ07.Text) > 0 Then
                    nQuantityinSize = 7
                ElseIf Val(tbOS08.Text) = nSize And Val(tbOQ08.Text) > 0 Then
                    nQuantityinSize = 8
                ElseIf Val(tbOS09.Text) = nSize And Val(tbOQ09.Text) > 0 Then
                    nQuantityinSize = 9
                ElseIf Val(tbOS10.Text) = nSize And Val(tbOQ10.Text) > 0 Then
                    nQuantityinSize = 10
                ElseIf Val(tbOS11.Text) = nSize And Val(tbOQ11.Text) > 0 Then
                    nQuantityinSize = 11
                ElseIf Val(tbOS12.Text) = nSize And Val(tbOQ12.Text) > 0 Then
                    nQuantityinSize = 12
                ElseIf Val(tbOS13.Text) = nSize And Val(tbOQ13.Text) > 0 Then
                    nQuantityinSize = 13
                ElseIf Val(tbOS14.Text) = nSize And Val(tbOQ14.Text) > 0 Then
                    nQuantityinSize = 14
                ElseIf Val(tbOS15.Text) = nSize And Val(tbOQ15.Text) > 0 Then
                    nQuantityinSize = 15
                ElseIf Val(tbOS16.Text) = nSize And Val(tbOQ16.Text) > 0 Then
                    nQuantityinSize = 16
                ElseIf Val(tbOS17.Text) = nSize And Val(tbOQ17.Text) > 0 Then
                    nQuantityinSize = 17
                ElseIf Val(tbOS18.Text) = nSize And Val(tbOQ18.Text) > 0 Then
                    nQuantityinSize = 18
                    'MsgBox("This Inner Box belongs to this Outer Carton")
                Else
                    nQuantityinSize = 0
                    Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                    MsgBox("This Inner Box Does Not belong to this Outer Carton")
                    GoTo Aa
                End If

                If nQuantityinSize > 0 Then


                    If nQuantityinSize = 1 And (Val(tbIQ01.Text) + 1 > Val(tbOQ01.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 2 And (Val(tbIQ02.Text) + 1 > Val(tbOQ02.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 3 And (Val(tbIQ03.Text) + 1 > Val(tbOQ03.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 4 And (Val(tbIQ04.Text) + 1 > Val(tbOQ04.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 5 And (Val(tbIQ05.Text) + 1 > Val(tbOQ05.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 6 And (Val(tbIQ06.Text) + 1 > Val(tbOQ06.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 7 And (Val(tbIQ07.Text) + 1 > Val(tbOQ07.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 8 And (Val(tbIQ08.Text) + 1 > Val(tbOQ08.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 9 And (Val(tbIQ09.Text) + 1 > Val(tbOQ09.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 10 And (Val(tbIQ10.Text) + 1 > Val(tbOQ10.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 11 And (Val(tbIQ11.Text) + 1 > Val(tbOQ11.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 12 And (Val(tbIQ12.Text) + 1 > Val(tbOQ12.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 13 And (Val(tbIQ13.Text) + 1 > Val(tbOQ13.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 14 And (Val(tbIQ14.Text) + 1 > Val(tbOQ14.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 15 And (Val(tbIQ15.Text) + 1 > Val(tbOQ15.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 16 And (Val(tbIQ16.Text) + 1 > Val(tbOQ16.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 17 And (Val(tbIQ17.Text) + 1 > Val(tbOQ17.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                    If nQuantityinSize = 18 And (Val(tbIQ18.Text) + 1 > Val(tbOQ18.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                End If



                If nQuantityinSize = 1 Then : tbIQ01.Text = Val(tbIQ01.Text) + 1
                ElseIf nQuantityinSize = 2 Then : tbIQ02.Text = Val(tbIQ02.Text) + 1
                ElseIf nQuantityinSize = 3 Then : tbIQ03.Text = Val(tbIQ03.Text) + 1
                ElseIf nQuantityinSize = 4 Then : tbIQ04.Text = Val(tbIQ04.Text) + 1
                ElseIf nQuantityinSize = 5 Then : tbIQ05.Text = Val(tbIQ05.Text) + 1
                ElseIf nQuantityinSize = 6 Then : tbIQ06.Text = Val(tbIQ06.Text) + 1
                ElseIf nQuantityinSize = 7 Then : tbIQ07.Text = Val(tbIQ07.Text) + 1
                ElseIf nQuantityinSize = 8 Then : tbIQ08.Text = Val(tbIQ08.Text) + 1
                ElseIf nQuantityinSize = 9 Then : tbIQ09.Text = Val(tbIQ09.Text) + 1
                ElseIf nQuantityinSize = 10 Then : tbIQ10.Text = Val(tbIQ10.Text) + 1
                ElseIf nQuantityinSize = 11 Then : tbIQ11.Text = Val(tbIQ11.Text) + 1
                ElseIf nQuantityinSize = 12 Then : tbIQ12.Text = Val(tbIQ12.Text) + 1
                ElseIf nQuantityinSize = 13 Then : tbIQ13.Text = Val(tbIQ13.Text) + 1
                ElseIf nQuantityinSize = 14 Then : tbIQ14.Text = Val(tbIQ14.Text) + 1
                ElseIf nQuantityinSize = 15 Then : tbIQ15.Text = Val(tbIQ15.Text) + 1
                ElseIf nQuantityinSize = 16 Then : tbIQ16.Text = Val(tbIQ16.Text) + 1
                ElseIf nQuantityinSize = 17 Then : tbIQ17.Text = Val(tbIQ17.Text) + 1
                ElseIf nQuantityinSize = 18 Then : tbIQ18.Text = Val(tbIQ18.Text) + 1
                End If

                Dim daInsOuterCartonInfo As New SqlDataAdapter("Insert Into WeltedOuterCartonPackedInfo Values ('" & sFKJobcardId & _
                                                               "','" & Val(tbBoxNo.Text) & "','" & Trim(tbOuterCartonNo.Text) & _
                                                               "','" & Trim(tbInnerBoxNo.Text) & "','" & Val(nQuantityinSize) & "','" & nSize & "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "')", sCnn)
                Dim dsInsOuterCartonInof As New DataSet
                daInsOuterCartonInfo.Fill(dsInsOuterCartonInof)
                dsInsOuterCartonInof.AcceptChanges()


                ''Coding for updating OuterCartonToImport
                ''Coding for updating OuterCartonToImport
                tbInnerBoxNo.Clear()

                If Val(tbIQ01.Text) + Val(tbIQ02.Text) + Val(tbIQ03.Text) + Val(tbIQ04.Text) + Val(tbIQ05.Text) + Val(tbIQ06.Text) + Val(tbIQ07.Text) + Val(tbIQ08.Text) + Val(tbIQ09.Text) + Val(tbIQ10.Text) + Val(tbIQ11.Text) + Val(tbIQ12.Text) + Val(tbIQ13.Text) + Val(tbIQ14.Text) + Val(tbIQ15.Text) + Val(tbIQ16.Text) + Val(tbIQ17.Text) + Val(tbIQ18.Text) >= Val(tbBoxQty.Text) Then
                    Dim sId As String
                    sId = System.Guid.NewGuid.ToString()
                    Dim daInsPackingInfo As New SqlDataAdapter("Insert Into PackingInfo(ID,CreatedDate,JobcardNo,OuterCartonNo,IsUpdatedinAP) Values ('" & sId & _
                                                               "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','" & Trim(tbJobcardNo.Text) & "','" & Trim(tbOuterCartonNo.Text) & "','0')", sCnn)
                    Dim dsInsPackingInfo As New DataSet
                    daInsPackingInfo.Fill(dsInsPackingInfo)
                    dsInsPackingInfo.AcceptChanges()

                    Clear()
                    tbOuterCartonNo.Focus()
                Else
                    tbInnerBoxNo.Focus()
                End If

                LoadPackedInformations()


Aa:


            End If

        End If
        Loadgrdinfo()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Clear()
        tbOuterCartonNo.Clear()
        tbInnerBoxNo.Clear()
        tbBoxNo.Clear()
        tbBoxQty.Clear()

        tbOQ01.Clear() : tbOQ02.Clear() : tbOQ03.Clear() : tbOQ04.Clear() : tbOQ05.Clear() : tbOQ06.Clear() : tbOQ07.Clear() : tbOQ08.Clear() : tbOQ09.Clear()
        tbOQ10.Clear() : tbOQ11.Clear() : tbOQ12.Clear() : tbOQ13.Clear() : tbOQ14.Clear() : tbOQ15.Clear() : tbOQ16.Clear() : tbOQ17.Clear() : tbOQ18.Clear()


        tbIQ01.Clear() : tbIQ02.Clear() : tbIQ03.Clear() : tbIQ04.Clear() : tbIQ05.Clear() : tbIQ06.Clear() : tbIQ07.Clear() : tbIQ08.Clear() : tbIQ09.Clear()
        tbIQ10.Clear() : tbIQ11.Clear() : tbIQ12.Clear() : tbIQ13.Clear() : tbIQ14.Clear() : tbIQ15.Clear() : tbIQ16.Clear() : tbIQ17.Clear() : tbIQ18.Clear()


        tbOuterCartonNo.Focus()

    End Sub

    Private Sub ClearTotalPackedInfo()

        tbJSize01.Clear() : tbJSize02.Clear() : tbJSize03.Clear() : tbJSize04.Clear() : tbJSize05.Clear() : tbJSize06.Clear() : tbJSize07.Clear() : tbJSize08.Clear() : tbJSize09.Clear()
        tbJSize10.Clear() : tbJSize11.Clear() : tbJSize12.Clear() : tbJSize13.Clear() : tbJSize14.Clear() : tbJSize15.Clear() : tbJSize16.Clear() : tbJSize17.Clear() : tbJSize18.Clear()

        tbJQ01.Clear() : tbJQ02.Clear() : tbJQ03.Clear() : tbJQ04.Clear() : tbJQ05.Clear() : tbJQ06.Clear() : tbJQ07.Clear() : tbJQ08.Clear() : tbJQ09.Clear()
        tbJQ10.Clear() : tbJQ11.Clear() : tbJQ12.Clear() : tbJQ13.Clear() : tbJQ14.Clear() : tbJQ15.Clear() : tbJQ16.Clear() : tbJQ17.Clear() : tbJQ18.Clear()

        tbPQ01.Clear() : tbPQ02.Clear() : tbPQ03.Clear() : tbPQ04.Clear() : tbPQ05.Clear() : tbPQ06.Clear() : tbPQ07.Clear() : tbPQ08.Clear() : tbPQ09.Clear()
        tbPQ10.Clear() : tbPQ11.Clear() : tbPQ12.Clear() : tbPQ13.Clear() : tbPQ14.Clear() : tbPQ15.Clear() : tbPQ16.Clear() : tbPQ17.Clear() : tbPQ18.Clear()

        tbBQ01.Clear() : tbBQ02.Clear() : tbBQ03.Clear() : tbBQ04.Clear() : tbBQ05.Clear() : tbBQ06.Clear() : tbBQ07.Clear() : tbBQ08.Clear() : tbBQ09.Clear()
        tbBQ10.Clear() : tbBQ11.Clear() : tbBQ12.Clear() : tbBQ13.Clear() : tbBQ14.Clear() : tbBQ15.Clear() : tbBQ16.Clear() : tbBQ17.Clear() : tbBQ18.Clear()
    End Sub
    Dim nJCQty As Integer
    Dim nJCSize As Decimal
    Dim nWeight As Decimal

    Private Sub LoadPackedInformations()
        'Try

        ''Size''

        ClearTotalPackedInfo()

        Dim daSelPackingListInfo As New SqlDataAdapter("Select Top 1 * from PackingDetail where packingListNo = (Select ID from Packing where CUSTWORKORDERnO = '" & Microsoft.VisualBasic.Left(sJobcardNo, 19) & _
                                                              "') order by cartonNo", sCnn)
        Dim dsSelPackingListInfo As New DataSet
        daSelPackingListInfo.Fill(dsSelPackingListInfo)

        If dsSelPackingListInfo.Tables(0).Rows.Count = 0 Then
            Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
            MsgBox("Packing List Not Generated for this Jobcard", MsgBoxStyle.Critical)
            sSAPAvailable = "N"
            Exit Sub
        End If
        tbJSize01.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size01")
        tbJSize02.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size02")
        tbJSize03.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size03")
        tbJSize04.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size04")
        tbJSize05.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size05")
        tbJSize06.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size06")
        tbJSize07.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size07")
        tbJSize08.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size08")
        tbJSize09.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size09")
        tbJSize10.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size10")
        tbJSize11.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size11")
        tbJSize12.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size12")
        tbJSize13.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size13")
        tbJSize14.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size14")
        tbJSize15.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size15")
        tbJSize16.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size16")
        tbJSize17.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size17")
        tbJSize18.Text = dsSelPackingListInfo.Tables(0).Rows(0).Item("Size18")
        ''Size''

        ''JobcardQuantity''
        Dim daSelJCQty As New SqlDataAdapter("Select * from JobcardDetail where ID = '" & sFKJobcardId & "'", sCnn)
        Dim dsSelJCQty As New DataSet
        daSelJCQty.Fill(dsSelJCQty)

        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size01").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity01").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size02").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity02").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size03").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity03").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size04").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity04").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size05").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity05").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size06").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity06").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size07").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity07").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size08").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity08").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size09").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity09").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size10").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity10").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size11").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity11").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size12").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity12").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size13").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity13").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size14").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity14").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size15").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity15").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size16").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity16").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size17").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity17").ToString) : PostJCQty()
        nJCSize = Val(dsSelJCQty.Tables(0).Rows(0).Item("Size18").ToString) : nJCQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity18").ToString) : PostJCQty()
        ''JobcardQuantity''

        'CheckforInnerWeight()

        ''Packed Qty''
        Dim daSelPkdQty As New SqlDataAdapter("Select QuantityinSize,Count(QuantityinSize) As PkdQty From WeltedOuterCartonPackedInfo Where FKJobcardId = '" & sFKJobcardId & _
                                                     "' Group By QuantityinSize", sCnn)
        Dim dsSelPkdQty As New DataSet
        daSelPkdQty.Fill(dsSelPkdQty)

        Dim i As Integer = 0
        For i = 0 To dsSelPkdQty.Tables(0).Rows.Count - 1
            nQuantityinSize = dsSelPkdQty.Tables(0).Rows(i).Item(0)


            If nQuantityinSize = 1 Then : tbPQ01.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 2 Then : tbPQ02.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 3 Then : tbPQ03.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 4 Then : tbPQ04.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 5 Then : tbPQ05.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 6 Then : tbPQ06.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 7 Then : tbPQ07.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 8 Then : tbPQ08.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 9 Then : tbPQ09.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 10 Then : tbPQ10.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 11 Then : tbPQ11.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 12 Then : tbPQ12.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 13 Then : tbPQ13.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 14 Then : tbPQ14.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 15 Then : tbPQ15.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 16 Then : tbPQ16.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 17 Then : tbPQ17.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            ElseIf nQuantityinSize = 18 Then : tbPQ18.Text = dsSelPkdQty.Tables(0).Rows(i).Item(1)
            End If
        Next
        ''Packed Qty''

        ''Bal to Pack Qty''
        tbBQ01.Text = Val(tbJQ01.Text) - Val(tbPQ01.Text)
        tbBQ02.Text = Val(tbJQ02.Text) - Val(tbPQ02.Text)
        tbBQ03.Text = Val(tbJQ03.Text) - Val(tbPQ03.Text)
        tbBQ04.Text = Val(tbJQ04.Text) - Val(tbPQ04.Text)
        tbBQ05.Text = Val(tbJQ05.Text) - Val(tbPQ05.Text)
        tbBQ06.Text = Val(tbJQ06.Text) - Val(tbPQ06.Text)
        tbBQ07.Text = Val(tbJQ07.Text) - Val(tbPQ07.Text)
        tbBQ08.Text = Val(tbJQ08.Text) - Val(tbPQ08.Text)
        tbBQ09.Text = Val(tbJQ09.Text) - Val(tbPQ09.Text)
        tbBQ10.Text = Val(tbJQ10.Text) - Val(tbPQ10.Text)
        tbBQ11.Text = Val(tbJQ11.Text) - Val(tbPQ11.Text)
        tbBQ12.Text = Val(tbJQ12.Text) - Val(tbPQ12.Text)
        tbBQ13.Text = Val(tbJQ13.Text) - Val(tbPQ13.Text)
        tbBQ14.Text = Val(tbJQ14.Text) - Val(tbPQ14.Text)
        tbBQ15.Text = Val(tbJQ15.Text) - Val(tbPQ15.Text)
        tbBQ16.Text = Val(tbJQ16.Text) - Val(tbPQ16.Text)
        tbBQ17.Text = Val(tbJQ17.Text) - Val(tbPQ17.Text)
        tbBQ18.Text = Val(tbJQ18.Text) - Val(tbPQ18.Text)
        ''Bal to Pack Qty''

        tbjqTotal.Text = Val(tbJQ01.Text) + Val(tbJQ02.Text) + Val(tbJQ03.Text) + Val(tbJQ04.Text) + Val(tbJQ05.Text) + Val(tbJQ06.Text) + Val(tbJQ07.Text) + Val(tbJQ08.Text) + Val(tbJQ09.Text) + Val(tbJQ10.Text) + Val(tbJQ11.Text) + Val(tbJQ12.Text) + Val(tbJQ13.Text) + Val(tbJQ14.Text) + Val(tbJQ15.Text) + Val(tbJQ16.Text) + Val(tbJQ17.Text) + Val(tbJQ18.Text)
        tbPQTotal.Text = Val(tbPQ01.Text) + Val(tbPQ02.Text) + Val(tbPQ03.Text) + Val(tbPQ04.Text) + Val(tbPQ05.Text) + Val(tbPQ06.Text) + Val(tbPQ07.Text) + Val(tbPQ08.Text) + Val(tbPQ09.Text) + Val(tbPQ10.Text) + Val(tbPQ11.Text) + Val(tbPQ12.Text) + Val(tbPQ13.Text) + Val(tbPQ14.Text) + Val(tbPQ15.Text) + Val(tbPQ16.Text) + Val(tbPQ17.Text) + Val(tbPQ18.Text)
        tbBQTotal.Text = Val(tbBQ01.Text) + Val(tbBQ02.Text) + Val(tbBQ03.Text) + Val(tbBQ04.Text) + Val(tbBQ05.Text) + Val(tbBQ06.Text) + Val(tbBQ07.Text) + Val(tbBQ08.Text) + Val(tbBQ09.Text) + Val(tbBQ10.Text) + Val(tbBQ11.Text) + Val(tbBQ12.Text) + Val(tbBQ13.Text) + Val(tbBQ14.Text) + Val(tbBQ15.Text) + Val(tbBQ16.Text) + Val(tbBQ17.Text) + Val(tbBQ18.Text)


        If Val(tbJQ01.Text) <= 0 Then : tbJQ01.Clear() : End If
        If Val(tbJQ02.Text) <= 0 Then : tbJQ02.Clear() : End If
        If Val(tbJQ03.Text) <= 0 Then : tbJQ03.Clear() : End If
        If Val(tbJQ04.Text) <= 0 Then : tbJQ04.Clear() : End If
        If Val(tbJQ05.Text) <= 0 Then : tbJQ05.Clear() : End If
        If Val(tbJQ06.Text) <= 0 Then : tbJQ06.Clear() : End If
        If Val(tbJQ07.Text) <= 0 Then : tbJQ07.Clear() : End If
        If Val(tbJQ08.Text) <= 0 Then : tbJQ08.Clear() : End If
        If Val(tbJQ09.Text) <= 0 Then : tbJQ09.Clear() : End If
        If Val(tbJQ10.Text) <= 0 Then : tbJQ10.Clear() : End If
        If Val(tbJQ11.Text) <= 0 Then : tbJQ11.Clear() : End If
        If Val(tbJQ12.Text) <= 0 Then : tbJQ12.Clear() : End If
        If Val(tbJQ13.Text) <= 0 Then : tbJQ13.Clear() : End If
        If Val(tbJQ14.Text) <= 0 Then : tbJQ14.Clear() : End If
        If Val(tbJQ15.Text) <= 0 Then : tbJQ15.Clear() : End If
        If Val(tbJQ16.Text) <= 0 Then : tbJQ16.Clear() : End If
        If Val(tbJQ17.Text) <= 0 Then : tbJQ17.Clear() : End If
        If Val(tbJQ18.Text) <= 0 Then : tbJQ18.Clear() : End If

        If Val(tbPQ01.Text) <= 0 Then : tbPQ01.Clear() : End If
        If Val(tbPQ02.Text) <= 0 Then : tbPQ02.Clear() : End If
        If Val(tbPQ03.Text) <= 0 Then : tbPQ03.Clear() : End If
        If Val(tbPQ04.Text) <= 0 Then : tbPQ04.Clear() : End If
        If Val(tbPQ05.Text) <= 0 Then : tbPQ05.Clear() : End If
        If Val(tbPQ06.Text) <= 0 Then : tbPQ06.Clear() : End If
        If Val(tbPQ07.Text) <= 0 Then : tbPQ07.Clear() : End If
        If Val(tbPQ08.Text) <= 0 Then : tbPQ08.Clear() : End If
        If Val(tbPQ09.Text) <= 0 Then : tbPQ09.Clear() : End If
        If Val(tbPQ10.Text) <= 0 Then : tbPQ10.Clear() : End If
        If Val(tbPQ11.Text) <= 0 Then : tbPQ11.Clear() : End If
        If Val(tbPQ12.Text) <= 0 Then : tbPQ12.Clear() : End If
        If Val(tbPQ13.Text) <= 0 Then : tbPQ13.Clear() : End If
        If Val(tbPQ14.Text) <= 0 Then : tbPQ14.Clear() : End If
        If Val(tbPQ15.Text) <= 0 Then : tbPQ15.Clear() : End If
        If Val(tbPQ16.Text) <= 0 Then : tbPQ16.Clear() : End If
        If Val(tbPQ17.Text) <= 0 Then : tbPQ17.Clear() : End If
        If Val(tbPQ18.Text) <= 0 Then : tbPQ18.Clear() : End If

        If Val(tbBQ01.Text) <= 0 Then : tbBQ01.Clear() : End If
        If Val(tbBQ02.Text) <= 0 Then : tbBQ02.Clear() : End If
        If Val(tbBQ03.Text) <= 0 Then : tbBQ03.Clear() : End If
        If Val(tbBQ04.Text) <= 0 Then : tbBQ04.Clear() : End If
        If Val(tbBQ05.Text) <= 0 Then : tbBQ05.Clear() : End If
        If Val(tbBQ06.Text) <= 0 Then : tbBQ06.Clear() : End If
        If Val(tbBQ07.Text) <= 0 Then : tbBQ07.Clear() : End If
        If Val(tbBQ08.Text) <= 0 Then : tbBQ08.Clear() : End If
        If Val(tbBQ09.Text) <= 0 Then : tbBQ09.Clear() : End If
        If Val(tbBQ10.Text) <= 0 Then : tbBQ10.Clear() : End If
        If Val(tbBQ11.Text) <= 0 Then : tbBQ11.Clear() : End If
        If Val(tbBQ12.Text) <= 0 Then : tbBQ12.Clear() : End If
        If Val(tbBQ13.Text) <= 0 Then : tbBQ13.Clear() : End If
        If Val(tbBQ14.Text) <= 0 Then : tbBQ14.Clear() : End If
        If Val(tbBQ15.Text) <= 0 Then : tbBQ15.Clear() : End If
        If Val(tbBQ16.Text) <= 0 Then : tbBQ16.Clear() : End If
        If Val(tbBQ17.Text) <= 0 Then : tbBQ17.Clear() : End If
        If Val(tbBQ18.Text) <= 0 Then : tbBQ18.Clear() : End If

        'Catch Exp As Exception

        'End Try
    End Sub

    Private Sub PostJCQty()

        If nJCSize = Val(tbJSize01.Text) Then : tbJQ01.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize02.Text) Then : tbJQ02.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize03.Text) Then : tbJQ03.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize04.Text) Then : tbJQ04.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize05.Text) Then : tbJQ05.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize06.Text) Then : tbJQ06.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize07.Text) Then : tbJQ07.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize08.Text) Then : tbJQ08.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize09.Text) Then : tbJQ09.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize10.Text) Then : tbJQ10.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize11.Text) Then : tbJQ11.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize12.Text) Then : tbJQ12.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize13.Text) Then : tbJQ13.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize14.Text) Then : tbJQ14.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize15.Text) Then : tbJQ15.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize16.Text) Then : tbJQ16.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize17.Text) Then : tbJQ17.Text = nJCQty
        ElseIf nJCSize = Val(tbJSize18.Text) Then : tbJQ18.Text = nJCQty
        End If
    End Sub

    Private Sub LoadfromExcel()
        Try
            Dim objExcel As New Application
            Dim objWorkbook As Workbook
            Dim objWorksheet As Worksheet
            Dim Data As String

            'objWorkbook = GetObject("C:\7995.xls")
            'objWorkbook = GetObject("C:\" + Trim(tbFocusJobcardNo.Text) + ".xls")

            '    If sImpExp = "Imp" Then
            '        objWorkbook = GetObject("\\Accounts2\F\Focus Check List\Import\" + Trim(tbFocusJobcardNo.Text) + ".xls")
            '    Else
            '        objWorkbook = GetObject("\\Accounts2\F\Focus Check List\Export\" + Trim(tbFocusJobcardNo.Text) + ".xls")
            '    End If

            '    objWorksheet = objWorkbook.Worksheets(1)

            '    Data = objWorksheet.Cells(18, 3).Value.ToString()

            '    ' or testdata = objSht.Rows(1).Cells(1).Text.ToString()
            '    ' or testdata = objSht.Range("A1").Value.ToString

            '    'TextBox1.Text = Data
            '    sSubject = ""
            '    If sImpExp = "Imp" Then
            '        'objWorksheet.Cells(RowIndex:=,Column)
            '        sSubject = "Clearance of " + objWorksheet.Cells(45, 2).Value.ToString() + _
            '        " " + objWorksheet.Cells(45, 3).Value.ToString() + ", No. of Pkgs: " + objWorksheet.Cells(20, 3).Value.ToString() + _
            '        ", Gross Wt: " + objWorksheet.Cells(20, 7).Value.ToString() + " Under Job No. " + sJobNo + " Dated " + sJobDate + _
            '        ", Vide BL No." + objWorksheet.Cells(18, 3).Value.ToString() + " Dated " + objWorksheet.Cells(19, 3).Value.ToString() + _
            '        ", H: " + objWorksheet.Cells(18, 7).Value.ToString() + " Dated " + objWorksheet.Cells(19, 7).Value.ToString() + _
            '        " Des:- " + objWorksheet.Cells(45, 2).Value.ToString() + " Vide Invoice No. " + objWorksheet.Cells(29, 3).Value.ToString() + _
            '        " From Supplier " + objWorksheet.Cells(29, 6).Value.ToString()
            '    ElseIf sImpExp = "Exp" Then
            '        sSubject = "Export of " + objWorksheet.Cells(45, 2).Value.ToString() + _
            '        " " + objWorksheet.Cells(45, 3).Value.ToString() + ", No. of Pkgs: " + objWorksheet.Cells(20, 3).Value.ToString() + _
            '        ", Gross Wt: " + objWorksheet.Cells(20, 7).Value.ToString() + "Under Job No. ____________ Dated ______ " + _
            '        ", Vide BL No." + objWorksheet.Cells(18, 3).Value.ToString() + " Dated " + objWorksheet.Cells(19, 3).Value.ToString() + _
            '        ", H: " + objWorksheet.Cells(18, 7).Value.ToString() + " Dated " + objWorksheet.Cells(19, 7).Value.ToString() + _
            '        " Des:- " + objWorksheet.Cells(45, 2).Value.ToString() + " Vide Invoice No. " + objWorksheet.Cells(29, 3).Value.ToString() + _
            '        " From Supplier " + objWorksheet.Cells(29, 6).Value.ToString()
            '    End If
            '    tbSubject.Text = sSubject
            '    'Clearance Of 
            '    'TGHU7730728 – B45 
            '    '69: Pkgs(-C20)
            '    '1010.20 Kgs – G20
            '    ' Under job No. Sea/Imp/3043 2/2/2011
            '    ' Vide B/L No. HJSCTPE037920300 / - C18/C19
            '    ' H: 111010KECHI DT: 06.01.2011, - G18/G19
            '    ' Des:- COW FINISHED LEATHER  - C45
            '    'Vide Invoice No. TCC101524Dtd 30-Dec-2010  - C29
            '    'Sup :- Techang Leather Product Co Ltd – F29
            '    'BE NO: 762339 DT: 28.01.2011 - ??????

            '    ''Clearance Of TGHU7730728 69 Pkgs 1010.20 Kgs Under job No. Sea/Imp/3043 2/2/2011 Vide B/L No. HJSCTPE037920300 / H: 111010KECHI DT: 06.01.2011,
            '    ' Des:- COW FINISHED LEATHER Vide Invoice No. TCC101524Dtd 30-Dec-2010 Sup :- Techang Leather Product Co LtdBE NO: 762339 DT: 28.01.2011
        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
        End Try
    End Sub
    Dim sFileName, sOuterFileName, sInnerFileName, sSAPAvailable As String

    Dim sOUBCartonNumber, sOUBSalesOrder, sOUBArticle, sOUBSizes As String
    Dim nOUBBoxSlNo, nOUBQuantity As Integer

    Dim nINBSlNo, nINBQty As Integer
    Dim sINBSalesOrder, sINBUPCCode, sINBTracingId, sINBArticle, sINBColour, sINBSize, sINBSizUS, sINBText, sINBENColour, sINBFRColour As String
    Dim curFile As String
    Dim excelapp As New Application

    Private Sub CheckforSAPFiles()
        'Try
        Dim objOCExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objOCWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objOCWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim OCData As String

        Dim daSelOuterCarton As New SqlDataAdapter("Select * from WeltedInnerBarcode where Style = '" & sArticleName & _
                                                   "' And Colour = '" & sArticleColour & "'", sCnn)
        Dim dsSelOuterCarton As New DataSet
        daSelOuterCarton.Fill(dsSelOuterCarton)

        If dsSelOuterCarton.Tables(0).Rows.Count = 0 Then
            ''TODO: Coding to be done for New Article Arrives as and when

            excelapp.Visible = True

            Dim WkBook As Workbook

            WkBook = excelapp.Workbooks.Open("\\ahserver\To day only\ECCO SAP File\" + sFileName + ".xls")

            objOCWorksheet = WkBook.Worksheets(1)

            Dim i As Integer = 2

            Do
                nOUBBoxSlNo = objOCWorksheet.Cells(i, 1).Value.ToString()
                sOUBCartonNumber = objOCWorksheet.Cells(i, 2).Value.ToString()
                sOUBSalesOrder = objOCWorksheet.Cells(i, 3).Value.ToString()
                sOUBArticle = objOCWorksheet.Cells(i, 4).Value.ToString()
                nOUBQuantity = objOCWorksheet.Cells(i, 5).Value.ToString()
                sOUBSizes = objOCWorksheet.Cells(i, 6).Value.ToString()
                i = i + 1

                Dim daInsOuterCartonSAP As New SqlDataAdapter("Insert Into OuterCartonToImport Values ('" & sFKJobcardId & _
                                                               "','" & nOUBBoxSlNo & "','" & sOUBCartonNumber & _
                                                               "','" & sOUBSalesOrder & "','" & sOUBArticle & _
                                                               "','" & nOUBQuantity & "','" & sOUBSizes & _
                                                               "','','0','0','0','0','0','','" & Format(Date.Now, "dd-MMM-yyyy:hh:mm:ss") & _
                                                               "','','','','')", sCnn)
                Dim dsInsOuterCartonSAP As New DataSet
                daInsOuterCartonSAP.Fill(dsInsOuterCartonSAP)
                dsInsOuterCartonSAP.AcceptChanges()

            Loop Until Val(objOCWorksheet.Cells(i, 2).Value) = 0
            ''Outer Carton''
        Else

            
            sSAPAvailable = "Y"
        End If

        
        'Catch ex As Exception

        'End Try
    End Sub

    Dim dtStudentGrade As DataTable
    Dim dtExcelData As DataTable

    Function ReadExcelFile()
        ' '' '' '' ''Use OleDbDataAdapter  to provide communication between the DataTable and the OleDb Data Sources
        '' '' '' ''Dim da As New OleDbDataAdapter

        ' '' '' '' ''Use DataTable as storage of data from excel
        '' '' '' ''Dim dt As DataTable

        ' '' '' '' ''Use OleDbCommand to execute our SQL statement
        '' '' '' ''Dim cmd As New OleDbCommand

        ' '' '' '' ''Use OleDbConnection that will be used by OleDbCommand to connect to excel file
        '' '' '' ''Dim xlsConn As OleDbConnection

        '' '' '' ''Dim sPath As String = String.Empty

        '' '' '' ''sPath = Me.lbFilePath.Text

        ' '' '' '' ''Create a new instance of connection and set the datasource value to excel's path
        '' '' '' ''xlsConn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & sPath & ";Extended Properties=Excel 12.0")

        ' '' '' '' ''Use try catch block to handle some or all possible errors that may occur in a 
        ' '' '' '' ''given block of code, while still running code.

        '' '' '' ''Try
        '' '' '' ''    'Open the connection
        '' '' '' ''    xlsConn.Open()

        '' '' '' ''    'Set the command connection to opened connection
        '' '' '' ''    cmd.Connection = xlsConn

        '' '' '' ''    'Set the command type to CommandType.Text in order to use SQL statment constructed here 
        '' '' '' ''    'in code editor
        '' '' '' ''    cmd.CommandType = CommandType.Text

        '' '' '' ''    'Assigned the command text to query the excel as shown below
        '' '' '' ''    cmd.CommandText = ("select * from [Sheet1$]")

        '' '' '' ''    'Assign the cmd to dataadapter
        '' '' '' ''    da.SelectCommand = cmd

        '' '' '' ''    'Fill the datatable with data from excel file using DataAdapter
        '' '' '' ''    da.Fill(dt)

        '' '' '' ''Catch
        '' '' '' ''    'This block Handle the exception.
        '' '' '' ''    MsgBox(ErrorToString)
        '' '' '' ''Finally
        '' '' '' ''    'We need to close the connection and set to nothing. This code will still execute even the code raised an error
        '' '' '' ''    xlsConn.Close()
        '' '' '' ''    xlsConn = Nothing
        '' '' '' ''End Try
        '' '' '' ''Return dt
    End Function


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ''Me.AxWindowsMediaPlayer1.URL = "C:\WrongBox.wma"
        'Me.AxWindowsMediaPlayer1.URL = "C:\SampleMP3.mp3"
        'Const DATA_FILE_EXTENSION As String = ".mp3"
        'Dim dlgFileDialog As New OpenFileDialog
        'With dlgFileDialog
        '    .Filter = DATA_FILE_EXTENSION & _
        '    " files (*" & DATA_FILE_EXTENSION & "|*" & DATA_FILE_EXTENSION
        '    .FilterIndex = 1
        '    .RestoreDirectory = True
        '    If .ShowDialog() = DialogResult.OK Then
        '        'Play the sound file
        '        Me.AxWindowsMediaPlayer1.URL = dlgFileDialog.FileName
        '    End If
        'End With
    End Sub

    Dim sMaterialCode, sMateiralName, sInnerBoxMatCode As String

    Dim sInnerWeightAvailable As String
    Dim nInnerBoxSize As Integer

    Dim nSizeCount, nPackQty, nBigSize As Integer

    
  
    Private Sub Loadgrdinfo()
        ''Try


        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdCartonDtlsV1.RowCount
        For i = 0 To ngrdRowCount
            grdCartonDtlsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdCartonDtlsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        grdCartonDtls.DataSource = myccKHLIOutstandingWithJobcard.LoadWeltedPackedInfo(sFKJobcardId)

        With grdCartonDtlsV1

            


            .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        End With


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub MultipleScreens()
        ''TO Update after Attaching Multiple Screen''
        '' '' ''Dim nUpperBound As Integer

        '' '' ''Dim Screens() As Screen = screen.AllScreens
        '' '' ''MsgBox(Screens.Count)
        '' '' ''LoadOutstanding()
        ''TO Update after Attaching Multiple Screen''
        'ScreenShotCapture()
    End Sub
    Dim screen As Screen

    Private Sub LoadOutstanding()

        screen = screen.AllScreens(1)

        frmKHLIScanningWeightOnly.StartPosition = FormStartPosition.Manual
        frmKHLIScanningWeightOnly.Location = screen.Bounds.Location '+ Point(100, 100)
        frmKHLIScanningWeightOnly.Show()


    End Sub

 
    Private Sub ScreenShotCapture()
        'Dim bounds As Rectangle
        'Dim screenshot As System.Drawing.Bitmap
        'Dim graph As Graphics
        'bounds = screen.PrimaryScreen.Bounds
        'screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        'graph = Graphics.FromImage(screenshot)
        'graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        'PictureBox1.Image = screenshot

        'Dim area As Rectangle
        'Dim capture As System.Drawing.Bitmap
        'Dim graph As Graphics
        'area = frmKHLIOuterAndInnerScanning.Bounds
        'capture = New System.Drawing.Bitmap(area.Width, area.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        'graph = Graphics.FromImage(capture)
        'graph.CopyFromScreen(area.X, area.Y, 0, 0, area.Size, CopyPixelOperation.SourceCopy)
        'PictureBox1.Image = capture

        Dim bm As Bitmap
        Dim gr As Graphics

        'bm = New Bitmap(screen.PrimaryScreen.Bounds.Width, screen.PrimaryScreen.Bounds.Height, Drawing.Imaging.PixelFormat.Format32bppArgb)
        'gr = Graphics.FromImage(bm)
        'gr.CopyFromScreen(screen.PrimaryScreen.Bounds.X, screen.PrimaryScreen.Bounds.Y, 0, 0, NewSize(screen.PrimaryScreen.Bounds.Width, screen.PrimaryScreen.Bounds.Height))

        Dim workingRectangle As System.Drawing.Rectangle = screen.PrimaryScreen.WorkingArea

        ' Set the size of the form slightly less than size of  
        ' working rectangle. 
        Me.Size = New System.Drawing.Size(workingRectangle.Width - 10, workingRectangle.Height - 10)

        ' Set the location so the entire form is visible. 
        Me.Location = New System.Drawing.Point(5, 5)

        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        'bounds = screen.PrimaryScreen.Bounds
        screenshot = New System.Drawing.Bitmap(150, 100, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(0, 0, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        PictureBox1.Image = screenshot

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mdlSGM.ScrrenShotCapture()
        Me.PictureBox1.Image.Save("d:\\capture.jpg")
    End Sub

    Private Sub tbInnerBoxNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbInnerBoxNo.TextChanged

    End Sub

    Private Sub tbOuterCartonNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbOuterCartonNo.TextChanged

    End Sub

    Private Sub tbDifference_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If sWeightFrom = "Actual" Then
        '    If Val(tbDifference.Text) > 0.5 Or Val(tbDifference.Text) < -0.5 Then
        '        MsgBox("Difference Above +/- 500 Grms will not be accepted")
        '        tbInnerBoxNo.Enabled = False
        '    Else
        '        tbInnerBoxNo.Enabled = True
        '    End If
        'End If
    End Sub

    Private Sub tbWei02_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbWei02.TextChanged

    End Sub

    Private Sub tbJobcardNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbJobcardNo.TextChanged

    End Sub
End Class