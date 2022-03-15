Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle
Imports System.Net.Mail

'Imports PCComm

Public Class frmSARACOuterAndInnerScanning

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
        SendSMS()
    End Sub

    Private Sub SendSMS()
        'Dim message As New MailMessage()
        'message.To.Add("9003637786@airtelcellular.net")
        'message.To.Add("noor2677@gmail.com")
        'message.From = New MailAddress("Noor2677@gmail.com")
        'message.Subject = "Hi"
        'message.Body = "SMS"
        'Dim smtp As New SmtpClient("smtp.gmail.com")
        'smtp.EnableSsl = True
        'smtp.Credentials = New System.Net.NetworkCredential("Noor2677@gmail.com", "77arshiya")
        'smtp.Send(message)



    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToXls("D:\ArticleMaster.xls")
        MsgBox("Export Completed")

    End Sub

    Dim keyascii As Integer
    Dim sJobcardNo, sOrderNo, sWeightAvailable, sArticleGroup, sArticleName, sArticleColour, sArticleId As String
    Dim nSize, nQuantityinSize As Integer
    Dim sSalesOrderDetailsID As String

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

            Dim daSelJobcard As New SqlDataAdapter("Select * from SalesOrderDetails where CustomerOrderNo = '" & Trim(tbJobcardNo.Text) & "'", sCnn)
            Dim dsSelJobcard As New DataSet
            daSelJobcard.Fill(dsSelJobcard)

            If dsSelJobcard.Tables(0).Rows.Count <> 1 Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Customer Order No")
                sSalesOrderDetailsID = ""
                sOrderNo = ""
                sArticleId = ""
                sArticleGroup = ""
                sArticleName = ""
                sArticleColour = ""
            Else
                sSalesOrderDetailsID = dsSelJobcard.Tables(0).Rows(0).Item("ID")
                sOrderNo = dsSelJobcard.Tables(0).Rows(0).Item("CustomerOrderNo")
                sArticleId = dsSelJobcard.Tables(0).Rows(0).Item("ArticleDetailID")
                sArticleGroup = dsSelJobcard.Tables(0).Rows(0).Item("ArticleGroup")
                sArticleName = dsSelJobcard.Tables(0).Rows(0).Item("Article")
                sArticleColour = dsSelJobcard.Tables(0).Rows(0).Item("ColorName")


                'CheckforOuterWeight()
                'If sWeightAvailable = "N" Then
                '    Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                '    Exit Sub
                'End If

                LoadPackedInformations()
                tbOuterCartonNo.Focus()
            End If
            Loadgrdinfo()
        End If

        'Catch ex As Exception

        'End Try
    End Sub

    Dim nCartonNo As Integer
    Dim sImageFileName, sPackingDetailID, sCartonNo As String

    Private Sub tbOuterCartonNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbOuterCartonNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then



            If Trim(tbOuterCartonNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Outer CartonNo")
                Exit Sub
            End If

            'Dim daSelPackingListInfo As New SqlDataAdapter("Select Top 1 * from EUPackingDetail where HID = (Select ID from EUPacking where SalesOrderDetailID = '" & sSalesOrderDetailsID & _
            '                                           "') order by cartonNo", sCnn)
            'Dim dsSelPackingListInfo As New DataSet
            'daSelPackingListInfo.Fill(dsSelPackingListInfo)

            nCartonNo = Microsoft.VisualBasic.Right(Trim(tbOuterCartonNo.Text), 2)
            sCartonNo = Microsoft.VisualBasic.Right(Trim(tbOuterCartonNo.Text), 2)
            tbBoxNo.Text = nCartonNo


            Dim daSelPackingListInfo As New SqlDataAdapter("Select * from EUPackingDetail where HID = (Select ID from EUPacking where SalesOrderDetailID = '" & sSalesOrderDetailsID & _
                                                           "') And CartonNo = '" & nCartonNo & "' order by cartonNo", sCnn)
            Dim dsSelPackingListInfo As New DataSet
            daSelPackingListInfo.Fill(dsSelPackingListInfo)

            If dsSelPackingListInfo.Tables(0).Rows.Count = 0 Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Outer CartonNo")
                Exit Sub
            End If

            If dsSelPackingListInfo.Tables(0).Rows(0).Item("IsPacked").ToString <> "" Then
                If dsSelPackingListInfo.Tables(0).Rows(0).Item("IsPacked").ToString = True Then
                    Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                    MsgBox("This Outer Carton already scanned", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            
            sPackingDetailID = dsSelPackingListInfo.Tables(0).Rows(0).Item("ID")
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

            'CheckforInnerWeight()

            
            'CalculateWeight()

            'LoadWeightfromWeighingMachine()

            'If Val(tbDifference.Text) > 0.5 Or Val(tbDifference.Text) < -0.5 Then
            '    MsgBox("Difference Above +/- 500 Grms will not be accepted")
            '    tbInnerBoxNo.Enabled = False
            'Else
            '    tbInnerBoxNo.Enabled = True
            'End If
            UpdateWeightInformationinSecondaryMonitor()

            LoadScannedData()

            tbInnerBoxNo.Focus()

        End If


        Loadgrdinfo()
        'Catch ex As Exception

        'End Try
    End Sub

    Dim nPkgQty As Integer
    Dim nPKGWeight, nCartonWeight, nPKGGrossWeight As Decimal

    Dim sPoNr, sBoxNr As String
    Private Sub tbInnerBoxNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbInnerBoxNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then

            If Trim(tbOuterCartonNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("Invalid Inner Box No")
            End If


            Dim daSel5PJC As New SqlDataAdapter("Select * from FivePairJobCard Where Barcode20 = '" & Trim(tbInnerBoxNo.Text) & "'", sConstr)
            Dim dsSel5PJC As New DataSet
            daSel5PJC.Fill(dsSel5PJC)

            If dsSel5PJC.Tables(0).Rows.Count = 0 Then
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("Invalid Inner Box No")
                Exit Sub
            End If

            If dsSel5PJC.Tables(0).Rows(0).Item("IsScanned").ToString <> "" Then
                If dsSel5PJC.Tables(0).Rows(0).Item("IsScanned").ToString = True Then
                    Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                    MsgBox("This Package already scanned", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            

            If Val(Microsoft.VisualBasic.Mid(Trim(tbInnerBoxNo.Text), 11, 2)) <> nCartonNo Then
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("The scanned Package does not belong to this outer carton", MsgBoxStyle.Critical)
                Exit Sub
            End If

            nSize = Val(dsSel5PJC.Tables(0).Rows(0).Item("Size"))
            nPkgQty = Val(dsSel5PJC.Tables(0).Rows(0).Item("Quantity"))

            CheckforInnerWeight()

            If sInnerWeightAvailable = "N" Then
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("Weight is not available for the scanned Package", MsgBoxStyle.Critical)
                Exit Sub
            Else
                nPKGWeight = Math.Round((nPkgQty * nUpperWeight), 4)
            End If

            sPoNr = dsSel5PJC.Tables(0).Rows(0).Item("PONr").ToString
            sBoxNr = dsSel5PJC.Tables(0).Rows(0).Item("BoxNr").ToString

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
                If nQuantityinSize = 1 And (Val(tbIQ01.Text) + nPkgQty > Val(tbOQ01.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 2 And (Val(tbIQ02.Text) + nPkgQty > Val(tbOQ02.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 3 And (Val(tbIQ03.Text) + nPkgQty > Val(tbOQ03.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 4 And (Val(tbIQ04.Text) + nPkgQty > Val(tbOQ04.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 5 And (Val(tbIQ05.Text) + nPkgQty > Val(tbOQ05.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 6 And (Val(tbIQ06.Text) + nPkgQty > Val(tbOQ06.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 7 And (Val(tbIQ07.Text) + nPkgQty > Val(tbOQ07.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 8 And (Val(tbIQ08.Text) + nPkgQty > Val(tbOQ08.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 9 And (Val(tbIQ09.Text) + nPkgQty > Val(tbOQ09.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 10 And (Val(tbIQ10.Text) + nPkgQty > Val(tbOQ10.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 11 And (Val(tbIQ11.Text) + nPkgQty > Val(tbOQ11.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 12 And (Val(tbIQ12.Text) + nPkgQty > Val(tbOQ12.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 13 And (Val(tbIQ13.Text) + nPkgQty > Val(tbOQ13.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 14 And (Val(tbIQ14.Text) + nPkgQty > Val(tbOQ14.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 15 And (Val(tbIQ15.Text) + nPkgQty > Val(tbOQ15.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 16 And (Val(tbIQ16.Text) + nPkgQty > Val(tbOQ16.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 17 And (Val(tbIQ17.Text) + nPkgQty > Val(tbOQ17.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
                If nQuantityinSize = 18 And (Val(tbIQ18.Text) + nPkgQty > Val(tbOQ18.Text)) Then : Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3" : MsgBox("Quantity Exceeds, for this Box") : GoTo Aa : End If
            End If



            If nQuantityinSize = 1 Then : tbIQ01.Text = Val(tbIQ01.Text) + nPkgQty
            ElseIf nQuantityinSize = 2 Then : tbIQ02.Text = Val(tbIQ02.Text) + nPkgQty
            ElseIf nQuantityinSize = 3 Then : tbIQ03.Text = Val(tbIQ03.Text) + nPkgQty
            ElseIf nQuantityinSize = 4 Then : tbIQ04.Text = Val(tbIQ04.Text) + nPkgQty
            ElseIf nQuantityinSize = 5 Then : tbIQ05.Text = Val(tbIQ05.Text) + nPkgQty
            ElseIf nQuantityinSize = 6 Then : tbIQ06.Text = Val(tbIQ06.Text) + nPkgQty
            ElseIf nQuantityinSize = 7 Then : tbIQ07.Text = Val(tbIQ07.Text) + nPkgQty
            ElseIf nQuantityinSize = 8 Then : tbIQ08.Text = Val(tbIQ08.Text) + nPkgQty
            ElseIf nQuantityinSize = 9 Then : tbIQ09.Text = Val(tbIQ09.Text) + nPkgQty
            ElseIf nQuantityinSize = 10 Then : tbIQ10.Text = Val(tbIQ10.Text) + nPkgQty
            ElseIf nQuantityinSize = 11 Then : tbIQ11.Text = Val(tbIQ11.Text) + nPkgQty
            ElseIf nQuantityinSize = 12 Then : tbIQ12.Text = Val(tbIQ12.Text) + nPkgQty
            ElseIf nQuantityinSize = 13 Then : tbIQ13.Text = Val(tbIQ13.Text) + nPkgQty
            ElseIf nQuantityinSize = 14 Then : tbIQ14.Text = Val(tbIQ14.Text) + nPkgQty
            ElseIf nQuantityinSize = 15 Then : tbIQ15.Text = Val(tbIQ15.Text) + nPkgQty
            ElseIf nQuantityinSize = 16 Then : tbIQ16.Text = Val(tbIQ16.Text) + nPkgQty
            ElseIf nQuantityinSize = 17 Then : tbIQ17.Text = Val(tbIQ17.Text) + nPkgQty
            ElseIf nQuantityinSize = 18 Then : tbIQ18.Text = Val(tbIQ18.Text) + nPkgQty
            End If



            Dim daUPD5PJC As New SqlDataAdapter("Update FivePairJobCard Set IsScanned = '1', ScannedOn = '" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & _
                                                "', Weight = '" & nPKGWeight & "'  Where Barcode20 = '" & Trim(tbInnerBoxNo.Text) & "'", sConstr)
            Dim dsUPD5PJC As New DataSet
            daUPD5PJC.Fill(dsUPD5PJC)

            tbInnerBoxNo.Clear()

            If Val(tbIQ01.Text) + Val(tbIQ02.Text) + Val(tbIQ03.Text) + Val(tbIQ04.Text) + Val(tbIQ05.Text) + Val(tbIQ06.Text) + Val(tbIQ07.Text) + Val(tbIQ08.Text) + Val(tbIQ09.Text) + Val(tbIQ10.Text) + Val(tbIQ11.Text) + Val(tbIQ12.Text) + Val(tbIQ13.Text) + Val(tbIQ14.Text) + Val(tbIQ15.Text) + Val(tbIQ16.Text) + Val(tbIQ17.Text) + Val(tbIQ18.Text) >= Val(tbBoxQty.Text) Then

                CheckforInnerWeight()
                LoadWeightfromWeighingMachine()

                Dim daUpdOuterCarton As New SqlDataAdapter("Update EUPackingDetail Set IsPacked = '1', PackedOn = '" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & _
                                                           "', UpperWeight = '" & nPKGWeight & "', CartonWeight = '" & nCartonWeight & _
                                                           "', TotalWeight = '" & nPKGGrossWeight & _
                                                           "', ActualWeight = '" & Val(tbActualWeight.Text) & "' Where ID = '" & sPackingDetailID & "'", sCnn)
                Dim dsUpdOuterCarton As New DataSet
                daUpdOuterCarton.Fill(dsUpdOuterCarton)
                dsUpdOuterCarton.AcceptChanges()


                sImageFileName = sOrderNo + "-" + tbOuterCartonNo.Text
                mdlSGM.ScrrenShotCapture()
                '' ''Me.PictureBox1.Image.Save("G:\Packing Images\" + sImageFileName + ".jpg")

                LoadPackedInformations()

                Clear()
                tbOuterCartonNo.Focus()
            Else
                tbInnerBoxNo.Focus()
            End If




Aa:


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
    Dim nJCSize, nJCQty As Integer
    Dim nWeight As Decimal
    Dim sHID As String
    Private Sub LoadPackedInformations()
        'Try

        ''Size''

        ClearTotalPackedInfo()

        Dim daSelPackingListInfo As New SqlDataAdapter("Select Top 1 * from EUPackingDetail where HID = (Select ID from EUPacking where SalesOrderDetailID = '" & sSalesOrderDetailsID & _
                                                       "') order by cartonNo", sCnn)
        Dim dsSelPackingListInfo As New DataSet
        daSelPackingListInfo.Fill(dsSelPackingListInfo)

        If dsSelPackingListInfo.Tables(0).Rows.Count = 0 Then
            Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
            MsgBox("Packing List Not Generated for this Jobcard", MsgBoxStyle.Critical)

            Exit Sub
        End If
        sHID = dsSelPackingListInfo.Tables(0).Rows(0).Item("HID").ToString
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
        Dim daSelJCQty As New SqlDataAdapter("Select * from SalesOrderDetails where ID = '" & sSalesOrderDetailsID & "'", sCnn)
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
        Dim daSelPkdQty As New SqlDataAdapter("SELECT     TOP (100) PERCENT SUM(Quantity) AS Quantity, SUM(Quantity01) AS Quantity01, SUM(Quantity02) AS Quantity02, SUM(Quantity03) AS Quantity03, SUM(Quantity04) AS Quantity04, SUM(Quantity05) AS Quantity05, SUM(Quantity06) AS Quantity06, SUM(Quantity07) AS Quantity07, SUM(Quantity08) AS Quantity08, SUM(Quantity09) AS Quantity09, SUM(Quantity10) AS Quantity10, SUM(Quantity11) AS Quantity11, SUM(Quantity12) AS Quantity12, SUM(Quantity13) AS Quantity13, SUM(Quantity14) AS Quantity14, SUM(Quantity15) AS Quantity15, SUM(Quantity16) AS Quantity16, SUM(Quantity17) AS Quantity17, SUM(Quantity18) AS Quantity18 FROM EUPackingDetail WHERE     (HID = '" & sHID & "') AND (IsPacked = 1)", sCnn)
        Dim dsSelPkdQty As New DataSet
        daSelPkdQty.Fill(dsSelPkdQty)

        

        tbPQ01.Text = dsSelPkdQty.Tables(0).Rows(0).Item(1).ToString
        tbPQ02.Text = dsSelPkdQty.Tables(0).Rows(0).Item(2).ToString
        tbPQ03.Text = dsSelPkdQty.Tables(0).Rows(0).Item(3).ToString
        tbPQ04.Text = dsSelPkdQty.Tables(0).Rows(0).Item(4).ToString
        tbPQ05.Text = dsSelPkdQty.Tables(0).Rows(0).Item(5).ToString
        tbPQ06.Text = dsSelPkdQty.Tables(0).Rows(0).Item(6).ToString
        tbPQ07.Text = dsSelPkdQty.Tables(0).Rows(0).Item(7).ToString
        tbPQ08.Text = dsSelPkdQty.Tables(0).Rows(0).Item(8).ToString
        tbPQ09.Text = dsSelPkdQty.Tables(0).Rows(0).Item(9).ToString
        tbPQ10.Text = dsSelPkdQty.Tables(0).Rows(0).Item(10).ToString
        tbPQ11.Text = dsSelPkdQty.Tables(0).Rows(0).Item(11).ToString
        tbPQ12.Text = dsSelPkdQty.Tables(0).Rows(0).Item(12).ToString
        tbPQ13.Text = dsSelPkdQty.Tables(0).Rows(0).Item(13).ToString
        tbPQ14.Text = dsSelPkdQty.Tables(0).Rows(0).Item(14).ToString
        tbPQ15.Text = dsSelPkdQty.Tables(0).Rows(0).Item(15).ToString
        tbPQ16.Text = dsSelPkdQty.Tables(0).Rows(0).Item(16).ToString
        tbPQ17.Text = dsSelPkdQty.Tables(0).Rows(0).Item(17).ToString
        tbPQ18.Text = dsSelPkdQty.Tables(0).Rows(0).Item(18).ToString


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

 
    Dim sOUBCartonNumber, sOUBSalesOrder, sOUBArticle, sOUBSizes As String
    Dim nOUBBoxSlNo, nOUBQuantity As Integer

    Dim nINBSlNo, nINBQty As Integer
    Dim sINBSalesOrder, sINBUPCCode, sINBTracingId, sINBArticle, sINBColour, sINBSize, sINBSizUS, sINBText, sINBENColour, sINBFRColour As String
    Dim curFile As String
    Dim excelapp As New Application


    Dim dtStudentGrade As DataTable
    Dim dtExcelData As DataTable

 

   
    Dim sMaterialCode, sMateiralName, sInnerBoxMatCode As String

    Private Sub CheckforOuterWeight()
        sWeightAvailable = "Y"
        'Dim daSelOuterCartons As New SqlDataAdapter("Select Distinct(MaterialCode), Description From DemandByJobcard where JobcardNo = '" & sJobcardNo & _
        '                                            "' And MaterialCode like 'PAC-SHB%'", sCnn)
        Dim daSelOuterCartons As New SqlDataAdapter("Select Distinct(a.OuterBoxMatCode),b.Description, a.InnerBoxMatCode From OuterCartonMapping a, Materials b Where a.OuterBoxMatCode = b.MaterialCode And a.InnerBoxMatCode in (Select Distinct(MaterialCode) From DemandByJobcard where JobcardNo  = '" & sJobcardNo & _
                                                    "' And MaterialCode like 'PAC-SHB%') Order by b.Description", sCnn)

        Dim dsSelOuterCartons As New DataSet
        daSelOuterCartons.Fill(dsSelOuterCartons)

        Dim i As Integer = 0

        For i = 0 To dsSelOuterCartons.Tables(0).Rows.Count - 1
            sMaterialCode = dsSelOuterCartons.Tables(0).Rows(i).Item(0)
            sMateiralName = dsSelOuterCartons.Tables(0).Rows(i).Item(1)
            sInnerBoxMatCode = dsSelOuterCartons.Tables(0).Rows(i).Item(2)

            Dim daSelWeight As New SqlDataAdapter("Select * from InnerOuterCartonWeight Where MaterialCode = '" & sMaterialCode & _
                                                  "' And InnerBox = '" & sInnerBoxMatCode & "'", sCnn)
            Dim dsSelWeight As New DataSet
            daSelWeight.Fill(dsSelWeight)

            If dsSelWeight.Tables(0).Rows.Count <= 0 Then
                sWeightAvailable = "N"
                plOuterCartonInfo.Visible = True
                plOuterCartonInfo.BringToFront()
                tbOCCode.Text = sMaterialCode
                tbOCDescription.Text = sMateiralName
                tbOCBoxQuantity.Focus()
                Exit Sub
            End If
        Next

        tbJobcardNo.Focus()

    End Sub

    Private Sub cbOCSaveOuterCartonInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOCSaveOuterCartonInfo.Click
        If Val(tbOCBoxQuantity.Text) < 0 Or Val(tbOCWeight.Text) < 0 Then
            Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
            MsgBox("Outer Carton Quantity / Outer Carton Weight is should not be Zero", MsgBoxStyle.Critical)
            Exit Sub
        End If
AA:
        If Val(tbOCWeight.Text) > 2 Then
            Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
            MsgBox("Outer Carton Weight Will not be more than 2 KGS", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim sGUID As String
        sGUID = System.Guid.NewGuid.ToString()

        Dim daInsOutWeight As New SqlDataAdapter("Insert Into InnerOuterCartonWeight Values ('" & sGUID & "','OUT','','','','','0','" & Val(tbOCBoxQuantity.Text) & _
                                                 "','" & sInnerBoxMatCode & "','" & sMaterialCode & "','" & sMateiralName & "','" & Val(tbOCWeight.Text) & _
                                                 "','','" & Format(Date.Now, "dd-MMM-yyyy:hh:mm:ss") & _
                                                 "','','','','')", sCnn)
        Dim dsInsOutWeight As New DataSet
        daInsOutWeight.Fill(dsInsOutWeight)
        dsInsOutWeight.AcceptChanges()

        MsgBox("Outer Carton Weght Updated")

        tbOCBoxQuantity.Clear()
        tbOCWeight.Clear()
        plOuterCartonInfo.Visible = False


        CheckforOuterWeight()
    End Sub

    Dim sInnerWeightAvailable As String
    Dim nInnerBoxSize As Integer
    Dim nUpperWeight As Decimal

    Private Sub CheckforInnerWeight()
        sInnerWeightAvailable = "Y"
        nUpperWeight = 0
        Dim daSelInnerCartons As New SqlDataAdapter("Select UW.Customer,UW.SalesOrderNo,UW.ArticleDetailId,UW.Season,UW.SalesOrderDetailsId,UW.SizeName,uwd.Size,uwd.UpperWeight,uwd.OthersWeight from upperweight As UW, UpperWeightDetails As UWD where UW.ID = UWD.HID And UW.IsActive = '1'  And UW.ArticleDetailId = '" & sArticleId & _
                                                    "' And Size = '" & nSize.ToString & "' Order by uw.ArticleDetailId,uwd.Size", sCnn)
        Dim dsSelInnerCartons As New DataSet
        daSelInnerCartons.Fill(dsSelInnerCartons)
        'adsa()
        Dim i As Integer = 0

        If dsSelInnerCartons.Tables(0).Rows.Count > 0 Then
            nUpperWeight = Val(dsSelInnerCartons.Tables(0).Rows(0).Item("UpperWeight").ToString) + Val(dsSelInnerCartons.Tables(0).Rows(0).Item("OthersWeight").ToString)

        Else
            MsgBox("", MsgBoxStyle.Critical)
            sInnerWeightAvailable = "N"
            Exit Sub
        End If

        Dim daTotalWeight As New SqlDataAdapter("Select ISNULL(Sum(Weight),0) AS Weight From FivePairJobCard Where PONr = '" & sPoNr & _
                                                       "' And BoxNr = '" & sBoxNr & "'", sConstr)
        Dim dsTotalWeight As New DataSet
        daTotalWeight.Fill(dsTotalWeight)

        nPKGWeight = Val(dsTotalWeight.Tables(0).Rows(0).Item("Weight").ToString)
        nCartonWeight = 1
        nPKGGrossWeight = Math.Round((nPKGWeight + nCartonWeight), 4)

        tbGrossWeight.Text = Math.Round((nUpperWeight + nPKGWeight + nCartonWeight), 4)

        tbJobcardNo.Focus()

    End Sub

    Private Sub cbOCSaveInnerCartonInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOCSaveInnerCartonInfo.Click
        If Val(tbICBoxQuantity.Text) < 0 Or Val(tbICWeight.Text) < 0 Then
            Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
            MsgBox("Inner Carton Size / Inner Carton Weight is should not be Zero", MsgBoxStyle.Critical)
            Exit Sub
        End If
AA:
        If Val(tbICWeight.Text) > 2 Then
            Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
            MsgBox("Inner Carton Weight Will not be more than 2 KGS", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim sGUID As String
        sGUID = System.Guid.NewGuid.ToString()

        Dim daInsOutWeight As New SqlDataAdapter("Insert Into InnerOuterCartonWeight Values ('" & sGUID & "','INN','" & sArticleId & "','" & sArticleGroup & _
                                                 "','" & sArticleName & "','" & sArticleColour & "','" & nInnerBoxSize & _
                                                 "','1','','" & sMaterialCode & "','" & sMateiralName & "','" & Val(tbICWeight.Text) & _
                                                 "','','" & Format(Date.Now, "dd-MMM-yyyy:hh:mm:ss") & _
                                                 "','','','','')", sCnn)
        Dim dsInsOutWeight As New DataSet
        daInsOutWeight.Fill(dsInsOutWeight)
        dsInsOutWeight.AcceptChanges()

        MsgBox("Inner Carton Weght Updated")

        tbICBoxQuantity.Clear()
        tbICWeight.Clear()
        plInnerCartonInfo.Visible = False


        CheckforInnerWeight()
    End Sub

    Private Sub PostWeight()

        If nJCSize = Val(tbJSize01.Text) Then : tbWei01.Text = nWeight
        ElseIf nJCSize = Val(tbJSize02.Text) Then : tbWei02.Text = nWeight
        ElseIf nJCSize = Val(tbJSize03.Text) Then : tbWei03.Text = nWeight
        ElseIf nJCSize = Val(tbJSize04.Text) Then : tbWei04.Text = nWeight
        ElseIf nJCSize = Val(tbJSize05.Text) Then : tbWei05.Text = nWeight
        ElseIf nJCSize = Val(tbJSize06.Text) Then : tbWei06.Text = nWeight
        ElseIf nJCSize = Val(tbJSize07.Text) Then : tbWei07.Text = nWeight
        ElseIf nJCSize = Val(tbJSize08.Text) Then : tbWei08.Text = nWeight
        ElseIf nJCSize = Val(tbJSize09.Text) Then : tbWei09.Text = nWeight
        ElseIf nJCSize = Val(tbJSize10.Text) Then : tbWei10.Text = nWeight
        ElseIf nJCSize = Val(tbJSize11.Text) Then : tbWei11.Text = nWeight
        ElseIf nJCSize = Val(tbJSize12.Text) Then : tbWei12.Text = nWeight
        ElseIf nJCSize = Val(tbJSize13.Text) Then : tbWei13.Text = nWeight
        ElseIf nJCSize = Val(tbJSize14.Text) Then : tbWei14.Text = nWeight
        ElseIf nJCSize = Val(tbJSize15.Text) Then : tbWei15.Text = nWeight
        ElseIf nJCSize = Val(tbJSize16.Text) Then : tbWei16.Text = nWeight
        ElseIf nJCSize = Val(tbJSize17.Text) Then : tbWei17.Text = nWeight
        ElseIf nJCSize = Val(tbJSize18.Text) Then : tbWei18.Text = nWeight
        End If
    End Sub
    Dim nSizeCount, nPackQty, nBigSize As Integer

    Private Sub CalculateWeight()
        nSizeCount = 0
        nPackQty = 0
        nBigSize = 0

        If Val(tbOQ01.Text) > 0 Then : nBigSize = Val(tbOS01.Text) : nJCSize = Val(tbOS01.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ01.Text) : End If
        If Val(tbOQ02.Text) > 0 Then : nBigSize = Val(tbOS02.Text) : nJCSize = Val(tbOS02.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ02.Text) : End If
        If Val(tbOQ03.Text) > 0 Then : nBigSize = Val(tbOS03.Text) : nJCSize = Val(tbOS03.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ03.Text) : End If
        If Val(tbOQ04.Text) > 0 Then : nBigSize = Val(tbOS04.Text) : nJCSize = Val(tbOS04.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ04.Text) : End If
        If Val(tbOQ05.Text) > 0 Then : nBigSize = Val(tbOS05.Text) : nJCSize = Val(tbOS05.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ05.Text) : End If
        If Val(tbOQ06.Text) > 0 Then : nBigSize = Val(tbOS06.Text) : nJCSize = Val(tbOS06.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ06.Text) : End If
        If Val(tbOQ07.Text) > 0 Then : nBigSize = Val(tbOS07.Text) : nJCSize = Val(tbOS07.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ07.Text) : End If
        If Val(tbOQ08.Text) > 0 Then : nBigSize = Val(tbOS08.Text) : nJCSize = Val(tbOS08.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ08.Text) : End If
        If Val(tbOQ09.Text) > 0 Then : nBigSize = Val(tbOS09.Text) : nJCSize = Val(tbOS09.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ09.Text) : End If
        If Val(tbOQ10.Text) > 0 Then : nBigSize = Val(tbOS10.Text) : nJCSize = Val(tbOS10.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ10.Text) : End If
        If Val(tbOQ11.Text) > 0 Then : nBigSize = Val(tbOS11.Text) : nJCSize = Val(tbOS11.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ11.Text) : End If
        If Val(tbOQ12.Text) > 0 Then : nBigSize = Val(tbOS12.Text) : nJCSize = Val(tbOS12.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ12.Text) : End If
        If Val(tbOQ13.Text) > 0 Then : nBigSize = Val(tbOS13.Text) : nJCSize = Val(tbOS13.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ13.Text) : End If
        If Val(tbOQ14.Text) > 0 Then : nBigSize = Val(tbOS14.Text) : nJCSize = Val(tbOS14.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ14.Text) : End If
        If Val(tbOQ15.Text) > 0 Then : nBigSize = Val(tbOS15.Text) : nJCSize = Val(tbOS15.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ15.Text) : End If
        If Val(tbOQ16.Text) > 0 Then : nBigSize = Val(tbOS16.Text) : nJCSize = Val(tbOS16.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ16.Text) : End If
        If Val(tbOQ17.Text) > 0 Then : nBigSize = Val(tbOS17.Text) : nJCSize = Val(tbOS17.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ17.Text) : End If
        If Val(tbOQ18.Text) > 0 Then : nBigSize = Val(tbOS18.Text) : nJCSize = Val(tbOS18.Text) : nSizeCount += 1 : nPackQty += Val(tbOQ18.Text) : End If


        tbInnerWeight.Text = Val(tbOQ01.Text) * Val(tbWei01.Text) + Val(tbOQ02.Text) * Val(tbWei02.Text) + Val(tbOQ03.Text) * Val(tbWei03.Text) + Val(tbOQ04.Text) * Val(tbWei04.Text) + Val(tbOQ05.Text) * Val(tbWei05.Text) + Val(tbOQ06.Text) * Val(tbWei06.Text) + Val(tbOQ07.Text) * Val(tbWei07.Text) + Val(tbOQ08.Text) * Val(tbWei08.Text) + Val(tbOQ09.Text) * Val(tbWei09.Text) + Val(tbOQ10.Text) * Val(tbWei10.Text) + Val(tbOQ11.Text) * Val(tbWei11.Text) + Val(tbOQ12.Text) * Val(tbWei12.Text) + Val(tbOQ13.Text) * Val(tbWei13.Text) + Val(tbOQ14.Text) * Val(tbWei14.Text) + Val(tbOQ15.Text) * Val(tbWei15.Text) + Val(tbOQ16.Text) * Val(tbWei16.Text) + Val(tbOQ17.Text) * Val(tbWei17.Text) + Val(tbOQ18.Text) * Val(tbWei18.Text)

        If nSizeCount > 1 Then
            nJCSize = nBigSize
        End If

        Dim daSelInnerCartons As New SqlDataAdapter("Select Distinct(MaterialCode), Description,Size From DemandByJobcard where JobcardNo = '" & sJobcardNo & _
                                                    "' And Size = '" & nJCSize & _
                                                    "' And MaterialCode like 'PAC-SHB%' Order by Size", sCnn)
        Dim dsSelInnerCartons As New DataSet
        daSelInnerCartons.Fill(dsSelInnerCartons)

        sMaterialCode = dsSelInnerCartons.Tables(0).Rows(0).Item(0)
Aa:
        Dim daSelWeight As New SqlDataAdapter("Select * from InnerOuterCartonWeight Where InnerBox = '" & sMaterialCode & _
                                              "' And BoxQuantity = '" & nPackQty & "'", sCnn)
        Dim dsSelWeight As New DataSet
        daSelWeight.Fill(dsSelWeight)

        If dsSelWeight.Tables(0).Rows.Count = 1 Then
            tbOuterBox.Text = dsSelWeight.Tables(0).Rows(0).Item("MaterialDescription")
            tbOuterWeight.Text = dsSelWeight.Tables(0).Rows(0).Item("Weight")
        ElseIf dsSelWeight.Tables(0).Rows.Count = 0 Then
            Dim daSelWeight1 As New SqlDataAdapter("Select * from InnerOuterCartonWeight Where InnerBox = '" & sMaterialCode & _
                                                   "' Order by BoxQuantity", sCnn)
            Dim dsSelWeight1 As New DataSet
            daSelWeight1.Fill(dsSelWeight1)

            Dim i As Integer = 0
            For i = 0 To dsSelWeight1.Tables(0).Rows.Count - 1
                If dsSelWeight1.Tables(0).Rows(i).Item("BoxQuantity") > nPackQty Then
                    nPackQty = dsSelWeight1.Tables(0).Rows(i).Item("BoxQuantity")
                    GoTo Aa
                End If
            Next
        Else
            tbOuterBox.Clear()
            tbOuterWeight.Clear()
        End If

        tbGrossWeight.Text = Math.Round((Val(tbInnerWeight.Text) + Val(tbOuterWeight.Text)), 2)

    End Sub
    ''######################################
    Dim sLastLIne As String

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        comm.Message = txtSend.Text
        comm.Type = CommManager.MessageType.Normal
        comm.WriteData(txtSend.Text)
        txtSend.Text = String.Empty
        txtSend.Focus()


        ''Weight from Reverse String''
        Dim sWeightfromMachine, sWeightReverseString, sWeightForwardString As String
        sWeightfromMachine = Microsoft.VisualBasic.Right(rtbDisplay.Text, 100)
        Dim nStartPosition, nEndPosition As Integer
        Dim i As Integer = 100
        nStartPosition = 0
        nEndPosition = 0
        Dim nCount As Integer = 1
        For i = 100 To 0 Step -1
Aa:
            If nEndPosition = 0 Then
                If Microsoft.VisualBasic.Mid(sWeightfromMachine, i, 1) = "=" Then
                    nEndPosition = i
                End If
            Else
                If nEndPosition <= 7 Then
                    GoTo Ab
                End If
                If Microsoft.VisualBasic.Mid(sWeightfromMachine, i, 1) = "=" Then
                    nStartPosition = i

                    sWeightReverseString = Microsoft.VisualBasic.Mid(sWeightfromMachine, nStartPosition, nEndPosition - nStartPosition)
                    sWeightForwardString = ""
                    Dim j As Integer = nEndPosition - nStartPosition

                    For j = nEndPosition - nStartPosition To 2 Step -1
                        sWeightForwardString = sWeightForwardString + Microsoft.VisualBasic.Mid(sWeightReverseString, j, 1)
                    Next


                    MsgBox(sWeightReverseString + " - " + sWeightForwardString + " ::: " + Str(nCount))
                    'If the difference is High Then to do this'
                    nCount = nCount + 1

                    nStartPosition = 0
                    nEndPosition = 0
                    GoTo Aa
                    'If the difference is High Then to do this'
                End If
            End If
            'MsgBox(i)
Ab:
        Next
        MsgBox("Loop Completed")
        Exit Sub

        ''Weight from Reverse String''


        sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 1)
        MsgBox("1st Line" + sLastLIne)

        sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 2)
        MsgBox("2nd Line" + sLastLIne)

        sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 3)
        MsgBox("3rd Line" + sLastLIne)

        sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 4)
        MsgBox("4th Line" + sLastLIne)

        sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 5)
        MsgBox("5th Line" + sLastLIne)
    End Sub

    Private Sub cboPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPort.SelectedIndexChanged
        comm.PortName = cboPort.Text()
    End Sub
    ''' <summary>
    ''' Method to initialize serial port
    ''' values to standard defaults
    ''' </summary>
    ''' 
    ''' 
    ''' 

    Private Sub SetDefaults()
        'cboPort.SelectedIndex = 0
        'cboBaud.SelectedText = "1200"
        'cboParity.SelectedIndex = 0
        'cboStop.SelectedIndex = 1
        'cboData.SelectedIndex = 1
    End Sub

    ''' <summary>
    ''' methos to load our serial
    ''' port option values
    ''' </summary>
    Private Sub LoadValues()
        comm.SetPortNameValues(cboPort)
        comm.SetParityValues(cboParity)
        comm.SetStopBitValues(cboStop)
    End Sub
    ''' <summary>
    ''' method to set the state of controls
    ''' when the form first loads
    ''' </summary>
    Private Sub SetControlState()
        rdoText.Checked = True
        cmdSend.Enabled = False
        cmdClose.Enabled = False
    End Sub
    Private Sub frmKHLIOuterAndInnerScanning_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MultipleScreens()
        LoadValues()
        SetDefaults()
        SetControlState()
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        comm.ClosePort()
        SetControlState()
        SetDefaults()
        cmdOpen.Enabled = True
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        comm.Parity = cboParity.Text
        comm.StopBits = cboStop.Text
        comm.DataBits = cboData.Text
        comm.BaudRate = cboBaud.Text
        comm.DisplayWindow = rtbDisplay
        comm.OpenPort()

        'cmdOpen.Enabled = False
        'cmdClose.Enabled = False
        'cmdSend.Enabled = False
        tbJobcardNo.Enabled = True
        tbOuterCartonNo.Enabled = True
    End Sub
    ''-----
    Private Sub rdoHex_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoHex.CheckedChanged
        If rdoHex.Checked() Then
            comm.CurrentTransmissionType = SolarERPForSGM.CommManager.TransmissionType.Hex
        Else
            comm.CurrentTransmissionType = SolarERPForSGM.CommManager.TransmissionType.Text
        End If
    End Sub

    Private Sub cboBaud_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBaud.SelectedIndexChanged
        comm.BaudRate = cboBaud.Text()
    End Sub

    Private Sub cboParity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboParity.SelectedIndexChanged
        comm.Parity = cboParity.Text()
    End Sub

    Private Sub cboStop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStop.SelectedIndexChanged
        comm.StopBits = cboStop.Text()
    End Sub

    Private Sub cboData_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboData.SelectedIndexChanged
        comm.StopBits = cboStop.Text()
    End Sub

    Private Sub LoadWeightfromWeighingMachine()
        '' '' '' '' ''        Dim nErrorcount As Integer = 0
        '' '' '' '' ''Aa:
        '' '' '' '' ''        comm.Message = txtSend.Text
        '' '' '' '' ''        comm.Type = CommManager.MessageType.Normal
        '' '' '' '' ''        comm.WriteData(txtSend.Text)
        '' '' '' '' ''        txtSend.Text = String.Empty
        '' '' '' '' ''        txtSend.Focus()

        '' '' '' '' ''        sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 1)
        '' '' '' '' ''        If Trim(sLastLIne) <> "" Then
        '' '' '' '' ''            tbActualWeight.Text = Format(Math.Round(Val(Microsoft.VisualBasic.Right(sLastLIne, 7)), 3), "0.000")
        '' '' '' '' ''        Else
        '' '' '' '' ''            sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 2)
        '' '' '' '' ''            If Trim(sLastLIne) <> "" Then
        '' '' '' '' ''                tbActualWeight.Text = Format(Math.Round(Val(Microsoft.VisualBasic.Right(sLastLIne, 7)), 3), "0.000")
        '' '' '' '' ''            Else
        '' '' '' '' ''                sLastLIne = rtbDisplay.Lines(Val(UBound(rtbDisplay.Lines)) - 3)
        '' '' '' '' ''                tbActualWeight.Text = Format(Math.Round(Val(Microsoft.VisualBasic.Right(sLastLIne, 7)), 3), "0.000")
        '' '' '' '' ''            End If
        '' '' '' '' ''        End If

        '' '' '' '' ''        tbGrossWeight.Text = Math.Round((Val(tbInnerWeight.Text) + Val(tbOuterWeight.Text)), 2)

        '' '' '' '' ''        If Val(tbActualWeight.Text) = 0 Then
        '' '' '' '' ''            GoTo Aa
        '' '' '' '' ''            nErrorcount += 1
        '' '' '' '' ''            If nErrorcount >= 30 Then
        '' '' '' '' ''                MsgBox("Actaul Weight from Weighing was Not loaded properly")
        '' '' '' '' ''                Exit Sub
        '' '' '' '' ''            End If
        '' '' '' '' ''        End If

        '' '' '' ''Coding to Made Active If The Weighing Machine works on Reverse String Concept''
        ''Exit Sub
        ''Coding to Made Active If The Weighing Machine works on Reverse String Concept''

        ''Weight from Reverse String''
        Dim sWeightfromMachine, sWeightReverseString, sWeightForwardString As String
        sWeightfromMachine = Microsoft.VisualBasic.Right(rtbDisplay.Text, 100)
        Dim nStartPosition, nEndPosition As Integer
        Dim i As Integer = 100
        nStartPosition = 0
        nEndPosition = 0
        Dim nCount As Integer = 1
        For i = 100 To 0 Step -1
Ac:
            If nEndPosition = 0 Then
                If Microsoft.VisualBasic.Mid(sWeightfromMachine, i, 1) = "=" Then
                    nEndPosition = i
                End If
            Else
                If nEndPosition <= 7 Then
                    GoTo Ab
                End If
                If Microsoft.VisualBasic.Mid(sWeightfromMachine, i, 1) = "=" Then
                    nStartPosition = i

                    sWeightReverseString = Microsoft.VisualBasic.Mid(sWeightfromMachine, nStartPosition, nEndPosition - nStartPosition)
                    sWeightForwardString = ""
                    Dim j As Integer = nEndPosition - nStartPosition

                    For j = nEndPosition - nStartPosition To 2 Step -1
                        sWeightForwardString = sWeightForwardString + Microsoft.VisualBasic.Mid(sWeightReverseString, j, 1)
                    Next


                    tbActualWeight.Text = Format(Math.Round(Val(sWeightForwardString), 3), "0.000")

                    'MsgBox(sWeightReverseString + " - " + sWeightForwardString + " ::: " + Str(nCount))
                    'If the difference is High Then to do this'
                    nCount = nCount + 1

                    If Val(tbDifference.Text) > 1 Then
                        nStartPosition = 0
                        nEndPosition = 0
                        GoTo Ac
                    End If
                    Exit For
                    'If the difference is High Then to do this'
                End If
            End If
            'MsgBox(i)
Ab:
        Next



    End Sub

    Dim sWeightFrom As String

    Private Sub tbGrossWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbGrossWeight.TextChanged, tbActualWeight.TextChanged
        sWeightFrom = "Gross"
        tbDifference.Text = Format((Math.Round((Val(tbGrossWeight.Text) - Val(tbActualWeight.Text)), 3)), "0.000")
        sWeightFrom = "Actual"
    End Sub

    Private Sub tbActualWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbActualWeight.TextChanged
        sWeightFrom = "Actual"
        tbDifference.Text = Format((Math.Round((Val(tbGrossWeight.Text) - Val(tbActualWeight.Text)), 3)), "0.000")
    End Sub

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

        grdCartonDtls.DataSource = myccKHLIOutstandingWithJobcard.LoadPackedInfo(sSalesOrderDetailsID)

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

        myccKHLIOutstandingWithJobcard.LoadPackedStatus(sSalesOrderDetailsID)
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

    Private Sub UpdateWeightInformationinSecondaryMonitor()
        frmKHLIScanningWeightOnly.tbOuterBoxNo.Text = tbOuterCartonNo.Text
        frmKHLIScanningWeightOnly.tbOuterBox.Text = tbOuterBox.Text
        frmKHLIScanningWeightOnly.tbInnerWeight.Text = tbInnerWeight.Text
        frmKHLIScanningWeightOnly.tbOuterWeight.Text = tbOuterWeight.Text
        frmKHLIScanningWeightOnly.tbActualWeight.Text = tbActualWeight.Text
        frmKHLIScanningWeightOnly.tbGrossWeight.Text = tbGrossWeight.Text
        frmKHLIScanningWeightOnly.tbDifference.Text = tbDifference.Text
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

    Private Sub tbDifference_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbDifference.TextChanged
        'If sWeightFrom = "Actual" Then
        '    If Val(tbDifference.Text) > 0.5 Or Val(tbDifference.Text) < -0.5 Then
        '        MsgBox("Difference Above +/- 500 Grms will not be accepted")
        '        tbInnerBoxNo.Enabled = False
        '    Else
        '        tbInnerBoxNo.Enabled = True
        '    End If
        'End If
    End Sub

   
    Private Sub LoadScannedData()
        Dim daSelSQTY As New SqlDataAdapter("Select Size,ISNULL(Sum(QUANTITY),0) As SQty From FivePairJobCard Where PONr = '" & Trim(tbJobcardNo.Text) & _
                                            "' And IsScanned = '1' And BoxNr = '" & sCartonNo & "' Group By Size Order by Size", sConstr)
        Dim dsSelSQTY As New DataSet
        daSelSQTY.Fill(dsSelSQTY)

        If dsSelSQTY.Tables(0).Rows.Count > 0 Then


            nSize = Val(dsSelSQTY.Tables(0).Rows(0).Item("Size"))
            nPkgQty = Val(dsSelSQTY.Tables(0).Rows(0).Item("SQty"))

            If Val(tbOS01.Text) = nSize Then
                nQuantityinSize = 1
            ElseIf Val(tbOS02.Text) = nSize Then
                nQuantityinSize = 2
            ElseIf Val(tbOS03.Text) = nSize Then
                nQuantityinSize = 3
            ElseIf Val(tbOS04.Text) = nSize Then
                nQuantityinSize = 4
            ElseIf Val(tbOS05.Text) = nSize Then
                nQuantityinSize = 5
            ElseIf Val(tbOS06.Text) = nSize Then
                nQuantityinSize = 6
            ElseIf Val(tbOS07.Text) = nSize Then
                nQuantityinSize = 7
            ElseIf Val(tbOS08.Text) = nSize Then
                nQuantityinSize = 8
            ElseIf Val(tbOS09.Text) = nSize Then
                nQuantityinSize = 9
            ElseIf Val(tbOS10.Text) = nSize Then
                nQuantityinSize = 10
            ElseIf Val(tbOS11.Text) = nSize Then
                nQuantityinSize = 11
            ElseIf Val(tbOS12.Text) = nSize Then
                nQuantityinSize = 12
            ElseIf Val(tbOS13.Text) = nSize Then
                nQuantityinSize = 13
            ElseIf Val(tbOS14.Text) = nSize Then
                nQuantityinSize = 14
            ElseIf Val(tbOS15.Text) = nSize Then
                nQuantityinSize = 15
            ElseIf Val(tbOS16.Text) = nSize Then
                nQuantityinSize = 16
            ElseIf Val(tbOS17.Text) = nSize Then
                nQuantityinSize = 17
            ElseIf Val(tbOS18.Text) = nSize Then
                nQuantityinSize = 18
                'MsgBox("This Inner Box belongs to this Outer Carton")
            Else
                nQuantityinSize = 0
                Me.AxWindowsMediaPlayer1.URL = "C:\pingwarb-7327.mp3"
                MsgBox("This Inner Box Does Not belong to this Outer Carton")
            End If

            If nQuantityinSize = 1 Then : tbIQ01.Text = nPkgQty
            ElseIf nQuantityinSize = 2 Then : tbIQ02.Text = nPkgQty
            ElseIf nQuantityinSize = 3 Then : tbIQ03.Text = nPkgQty
            ElseIf nQuantityinSize = 4 Then : tbIQ04.Text = nPkgQty
            ElseIf nQuantityinSize = 5 Then : tbIQ05.Text = nPkgQty
            ElseIf nQuantityinSize = 6 Then : tbIQ06.Text = nPkgQty
            ElseIf nQuantityinSize = 7 Then : tbIQ07.Text = nPkgQty
            ElseIf nQuantityinSize = 8 Then : tbIQ08.Text = nPkgQty
            ElseIf nQuantityinSize = 9 Then : tbIQ09.Text = nPkgQty
            ElseIf nQuantityinSize = 10 Then : tbIQ10.Text = nPkgQty
            ElseIf nQuantityinSize = 11 Then : tbIQ11.Text = nPkgQty
            ElseIf nQuantityinSize = 12 Then : tbIQ12.Text = nPkgQty
            ElseIf nQuantityinSize = 13 Then : tbIQ13.Text = nPkgQty
            ElseIf nQuantityinSize = 14 Then : tbIQ14.Text = nPkgQty
            ElseIf nQuantityinSize = 15 Then : tbIQ15.Text = nPkgQty
            ElseIf nQuantityinSize = 16 Then : tbIQ16.Text = nPkgQty
            ElseIf nQuantityinSize = 17 Then : tbIQ17.Text = nPkgQty
            ElseIf nQuantityinSize = 18 Then : tbIQ18.Text = nPkgQty
            End If
        End If
    End Sub

End Class