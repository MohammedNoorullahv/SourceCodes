Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

#End Region

Public Class ccProductStock

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup
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

    Public Function InsertProductStock() As Boolean

        Dim sCmd As New SqlCommand

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStock"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSSTOCK"
        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
        End If
        sCnn.Close()
        Return False
    End Function

    Public Function InsertProductStockInHouse() As Boolean

        Dim sCmd As New SqlCommand

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStockIH"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSSTOCK"
        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
        End If
        sCnn.Close()
        Return False
    End Function

    Public Function UpdateStockDaysCount() As Boolean

        Dim sCmd As New SqlCommand
        Dim daSelProductStock As New SqlDataAdapter
        Dim dsSelProductStock As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStock"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELTODAYSTOCK"

        daSelProductStock = New SqlDataAdapter(sCmd)
        daSelProductStock.Fill(dsSelProductStock)

        If dsSelProductStock.Tables(0).Rows.Count > 0 Then
            
            Dim i As Integer = 0
            For i = 0 To dsSelProductStock.Tables(0).Rows.Count - 1
                Dim sJobcardNo As String
                Dim nCurrentQty, nLastQty, nPKID, nDayCount As Integer

                sJobcardNo = dsSelProductStock.Tables(0).Rows(i).Item("JobcardNo").ToString()
                nLastQty = Val(dsSelProductStock.Tables(0).Rows(i).Item("Quantity"))
                nPKID = Val(dsSelProductStock.Tables(0).Rows(i).Item("PKID"))
                nDayCount = Val(dsSelProductStock.Tables(0).Rows(i).Item("DayCount"))

                Dim sCmd1 As New SqlCommand
                Dim daSelJCQty As New SqlDataAdapter
                Dim dsSelJCQty As New DataSet

                sCmd1.Connection = sCnn
                sCmd1.CommandText = "sgm_ProductStock"
                sCmd1.CommandType = CommandType.StoredProcedure

                sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELJCSTOCK"
                sCmd1.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo

                daSelJCQty = New SqlDataAdapter(sCmd1)
                daSelJCQty.Fill(dsSelJCQty)

                If dsSelJCQty.Tables(0).Rows.Count > 0 Then
                    nCurrentQty = Val(dsSelJCQty.Tables(0).Rows(0).Item("Quantity"))
                    If nLastQty = nCurrentQty Then
                        nDayCount = nDayCount + 1

                        Dim sCmd2 As New SqlCommand
                        Dim daUpdJCQty As New SqlDataAdapter
                        Dim dsUpdJCQty As New DataSet

                        sCmd2.Connection = sCnn
                        sCmd2.CommandText = "sgm_ProductStock"
                        sCmd2.CommandType = CommandType.StoredProcedure

                        sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDJCSTOCK"
                        sCmd2.Parameters.Add(New SqlParameter("@mDaycount", SqlDbType.Int)).Value() = nDayCount
                        sCmd2.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = nPKID

                        daUpdJCQty = New SqlDataAdapter(sCmd2)
                        daUpdJCQty.Fill(dsUpdJCQty)
                        dsUpdJCQty.AcceptChanges()

                    End If
                End If
            Next
        End If

        Return True
    End Function

    Public Function LoadCustomer(ByVal dStockDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStock"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADCUSTOMER"
        sCmd.Parameters.Add(New SqlParameter("@mPSDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        
        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticleMould(ByVal dStockDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelMould As New SqlDataAdapter
        Dim dsSelMould As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStock"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMOULD"
        sCmd.Parameters.Add(New SqlParameter("@mPSDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate

        dsSelMould.Clear()
        daSelMould = New SqlDataAdapter(sCmd)
        daSelMould.Fill(dsSelMould, "Mould")
        Return dsSelMould.Tables(0)

        dsSelMould = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticle(ByVal dStockDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelArticle As New SqlDataAdapter
        Dim dsSelArticle As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStock"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADARTICLE"
        sCmd.Parameters.Add(New SqlParameter("@mPSDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate

        dsSelArticle.Clear()
        daSelArticle = New SqlDataAdapter(sCmd)
        daSelArticle.Fill(dsSelArticle, "Article")
        Return dsSelArticle.Tables(0)

        dsSelArticle = Nothing
        sCnn.Close()

    End Function

    Public Function LoadProductStock(ByVal dPSDate As Date, ByVal sDepartment As String, ByVal sDescription As String, ByVal sArticleMould As String, ByVal sBuyerName As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_ProductStock"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption
        sCmd.Parameters.Add(New SqlParameter("@mPSDate", SqlDbType.DateTime)).Value() = dPSDate
        sCmd.Parameters.Add(New SqlParameter("@mDepartment", SqlDbType.VarChar)).Value() = sDepartment
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sDescription
        sCmd.Parameters.Add(New SqlParameter("@mArticleMould", SqlDbType.VarChar)).Value() = sArticleMould
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = sBuyerName

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

#End Region

End Class
