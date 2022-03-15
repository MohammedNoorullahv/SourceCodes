Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strDailyTransaction

    Dim PKID As Integer
    Dim TranType As String
    Dim TranDate As Date
    Dim FKAccountHead As Integer
    Dim PaidTo As String
    Dim OpeningBalance As Decimal
    Dim Receipt As Decimal
    Dim Payment As Decimal
    Dim ClosingBalance As Decimal
    Dim Remarks As String
    Dim IsVerified As Integer
    Dim CreatedBy As Integer
    Dim CreatedDate As String
    Dim ModifiedBy As Integer
    Dim ModifiedDate As String
    Dim DeletedBy As Integer
    Dim DeletedDate As String

End Structure

#End Region

Public Class ccArticleMaster

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer
#End Region

#Region "Properties"

    ReadOnly Property ErrorCode() As String
        Get
            Return sErrCode
        End Get
    End Property

    ReadOnly Property ErrorMessage() As String
        Get
            Return sErrMsg
        End Get
    End Property

#End Region

#Region "Functions"


    Public Function LoadArticles() As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadArticles As New SqlDataAdapter
        Dim dsLoadArticles As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ArticleDetail"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectOption = "All Articles" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALLARTICLES"
        ElseIf mdlSGM.sSelectOption = "Customers Article - Article Wise" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELCUSTWART"
            sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
            sCmd.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        ElseIf mdlSGM.sSelectOption = "Customers Article - Codification Wise" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELCUSTWCODE"
            sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
            sCmd.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCodification
        ElseIf mdlSGM.sSelectOption = "Customers Article" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELARTWCUST"
            sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        End If

        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        daLoadArticles = New SqlDataAdapter(sCmd)
        daLoadArticles.Fill(dsLoadArticles, "Art")
        Return dsLoadArticles.Tables(0)

        dsLoadArticles = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ArticleDetail"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCust"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticleofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ArticleDetail"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadArt"
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCodificationofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ArticleDetail"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCode"
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

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

#End Region

End Class
