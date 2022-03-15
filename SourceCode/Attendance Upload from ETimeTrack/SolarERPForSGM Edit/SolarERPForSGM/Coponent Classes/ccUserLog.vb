Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strUserLogStatus

    Dim PKId As Integer
    Dim SystemId As String
    Dim SystemName As String
    Dim LoginName As String
    Dim UserName As String
    Dim IPAddress As String
    Dim LoginTime As Date
    Dim IsCurrentlyLoggedIn As Integer
    Dim LogoutTime As Date
    Dim Duration As String

End Structure

Public Structure strMessageHdr

    Dim PKID As Integer
    Dim MsgDate As Date
    Dim MsgFrom As String
    Dim FromSystemId As String
    Dim Message As String
    Dim AcknowledgeRequired As Integer

End Structure

Public Structure strMessageDtl

    Dim PKID As Integer
    Dim FKMessageHdr As Integer
    Dim MessageTo As String
    Dim ToSystemId As String
    Dim AcknowledgeRequired As Integer
    Dim SentTime As Date
    Dim AcknowledgeTime As Date
    Dim IsSeen As Integer
    Dim TimeofViewing As Date
    Dim ReplyOf As Integer

End Structure
#End Region

Public Class ccUserLog

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

#End Region


#Region "Functions"

    Public Function LoadSystemInfo(ByVal sLoginName As String) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim daSelSystemInfo As New SqlDataAdapter
            Dim dsSelSystemInfo As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSYSINFO"
            sCmd.Parameters.Add(New SqlParameter("@mLoginName", SqlDbType.VarChar)).Value() = sLoginName


            daSelSystemInfo = New SqlDataAdapter(sCmd)
            daSelSystemInfo.Fill(dsSelSystemInfo, "SystemInfo")

            If dsSelSystemInfo.Tables(0).Rows.Count = 0 Then
                mainWinForm.sSystemId = ""
                mainWinForm.sSystemUserName = ""
            Else
                mainWinForm.sSystemId = dsSelSystemInfo.Tables(0).Rows(0).Item("ID")
                mainWinForm.sSystemUserName = dsSelSystemInfo.Tables(0).Rows(0).Item("UserName")
            End If


            
        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function InsertLoginStatus(ByVal oNv As strUserLogStatus) As Boolean
        Try

        
            Dim sCmd As New SqlCommand

            Dim daInsLogin As New SqlDataAdapter
            Dim dsInsLogin As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERT"

            sCmd.Parameters.Add(New SqlParameter("@mSystemId", SqlDbType.VarChar)).Value() = oNv.SystemId
            sCmd.Parameters.Add(New SqlParameter("@mSystemName", SqlDbType.VarChar)).Value() = oNv.SystemName
            sCmd.Parameters.Add(New SqlParameter("@mLoginName", SqlDbType.VarChar)).Value() = oNv.LoginName
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value() = oNv.UserName
            sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value() = oNv.IPAddress
            sCmd.Parameters.Add(New SqlParameter("@mLoginTime", SqlDbType.DateTime)).Value() = oNv.LoginTime
            sCmd.Parameters.Add(New SqlParameter("@mIsCurrentlyLoggedIn", SqlDbType.Bit)).Value() = oNv.IsCurrentlyLoggedIn
            sCmd.Parameters.Add(New SqlParameter("@mLogoutTime", SqlDbType.DateTime)).Value() = oNv.LogoutTime
            sCmd.Parameters.Add(New SqlParameter("@mDuration", SqlDbType.VarChar)).Value() = oNv.Duration

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function UpdateLogoutStatus(ByVal oNv As strUserLogStatus) As Boolean
        Try


            Dim sCmd As New SqlCommand

            Dim daInsLogin As New SqlDataAdapter
            Dim dsInsLogin As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOGOUT"

            sCmd.Parameters.Add(New SqlParameter("@mSystemId", SqlDbType.VarChar)).Value() = oNv.SystemId
            sCmd.Parameters.Add(New SqlParameter("@mSystemName", SqlDbType.VarChar)).Value() = oNv.SystemName
            sCmd.Parameters.Add(New SqlParameter("@mLoginName", SqlDbType.VarChar)).Value() = oNv.LoginName
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value() = oNv.UserName
            sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value() = oNv.IPAddress
            sCmd.Parameters.Add(New SqlParameter("@mLoginTime", SqlDbType.DateTime)).Value() = oNv.LoginTime
            sCmd.Parameters.Add(New SqlParameter("@mIsCurrentlyLoggedIn", SqlDbType.Bit)).Value() = 1 'oNv.IsCurrentlyLoggedIn
            sCmd.Parameters.Add(New SqlParameter("@mLogoutTime", SqlDbType.DateTime)).Value() = oNv.LogoutTime
            sCmd.Parameters.Add(New SqlParameter("@mDuration", SqlDbType.VarChar)).Value() = oNv.Duration

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function InsertMessageHeader(ByVal oNv As strMessageHdr) As Boolean
        Try

            Dim sCmd As New SqlCommand

            Dim daInsMsgHdr As New SqlDataAdapter
            Dim dsInsMsgHdr As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTMSG"
            sCmd.Parameters.Add(New SqlParameter("@mMsgDate", SqlDbType.DateTime)).Value() = oNv.MsgDate
            sCmd.Parameters.Add(New SqlParameter("@mFrom", SqlDbType.VarChar)).Value() = oNv.MsgFrom
            sCmd.Parameters.Add(New SqlParameter("@mFromSystemId", SqlDbType.VarChar)).Value() = oNv.FromSystemId
            sCmd.Parameters.Add(New SqlParameter("@mMessage", SqlDbType.VarChar)).Value() = oNv.Message
            sCmd.Parameters.Add(New SqlParameter("@mAcknowledgeRequired", SqlDbType.Bit)).Value() = oNv.AcknowledgeRequired
           

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function LoadMessageId(ByVal oNv As strMessageHdr) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim daSelSystemInfo As New SqlDataAdapter
            Dim dsSelSystemInfo As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELMSGID"
            sCmd.Parameters.Add(New SqlParameter("@mMsgDate", SqlDbType.DateTime)).Value() = oNv.MsgDate
            sCmd.Parameters.Add(New SqlParameter("@mFrom", SqlDbType.VarChar)).Value() = oNv.MsgFrom
            sCmd.Parameters.Add(New SqlParameter("@mFromSystemId", SqlDbType.VarChar)).Value() = oNv.FromSystemId

            daSelSystemInfo = New SqlDataAdapter(sCmd)
            daSelSystemInfo.Fill(dsSelSystemInfo, "SystemInfo")

            If dsSelSystemInfo.Tables(0).Rows.Count = 0 Then
                mainWinForm.nMessageId = 0
            Else
                mainWinForm.nMessageId = dsSelSystemInfo.Tables(0).Rows(0).Item("PKID")
            End If



        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function InsertMessageDetail(ByVal oNv As strMessageDtl) As Boolean
        Try

            Dim sCmd As New SqlCommand

            Dim daInsMsgHdr As New SqlDataAdapter
            Dim dsInsMsgHdr As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTMSGDTL"
            sCmd.Parameters.Add(New SqlParameter("@mFKMessageHdr", SqlDbType.Int)).Value() = oNv.FKMessageHdr
            sCmd.Parameters.Add(New SqlParameter("@mMessageTo", SqlDbType.VarChar)).Value() = oNv.MessageTo
            sCmd.Parameters.Add(New SqlParameter("@mToSystemId", SqlDbType.VarChar)).Value() = oNv.ToSystemId
            sCmd.Parameters.Add(New SqlParameter("@mAcknowledgeRequired", SqlDbType.Bit)).Value() = oNv.AcknowledgeRequired
            sCmd.Parameters.Add(New SqlParameter("@mSentTime", SqlDbType.DateTime)).Value() = oNv.SentTime
            sCmd.Parameters.Add(New SqlParameter("@mAcknowledgeTime", SqlDbType.DateTime)).Value() = oNv.AcknowledgeTime
            sCmd.Parameters.Add(New SqlParameter("@mIsSeen", SqlDbType.Int)).Value() = oNv.IsSeen
            sCmd.Parameters.Add(New SqlParameter("@mTimeofViewing", SqlDbType.DateTime)).Value() = oNv.TimeofViewing
            sCmd.Parameters.Add(New SqlParameter("@mReplyOf", SqlDbType.Int)).Value() = oNv.ReplyOf

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function UpdateMessageDetail(ByVal oNv As strMessageDtl) As Boolean
        Try

            Dim sCmd As New SqlCommand

            Dim daInsMsgHdr As New SqlDataAdapter
            Dim dsInsMsgHdr As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDATEMSGDTL"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = oNv.PKID
            sCmd.Parameters.Add(New SqlParameter("@mAcknowledgeTime", SqlDbType.DateTime)).Value() = oNv.AcknowledgeTime
            sCmd.Parameters.Add(New SqlParameter("@mIsSeen", SqlDbType.Int)).Value() = oNv.IsSeen
            sCmd.Parameters.Add(New SqlParameter("@mTimeofViewing", SqlDbType.DateTime)).Value() = oNv.TimeofViewing


            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function InsertMessageToTemp(ByVal sMessageFrom As String, ByVal sToSystemId As String) As Boolean
        Try

            Dim sCmd As New SqlCommand

            Dim daInsMessageTo As New SqlDataAdapter
            Dim dsInsMessageTo As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTM2"

            sCmd.Parameters.Add(New SqlParameter("@MessageFrom", SqlDbType.VarChar)).Value() = sMessageFrom
            sCmd.Parameters.Add(New SqlParameter("@mSystemId", SqlDbType.VarChar)).Value() = sToSystemId
            
            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar


            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function DeleteMessageToTemp(ByVal nPKID As Integer) As Boolean
        Try

            Dim sCmd As New SqlCommand

            Dim daInsMessageTo As New SqlDataAdapter
            Dim dsInsMessageTo As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "REMOVEM2"
            sCmd.Parameters.Add(New SqlParameter("@mPKId", SqlDbType.VarChar)).Value() = nPKID


            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar


            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Function

    Public Function LoadActiveUsers(ByVal sMessageFrom As String) As DataTable

        Dim sCmd As New SqlCommand

        Dim daActiveUser As New SqlDataAdapter
        Dim dsActiveUser As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "khli_userlogstatus"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELACTIVEUSER"
        sCmd.Parameters.Add(New SqlParameter("@MessageFrom", SqlDbType.VarChar)).Value = sMessageFrom

        daActiveUser = New SqlDataAdapter(sCmd)
        daActiveUser.Fill(dsActiveUser, "PackedInfo")
        Return dsActiveUser.Tables(0)

        dsActiveUser = Nothing
        sCnn.Close()

    End Function

    Public Function LoadMessageTo(ByVal sMessageFrom As String) As DataTable

        Dim sCmd As New SqlCommand

        Dim daActiveUser As New SqlDataAdapter
        Dim dsActiveUser As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "khli_userlogstatus"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSELECTUSER"
        sCmd.Parameters.Add(New SqlParameter("@MessageFrom", SqlDbType.VarChar)).Value = sMessageFrom

        daActiveUser = New SqlDataAdapter(sCmd)
        daActiveUser.Fill(dsActiveUser, "PackedInfo")
        Return dsActiveUser.Tables(0)

        dsActiveUser = Nothing
        sCnn.Close()

    End Function

    Public Function LoadSentMessage(ByVal sSystemId As String) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim daSelSystemInfo As New SqlDataAdapter
            Dim dsSelSystemInfo As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "khli_userlogstatus"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "MESSAGE"
            sCmd.Parameters.Add(New SqlParameter("@mToSystemId", SqlDbType.VarChar)).Value() = sSystemId

            daSelSystemInfo = New SqlDataAdapter(sCmd)
            daSelSystemInfo.Fill(dsSelSystemInfo, "SystemInfo")

            If dsSelSystemInfo.Tables(0).Rows.Count = 0 Then
                mainWinForm.nMessageReceivedCount = 0
                mainWinForm.nReceivedMessageId = 0
                mainWinForm.nAcknowledgeRequiredforReceivedMessage = 0
                mainWinForm.sReceivedMessage = ""
                mainWinForm.sReceivedMessage = ""
                mainWinForm.sSenderName = ""
                mainWinForm.sSenderLoginId = ""
            Else
                mainWinForm.nMessageReceivedCount = dsSelSystemInfo.Tables(0).Rows.Count
                mainWinForm.nReceivedMessageId = dsSelSystemInfo.Tables(0).Rows(0).Item("PKID")
                mainWinForm.nAcknowledgeRequiredforReceivedMessage = dsSelSystemInfo.Tables(0).Rows(0).Item("AcknowledgeRequired")
                mainWinForm.dReceivedMessageSenTime = dsSelSystemInfo.Tables(0).Rows(0).Item("SentTime")
                mainWinForm.sReceivedMessage = dsSelSystemInfo.Tables(0).Rows(0).Item("Message")
                mainWinForm.sSenderName = dsSelSystemInfo.Tables(0).Rows(0).Item("UserName")
                mainWinForm.sSenderLoginId = dsSelSystemInfo.Tables(0).Rows(0).Item("MessageFrom")
                mainWinForm.sSenderMsgId = dsSelSystemInfo.Tables(0).Rows(0).Item("FKMessageHdr")

                mainWinForm.sSenderSystemId = dsSelSystemInfo.Tables(0).Rows(0).Item("FromSystemId")
                mainWinForm.sSenderSystemName = dsSelSystemInfo.Tables(0).Rows(0).Item("SystemName")
                mainWinForm.sSentOn = Format(dsSelSystemInfo.Tables(0).Rows(0).Item("SentTime"), "dd-MMM-yyyy - hh:mm:ss:tt")


                Dim sCmd1 As New SqlCommand
                Dim daMsgSentTo As New SqlDataAdapter
                Dim dsMsgSentTo As New DataSet

                sCmd1.Connection = sCnn
                sCmd1.CommandText = "khli_userlogstatus"
                sCmd1.CommandType = CommandType.StoredProcedure

                sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "MESSAGESENTTO"
                sCmd1.Parameters.Add(New SqlParameter("@mFKMessageHdr", SqlDbType.VarChar)).Value() = mainWinForm.sSenderMsgId

                daMsgSentTo = New SqlDataAdapter(sCmd1)
                daMsgSentTo.Fill(dsMsgSentTo, "MsgSentTo")

                If dsMsgSentTo.Tables(0).Rows.Count = 1 Then
                    mainWinForm.cbReplyAll.Enabled = False
                Else
                    mainWinForm.cbReplyAll.Enabled = True
                End If
            End If

            

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function LoadPendingAcknowledgement(ByVal sMessageFrom As String) As DataTable

        Dim sCmd As New SqlCommand

        Dim daActiveUser As New SqlDataAdapter
        Dim dsActiveUser As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "khli_userlogstatus"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPENACK"
        sCmd.Parameters.Add(New SqlParameter("@MessageFrom", SqlDbType.VarChar)).Value = sMessageFrom

        daActiveUser = New SqlDataAdapter(sCmd)
        daActiveUser.Fill(dsActiveUser, "PackedInfo")
        Return dsActiveUser.Tables(0)

        dsActiveUser = Nothing
        sCnn.Close()

    End Function

    Public Function LoadHistory(ByVal sMessageFrom As String, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand

        Dim daActiveUser As New SqlDataAdapter
        Dim dsActiveUser As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "khli_userlogstatus"
        sCmd.CommandType = CommandType.StoredProcedure

        If mainWinForm.rbSentHistory.Checked = True Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SENTHIST"
        ElseIf mainWinForm.rbReceivedHistory.Checked = True Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "RECDHIST"
        End If

        sCmd.Parameters.Add(New SqlParameter("@MessageFrom", SqlDbType.VarChar)).Value = sMessageFrom
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value = dToDate

        daActiveUser = New SqlDataAdapter(sCmd)
        daActiveUser.Fill(dsActiveUser, "PackedInfo")
        Return dsActiveUser.Tables(0)

        dsActiveUser = Nothing
        sCnn.Close()

    End Function
#End Region

End Class

