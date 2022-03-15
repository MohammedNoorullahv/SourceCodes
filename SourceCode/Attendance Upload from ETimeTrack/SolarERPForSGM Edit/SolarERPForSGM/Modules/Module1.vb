Module Module1
    Sub Main()

        'Dim sendsmscall As New SendSMSVBDotNet.SendSms
        Dim url As String
        Dim url1 As String
        Dim yourAuthKey As String
        Dim message As String
        Dim senderid As String
        Dim routeid As String
        Dim mobileNo As String
        Dim smsType As String
        Dim scheduleTime As String
        Dim signature As String
        Dim groupName As String
        Dim builder As New System.Text.StringBuilder

        url = "Sample URL for Post and Get"    ' URL = "Sample"  'eg-- www.abc.com       
        yourAuthKey = "Sample Auth key" 'eg -- 16 digits alphanumeric
        message = "Sample message to sent" '"Hello this is test"
        senderid = "Sample Sender Id" '"Sample" 'eg -- Testin'
        routeid = "Sampe route Id" 'eg 1
        mobileNo = "Sample Mobile No" 'eg-- '99999999xx,99999998xx
        smsType = "english" 'eg - english or unicode  
        scheduleTime = "Sample Schedule time"
        signature = "Sample Signature"
        groupName = "Sample Group Name"
        'sendsmscall.sendSmsGet(url, yourAuthKey, message, senderid, routeid, mobileNo, smsType, scheduleTime, signature, groupName)
        Console.ReadLine()


    End Sub

End Module
