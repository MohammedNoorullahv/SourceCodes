Option Explicit On
Imports System.Data.SqlClient


Public Class ccSalesOrderForecastMapping

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

    Dim sDemID As String
    Dim sDemShipper As String
    Dim sDemOrderNo As String
    Dim sDemSalesOrderNo As String
    Dim sDemBuyerCode As String
    Dim sDemBuyerGroupCode As String
    Dim sDemBuyerOrderNo As String
    Dim sDemArticleGroup As String
    Dim sDemArticle As String
    Dim sDemLeatherCode As String
    Dim sDemLeatherName As String
    Dim sDemColorCode As String
    Dim sDemColorName As String
    Dim sDemMaterialCode As String
    Dim sDemDescription As String
    Dim sDemMaterialTypeCode As String
    Dim sDemMaterialCategory As String
    Dim sDemMaterialColor As String
    Dim sDemMaterialSize As String
    Dim sDemMaterialSizeGroup As String
    Dim sDemUnit As String
    Dim nDemConsumptionQuantity As Decimal
    Dim nDemConsumptionPcs As Decimal
    Dim nDemQuantity As Decimal
    Dim nDemPcs As Decimal
    Dim sDemCreatedBy As String
    Dim sDemCreatedDate As String
    Dim sDemModifiedBy As String
    Dim sDemModifiedDate As String
    Dim sDemEnteredOnMachineID As String
    Dim sDemIsApproved As String
    Dim sDemApprovedBy As String
    Dim sDemApprovedOn As String
    Dim sDemModuleName As String
    Dim nDemIndentQuantity As Decimal
    Dim nDemIndentPcs As Decimal
    Dim nDemPOQuantity As Decimal
    Dim nDemPOQuantityPcs As Decimal
    Dim nDemReceivedQuantity As Decimal
    Dim nDemReceivedPcs As Decimal
    Dim sDemDepartment As String
    Dim sDemSize As String
    Dim sDemMaterialSizeGroupName As String
    Dim sDemVariant As String
    Dim nDemConsumption As Decimal
    Dim sDemComponentGroup As String
    Dim sDemStatus As String
    Dim sDemParentMaterialCode As String
    Dim sDemForecastOrderNo As String
    Dim sDemCommitmentOrderNo As String
    Dim sDemExeVersionNo As String
    Dim sDemProcessOrderQuantity As String


#Region "Fields of SalesOrderforForecastMapping"

    Dim sID As String
    Dim sCreatedBy As String
    Dim dCreatedDate As Date
    Dim sModifiedBy As String
    Dim dModifiedDate As Date
    Dim sExeVersionNo As String
    Dim bIsApproved As Integer
    Dim sApprovedBy As String
    Dim dApprovedOn As Date
    Dim sModuleName As String
    Dim sSalesOrderId As String
    Dim sSalesOrderNo As String
    Dim sBuyerBuy As String
    Dim sBuyerOrderNo As String
    Dim sBuyerGroupCode As String
    Dim sDestination As String
    Dim dOrderRecivedDate As Date
    Dim dOrderConfirmedDate As Date
    Dim sOrderQuality As String
    Dim sOrderStatus As String
    Dim sSeason As String
    Dim sShipper As String
    Dim sArticleGroup As String
    Dim sUnit As String
    Dim sCurrency As String
    Dim decCurrencyConversion As Decimal
    Dim sIsAssortedOrder As String
    Dim decTotalOrderQuantity As Decimal
    Dim sUserCategory As String
    Dim dSalesOrderDate As Date
    Dim sBuyerOrderType As String
    Dim sOrderNo As String
    Dim sSizeName As String
    Dim sPortOfDischarge As String
    Dim sInternalSalesOrderNo As String
    Dim sInternalBuyer As String
    Dim sType As String
    Dim sBuyerOrderID As String

#End Region


#Region "Fields of SalesOrderforForecastMappingDetails"

    Dim sDetID As String
    Dim sDetCreatedBy As String
    Dim dDetCreatedDate As Date
    Dim sDetModifiedBy As String
    Dim dDetModifiedDate As Date
    Dim sDetExeVersionNo As String
    Dim bDetIsApproved As Integer
    Dim sDetApprovedBy As String
    Dim dDetApprovedOn As Date
    Dim sDetModuleName As String
    Dim sDetShipper As String
    Dim sDetOrderNo As String
    Dim sDetSalesOrderNo As String
    Dim sDetBuyerGroupCode As String
    Dim sDetBuyerOrderNo As String
    Dim sDetArticleGroup As String
    Dim sDetArticle As String
    Dim sDetLeatherCode As String
    Dim sDetLeatherName As String
    Dim sDetColorCode As String
    Dim sDetColorName As String
    Dim sDetMaterialCode As String
    Dim sDetDescription As String
    Dim sDetMaterialTypeCode As String
    Dim sDetMaterialColor As String
    Dim sDetMaterialSize As String
    Dim sDetMaterialSizeGroup As String
    Dim sDetUnit As String
    Dim decDetConsumptionQuantity As Decimal
    Dim decDetConsumptionPcs As Decimal
    Dim decDetQuantity As Decimal
    Dim decDetPcs As Decimal
    Dim decDetIndentQuantity As Decimal
    Dim decDetIndentPcs As Decimal
    Dim decDetPOQuantity As Decimal
    Dim decDetPOQuantityPcs As Decimal
    Dim decDetReceivedQuantity As Decimal
    Dim decDetReceivedPcs As Decimal
    Dim sDetDepartment As String
    Dim sDetSize As String
    Dim sDetMaterialSizeGroupName As String
    Dim nDetVariant As Integer
    Dim decDetConsumption As Decimal
    Dim sDetComponentGroup As String
    Dim sDetStatus As String
    Dim sDetParentMaterialCode As String
    Dim sDetForecastOrderNo As String
    Dim sDetSOFMID As String
    Dim sDetDemandId As String

#End Region

#End Region


#Region "Functions"


    Public Function LoadSeason() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelSeason As New SqlDataAdapter
        Dim dsSelSeason As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadSeason"

        dsSelSeason.Clear()
        daSelSeason = New SqlDataAdapter(sCmd)
        daSelSeason.Fill(dsSelSeason, "Season")
        Return dsSelSeason.Tables(0)

        dsSelSeason = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCustomer(ByVal sSeason As String) As DataTable
        ''Try

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCustomer"
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = sSeason

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Function

    Public Function LoadSalesOrder(ByVal sSeason As String, ByVal sCustomer As String) As DataTable
        ''Try

        Dim sCmd As New SqlCommand
        Dim daSelSalesOrder As New SqlDataAdapter
        Dim dsSelSalesOrder As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadOrderNos"
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = sSeason
        sCmd.Parameters.Add(New SqlParameter("@mCustomer", SqlDbType.VarChar)).Value() = sCustomer

        dsSelSalesOrder.Clear()
        daSelSalesOrder = New SqlDataAdapter(sCmd)
        daSelSalesOrder.Fill(dsSelSalesOrder, "SalesOrder")
        Return dsSelSalesOrder.Tables(0)

        dsSelSalesOrder = Nothing
        sCnn.Close()

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Function

    Public Function LoadForecastOrder(ByVal sSeason As String, ByVal sCustomer As String) As DataTable
        ''Try

        Dim sCmd As New SqlCommand
        Dim daSelForecastOrder As New SqlDataAdapter
        Dim dsSelForecastOrder As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadForecastOrder"
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = sSeason
        sCmd.Parameters.Add(New SqlParameter("@mCustomer", SqlDbType.VarChar)).Value() = sCustomer

        dsSelForecastOrder.Clear()
        daSelForecastOrder = New SqlDataAdapter(sCmd)
        daSelForecastOrder.Fill(dsSelForecastOrder, "ForecastOrder")
        Return dsSelForecastOrder.Tables(0)

        dsSelForecastOrder = Nothing
        sCnn.Close()

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Function

    Dim sSalesORderDetailNo, sForeCastORderNo, sNewSalesORder As String
    Dim dForecastQuantity As Decimal

    Dim nTmpIndentQuantity, nTmpPOQuantity As Decimal
    Dim nTmpAdjustedQuantity, nTmpBal2AdjustQuantity, nTmpPOAdjustedQuantity, nTmpPOBal2AdjustQuantity As Decimal
    Dim nTmpAdjustedQuantity1, nTmpPOAdjustedQuantity1 As Decimal
    Dim nTmpID As Integer

    Public Function UpdateDemand(ByVal sOrderNo As String) As DataTable

        Dim sCmd1 As New SqlCommand
        Dim daSelSalesOrder As New SqlDataAdapter
        Dim dsSelSalesOrder As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadSalesOrder"
        sCmd1.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value() = sOrderNo

        dsSelSalesOrder.Clear()
        daSelSalesOrder = New SqlDataAdapter(sCmd1)
        daSelSalesOrder.Fill(dsSelSalesOrder, "SO")

        Dim i As Integer = 0

        For i = 0 To dsSelSalesOrder.Tables(0).Rows.Count - 1
            'Dim sGUID As String
            'sGUID = System.Guid.NewGuid.ToString()
            'MessageBox.Show(sGUID)
            sNewSalesORder = "Y"

            sID = System.Guid.NewGuid.ToString()
            sCreatedBy = dsSelSalesOrder.Tables(0).Rows(i).Item("CreatedBy").ToString
            dCreatedDate = dsSelSalesOrder.Tables(0).Rows(i).Item("CreatedDate")
            sModifiedBy = dsSelSalesOrder.Tables(0).Rows(i).Item("ModifiedBy").ToString
            dModifiedDate = dsSelSalesOrder.Tables(0).Rows(i).Item("ModifiedDate").ToString
            sExeVersionNo = dsSelSalesOrder.Tables(0).Rows(i).Item("ExeVersionNo").ToString
            bIsApproved = dsSelSalesOrder.Tables(0).Rows(i).Item("IsApproved")
            sApprovedBy = dsSelSalesOrder.Tables(0).Rows(i).Item("ApprovedBy").ToString
            dApprovedOn = dsSelSalesOrder.Tables(0).Rows(i).Item("ApprovedOn")
            sModuleName = dsSelSalesOrder.Tables(0).Rows(i).Item("ModuleName").ToString
            sSalesOrderId = dsSelSalesOrder.Tables(0).Rows(i).Item("Id").ToString
            sSalesOrderNo = dsSelSalesOrder.Tables(0).Rows(i).Item("SalesOrderNo").ToString
            sBuyerBuy = dsSelSalesOrder.Tables(0).Rows(i).Item("BuyerBuy").ToString
            sBuyerOrderNo = dsSelSalesOrder.Tables(0).Rows(i).Item("BuyerOrderNO").ToString
            sBuyerGroupCode = dsSelSalesOrder.Tables(0).Rows(i).Item("BuyerGroupCode").ToString
            sDestination = dsSelSalesOrder.Tables(0).Rows(i).Item("Destination").ToString
            dOrderRecivedDate = dsSelSalesOrder.Tables(0).Rows(i).Item("OrderRecivedDate")
            dOrderConfirmedDate = dsSelSalesOrder.Tables(0).Rows(i).Item("OrderConfirmedDate").ToString
            sOrderQuality = dsSelSalesOrder.Tables(0).Rows(i).Item("OrderQuality").ToString
            sOrderStatus = dsSelSalesOrder.Tables(0).Rows(i).Item("OrderStatus").ToString
            sSeason = dsSelSalesOrder.Tables(0).Rows(i).Item("Season").ToString
            sShipper = dsSelSalesOrder.Tables(0).Rows(i).Item("Shipper").ToString
            sArticleGroup = dsSelSalesOrder.Tables(0).Rows(i).Item("ArticleGroup").ToString
            sUnit = dsSelSalesOrder.Tables(0).Rows(i).Item("Unit").ToString
            sCurrency = dsSelSalesOrder.Tables(0).Rows(i).Item("Currency").ToString
            decCurrencyConversion = dsSelSalesOrder.Tables(0).Rows(i).Item("CurrencyConversion").ToString
            sIsAssortedOrder = dsSelSalesOrder.Tables(0).Rows(i).Item("IsAssortedOrder").ToString
            decTotalOrderQuantity = dsSelSalesOrder.Tables(0).Rows(i).Item("TotalOrderQuantity").ToString
            sUserCategory = dsSelSalesOrder.Tables(0).Rows(i).Item("UserCategory").ToString
            dSalesOrderDate = dsSelSalesOrder.Tables(0).Rows(i).Item("SalesOrderDate")
            sBuyerOrderType = dsSelSalesOrder.Tables(0).Rows(i).Item("BuyerOrderType").ToString
            sOrderNo = dsSelSalesOrder.Tables(0).Rows(i).Item("OrderNo").ToString
            sSizeName = dsSelSalesOrder.Tables(0).Rows(i).Item("SizeName").ToString
            sPortOfDischarge = dsSelSalesOrder.Tables(0).Rows(i).Item("PortofDischarge").ToString
            sInternalSalesOrderNo = dsSelSalesOrder.Tables(0).Rows(i).Item("InternalSalesORderNo").ToString
            sInternalBuyer = dsSelSalesOrder.Tables(0).Rows(i).Item("InternalBuyer").ToString
            sType = dsSelSalesOrder.Tables(0).Rows(i).Item("Type").ToString
            sBuyerOrderID = dsSelSalesOrder.Tables(0).Rows(i).Item("BuyerOrderId").ToString

            Dim sCmd2 As New SqlCommand
            Dim daSelSalesOrderDetail As New SqlDataAdapter
            Dim dsSelSalesOrderDetail As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "KHLI_SalesOrder2ForeCastMapping"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadSalesOrderDetail"
            sCmd2.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value() = sSalesOrderNo

            dsSelSalesOrderDetail.Clear()
            daSelSalesOrderDetail = New SqlDataAdapter(sCmd2)
            daSelSalesOrderDetail.Fill(dsSelSalesOrderDetail, "SOD")

            Dim j As Integer = 0

            For j = 0 To dsSelSalesOrderDetail.Tables(0).Rows.Count - 1
                sSalesORderDetailNo = dsSelSalesOrderDetail.Tables(0).Rows(j).Item("SalesOrderNo")
                sForeCastORderNo = dsSelSalesOrderDetail.Tables(0).Rows(j).Item("ForeCastOrderNo")


                Dim sCmd3 As New SqlCommand
                Dim daSelSODemand As New SqlDataAdapter
                Dim dsSelSODemand As New DataSet

                sCmd3.Connection = sCnn
                sCmd3.CommandText = "KHLI_SalesOrder2ForeCastMapping"
                sCmd3.CommandType = CommandType.StoredProcedure

                sCmd3.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadDemandBySO"
                sCmd3.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = sSalesORderDetailNo

                dsSelSODemand.Clear()
                daSelSODemand = New SqlDataAdapter(sCmd3)
                daSelSODemand.Fill(dsSelSODemand, "Demand")

                Dim k As Integer = 0
                For k = 0 To dsSelSODemand.Tables(0).Rows.Count - 1

                    sDetID = System.Guid.NewGuid.ToString()
                    sDetCreatedBy = dsSelSODemand.Tables(0).Rows(k).Item("CreatedBy").ToString
                    dDetCreatedDate = dsSelSODemand.Tables(0).Rows(k).Item("CreatedDate").ToString
                    sDetModifiedBy = dsSelSODemand.Tables(0).Rows(k).Item("ModifiedBy").ToString
                    dDetModifiedDate = dsSelSODemand.Tables(0).Rows(k).Item("ModifiedDate").ToString
                    sDetExeVersionNo = dsSelSODemand.Tables(0).Rows(k).Item("ExeVersionNo").ToString
                    '' '' ''bDetIsApproved = dsSelSODemand.Tables(0).Rows(k).Item("IsApproved")
                    sDetApprovedBy = dsSelSODemand.Tables(0).Rows(k).Item("ApprovedBy").ToString
                    '' '' ''dDetApprovedOn = dsSelSODemand.Tables(0).Rows(k).Item("ApprovedOn")
                    sDetModuleName = dsSelSODemand.Tables(0).Rows(k).Item("ModuleName").ToString
                    sDetShipper = dsSelSODemand.Tables(0).Rows(k).Item("Shipper").ToString
                    sDetOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("OrderNo").ToString
                    sDetSalesOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("SalesOrderNo").ToString
                    sDetBuyerGroupCode = dsSelSODemand.Tables(0).Rows(k).Item("BuyerGroupCode").ToString
                    sDetBuyerOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("BuyerOrderNo").ToString
                    sDetArticleGroup = dsSelSODemand.Tables(0).Rows(k).Item("ArticleGroup").ToString
                    sDetArticle = dsSelSODemand.Tables(0).Rows(k).Item("Article").ToString
                    sDetLeatherCode = dsSelSODemand.Tables(0).Rows(k).Item("LeatherCode").ToString
                    sDetLeatherName = dsSelSODemand.Tables(0).Rows(k).Item("LeatherName").ToString
                    sDetColorCode = dsSelSODemand.Tables(0).Rows(k).Item("ColorCode").ToString
                    sDetColorName = dsSelSODemand.Tables(0).Rows(k).Item("ColorName").ToString
                    sDetMaterialCode = dsSelSODemand.Tables(0).Rows(k).Item("MaterialCode").ToString
                    sDetDescription = dsSelSODemand.Tables(0).Rows(k).Item("Description").ToString
                    sDetMaterialTypeCode = dsSelSODemand.Tables(0).Rows(k).Item("MaterialTypeCode").ToString
                    sDetMaterialColor = dsSelSODemand.Tables(0).Rows(k).Item("MaterialColor").ToString
                    sDetMaterialSize = dsSelSODemand.Tables(0).Rows(k).Item("MaterialSize").ToString
                    sDetMaterialSizeGroup = dsSelSODemand.Tables(0).Rows(k).Item("MaterialSizeGroup").ToString
                    sDetUnit = dsSelSODemand.Tables(0).Rows(k).Item("Unit").ToString
                    decDetConsumptionQuantity = dsSelSODemand.Tables(0).Rows(k).Item("ConsumptionQuantity").ToString
                    decDetQuantity = dsSelSODemand.Tables(0).Rows(k).Item("Quantity").ToString
                    decDetIndentQuantity = dsSelSODemand.Tables(0).Rows(k).Item("IndentQuantity").ToString

                    If decDetIndentQuantity > 0 Then
                        GoTo Aa
                    End If
                    decDetIndentPcs = dsSelSODemand.Tables(0).Rows(k).Item("IndentPcs").ToString
                    decDetPOQuantity = dsSelSODemand.Tables(0).Rows(k).Item("POQuantity").ToString
                    decDetPOQuantityPcs = dsSelSODemand.Tables(0).Rows(k).Item("POQuantityPcs").ToString
                    decDetReceivedQuantity = dsSelSODemand.Tables(0).Rows(k).Item("ReceivedQuantity").ToString
                    decDetReceivedPcs = dsSelSODemand.Tables(0).Rows(k).Item("ReceivedPcs").ToString
                    sDetDepartment = dsSelSODemand.Tables(0).Rows(k).Item("Department").ToString
                    sDetSize = dsSelSODemand.Tables(0).Rows(k).Item("Size").ToString
                    sDetMaterialSizeGroupName = dsSelSODemand.Tables(0).Rows(k).Item("MaterialSizeGroupName").ToString
                    nDetVariant = dsSelSODemand.Tables(0).Rows(k).Item("Variant").ToString
                    decDetConsumption = dsSelSODemand.Tables(0).Rows(k).Item("Consumption").ToString
                    sDetComponentGroup = dsSelSODemand.Tables(0).Rows(k).Item("ComponentGroup").ToString
                    sDetStatus = dsSelSODemand.Tables(0).Rows(k).Item("Status").ToString
                    sDetParentMaterialCode = dsSelSODemand.Tables(0).Rows(k).Item("ParentMaterialCode").ToString
                    sDetForecastOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("ForecastOrderNo").ToString
                    sDetSOFMID = sID
                    sDetDemandId = dsSelSODemand.Tables(0).Rows(k).Item("Id").ToString

                    If decDetIndentQuantity = 0 Then
                        ''Coding to Check for Forecast Order Quantity''

                        Dim sCmd4 As New SqlCommand
                        Dim daSelForecastQuantity As New SqlDataAdapter
                        Dim dsSelForecastQuantity As New DataSet

                        sCmd4.Connection = sCnn
                        sCmd4.CommandText = "KHLI_SalesOrder2ForeCastMapping"
                        sCmd4.CommandType = CommandType.StoredProcedure

                        sCmd4.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadForecastQty"
                        sCmd4.Parameters.Add(New SqlParameter("@mForecastOrderNo", SqlDbType.VarChar)).Value() = sForeCastORderNo
                        sCmd4.Parameters.Add(New SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value() = sDetArticleGroup
                        sCmd4.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = sDetArticle
                        sCmd4.Parameters.Add(New SqlParameter("@mColorCode", SqlDbType.VarChar)).Value() = sDetColorCode
                        sCmd4.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDetMaterialCode
                        sCmd4.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDetMaterialSize



                        dsSelForecastQuantity.Clear()
                        daSelForecastQuantity = New SqlDataAdapter(sCmd4)
                        daSelForecastQuantity.Fill(dsSelForecastQuantity, "Demand")

                        dForecastQuantity = dsSelForecastQuantity.Tables(0).Rows(0).Item(0)

                        If dForecastQuantity = 0 Then
                            GoTo Aa
                        Else
                            ''Coding to do for If Indent Generated for Other Orders''
                            ''Coding to do for If Indent Generated for Other Orders''

                            ''Adjusting Forecast Quantity With the Current SalesOrderQuantity''

                            Dim sCmd5 As New SqlCommand
                            Dim daSelCurrentFCQty As New SqlDataAdapter
                            Dim dsSelCurrentFCQty As New DataSet

                            sCmd5.Connection = sCnn
                            sCmd5.CommandText = "KHLI_SalesOrder2ForeCastMapping"
                            sCmd5.CommandType = CommandType.StoredProcedure

                            sCmd5.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCurrentFCQty"
                            sCmd5.Parameters.Add(New SqlParameter("@mDtlSOFMID", SqlDbType.VarChar)).Value() = sDetSOFMID
                            

                            dsSelCurrentFCQty.Clear()
                            daSelCurrentFCQty = New SqlDataAdapter(sCmd5)
                            daSelCurrentFCQty.Fill(dsSelCurrentFCQty, "Demand")

                            dForecastQuantity = dForecastQuantity - dsSelCurrentFCQty.Tables(0).Rows(0).Item(0)

                            ''Adjusting Forecast Quantity With the Current SalesOrderQuantity''
                            If decDetQuantity < dForecastQuantity Then
                                decDetIndentQuantity = decDetQuantity
                                dForecastQuantity = dForecastQuantity - decDetIndentQuantity
                            End If

                            If sNewSalesORder = "Y" Then
                                InsertSalesOrderForeCastMapping()
                                sNewSalesORder = "N"
                            End If
                            InsertSalesOrderForeCastMappingDetails()
                        End If


                        
                        ''Coding to Check for Forecast Order Quantity''
                    End If

Aa:
                Next
            Next
        Next

        ''To Delete  after completing this code''
        daSelSalesOrder.Fill(dsSelSalesOrder, "SalesOrder")
        Return dsSelSalesOrder.Tables(0)

        dsSelSalesOrder = Nothing
        sCnn.Close()
        ''To Delete  after completing this code''
    End Function

    Public Function UpdateDemandAgainstForecastOrder(ByVal sOrderNo As String) As DataTable

        Dim sCmd1 As New SqlCommand
        Dim daSelFCDemand As New SqlDataAdapter
        Dim dsSelFCDemand As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "FCDemand"
        sCmd1.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo

        dsSelFCDemand.Clear()
        daSelFCDemand = New SqlDataAdapter(sCmd1)
        daSelFCDemand.Fill(dsSelFCDemand, "FCD")

        Dim i As Integer = 0

        For i = 0 To dsSelFCDemand.Tables(0).Rows.Count - 1
            sNewSalesORder = "Y"

            sDemID = System.Guid.NewGuid.ToString()
            sDemShipper = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("Shipper").ToString
            sDemOrderNo = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("OrderNo").ToString
            sDemSalesOrderNo = dsSelFCDemand.Tables(0).Rows(i).Item("SalesOrderNo").ToString
            sDemBuyerCode = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("BuyerCode").ToString
            sDemBuyerGroupCode = dsSelFCDemand.Tables(0).Rows(i).Item("BuyerGroupCode").ToString
            sDemBuyerOrderNo = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("BuyerOrderNo").ToString
            sDemArticleGroup = dsSelFCDemand.Tables(0).Rows(i).Item("ArticleGroup").ToString
            sDemArticle = dsSelFCDemand.Tables(0).Rows(i).Item("Article").ToString
            sDemLeatherCode = dsSelFCDemand.Tables(0).Rows(i).Item("LeatherCode").ToString
            sDemLeatherName = dsSelFCDemand.Tables(0).Rows(i).Item("LeatherName").ToString
            sDemColorCode = dsSelFCDemand.Tables(0).Rows(i).Item("ColorCode").ToString
            sDemColorName = dsSelFCDemand.Tables(0).Rows(i).Item("ColorName").ToString
            sDemMaterialCode = dsSelFCDemand.Tables(0).Rows(i).Item("MaterialCode").ToString
            sDemDescription = dsSelFCDemand.Tables(0).Rows(i).Item("Description").ToString
            sDemMaterialTypeCode = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("MaterialTypeCode").ToString
            sDemMaterialCategory = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("MaterialCategory").ToString
            sDemMaterialColor = dsSelFCDemand.Tables(0).Rows(i).Item("MaterialColor").ToString
            sDemMaterialSize = dsSelFCDemand.Tables(0).Rows(i).Item("MaterialSize").ToString
            sDemMaterialSizeGroup = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("MaterialSizeGroup").ToString
            sDemUnit = dsSelFCDemand.Tables(0).Rows(i).Item("Unit").ToString
            nDemConsumptionQuantity = dsSelFCDemand.Tables(0).Rows(i).Item("ConsumptionQuantity").ToString
            nDemConsumptionPcs = "0" '' dsSelFCDemand.Tables(0).Rows(i).Item("ConsumptionPcs").ToString
            nDemQuantity = dsSelFCDemand.Tables(0).Rows(i).Item("Quantity").ToString
            nDemPcs = "0" '' dsSelFCDemand.Tables(0).Rows(i).Item("Pcs").ToString
            sDemCreatedBy = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("CreatedBy").ToString
            sDemCreatedDate = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("CreatedDate").ToString
            sDemModifiedBy = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ModifiedBy").ToString
            sDemModifiedDate = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ModifiedDate").ToString
            sDemEnteredOnMachineID = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("EnteredOnMachineID").ToString
            sDemIsApproved = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("IsApproved").ToString
            sDemApprovedBy = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ApprovedBy").ToString
            sDemApprovedOn = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ApprovedOn").ToString
            sDemModuleName = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ModuleName").ToString

            '''''''

            Dim sCmd10001 As New SqlCommand
            Dim daSelTmpTable As New SqlDataAdapter
            Dim dsSelTmpTable As New DataSet

            sCmd10001.Connection = sCnn
            sCmd10001.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
            sCmd10001.CommandType = CommandType.StoredProcedure

            sCmd10001.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELD4TMP"
            sCmd10001.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
            sCmd10001.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
            sCmd10001.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize

            dsSelTmpTable.Clear()
            daSelTmpTable = New SqlDataAdapter(sCmd10001)
            daSelTmpTable.Fill(dsSelTmpTable, "FCD")

            If dsSelTmpTable.Tables(0).Rows.Count = 0 Then

                Dim sCmd1002 As New SqlCommand
                
                sCmd1002.Connection = sCnn
                sCmd1002.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
                sCmd1002.CommandType = CommandType.StoredProcedure

                sCmd1002.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSD4TMP"
                sCmd1002.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
                sCmd1002.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
                sCmd1002.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize

                Dim sCmd100 As New SqlCommand
                Dim daSelFCIndent As New SqlDataAdapter
                Dim dsSelFCIndent As New DataSet

                sCmd100.Connection = sCnn
                sCmd100.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
                sCmd100.CommandType = CommandType.StoredProcedure

                sCmd100.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "FCIndQty"
                sCmd100.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
                sCmd100.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
                sCmd100.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize

                dsSelFCIndent.Clear()
                daSelFCIndent = New SqlDataAdapter(sCmd100)
                daSelFCIndent.Fill(dsSelFCIndent, "FCD")

                nDemIndentQuantity = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString
                nDemIndentPcs = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString


                sCmd1002.Parameters.Add(New SqlParameter("@mIndentQuantity", SqlDbType.VarChar)).Value() = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString
                nTmpIndentQuantity = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString
                sCmd1002.Parameters.Add(New SqlParameter("@mAdjustedQuantity", SqlDbType.VarChar)).Value() = 0
                sCmd1002.Parameters.Add(New SqlParameter("@mBal2AdjustQuantity", SqlDbType.VarChar)).Value() = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString
                nTmpBal2AdjustQuantity = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString

                Dim sCmd1001 As New SqlCommand
                Dim daSelFCPO As New SqlDataAdapter
                Dim dsSelFCPO As New DataSet

                sCmd1001.Connection = sCnn
                sCmd1001.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
                sCmd1001.CommandType = CommandType.StoredProcedure

                sCmd1001.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "FCPOQty"
                sCmd1001.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
                sCmd1001.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
                sCmd1001.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize


                dsSelFCPO.Clear()
                daSelFCPO = New SqlDataAdapter(sCmd1001)
                daSelFCPO.Fill(dsSelFCPO, "FCD")

                sCmd1002.Parameters.Add(New SqlParameter("@mPOQuantity", SqlDbType.VarChar)).Value() = dsSelFCPO.Tables(0).Rows(0).Item("POQuantity").ToString
                nTmpPOQuantity = dsSelFCPO.Tables(0).Rows(0).Item("POQuantity").ToString
                sCmd1002.Parameters.Add(New SqlParameter("@mPOAdjustedQuantity", SqlDbType.VarChar)).Value() = 0
                sCmd1002.Parameters.Add(New SqlParameter("@mPOBal2AdjustQuantity", SqlDbType.VarChar)).Value() = dsSelFCPO.Tables(0).Rows(0).Item("POQuantity").ToString
                nTmpPOBal2AdjustQuantity = dsSelFCPO.Tables(0).Rows(0).Item("POQuantity").ToString

                sCnn.Open()

                Dim sRes1002 As String = sCmd1002.ExecuteScalar

                If Val(sRes1002) = 0 Then
                    sCnn.Close()
                Else
                    'setError(Val(sRes))
                End If
                sCnn.Close()

                nDemIndentQuantity = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString
                nDemIndentPcs = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString

                nDemPOQuantity = dsSelFCPO.Tables(0).Rows(0).Item("POQuantity").ToString
                nDemPOQuantityPcs = dsSelFCPO.Tables(0).Rows(0).Item("POQuantity").ToString


                Dim sCmd10002 As New SqlCommand
                Dim daSelTmpTable1 As New SqlDataAdapter
                Dim dsSelTmpTable1 As New DataSet

                sCmd10002.Connection = sCnn
                sCmd10002.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
                sCmd10002.CommandType = CommandType.StoredProcedure

                sCmd10002.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELD4TMP"
                sCmd10002.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
                sCmd10002.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
                sCmd10002.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize

                dsSelTmpTable1.Clear()
                daSelTmpTable1 = New SqlDataAdapter(sCmd10002)
                daSelTmpTable1.Fill(dsSelTmpTable1, "FCD")

                nTmpID = dsSelTmpTable1.Tables(0).Rows(0).Item("PKID").ToString
            Else
              

                nTmpID = dsSelTmpTable.Tables(0).Rows(0).Item("PKID").ToString

                nTmpIndentQuantity = dsSelTmpTable.Tables(0).Rows(0).Item("IndentQuantity").ToString
                nTmpAdjustedQuantity = dsSelTmpTable.Tables(0).Rows(0).Item("AdjustedQuantity").ToString
                nTmpBal2AdjustQuantity = dsSelTmpTable.Tables(0).Rows(0).Item("Bal2AdjustQuantity").ToString

                nDemIndentQuantity = 0 'dsSelTmpTable.Tables(0).Rows(0).Item("Bal2AdjustQuantity").ToString
                nDemIndentPcs = dsSelTmpTable.Tables(0).Rows(0).Item("Bal2AdjustQuantity").ToString

                nTmpPOQuantity = dsSelTmpTable.Tables(0).Rows(0).Item("POQuantity").ToString
                nTmpPOAdjustedQuantity = dsSelTmpTable.Tables(0).Rows(0).Item("POAdjustedQuantity").ToString
                nTmpPOBal2AdjustQuantity = dsSelTmpTable.Tables(0).Rows(0).Item("POBal2AdjustQuantity").ToString

                nDemPOQuantity = 0 'dsSelTmpTable.Tables(0).Rows(0).Item("POBal2AdjustQuantity").ToString
                nDemPOQuantityPcs = dsSelTmpTable.Tables(0).Rows(0).Item("POBal2AdjustQuantity").ToString
            End If

            '''''''

            'Dim sCmd100 As New SqlCommand
            'Dim daSelFCIndent As New SqlDataAdapter
            'Dim dsSelFCIndent As New DataSet

            'sCmd100.Connection = sCnn
            'sCmd100.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
            'sCmd100.CommandType = CommandType.StoredProcedure

            'sCmd100.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "FCIndQty"
            'sCmd100.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
            'sCmd100.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
            'sCmd100.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize
            'sCmd100.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = sDemArticle
            'sCmd100.Parameters.Add(New SqlParameter("@mColorCode", SqlDbType.VarChar)).Value() = sDemColorCode
            'sCmd100.Parameters.Add(New SqlParameter("@mLeathercode", SqlDbType.VarChar)).Value() = sDemLeatherCode


            'dsSelFCIndent.Clear()
            'daSelFCIndent = New SqlDataAdapter(sCmd100)
            'daSelFCIndent.Fill(dsSelFCIndent, "FCD")

            'nDemIndentQuantity = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString
            'nDemIndentPcs = dsSelFCIndent.Tables(0).Rows(0).Item("IndentQuantity").ToString

            

            nDemReceivedQuantity = dsSelFCDemand.Tables(0).Rows(i).Item("ReceivedQuantity").ToString
            nDemReceivedPcs = dsSelFCDemand.Tables(0).Rows(i).Item("ReceivedQuantity").ToString
            sDemDepartment = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("Department").ToString
            sDemSize = dsSelFCDemand.Tables(0).Rows(i).Item("Size").ToString
            sDemMaterialSizeGroupName = dsSelFCDemand.Tables(0).Rows(i).Item("MaterialSizeGroupName").ToString
            sDemVariant = dsSelFCDemand.Tables(0).Rows(i).Item("Variant").ToString
            nDemConsumption = "0" '' dsSelFCDemand.Tables(0).Rows(i).Item("Consumption").ToString
            sDemComponentGroup = dsSelFCDemand.Tables(0).Rows(i).Item("ComponentGroup").ToString
            sDemStatus = "From ForeCast" '' dsSelFCDemand.Tables(0).Rows(i).Item("Status").ToString
            sDemParentMaterialCode = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ParentMaterialCode").ToString
            sDemForecastOrderNo = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ForecastOrderNo").ToString
            sDemCommitmentOrderNo = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("CommitmentOrderNo").ToString
            sDemExeVersionNo = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ExeVersionNo").ToString
            sDemProcessOrderQuantity = "" '' dsSelFCDemand.Tables(0).Rows(i).Item("ProcessOrderQuantity").ToString



            ''''''
            sCnn.Close()
            Dim sCmd101 As New SqlCommand
            Dim daInsFCDemand As New SqlDataAdapter
            Dim dsInsFCDemand As New DataSet

            sCmd101.Connection = sCnn
            sCmd101.CommandText = "[KHLI_Forecast2SalesOrderAdjustments]"
            sCmd101.CommandType = CommandType.StoredProcedure

            sCmd101.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTDWAD"

            sCmd101.Parameters.Add(New SqlParameter("@mDWADID", SqlDbType.VarChar)).Value() = sDemID
            sCmd101.Parameters.Add(New SqlParameter("@mDWADShipper", SqlDbType.VarChar)).Value() = sDemShipper
            sCmd101.Parameters.Add(New SqlParameter("@mDWADOrderNo", SqlDbType.VarChar)).Value() = sDemOrderNo
            sCmd101.Parameters.Add(New SqlParameter("@mDWADSalesOrderNo", SqlDbType.VarChar)).Value() = sDemSalesOrderNo
            sCmd101.Parameters.Add(New SqlParameter("@mDWADBuyerCode", SqlDbType.VarChar)).Value() = sDemBuyerCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADBuyerGroupCode", SqlDbType.VarChar)).Value() = sDemBuyerGroupCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADBuyerOrderNo", SqlDbType.VarChar)).Value() = sDemBuyerOrderNo
            sCmd101.Parameters.Add(New SqlParameter("@mDWADArticleGroup", SqlDbType.VarChar)).Value() = sDemArticleGroup
            sCmd101.Parameters.Add(New SqlParameter("@mDWADArticle", SqlDbType.VarChar)).Value() = sDemArticle
            sCmd101.Parameters.Add(New SqlParameter("@mDWADLeatherCode", SqlDbType.VarChar)).Value() = sDemLeatherCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADLeatherName", SqlDbType.VarChar)).Value() = sDemLeatherName
            sCmd101.Parameters.Add(New SqlParameter("@mDWADColorCode", SqlDbType.VarChar)).Value() = sDemColorCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADColorName", SqlDbType.VarChar)).Value() = sDemColorName
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADDescription", SqlDbType.VarChar)).Value() = sDemDescription
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialTypeCode", SqlDbType.VarChar)).Value() = sDemMaterialTypeCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialCategory", SqlDbType.VarChar)).Value() = sDemMaterialCategory
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialColor", SqlDbType.VarChar)).Value() = sDemMaterialColor
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialSizeGroup", SqlDbType.VarChar)).Value() = sDemMaterialSizeGroup
            sCmd101.Parameters.Add(New SqlParameter("@mDWADUnit", SqlDbType.VarChar)).Value() = sDemUnit
            sCmd101.Parameters.Add(New SqlParameter("@mDWADConsumptionQuantity", SqlDbType.Decimal)).Value() = nDemConsumptionQuantity
            sCmd101.Parameters.Add(New SqlParameter("@mDWADConsumptionPcs", SqlDbType.Decimal)).Value() = nDemConsumptionPcs
            sCmd101.Parameters.Add(New SqlParameter("@mDWADQuantity", SqlDbType.Decimal)).Value() = nDemQuantity
            sCmd101.Parameters.Add(New SqlParameter("@mDWADPcs", SqlDbType.Decimal)).Value() = nDemPcs
            sCmd101.Parameters.Add(New SqlParameter("@mDWADCreatedBy", SqlDbType.VarChar)).Value() = sDemCreatedBy
            sCmd101.Parameters.Add(New SqlParameter("@mDWADCreatedDate", SqlDbType.DateTime)).Value() = Date.Now
            sCmd101.Parameters.Add(New SqlParameter("@mDWADModifiedBy", SqlDbType.VarChar)).Value() = sDemModifiedBy
            sCmd101.Parameters.Add(New SqlParameter("@mDWADModifiedDate", SqlDbType.DateTime)).Value() = Date.Now
            sCmd101.Parameters.Add(New SqlParameter("@mDWADEnteredOnMachineID", SqlDbType.VarChar)).Value() = sDemEnteredOnMachineID
            sCmd101.Parameters.Add(New SqlParameter("@mDWADIsApproved", SqlDbType.Bit)).Value() = 1
            sCmd101.Parameters.Add(New SqlParameter("@mDWADApprovedBy", SqlDbType.VarChar)).Value() = sDemApprovedBy
            sCmd101.Parameters.Add(New SqlParameter("@mDWADApprovedOn", SqlDbType.DateTime)).Value() = Date.Now
            sCmd101.Parameters.Add(New SqlParameter("@mDWADModuleName", SqlDbType.VarChar)).Value() = sDemModuleName
            sCmd101.Parameters.Add(New SqlParameter("@mDWADIndentQuantity", SqlDbType.Decimal)).Value() = nDemIndentQuantity
            sCmd101.Parameters.Add(New SqlParameter("@mDWADIndentPcs", SqlDbType.Decimal)).Value() = nDemIndentPcs
            sCmd101.Parameters.Add(New SqlParameter("@mDWADPOQuantity", SqlDbType.Decimal)).Value() = nDemPOQuantity
            sCmd101.Parameters.Add(New SqlParameter("@mDWADPOQuantityPcs", SqlDbType.Decimal)).Value() = nDemPOQuantityPcs
            sCmd101.Parameters.Add(New SqlParameter("@mDWADReceivedQuantity", SqlDbType.Decimal)).Value() = nDemReceivedQuantity
            sCmd101.Parameters.Add(New SqlParameter("@mDWADReceivedPcs", SqlDbType.Decimal)).Value() = nDemReceivedPcs
            sCmd101.Parameters.Add(New SqlParameter("@mDWADDepartment", SqlDbType.VarChar)).Value() = sDemDepartment
            sCmd101.Parameters.Add(New SqlParameter("@mDWADSize", SqlDbType.VarChar)).Value() = sDemSize
            sCmd101.Parameters.Add(New SqlParameter("@mDWADMaterialSizeGroupName", SqlDbType.VarChar)).Value() = sDemMaterialSizeGroupName
            sCmd101.Parameters.Add(New SqlParameter("@mDWADVariant", SqlDbType.VarChar)).Value() = sDemVariant
            sCmd101.Parameters.Add(New SqlParameter("@mDWADConsumption", SqlDbType.Decimal)).Value() = nDemConsumption
            sCmd101.Parameters.Add(New SqlParameter("@mDWADComponentGroup", SqlDbType.VarChar)).Value() = sDemComponentGroup
            sCmd101.Parameters.Add(New SqlParameter("@mDWADStatus", SqlDbType.VarChar)).Value() = sDemStatus
            sCmd101.Parameters.Add(New SqlParameter("@mDWADParentMaterialCode", SqlDbType.VarChar)).Value() = sOrderNo ' sDemParentMaterialCode
            sCmd101.Parameters.Add(New SqlParameter("@mDWADForecastOrderNo", SqlDbType.VarChar)).Value() = sDemForecastOrderNo
            sCmd101.Parameters.Add(New SqlParameter("@mDWADCommitmentOrderNo", SqlDbType.VarChar)).Value() = sDemCommitmentOrderNo
            sCmd101.Parameters.Add(New SqlParameter("@mDWADExeVersionNo", SqlDbType.VarChar)).Value() = sDemExeVersionNo
            sCmd101.Parameters.Add(New SqlParameter("@mDWADProcessOrderQuantity", SqlDbType.Decimal)).Value() = 0 'sDemProcessOrderQuantity


            sCnn.Open()

            Dim sRes As String = sCmd101.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()


            ''''''


            Dim sCmd2 As New SqlCommand
            Dim daSelSalesOrders As New SqlDataAdapter
            Dim dsSelSalesOrders As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadSalesOrders"
            sCmd2.Parameters.Add(New SqlParameter("@mFCOrderNo", SqlDbType.VarChar)).Value() = sOrderNo

            dsSelSalesOrders.Clear()
            daSelSalesOrders = New SqlDataAdapter(sCmd2)
            daSelSalesOrders.Fill(dsSelSalesOrders, "SO")

            Dim j As Integer = 0

            For j = 0 To dsSelSalesOrders.Tables(0).Rows.Count - 1
                sSalesOrderNo = dsSelSalesOrders.Tables(0).Rows(j).Item("SalesOrderNo")

                Dim sCmd3 As New SqlCommand
                Dim daSelSODemand As New SqlDataAdapter
                Dim dsSelSODemand As New DataSet

                sCmd3.Connection = sCnn
                sCmd3.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
                sCmd3.CommandType = CommandType.StoredProcedure

                sCmd3.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadDemandBySO"
                sCmd3.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = sSalesOrderNo
                sCmd3.Parameters.Add(New SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value() = sDemBuyerGroupCode
                sCmd3.Parameters.Add(New SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value() = sDemArticleGroup
                sCmd3.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = sDemArticle
                sCmd3.Parameters.Add(New SqlParameter("@mLeatherCode", SqlDbType.VarChar)).Value() = sDemLeatherCode
                sCmd3.Parameters.Add(New SqlParameter("@mLeatherName", SqlDbType.VarChar)).Value() = sDemLeatherName
                sCmd3.Parameters.Add(New SqlParameter("@mColorCode", SqlDbType.VarChar)).Value() = sDemColorCode
                sCmd3.Parameters.Add(New SqlParameter("@mColorName", SqlDbType.VarChar)).Value() = sDemColorName
                sCmd3.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = sDemMaterialCode
                sCmd3.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = sDemDescription
                sCmd3.Parameters.Add(New SqlParameter("@mMaterialColor", SqlDbType.VarChar)).Value() = sDemMaterialColor
                sCmd3.Parameters.Add(New SqlParameter("@mMaterialSize", SqlDbType.VarChar)).Value() = sDemMaterialSize
                sCmd3.Parameters.Add(New SqlParameter("@mUnit", SqlDbType.VarChar)).Value() = sDemUnit
                sCmd3.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = sDemSize
                sCmd3.Parameters.Add(New SqlParameter("@mMaterialSizeGroupName", SqlDbType.VarChar)).Value() = sDemMaterialSizeGroupName
                sCmd3.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = sDemVariant
                sCmd3.Parameters.Add(New SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value() = sDemComponentGroup

                dsSelSODemand.Clear()
                daSelSODemand = New SqlDataAdapter(sCmd3)
                daSelSODemand.Fill(dsSelSODemand, "Demand")


                Dim k As Integer = 0
                For k = 0 To dsSelSODemand.Tables(0).Rows.Count - 1


                    Dim sDetBuyerCode, sDetMaterialCategory, sDetEnteredonMachineID, sDetApprovedOn, sDetVariant, sDetCommitmentOrderNo As String
                    Dim nDetIsApproved As Integer
                    Dim nDetConsumption, dDetProcessOrderQuantity As Decimal

                    sDetID = System.Guid.NewGuid.ToString()
                    sDetCreatedBy = dsSelSODemand.Tables(0).Rows(k).Item("CreatedBy").ToString
                    dDetCreatedDate = dsSelSODemand.Tables(0).Rows(k).Item("CreatedDate").ToString
                    sDetModifiedBy = dsSelSODemand.Tables(0).Rows(k).Item("ModifiedBy").ToString
                    dDetModifiedDate = dsSelSODemand.Tables(0).Rows(k).Item("ModifiedDate").ToString
                    sDetEnteredonMachineID = dsSelSODemand.Tables(0).Rows(k).Item("EnteredonMachineID").ToString
                    sDetExeVersionNo = dsSelSODemand.Tables(0).Rows(k).Item("ExeVersionNo").ToString
                    nDetIsApproved = Val(dsSelSODemand.Tables(0).Rows(k).Item("IsApproved").ToString)
                    sDetApprovedBy = dsSelSODemand.Tables(0).Rows(k).Item("ApprovedBy").ToString
                    sDetApprovedOn = dsSelSODemand.Tables(0).Rows(k).Item("ApprovedOn").ToString
                    sDetModuleName = dsSelSODemand.Tables(0).Rows(k).Item("ModuleName").ToString
                    sDetShipper = dsSelSODemand.Tables(0).Rows(k).Item("Shipper").ToString
                    sDetOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("OrderNo").ToString
                    sDetSalesOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("SalesOrderNo").ToString
                    sDetBuyerCode = dsSelSODemand.Tables(0).Rows(k).Item("BuyerCode").ToString
                    sDetBuyerGroupCode = dsSelSODemand.Tables(0).Rows(k).Item("BuyerGroupCode").ToString
                    sDetBuyerOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("BuyerOrderNo").ToString
                    sDetArticleGroup = dsSelSODemand.Tables(0).Rows(k).Item("ArticleGroup").ToString
                    sDetArticle = dsSelSODemand.Tables(0).Rows(k).Item("Article").ToString
                    sDetLeatherCode = dsSelSODemand.Tables(0).Rows(k).Item("LeatherCode").ToString
                    sDetLeatherName = dsSelSODemand.Tables(0).Rows(k).Item("LeatherName").ToString
                    sDetColorCode = dsSelSODemand.Tables(0).Rows(k).Item("ColorCode").ToString
                    sDetColorName = dsSelSODemand.Tables(0).Rows(k).Item("ColorName").ToString
                    sDetMaterialCode = dsSelSODemand.Tables(0).Rows(k).Item("MaterialCode").ToString
                    sDetDescription = dsSelSODemand.Tables(0).Rows(k).Item("Description").ToString
                    sDetMaterialTypeCode = dsSelSODemand.Tables(0).Rows(k).Item("MaterialTypeCode").ToString
                    sDetMaterialCategory = dsSelSODemand.Tables(0).Rows(k).Item("MaterialCategory").ToString
                    sDetMaterialColor = dsSelSODemand.Tables(0).Rows(k).Item("MaterialColor").ToString
                    sDetMaterialSize = dsSelSODemand.Tables(0).Rows(k).Item("MaterialSize").ToString
                    sDetMaterialSizeGroup = dsSelSODemand.Tables(0).Rows(k).Item("MaterialSizeGroup").ToString
                    sDetUnit = dsSelSODemand.Tables(0).Rows(k).Item("Unit").ToString

                    decDetConsumptionQuantity = dsSelSODemand.Tables(0).Rows(k).Item("ConsumptionQuantity").ToString
                    decDetConsumptionPcs = dsSelSODemand.Tables(0).Rows(k).Item("ConsumptionPcs").ToString

                    decDetQuantity = dsSelSODemand.Tables(0).Rows(k).Item("Quantity").ToString
                    decDetPcs = dsSelSODemand.Tables(0).Rows(k).Item("Pcs").ToString

                    decDetIndentQuantity = dsSelSODemand.Tables(0).Rows(k).Item("IndentQuantity").ToString

                    If Val(decDetIndentQuantity) = 0 Then
                        If nDemIndentPcs <> 0 Then
                            decDetIndentQuantity = dsSelSODemand.Tables(0).Rows(k).Item("Quantity").ToString
                            If decDetIndentQuantity > nDemIndentPcs Then
                                decDetIndentQuantity = nDemIndentPcs
                                sDetStatus = "Adjusted from Forecast (Partly)"
                            Else
                                sDetStatus = "Adjusted from Forecast"
                            End If
                            nDemIndentPcs = nDemIndentPcs - decDetIndentQuantity
                            decDetIndentPcs = nDemIndentPcs
                        Else
                            decDetIndentQuantity = 0
                            nDemIndentPcs = nDemIndentPcs - decDetIndentQuantity
                            decDetIndentPcs = nDemIndentPcs
                            sDetStatus = "Indent to be Generated"
                        End If

                        nTmpAdjustedQuantity = nTmpAdjustedQuantity + decDetIndentQuantity

                    Else
                        decDetIndentPcs = nDemIndentPcs 'dsSelSODemand.Tables(0).Rows(k).Item("IndentPcs").ToString
                        sDetStatus = "Demand Quantity"
                    End If

                        decDetPOQuantity = dsSelSODemand.Tables(0).Rows(k).Item("POQuantity").ToString
                        If Val(decDetPOQuantity) = 0 Then
                            decDetPOQuantity = dsSelSODemand.Tables(0).Rows(k).Item("Quantity").ToString
                            nTmpPOAdjustedQuantity = nTmpPOAdjustedQuantity + decDetPOQuantity

                            nDemPOQuantityPcs = nDemPOQuantityPcs - decDetPOQuantity
                            decDetPOQuantityPcs = nDemPOQuantityPcs
                        Else
                            decDetPOQuantityPcs = nDemPOQuantityPcs ''dsSelSODemand.Tables(0).Rows(k).Item("POQuantityPcs").ToString
                        End If

                        decDetReceivedQuantity = dsSelSODemand.Tables(0).Rows(k).Item("ReceivedQuantity").ToString
                        If Val(decDetReceivedQuantity) = 0 Then
                            decDetReceivedQuantity = dsSelSODemand.Tables(0).Rows(k).Item("Quantity").ToString
                            nDemReceivedPcs = nDemReceivedPcs - decDetReceivedQuantity
                            decDetReceivedPcs = nDemReceivedPcs
                        Else
                            decDetReceivedPcs = nDemReceivedPcs ''dsSelSODemand.Tables(0).Rows(k).Item("ReceivedPcs").ToString
                        End If

                        sDetDepartment = dsSelSODemand.Tables(0).Rows(k).Item("Department").ToString
                        sDetSize = dsSelSODemand.Tables(0).Rows(k).Item("Size").ToString
                        sDetMaterialSizeGroupName = dsSelSODemand.Tables(0).Rows(k).Item("MaterialSizeGroupName").ToString
                        nDetVariant = dsSelSODemand.Tables(0).Rows(k).Item("Variant").ToString
                        decDetConsumption = dsSelSODemand.Tables(0).Rows(k).Item("Consumption").ToString
                        sDetComponentGroup = dsSelSODemand.Tables(0).Rows(k).Item("ComponentGroup").ToString

                        sDetParentMaterialCode = dsSelSODemand.Tables(0).Rows(k).Item("ParentMaterialCode").ToString
                        sDetForecastOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("ForecastOrderNo").ToString
                        sDetSOFMID = sID
                        sDetDemandId = dsSelSODemand.Tables(0).Rows(k).Item("Id").ToString
                        sDetVariant = dsSelSODemand.Tables(0).Rows(k).Item("Variant").ToString
                        sDetCommitmentOrderNo = dsSelSODemand.Tables(0).Rows(k).Item("CommitmentOrderNo").ToString
                        nDetConsumption = dsSelSODemand.Tables(0).Rows(k).Item("Consumption").ToString
                        dDetProcessOrderQuantity = dsSelSODemand.Tables(0).Rows(k).Item("ProcessOrderQuantity").ToString

                        ''''''
                        sCnn.Close()
                        Dim sCmd301 As New SqlCommand
                        Dim daInsFCDemandAdj As New SqlDataAdapter
                        Dim dsInsFCDemandAdj As New DataSet

                        sCmd301.Connection = sCnn
                        sCmd301.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
                        sCmd301.CommandType = CommandType.StoredProcedure

                        sCmd301.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTDWAD"

                        sCmd301.Parameters.Add(New SqlParameter("@mDWADID", SqlDbType.VarChar)).Value() = sDetID
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADShipper", SqlDbType.VarChar)).Value() = sDetShipper
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADOrderNo", SqlDbType.VarChar)).Value() = sDetOrderNo
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADSalesOrderNo", SqlDbType.VarChar)).Value() = sDetSalesOrderNo
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADBuyerCode", SqlDbType.VarChar)).Value() = sDetBuyerCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADBuyerGroupCode", SqlDbType.VarChar)).Value() = sDetBuyerGroupCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADBuyerOrderNo", SqlDbType.VarChar)).Value() = sDetBuyerOrderNo
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADArticleGroup", SqlDbType.VarChar)).Value() = sDetArticleGroup
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADArticle", SqlDbType.VarChar)).Value() = sDetArticle
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADLeatherCode", SqlDbType.VarChar)).Value() = sDetLeatherCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADLeatherName", SqlDbType.VarChar)).Value() = sDetLeatherName
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADColorCode", SqlDbType.VarChar)).Value() = sDetColorCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADColorName", SqlDbType.VarChar)).Value() = sDetColorName
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialCode", SqlDbType.VarChar)).Value() = sDetMaterialCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADDescription", SqlDbType.VarChar)).Value() = sDetDescription
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialTypeCode", SqlDbType.VarChar)).Value() = sDetMaterialTypeCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialCategory", SqlDbType.VarChar)).Value() = sDetMaterialCategory
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialColor", SqlDbType.VarChar)).Value() = sDetMaterialColor
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialSize", SqlDbType.VarChar)).Value() = sDetMaterialSize
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialSizeGroup", SqlDbType.VarChar)).Value() = sDetMaterialSizeGroup
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADUnit", SqlDbType.VarChar)).Value() = sDetUnit
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADConsumptionQuantity", SqlDbType.Decimal)).Value() = decDetConsumptionQuantity
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADConsumptionPcs", SqlDbType.Decimal)).Value() = decDetConsumptionPcs
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADQuantity", SqlDbType.Decimal)).Value() = decDetQuantity
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADPcs", SqlDbType.Decimal)).Value() = decDetPcs
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADCreatedBy", SqlDbType.VarChar)).Value() = sDetCreatedBy
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADCreatedDate", SqlDbType.DateTime)).Value() = dDetCreatedDate
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADModifiedBy", SqlDbType.VarChar)).Value() = sDetModifiedBy
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADModifiedDate", SqlDbType.DateTime)).Value() = dDetModifiedDate
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADEnteredOnMachineID", SqlDbType.VarChar)).Value() = sDetEnteredonMachineID
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADIsApproved", SqlDbType.Bit)).Value() = nDetIsApproved
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADApprovedBy", SqlDbType.VarChar)).Value() = sDetApprovedBy
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADApprovedOn", SqlDbType.DateTime)).Value() = Date.Now
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADModuleName", SqlDbType.VarChar)).Value() = sDetModuleName
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADIndentQuantity", SqlDbType.Decimal)).Value() = decDetIndentQuantity
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADIndentPcs", SqlDbType.Decimal)).Value() = decDetIndentPcs
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADPOQuantity", SqlDbType.Decimal)).Value() = decDetPOQuantity
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADPOQuantityPcs", SqlDbType.Decimal)).Value() = decDetPOQuantityPcs
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADReceivedQuantity", SqlDbType.Decimal)).Value() = decDetReceivedQuantity
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADReceivedPcs", SqlDbType.Decimal)).Value() = decDetReceivedPcs
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADDepartment", SqlDbType.VarChar)).Value() = sDetDepartment
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADSize", SqlDbType.VarChar)).Value() = sDetSize
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADMaterialSizeGroupName", SqlDbType.VarChar)).Value() = sDetMaterialSizeGroupName
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADVariant", SqlDbType.VarChar)).Value() = sDetVariant
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADConsumption", SqlDbType.Decimal)).Value() = nDetConsumption
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADComponentGroup", SqlDbType.VarChar)).Value() = sDetComponentGroup
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADStatus", SqlDbType.VarChar)).Value() = sDetStatus
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADParentMaterialCode", SqlDbType.VarChar)).Value() = sOrderNo 'sDetParentMaterialCode
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADForecastOrderNo", SqlDbType.VarChar)).Value() = sDetForecastOrderNo
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADCommitmentOrderNo", SqlDbType.VarChar)).Value() = sDetCommitmentOrderNo
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADExeVersionNo", SqlDbType.VarChar)).Value() = sDetExeVersionNo
                        sCmd301.Parameters.Add(New SqlParameter("@mDWADProcessOrderQuantity", SqlDbType.Decimal)).Value() = dDetProcessOrderQuantity
                        ''sCmd301.Parameters.Add(New SqlParameter("@mDWADSlNo", SqlDbType.Int)).Value() = ""
                        ''''''

                        sCnn.Open()

                        Dim sRes1 As String = sCmd301.ExecuteScalar

                        If Val(sRes1) = 0 Then
                            sCnn.Close()
                        Else
                            'setError(Val(sRes))
                        End If
                        sCnn.Close()
                Next

                
            Next
            Dim sCmd4 As New SqlCommand

            sCmd4.Connection = sCnn
            sCmd4.CommandText = "KHLI_Forecast2SalesOrderAdjustments"
            sCmd4.CommandType = CommandType.StoredProcedure

            sCmd4.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDD4TMP"
            sCmd4.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.VarChar)).Value() = nTmpID


            nTmpBal2AdjustQuantity = nTmpIndentQuantity - nTmpAdjustedQuantity

            sCmd4.Parameters.Add(New SqlParameter("@mAdjustedQuantity", SqlDbType.VarChar)).Value() = nTmpAdjustedQuantity
            sCmd4.Parameters.Add(New SqlParameter("@mBal2AdjustQuantity", SqlDbType.VarChar)).Value() = nTmpBal2AdjustQuantity

            nTmpPOBal2AdjustQuantity = nTmpPOQuantity - nTmpPOAdjustedQuantity

            sCmd4.Parameters.Add(New SqlParameter("@mPOAdjustedQuantity", SqlDbType.VarChar)).Value() = nTmpPOAdjustedQuantity
            sCmd4.Parameters.Add(New SqlParameter("@mPOBal2AdjustQuantity", SqlDbType.VarChar)).Value() = nTmpPOBal2AdjustQuantity


            sCnn.Open()

            Dim sRes4 As String = sCmd4.ExecuteScalar

            If Val(sRes4) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()

            nTmpBal2AdjustQuantity = 0
            nTmpIndentQuantity = 0
            nTmpAdjustedQuantity = 0

            nTmpPOBal2AdjustQuantity = 0
            nTmpPOQuantity = 0
            nTmpPOAdjustedQuantity = 0

        Next
        MsgBox("Completed")
        ''To Delete  after completing this code''
        
        ''To Delete  after completing this code''
    End Function

    Private Function InsertSalesOrderForeCastMapping() As Boolean

        Dim sCmd As New SqlCommand

        Dim daInsSOM As New SqlDataAdapter
        Dim dsInsSOM As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTHDR"
        sCmd.Parameters.Add(New SqlParameter("@mHdrID", SqlDbType.VarChar)).Value() = sID
        sCmd.Parameters.Add(New SqlParameter("@mHdrCreatedBy", SqlDbType.VarChar)).Value() = sCreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mHdrCreatedDate", SqlDbType.DateTime)).Value() = dCreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mHdrModifiedBy", SqlDbType.VarChar)).Value() = sModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mHdrModifiedDate", SqlDbType.DateTime)).Value() = dModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mHdrExeVersionNo", SqlDbType.VarChar)).Value() = sExeVersionNo
        sCmd.Parameters.Add(New SqlParameter("@mHdrIsApproved", SqlDbType.Bit)).Value() = bIsApproved
        sCmd.Parameters.Add(New SqlParameter("@mHdrApprovedBy", SqlDbType.VarChar)).Value() = sApprovedBy
        sCmd.Parameters.Add(New SqlParameter("@mHdrApprovedOn", SqlDbType.DateTime)).Value() = dApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mHdrModuleName", SqlDbType.VarChar)).Value() = sModuleName
        sCmd.Parameters.Add(New SqlParameter("@mHdrSalesOrderId", SqlDbType.VarChar)).Value() = sSalesOrderId
        sCmd.Parameters.Add(New SqlParameter("@mHdrSalesOrderNo", SqlDbType.VarChar)).Value() = sSalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mHdrBuyerBuy", SqlDbType.VarChar)).Value() = sBuyerBuy
        sCmd.Parameters.Add(New SqlParameter("@mHdrBuyerOrderNo", SqlDbType.VarChar)).Value() = sBuyerOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mHdrBuyerGroupCode", SqlDbType.VarChar)).Value() = sBuyerGroupCode
        sCmd.Parameters.Add(New SqlParameter("@mHdrDestination", SqlDbType.VarChar)).Value() = sDestination
        sCmd.Parameters.Add(New SqlParameter("@mHdrOrderRecivedDate", SqlDbType.Date)).Value() = dOrderRecivedDate
        sCmd.Parameters.Add(New SqlParameter("@mHdrOrderConfirmedDate", SqlDbType.Date)).Value() = dOrderConfirmedDate
        sCmd.Parameters.Add(New SqlParameter("@mHdrOrderQuality", SqlDbType.VarChar)).Value() = sOrderQuality
        sCmd.Parameters.Add(New SqlParameter("@mHdrOrderStatus", SqlDbType.VarChar)).Value() = sOrderStatus
        sCmd.Parameters.Add(New SqlParameter("@mHdrSeason", SqlDbType.VarChar)).Value() = sSeason
        sCmd.Parameters.Add(New SqlParameter("@mHdrShipper", SqlDbType.VarChar)).Value() = sShipper
        sCmd.Parameters.Add(New SqlParameter("@mHdrArticleGroup", SqlDbType.VarChar)).Value() = sArticleGroup
        sCmd.Parameters.Add(New SqlParameter("@mHdrUnit", SqlDbType.VarChar)).Value() = sUnit
        sCmd.Parameters.Add(New SqlParameter("@mHdrCurrency", SqlDbType.VarChar)).Value() = sCurrency
        sCmd.Parameters.Add(New SqlParameter("@mHdrCurrencyConversion", SqlDbType.Decimal)).Value() = decCurrencyConversion
        sCmd.Parameters.Add(New SqlParameter("@mHdrIsAssortedOrder", SqlDbType.VarChar)).Value() = sIsAssortedOrder
        sCmd.Parameters.Add(New SqlParameter("@mHdrTotalOrderQuantity", SqlDbType.Decimal)).Value() = decTotalOrderQuantity
        sCmd.Parameters.Add(New SqlParameter("@mHdrUserCategory", SqlDbType.VarChar)).Value() = sUserCategory
        sCmd.Parameters.Add(New SqlParameter("@mHdrSalesOrderDate", SqlDbType.Date)).Value() = dSalesOrderDate
        sCmd.Parameters.Add(New SqlParameter("@mHdrBuyerOrderType", SqlDbType.VarChar)).Value() = sBuyerOrderType
        sCmd.Parameters.Add(New SqlParameter("@mHdrOrderNo", SqlDbType.VarChar)).Value() = sOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mHdrSizeName", SqlDbType.VarChar)).Value() = sSizeName
        sCmd.Parameters.Add(New SqlParameter("@mHdrPortOfDischarge", SqlDbType.VarChar)).Value() = sPortOfDischarge
        sCmd.Parameters.Add(New SqlParameter("@mHdrInternalSalesOrderNo", SqlDbType.VarChar)).Value() = sInternalSalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mHdrInternalBuyer", SqlDbType.VarChar)).Value() = sInternalBuyer
        sCmd.Parameters.Add(New SqlParameter("@mHdrType", SqlDbType.VarChar)).Value() = sType
        sCmd.Parameters.Add(New SqlParameter("@mHdrBuyerOrderID", SqlDbType.VarChar)).Value() = sBuyerOrderID


        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            'setError(Val(sRes))
        End If
        sCnn.Close()



    End Function

    Private Function InsertSalesOrderForeCastMappingDetails() As Boolean

        Dim sCmd As New SqlCommand

        Dim daInsSOM As New SqlDataAdapter
        Dim dsInsSOM As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_SalesOrder2ForeCastMapping"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERTDTL"
        sCmd.Parameters.Add(New SqlParameter("@mDtlID", SqlDbType.VarChar)).Value() = sDetID
        sCmd.Parameters.Add(New SqlParameter("@mDtlCreatedBy", SqlDbType.VarChar)).Value() = sDetCreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mDtlCreatedDate", SqlDbType.DateTime)).Value() = dDetCreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mDtlModifiedBy", SqlDbType.VarChar)).Value() = sDetModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mDtlModifiedDate", SqlDbType.DateTime)).Value() = dDetModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mDtlExeVersionNo", SqlDbType.VarChar)).Value() = sDetExeVersionNo
        sCmd.Parameters.Add(New SqlParameter("@mDtlIsApproved", SqlDbType.Bit)).Value() = bDetIsApproved
        sCmd.Parameters.Add(New SqlParameter("@mDtlApprovedBy", SqlDbType.VarChar)).Value() = sDetApprovedBy
        'sCmd.Parameters.Add(New SqlParameter("@mDtlApprovedOn", SqlDbType.DateTime)).Value() = dDetApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mDtlModuleName", SqlDbType.VarChar)).Value() = sDetModuleName
        sCmd.Parameters.Add(New SqlParameter("@mDtlShipper", SqlDbType.VarChar)).Value() = sDetShipper
        sCmd.Parameters.Add(New SqlParameter("@mDtlOrderNo", SqlDbType.VarChar)).Value() = sDetOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mDtlSalesOrderNo", SqlDbType.VarChar)).Value() = sDetSalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mDtlBuyerGroupCode", SqlDbType.VarChar)).Value() = sDetBuyerGroupCode
        sCmd.Parameters.Add(New SqlParameter("@mDtlBuyerOrderNo", SqlDbType.VarChar)).Value() = sDetBuyerOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mDtlArticleGroup", SqlDbType.VarChar)).Value() = sDetArticleGroup
        sCmd.Parameters.Add(New SqlParameter("@mDtlArticle", SqlDbType.VarChar)).Value() = sDetArticle
        sCmd.Parameters.Add(New SqlParameter("@mDtlLeatherCode", SqlDbType.VarChar)).Value() = sDetLeatherCode
        sCmd.Parameters.Add(New SqlParameter("@mDtlLeatherName", SqlDbType.VarChar)).Value() = sDetLeatherName
        sCmd.Parameters.Add(New SqlParameter("@mDtlColorCode", SqlDbType.VarChar)).Value() = sDetColorCode
        sCmd.Parameters.Add(New SqlParameter("@mDtlColorName", SqlDbType.VarChar)).Value() = sDetColorName
        sCmd.Parameters.Add(New SqlParameter("@mDtlMaterialCode", SqlDbType.VarChar)).Value() = sDetMaterialCode
        sCmd.Parameters.Add(New SqlParameter("@mDtlDescription", SqlDbType.VarChar)).Value() = sDetDescription
        sCmd.Parameters.Add(New SqlParameter("@mDtlMaterialTypeCode", SqlDbType.VarChar)).Value() = sDetMaterialTypeCode
        sCmd.Parameters.Add(New SqlParameter("@mDtlMaterialColor", SqlDbType.VarChar)).Value() = sDetMaterialColor
        sCmd.Parameters.Add(New SqlParameter("@mDtlMaterialSize", SqlDbType.VarChar)).Value() = sDetMaterialSize
        sCmd.Parameters.Add(New SqlParameter("@mDtlMaterialSizeGroup", SqlDbType.VarChar)).Value() = sDetMaterialSizeGroup
        sCmd.Parameters.Add(New SqlParameter("@mDtlUnit", SqlDbType.VarChar)).Value() = sDetUnit
        sCmd.Parameters.Add(New SqlParameter("@mDtlConsumptionQuantity", SqlDbType.Decimal)).Value() = decDetConsumptionQuantity
        sCmd.Parameters.Add(New SqlParameter("@mDtlQuantity", SqlDbType.Decimal)).Value() = decDetQuantity
        sCmd.Parameters.Add(New SqlParameter("@mDtlIndentQuantity", SqlDbType.Decimal)).Value() = 0 ''decDetIndentQuantity
        sCmd.Parameters.Add(New SqlParameter("@mDtlIndentPcs", SqlDbType.Decimal)).Value() = decDetIndentPcs
        sCmd.Parameters.Add(New SqlParameter("@mDtlPOQuantity", SqlDbType.Decimal)).Value() = decDetPOQuantity
        sCmd.Parameters.Add(New SqlParameter("@mDtlPOQuantityPcs", SqlDbType.Decimal)).Value() = decDetPOQuantityPcs
        sCmd.Parameters.Add(New SqlParameter("@mDtlReceivedQuantity", SqlDbType.Decimal)).Value() = decDetReceivedQuantity
        sCmd.Parameters.Add(New SqlParameter("@mDtlReceivedPcs", SqlDbType.Decimal)).Value() = decDetReceivedPcs
        sCmd.Parameters.Add(New SqlParameter("@mDtlDepartment", SqlDbType.VarChar)).Value() = sDetDepartment
        sCmd.Parameters.Add(New SqlParameter("@mDtlSize", SqlDbType.VarChar)).Value() = sDetSize
        sCmd.Parameters.Add(New SqlParameter("@mDtlMaterialSizeGroupName", SqlDbType.VarChar)).Value() = sDetMaterialSizeGroupName
        sCmd.Parameters.Add(New SqlParameter("@mDtlVariant", SqlDbType.Int)).Value() = nDetVariant
        sCmd.Parameters.Add(New SqlParameter("@mDtlConsumption", SqlDbType.Decimal)).Value() = decDetConsumption
        sCmd.Parameters.Add(New SqlParameter("@mDtlComponentGroup", SqlDbType.VarChar)).Value() = sDetComponentGroup
        sCmd.Parameters.Add(New SqlParameter("@mDtlStatus", SqlDbType.VarChar)).Value() = sDetStatus
        sCmd.Parameters.Add(New SqlParameter("@mDtlParentMaterialCode", SqlDbType.VarChar)).Value() = sDetParentMaterialCode
        sCmd.Parameters.Add(New SqlParameter("@mDtlForecastOrderNo", SqlDbType.VarChar)).Value() = sDetForecastOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mDtlSOFMID", SqlDbType.VarChar)).Value() = sDetSOFMID
        sCmd.Parameters.Add(New SqlParameter("@mDtlDemandId", SqlDbType.VarChar)).Value() = sDetDemandId
        sCmd.Parameters.Add(New SqlParameter("@mArrivedIndentQuantity", SqlDbType.Decimal)).Value() = decDetIndentQuantity

        'sCnn.Close()

        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            'setError(Val(sRes))
        End If
        sCnn.Close()



    End Function
#End Region

End Class
