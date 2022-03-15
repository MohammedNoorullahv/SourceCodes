Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

#End Region

Public Class ccAllinOne

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


    Public Function LoadCustomer(ByVal sTypeofOrder As String, ByVal sTypeofDocument As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOne"
        sCmd.CommandType = CommandType.StoredProcedure

        If sTypeofDocument = "Order" Then
            If sTypeofOrder = "All" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdCustA"
            ElseIf sTypeofOrder = "Sales" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdCustS"
            ElseIf sTypeofOrder = "Job" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdCustJ"
            End If
        ElseIf sTypeofDocument = "Invoice" Then
            If sTypeofOrder = "All" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadINVCustA"
            ElseIf sTypeofOrder = "Sales" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadINVCustS"
            ElseIf sTypeofOrder = "Job" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadINVCustJ"
            End If

        End If

        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadBrand(ByVal sTypeofOrder As String, ByVal sTypeofDocument As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelBrand As New SqlDataAdapter
        Dim dsSelBrand As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOne"
        sCmd.CommandType = CommandType.StoredProcedure

        If sTypeofDocument.ToUpper = "ORDER" Then
            If sTypeofOrder = "All" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdBrandA"
            ElseIf sTypeofOrder = "Sales" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdBrandS"
            ElseIf sTypeofOrder = "Job" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdBrandJ"
            End If
        ElseIf sTypeofDocument.ToUpper = "INVOICE" Then
            If sTypeofOrder = "All" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadInvBrandA"
            ElseIf sTypeofOrder = "Sales" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadInvBrandS"
            ElseIf sTypeofOrder = "Job" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadInvBrandJ"
            End If

        End If

        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelBrand.Clear()
        daSelBrand = New SqlDataAdapter(sCmd)
        daSelBrand.Fill(dsSelBrand, "Brand")
        Return dsSelBrand.Tables(0)

        dsSelBrand = Nothing
        sCnn.Close()

    End Function


    Public Function LoadSupplier(ByVal sTypeofOrder As String, ByVal sTypeofDocument As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelSupplier As New SqlDataAdapter
        Dim dsSelSupplier As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOne"
        sCmd.CommandType = CommandType.StoredProcedure

        If sTypeofDocument = "Order" Then
            If sTypeofOrder = "All" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppA"
            ElseIf sTypeofOrder = "Sales" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppS"
            ElseIf sTypeofOrder = "Job" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppJ"
            Else
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppA"
            End If
        ElseIf sTypeofDocument = "Invoice" Then
            If sTypeofOrder = "All" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppA"
            ElseIf sTypeofOrder = "Sales" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppS"
            ElseIf sTypeofOrder = "Job" Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdSuppJ"
            End If
        End If

        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelSupplier.Clear()
        daSelSupplier = New SqlDataAdapter(sCmd)
        daSelSupplier.Fill(dsSelSupplier, "Supplier")
        Return dsSelSupplier.Tables(0)

        dsSelSupplier = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticleofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOne"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedCustomer = " ALL CUSTOMERS" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdALLArt"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrdArt"
        End If

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

    Public Function LoadMaterialsofSupplier() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOne"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedCustomer = " ALL SUPPLIERS" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadPurALLMat"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadPurMat"
        End If

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

    Public Function LoadSampleType(ByVal sOrderType As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOne"
        sCmd.CommandType = CommandType.StoredProcedure

        If frmAllinOnev1.rbOrderSample.Checked = True Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADSAMPTYPE"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADPRODORDTYPE"

        End If

        sCmd.Parameters.Add(New SqlParameter("@mOrderType", SqlDbType.VarChar)).Value() = sOrderType

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function


    Dim nIsSampleOrder As Integer
    Public Function LoadSalesOrders(ByVal sTypeofOrder As String, ByVal sOrderStatus As String, ByVal sArticleCode As String, ByVal sArticleDescription As String, ByVal sIsSampleOrder As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        If frmAllinOnev1.sBrand = "ALL BRANDS" Then
            If mdlSGM.nOption = 101 Then
                If sIsSampleOrder = "All" Then
                    sCmd.CommandText = "sgm_AllinOne101"
                Else

                    If sIsSampleOrder = "Production" Then
                        'sCmd.CommandText = "sgm_AllinOne101SOP"
                        nIsSampleOrder = 0
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne101SOP"
                        Else
                            sCmd.CommandText = "sgm_AllinOne101SOPF"
                        End If
                    Else
                        nIsSampleOrder = 1
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne101SOP"
                        Else
                            sCmd.CommandText = "sgm_AllinOne101SOPF"
                        End If
                    End If
                End If

            ElseIf mdlSGM.nOption = 102 Then
                If sOrderStatus = "CLOSE" Then
                    sCmd.CommandText = "sgm_AllinOne1021"
                Else
                    If sIsSampleOrder = "All" Then
                        sCmd.CommandText = "sgm_AllinOne1031"
                    Else
                        If sIsSampleOrder = "Production" Then
                            'sCmd.CommandText = "sgm_AllinOne1031SOP"
                            nIsSampleOrder = 0
                            If frmAllinOnev1.sSampleOrderType = "000" Then
                                sCmd.CommandText = "sgm_AllinOne1031SOP"
                            Else
                                sCmd.CommandText = "sgm_AllinOne1031SOPF"
                            End If
                        Else
                            nIsSampleOrder = 1
                            If frmAllinOnev1.sSampleOrderType = "000" Then
                                sCmd.CommandText = "sgm_AllinOne1031SOP"
                            Else
                                sCmd.CommandText = "sgm_AllinOne1031SOPF"
                            End If
                        End If
                    End If
                End If

            ElseIf mdlSGM.nOption = 104 Then
                If sIsSampleOrder = "All" Then
                    sCmd.CommandText = "sgm_AllinOne104"
                Else
                    If sIsSampleOrder = "Production" Then
                        'sCmd.CommandText = "sgm_AllinOne104SOP"
                        nIsSampleOrder = 0
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne104SOP"
                        Else
                            sCmd.CommandText = "sgm_AllinOne104SOPF"
                        End If
                    Else
                        nIsSampleOrder = 1
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne104SOP"
                        Else
                            sCmd.CommandText = "sgm_AllinOne104SOPF"
                        End If
                    End If
                End If

            ElseIf mdlSGM.nOption = 105 Then
                sCmd.CommandText = "sgm_AllinOne105"
            ElseIf mdlSGM.nOption = 201 Then
                sCmd.CommandText = "sgm_AllinOne201"
                ''S0SAAAAAS
            Else
                sCmd.CommandText = "sgm_AllinOne"
            End If
        Else
            If mdlSGM.nOption = 101 Then
                If sIsSampleOrder = "All" Then
                    sCmd.CommandText = "sgm_AllinOne101WB"
                Else

                    If sIsSampleOrder = "Production" Then
                        'sCmd.CommandText = "sgm_AllinOne101SOPWB"
                        nIsSampleOrder = 0
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne101SOPWB"
                        Else
                            sCmd.CommandText = "sgm_AllinOne101SOPFWB"
                        End If
                    Else
                        nIsSampleOrder = 1
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne101SOPWB"
                        Else
                            sCmd.CommandText = "sgm_AllinOne101SOPFWB"
                        End If
                    End If
                End If

            ElseIf mdlSGM.nOption = 102 Then
                If sOrderStatus = "CLOSE" Then
                    sCmd.CommandText = "sgm_AllinOne1021WB"
                Else
                    If sIsSampleOrder = "All" Then
                        sCmd.CommandText = "sgm_AllinOne1031WB"
                    Else
                        If sIsSampleOrder = "Production" Then
                            'sCmd.CommandText = "sgm_AllinOne1031SOPWB"
                            nIsSampleOrder = 0
                            If frmAllinOnev1.sSampleOrderType = "000" Then
                                sCmd.CommandText = "sgm_AllinOne1031SOPWB"
                            Else
                                sCmd.CommandText = "sgm_AllinOne1031SOPFWB"
                            End If
                        Else
                            nIsSampleOrder = 1
                            If frmAllinOnev1.sSampleOrderType = "000" Then
                                sCmd.CommandText = "sgm_AllinOne1031SOPWB"
                            Else
                                sCmd.CommandText = "sgm_AllinOne1031SOPFWB"
                            End If
                        End If
                    End If
                End If

            ElseIf mdlSGM.nOption = 104 Then
                If sIsSampleOrder = "All" Then
                    sCmd.CommandText = "sgm_AllinOne104WB"
                Else
                    If sIsSampleOrder = "Production" Then
                        'sCmd.CommandText = "sgm_AllinOne104SOPWB"
                        nIsSampleOrder = 0
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne104SOPWB"
                        Else
                            sCmd.CommandText = "sgm_AllinOne104SOPFWB"
                        End If
                    Else
                        nIsSampleOrder = 1
                        If frmAllinOnev1.sSampleOrderType = "000" Then
                            sCmd.CommandText = "sgm_AllinOne104SOPWB"
                        Else
                            sCmd.CommandText = "sgm_AllinOne104SOPFWB"
                        End If
                    End If
                End If

            ElseIf mdlSGM.nOption = 105 Then
                sCmd.CommandText = "sgm_AllinOne105WB"
            ElseIf mdlSGM.nOption = 201 Then
                sCmd.CommandText = "sgm_AllinOne201WB"
                ''S0SAAAAAS
            Else
                sCmd.CommandText = "sgm_AllinOneWB"
            End If
        End If
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus

        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleCode", SqlDbType.VarChar)).Value() = sArticleCode
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sArticleDescription
        If sIsSampleOrder <> "All" Then
            sCmd.Parameters.Add(New SqlParameter("@mIsSampleOrder", SqlDbType.Bit)).Value() = nIsSampleOrder
            'If sIsSampleOrder = "Sample" And frmAllinOnev1.sSampleOrderType <> "000" Then
            If frmAllinOnev1.sSampleOrderType <> "000" Then
                sCmd.Parameters.Add(New SqlParameter("@mSampleType", SqlDbType.VarChar)).Value() = frmAllinOnev1.sSampleOrderType
            End If
        End If

        If frmAllinOnev1.sBrand <> "ALL BRANDS" Then
            sCmd.Parameters.Add(New SqlParameter("@mBrand", SqlDbType.VarChar)).Value() = frmAllinOnev1.sBrand
        End If
        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function LoadSalesInvoices(ByVal sTypeofOrder As String, ByVal sOrderStatus As String, ByVal sArticleCode As String, ByVal sArticleDescription As String, ByVal sIsSampleOrder As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        If frmAllinOnev1.sBrand = " ALL BRANDS" Then
            If sIsSampleOrder = "All" Then
                sCmd.CommandText = "sgm_AllinOneSaleInvoices"
            Else
                If sIsSampleOrder = "Production" Then

                    nIsSampleOrder = 0
                    If frmAllinOnev1.sSampleOrderType = "000" Then
                        sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOP"
                    Else
                        sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOPF"
                    End If
                Else
                    nIsSampleOrder = 1
                    If frmAllinOnev1.sSampleOrderType = "000" Then
                        sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOP"
                    Else
                        sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOPF"
                    End If
                End If
            End If
        Else
            If sIsSampleOrder = "All" Then
                sCmd.CommandText = "sgm_AllinOneSaleInvoicesWB"
            Else
                If sIsSampleOrder = "Production" Then
                    sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOPWB"
                    nIsSampleOrder = 0
                Else
                    nIsSampleOrder = 1
                    If frmAllinOnev1.sSampleOrderType = "000" Then
                        sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOPWB"
                    Else
                        sCmd.CommandText = "sgm_AllinOneSaleInvoicesSOPFWB"
                    End If
                End If
            End If
            sCmd.Parameters.Add(New SqlParameter("@mBrand", SqlDbType.VarChar)).Value() = frmAllinOnev1.sBrand
        End If
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus

        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleCode", SqlDbType.VarChar)).Value() = sArticleCode
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sArticleDescription
        sCmd.Parameters.Add(New SqlParameter("@mIsSample", SqlDbType.Bit)).Value() = nIsSampleOrder
        sCmd.Parameters.Add(New SqlParameter("@mSampleOrderType", SqlDbType.VarChar)).Value() = frmAllinOnev1.sSampleOrderType


        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function LoadPurchaseOrders(ByVal sTypeofOrder As String, ByVal sOrderStatus As String, ByVal sArticleCode As String, ByVal sArticleDescription As String, ByVal sMaterialType As String, ByVal sMaterailSubType As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        If frmAllinOnev1.chkbxNoMRP.Checked = True Then
            sCmd.CommandText = "sgm_AllinOnePurchaseOrdersNOMRP"
        Else
            sCmd.CommandText = "sgm_AllinOnePurchaseOrders"
        End If

        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus

        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleCode", SqlDbType.VarChar)).Value() = sArticleCode
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sArticleDescription

        sCmd.Parameters.Add(New SqlParameter("@mMaterialType", SqlDbType.VarChar)).Value() = sMaterialType
        sCmd.Parameters.Add(New SqlParameter("@mMaterialSubType", SqlDbType.VarChar)).Value() = sMaterailSubType

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function LoadPurchaseInvoices(ByVal sTypeofOrder As String, ByVal sOrderStatus As String, ByVal sArticleCode As String, ByVal sArticleDescription As String, ByVal sMaterialType As String, ByVal sMaterailSubType As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_AllinOnePurchaseInvoices"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus

        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleCode", SqlDbType.VarChar)).Value() = sArticleCode
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sArticleDescription

        sCmd.Parameters.Add(New SqlParameter("@mMaterialType", SqlDbType.VarChar)).Value() = sMaterialType
        sCmd.Parameters.Add(New SqlParameter("@mMaterialSubType", SqlDbType.VarChar)).Value() = sMaterailSubType

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function


    Public Function DCNoteAgainstSales(ByVal sTypeofOrder As String, ByVal sOrderStatus As String, ByVal sArticleCode As String, ByVal sArticleDescription As String, ByVal sIsSampleOrder As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        If mdlSGM.nOption = 101 Then
            If sIsSampleOrder = "All" Then
                sCmd.CommandText = "sgm_AllinOneDCNote"
            Else

                If sIsSampleOrder = "Production" Then
                    sCmd.CommandText = "sgm_AllinOne101SOP"
                    nIsSampleOrder = 0
                Else
                    nIsSampleOrder = 1
                    If frmAllinOnev1.sSampleOrderType = "000" Then
                        sCmd.CommandText = "sgm_AllinOne101SOP"
                    Else
                        sCmd.CommandText = "sgm_AllinOne101SOPF"
                    End If
                End If
            End If

        ElseIf mdlSGM.nOption = 102 Then
            If sOrderStatus = "CLOSE" Then
                sCmd.CommandText = "sgm_AllinOne1021"
            Else
                sCmd.CommandText = "sgm_AllinOne1031"
            End If

        ElseIf mdlSGM.nOption = 104 Then
            If sIsSampleOrder = "All" Then
                sCmd.CommandText = "sgm_AllinOneDCNote104"
            Else
                If sIsSampleOrder = "Production" Then
                    sCmd.CommandText = "sgm_AllinOne104SOP"
                    nIsSampleOrder = 0
                Else
                    nIsSampleOrder = 1
                    If frmAllinOnev1.sSampleOrderType = "000" Then
                        sCmd.CommandText = "sgm_AllinOne104SOP"
                    Else
                        sCmd.CommandText = "sgm_AllinOne104SOPF"
                    End If
                End If
            End If

        ElseIf mdlSGM.nOption = 105 Then
            sCmd.CommandText = "sgm_AllinOne105"
        ElseIf mdlSGM.nOption = 201 Then
            sCmd.CommandText = "sgm_AllinOne201"
            ''S0SAAAAAS
        Else
            sCmd.CommandText = "sgm_AllinOne"
        End If

        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus

        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleCode", SqlDbType.VarChar)).Value() = sArticleCode
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sArticleDescription
        If sIsSampleOrder <> "All" Then
            sCmd.Parameters.Add(New SqlParameter("@mIsSampleOrder", SqlDbType.Bit)).Value() = nIsSampleOrder
            If sIsSampleOrder = "Sample" And frmAllinOnev1.sSampleOrderType <> "000" Then
                sCmd.Parameters.Add(New SqlParameter("@mSampleType", SqlDbType.VarChar)).Value() = frmAllinOnev1.sSampleOrderType
            End If
        End If

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function DCNoteAgainstPurchase(ByVal sTypeofOrder As String, ByVal sOrderStatus As String, ByVal sArticleCode As String, ByVal sArticleDescription As String, ByVal sIsSampleOrder As String, ByVal sMaterialType As String, ByVal sMaterailSubType As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        If frmAllinOnev1.chkbxNoMRP.Checked = True Then
            sCmd.CommandText = "sgm_AllinOnePurchaseOrdersNOMRP"
        Else
            sCmd.CommandText = "sgm_AllinOneDCNotePurchase"
        End If


        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus

        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleCode", SqlDbType.VarChar)).Value() = sArticleCode
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sArticleDescription

        sCmd.Parameters.Add(New SqlParameter("@mMaterialType", SqlDbType.VarChar)).Value() = sMaterialType
        sCmd.Parameters.Add(New SqlParameter("@mMaterialSubType", SqlDbType.VarChar)).Value() = sMaterailSubType

        If sIsSampleOrder <> "All" Then
            sCmd.Parameters.Add(New SqlParameter("@mIsSampleOrder", SqlDbType.Bit)).Value() = nIsSampleOrder
            If sIsSampleOrder = "Sample" And frmAllinOnev1.sSampleOrderType <> "000" Then
                sCmd.Parameters.Add(New SqlParameter("@mSampleType", SqlDbType.VarChar)).Value() = frmAllinOnev1.sSampleOrderType
            End If
        End If

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
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