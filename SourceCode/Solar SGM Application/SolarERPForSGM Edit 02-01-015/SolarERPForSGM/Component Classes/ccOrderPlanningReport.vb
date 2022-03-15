Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strOrderPlanning

    Dim FromDate As Date
    Dim ToDate As Date
    Dim Customer As String
    Dim ArticleMould As String
    Dim ArticleDescription As String
    Dim ProductionStatus As String
    Dim ShipmentStatus As String
    Dim Negotiable As Integer
    Dim nYear As Integer
    Dim FromWeek As Integer
    Dim ToWeek As Integer
    Dim ProductType As String
    Dim SortingType As String
    Dim ProductTypeMain As String
End Structure

#End Region

Public Class ccOrderPlanningReport

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

    Public Function LoadCustomer(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_OrderPlanningReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADCUSTOMER"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticles(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_OrderPlanningReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADARTICLES"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadYear(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_OrderPlanningReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADYEAR"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadProductTypeMain(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_OrderPlanningReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADPRODUCTTYPEMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadProductType(ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sProductType As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_OrderPlanningReport"
        sCmd.CommandType = CommandType.StoredProcedure


        If sProductType = "" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADPRODUCTTYPE"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADPRODUCTTYPEF"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mProductType", SqlDbType.VarChar)).Value() = sProductType

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadSalesOrderDetails(ByVal oNV As strOrderPlanning) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn

        If oNV.ProductTypeMain = "ALL PRODUCT TYPE" Then
            If mdlSGM.sSortType = "0" Then
                If mdlSGM.sReferencedFrom = "Details" Then
                    sCmd.CommandText = "proc_OrderPlanningReport"
                ElseIf mdlSGM.sReferencedFrom = "Summary" Then
                    sCmd.CommandText = "proc_OrderPlanningReportWKSUMMARY"
                End If
            Else
                If mdlSGM.sReferencedFrom = "Details" Then
                    sCmd.CommandText = "proc_OrderPlanningReportV1"
                ElseIf mdlSGM.sReferencedFrom = "Summary" Then
                    sCmd.CommandText = "proc_OrderPlanningReportWKSUMMARYV1"
                End If
            End If
        Else
            If mdlSGM.sSortType = "0" Then
                If mdlSGM.sReferencedFrom = "Details" Then
                    sCmd.CommandText = "proc_OrderPlanningReportWPTYPE"
                ElseIf mdlSGM.sReferencedFrom = "Summary" Then
                    sCmd.CommandText = "proc_OrderPlanningReportWKSUMMARYWPTYPE"
                End If
            Else
                If mdlSGM.sReferencedFrom = "Details" Then
                    sCmd.CommandText = "proc_OrderPlanningReportV1WPTYPE"
                ElseIf mdlSGM.sReferencedFrom = "Summary" Then
                    sCmd.CommandText = "proc_OrderPlanningReportWKSUMMARYV1WPTYPE"
                End If
            End If
        End If
        
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mCodificationNew", SqlDbType.VarChar)).Value() = oNV.ProductType
        sCmd.Parameters.Add(New SqlParameter("@mWeekFrom", SqlDbType.Int)).Value() = Val(oNV.FromWeek)
        sCmd.Parameters.Add(New SqlParameter("@mWeekTo", SqlDbType.Int)).Value() = Val(oNV.ToWeek)
        sCmd.Parameters.Add(New SqlParameter("@mYear", SqlDbType.Int)).Value() = oNV.nYear
        sCmd.Parameters.Add(New SqlParameter("@mIsEDDNegotiable", SqlDbType.Bit)).Value() = oNV.Negotiable
        sCmd.Parameters.Add(New SqlParameter("@mShipmentStatus", SqlDbType.VarChar)).Value() = oNV.ShipmentStatus
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = oNV.ProductionStatus
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = oNV.ArticleDescription
        sCmd.Parameters.Add(New SqlParameter("@mArticleMould", SqlDbType.VarChar)).Value() = oNV.ArticleMould
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = oNV.Customer
        sCmd.Parameters.Add(New SqlParameter("@mProductTypeMain", SqlDbType.VarChar)).Value() = oNV.ProductTypeMain

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function


#End Region
End Class
