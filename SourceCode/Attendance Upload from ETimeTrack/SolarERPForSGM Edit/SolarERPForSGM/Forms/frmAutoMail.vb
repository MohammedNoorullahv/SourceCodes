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

Public Class frmAutoMail

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccSLIOutstandng As New ccSLIOutstandng


    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Try
            'Loadgrdinfo()
            SendMail()
            'SendMail1()
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
        Timer1.Enabled = True
        sIsMessageSent = "N"
        nHour = 0
        nMinute = 0
    End Sub



    Dim keyascii As Integer
    Dim sIsMessageSent As String = "N"
    Dim dMessageTime As Date
    Dim nHour, nMinute As Integer

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        lblTime.Text = Format(Date.Now, "hh:mm:ss-tt")
        dMessageTime = DateTime.Parse("10:00:00 AM")

        'lblCountDown.Text = (DateDiff(DateInterval.Hour, dMessageTime, Date.Now)).ToString + " : " + (DateDiff(DateInterval.Minute, dMessageTime, Date.Now)).ToString + " : " + (Format(DateDiff(DateInterval.Second, dMessageTime, Date.Now), "ss")).ToString

        If sIsMessageSent = "N" Then
            If nHour = 0 Then
                Dim daSelMailTime As New SqlDataAdapter("select * from MailCategory Where Duration = 'DLY'", sConstr)
                Dim dsSelMailTime As New DataSet
                daSelMailTime.Fill(dsSelMailTime)

                nHour = Val(dsSelMailTime.Tables(0).Rows(0).Item("MailTimingHrs").ToString)
                nMinute = Val(dsSelMailTime.Tables(0).Rows(0).Item("MailTimingMins").ToString)


            End If
            If Format(Date.Now, "hh") = nHour And Format(Date.Now, "mm") = nMinute And Format(Date.Now, "tt") = "AM" Then
                sIsMessageSent = "Y"
                Loadgrdinfo()

            End If

        End If

        If Format(Date.Now, "hh") = "10" And Format(Date.Now, "mm") = "40" And Format(Date.Now, "tt") = "AM" Then
            End
        End If
        'lblTimeDifference.Text = "UT-" + Trim(Str(ncfTotalLoginHours)).PadLeft(2, CChar(CStr(0))) + ":" + Trim(Str(ncfTotalLoginMinutes)).PadLeft(2, CChar(CStr(0))) + ":" + Trim(Str(ncfTotalLoginSeconds)).PadLeft(2, CChar(CStr(0)))

    End Sub
    Dim sIsLoaded, sSeason, sTypeofOrder, sFileName, sMailingSeason As String
    Private Sub Loadgrdinfo()
        Try
            sIsLoaded = "N"
            Dim daSelMailCategory As New SqlDataAdapter("Select * from MailCategory", sConstr)
            Dim dsSelMailCategory As New DataSet
            daSelMailCategory.Fill(dsSelMailCategory)

            Dim j As Integer = 1

            For j = 1 To 4
                If j = 1 Then
                    sSeason = dsSelMailCategory.Tables(0).Rows(0).Item("Season1").ToString
                ElseIf j = 2 Then
                    sSeason = dsSelMailCategory.Tables(0).Rows(0).Item("Season2").ToString
                ElseIf j = 3 Then
                    sSeason = dsSelMailCategory.Tables(0).Rows(0).Item("Season3").ToString
                ElseIf j = 4 Then
                    sSeason = dsSelMailCategory.Tables(0).Rows(0).Item("Season4").ToString
                End If

                If sSeason <> "" Then
                    If j = 1 Then
                        sMailingSeason = sSeason
                    Else
                        sMailingSeason = sMailingSeason + " + " + sSeason
                    End If
                    sTypeofOrder = "FULLSHOE"
                    grdPendingExcessArrivals.DataSource = myccSLIOutstandng.LoadOutstanding(sSeason, sTypeofOrder)

                    With grdPendingExcessArrivalsV1
                        .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                        Dim i As Integer = 2
                        For i = 2 To 15
                            .Columns(i).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        Next
                    End With

                    sFileName = sSeason + "-" + sTypeofOrder

                    grdPendingExcessArrivals.ExportToXlsx("D:\" + sFileName + ".xlsx")

                    xlBook = xl.Workbooks.Open("D:\" + sFileName + ".xlsx")
                    xlSheet1 = xlBook.Worksheets(1)
                    xlSheet1.Range("A1:R1").Columns.AutoFit()
                    xlBook.Close(SaveChanges:=True)

                    sTypeofOrder = "WELTED"
                    grdPendingExcessArrivals.DataSource = myccSLIOutstandng.LoadOutstanding(sSeason, sTypeofOrder)

                    With grdPendingExcessArrivalsV1
                        .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                        Dim i As Integer = 2
                        For i = 2 To 15
                            .Columns(i).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                        Next
                    End With


                    sFileName = sSeason + "-" + sTypeofOrder

                    grdPendingExcessArrivals.ExportToXlsx("D:\" + sFileName + ".xlsx")
                    xlBook = xl.Workbooks.Open("D:\" + sFileName + ".xlsx")
                    xlSheet1 = xlBook.Worksheets(1)
                    xlSheet1.Range("A1:R1").Columns.AutoFit()
                    xlBook.Close(SaveChanges:=True)
                End If
            Next
            'MsgBox("Completed")
            SendMail()
            'End
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
    Private Sub grdPendingExcessArrivalsV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdPendingExcessArrivalsV1.RowStyle
        'If sIsLoaded = "Y" Then
        '    If e.RowHandle > -1 Then
        '        If grdPendingExcessArrivalsV1.GetRowCellValue(e.RowHandle, grdPendingExcessArrivalsV1.Columns(0)).ToString() = "TOTAL" Then
        '            e.Appearance.ForeColor = Color.DarkRed
        '            e.Appearance.BackColor = Color.Yellow
        '        End If
        '    End If
        'End If
    End Sub


    Dim sTransactionNo, sMailId As String

    Dim xl As New Excel.Application
    Dim xlBook As Excel.Workbook
    Dim xlSheet1 As Excel.Worksheet

    Private Sub cbExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExporttoExcel.Click
        grdPendingExcessArrivals.ExportToXlsx("D:\Pending Excess Arrivals.xlsx")

        xlBook = xl.Workbooks.Open("D:\Pending Excess Arrivals.xlsx")
        xlSheet1 = xlBook.Worksheets(1)


        xlSheet1.Range("A1:R1").Columns.AutoFit()


        xlBook.Close(SaveChanges:=True)

        MsgBox("Successfully Exported")

    End Sub


    Private Sub SendMail()
        Try


            Dim Mail As New MailMessage
            'Mail.From = New MailAddress("cplan@ahindia.com", "cplan")
            Mail.From = New MailAddress("Noor2677@gmail.com", "cplan")

            'MailMessage.To.Add(New MailAddress("ppc@ahindia.com"))
            'MailMessage.[To].Add(New MailAddress("erp@ahindia.com, ppc@ahindia.com"))

            Dim daSelMailList As New SqlDataAdapter("Select * from MAILCATEGORYDtls", sConstr)
            Dim dsSelMailList As New DataSet
            daSelMailList.Fill(dsSelMailList)

            Dim i = 0
            sMailId = ""

            For i = 0 To dsSelMailList.Tables(0).Rows.Count - 1
                If Trim(dsSelMailList.Tables(0).Rows(i).Item("ContactType").ToString) = "TO" Then
                    sMailId = Trim(dsSelMailList.Tables(0).Rows(i).Item("EMailID").ToString)
                    Mail.[To].Add(New MailAddress(sMailId))
                ElseIf Trim(dsSelMailList.Tables(0).Rows(i).Item("ContactType").ToString) = "CC" Then
                    sMailId = Trim(dsSelMailList.Tables(0).Rows(i).Item("EMailID").ToString)
                    Mail.[CC].Add(New MailAddress(sMailId))
                ElseIf Trim(dsSelMailList.Tables(0).Rows(i).Item("ContactType").ToString) = "BCC" Then
                    sMailId = Trim(dsSelMailList.Tables(0).Rows(i).Item("EMailID").ToString)
                    Mail.[Bcc].Add(New MailAddress(sMailId))
                End If
                
            Next
            'Mail.To.Add("Noorullahv@gmail.com")

            'Mail.IsBodyHtml = True
            Mail.Subject = "UPPER TO FULL SHOE OUTSTANDING REPORT AS ON " + Format(Date.Now, "dd-MMM-yyyy") + " Seasons :- " + sMailingSeason
            Dim strMessage As String
            strMessage = "Dear All" & vbCrLf & vbCrLf & _
            "Please find attached Upper to FullShoe Outstanding Status / Report ( " + sMailingSeason + " ) As on " + Format(Date.Now, "dd-MMM-yyyy") & vbCrLf & _
            "" & vbCrLf & vbCrLf & _
            "This is an autogenerated mail."
            Mail.Body = strMessage
            Mail.Priority = MailPriority.Normal

            Dim SMTP As New SmtpClient

            'SMTP.Host = "smtp3.netcore.co.in"
            'SMTP.Port = 465
            'SMTP.UseDefaultCredentials = True
            'SMTP.Credentials = New System.Net.NetworkCredential("cplan@ahindia.com", "Spa@dat#15")

            SMTP.Host = "smtp.gmail.com"
            SMTP.Port = 587
            SMTP.UseDefaultCredentials = False
            SMTP.EnableSsl = True
            'SMTP.Credentials = New System.Net.NetworkCredential(SenderEmail.Trim(), SenderEmailPassword.Trim())
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network
            'SMTP.Port = PORT
            SMTP.Credentials = New System.Net.NetworkCredential("Noor2677@gmail.com", "77arshiya")

            Dim attachment As System.Net.Mail.Attachment
            attachment = New System.Net.Mail.Attachment("D:\SS19-FULLSHOE.xlsx")
            Mail.Attachments.Add(attachment)

            attachment = New System.Net.Mail.Attachment("D:\AW19-FULLSHOE.xlsx")
            Mail.Attachments.Add(attachment)

            attachment = New System.Net.Mail.Attachment("D:\SS19-WELTED.xlsx")
            Mail.Attachments.Add(attachment)

            attachment = New System.Net.Mail.Attachment("D:\AW19-WELTED.xlsx")
            Mail.Attachments.Add(attachment)

            SMTP.EnableSsl = False
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network

            SMTP.Send(Mail)

            
            
            
            



            





            Exit Sub

            
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub SendMail1()
        Dim mail As New MailMessage()
        mail.From = New MailAddress("Noor2677@gmail.com", "ERP")
        mail.To.Add("Noorullahv@gmail.com")
        mail.IsBodyHtml = True
        mail.Subject = "Registration"
        mail.Body = "Some Text"
        mail.Priority = MailPriority.High

        Dim SMTP As New SmtpClient()
        'SMTPServer.Host = "smtp.gmail.com"

        SMTP.Host = "smtp.gmail.com"

        SMTP.Port = 587
        'SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        'SMTPServer.UseDefaultCredentials = True
        'Smtp.UseDefaultCredentials = True
        SMTP.UseDefaultCredentials = True
        SMTP.Credentials = New System.Net.NetworkCredential("Noor2677@gmail.com", "77arshiya")

        SMTP.EnableSsl = True
        SMTP.DeliveryMethod = SmtpDeliveryMethod.Network

        SMTP.Send(mail)
    End Sub
End Class