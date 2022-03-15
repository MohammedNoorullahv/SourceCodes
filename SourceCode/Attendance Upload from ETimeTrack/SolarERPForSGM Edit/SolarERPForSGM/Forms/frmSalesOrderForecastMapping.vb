Option Explicit On
Imports System.Data.SqlClient

Public Class frmSalesOrderForecastMapping

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccSalesOrderForecastMapping As New ccSalesOrderForecastMapping


    ''MaterialIssues4Adjustments Fields
    Dim da003IssueDate, da0022PurchaseOrderDate, da0030CreatedDate, da0032ModifiedDate, da0041SupplierRefDate, da0042SupplierBillDate As Date
    Dim da0045PaidOn, da0059ApprovedOn, da0084Ack_Date, da0086ReceivedDateTime, da0091DCDate As Date

    Dim de006IssueQuantity, de0011IssuePcs, de0012IssuePrice, de0013IssueValue, de0070ChallanQuantity, de0071ChallanPcs As Decimal
    Dim de0099ApprovedQty, de00100ApprovedPcs, de00105LandedPrice, de00107POPrice, de00108ExchangeRate, de00109PercentageonValue As Decimal

    Dim in0057IsApproved, in0076NoOfBundles, in0077IsCustomerSpecific, in0094IsInspectionDone As Integer

    Dim st001ID, st002VoucherNo, st004RsNo, st005MaterialCode, st007PurchaseOrderNo, st008TransactionType, st009SalesOrderNo As String
    Dim st0010IssueUnits, st0014CompanyCode, st0015JobberCode, st0016FromLocation, st0017FromStage, st0018ToLocation, st0019ToStage As String
    Dim st0020FromMaterial, st0021ToMaterial, st0023SupplierCode, st0024FromColor, st0025ToColor, st0026Remarks, st0027Material As String
    Dim st0028MaterialTypeCode, st0029CreatedBy, st0031ModifiedBy, st0033EnteredOnMachineID, st0034FromLotNo, st0035ToLotNo As String
    Dim st0036Size, st0037Color, st0038WorkOrderNo, st0039SupplierRefNo, st0040SupplierBillNo, st0043MaterialColor, st0044SerialNo As String
    Dim st0046ChequeNo, st0047Variant, st0048ToVariant, st0049InspectionNumber, st0050SizeSequenceNo, st0051GroupCode, st0052FromWorkOrderNo As String
    Dim st0053Buyer, st0054POSize, st0055InspectedBy, st0056CustomerStyleNo, st0058ApprovedBy, st0060ModuleName, st0061JobCardNo As String
    Dim st0062UnitCode, st0063Origin, st0064Source, st0065Article, st0066ArticleGroup, st0067LeatherCode, st0068ColorCode As String
    Dim st0069BrandCode, st0072SkinType, st0073Substance, st0074ContainerNo, st0075WeightRange, st0078Quality, st0079Status As String
    Dim st0080BatchNo, st0081LeatherType, st0082OldFromLocation, st0083OldToLocation, st0085Ack_By, st0087Authorisedby As String
    Dim st0088ReceivedBy, st0089ConfirmedStatus, st0090ApprovedStatus, st0092Grade, st0093FromCompanyCode, st0095ComponentGroup As String
    Dim st0096RID, st0097FromSalesOrder, st0098Season, st00101ExeVersionNo, st00102OldJobCardNo, st00103OldSalesOrderNo As String
    Dim st00104ReasonCode, st00106CurrencyCode As String
    ''MaterialIssues4Adjustments Fields

    Private Sub frmSalesOrderForecastMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadSeason()
    End Sub


#Region "Tools Functions"

    Private Sub chkbxLoadCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxLoadCustomer.CheckedChanged
        If chkbxLoadCustomer.Checked = True Then
            LoadCustomer()
        End If
    End Sub

    Private Sub chkbxLoadSalesORderNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxLoadSalesORderNo.CheckedChanged
        If chkbxLoadSalesORderNo.Checked = True Then
            LoadSalesOrder()
            chkbxLoadForecastORderNo.Checked = False
        End If
    End Sub

    Private Sub cbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUpdate.Click
        If chkbxLoadSalesORderNo.Checked = True Then
            myccSalesOrderForecastMapping.UpdateDemand(cbxSalesOrder.Text)
        ElseIf chkbxLoadForecastORderNo.Checked = True Then
            myccSalesOrderForecastMapping.UpdateDemandAgainstForecastOrder(cbxForecastOrder.Text)
        End If
    End Sub

#End Region

#Region "Functions"

    Private Sub LoadSeason()
        cbxSeason.DataSource = Nothing : cbxSeason.Items.Clear()
        cbxSeason.DataSource = myccSalesOrderForecastMapping.LoadSeason
        cbxSeason.DisplayMember = "SeasonCode"
        cbxSeason.ValueMember = "SeasonCode"
    End Sub

    Private Sub LoadCustomer()
        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()
        cbxCustomer.DataSource = myccSalesOrderForecastMapping.LoadCustomer(cbxSeason.Text)
        cbxCustomer.DisplayMember = "BuyerGroupCode"
        cbxCustomer.ValueMember = "BuyerGroupCode"
    End Sub

    Private Sub LoadSalesOrder()
        cbxSalesOrder.DataSource = Nothing : cbxSalesOrder.Items.Clear()
        cbxSalesOrder.DataSource = myccSalesOrderForecastMapping.LoadSalesOrder(cbxSeason.Text, cbxCustomer.Text)
        cbxSalesOrder.DisplayMember = "OrderNo"
        cbxSalesOrder.ValueMember = "OrderNo"
    End Sub

    Private Sub LoadForecastOrder()
        cbxForecastOrder.DataSource = Nothing : cbxForecastOrder.Items.Clear()
        cbxForecastOrder.DataSource = myccSalesOrderForecastMapping.LoadForecastOrder(cbxSeason.Text, cbxCustomer.Text)
        cbxForecastOrder.DisplayMember = "ForecastOrderNo"
        cbxForecastOrder.ValueMember = "ForecastOrderNo"
    End Sub


#End Region



    Private Sub chkbxLoadForecastORderNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxLoadForecastORderNo.CheckedChanged
        If chkbxLoadForecastORderNo.Checked = True Then
            LoadForecastOrder()
            chkbxLoadSalesORderNo.Checked = False
        End If
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
    End Sub

    Dim nPKID As Integer
    Dim sMaterialCode, sSizeInfo, sSizeCode, sStatus As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim daSelAllItems As New SqlDataAdapter("Select * from tmptbl_ForStockImporting Order by PKID", sConstr)
            Dim dsSelAllItems As New DataSet
            daSelAllItems.Fill(dsSelAllItems)

            Dim i As Integer = 0

            For i = 0 To dsSelAllItems.Tables(0).Rows.Count - 1
                'MsgBox(i)

                nPKID = dsSelAllItems.Tables(0).Rows(i).Item("PKID")
                If nPKID = 139 Then
                    MsgBox(nPKID)
                End If
                sMaterialCode = dsSelAllItems.Tables(0).Rows(i).Item("MaterialCode")
                If sMaterialCode = "" Then
                    sStatus = "Material Code Not Available"
                    sSizeInfo = ""
                    GoTo Aa
                End If
                sSizeInfo = dsSelAllItems.Tables(0).Rows(i).Item("Size")
                If sSizeInfo = "" Then
                    GoTo Ab
                End If
                Dim j As Integer = 1

                For j = 1 To Len(Microsoft.VisualBasic.Replace(sSizeInfo, " ", ""))
                    If Val(Microsoft.VisualBasic.Mid(Microsoft.VisualBasic.Replace(sSizeInfo, " ", ""), j, 1)) = 0 Then
                        GoTo Ab
                    End If
                Next
                GoTo Ac
                If sSizeInfo = 0 Or Val(sSizeInfo) > 46 Then
Ab:
                    sSizeInfo = dsSelAllItems.Tables(0).Rows(i).Item("Size")
                    If sSizeInfo = "" Then
                        sSizeCode = "P5"
                        sStatus = "MCA - With Size As NA"
                    Else
                        Dim daSelSizeCode As New SqlDataAdapter("Select * from AbbrevTable Where FullName_ = '" & sSizeInfo & _
                                                                "' And Group_ = 'MATERIALSIZE'", sConstr)
                        Dim dsSelSizeCode As New DataSet
                        daSelSizeCode.Fill(dsSelSizeCode)

                        If dsSelSizeCode.Tables(0).Rows.Count = 0 Then
                            sStatus = "MCA - Without Sizes Info"
                        ElseIf dsSelSizeCode.Tables(0).Rows.Count = 1 Then
                            sSizeCode = dsSelSizeCode.Tables(0).Rows(0).Item("Abbrev_")
                            sStatus = "Updated"
                        Else
                            sStatus = "MCA - With Multiple Sizes (String)"
                        End If

                    End If
                ElseIf sSizeInfo <> 0 Then
Ac:
                    Dim daSelSizeCount As New SqlDataAdapter("Select Distinct(MaterialSize) From Stock Where MaterialCode = '" & sMaterialCode & _
                                                         "' And MaterialSize Is Not Null", sConstr)
                    Dim dsSelSizeCount As New DataSet
                    daSelSizeCount.Fill(dsSelSizeCount)

                    If dsSelSizeCount.Tables(0).Rows.Count = 0 Then
                        sStatus = "MCA - Without Sizes Info"
                    ElseIf dsSelSizeCount.Tables(0).Rows.Count = 1 Then
                        If Val(sSizeInfo) <> Val(dsSelSizeCount.Tables(0).Rows(1).Item("MaterialSize")) Then
                            sStatus = "MCA - With New Size Info"
                        End If
                    ElseIf dsSelSizeCount.Tables(0).Rows.Count > 1 Then

                        sStatus = "MCA - With Multiple Sizes (Numeric)"

                    End If
                End If

Aa:
                Dim daUpdtmptbl As New SqlDataAdapter("Update tmptbl_ForStockImporting Set SizeCodeAndInfo = '" & sSizeCode & _
                                                      "', Status = '" & sStatus & _
                                                      "' Where PKID = '" & nPKID & "'", sConstr)
                Dim dsUpdtmptbl As New DataSet
                daUpdtmptbl.Fill(dsUpdtmptbl)
                dsUpdtmptbl.AcceptChanges()

                sSizeCode = ""
                sStatus = ""
            Next
            MsgBox("Completed")
        Catch ex As Exception
            'HandleException(Me.Name, Expressions)
        End Try
    End Sub

    Dim sSupplierCode, sUnitCode, sStoreCode, sUom As String
    Dim nOpeningStock, nArrival, nIssue, nClosingStock, nArrivedClosingStock, nStockDifference, nRate, nValue As Decimal
    Dim nCurrentStock As Decimal
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTmp2MaterislIssues4Adjustments.Click

        Dim nTimes As Integer = 1



        For nTimes = 1 To 2
            Dim dsSelAllItems As New DataSet
            If nTimes = 1 Then
                Dim daSelAllItems As New SqlDataAdapter("Select Distinct UnitCode,StoreCode,MaterialCode,SupplierCode,Size from tmptbl_ForStockImporting Where StoreCode in ('KWS','SWS') order by UnitCode,StoreCode,MaterialCode,Size", sConstr)
                daSelAllItems.Fill(dsSelAllItems)
            Else
                Dim daSelAllItems As New SqlDataAdapter("Select Distinct Companycode As UnitCode,Location,MaterialCode,SupplierCode,Size from StockByMonth As sbm Where StockDate >= '2015-02-28' And StockDate <= '2015-03-01' And Stage = 'INSTK' And Location in ('KFS','SFS') And sbm.MaterialCode Not in (Select MaterialCode From tmptbl_ForStockImporting Where MaterialCode = sbm.MaterialCode And SupplierCode = sbm.SupplierCode And StoreCode = sbm.Location)order by UnitCode,Location,MaterialCode", sConstr)
                daSelAllItems.Fill(dsSelAllItems)
            End If

            'Dim daSelAllItems As New SqlDataAdapter("Select Distinct Companycode As UnitCode,Location,MaterialCode,SupplierCode from StockByMonth Where StockDate >= '2015-02-28' And StockDate <= '2015-03-01' And Stage = 'INSTK' And Location = 'KFS'Union Select Distinct UnitCode,StoreCode,MaterialCode,SupplierCode from tmptbl_ForStockImporting order by UnitCode,Location,MaterialCode", sConstr)
            'daSelAllItems.Fill(dsSelAllItems)

            Dim j As Integer = 0

            For j = 0 To dsSelAllItems.Tables(0).Rows.Count - 1
                sSupplierCode = dsSelAllItems.Tables(0).Rows(j).Item("SupplierCode")
                sUnitCode = dsSelAllItems.Tables(0).Rows(j).Item("UnitCode")
                sStoreCode = dsSelAllItems.Tables(0).Rows(j).Item("StoreCode")
                sMaterialCode = dsSelAllItems.Tables(0).Rows(j).Item("MaterialCode")
                sSizeCode = dsSelAllItems.Tables(0).Rows(j).Item("Size")
                If sMaterialCode = "PAC-TAP-XX-0001" Then
                    MsgBox(sMaterialCode)
                End If

                If sSupplierCode = "" Or sMaterialCode = "" Then
                    sStatus = "Not Updated"
                    GoTo Aa
                End If

                Dim daMarOpnStk As New SqlDataAdapter("Select IsNull(Sum(qUANTITY),'0') As OpeningStock  from StockByMonth Where Companycode = '" & sUnitCode & _
                                                      "' And Location = '" & sStoreCode & _
                                                      "' And MaterialCode = '" & sMaterialCode & _
                                                      "' And SupplierCode = '" & sSupplierCode & _
                                                      "' And MaterialSize = '" & sSizeCode & _
                                                      "' And StockDate >= '2015-02-28' And StockDate <= '2015-03-01' And Stage = 'INSTK'", sConstr)
                Dim dsMarOpnStk As New DataSet
                daMarOpnStk.Fill(dsMarOpnStk)

                LoadPhysicalStock()

                ''Opening Stock''
                nOpeningStock = dsMarOpnStk.Tables(0).Rows(0).Item("OpeningStock")
                ''Opening Stock''

                ''Arrival''
                Dim daInwQuantity As New SqlDataAdapter("Select IsNull(SUM(IssueQuantity),0) As ArrivalQuantity  from Materialissues Where Companycode = '" & sUnitCode & _
                                                        "' And MaterialCode = '" & sMaterialCode & _
                                                        "' And SupplierCode = '" & sSupplierCode & _
                                                        "' And ToLocation = '" & sStoreCode & _
                                                        "' And Size = '" & sSizeCode & _
                                                        "' And IssueDate >= '2015-03-01' And IssueDate <= '2015-03-31' And TransactionType in ('OPENINGSTOCK','MaterialInspected','JobCardReturn','','MaterialTransfer','ProcessIssue') And FromLocation <> ToLocation", sConstr)
                Dim dsInwQuantity As New DataSet
                daInwQuantity.Fill(dsInwQuantity)

                nArrival = dsInwQuantity.Tables(0).Rows(0).Item("ArrivalQuantity")
                ''Arrival''

                ''Issue''
                Dim daOutQuantity As New SqlDataAdapter("Select IsNull(SUM(IssueQuantity),0) As IssueQuantity  from Materialissues Where Companycode = '" & sUnitCode & _
                                                        "' And MaterialCode = '" & sMaterialCode & _
                                                        "' And SupplierCode = '" & sSupplierCode & _
                                                        "' And FromLocation = '" & sStoreCode & _
                                                        "' And Size = '" & sSizeCode & _
                                                        "' And IssueDate >= '2015-03-01' And IssueDate <= '2015-03-31' And TransactionType in ('General Material Issue','JobCardIssue','RETURNTOSUPPLIER','MaterialTransfer','ProcessIssue') And FromLocation <> ToLocation", sConstr)
                Dim dsOutQuantity As New DataSet
                daOutQuantity.Fill(dsOutQuantity)

                nIssue = dsOutQuantity.Tables(0).Rows(0).Item("IssueQuantity")
                ''Issue''

                ''Closing Stock''
                nArrivedClosingStock = nOpeningStock + nArrival - nIssue
                ''Closing Stock''

                ''Adjustment Quantity''
                nStockDifference = nClosingStock - nArrivedClosingStock

                If nStockDifference < 0 Then
                    st008TransactionType = "Adjustment Out"
                    nStockDifference = nStockDifference * -1

                    ''Issue''
                    Dim daSelStockQty As New SqlDataAdapter("Select IsNull(SUM(Quantity),0) As StockQuantity  from Stock Where Companycode = '" & sUnitCode & _
                                                            "' And MaterialCode = '" & sMaterialCode & _
                                                            "' And SupplierCode = '" & sSupplierCode & _
                                                            "' And Location = '" & sStoreCode & _
                                                            "' And MaterialSize = '" & sSizeCode & _
                                                            "' And Stage = 'INSTK'", sConstr)
                    Dim dsSelStockQty As New DataSet
                    daSelStockQty.Fill(dsSelStockQty)

                    nCurrentStock = dsSelStockQty.Tables(0).Rows(0).Item("StockQuantity")

                    If nCurrentStock < nStockDifference Then
                        nStockDifference = 0
                    End If
                    ''Issue''

                Else
                    st008TransactionType = "Adjustment In"
                End If
                ''Adjustment Quantity''


                st0036Size = sSizeCode

                Dim dsSelLastTran As New DataSet

                If st008TransactionType = "Adjustment Out" Then
                    st0016FromLocation = sStoreCode
                    st0018ToLocation = "KFD"
                    st0019ToStage = "WIP"

                    Dim daSelLastTran1 As New SqlDataAdapter("Select Top 1 *  from Materialissues Where Companycode = '" & sUnitCode & _
                                                          "' And MaterialCode = '" & sMaterialCode & _
                                                          "' And SupplierCode = '" & sSupplierCode & _
                                                          "' And FromLocation = '" & sStoreCode & _
                                                          "' And TransactionType in ('General Material Issue','JobCardIssue','RETURNTOSUPPLIER','MaterialTransfer','ProcessIssue') And FromLocation <> ToLocation Order By Createddate desc", sConstr)
                    Dim dsSelLastTran1 As New DataSet
                    daSelLastTran1.Fill(dsSelLastTran1)

                    If dsSelLastTran1.Tables(0).Rows.Count = 1 Then
                        Dim daSelLastTran As New SqlDataAdapter("Select Top 1 *  from Materialissues Where Companycode = '" & sUnitCode & _
                                                            "' And MaterialCode = '" & sMaterialCode & _
                                                            "' And SupplierCode = '" & sSupplierCode & _
                                                            "' And FromLocation = '" & sStoreCode & _
                                                            "' And TransactionType in ('General Material Issue','JobCardIssue','RETURNTOSUPPLIER','MaterialTransfer','ProcessIssue') And FromLocation <> ToLocation Order By Createddate desc", sConstr)
                        daSelLastTran.Fill(dsSelLastTran)
                    Else
                        Dim daSelLastTran As New SqlDataAdapter("Select Top 1 *  from Materialissues Where Companycode = '" & sUnitCode & _
                                                            "' And MaterialTypeCode = '" & Microsoft.VisualBasic.Left(sMaterialCode, 10) & _
                                                            "' Order By Createddate desc", sConstr)
                        daSelLastTran.Fill(dsSelLastTran)
                    End If


                ElseIf st008TransactionType = "Adjustment In" Then
                    st0016FromLocation = sStoreCode
                    st0018ToLocation = sStoreCode
                    st0019ToStage = "INSTK"

                    Dim daSelLastTran2 As New SqlDataAdapter("Select Top 1 *  from Materialissues Where Companycode = '" & sUnitCode & _
                                                         "' And MaterialCode = '" & sMaterialCode & _
                                                         "' And SupplierCode = '" & sSupplierCode & _
                                                         "' And ToLocation = '" & sStoreCode & _
                                                         "' And TransactionType in ('OPENINGSTOCK','MaterialInspected','JobCardReturn','','MaterialTransfer','ProcessIssue') And FromLocation <> ToLocation Order By Createddate desc", sConstr)
                    Dim dsSelLastTran2 As New DataSet
                    daSelLastTran2.Fill(dsSelLastTran2)

                    If dsSelLastTran2.Tables(0).Rows.Count = 1 Then
                        Dim daSelLastTran As New SqlDataAdapter("Select Top 1 *  from Materialissues Where Companycode = '" & sUnitCode & _
                                                           "' And MaterialCode = '" & sMaterialCode & _
                                                           "' And SupplierCode = '" & sSupplierCode & _
                                                           "' And ToLocation = '" & sStoreCode & _
                                                           "' And TransactionType in ('OPENINGSTOCK','MaterialInspected','JobCardReturn','','MaterialTransfer','ProcessIssue') And FromLocation <> ToLocation Order By Createddate desc", sConstr)
                        daSelLastTran.Fill(dsSelLastTran)
                    Else
                        Dim daSelLastTran As New SqlDataAdapter("Select Top 1 *  from Materialissues Where Companycode = '" & sUnitCode & _
                                                           "' And MaterialTypeCode = '" & Microsoft.VisualBasic.Left(sMaterialCode, 10) & _
                                                           "' Order By Createddate desc", sConstr)
                        daSelLastTran.Fill(dsSelLastTran)
                    End If

                End If

                If dsSelLastTran.Tables(0).Rows.Count = 0 Then
                    sStatus = "Not Previous Transaction"
                    GoTo Aa
                End If

                st001ID = System.Guid.NewGuid.ToString()
                st002VoucherNo = ""
                da003IssueDate = Format(Date.Now, "dd-MMM-yyyy")
                st004RsNo = dsSelLastTran.Tables(0).Rows(0).Item("RsNo").ToString
                st005MaterialCode = sMaterialCode ''dsSelLastTran.Tables(0).Rows(0).Item("MaterialCode").ToString
                de006IssueQuantity = nStockDifference
                st007PurchaseOrderNo = ""
                st008TransactionType = st008TransactionType
                st009SalesOrderNo = ""
                st0010IssueUnits = dsSelLastTran.Tables(0).Rows(0).Item("IssueUnits").ToString
                de0011IssuePcs = 0
                de0012IssuePrice = Val(dsSelLastTran.Tables(0).Rows(0).Item("IssuePrice").ToString)
                de0013IssueValue = Val(de006IssueQuantity) * Val(de0012IssuePrice)
                st0014CompanyCode = dsSelLastTran.Tables(0).Rows(0).Item("CompanyCode").ToString
                st0015JobberCode = dsSelLastTran.Tables(0).Rows(0).Item("JobberCode").ToString
                st0017FromStage = dsSelLastTran.Tables(0).Rows(0).Item("FromStage").ToString


                st0020FromMaterial = dsSelLastTran.Tables(0).Rows(0).Item("FromMaterial").ToString
                st0021ToMaterial = dsSelLastTran.Tables(0).Rows(0).Item("ToMaterial").ToString
                da0022PurchaseOrderDate = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("PurchaseOrderDate").ToString
                st0023SupplierCode = sSupplierCode
                st0024FromColor = dsSelLastTran.Tables(0).Rows(0).Item("FromColor").ToString
                st0025ToColor = dsSelLastTran.Tables(0).Rows(0).Item("ToColor").ToString
                st0026Remarks = dsSelLastTran.Tables(0).Rows(0).Item("Remarks").ToString
                st0027Material = dsSelLastTran.Tables(0).Rows(0).Item("Material").ToString
                st0028MaterialTypeCode = dsSelLastTran.Tables(0).Rows(0).Item("MaterialTypeCode").ToString
                st0029CreatedBy = "noorullah / MOHAMMED NOORULLAH / KH-00307"
                da0030CreatedDate = Format(Date.Now, "yyyy-MM-dd hh:mm:ss.fff") ''2015-09-28 16:05:43.423
                st0031ModifiedBy = "noorullah / MOHAMMED NOORULLAH / KH-00307"
                da0032ModifiedDate = da0030CreatedDate
                st0033EnteredOnMachineID = "KHLI-ERPSYS / CPU0"
                st0034FromLotNo = dsSelLastTran.Tables(0).Rows(0).Item("FromLotNo").ToString
                st0035ToLotNo = dsSelLastTran.Tables(0).Rows(0).Item("ToLotNo").ToString

                st0037Color = dsSelLastTran.Tables(0).Rows(0).Item("Color").ToString
                st0038WorkOrderNo = dsSelLastTran.Tables(0).Rows(0).Item("WorkOrderNo").ToString
                st0039SupplierRefNo = ""
                st0040SupplierBillNo = ""
                da0041SupplierRefDate = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("SupplierRefDate").ToString
                da0042SupplierBillDate = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("SupplierBillDate").ToString
                st0043MaterialColor = dsSelLastTran.Tables(0).Rows(0).Item("MaterialColor").ToString
                st0044SerialNo = dsSelLastTran.Tables(0).Rows(0).Item("SerialNo").ToString
                da0045PaidOn = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("PaidOn").ToString
                st0046ChequeNo = dsSelLastTran.Tables(0).Rows(0).Item("ChequeNo").ToString
                st0047Variant = dsSelLastTran.Tables(0).Rows(0).Item("Variant").ToString
                st0048ToVariant = dsSelLastTran.Tables(0).Rows(0).Item("ToVariant").ToString
                st0049InspectionNumber = dsSelLastTran.Tables(0).Rows(0).Item("InspectionNumber").ToString
                st0050SizeSequenceNo = dsSelLastTran.Tables(0).Rows(0).Item("SizeSequenceNo").ToString
                st0051GroupCode = dsSelLastTran.Tables(0).Rows(0).Item("GroupCode").ToString
                st0052FromWorkOrderNo = dsSelLastTran.Tables(0).Rows(0).Item("FromWorkOrderNo").ToString
                st0053Buyer = dsSelLastTran.Tables(0).Rows(0).Item("Buyer").ToString
                st0054POSize = dsSelLastTran.Tables(0).Rows(0).Item("POSize").ToString
                st0055InspectedBy = dsSelLastTran.Tables(0).Rows(0).Item("InspectedBy").ToString
                st0056CustomerStyleNo = dsSelLastTran.Tables(0).Rows(0).Item("CustomerStyleNo").ToString
                in0057IsApproved = 0 ''dsSelLastTran.Tables(0).Rows(0).Item("IsApproved").ToString
                st0058ApprovedBy = dsSelLastTran.Tables(0).Rows(0).Item("ApprovedBy").ToString
                da0059ApprovedOn = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("ApprovedOn").ToString
                st0060ModuleName = dsSelLastTran.Tables(0).Rows(0).Item("ModuleName").ToString
                st0061JobCardNo = dsSelLastTran.Tables(0).Rows(0).Item("JobCardNo").ToString
                st0062UnitCode = sUnitCode ' dsSelLastTran.Tables(0).Rows(0).Item("UnitCode").ToString
                st0063Origin = dsSelLastTran.Tables(0).Rows(0).Item("Origin").ToString
                st0064Source = dsSelLastTran.Tables(0).Rows(0).Item("Source").ToString
                st0065Article = dsSelLastTran.Tables(0).Rows(0).Item("Article").ToString
                st0066ArticleGroup = dsSelLastTran.Tables(0).Rows(0).Item("ArticleGroup").ToString
                st0067LeatherCode = dsSelLastTran.Tables(0).Rows(0).Item("LeatherCode").ToString
                st0068ColorCode = dsSelLastTran.Tables(0).Rows(0).Item("ColorCode").ToString
                st0069BrandCode = dsSelLastTran.Tables(0).Rows(0).Item("BrandCode").ToString
                de0070ChallanQuantity = nClosingStock ''dsSelLastTran.Tables(0).Rows(0).Item("ChallanQuantity").ToString
                de0071ChallanPcs = 0 ''dsSelLastTran.Tables(0).Rows(0).Item("ChallanPcs").ToString
                st0072SkinType = dsSelLastTran.Tables(0).Rows(0).Item("SkinType").ToString
                st0073Substance = dsSelLastTran.Tables(0).Rows(0).Item("Substance").ToString
                st0074ContainerNo = dsSelLastTran.Tables(0).Rows(0).Item("ContainerNo").ToString
                st0075WeightRange = dsSelLastTran.Tables(0).Rows(0).Item("WeightRange").ToString
                in0076NoOfBundles = Val(dsSelLastTran.Tables(0).Rows(0).Item("NoOfBundles").ToString)
                in0077IsCustomerSpecific = Val(dsSelLastTran.Tables(0).Rows(0).Item("IsCustomerSpecific").ToString)
                st0078Quality = dsSelLastTran.Tables(0).Rows(0).Item("Quality").ToString
                st0079Status = dsSelLastTran.Tables(0).Rows(0).Item("Status").ToString
                st0080BatchNo = dsSelLastTran.Tables(0).Rows(0).Item("BatchNo").ToString
                st0081LeatherType = dsSelLastTran.Tables(0).Rows(0).Item("LeatherType").ToString
                st0082OldFromLocation = dsSelLastTran.Tables(0).Rows(0).Item("OldFromLocation").ToString
                st0083OldToLocation = dsSelLastTran.Tables(0).Rows(0).Item("OldToLocation").ToString
                da0084Ack_Date = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("Ack_Date").ToString
                st0085Ack_By = dsSelLastTran.Tables(0).Rows(0).Item("Ack_By").ToString
                da0086ReceivedDateTime = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("ReceivedDateTime").ToString
                st0087Authorisedby = dsSelLastTran.Tables(0).Rows(0).Item("Authorisedby").ToString
                st0088ReceivedBy = dsSelLastTran.Tables(0).Rows(0).Item("ReceivedBy").ToString
                st0089ConfirmedStatus = dsSelLastTran.Tables(0).Rows(0).Item("ConfirmedStatus").ToString
                st0090ApprovedStatus = dsSelLastTran.Tables(0).Rows(0).Item("ApprovedStatus").ToString
                da0091DCDate = Format(Date.Now, "dd-MMM-yyyy") ''dsSelLastTran.Tables(0).Rows(0).Item("DCDate").ToString
                st0092Grade = dsSelLastTran.Tables(0).Rows(0).Item("Grade").ToString
                st0093FromCompanyCode = dsSelLastTran.Tables(0).Rows(0).Item("FromCompanyCode").ToString
                in0094IsInspectionDone = Val(dsSelLastTran.Tables(0).Rows(0).Item("IsInspectionDone").ToString)
                st0095ComponentGroup = dsSelLastTran.Tables(0).Rows(0).Item("ComponentGroup").ToString
                st0096RID = dsSelLastTran.Tables(0).Rows(0).Item("RID").ToString
                st0097FromSalesOrder = dsSelLastTran.Tables(0).Rows(0).Item("FromSalesOrder").ToString
                st0098Season = dsSelLastTran.Tables(0).Rows(0).Item("Season").ToString
                de0099ApprovedQty = Val(dsSelLastTran.Tables(0).Rows(0).Item("ApprovedQty").ToString)
                de00100ApprovedPcs = Val(dsSelLastTran.Tables(0).Rows(0).Item("ApprovedPcs").ToString)
                st00101ExeVersionNo = dsSelLastTran.Tables(0).Rows(0).Item("ExeVersionNo").ToString
                st00102OldJobCardNo = dsSelLastTran.Tables(0).Rows(0).Item("OldJobCardNo").ToString
                st00103OldSalesOrderNo = dsSelLastTran.Tables(0).Rows(0).Item("OldSalesOrderNo").ToString
                st00104ReasonCode = dsSelLastTran.Tables(0).Rows(0).Item("ReasonCode").ToString
                de00105LandedPrice = Val(dsSelLastTran.Tables(0).Rows(0).Item("LandedPrice").ToString)
                st00106CurrencyCode = dsSelLastTran.Tables(0).Rows(0).Item("CurrencyCode").ToString
                de00107POPrice = Val(dsSelLastTran.Tables(0).Rows(0).Item("POPrice").ToString)
                de00108ExchangeRate = Val(dsSelLastTran.Tables(0).Rows(0).Item("ExchangeRate").ToString)
                de00109PercentageonValue = Val(dsSelLastTran.Tables(0).Rows(0).Item("PercentageonValue").ToString)


                Dim daInsMaterialIssues4Adju As New SqlDataAdapter("Insert Into MaterialIssues4Adjustments Values ('" & st001ID & "','" & st002VoucherNo & _
                                                                   "','" & Format(da003IssueDate.Date, "dd-MMM-yyyy") & "','" & st004RsNo & "','" & sMaterialCode & _
                                                                   "','" & de006IssueQuantity & "','" & st007PurchaseOrderNo & "','" & st008TransactionType & _
                                                                   "','" & st009SalesOrderNo & "','" & st0010IssueUnits & "','" & de0011IssuePcs & _
                                                                   "','" & de0012IssuePrice & "','" & de0013IssueValue & "','" & st0014CompanyCode & _
                                                                   "','" & st0015JobberCode & "','" & st0016FromLocation & "','" & st0017FromStage & _
                                                                   "','" & st0018ToLocation & "','" & st0019ToStage & "','" & st0020FromMaterial & _
                                                                   "','" & st0021ToMaterial & "','" & Format(da0022PurchaseOrderDate.Date, "dd-MMM-yyyy") & "','" & st0023SupplierCode & _
                                                                   "','" & st0024FromColor & "','" & st0025ToColor & "','" & st0026Remarks & _
                                                                   "','" & st0027Material & "','" & st0028MaterialTypeCode & "','" & st0029CreatedBy & _
                                                                   "','" & Format(da0030CreatedDate.Now, "yyyy-MM-dd hh:mm:ss.fff") & "','" & st0031ModifiedBy & "','" & Format(da0032ModifiedDate.Now, "yyyy-MM-dd hh:mm:ss.fff") & _
                                                                   "','" & st0033EnteredOnMachineID & "','" & st0034FromLotNo & "','" & st0035ToLotNo & _
                                                                   "','" & st0036Size & "','" & st0037Color & "','" & st0038WorkOrderNo & "','" & st0039SupplierRefNo & _
                                                                   "','" & st0040SupplierBillNo & "','" & Format(da0041SupplierRefDate.Date, "dd-MMM-yyyy") & "','" & Format(da0042SupplierBillDate.Date, "dd-MMM-yyyy") & _
                                                                   "','" & st0043MaterialColor & "','" & st0044SerialNo & "','" & Format(da0045PaidOn.Date, "dd-MMM-yyyy") & _
                                                                   "','" & st0046ChequeNo & "','" & st0047Variant & "','" & st0048ToVariant & _
                                                                   "','" & st0049InspectionNumber & "','" & st0050SizeSequenceNo & "','" & st0051GroupCode & _
                                                                   "','" & st0052FromWorkOrderNo & "','" & st0053Buyer & "','" & st0054POSize & _
                                                                   "','" & st0055InspectedBy & "','" & st0056CustomerStyleNo & "','" & in0057IsApproved & _
                                                                   "','" & st0058ApprovedBy & "','" & Format(da0059ApprovedOn.Date, "dd-MMM-yyyy") & "','" & st0060ModuleName & _
                                                                   "','" & st0061JobCardNo & "','" & st0062UnitCode & "','" & st0063Origin & "','" & st0064Source & _
                                                                   "','" & st0065Article & "','" & st0066ArticleGroup & "','" & st0067LeatherCode & _
                                                                   "','" & st0068ColorCode & "','" & st0069BrandCode & "','" & de0070ChallanQuantity & _
                                                                   "','" & de0071ChallanPcs & "','" & st0072SkinType & "','" & st0073Substance & _
                                                                   "','" & st0074ContainerNo & "','" & st0075WeightRange & "','" & in0076NoOfBundles & _
                                                                   "','" & in0077IsCustomerSpecific & "','" & st0078Quality & "','" & st0079Status & _
                                                                   "','" & st0080BatchNo & "','" & st0081LeatherType & "','" & st0082OldFromLocation & _
                                                                   "','" & st0083OldToLocation & "','" & Format(da0084Ack_Date.Date, "dd-MMM-yyyy") & "','" & st0085Ack_By & "','" & Format(da0086ReceivedDateTime.Date, "dd-MMM-yyyy") & _
                                                                   "','" & st0087Authorisedby & "','" & st0088ReceivedBy & "','" & st0089ConfirmedStatus & "','" & st0090ApprovedStatus & _
                                                                   "','" & Format(da0091DCDate.Date, "dd-MMM-yyyy") & "','" & st0092Grade & "','" & st0093FromCompanyCode & "','" & in0094IsInspectionDone & _
                                                                   "','" & st0095ComponentGroup & "','" & st0096RID & "','" & st0097FromSalesOrder & _
                                                                   "','" & st0098Season & "','" & de0099ApprovedQty & "','" & de00100ApprovedPcs & _
                                                                   "','" & st00101ExeVersionNo & "','" & st00102OldJobCardNo & "','" & st00103OldSalesOrderNo & _
                                                                   "','" & st00104ReasonCode & "','" & de00105LandedPrice & "','" & st00106CurrencyCode & _
                                                                   "','" & de00107POPrice & "','" & de00108ExchangeRate & "','" & de00109PercentageonValue & "')", sConstr)
                Dim dsInsMaterialIssues4Adju As New DataSet
                daInsMaterialIssues4Adju.Fill(dsInsMaterialIssues4Adju)
                dsInsMaterialIssues4Adju.AcceptChanges()
                ClearAll()
                sStatus = "Updated"
Aa:
                If nTimes = 1 Then
                    Dim daUpdTmpStock As New SqlDataAdapter("Update tmptbl_ForStockImporting Set ISysStock = '" & nArrivedClosingStock & _
                                                            "', IDiff = '" & nStockDifference & _
                                                            "', Status = '" & sStatus & _
                                                            "' Where SupplierCode = '" & sSupplierCode & _
                                                            "' And UnitCode = '" & sUnitCode & _
                                                            "' AND StoreCode = '" & sStoreCode & _
                                                            "' And MaterialCode = '" & sMaterialCode & "'", sConstr)
                    Dim dsUpdTmpStock As New DataSet
                    daUpdTmpStock.Fill(dsUpdTmpStock)
                    dsUpdTmpStock.AcceptChanges()

                    nArrivedClosingStock = 0
                    nStockDifference = 0



                End If

            Next
        Next

    End Sub

    Private Sub ClearAll()

        st001ID = "" : st002VoucherNo = "" : da003IssueDate = Date.Now : st004RsNo = "" : st005MaterialCode = "" : de006IssueQuantity = 0 : st007PurchaseOrderNo = ""
        st008TransactionType = "" : st009SalesOrderNo = "" : st0010IssueUnits = "" : de0011IssuePcs = 0 : de0012IssuePrice = 0 : de0013IssueValue = 0
        st0014CompanyCode = "" : st0015JobberCode = "" : st0016FromLocation = "" : st0017FromStage = "" : st0018ToLocation = "" : st0019ToStage = ""
        st0020FromMaterial = "" : st0021ToMaterial = "" : da0022PurchaseOrderDate = Date.Now : st0023SupplierCode = "" : st0024FromColor = "" : st0025ToColor = ""
        st0026Remarks = "" : st0027Material = "" : st0028MaterialTypeCode = "" : st0029CreatedBy = "" : da0030CreatedDate = Date.Now : st0031ModifiedBy = ""
        da0032ModifiedDate = Date.Now : st0033EnteredOnMachineID = "" : st0034FromLotNo = "" : st0035ToLotNo = "" : st0036Size = "" : st0037Color = ""
        st0038WorkOrderNo = "" : st0039SupplierRefNo = "" : st0040SupplierBillNo = "" : da0041SupplierRefDate = Date.Now : da0042SupplierBillDate = Date.Now : st0043MaterialColor = ""
        st0044SerialNo = "" : da0045PaidOn = Date.Now : st0046ChequeNo = "" : st0047Variant = "" : st0048ToVariant = "" : st0049InspectionNumber = "" : st0050SizeSequenceNo = ""
        st0051GroupCode = "" : st0052FromWorkOrderNo = "" : st0053Buyer = "" : st0054POSize = "" : st0055InspectedBy = "" : st0056CustomerStyleNo = "" : in0057IsApproved = 0
        st0058ApprovedBy = "" : da0059ApprovedOn = Date.Now : st0060ModuleName = "" : st0061JobCardNo = "" : st0062UnitCode = "" : st0063Origin = ""
        st0064Source = "" : st0065Article = "" : st0066ArticleGroup = "" : st0067LeatherCode = "" : st0068ColorCode = "" : st0069BrandCode = ""
        de0070ChallanQuantity = 0 : de0071ChallanPcs = 0 : st0072SkinType = "" : st0073Substance = "" : st0074ContainerNo = "" : st0075WeightRange = ""
        in0076NoOfBundles = 0 : in0077IsCustomerSpecific = 0 : st0078Quality = "" : st0079Status = "" : st0080BatchNo = "" : st0081LeatherType = ""
        st0082OldFromLocation = "" : st0083OldToLocation = "" : da0084Ack_Date = Date.Now : st0085Ack_By = "" : da0086ReceivedDateTime = Date.Now
        st0087Authorisedby = "" : st0088ReceivedBy = "" : st0089ConfirmedStatus = "" : st0090ApprovedStatus = "" : da0091DCDate = Date.Now : st0092Grade = ""
        st0093FromCompanyCode = "" : in0094IsInspectionDone = 0 : st0095ComponentGroup = "" : st0096RID = "" : st0097FromSalesOrder = "" : st0098Season = ""
        de0099ApprovedQty = 0 : de00100ApprovedPcs = 0 : st00101ExeVersionNo = "" : st00102OldJobCardNo = "" : st00103OldSalesOrderNo = "" : st00104ReasonCode = ""
        de00105LandedPrice = 0 : st00106CurrencyCode = "" : de00107POPrice = 0 : de00108ExchangeRate = 0 : de00109PercentageonValue = 0

    End Sub
    Private Sub LoadPhysicalStock()
        'Select * from tmptbl_ForStockImporting Where UnitCode = 'KHLI' And StoreCode = 'KFS' And 
        'MaterialCode = 'CON-FIN-CR-0020'
        Dim daSelStock As New SqlDataAdapter("Select * from tmptbl_ForStockImporting Where UnitCode = '" & sUnitCode & _
                                                  "' And StoreCode = '" & sStoreCode & _
                                                  "' And MaterialCode = '" & sMaterialCode & _
                                                  "' And SupplierCode = '" & sSupplierCode & _
                                                  "' And Size = '" & sSizeCode & _
                                                  "' Order by PKID", sConstr)
        Dim dsSelStock As New DataSet
        daSelStock.Fill(dsSelStock)


        If dsSelStock.Tables(0).Rows.Count = 0 Then
            nClosingStock = 0
            nRate = 0
            nValue = 0
        Else
            nClosingStock = Val(dsSelStock.Tables(0).Rows(0).Item("IPhyStock").ToString)
            nRate = dsSelStock.Tables(0).Rows(0).Item("Rate").ToString
            nValue = Val(dsSelStock.Tables(0).Rows(0).Item("IValue").ToString)
        End If


    End Sub
End Class