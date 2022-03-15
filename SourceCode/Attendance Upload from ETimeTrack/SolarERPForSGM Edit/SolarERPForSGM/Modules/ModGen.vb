Module ModGen


    Public gsErrFile As String = "D:\ErrorLog.txt"
    Public gbErrShow As Boolean = True


    Friend Sub HandleException(ByVal strModule As String, ByVal e As Exception)

        Dim strMessage As String
        Dim strCaption As String

        Try
            strMessage = "Exception: " & e.Message & vbCrLf & vbCrLf & "Module: " & strModule & vbCrLf & _
             "Method:  " & e.TargetSite.Name & vbCrLf & vbCrLf & "Please Notify Tech Support @ Ext:107 about this Issue." & vbCrLf & _
             "Please Provide the Support technician with Information shown in " & vbCrLf & _
             "this dialog box as well as an explanation of What you were " & vbCrLf & _
             "doing when this error occured."
            With System.Reflection.Assembly.GetExecutingAssembly.GetName.Version
                strCaption = "Unexpected Exception! Version: " & .Major & "." & .Minor & "." & Format(.Revision, "0000")
            End With


            Dim objStream As New System.IO.StreamWriter(gsErrFile, True)
            Dim strLogText As String
            'Create the Log Text
            strLogText = DateTime.Now & ControlChars.CrLf & _
            "Exception: " & e.Message & ControlChars.CrLf & _
            "Module:    " & strModule & ControlChars.CrLf & _
            "Method:    " & e.TargetSite.Name & ControlChars.CrLf & _
            "Stack:     " & e.StackTrace & ControlChars.CrLf
            objStream.WriteLine(strLogText)
            objStream.Flush()
            objStream.Close()
            objStream.Dispose()

            If gbErrShow Then
                MessageBox.Show(strMessage, strCaption, MessageBoxButtons.OK, _
    MessageBoxIcon.Exclamation)
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Module
