Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmSpoolManagingTools
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccWIPManageTools As New ccWIPManageTools
    Dim myccOrderPlanningReport As New ccOrderPlanningReport
    
#Region "Declaration"
    Dim nPKID As Integer
    Dim nSlNo As Integer
    Dim sID As String
    Dim sSalesOrderNo As String
    Dim sCustomerOrderNo As String
    Dim dOrderReceivedDate As Date
    Dim sBuyerName As String
    Dim sArticle As String
    Dim sArticleName As String
    Dim nOrderQuantity As Integer
    Dim nPrice As Decimal
    Dim nOrderValue As Decimal
    Dim dExpectedDeliveryDate As Date
    Dim sShipmentStatus As String
    Dim sCodificationNew As String
    Dim nDispatch As Integer
    Dim nBalance As Integer
    Dim sOrderStatus As String
    Dim sArticleMould As String
    Dim sAssortmentName As String
    Dim sRowInfo As String
    Dim sSize01 As String
    Dim sSize02 As String
    Dim sSize03 As String
    Dim sSize04 As String
    Dim sSize05 As String
    Dim sSize06 As String
    Dim sSize07 As String
    Dim sSize08 As String
    Dim sSize09 As String
    Dim sSize10 As String
    Dim sSize11 As String
    Dim sSize12 As String
    Dim sSize13 As String
    Dim sSize14 As String
    Dim sSize15 As String
    Dim sSize16 As String
    Dim sSize17 As String
    Dim sSize18 As String

    Dim nSODOrderQuantity, nSODQuantity01, nSODQuantity02, nSODQuantity03, nSODQuantity04, nSODQuantity05, nSODQuantity06, nSODQuantity07, nSODQuantity08, nSODQuantity09, nSODQuantity10 As Integer
    Dim nSODQuantity11, nSODQuantity12, nSODQuantity13, nSODQuantity14, nSODQuantity15, nSODQuantity16, nSODQuantity17, nSODQuantity18 As Integer

#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub

    Dim ngrdRowCount As Integer

    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)

        LoadComboItems()

        LoadAccesInfo()
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        'grdWIPSummaryDetailsV1.ExportToXlsx("E:\ERP Tracking System Details.xlsx")
        MsgBox("Export Completed")

    End Sub

    Dim sTypeofOrder, sTypeofDocument As String
    Dim sSelOption As String
    Dim nIsEDDNegotiable As Integer
    Dim sCustomer, sTypeofSpoon, sUserType, sSection As String

    Private Sub LoadData()
        LoadAccesInfo()
        mdlSGM.sSelectedOption = ""

        If cbxCustomer.Text = " ALL CUSTOMERS" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If
        sCustomer = cbxCustomer.Text

        If sUserCategory.ToUpper = "ADMIN" Then
            sTypeofSpoon = cbxTypeofSpoon.Text
        End If

        If sTypeofSpoon = "All" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
            sSection = cbxTypeofSpoon.Text
            If sUserCategory.ToUpper = "ADMIN" Then
                sTypeofSpoon = cbxTypeofSpoon.Text
            Else
                sSection = sTypeofSpoon
            End If
        End If

        sIsLoaded = "N"
        Dim i As Integer = 0


        grdWIPSummary.BringToFront()
Ab:
        ngrdRowCount = grdWIPSummaryV1.RowCount
        For i = 0 To ngrdRowCount
            grdWIPSummaryV1.DeleteRow(i)
        Next
        ngrdRowCount = grdWIPSummaryV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If
        sIsLoaded = "N"
        grdWIPSummary.DataSource = myccWIPManageTools.LoadWIPSummary(Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"), sCustomer)

        With grdWIPSummaryV1
            .Columns(3).VisibleIndex = -1

            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        End With
        MsgBox("Loading Completed")
        ngrdRowNo = 0
        LoadWIPDetails()
        sIsLoaded = "Y"
    End Sub

    Private Sub dpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpTo.ValueChanged, dpFrom.ValueChanged

        LoadComboItems()

    End Sub

    Private Sub LoadComboItems()

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        cbxCustomer.DataSource = myccWIPManageTools.LoadCustomer(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxCustomer.DisplayMember = "BuyerName"

        cbxTypeofSpoon.SelectedIndex = 0
        'cbxArticleMould.DataSource = myccOrderPlanningReport.LoadArticles(mdlSGM.dFromDate, mdlSGM.dToDate)
        'cbxArticleMould.DisplayMember = "SoleName"

    End Sub



    Dim ngrdRowNo, nBal2Dispatch As Integer
    Dim sSalesOrderDetailsID, sIsNegotiable, sIsLoaded, sJobCard As String
    Dim WeekNumber As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        LoadData()
    End Sub

    Dim nMouldQty, nM2FQty, nFinishQty, nPkdStockQty, nRejectionQty As Integer


    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        If chkbxSpoolInfo.Checked = True Then
            ngrdRowNo = grdWIPSummaryV1.FocusedRowHandle
            mdlSGM.sSelectedOption = grdWIPSummaryV1.GetDataRow(ngrdRowNo).Item("WIPID").ToString
            mdlSGM.sReportType = "SPOOL INFO"

            InsertSpoolInfoForPrinting()
        Else
            mdlSGM.sReportType = "ERP TRACKING SYSTEM DETAILS"
        End If


        frmReport.Show()
        'frmReport1.Show()
    End Sub

    Private Sub grdWIPSummaryV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdWIPSummaryV1.FocusedRowChanged
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdWIPSummaryV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                LoadWIPDetails()
            End If

        End If
    End Sub

    Private Sub LoadWIPDetails()
        If ngrdRowNo >= 0 Then

            sJobCard = grdWIPSummaryV1.GetDataRow(ngrdRowNo).Item("JobcardNo").ToString

            If sJobCard <> "" Then
                If sSection <> "" Then
                    grdWIPDetails.DataSource = myccWIPManageTools.LoadWIPDetails(sJobCard, sSection)

                    With grdWIPDetailsV1
                        .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                        .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    End With
                End If
            Else

                sJobCard = grdWIPSummaryV1.GetDataRow(ngrdRowNo).Item("WIPID").ToString

                grdWIPDetails.DataSource = myccWIPManageTools.LoadWIPDetails(sJobCard, sSection)

                With grdWIPDetailsV1
                    .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                    .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                End With
            End If

            


        Else
            MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
            Exit Sub
        End If
    End Sub

    Dim sIPAddress, sUserCategory As String
    Private Sub LoadAccesInfo()

        sIPAddress = mdlSGM.strIPAddress
        Dim daSelSystemInfo As New SqlDataAdapter("Select * from AccessPermission where SystemIp = '" & sIPAddress & "'", sConstr)
        Dim dsSelSystemInfo As New DataSet
        daSelSystemInfo.Fill(dsSelSystemInfo)

        sUserCategory = "ADMIN"
        GoTo Aa
        If dsSelSystemInfo.Tables(0).Rows.Count <= 0 Then
            MsgBox("System Access Not Defined", MsgBoxStyle.Critical)
            cbReferesh.Enabled = False
        Else
            cbReferesh.Enabled = True
            sUserCategory = dsSelSystemInfo.Tables(0).Rows(0).Item("UserCatgory").ToString

            If sUserCategory.ToUpper = "ADMIN" Then
                cbxTypeofSpoon.Enabled = True
            Else
                cbxTypeofSpoon.Enabled = False
                sTypeofSpoon = dsSelSystemInfo.Tables(0).Rows(0).Item("ProcessName").ToString
            End If
        End If
Aa:
    End Sub

    Dim sUserName As String
    Dim nStatus As Integer
    Private Sub InsertSpoolInfoForPrinting()
        Try
            If chkbxSpoolInfo.Checked = True Then
                ngrdRowNo = grdWIPSummaryV1.FocusedRowHandle
                mdlSGM.sSelectedOption = grdWIPSummaryV1.GetDataRow(ngrdRowNo).Item("WIPID").ToString

                Dim daSpool As New SqlDataAdapter("Select * from Spool Where SpoolId = '" & mdlSGM.sSelectedOption & "'", sConstr)
                Dim dsSpool As New DataSet
                daSpool.Fill(dsSpool)

                If dsSpool.Tables(0).Rows.Count = 0 Then
                Else
                    sUserName = dsSpool.Tables(0).Rows(0).Item("CreatedBy").ToString
                End If

                Dim daDelSpoolPrintInfo As New SqlDataAdapter("Delete from TempSpoolPrintInfo Where CreatedBy = '" & sUserName & "' And CreatedDate < '" & Format(Date.Now, "dd-MMM-yyyy") & "'", sConstr)
                Dim dsDelSpoolPrintInfo As New DataSet
                daDelSpoolPrintInfo.Fill(dsDelSpoolPrintInfo)
                dsDelSpoolPrintInfo.AcceptChanges()

                Dim daDelSpoolPrintInfo1 As New SqlDataAdapter("Delete from TempSpoolPrintInfo Where SpoolId = '" & mdlSGM.sSelectedOption & "'", sConstr)
                Dim dsDelSpoolPrintInfo1 As New DataSet
                daDelSpoolPrintInfo1.Fill(dsDelSpoolPrintInfo1)
                dsDelSpoolPrintInfo1.AcceptChanges()


                Dim daSelSpoolInfo As New SqlDataAdapter("Select * from vw_SpoolDetails where SpoolID = '" & mdlSGM.sSelectedOption & _
                                                         "' Order by JobcardNo,CartonNo", sConstr)
                Dim dsSelSpoolInfo As New DataSet
                daSelSpoolInfo.Fill(dsSelSpoolInfo)

                Dim i As Integer = 0
                Dim nInternalSlno As Integer = 1
                nCorrectBox = 0 : nCorrectQuantity = 0

                For i = 0 To dsSelSpoolInfo.Tables(0).Rows.Count - 1
                    sCartonNo = dsSelSpoolInfo.Tables(0).Rows(i).Item("CartonNo").ToString
                    sJobCardNo = dsSelSpoolInfo.Tables(0).Rows(i).Item("JobCardNo").ToString

                    Dim nBarcodeLen As Integer = Microsoft.VisualBasic.Len(sCartonNo)

                    Dim nCartonNo As Integer = Val(Microsoft.VisualBasic.Right(sCartonNo, nBarcodeLen - 14))

                    Dim daSelPkgDtls As New SqlDataAdapter("Select IsNull(Status,'') As Status from PackingDetail Where JobcardNo = '" & sJobCardNo & _
                                                           "' And CartonNo = '" & nCartonNo & "'", sConstr)
                    Dim dsSelPkgDtls As New DataSet
                    daSelPkgDtls.Fill(dsSelPkgDtls)

                    Dim sStatus As String

                    If dsSelPkgDtls.Tables(0).Rows(0).Item("Status").ToString <> "" Then
                        sStatus = ""
                    Else
                        sStatus = ": {*}"
                    End If

                    If Len(sCartonNo) = 15 Then
                        sCartonNo = Microsoft.VisualBasic.Right(sCartonNo, 1)
                    ElseIf Len(sCartonNo) = 16 Then
                        sCartonNo = Microsoft.VisualBasic.Right(sCartonNo, 2)
                    Else
                        sCartonNo = Microsoft.VisualBasic.Right(sCartonNo, 3)
                    End If

                    nSize = Val(dsSelSpoolInfo.Tables(0).Rows(i).Item("Size").ToString) ''decimal
                    nQuantity = Val(dsSelSpoolInfo.Tables(0).Rows(i).Item("Quantity").ToString)
                    nStatus = Val(dsSelSpoolInfo.Tables(0).Rows(i).Item("FKStatus").ToString)
                    If nStatus = 1 Then
                        nCorrectBox = nCorrectBox + 1
                        nCorrectQuantity = nCorrectQuantity + nQuantity
                    End If

                    If nInternalSlno = 1 Then
                        sCustomerOrderNo = dsSelSpoolInfo.Tables(0).Rows(i).Item("CustomerOrderNo").ToString
                        dOrderReceivedDate = dsSelSpoolInfo.Tables(0).Rows(i).Item("OrderReceivedDate")
                        sProcessName = dsSelSpoolInfo.Tables(0).Rows(i).Item("ProcessName").ToString
                        dProcessDate = dsSelSpoolInfo.Tables(0).Rows(i).Item("ProcessDate")

                        dSpoolDate = dsSelSpoolInfo.Tables(0).Rows(i).Item("SpoolDate")
                        sSpoolID = dsSelSpoolInfo.Tables(0).Rows(i).Item("SpoolID").ToString
                        sDepartment = dsSelSpoolInfo.Tables(0).Rows(i).Item("Department").ToString
                        sDeviceID = dsSelSpoolInfo.Tables(0).Rows(i).Item("DeviceID").ToString
                        sFKStatus = dsSelSpoolInfo.Tables(0).Rows(i).Item("FKStatus").ToString
                        sArticleName = dsSelSpoolInfo.Tables(0).Rows(i).Item("ArticleName").ToString
                        sArticleColor = dsSelSpoolInfo.Tables(0).Rows(i).Item("ArticleColor").ToString
                        sJobCardNo = dsSelSpoolInfo.Tables(0).Rows(i).Item("JobCardNo").ToString
                        sCreatedBy = dsSelSpoolInfo.Tables(0).Rows(i).Item("CreatedBy").ToString
                        dCreatedDate = dsSelSpoolInfo.Tables(0).Rows(i).Item("CreatedDate").ToString
                        nBoxCount = Val(dsSelSpoolInfo.Tables(0).Rows(i).Item("BoxCount").ToString)
                        nTotalQuantity = Val(dsSelSpoolInfo.Tables(0).Rows(i).Item("TotalQuantity").ToString)
                        sInfo01 = sCartonNo + " : " + nSize.ToString + " : " + nQuantity.ToString + sStatus
                        sInfo02 = ""
                        sInfo03 = ""
                        sInfo04 = ""
                        sInfo05 = ""
                        sInfo06 = ""

                        nInfo01 = nStatus
                        nInfo02 = 0 : nInfo03 = 0 : nInfo04 = 0 : nInfo05 = 0 : nInfo06 = 0
                    ElseIf nInternalSlno = 2 Then
                        sInfo02 = sCartonNo + " : " + nSize.ToString + " : " + nQuantity.ToString + sStatus
                        nInfo02 = nStatus
                    ElseIf nInternalSlno = 3 Then
                        sInfo03 = sCartonNo + " : " + nSize.ToString + " : " + nQuantity.ToString + sStatus
                        nInfo03 = nStatus
                    ElseIf nInternalSlno = 4 Then
                        sInfo04 = sCartonNo + " : " + nSize.ToString + " : " + nQuantity.ToString + sStatus
                        nInfo04 = nStatus
                    ElseIf nInternalSlno = 5 Then
                        sInfo05 = sCartonNo + " : " + nSize.ToString + " : " + nQuantity.ToString + sStatus
                        nInfo05 = nStatus
                    ElseIf nInternalSlno = 6 Then
                        sInfo06 = sCartonNo + " : " + nSize.ToString + " : " + nQuantity.ToString + sStatus
                        nInfo06 = nStatus
                        InsertTempSpoolPrintInfo()
                        nInternalSlno = 0
                        GoTo Aa
                    End If

                    If i = dsSelSpoolInfo.Tables(0).Rows.Count - 1 Then
                        InsertTempSpoolPrintInfo()
                    ElseIf sJobCardNo <> dsSelSpoolInfo.Tables(0).Rows(i + 1).Item("JobCardNo").ToString Then
                        InsertTempSpoolPrintInfo()
                        nInternalSlno = 0
                    End If
Aa:
                    nInternalSlno = nInternalSlno + 1


                Next
                Dim daUpdTempSpoolPrintInfo As New SqlDataAdapter("Update TempSpoolPrintInfo Set CorrectBoxCount = '" & nCorrectBox & _
                                                                  "', CorrectBoxQty = '" & nCorrectQuantity & _
                                                                  "' Where SpoolId = '" & sSpoolID & "'", sConstr)
                Dim dsUpdTempSpoolPrintInfo As New DataSet
                daUpdTempSpoolPrintInfo.Fill(dsUpdTempSpoolPrintInfo)
                dsUpdTempSpoolPrintInfo.AcceptChanges()
            Else

            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim sProcessName, sSpoolID, sDepartment, sDeviceID, sFKStatus As String
    Dim dProcessDate, dSpoolDate, dCreatedDate As Date
    Dim nSize As Decimal
    Dim nQuantity, nBoxCount, nTotalQuantity As Integer
    Dim sArticleColor, sJobCardNo, sCartonNo, sCreatedBy, sInfo01, sInfo02, sInfo03, sInfo04, sInfo05, sInfo06 As String
    Dim nInfo01, nInfo02, nInfo03, nInfo04, nInfo05, nInfo06, nCorrectBox, nCorrectQuantity As Integer


    Private Sub InsertTempSpoolPrintInfo()
        Try
            Dim daInsTempSpoolPrintInfo As New SqlDataAdapter("Insert Into TempSpoolPrintInfo values ('" & sCustomerOrderNo & _
                                                              "','" & Format(dOrderReceivedDate.Date, "dd-MMM-yyyy") & "','" & sProcessName & _
                                                              "','" & Format(dProcessDate.Date, "dd-MMM-yyyy") & "','" & nSize & "','" & nQuantity & _
                                                              "','" & Format(dSpoolDate.Date, "dd-MMM-yyyy") & "','" & sSpoolID & "','" & sDepartment & _
                                                              "','" & sDeviceID & "','" & sFKStatus & "','" & sArticleName & _
                                                              "','" & sArticleColor & "','" & sJobCardNo & "','" & sCartonNo & _
                                                              "','" & sCreatedBy & "','" & Format(dCreatedDate.Date, "dd-MMM-yyyy") & "','" & sInfo01 & _
                                                              "','" & sInfo02 & "','" & sInfo03 & "','" & sInfo04 & _
                                                              "','" & sInfo05 & "','" & sInfo06 & "','" & nBoxCount & _
                                                              "','" & nTotalQuantity & "','" & nInfo01 & _
                                                              "','" & nInfo02 & "','" & nInfo03 & "','" & nInfo04 & _
                                                              "','" & nInfo05 & "','" & nInfo06 & "','0','0')", sConstr)
            Dim dsInsTempSpoolPrintInfo As New DataSet
            daInsTempSpoolPrintInfo.Fill(dsInsTempSpoolPrintInfo)
            dsInsTempSpoolPrintInfo.AcceptChanges()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nCartonNo As Integer
    Private Sub grdWIPDetailsV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdWIPDetailsV1.FocusedRowChanged
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdWIPDetailsV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sJobCardNo = Microsoft.VisualBasic.Left((grdWIPDetailsV1.GetDataRow(ngrdRowNo).Item("JobCardNo").ToString), 13)
                nCartonNo = Val(Microsoft.VisualBasic.Right((grdWIPDetailsV1.GetDataRow(ngrdRowNo).Item("JobCardNo").ToString), 3))

                ClearSizeAndQty()

                Dim daSelJCInfo As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo = '" & sJobCardNo & "'", sConstr)
                Dim dsSelJCInfo As New DataSet
                daSelJCInfo.Fill(dsSelJCInfo)

                lblSize01.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size01").ToString
                lblSize02.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size02").ToString
                lblSize03.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size03").ToString
                lblSize04.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size04").ToString
                lblSize05.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size05").ToString
                lblSize06.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size06").ToString
                lblSize07.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size07").ToString
                lblSize08.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size08").ToString
                lblSize09.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size09").ToString
                lblSize10.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size10").ToString
                lblSize11.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size11").ToString
                lblSize12.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size12").ToString
                lblSize13.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size13").ToString
                lblSize14.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size14").ToString
                lblSize15.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size15").ToString
                lblSize16.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size16").ToString
                lblSize17.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size17").ToString
                lblSize18.Text = dsSelJCInfo.Tables(0).Rows(0).Item("Size18").ToString

                Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where JobcardNo = '" & sJobCardNo & _
                                                      "' And CartonNo = '" & nCartonNo & "'", sConstr)
                Dim dsSelPkgDtl As New DataSet
                daSelPkgDtl.Fill(dsSelPkgDtl)

                tbJCQty01.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity01").ToString
                tbJCQty02.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity02").ToString
                tbJCQty03.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity03").ToString
                tbJCQty04.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity04").ToString
                tbJCQty05.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity05").ToString
                tbJCQty06.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity06").ToString
                tbJCQty07.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity07").ToString
                tbJCQty08.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity08").ToString
                tbJCQty09.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity09").ToString
                tbJCQty10.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity10").ToString
                tbJCQty11.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity11").ToString
                tbJCQty12.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity12").ToString
                tbJCQty13.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity13").ToString
                tbJCQty14.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity14").ToString
                tbJCQty15.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity15").ToString
                tbJCQty16.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity16").ToString
                tbJCQty17.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity17").ToString
                tbJCQty18.Text = dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity18").ToString

                If Val(tbJCQty01.Text) = 0 Then : tbJCQty01.Clear() : End If
                If Val(tbJCQty02.Text) = 0 Then : tbJCQty02.Clear() : End If
                If Val(tbJCQty03.Text) = 0 Then : tbJCQty03.Clear() : End If
                If Val(tbJCQty04.Text) = 0 Then : tbJCQty04.Clear() : End If
                If Val(tbJCQty05.Text) = 0 Then : tbJCQty05.Clear() : End If
                If Val(tbJCQty06.Text) = 0 Then : tbJCQty06.Clear() : End If
                If Val(tbJCQty07.Text) = 0 Then : tbJCQty07.Clear() : End If
                If Val(tbJCQty08.Text) = 0 Then : tbJCQty08.Clear() : End If
                If Val(tbJCQty09.Text) = 0 Then : tbJCQty09.Clear() : End If
                If Val(tbJCQty10.Text) = 0 Then : tbJCQty10.Clear() : End If
                If Val(tbJCQty11.Text) = 0 Then : tbJCQty11.Clear() : End If
                If Val(tbJCQty12.Text) = 0 Then : tbJCQty12.Clear() : End If
                If Val(tbJCQty13.Text) = 0 Then : tbJCQty13.Clear() : End If
                If Val(tbJCQty14.Text) = 0 Then : tbJCQty14.Clear() : End If
                If Val(tbJCQty15.Text) = 0 Then : tbJCQty15.Clear() : End If
                If Val(tbJCQty16.Text) = 0 Then : tbJCQty16.Clear() : End If
                If Val(tbJCQty17.Text) = 0 Then : tbJCQty17.Clear() : End If
                If Val(tbJCQty18.Text) = 0 Then : tbJCQty18.Clear() : End If

            End If

        End If
    End Sub

    Private Sub ClearSizeAndQty()
        lblSize01.Text = "" : lblSize02.Text = "" : lblSize03.Text = "" : lblSize04.Text = "" : lblSize05.Text = "" : lblSize06.Text = ""
        lblSize07.Text = "" : lblSize08.Text = "" : lblSize09.Text = "" : lblSize10.Text = "" : lblSize11.Text = "" : lblSize12.Text = ""
        lblSize13.Text = "" : lblSize14.Text = "" : lblSize15.Text = "" : lblSize16.Text = "" : lblSize17.Text = "" : lblSize18.Text = ""

        tbJCQty01.Clear() : tbJCQty02.Clear() : tbJCQty03.Clear() : tbJCQty04.Clear() : tbJCQty05.Clear() : tbJCQty06.Clear()
        tbJCQty07.Clear() : tbJCQty08.Clear() : tbJCQty09.Clear() : tbJCQty10.Clear() : tbJCQty11.Clear() : tbJCQty12.Clear()
        tbJCQty13.Clear() : tbJCQty14.Clear() : tbJCQty15.Clear() : tbJCQty16.Clear() : tbJCQty17.Clear() : tbJCQty18.Clear()

    End Sub

End Class