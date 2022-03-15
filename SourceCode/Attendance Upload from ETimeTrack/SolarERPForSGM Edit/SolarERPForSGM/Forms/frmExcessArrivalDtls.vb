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

Public Class frmExcessArrivalDtls

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard


    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Try
            Loadgrdinfo()
            'SendMail()
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
    End Sub



    Dim keyascii As Integer
    Dim sIsMessageSent As String = "N"

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        'lblTime.Text = Format(Date.Now, "tt-hh:mm:ss")

        If sIsMessageSent = "N" Then
            If Format(Date.Now, "hh") = "09" And Format(Date.Now, "mm") = "30" And Format(Date.Now, "tt") = "AM" Then
                sIsMessageSent = "Y"
                Loadgrdinfo()

            End If

        End If

        If Format(Date.Now, "hh") = "09" And Format(Date.Now, "mm") = "40" And Format(Date.Now, "tt") = "AM" Then
            End
        End If
        'lblTimeDifference.Text = "UT-" + Trim(Str(ncfTotalLoginHours)).PadLeft(2, CChar(CStr(0))) + ":" + Trim(Str(ncfTotalLoginMinutes)).PadLeft(2, CChar(CStr(0))) + ":" + Trim(Str(ncfTotalLoginSeconds)).PadLeft(2, CChar(CStr(0)))

    End Sub

    Private Sub Loadgrdinfo()
        Try


            grdPendingExcessArrivals.DataSource = myccKHLIOutstandingWithJobcard.LoadPendingExcessArrivals()

            With grdPendingExcessArrivalsV1

                .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


                .Columns(1).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(1).DisplayFormat.FormatString = "dd-MMM-yyyy"
                .Columns(12).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(12).DisplayFormat.FormatString = "dd-MMM-yyyy hh:mm:ss:sss"
                .Columns(13).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(13).DisplayFormat.FormatString = "dd-MMM-yyyy"
            End With

            grdPendingExcessArrivals.ExportToXlsx("D:\Pending Excess Arrivals.xlsx")
            SendMail()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim sTransactionNo As String

   
    Private Sub cbExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExporttoExcel.Click
        grdPendingExcessArrivals.ExportToXlsx("D:\Pending Excess Arrivals.xlsx")
        MsgBox("Successfully Exported")
    End Sub

    Private Sub SendMail()
        Try

        
            'Dim oMsg As System.Web.Mail.MailMessage = New System.Web.Mail.MailMessage()

            'oMsg.From = "noone@nobody.com"
            'oMsg.To = "someone@somewhere.com"
            'oMsg.Subject = "Email with Attachment Demo"
            'oMsg.Body = "This is the main body of the email"
            'Dim oAttch As MailAttachment = New MailAttachment("C:\myattachment.zip")
            'oMsg.Attachments.Add(oAttch)
            'SmtpMail.Send(oMsg)

            ''Option 01''
            Dim SMTPServer As New SmtpClient()
            'SMTPServer.Host = "smtp.gmail.com"
            SMTPServer.Host = "rainmail.ahindia.com"

            'SMTPServer.Port = 587
            SMTPServer.Port = 25

            'SMTPServer.EnableSsl = True

            SMTPServer.UseDefaultCredentials = False

            'SMTPServer.Credentials = New System.Net.NetworkCredential("noor2677@gmail.com", "77")
            SMTPServer.Credentials = New System.Net.NetworkCredential("erp", "yarabbi786")
            Dim MailMessage As New MailMessage()

            MailMessage.From = New MailAddress("erp@ahindia.com", "ERP", System.Text.Encoding.UTF8)

            'MailMessage.To.Add(New MailAddress("ppc@ahindia.com"))
            'MailMessage.[To].Add(New MailAddress("erp@ahindia.com, ppc@ahindia.com"))

            Dim daSelMailList As New SqlDataAdapter("Select * from AutoMailer Where MailName = 'EXAR' And Type = 'TO'", sConstr)
            Dim dsSelMailList As New DataSet
            daSelMailList.Fill(dsSelMailList)

            Dim i As Integer = 1
            Dim sMailId As String = ""
            Dim sMailId1 As String = ""
            For i = 0 To dsSelMailList.Tables(0).Rows.Count - 1
                sMailId = Trim(dsSelMailList.Tables(0).Rows(i).Item("EMailAddress"))
                MailMessage.[To].Add(New MailAddress(sMailId))
            Next

            'Dim nStartPosition, nEndPosition As Integer
            'nStartPosition = 1 : nEndPosition = 0
            'Dim sSelectedText As String
            'For i = 1 To Len(sMailId)
            '    If i = 1 Then
            '        nStartPosition = 1
            '    End If

            '    sSelectedText = Mid(sMailId, i, 1)

            '    If sSelectedText = "," Or i = Len(sMailId) Then
            '        sMailId1 = Mid(sMailId, nStartPosition, nEndPosition)
            '        MailMessage.[To].Add(New MailAddress(sMailId1))

            '        nStartPosition = i + 2
            '        nEndPosition = 0
            '    End If
            '    nEndPosition = nEndPosition + 1
            'Next


            Dim daSelMailListCC As New SqlDataAdapter("Select * from AutoMailer Where MailName = 'EXAR' And Type = 'CC'", sConstr)
            Dim dsSelMailListCC As New DataSet
            daSelMailListCC.Fill(dsSelMailListCC)

            i = 0
            sMailId = ""
            sMailId1 = ""

            
            For i = 0 To dsSelMailListCC.Tables(0).Rows.Count - 1
                sMailId = Trim(dsSelMailListCC.Tables(0).Rows(i).Item("EMailAddress"))
                MailMessage.[CC].Add(New MailAddress(sMailId))
            Next



            'nStartPosition = 1 : nEndPosition = 0
            'sSelectedText = ""
            'For i = 1 To Len(sMailId)
            '    If i = 1 Then
            '        nStartPosition = 1
            '    End If

            '    sSelectedText = Mid(sMailId, i, 1)

            '    If sSelectedText = "," Or i = Len(sMailId) Then
            '        sMailId1 = Mid(sMailId, nStartPosition, nEndPosition)
            '        MailMessage.[CC].Add(New MailAddress(sMailId1))

            '        nStartPosition = i + 2
            '        nEndPosition = 0
            '    End If
            '    nEndPosition = nEndPosition + 1
            'Next




            MailMessage.Subject = "Pending Excess Arrivals List As on" + Format(Date.Now, "dd-MMM-yyyy")

            Dim strMessage As String
            strMessage = "Dear All" & vbCrLf & vbCrLf & _
            "This is the pending list of Excess Material received against the PO Quantity, against which action/steps has to be taken" & vbCrLf & _
            "" & vbCrLf & vbCrLf & _
            "This is an autogenerated mail."

            MailMessage.Body = strMessage '"This is the list of Excess Material received against the PO Quantity." & vbCrLf & 

            Dim attachment As System.Net.Mail.Attachment
            attachment = New System.Net.Mail.Attachment("D:\Pending Excess Arrivals.xlsx")
            MailMessage.Attachments.Add(attachment)


            SMTPServer.Send(MailMessage)


            Exit Sub

            ''Option 01''

            ''Dim mail As New MailMessage()
            ''Dim SmtpServer As New SmtpClient("rainmail.ahindia.com")
            ''mail.From = New MailAddress("erp@ahindia.com")
            ''mail.To.Add("ppc@ahindia.com")
            ' ''mail.[To].Add("to_address")
            ''mail.Subject = "Test Mail - 1"
            ''mail.Body = "mail with attachment"

            '' ''Dim attachment As System.Net.Mail.Attachment
            '' ''attachment = New System.Net.Mail.Attachment("your attachment file")
            '' ''mail.Attachments.Add(attachment)

            ''SmtpServer.Port = 25 '587
            ''SmtpServer.Credentials = New System.Net.NetworkCredential("erp", "yarabbi786")
            ''SmtpServer.EnableSsl = True

            ''SmtpServer.Send(mail)
            ''MessageBox.Show("mail Send")

            '' ''Dim mail As New MailMessage()
            ' '' ''Dim att = New Attachment("J:\vb\RizRandom\RizRandom\Results\UBM.xlsx")
            '' ''Dim SmtpServer As New SmtpClient()


            '' ''Dim Smtp As SmtpClient = New SmtpClient("smtp.gmail.com", 587)


            '' ''Smtp.UseDefaultCredentials = False
            '' ''Smtp.Credentials = New Net.NetworkCredential("erp", "yarabbi786")
            '' ''Smtp.EnableSsl = True
            ' '' ''            mail.Attachments.Add(att)

            '' ''SmtpServer.Host = "smtp.gmail.com"
            '' ''mail = New MailMessage()
            '' ''mail.From = New MailAddress("Noor2677@gmail.com")
            '' ''mail.To.Add("erp@ahindia.com")
            '' ''mail.Subject = "Test Mail"
            '' ''mail.Body = "This is for testing SMTP mail from GMAIL"

            '' ''SmtpServer.Send(mail)
            '' ''MsgBox("mail send")
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
End Class