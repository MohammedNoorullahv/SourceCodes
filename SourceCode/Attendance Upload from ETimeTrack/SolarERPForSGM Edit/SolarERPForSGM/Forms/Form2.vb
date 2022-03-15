
Imports System.Configuration
Imports System.IO
Imports System.DirectoryServices

''Imports System.Data.SqlClient
''Imports System.IO
''Imports System.Data.OleDb
''Imports System.Drawing
Imports Microsoft.Win32

Public Class mainWinForm

    Dim sUserName As String

    Dim myccUserLog As New ccUserLog
    Dim mystrUserLogStatus As New strUserLogStatus
    Dim mystrMessageHdr As New strMessageHdr
    Dim mystrMessageDtl As New strMessageDtl


    Private Sub bntStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntStart.Click
        'System.Diagnostics.Process.Start("shutdown.exe", "-l -t 600")

        'Dim nLen As Integer
        'Dim connection As String
        'Dim dssearch As System.DirectoryServices.DirectorySearcher
        'Dim sresult As System.DirectoryServices.SearchResult
        'Dim dresult As System.DirectoryServices.DirectoryEntry

        'nLen = Len(Page.User.Identity.Name)
        'Session("UserName") = ""
        'Session("UserName") = Mid(Page.User.Identity.Name, 15, nLen)

        sUserName = SystemInformation.UserName
        ' '' '' '' '' '' '' ''MsgBox(sUserName)

        sUserName = Environment.UserName.ToUpper
        sUserName = System.Windows.Forms.SystemInformation.UserName.ToUpper
        sUserName = sUserName.ToUpper
        MsgBox(sUserName)

        sUserName = System.Net.Dns.GetHostName
        '' '' '' '' '' '' '' ''MsgBox(sUserName)

        sUserName = System.Windows.Forms.SystemInformation.ComputerName
        MsgBox(sUserName)

        sUserName = System.Net.Dns.GetHostByName(sUserName).AddressList(0).ToString()
        MsgBox(sUserName)


        'Dim strHostName As String = "www.codeproject.com"
        'Dim strIPAddress As String = ""
        'Dim objAddressList() As System.Net.IPAddress = _
        '    System.Net.Dns.GetHostEntry(strHostName).AddressList
        'For x = 0 To objAddressList.GetUpperBound(0)
        '    If objAddressList(x).AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
        '        strIPAddress = objAddressList(x).ToString
        '        Exit For
        '    End If
        'Next
        Exit Sub
        FindingThreats()

    End Sub

    Sub FindingThreats()
        ' '' '' ''ListView1.Items.Clear()
        ' '' '' ''Dim childEntry As DirectoryEntry
        ' '' '' ''Dim ParentEntry As New DirectoryEntry
        ' '' '' ''Try
        ' '' '' ''    ParentEntry.Path = "WinNT:"
        ' '' '' ''    For Each childEntry In ParentEntry.Children
        ' '' '' ''        Select Case childEntry.SchemaClassName
        ' '' '' ''            Case "Domain"

        ' '' '' ''                Dim SubChildEntry As DirectoryEntry
        ' '' '' ''                Dim SubParentEntry As New DirectoryEntry
        ' '' '' ''                SubParentEntry.Path = "WinNT://" & childEntry.Name
        ' '' '' ''                For Each SubChildEntry In SubParentEntry.Children
        ' '' '' ''                    Select Case SubChildEntry.SchemaClassName
        ' '' '' ''                        Case "Computer"
        ' '' '' ''                            ListView1.Items.Add(SubChildEntry.Name)

        ' '' '' ''                    End Select
        ' '' '' ''                Next
        ' '' '' ''        End Select
        ' '' '' ''    Next
        ' '' '' ''Catch Excep As Exception
        ' '' '' ''    MsgBox("Error While Reading Directories : " + Excep.Message.ToString)
        ' '' '' ''Finally
        ' '' '' ''    ParentEntry = Nothing
        ' '' '' ''    MsgBox(ListView1.Items.Count)

        ' '' '' ''End Try
    End Sub

    Private Sub mainWinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            InsertLoginStatus()
            LoadActiveUsers()
            Timer1.Enabled = True
            Timer1.Interval = 1000

            tbSystemName.Text = System.Windows.Forms.SystemInformation.ComputerName
            tbLoginName.Text = System.Windows.Forms.SystemInformation.UserName.ToUpper
            tbUserName.Text = sSystemUserName

            nReceivedMessageId = 0

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Public sSystemId, sSystemUserName As String
    Public nMessageId As Integer
    Dim ngrdRowCount As Integer

    Public nMessageReceivedCount, nReceivedMessageId, nAcknowledgeRequiredforReceivedMessage As Integer
    Public sReceivedMessage, sSenderName, sSenderLoginId, sSenderSystemId, sSenderSystemName, sSentOn, sSenderMsgId As String
    Public dReceivedMessageSenTime As Date
    Dim dLoginTime As Date


    Private Sub InsertLoginStatus()
        Try
            myccUserLog.LoadSystemInfo(System.Windows.Forms.SystemInformation.UserName.ToUpper)

            sUserName = System.Windows.Forms.SystemInformation.UserName

            mystrUserLogStatus.SystemId = sSystemId
            mystrUserLogStatus.SystemName = System.Windows.Forms.SystemInformation.ComputerName
            mystrUserLogStatus.LoginName = System.Windows.Forms.SystemInformation.UserName.ToUpper
            sLoginName = System.Windows.Forms.SystemInformation.UserName.ToUpper
            mystrUserLogStatus.UserName = sSystemUserName
            
            Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)

            mystrUserLogStatus.IPAddress = h.AddressList.GetValue(0).ToString
            dLoginTime = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrUserLogStatus.LoginTime = dLoginTime 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrUserLogStatus.IsCurrentlyLoggedIn = 1
            mystrUserLogStatus.LogoutTime = dLoginTime 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrUserLogStatus.Duration = 0

            myccUserLog.InsertLoginStatus(mystrUserLogStatus)


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub LoadActiveUsers()
        ''Try

        Try
            Dim i As Integer = 0

Ab:
            ngrdRowCount = grdActiveUserV1.RowCount
            For i = 0 To ngrdRowCount
                grdActiveUserV1.DeleteRow(i)
            Next
            ngrdRowCount = grdActiveUserV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If

            grdActiveUser.DataSource = myccUserLog.LoadActiveUsers(sLoginName)

            With grdActiveUserV1

                .Columns(0).VisibleIndex = -1
                .Columns(1).VisibleIndex = -1
                .Columns(5).VisibleIndex = -1
                .Columns(6).VisibleIndex = -1
                .Columns(7).VisibleIndex = -1
                .Columns(8).VisibleIndex = -1
                .Columns(9).VisibleIndex = -1

                .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            End With

            If grdActiveUserV1.RowCount > 0 Then
                cbSelectSingle.Enabled = True
                cbSelectAll.Enabled = True
            Else
                cbSelectSingle.Enabled = False
                cbSelectAll.Enabled = False
            End If


            i = 0
Ac:
            ngrdRowCount = grdSelectedUserV1.RowCount
            For i = 0 To ngrdRowCount
                grdSelectedUserV1.DeleteRow(i)
            Next
            ngrdRowCount = grdSelectedUserV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ac
            End If

            grdSelectedUser.DataSource = myccUserLog.LoadMessageTo(sLoginName)

            With grdSelectedUserV1

                .Columns(0).VisibleIndex = -1
                .Columns(1).VisibleIndex = -1
                .Columns(2).VisibleIndex = -1
                .Columns(3).VisibleIndex = -1
                .Columns(5).VisibleIndex = -1
                .Columns(6).VisibleIndex = -1
                .Columns(7).VisibleIndex = -1
                .Columns(8).VisibleIndex = -1
                .Columns(9).VisibleIndex = -1
                .Columns(10).VisibleIndex = -1
                .Columns(11).VisibleIndex = -1

                .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            End With

            If grdSelectedUserV1.RowCount > 0 Then
                cbRemoveSingle.Enabled = True
                cbRemoveAll.Enabled = True
                cbSend.Enabled = True
            Else
                cbRemoveSingle.Enabled = False
                cbRemoveAll.Enabled = False
                cbSend.Enabled = False
            End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Logout()
        'System.Diagnostics.Process.Start("shutdown", "-s -t 00")
        End
    End Sub

    Dim ngrdRowNo As Integer
    Dim sLoginName, sMessageToSystemId As String


    Private Sub cbSelectSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelectSingle.Click

        Try
            ngrdRowNo = grdActiveUserV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Active Users available for Messaging  Purpose", MsgBoxStyle.Information)
                Exit Sub
            Else
                sMessageToSystemId = grdActiveUserV1.GetDataRow(ngrdRowNo).Item("SystemId")
                InsertMessageTo()
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub InsertMessageTo()
        myccUserLog.InsertMessageToTemp(sLoginName, sMessageToSystemId)
        LoadActiveUsers()
    End Sub

    Private Sub cbRemoveSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRemoveSingle.Click
        Try
            ngrdRowNo = grdSelectedUserV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Selected Users available for Removing Purpose", MsgBoxStyle.Information)
                Exit Sub
            Else
                sMessageToSystemId = grdSelectedUserV1.GetDataRow(ngrdRowNo).Item("MessageToID")
                DeleteMessageTo()
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub DeleteMessageTo()
        Try
            myccUserLog.DeleteMessageToTemp(sMessageToSystemId)
            LoadActiveUsers()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim dMessageDAte As Date

    Private Sub cbSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSend.Click
        Try
            dMessageDAte = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrMessageHdr.MsgDate = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrMessageHdr.MsgFrom = sLoginName
            mystrMessageHdr.FromSystemId = sSystemId
            mystrMessageHdr.Message = Trim(rtbMessage.Text)
            mystrMessageHdr.AcknowledgeRequired = chkbxAcknowledge.CheckState

            myccUserLog.InsertMessageHeader(mystrMessageHdr)

            myccUserLog.LoadMessageId(mystrMessageHdr)

            rtbMessage.Clear()

            Dim i As Integer

            For i = 0 To grdSelectedUserV1.RowCount - 1

                mystrMessageDtl.FKMessageHdr = nMessageId
                mystrMessageDtl.MessageTo = grdSelectedUserV1.GetDataRow(i).Item("SystemName")
                mystrMessageDtl.ToSystemId = grdSelectedUserV1.GetDataRow(i).Item("SystemId")
                mystrMessageDtl.AcknowledgeRequired = chkbxAcknowledge.CheckState
                mystrMessageDtl.SentTime = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
                mystrMessageDtl.AcknowledgeTime = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
                mystrMessageDtl.IsSeen = 0
                mystrMessageDtl.TimeofViewing = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
                mystrMessageDtl.ReplyOf = nReceivedMessageId

                myccUserLog.InsertMessageDetail(mystrMessageDtl)
                ''asdf()
            Next

            cbSelectSingle.Enabled = True
            cbSelectAll.Enabled = True
            cbRemoveSingle.Enabled = True
            cbRemoveAll.Enabled = True

            nReceivedMessageId = 0

            Timer1.Enabled = True
            Timer1.Interval = 1000

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CheckforNewMessage()
    End Sub
    
    Private Sub CheckforNewMessage()
        Try
            myccUserLog.LoadSentMessage(sSystemId)

            If nMessageReceivedCount = 0 Then
                plMessageInfo.Visible = False

                Timer1.Enabled = True
                Timer1.Interval = 1000
            Else
                Media.SystemSounds.Beep.Play()
                Me.WindowState = FormWindowState.Normal
                Me.BringToFront()

                ''Me.BringToFront()
                plMessageInfo.Visible = True
                plMessageInfo.BringToFront()
                rtbReceivedMessage.Text = sReceivedMessage
                tbFrom.Text = sSenderName
                tbSystem.Text = sSenderLoginId
                tbSentOn.Text = sSentOn
                If nAcknowledgeRequiredforReceivedMessage = 0 Then
                    cbAcknowledgement.Visible = False
                Else
                    cbAcknowledgement.Visible = True
                    cbAcknowledgement.BringToFront()
                End If

                Timer1.Enabled = False
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOK.Click
        Try
            Timer1.Enabled = True
            Timer1.Interval = 1000

            mystrMessageDtl.PKID = nReceivedMessageId

            mystrMessageDtl.SentTime = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            If nAcknowledgeRequiredforReceivedMessage = 0 Then
                mystrMessageDtl.AcknowledgeTime = dReceivedMessageSenTime
            Else
                mystrMessageDtl.AcknowledgeTime = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            End If

            mystrMessageDtl.IsSeen = 1
            mystrMessageDtl.TimeofViewing = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            ''mystrMessageDtl.ReplyOf = 0

            myccUserLog.UpdateMessageDetail(mystrMessageDtl)

            Me.WindowState = FormWindowState.Minimized
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbAcknowledgement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAcknowledgement.Click
        Try
            dMessageDAte = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrMessageHdr.MsgDate = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            mystrMessageHdr.MsgFrom = sLoginName
            mystrMessageHdr.FromSystemId = sSystemId
            mystrMessageHdr.Message = "Acknowledgment For :-" + vbCrLf + "=====================" + vbCrLf + vbCrLf + sReceivedMessage
            mystrMessageHdr.AcknowledgeRequired = 0

            myccUserLog.InsertMessageHeader(mystrMessageHdr)

            myccUserLog.LoadMessageId(mystrMessageHdr)

            mystrMessageDtl.FKMessageHdr = nMessageId
            mystrMessageDtl.MessageTo = sSenderSystemName
            mystrMessageDtl.ToSystemId = sSenderSystemId
            mystrMessageDtl.AcknowledgeRequired = 0
            mystrMessageDtl.SentTime = dMessageDAte
            mystrMessageDtl.AcknowledgeTime = dMessageDAte
            mystrMessageDtl.IsSeen = 0
            mystrMessageDtl.TimeofViewing = dMessageDAte
            mystrMessageDtl.ReplyOf = nReceivedMessageId

            myccUserLog.InsertMessageDetail(mystrMessageDtl)

            cbAcknowledgement.Visible = False
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub Logout()
        mystrUserLogStatus.SystemId = sSystemId
        mystrUserLogStatus.SystemName = System.Windows.Forms.SystemInformation.ComputerName
        mystrUserLogStatus.LoginName = System.Windows.Forms.SystemInformation.UserName.ToUpper
        sLoginName = System.Windows.Forms.SystemInformation.UserName.ToUpper
        mystrUserLogStatus.UserName = sSystemUserName

        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)

        mystrUserLogStatus.IPAddress = h.AddressList.GetValue(0).ToString
        'dLoginTime = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
        mystrUserLogStatus.LoginTime = dLoginTime 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
        mystrUserLogStatus.IsCurrentlyLoggedIn = 0
        mystrUserLogStatus.LogoutTime = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")

        Dim startTime As DateTime = dLoginTime ''DateTime.Now

        Dim endTime As DateTime = DateTime.Now ''DateTime.Now.AddSeconds(75)

        Dim span As TimeSpan = endTime.Subtract(startTime)
        'Console.WriteLine("Time Difference (seconds): " + span.Seconds)
        'Console.WriteLine("Time Difference (minutes): " + span.Minutes)
        'Console.WriteLine("Time Difference (hours): " + span.Hours)
        'Console.WriteLine("Time Difference (days): " + span.Days)

        Dim sDuration As String
        sDuration = Str(span.Days) + "-" + Str(span.Hours) + "." + Str(span.Minutes) + "." + Str(span.Seconds)

        mystrUserLogStatus.Duration = sDuration

        myccUserLog.UpdateLogoutStatus(mystrUserLogStatus)
    End Sub

    Private Sub cbSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSelectAll.Click

        Try
Aa:
            Dim i As Integer = 0

            For i = 0 To grdActiveUserV1.RowCount - 1
                'ngrdRowNo = grdActiveUserV1.FocusedRowHandle
                If ngrdRowNo < 0 Then
                    MsgBox("No Active Users available for Messaging  Purpose", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    sMessageToSystemId = grdActiveUserV1.GetDataRow(i).Item("SystemId")
                    InsertMessageTo()
                    GoTo Aa
                End If
            Next
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRemoveAll.Click
        Try
Aa:
            Dim i As Integer = 0

            For i = 0 To grdSelectedUserV1.RowCount - 1
                If ngrdRowNo < 0 Then
                    MsgBox("No Selected Users available for Removing Purpose", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    sMessageToSystemId = grdSelectedUserV1.GetDataRow(i).Item("MessageToID")
                    DeleteMessageTo()
                    GoTo Aa
                End If
            Next
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Private Sub cbRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRefresh.Click
        Try
            Dim i As Integer = 0

Ab:
            ngrdRowCount = grdPendingAcknowledgementV1.RowCount
            For i = 0 To ngrdRowCount
                grdPendingAcknowledgementV1.DeleteRow(i)
            Next
            ngrdRowCount = grdPendingAcknowledgementV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If

            grdPendingAcknowledgement.DataSource = myccUserLog.LoadPendingAcknowledgement(sLoginName)

            With grdPendingAcknowledgementV1

                .Columns(0).VisibleIndex = -1
                .Columns(1).VisibleIndex = -1
                .Columns(2).VisibleIndex = -1
                .Columns(3).VisibleIndex = -1
                .Columns(5).VisibleIndex = -1
                .Columns(6).VisibleIndex = -1
                .Columns(7).VisibleIndex = -1
                .Columns(8).VisibleIndex = -1
                .Columns(9).VisibleIndex = -1
                .Columns(10).VisibleIndex = -1
                '.Columns(11).VisibleIndex = -1

                .Columns(4).Width = 200
                .Columns(11).Width = 50

                .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            End With

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Private Sub cbExitHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExitHistory.Click
        plHistory.Visible = False
    End Sub

    Private Sub cbHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbHistory.Click
        plHistory.Visible = True
        plHistory.BringToFront()
    End Sub

    Private Sub cbRefreshHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRefreshHistory.Click
        Try
            Dim i As Integer = 0

Ab:
            ngrdRowCount = grdHistoryV1.RowCount
            For i = 0 To ngrdRowCount
                grdHistoryV1.DeleteRow(i)
            Next
            ngrdRowCount = grdHistoryV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If

            grdHistory.DataSource = myccUserLog.LoadHistory(sLoginName, Format(dpFromDate.Value, "dd-MMM-yyyy"), Format(((DateAdd(DateInterval.Day, 1, dpToDate.Value).Date)), "dd-MMM-yyyy"))

            With grdHistoryV1


                .Columns(1).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(1).DisplayFormat.FormatString = "dd-MMM-yyyy"
                .Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(8).DisplayFormat.FormatString = "dd-MMM-yyyy"
                .Columns(10).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(10).DisplayFormat.FormatString = "dd-MMM-yyyy"

                .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            End With

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExport2Excel.Click
        If rbSentHistory.Checked = True Then
            grdHistory.ExportToXls("D:\SentMessageHistory.xls")
        Else
            grdHistory.ExportToXls("D:\ReceivedMessageHistory.xls")
        End If
        MsgBox("Export Completed")
    End Sub

    Private Sub cbReply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReply.Click
        Try

            mystrMessageDtl.PKID = nReceivedMessageId

            mystrMessageDtl.SentTime = dMessageDAte 'Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            If nAcknowledgeRequiredforReceivedMessage = 0 Then
                mystrMessageDtl.AcknowledgeTime = dReceivedMessageSenTime
            Else
                mystrMessageDtl.AcknowledgeTime = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            End If

            mystrMessageDtl.IsSeen = 1
            mystrMessageDtl.TimeofViewing = Format(Date.Now, "dd-MMM-yyyy hh:mm:ss.sss")
            ''mystrMessageDtl.ReplyOf = 0

            myccUserLog.UpdateMessageDetail(mystrMessageDtl)


Aa:
            Dim i As Integer = 0

            For i = 0 To grdSelectedUserV1.RowCount - 1
                If ngrdRowNo < 0 Then
                    MsgBox("No Selected Users available for Removing Purpose", MsgBoxStyle.Information)
                    Exit Sub
                Else
                    sMessageToSystemId = grdSelectedUserV1.GetDataRow(i).Item("MessageToID")
                    DeleteMessageTo()
                    GoTo Aa
                End If
            Next

            sMessageToSystemId = sSenderSystemId
            InsertMessageTo()

            rtbMessage.Text = vbCrLf + vbCrLf + vbCrLf + "Reply Of :-" + vbCrLf + "===========" + vbCrLf + vbCrLf + sReceivedMessage
            rtbMessage.Focus()

            cbSelectSingle.Enabled = False
            cbSelectAll.Enabled = False
            cbRemoveSingle.Enabled = False
            cbRemoveAll.Enabled = False

            plMessageInfo.Visible = False

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub
End Class
