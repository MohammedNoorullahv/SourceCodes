Public Class forPackingList

    '    Public Function GeneratePackingDetailsForTheFullShoe(ByRef dsMain As DataSet, ByVal app_DataAccess As mdlSGM.Sample.AppDataAccess, ByVal strOrderNo As String, ByVal strShipper As String) As Boolean
    '        Try
    '            Dim strQuery As String = String.Empty
    '            Dim dtJobCardDetails As DataTable = Nothing
    '            Dim dtOrderDetails As DataTable = Nothing
    '            If dsMain IsNot Nothing Then
    '                Dim dtTemp As DataTable = Nothing
    '                If Not String.IsNullOrEmpty(strOrderNo.Trim) Then




    '                    If strOrderNo.ToUpper.Trim.Contains("-F-") Then
    '                        strQuery = "Select * From vw_JobCardAndAssortmentDetailsForPacking where OrderNo = '" + strOrderNo + "' And Shipper = '" + strShipper + "' And ComponentGroup = 'FULLSHOE'"
    '                    Else
    '                        strQuery = "Select * From vw_JobCardAndAssortmentDetailsForPacking where OrderNo = '" + strOrderNo + "' And Shipper = '" + strShipper + "' And ComponentGroup = 'UPPER'"
    '                    End If
    '                    app_DataAccess.GetDataTableFromDataBase(strQuery, dtJobCardDetails)
    '                    If dtJobCardDetails IsNot Nothing Then
    '                    End If
    '                    Dim drPackingMain As DataRow = dsMain.Tables("Packing").Rows(0)
    '                    Dim objArtType As Object = Nothing
    '                    Dim strArticleType As String = String.Empty
    '                    app_DataAccess.ExecuteScalar("select articletype from buyerorder where orderno='" + strOrderNo + "'", objArtType)
    '                    If objArtType IsNot Nothing AndAlso Not String.IsNullOrEmpty(Convert.ToString(objArtType)) Then
    '                        strArticleType = Convert.ToString(objArtType)
    '                    Else
    '                        MessageBox.Show("Article Type is blank in Buyer Order table for Orderno='" + strOrderNo + "'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    End If
    '                    ''C   F   J   U   UP    W

    '                    strQuery = "Select * From SalesOrderDetails where  OrderNo = '" + strOrderNo + "' And " _
    '                       + " Shipper = '" + strShipper + "' And SalesOrderNo = '" + Convert.ToString(drPackingMain("SalesOrderNo")) + "' and " _
    '                      + " CustWorkOrderNo = '" + Convert.ToString(drPackingMain("CustWorkOrderNo")) + "'"
    '                    app_DataAccess.GetDataTableFromDataBase(strQuery, dtOrderDetails)
    '                    If dtOrderDetails IsNot Nothing Then
    '                        dtOrderDetails.TableName = "SalesOrderDetails"
    '                        If dsMain.Tables.Contains("SalesOrderDetails") Then
    '                            dsMain.Tables.Remove("SalesOrderDetails")
    '                        End If

    '                        dsMain.Merge(dtOrderDetails)
    '                        If Convert.ToString(drPackingMain("TypeOfPacking")).Trim.ToLower.Equals("cbp") Then
    '                            Dim obj As New frmExcelImportPacking(app_DataAccess, dsMain, dtOrderDetails)
    '                            obj.StartPosition = FormStartPosition.CenterParent
    '                            If obj.ShowDialog() <> DialogResult.OK Then
    '                                Throw (New Exception(""))
    '                            End If
    '                            obj.Dispose()
    '                            obj = Nothing
    '                            Application.DoEvents()
    '                            GC.Collect()
    '                            Application.DoEvents()

    '                        Else
    '                            For Each dr As DataRow In dtOrderDetails.Rows
    '                                If Convert.ToString(dr("IsAssortedOrder")).Trim.ToLower.Equals("y") Then
    '                                    MakeDetailsEntryForTheFullShoePackingForAssortmentOrder(app_DataAccess, dr, dsMain, drPackingMain)
    '                                Else
    '                                    If Convert.ToString(drPackingMain("TypeOfPacking")).Trim.ToLower.Equals("asp") Then
    '                                        Throw New Exception("This is not Assorted Order,kindly select appropriate packing type and try again later.")
    '                                    ElseIf Convert.ToString(drPackingMain("TypeOfPacking")).Trim.ToLower.Equals("smr") Then
    '                                        MakePackingDetailsEntryForFullShoeUnAssortedMixedMiddle(app_DataAccess, dsMain, dr, drPackingMain)
    '                                    ElseIf Convert.ToString(drPackingMain("TypeOfPacking")).Trim.ToLower.Equals("ssm") Then
    '                                        MakePackingDetailsEntryForFullShoeUnAssortedSingalMiddle(app_DataAccess, dsMain, dr, drPackingMain)
    '                                    ElseIf Convert.ToString(drPackingMain("TypeOfPacking")).Trim.ToLower.Equals("sms") Then
    '                                        MakePackingDetailsEntryForFullShoeUnAssortedMixLast(app_DataAccess, dsMain, dr, drPackingMain)
    '                                    ElseIf Convert.ToString(drPackingMain("TypeOfPacking")).Trim.ToLower.Equals("sps") Then
    '                                        MakePackingDetailsEntryForFullShoeUnAssortedSingalLast(app_DataAccess, dsMain, dr, drPackingMain)
    '                                    End If
    '                                End If
    '                            Next
    '                        End If
    '                        If dsMain.Tables.Contains("PackingDetail") AndAlso dsMain.Tables("SalesOrderDetails").Rows.Count > 0 Then
    '                            If Not String.IsNullOrEmpty(Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0)("Season"))) Then
    '                                For Each drPakgDetail As DataRow In dsMain.Tables("PackingDetail").Rows
    '                                    drPakgDetail("BarCode") = GetNextBarCode(app_DataAccess, dsMain.Tables("SalesOrderDetails").Rows(0)("Season"), strShipper)
    '                                Next
    '                            End If
    '                        End If

    '                        Dim strTempSizeName As String = String.Empty
    '                        Dim strTempQtyName As String = String.Empty
    '                        Dim iCurrentQty As Integer = 0
    '                        Dim iQty As Integer = 0
    '                        Dim drPD As DataRow = Nothing
    '                        Dim dvPAckingDetails As DataView = New DataView(dsMain.Tables("PackingDetail"))


    '                        Dim dtOuterCarton As DataTable = Nothing


    '                        strQuery = "select * From ArticleCosting where  Article = '" + Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0).Item("Article")) + "' and ColorCode = '" + Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0).Item("ColorCode")) + "' and LeatherCode = '" + Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0).Item("MainRawMaterialCode")) + "' and Variant = '" + Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0).Item("Variant")) + "' And MaterialCode Like 'PAC-SHB-%' "
    '                        Dim dtCosting As DataTable = Nothing
    '                        app_DataAccess.GetDataTableFromDataBase(strQuery, dtCosting)
    '                        Dim dtArticleMaterialSizeMapping As DataTable = Nothing
    '                        strQuery = "select * From ArticleMaterialSizeGroupMapping where  Article = '" + Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0).Item("Article")) + "' and Variant = '" + Convert.ToString(dsMain.Tables("SalesOrderDetails").Rows(0).Item("Variant")) + "' And MaterialCode Like 'PAC-SHB-%'"
    '                        app_DataAccess.GetDataTableFromDataBase(strQuery, dtArticleMaterialSizeMapping)

    '                        Dim dtSizeTemp As DataTable = New DataTable
    '                        dtSizeTemp.Columns.Add("Size")
    '                        dtSizeTemp.Columns.Add("Qty", GetType(System.Double))

    '                        Dim strMessage As String = String.Empty
    '                        Dim strCurrrentMessage As String = String.Empty
    '                        Dim blnAllOuterCartonFound As Boolean = True

    '                        Dim dblCartonQty As Double = 0
    '                        For Each drSOD As DataRow In dsMain.Tables("SalesOrderDetails").Rows
    '                            If dtSizeAndQuantityByMaterialAndGroup Is Nothing Then
    '                                GetOuterCardtonMaterialCodeForTheBuyer(drSOD, drPD, dtCosting, dtArticleMaterialSizeMapping, app_DataAccess)
    '                            End If
    '                            dtOuterCarton = Nothing
    '                            Dim strInnerMaterialBox As String = String.Empty

    '                            dvPAckingDetails.RowFilter = "SalesOrderDetailID = '" + Convert.ToString(drSOD("ID")) + "'"
    '                            If dvPAckingDetails.Count > 0 Then
    '                                For iPD As Integer = 0 To dvPAckingDetails.Count - 1
    '                                    If dvPAckingDetails(iPD).Row.RowState <> DataRowState.Deleted Then
    '                                        iCurrentQty = 0
    '                                        drPD = dvPAckingDetails(iPD).Row

    '                                        dtSizeTemp.Rows.Clear()

    '                                        For i As Integer = 1 To 18
    '                                            strTempSizeName = "Size" + i.ToString("00")
    '                                            strTempQtyName = "Quantity" + i.ToString("00")
    '                                            If drPD(strTempSizeName) Is DBNull.Value Then
    '                                                drPD(strTempSizeName) = drSOD(strTempSizeName)
    '                                                If drPD(strTempSizeName) Is DBNull.Value Then
    '                                                    drPD(strTempSizeName) = ""
    '                                                End If
    '                                            End If
    '                                            If drPD(strTempQtyName) Is DBNull.Value Then
    '                                                drPD(strTempQtyName) = 0
    '                                            End If
    '                                            If Integer.TryParse(Convert.ToString(drPD(strTempQtyName)), iQty) Then
    '                                                iCurrentQty += iQty
    '                                            End If



    '                                            If Not String.IsNullOrEmpty(Convert.ToString(drPD(strTempSizeName)).Trim) Then
    '                                                If Not Double.TryParse(Convert.ToString(drPD(strTempQtyName)), dblCartonQty) Then
    '                                                    dblCartonQty = 0
    '                                                End If
    '                                                If dblCartonQty > 0 Then
    '                                                    Dim drSize As DataRow = dtSizeTemp.NewRow
    '                                                    Dim iDouble As Double = 0

    '                                                    If Not Double.TryParse(Convert.ToString(drPD(strTempSizeName)), iDouble) Then
    '                                                        iDouble = 0
    '                                                    End If

    '                                                    drSize("Size") = Convert.ToString(iDouble)
    '                                                    drSize("Qty") = dblCartonQty
    '                                                    dtSizeTemp.Rows.Add(drSize)
    '                                                End If
    '                                            End If

    '                                        Next
    '                                        dtSizeTemp.DefaultView.Sort = "Size"
    '                                        drPD("Quantity") = iCurrentQty

    '                                        strInnerMaterialBox = String.Empty
    '                                        If dtCosting IsNot Nothing AndAlso dtCosting.Rows.Count = 1 Then
    '                                            strInnerMaterialBox = Convert.ToString(dtCosting.Rows(0).Item("MaterialCode"))
    '                                        Else
    '                                            If dtCosting IsNot Nothing AndAlso dtSizeTemp.Rows.Count > 0 Then
    '                                                Dim dvSizeMapping As DataView = New DataView(dtSizeAndQuantityByMaterialAndGroup)
    '                                                For iCosting As Integer = 0 To dtCosting.Rows.Count - 1
    '                                                    dvSizeMapping.RowFilter = "MaterialCode = '" + Convert.ToString(dtCosting.Rows(iCosting).Item("MaterialCode")) + "' and Size = '" + Convert.ToString(dtSizeTemp.Rows(dtSizeTemp.Rows.Count - 1).Item("Size")) + "'"
    '                                                    If dvSizeMapping.Count > 0 Then
    '                                                        strInnerMaterialBox = Convert.ToString(dvSizeMapping(0).Item("MaterialCode"))
    '                                                        Exit For
    '                                                    End If
    '                                                Next
    '                                                If String.IsNullOrEmpty(strInnerMaterialBox.Trim) Then
    '                                                    blnAllOuterCartonFound = False
    '                                                    strCurrrentMessage = "outerbox carton Mapping not done for the article."
    '                                                    If Not strMessage.Trim.ToLower.Contains(strCurrrentMessage.Trim.ToLower) Then
    '                                                        strMessage = strCurrrentMessage + vbCrLf
    '                                                    End If
    '                                                End If

    '                                            Else
    '                                                If dtCosting Is Nothing Then
    '                                                    blnAllOuterCartonFound = False
    '                                                    strCurrrentMessage = "Costing for Inner Shoebox not done"
    '                                                    If Not strMessage.Trim.ToLower.Contains(strCurrrentMessage.Trim.ToLower) Then
    '                                                        strMessage = strCurrrentMessage + vbCrLf
    '                                                    End If

    '                                                End If
    '                                            End If
    '                                        End If

    '                                        If Not String.IsNullOrEmpty(strInnerMaterialBox.Trim) Then
    '                                            strQuery = "SELECT     AbbrevTable.FullName_,Materials.MaterialCode FROM Materials INNER JOIN OuterCartonMapping ON " _
    '                                                    + " Materials.MaterialCode = OuterCartonMapping.OuterBoxMatCode INNER JOIN AbbrevTable ON " _
    '                                                    + " Materials.MaterialSize = AbbrevTable.Abbrev_ where OuterCartonMapping.BuyerGroupCode = '" _
    '                                                    + Convert.ToString(drSOD("BuyerGroupCode")) + "' and OuterCartonMapping.InnerBoxMatCode = '" _
    '                                                    + strInnerMaterialBox + "' and OuterCartonMapping.ShoeQuantity = " + Convert.ToString(drPD("Quantity")) _
    '                                                    + " and OuterCartonMapping.IsActive = 1 And  (AbbrevTable.Group_ = 'MaterialSize') "
    '                                            app_DataAccess.GetDataTableFromDataBase(strQuery, dtOuterCarton)
    '                                            If dtOuterCarton IsNot Nothing AndAlso dtOuterCarton.Rows.Count > 0 Then
    '                                                drPD("MaterialCode") = dtOuterCarton.Rows(0).Item("MaterialCode")
    '                                                Dim strCBM As String = Convert.ToString(dtOuterCarton.Rows(0).Item("FullName_"))
    '                                                If strCBM.Trim.ToLower.EndsWith("cm") Then
    '                                                    strCBM = strCBM.Trim
    '                                                    strCBM = strCBM.Substring(0, strCBM.Length - 2)

    '                                                End If
    '                                                Dim strVal() As String = strCBM.Split("X")
    '                                                If strVal.Length > 0 Then
    '                                                    Dim iDoubleVal As Double = 1
    '                                                    Dim iCurrentVal As Double = 0
    '                                                    For z As Integer = 0 To strVal.Length - 1
    '                                                        If Double.TryParse(strVal(z), iCurrentVal) Then
    '                                                            iDoubleVal = iDoubleVal * iCurrentVal
    '                                                        End If
    '                                                    Next
    '                                                    drPD("CartonCBM") = iDoubleVal / 1000000
    '                                                End If
    '                                            Else
    '                                                blnAllOuterCartonFound = False
    '                                                If dtOuterCarton Is Nothing Then
    '                                                    strCurrrentMessage = "Unable to load outer carton mapping table for the filter : BuyerGroupCode = '" + Convert.ToString(drSOD("BuyerGroupCode")) + "' and InnerBoxMatCode = '" + strInnerMaterialBox + "' and ShoeQuantity = " + Convert.ToString(drPD("Quantity")) + " and IsActive = 1   "
    '                                                    If Not strMessage.Trim.ToLower.Contains(strCurrrentMessage.Trim.ToLower) Then
    '                                                        strMessage = strCurrrentMessage + vbCrLf
    '                                                    End If

    '                                                Else
    '                                                    strCurrrentMessage = "Outer Carton Mapping not done : BuyerGroupCode = '" + Convert.ToString(drSOD("BuyerGroupCode")) + "' and InnerBoxMatCode = '" + strInnerMaterialBox + "' and ShoeQuantity = " + Convert.ToString(drPD("Quantity")) + " and IsActive = 1   "
    '                                                    If Not strMessage.Trim.ToLower.Contains(strCurrrentMessage.Trim.ToLower) Then
    '                                                        strMessage = strCurrrentMessage + vbCrLf
    '                                                    End If
    '                                                End If
    '                                            End If
    '                                        Else
    '                                            blnAllOuterCartonFound = False
    '                                        End If




    '                                    End If
    '                                Next
    '                            End If
    '                            If IsDBNull(drSOD("PackedQty")) Then
    '                                drSOD("PackedQty") = 0
    '                            End If

    '                            If Integer.TryParse(Convert.ToString(dsMain.Tables("PackingDetail").Compute("sum(Quantity)", dvPAckingDetails.RowFilter)), iQty) Then
    '                                If iQty > 0 Then
    '                                    drSOD("PackedQty") += iQty
    '                                End If
    '                            End If
    '                        Next
    '                        '' count carton no from pkg detail and insert in packing 
    '                        If dsMain.Tables.Contains("PackingDetail") AndAlso dsMain.Tables("Packing").Rows.Count > 0 Then
    '                            dsMain.Tables("Packing").Rows(0)("TotalCarton") = dsMain.Tables("PackingDetail").Rows.Count
    '                        End If

    '                        If Not blnAllOuterCartonFound Then

    '                            Throw (New Exception(strMessage))

    '                        End If

    '                    End If
    '                Else
    '                    Throw (New Exception("Order number recived is blank."))
    '                End If
    '            Else
    '                Throw (New Exception("Dataset recived is not set."))
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message + " [FillPackingDetails][clsProcess]", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            Throw (ex)
    '        End Try
    '    End Function

    '    Private Function GetOuterCardtonMaterialCodeForTheBuyer(ByVal drSOD As DataRow, ByVal drPaking As DataRow, ByVal dtCosting As DataTable, ByVal dtArticleMaterialSizeMapping As DataTable, ByVal app_DataAccess As AppDataAccess) As Boolean
    '        Try
    '            Dim dtSizeAndQuantityTemp As DataTable = New DataTable
    '            dtSizeAndQuantityTemp.Columns.Add("Size")
    '            dtSizeAndQuantityTemp.Columns.Add("Quantity", GetType(System.Double))
    '            dtSizeAndQuantityTemp.Columns.Add("SizeStandard")
    '            dtSizeAndQuantityTemp.Columns.Add("UserCategory")
    '            dtSizeAndQuantityTemp.Columns.Add("Group")
    '            dtSizeAndQuantityTemp.Columns.Add("IsStdMappingAvailable", GetType(System.Boolean))
    '            dtSizeAndQuantityTemp.Columns.Add("IsSizeMappingAvailable", GetType(System.Boolean))
    '            Dim strAllMappedSizes As String = ""
    '            Dim strSizeFieldName As String = String.Empty
    '            Dim strQtyFieldName As String = String.Empty
    '            Dim dblSize As Double = 0
    '            Dim dblCartonQty As Double = 0
    '            For z As Integer = 1 To 18
    '                strSizeFieldName = "Size" + z.ToString("00")
    '                strQtyFieldName = "Quantity" + z.ToString("00")
    '                If Not String.IsNullOrEmpty(Convert.ToString(drSOD(strSizeFieldName)).Trim) Then
    '                    If Not Double.TryParse(Convert.ToString(drSOD(strQtyFieldName)), dblCartonQty) Then
    '                        dblCartonQty = 0
    '                    End If
    '                    If Not Double.TryParse(Convert.ToString(drSOD(strSizeFieldName)), dblSize) Then
    '                        dblSize = 0
    '                    End If
    '                    If dblCartonQty Then
    '                        Dim drSize As DataRow = dtSizeAndQuantityTemp.NewRow
    '                        drSize("Size") = dblSize 'Convert.ToString(drSOD(strSizeFieldName))

    '                        drSize("Quantity") = dblCartonQty
    '                        drSize("SizeStandard") = Convert.ToString(drSOD("SizeStandard"))
    '                        drSize("UserCategory") = Convert.ToString(drSOD("UserCategory"))
    '                        dtSizeAndQuantityTemp.Rows.Add(drSize)
    '                    End If
    '                End If
    '            Next


    '            dtSizeAndQuantityByMaterialAndGroup = New DataTable
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("Size")
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("MaterialCode")
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("Group")
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("Quantity", GetType(System.Double))
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("SizeStandard")
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("UserCategory")
    '            dtSizeAndQuantityByMaterialAndGroup.Columns.Add("SizeType")


    '            Dim dtStandardSizeMapping As DataTable = Nothing
    '            Dim strQuery As String = "Select UserCategory, IND,EU,US,UK from StandardSizeMapping "
    '            app_DataAccess.GetDataTableFromDataBase(strQuery, dtStandardSizeMapping)
    '            If dtStandardSizeMapping IsNot Nothing Then

    '                For Each dr As DataRow In dtStandardSizeMapping.Rows
    '                    For Each dcol As DataColumn In dr.Table.Columns
    '                        If Not dcol.ColumnName.Equals("UserCategory") Then
    '                            If Double.TryParse(Convert.ToString(dr(dcol.ColumnName)), dblSize) Then
    '                                dr(dcol.ColumnName) = dblSize
    '                            End If
    '                        End If
    '                    Next
    '                Next


    '                Dim strSizeMsg As String = ""
    '                blnMessageShown = True
    '                For Each DR As DataRow In dtCosting.Rows
    '                    Dim dvArticleMaterialSizeMapping As DataView = New DataView(dtArticleMaterialSizeMapping)
    '                    dvArticleMaterialSizeMapping.RowFilter = "  MaterialCode='" + Convert.ToString(DR("MaterialCode")) + "'"
    '                    If dvArticleMaterialSizeMapping.Count > 0 Then
    '                        CheckSizeGroupAndStandardMappingForSG(dvArticleMaterialSizeMapping, dtSizeAndQuantityTemp, "", dtStandardSizeMapping)
    '                    End If
    '                    dvArticleMaterialSizeMapping.RowFilter = ""
    '                Next
    '                If dtSizeAndQuantityTemp IsNot Nothing AndAlso dtSizeAndQuantityByMaterialAndGroup IsNot Nothing Then

    '                    For Each dr As DataRow In dtSizeAndQuantityByMaterialAndGroup.Rows
    '                        strAllMappedSizes += "'" + Convert.ToString(dr("Size")) + "' ,"
    '                    Next
    '                    If Not String.IsNullOrEmpty(strAllMappedSizes) Then
    '                        strAllMappedSizes = strAllMappedSizes.TrimEnd(",")
    '                        Dim dv As DataView = New DataView(dtSizeAndQuantityTemp)
    '                        dv.RowFilter = "Size not in (" + strAllMappedSizes + ")"
    '                        If dv.Count > 0 Then
    '                            For Each drv As DataRowView In dv
    '                                strSizeMsg += Convert.ToString(drv("Size")) + ","
    '                            Next
    '                        End If
    '                        dv.RowFilter = ""
    '                    End If
    '                    If Not String.IsNullOrEmpty(strSizeMsg) Then
    '                        strSizeMsg = "Size Group Mapping is not available for size(s) :" + strSizeMsg.Remove(strSizeMsg.LastIndexOf(",")) + vbCrLf
    '                    End If

    '                End If

    '                If Not String.IsNullOrEmpty(strSizeMsg) Then
    '                    Throw New Exception(strSizeMsg)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function

    '    Public Function ConsumptionOfTannery(ByVal app_DataAccess As AppDataAccess, ByRef dsMain As DataSet, ByVal strJobCardNo As String, ByVal strShipper As String) As Boolean
    '        Try
    '            Dim dblQty As Double = 0
    '            Dim dsDummy As New DataSet
    '            Dim strMaterial As String = String.Empty
    '            Dim drJobCardDetail As DataRow = Nothing
    '            app_DataAccess.GetDataTableFromDataBase("select * from JobCardDetail where JobCardNo='" + strJobCardNo + "' and Shipper='" + strShipper + "'", dtTemp)
    '            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
    '                dtTemp.TableName = "JobCardDetail"
    '                dsMain.Merge(dtTemp)
    '                drJobCardDetail = dtTemp.Rows(0)
    '                app_DataAccess.GetTableMetaDataFromDataBase("select * from ProductionMain", dtTemp)
    '                If dtTemp IsNot Nothing Then
    '                    dtTemp.TableName = "ProductionMain"
    '                    dsMain.Merge(dtTemp)

    '                    app_DataAccess.GetTableMetaDataFromDataBase("select * from ProductionDetails", dtTemp)
    '                    If dtTemp IsNot Nothing Then
    '                        dtTemp.TableName = "ProductionDetails"
    '                        dsMain.Merge(dtTemp)
    '                        strQuery = " SELECT PBP.ProcessName, PC.Section, PC.ProcessCode, PC.Animal, PC.CostingUnit, PC.CostPerUnit, PBP.WorkOrderNo, PBP.SalesOrderNo, PBP.Article, PBP.MaterialCode, PBP.Pcs, PBP.Unit, " _
    '                     + "  PBP.Quantity, PBP.Price, PBP.Value, PBP.CompanyCode, PBP.LotNo, PBP.MaterialColor, PBP.LocationName, PBP.BuyerCode, PBP.Buyer, PBP.SupplierCode, PBP.BrandCode,  OTM.OperationType, PBP.ProcessDate" _
    '                     + " , PC.ApplicableFromDate,PBP.JobCardDetailID, PBP.LossQuantity, PBP.CurrentQuantity, PBP.FromLocation, PBP.Location, PBP.RejectPcs, PBP.SkinType, PBP.CurrentPcs, PBP.LossPcs, M.MaterialSubType FROM dbo.Materials AS M " _
    '                     + " INNER JOIN  dbo.ProductionByProcess AS PBP ON M.MaterialCode = PBP.MaterialCode INNER JOIN dbo.OperationTypeMaster AS OTM ON PBP.ProcessName = OTM.OperationTypeCode LEFT OUTER JOIN" _
    '                     + " dbo.ProcessCosting AS PC ON PBP.SkinType = PC.SkinType AND PBP.ProcessName = PC.ProcessCode WHERE (PBP.WorkOrderNo = '" + strJobCardNo + "') AND PBP.CompanyCode='VA'"
    '                        app_DataAccess.GetDataTableFromDataBase(strQuery, dtTemp)
    '                        If dtTemp IsNot Nothing Then
    '                            dtTemp.TableName = "ProductionByProcess"
    '                            dsDummy.Merge(dtTemp)
    '                            Dim DV As New DataView(dtTemp)
    '                            DV.RowFilter = "ProcessCode Is Null"
    '                            For Each drv As DataRowView In DV
    '                                strMaterial += "" + Convert.ToString(drv("OperationType")) + " - " + Convert.ToString(drv("ProcessName")) + "" + vbCrLf
    '                            Next
    '                            If Not String.IsNullOrEmpty(strMaterial) Then
    '                                strMaterial = "Process Name - Process Code " + vbCrLf + vbCrLf + strMaterial
    '                                Dim objMsg As New frmRegenerateDemandMsg("Process Cost is not defined for," + vbCrLf + vbCrLf + strMaterial, False)
    '                                objMsg.Text = "Tannery Process Costing Message"
    '                                objMsg.BringToFront()
    '                                objMsg.ShowDialog()

    '                                If objMsg IsNot Nothing Then
    '                                    objMsg.Dispose()
    '                                    objMsg = Nothing
    '                                End If
    '                            End If
    '                            DV.RowFilter = ""
    '                            strQuery = "select * from Stock where JobCardNo='" + Convert.ToString(strJobCardNo) + "' and CompanyCode='" + strShipper + "'" _
    '                                + " and Stage in( 'wip') "
    '                            app_DataAccess.GetDataTableFromDataBase(strQuery, dtTemp)
    '                            If dtTemp IsNot Nothing Then
    '                                dtTemp.TableName = "Stock"
    '                                dsMain.Merge(dtTemp)

    '                                strQuery = "Select * From MaterialIssues where TransactionType in ('jobcardreturn','JobCardRejection') and JobCardNo = '" + Convert.ToString(strJobCardNo) + "'  And CompanyCode = 'VA'"
    '                                app_DataAccess.GetDataTableFromDataBase(strQuery, dtTemp)
    '                                If dtTemp IsNot Nothing Then
    '                                    dtTemp.TableName = "MaterialIssues"
    '                                    dsMain.Merge(dtTemp)
    '                                    Dim strMainID As String = String.Empty

    '                                    ''update in production main
    '                                    Dim drMain As DataRow = dsMain.Tables("ProductionMain").NewRow
    '                                    drMain("ID") = Convert.ToString(Guid.NewGuid)
    '                                    strMainID = drMain("ID")
    '                                    drMain("LotDate") = Now.Date
    '                                    drMain("Article") = Convert.ToString(drJobCardDetail("MaterialCode"))
    '                                    drMain("SalesOrderNo") = Convert.ToString(drJobCardDetail("SalesOrderNo"))
    '                                    drMain("JobCardNo") = Convert.ToString(drJobCardDetail("JobCardNo"))
    '                                    drMain("OrderNo") = Convert.ToString(drJobCardDetail("OrderNo"))
    '                                    drMain("Shipper") = Convert.ToString(drJobCardDetail("Shipper"))
    '                                    drMain("BuyerCode") = Convert.ToString(drJobCardDetail("BuyerCode"))
    '                                    dsMain.Tables("ProductionMain").Rows.Add(drMain)


    '                                    ''update in MaterialIssues
    '                                    For Each dr As DataRow In dsMain.Tables("MaterialIssues").Rows
    '                                        If Convert.ToString(dr("TransactionType")).Trim.ToLower.Equals("jobcardreturn") Then 'If Convert.ToString(dr("ToStage")).Trim.ToLower.Equals("instk") Then
    '                                            MakeProductionDetailsEntryTannery(dsMain, drMain, dr, "Return")
    '                                        Else
    '                                            MakeProductionDetailsEntryTannery(dsMain, drMain, dr, "Rejected")
    '                                        End If
    '                                    Next



    '                                    For Each drProductionByProcess As DataRow In dsDummy.Tables("ProductionByProcess").Rows

    '                                        Dim dr As DataRow = dsMain.Tables("ProductionDetails").NewRow
    '                                        dr("ID") = Convert.ToString(Guid.NewGuid)
    '                                        dr("Article") = Convert.ToString(drJobCardDetail("MaterialCode"))
    '                                        dr("MaterialCode") = drProductionByProcess("ProcessName")
    '                                        dr("ProductionMainID") = strMainID
    '                                        dr("SalesOrderNo") = Convert.ToString(drJobCardDetail("SalesOrderNo"))
    '                                        dr("JobCardNo") = Convert.ToString(drJobCardDetail("JobCardNo"))
    '                                        dr("OrderNo") = Convert.ToString(drJobCardDetail("OrderNo"))
    '                                        dr("Shipper") = Convert.ToString(drJobCardDetail("Shipper"))
    '                                        dr("MaterialColor") = Convert.ToString(drJobCardDetail("MaterialColor"))
    '                                        dr("BuyerCode") = Convert.ToString(drJobCardDetail("BuyerCode"))
    '                                        dr("Quantity") = drProductionByProcess("Quantity")
    '                                        If Not Double.TryParse(Convert.ToString(drProductionByProcess("Pcs")), dblQty) Then
    '                                            dblQty = 0
    '                                        End If
    '                                        dr("Pcs") = dblQty
    '                                        If Not Double.TryParse(Convert.ToString(drProductionByProcess("CostPerUnit")), dblQty) Then
    '                                            dblQty = 0
    '                                        End If
    '                                        dr("MaterialPrice") = dblQty
    '                                        dr("MaterialValue") = dr("MaterialPrice") * dblQty
    '                                        dr("BuyerGroupCode") = Convert.ToString(drJobCardDetail("BuyerGroupCode"))
    '                                        dsMain.Tables("ProductionDetails").Rows.Add(dr)

    '                                    Next

    '                                    For Each drStock As DataRow In dsMain.Tables("Stock").Rows
    '                                        Dim drMI As DataRow = dsMain.Tables("MaterialIssues").NewRow
    '                                        drMI("ID") = Guid.NewGuid
    '                                        drMI("IssueDate") = Now
    '                                        drMI("MaterialCode") = drStock("MaterialCode")
    '                                        drMI("TransactionType") = "CONSUMPTION"
    '                                        drMI("IssueQuantity") = drStock("Quantity")
    '                                        drMI("SalesOrderNo") = drJobCardDetail("SalesOrderNo")
    '                                        drMI("IssueUnits") = drStock("Unit")
    '                                        drMI("IssuePcs") = drStock("Pcs")
    '                                        drMI("CompanyCode") = drStock("CompanyCode")
    '                                        drMI("FromLocation") = drStock("Location")
    '                                        drMI("FromStage") = drStock("Stage")
    '                                        drMI("ToLocation") = drStock("Location")
    '                                        drMI("ToStage") = "CONSUME"
    '                                        drMI("Material") = drStock("Material")
    '                                        drMI("MaterialTypeCode") = drStock("MaterialTypeCode")
    '                                        drMI("ToLotNo") = drStock("LotNo")
    '                                        drMI("Size") = drStock("MaterialSize")
    '                                        drMI("Origin") = drStock("Origin")
    '                                        drMI("Source") = drStock("Source")
    '                                        drMI("SkinType") = drStock("HideOrSide")
    '                                        drMI("Quality") = drStock("Quality")
    '                                        drMI("Grade") = drStock("Quality")
    '                                        dsMain.Tables("MaterialIssues").Rows.Add(drMI)
    '                                    Next



    '                                    Dim dvStock As DataView = New DataView(dsMain.Tables("Stock"))
    '                                    For i = dvStock.Count - 1 To 0 Step -1
    '                                        dvStock(i).Row.Delete()
    '                                    Next
    '                                    dsMain.Tables("JobCardDetail").Rows(0)("JobCardStatus") = "CLOSED"
    '                                    Return True
    '                                Else
    '                                    MessageBox.Show("Not able to fill table MaterialIssues from database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                                End If
    '                            Else
    '                                MessageBox.Show("Not able to fill table Stock from database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If

    '                        Else
    '                            MessageBox.Show("Not able to fill table ProductionByProcess from database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    Else
    '                        MessageBox.Show("Not able to fill table ProductionDetails from database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    End If
    '                Else
    '                    MessageBox.Show("Not able to fill table ProductionMain from database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                End If

    '            Else
    '                If dtTemp Is Nothing Then
    '                    MessageBox.Show("Not able to fill table JobCardDetail from database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Else
    '                    MessageBox.Show("JobCardNo :" + strJobCardNo + " is not found in JobCardDetail.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("[clsProcesses][ConsumptionOfTannery]" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '        Return False
    '    End Function
End Class
