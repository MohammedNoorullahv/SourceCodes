Option Explicit On
Imports System.Data.SqlClient
Imports System.Data

Public Structure StrUserAuthentication
    Public nPKID As Integer
    Public sUserName As String
    Public sIPAddress As String
    Public sSystemName As String
    Public sServer As String
    Public sLoginTime As String
    Public sLogoutTime As String
    Public sIsActive As Integer
    Public sReason As String
    Public sVersion As String
End Structure

Public Structure StrEmployee

    Dim FKUser As Long
    Dim FKFirm As Long
    Dim FirmName As String
    Dim UnitType As String
    Dim userName As String
    Dim Designation As String
    Dim LoginName As String
    Dim Password As String
    Dim UserType As String

End Structure

Public Class ccOptimizerComponent
    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Public Function CheckUserAlredyLogin(ByVal oNv As StrUserAuthentication) As Boolean


        Dim sCmd As New SqlCommand
        Dim SdaInsUserAuthen As New SqlDataAdapter
        Dim dsInsUserAuthen As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKUSERALRDYLOG"
        sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNv.sUserName
        'sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive

        SdaInsUserAuthen = New SqlDataAdapter(sCmd)
        SdaInsUserAuthen.Fill(dsInsUserAuthen)

        If dsInsUserAuthen.Tables(0).Rows.Count = 0 Then
            mdlSGM.sLoggedin = "N"
        Else
            mdlSGM.nUAId = dsInsUserAuthen.Tables(0).Rows(0).Item("PKID")
            mdlSGM.sLoggedin = "Y"
            mdlSGM.sIPAddress = dsInsUserAuthen.Tables(0).Rows(0).Item("IPAddress")
        End If



    End Function

    Public Function CheckIPAddress(ByVal oNv As StrUserAuthentication) As Boolean

        Dim sCmd As New SqlCommand
        Dim SdaInsUserAuthen As New SqlDataAdapter
        Dim dsInsUserAuthen As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKIPADDRLOG"
        ' sCmd.Parameters.Add(New SqlParameter("@mFKUserName", SqlDbType.Int)).Value = oNv.nFKUserName
        sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.Char)).Value = oNv.sIPAddress
        ' sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive

        SdaInsUserAuthen = New SqlDataAdapter(sCmd)
        SdaInsUserAuthen.Fill(dsInsUserAuthen)

        If dsInsUserAuthen.Tables(0).Rows.Count = 0 Then
            mdlSGM.sLoggedin = "N"
        Else
            mdlSGM.nUAId = dsInsUserAuthen.Tables(0).Rows(0).Item("PKID")
            mdlSGM.sLoggedin = "Y"
            mdlSGM.sLoggedUser = dsInsUserAuthen.Tables(0).Rows(0).Item("UserName")
            mdlSGM.sIPAddress = dsInsUserAuthen.Tables(0).Rows(0).Item("IPAddress")
        End If


    End Function

    Public Function CheckForSameIPAddress(ByVal oNv As StrUserAuthentication) As Boolean

        Dim sCmd As New SqlCommand
        Dim SdaInsUserAuthen As New SqlDataAdapter
        Dim dsInsUserAuthen As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKSAMEIP"
        'sCmd.Parameters.Add(New SqlParameter("@mFKUserName", SqlDbType.Int)).Value = oNv.nFKUserName
        sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.Char)).Value = oNv.sIPAddress
        ' sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive

        SdaInsUserAuthen = New SqlDataAdapter(sCmd)
        SdaInsUserAuthen.Fill(dsInsUserAuthen)

        If dsInsUserAuthen.Tables(0).Rows.Count = 0 Then
            mdlSGM.sLoggedin = "N"
        Else
            mdlSGM.sLoggedin = "Y"
            mdlSGM.nUAId = dsInsUserAuthen.Tables(0).Rows(0).Item("PKID")
            mdlSGM.sLoggedUser = dsInsUserAuthen.Tables(0).Rows(0).Item("UserName")
            mdlSGM.sIPAddress = dsInsUserAuthen.Tables(0).Rows(0).Item("IPAddress")
        End If


    End Function

    Public Function InsertUserAuthentication(ByVal oNv As StrUserAuthentication) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsUserAuthen As New DataSet
            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTUSERAUTH"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value = oNv.nPKID
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNv.sUserName
            sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.Char)).Value = oNv.sIPAddress
            sCmd.Parameters.Add(New SqlParameter("@mSystemName", SqlDbType.Char)).Value = oNv.sSystemName
            sCmd.Parameters.Add(New SqlParameter("@mServer", SqlDbType.Char)).Value = oNv.sServer
            sCmd.Parameters.Add(New SqlParameter("@mLoginTime", SqlDbType.Char)).Value = oNv.sLoginTime
            sCmd.Parameters.Add(New SqlParameter("@mLogoutTime", SqlDbType.Char)).Value = oNv.sLogoutTime
            sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive
            sCmd.Parameters.Add(New SqlParameter("@mVersion", SqlDbType.VarChar)).Value = oNv.sVersion

            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function


    Public Function SelectEmployee(ByVal sUserName As String) As StrEmployee

        'Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim sDaEmp As New SqlDataAdapter
        Dim dsEmp As New DataSet

        Dim myRec As StrEmployee

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Login"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALL"
        sCmd.Parameters.Add(New SqlParameter("@mLoginName", SqlDbType.VarChar)).Value() = sUserName

        sDaEmp = New SqlDataAdapter(sCmd)
        sDaEmp.Fill(dsEmp, "Employee")

        If dsEmp.Tables("Employee").Rows.Count > 0 Then

            myRec.userName = dsEmp.Tables("Employee").Rows(0).Item("UserId") & ""
            myRec.Password = dsEmp.Tables("Employee").Rows(0).Item("sPassword").ToString
            mdlSGM.sUserPassword = dsEmp.Tables("Employee").Rows(0).Item("sPassword").ToString

            mdlSGM.nUnitId = 0 'dsEmp.Tables("Employee").Rows(0).Item("nFKFirm")
            mdlSGM.sUserName = myRec.userName '1 'dsEmp.Tables("Employee").Rows(0).Item("nUserId")
            mdlSGM.sUnitName = "" 'dsEmp.Tables("Employee").Rows(0).Item("sFirmName")
            mdlSGM.sUnitType = "" 'dsEmp.Tables("Employee").Rows(0).Item("sUnitType")
            'mdlSGM.sUserName = "" 'dsEmp.Tables("Employee").Rows(0).Item("sUserName")
            mdlSGM.sUserDesignation = "" 'dsEmp.Tables("Employee").Rows(0).Item("sDesignation")
            mdlSGM.sUserType = "" 'dsEmp.Tables("Employee").Rows(0).Item("sUserType")
            'mdlTFErp.nEnteringForm = -1
            'mdlKHLIERP.VerifyAccess()
            'mdlKHLIERP.LoadYear()

            'frmMdiKHLIErp.Show()
            'frmMdiKHLIErp.BringToFront()
            'sErrMsg = "No such order record."
            Return myRec
        Else
            'sErrMsg = "No such order record."
            mdlSGM.sUserName = 0
            Return Nothing
            'mdlTFErp.nUserName = 0
        End If

        'dsEmp.Clear()
        dsEmp = Nothing
        sCnn.Close()

    End Function

    Public Function UpdateUserAuthentication(ByVal oNv As StrUserAuthentication) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsUserAuthen As New DataSet
            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEUSERAUTH"
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNv.sUserName
            sCmd.Parameters.Add(New SqlParameter("@mLogoutTime", SqlDbType.Char)).Value = oNv.sLogoutTime
            sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive





            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function


    Private Sub setError(ByVal nNo As Integer)
        If nNo = 1 Then
            sErrCode = "NOREQPARAM"
            sErrMsg = "Required parameters value not specified"
        ElseIf nNo = 2 Then
            sErrCode = "INVALIDPARAM"
            sErrMsg = "Invalid parameters value specified"
        ElseIf nNo = 3 Then
            sErrCode = "SQLERROR"
            sErrMsg = "Internal SQL Server Error"
        ElseIf nNo = 4 Then
            sErrCode = "NOITEM"
            sErrMsg = "No Such Contact"
        ElseIf nNo = 5 Then
            sErrCode = "ACTIVEITEM"
            sErrMsg = "User is active and cannot be deleted"
        End If
    End Sub

End Class
