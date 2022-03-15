Option Explicit On
Imports System.Data.SqlClient
Public Class xtrafrmforStatusAgainstSalesOrder

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sSalesOrderNo, sBuyerGroupcode, sBuyerOrderNo, sMaterialCode, sDescription, sMaterialTypeCode, sSize, sUnit As String
    Dim nQuantity, nConsumptionQuantity, nCostPrice, nCostvalue, nPOQuantity, nPOValue, nInwQuantity, nInwValue As Decimal
    Dim nOutwQuantity, nOutwValue As Decimal


    Private Sub cbLoadDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLoadDetails.Click

        Dim daSelDemandInfo As New SqlDataAdapter("Select SalesOrderNo,BuyerGroupcode,BuyerOrderNo,MaterialCode,Description,MaterialTypeCode,Size,Unit,Sum(Quantity) As Quantity,Sum(ConsumptionQuantity) As ConsumptionQuantity  From Demand Where SalesOrderNo = 'S-F-SS17-045-08' Group By SalesOrderNo,BuyerGroupcode,BuyerOrderNo,MaterialCode,Description,MaterialTypeCode,Size,Unit", sCnn)
        Dim dsSelDemandInfo As New DataSet
        daSelDemandInfo.Fill(dsSelDemandInfo)

        Dim i As Integer = 0

        MsgBox(dsSelDemandInfo.Tables(0).Rows.Count)

        For i = 0 To dsSelDemandInfo.Tables(0).Rows.Count - 1
            sSalesOrderNo = dsSelDemandInfo.Tables(0).Rows(i).Item("SalesOrderNo").ToString
            sBuyerGroupcode = dsSelDemandInfo.Tables(0).Rows(i).Item("BuyerGroupcode").ToString
            sBuyerOrderNo = dsSelDemandInfo.Tables(0).Rows(i).Item("BuyerOrderNo").ToString
            sMaterialCode = dsSelDemandInfo.Tables(0).Rows(i).Item("MaterialCode").ToString
            sDescription = dsSelDemandInfo.Tables(0).Rows(i).Item("Description").ToString
            sMaterialTypeCode = dsSelDemandInfo.Tables(0).Rows(i).Item("MaterialTypeCode").ToString
            sSize = dsSelDemandInfo.Tables(0).Rows(i).Item("Size").ToString
            sUnit = dsSelDemandInfo.Tables(0).Rows(i).Item("Unit").ToString
            nQuantity = Val(dsSelDemandInfo.Tables(0).Rows(i).Item("Quantity").ToString)
            nConsumptionQuantity = Val(dsSelDemandInfo.Tables(0).Rows(i).Item("ConsumptionQuantity").ToString)

            ''Costing Price of the Materials''
            Dim daselCostPrice As New SqlDataAdapter("Select * from MaterialCostingRate Where MaterialCode = '" & sMaterialCode & _
                                                     "'", sCnn)
            Dim dsSelCostPrice As New DataSet
            daselCostPrice.Fill(dsSelCostPrice)

            If dsSelCostPrice.Tables(0).Rows.Count = 0 Then
                nCostPrice = 0
            Else
                nCostPrice = Val(dsSelCostPrice.Tables(0).Rows(0).Item("Rate").ToString)
            End If

            nCostvalue = Math.Round((nConsumptionQuantity * nCostPrice), 2)
            ''Costing Price of the Materials''

            ''PO Quantity & Value''
            Dim daSelPoInfo As New SqlDataAdapter("Select Materialcode,Price,IsNull(Sum(Quantity),0) As POQuantity,IsNull(Sum(MaterialValue),0) AS POValue   from PurchaseOrderDetails Where SalesOrderNo = ' " & sSalesOrderNo & _
                                                  "' And MaterialCode = '" & sMaterialCode & _
                                                  "'  Group By MaterialCode,Price", sCnn)
            Dim dsSelPoInfo As New DataSet
            daSelPoInfo.Fill(dsSelPoInfo)

            nPOQuantity = dsSelPoInfo.Tables(0).Rows(0).Item("POQuantity").ToString
            nPOValue = dsSelPoInfo.Tables(0).Rows(0).Item("POValue").ToString
            ''PO Quatntiy & Value''

            ''Inward Quantity & Value Against Sales Order''
            Dim daSelInwDtls As New SqlDataAdapter("Select Materialcode,IssuePrice,IsNull(Sum(IssueQuantity),0) As InwQuantity,IsNull(Sum(IssueValue),0) AS InwValue   from Materialissues Where SalesOrderNo = ' " & sSalesOrderNo & _
                                                   "' And MaterialCode = '" & sMaterialCode & _
                                                   "' And TransactonType = 'NEW PURCHASE' Group By MaterialCode,IssuePrice", sCnn)
            Dim dsSelInwDtls As New DataSet
            daSelInwDtls.Fill(dsSelInwDtls)

            nInwQuantity = dsSelInwDtls.Tables(0).Rows(0).Item("InwQuantity").ToString
            nInwValue = dsSelInwDtls.Tables(0).Rows(0).Item("InwValue").ToString
            ''Inward Quantity & Value Against Sales Order''

            ''Outward Quantity & Value Against Sales Order''
            Dim daSelOutwDtls As New SqlDataAdapter("Select Materialcode,IssuePrice,IsNull(Sum(IssueQuantity),0) As OutwQuantity,IsNull(Sum(IssueValue),0) AS OutwValue   from Materialissues Where SalesOrderNo = ' " & sSalesOrderNo & _
                                                   "' And MaterialCode = '" & sMaterialCode & _
                                                   "' And TransactonType in ('JOBCARD ISSUE','GENERAL ISSUE') Group By MaterialCode,IssuePrice", sCnn)
            Dim dsSelOutwDtls As New DataSet
            daSelOutwDtls.Fill(dsSelOutwDtls)

            nOutwQuantity = dsSelOutwDtls.Tables(0).Rows(0).Item("OutwQuantity").ToString
            nOutwValue = dsSelOutwDtls.Tables(0).Rows(0).Item("OutwValue").ToString
            ''Outward Quantity & Value Against Sales Order''

        Next
        MsgBox("Completed")

    End Sub
End Class