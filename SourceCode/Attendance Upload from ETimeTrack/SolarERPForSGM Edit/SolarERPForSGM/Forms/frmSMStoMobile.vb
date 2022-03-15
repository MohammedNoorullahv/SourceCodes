Imports System.IO.Ports
Imports System.IO

Public Class frmSMStoMobile

    Dim SerialPort As New System.IO.Ports.SerialPort()
    Dim CR As String


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If SerialPort.IsOpen Then
            SerialPort.Close()
        End If
        'SerialPort.PortName = COM4
        SerialPort.BaudRate = 9600
        SerialPort.Parity = Parity.None
        SerialPort.StopBits = StopBits.One
        SerialPort.DataBits = 8
        SerialPort.Handshake = Handshake.RequestToSend
        SerialPort.DtrEnable = True
        SerialPort.RtsEnable = True
        SerialPort.NewLine = vbCrLf
        Dim message As String
        message = MsgRichTextBox.Text
        SerialPort.Open()
        If SerialPort.IsOpen() Then
            SerialPort.Write("AT" & vbCrLf)
            SerialPort.Write("AT+CMGF=1" & vbCrLf)
            SerialPort.Write("AT+CMGS=" & Chr(34) & phoneNumBox.Text & Chr(34) & vbCrLf)
            SerialPort.Write(message & Chr(26))
            'SentPicture.Visible = True
            'SentLabel.Visible = True
            'SentTimer.Start()
        Else
            MsgBox("Port not available")
        End If
    End Sub
End Class