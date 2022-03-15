Imports System.Net.Mail
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Public Class frmSMS
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim smtpServer As New SmtpClient()
    '    Dim mail As New MailMessage
    '    smtpServer.Credentials = New Net.NetworkCredential(TextBox1.Text & "@gmail.com", TextBox2.Text)
    '    smtpServer.Port = 587
    '    smtpServer.Host = "smtp.gmail.com"
    '    smtpServer.EnableSsl = True
    '    mail.From = New MailAddress(TextBox1.Text & "@gmail.com")
    '    If RadioButton1.Checked = True Then
    '        mail.To.Add("91" & TextBox3.Text & "@m3m.in")
    '    ElseIf RadioButton2.Checked = True Then
    '        mail.To.Add(TextBox3.Text)
    '    End If
    '    mail.Subject = TextBox4.Text
    '    mail.Body = TextBox5.Text()
    '    smtpServer.Send(mail)
    '    MsgBox("mail is sent", MsgBoxStyle.OkOnly, "Report")
    'End Sub

    'Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
    '    TextBox3.Enabled = True
    'End Sub

    'Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
    '    TextBox3.Enabled = True
    'End Sub


    'Inherits System.Web.UI.Page
    Partial Class _Default

        'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '    Dim sURL As String
        '    Dim objReader As StreamReadersURL = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=yourapicode&senderid=WEBSMS&channel=2&DCS=0&flashsms=0&number=91989xxxxxxx&text=test message&route=1"

        '    Dim sResponse As WebRequest
        '    sResponse = WebRequest.Create(sURL)

        '    Try
        '        Dim objStream As Stream
        '        objStream = sResponse.GetResponse.GetResponseStream()
        '        objReader = New StreamReader(objStream)
        '        Response.Write(objReader.ReadToEnd())
        '        objReader.Close()
        '    Catch ex As Exception
        '        ex.Message()
        '    End Try
        'End Sub

    End Class


End Class
