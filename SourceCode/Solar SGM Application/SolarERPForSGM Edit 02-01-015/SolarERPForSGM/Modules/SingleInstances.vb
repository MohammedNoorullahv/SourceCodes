Module SingleInstances

    'Entry point for the application. Main checks for a prior instance and closes
    'this one if it finds a prior one running.
    Public Sub Main()

        If CheckForDuplicateProcess("MyAppName") Then
            Dim dupProcess As String
            dupProcess = "There is another instance of MyAppName" & _
        " running on this machine." & vbCrLf & _
        "This new instance must close." & vbCrLf & vbCrLf & _
        "Only 1 instance of MyAppName can exist" & _
        " on a machine." & vbCrLf & vbCrLf

            MsgBox(dupProcess, MsgBoxStyle.Critical, "Duplicate Process Detected")

            Application.Exit()

        Else
            '****** Change The FormName Below *****
            'Change the name of the form to load, to the one 
            'applicable for your application
            'Application.Run(New Form1)
        End If
    End Sub

    Private Function CheckForDuplicateProcess(ByVal processName As String) As Boolean
        'function returns true if it finds more than one 'processName' running
        Dim Procs() As Process
        Dim proc As Process
        'get ALL processes running on this machine in all desktops
        'this also finds all services running as well.
        Procs = Process.GetProcesses()
        Dim count As Integer = 0
        For Each proc In Procs
            If proc.ProcessName.ToString.Equals(processName) Then
                count += 1
            End If
        Next proc
        If count > 1 Then
            Return True
        Else
            Return False
        End If
    End Function

End Module
