Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strSolarArticleMaster4SGM4Print

    Dim PKID As Integer
    Dim Client As String
    Dim Code As String
    Dim Gender As String
    Dim SoleType As String
    Dim SoleName As String
    Dim Colour As String
    Dim Granules As String
    Dim NettWt As Decimal
    Dim LeatherSQM As String
    Dim SQMConsumption As Decimal
    Dim SQMDeclaredConsumption As Decimal
    Dim LeatherKGS As String
    Dim KGSConsumption As Decimal
    Dim KGSDeclaredConsumption As Decimal
    Dim DeclaredWt As Decimal
    Dim Codification As String
    Dim CodificationNew As String

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

    Public Function DelArticle4Print() As Boolean
        ''aasdfas()
        Dim sCmd1 As New SqlCommand

        Dim daDelOutstanding As New SqlDataAdapter
        Dim dsDelOutstanding As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "sgm_ArticleDetail"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "DELART"

        daDelOutstanding = New SqlDataAdapter(sCmd1)
        daDelOutstanding.Fill(dsDelOutstanding, "Date")
        dsDelOutstanding.AcceptChanges()

    End Function

    Public Function InsArticle4Print(ByVal oNV As strSolarArticleMaster4SGM4Print) As DataTable
        ''aasdfas()
        Dim sCmd2 As New SqlCommand

        Dim daInsOutstanding As New SqlDataAdapter
        Dim dsInsOutstanding As New DataSet

        sCmd2.Connection = sCnn
        sCmd2.CommandText = "sgm_ArticleDetail"
        sCmd2.CommandType = CommandType.StoredProcedure

        sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSART"

        sCmd2.Parameters.Add(New SqlParameter("@mClient", SqlDbType.VarChar)).Value() = oNV.Client
        sCmd2.Parameters.Add(New SqlParameter("@mCode", SqlDbType.VarChar)).Value() = oNV.Code
        sCmd2.Parameters.Add(New SqlParameter("@mGender", SqlDbType.VarChar)).Value() = oNV.Gender
        sCmd2.Parameters.Add(New SqlParameter("@mSoleType", SqlDbType.VarChar)).Value() = oNV.SoleType
        sCmd2.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = oNV.SoleName
        sCmd2.Parameters.Add(New SqlParameter("@mColour", SqlDbType.VarChar)).Value() = oNV.Colour
        sCmd2.Parameters.Add(New SqlParameter("@mGranules", SqlDbType.VarChar)).Value() = oNV.Granules
        sCmd2.Parameters.Add(New SqlParameter("@mNettWt", SqlDbType.Decimal)).Value() = oNV.NettWt
        sCmd2.Parameters.Add(New SqlParameter("@mLeatherSQM", SqlDbType.VarChar)).Value() = oNV.LeatherSQM
        sCmd2.Parameters.Add(New SqlParameter("@mSQMConsumption", SqlDbType.Decimal)).Value() = oNV.SQMConsumption
        sCmd2.Parameters.Add(New SqlParameter("@mSQMDeclaredConsumption", SqlDbType.Decimal)).Value() = oNV.SQMDeclaredConsumption
        sCmd2.Parameters.Add(New SqlParameter("@mLeatherKGS", SqlDbType.VarChar)).Value() = oNV.LeatherKGS
        sCmd2.Parameters.Add(New SqlParameter("@mKGSConsumption", SqlDbType.Decimal)).Value() = oNV.KGSConsumption
        sCmd2.Parameters.Add(New SqlParameter("@mKGSDeclaredConsumption", SqlDbType.Decimal)).Value() = oNV.KGSDeclaredConsumption
        sCmd2.Parameters.Add(New SqlParameter("@mDeclaredWt", SqlDbType.Decimal)).Value() = oNV.DeclaredWt
        sCmd2.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = oNV.Codification
        sCmd2.Parameters.Add(New SqlParameter("@mCodificationNew", SqlDbType.VarChar)).Value() = oNV.CodificationNew



        sCnn.Open()

        Dim sRes As String = sCmd2.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            setError(Val(sRes))
        End If
        sCnn.Close()

    End Function

    Public Function LoadMaterials() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ArticleDetail"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadMaterial"
        

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
