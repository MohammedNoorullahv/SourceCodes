Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strSalesAnalysis

    Dim FromDate As Date
    Dim ToDate As Date
    Dim Customer As String
    Dim GranuleType As String
    Dim ArticleMould As String
    Dim ArticleCode As String
    Dim ArticleDescription As String
    Dim ProductType As String
    Dim SampleType As String
    Dim OrderType As String
    Dim IsSampleOrder As Integer
    Dim ProductTypeMain As String

End Structure

#End Region

Public Class ccSalesAnalysisReport

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
        sCmd.CommandText = "proc_SaleAnalysisReport"
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

    Public Function LoadGranuleType(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SaleAnalysisReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADGRANULETYPE"
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

    Public Function LoadArticleCode(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SaleAnalysisReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADARTICLECODE"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadSampleType() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SaleAnalysisReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADSAMPTYPE"

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadProductType(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SaleAnalysisReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADPRODUCTTYPE"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadInvoiceDetails(ByVal oNV As strSalesAnalysis) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn


        If mdlSGM.sBrand.ToUpper = " ALL BRANDS" Then
            If oNV.ProductTypeMain = "ALL PRODUCT TYPE" Then

                If mdlSGM.sTypeofDocument = "INVOICE" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReport"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMould"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCAM"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMAC"
                    End If
                ElseIf mdlSGM.sTypeofDocument = "ORDER" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORD"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMould"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDCAM"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMAC"
                    End If
                Else
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCN"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNMould"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNCAM"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNMAC"
                    End If
                End If

            Else

                If mdlSGM.sTypeofDocument = "INVOICE" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMouldWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCAMWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMACWPTYPE"
                    End If
                ElseIf mdlSGM.sTypeofDocument = "ORDER" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMouldWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDCAMWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMACWPTYPE"
                    End If
                Else
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNMouldWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNCAMWPTYPE"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNMACWPTYPE"
                    End If
                End If
            End If
        Else
            If oNV.ProductTypeMain = "ALL PRODUCT TYPE" Then

                If mdlSGM.sTypeofDocument = "INVOICE" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMouldWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCAMWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMACWB"
                    End If
                ElseIf mdlSGM.sTypeofDocument = "ORDER" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMouldWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDCAMWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDCAMWB"
                    End If
                Else
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNMouldWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        ''sCmd.CommandText = "proc_SaleAnalysisReportCNCAMWB" -- Coding to be Done
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        ''sCmd.CommandText = "proc_SaleAnalysisReportCNMACWB" -- Coding to be Done
                    End If
                End If

            Else

                If mdlSGM.sTypeofDocument = "INVOICE" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMouldWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCAMWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportMACWPTYPEWB"
                    End If
                ElseIf mdlSGM.sTypeofDocument = "ORDER" Then
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMouldWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDCAMWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportORDMACWPTYPEWB"
                    End If
                Else
                    If mdlSGM.sReferencedFrom = "Customer" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Moulds" Then
                        sCmd.CommandText = "proc_SaleAnalysisReportCNMouldWPTYPEWB"
                    ElseIf mdlSGM.sReferencedFrom = "Customer / Moulds" Then
                        ''sCmd.CommandText = "proc_SaleAnalysisReportCNCAMWPTYPEWB" -- Coding to be Done
                    ElseIf mdlSGM.sReferencedFrom = "Moulds / Customer" Then
                        ''sCmd.CommandText = "proc_SaleAnalysisReportCNMACWPTYPEWB" -- Coding to be Done    
                    End If
                End If
            End If
            sCmd.Parameters.Add(New SqlParameter("@mBrand", SqlDbType.VarChar)).Value() = mdlSGM.sBrand
        End If
        'sCmd.CommandText = "proc_SaleAnalysisReport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mProductType", SqlDbType.VarChar)).Value() = oNV.ProductType
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = oNV.ArticleDescription
        sCmd.Parameters.Add(New SqlParameter("@mArticleMould", SqlDbType.VarChar)).Value() = oNV.ArticleMould
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = oNV.Customer
        sCmd.Parameters.Add(New SqlParameter("@mIsSampleOrder", SqlDbType.VarChar)).Value() = oNV.IsSampleOrder
        sCmd.Parameters.Add(New SqlParameter("@mSampleType", SqlDbType.VarChar)).Value() = oNV.SampleType
        sCmd.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.ArticleCode
        sCmd.Parameters.Add(New SqlParameter("@GranuleType", SqlDbType.VarChar)).Value() = oNV.GranuleType
        sCmd.Parameters.Add(New SqlParameter("@mProductTypeMain", SqlDbType.VarChar)).Value() = oNV.ProductTypeMain



        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function


#End Region
End Class
