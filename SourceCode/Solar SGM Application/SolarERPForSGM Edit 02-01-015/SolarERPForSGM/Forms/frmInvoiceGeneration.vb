Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO

Public Class frmInvoiceGeneration

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrAudit As String = Global.SolarERPForSGM.My.Settings.AHGroupAudit '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnnAudit As New SqlConnection(sConstrAudit)

    Dim keyascii As Integer

    Dim myccInvoice As New ccInvoice
    Dim mystrInvoice As New strInvoice
    Dim mystrInvoiceDetails As New strInvoiceDetails

    Dim sSalesOrderNo, sArticle, sBuyerCode, sBuyer, sJobcardDetailID, sOrderType, sMaterialType, sR2DID As String
    Dim sSize01, sSize02, sSize03, sSize04, sSize05, sSize06, sSize07, sSize08, sSize09, sSize10 As String
    Dim sSize11, sSize12, sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize, sPartQtySize As String
    Dim sShipper, sBuyerGroup, sPackNo, sSalesOrderDetailId, sSalesOrder, sOrderTypeforHSNCode As String

    Dim nQuantity01, nQuantity02, nQuantity03, nQuantity04, nQuantity05, nQuantity06, nQuantity07, nQuantity08, nQuantity09, nQuantity10 As Decimal
    Dim nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18 As Decimal
    Dim nQuantity, nProductValue, nGSTValue, nTotalValue As Decimal
    Dim nSGSTValue, nCGSTValue, nIGSTValue As Decimal

    Dim ngrdRowCount, nStringLength, nBoxNo, nBoxnoLength, ngrdRowNo, nYesNo As Integer
    Dim sJobcardNo, sProcess, sBeginsWith, sDescription, sIsLoaded, sCustomercode, sID, sUpdateMode, sUpdateModeinAudit As String

#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        MsgBox("Export Completed")
    End Sub

 
 
  
    Private Sub frmPackingEntries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        cbxCustomer.SelectedIndex = -1
        cbxType.SelectedIndex = 0
        LoadCustomer()
        LoadSignatory()
        LoadCancelReason()
    End Sub

    Dim sStatus As String
 
    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        PrintDocument1.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static intCurrentChar As Int32

        Dim font As New Font("Verdana", 8)

        Dim PrintAreaHeight, PrintAreaWidth, marginLeft, marginTop As Int32

        With PrintDocument1.DefaultPageSettings

            PrintAreaHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom

            PrintAreaWidth = .PaperSize.Width - .Margins.Left - .Margins.Right

            marginLeft = .Margins.Left

            marginTop = .Margins.Top

        End With

        Dim intLineCount As Int32 = CInt(PrintAreaHeight / font.Height)

        Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, PrintAreaWidth, PrintAreaHeight)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        Dim intLinesFilled, intCharsFitted As Int32

        'e.Graphics.MeasureString(Mid(tbBarcode.Text, intCurrentChar + 1), font, New SizeF(PrintAreaWidth, PrintAreaHeight), fmt, intCharsFitted, intLinesFilled)

        'e.Graphics.DrawString(Mid(tbBarcode.Text, intCurrentChar + 1), font, Brushes.Black, rectPrintingArea, fmt)

        intCurrentChar += intCharsFitted
    End Sub

    Private Sub UpdateNotePad()

        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then
                'writer.WriteLine(Trim(tbBarcode.Text) + " | " + sStatus + " | " & DateTime.Now)
            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub

    Dim sCustomer, sType As String
    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click

        LoadReadytoDispatchSummary()

    End Sub

    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()


        cbxCustomer.DataSource = myccInvoice.LoadCustomers(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxCustomer.DisplayMember = "BuyerName"
        cbxCustomer.ValueMember = "BuyerGroup"

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadReadytoDispatchSummary()
        sIsLoaded = "N"
        sCustomer = cbxCustomer.Text
        sType = cbxType.Text
        sBuyerCode = cbxCustomer.SelectedValue

        mdlSGM.sSelectOption = ""
        If sCustomer = " ALL CUSTOMERS" Then
            mdlSGM.sSelectOption = mdlSGM.sSelectOption + "A"
        Else
            mdlSGM.sSelectOption = mdlSGM.sSelectOption + "F"
        End If

        If sType = "All" Then
            mdlSGM.sSelectOption = mdlSGM.sSelectOption + "A"
        ElseIf sType = "Processed" Then
            mdlSGM.sSelectOption = mdlSGM.sSelectOption + "P"
        Else
            mdlSGM.sSelectOption = mdlSGM.sSelectOption + "N"
        End If

        Dim i As Integer = 0
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        grdReadyToDispatch.BringToFront()

Ab:
        ngrdRowCount = grdReadyToDispatchV1.RowCount
        For i = 0 To ngrdRowCount
            grdReadyToDispatchV1.DeleteRow(i)
        Next
        ngrdRowCount = grdReadyToDispatchV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        grdReadyToDispatch.DataSource = myccInvoice.LoadReadytoDispatch(sBuyerCode, mdlSGM.dFromDate, mdlSGM.dToDate)

        With grdReadyToDispatchV1

            '.Columns(0).VisibleIndex = -1


            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            i = 7
            For i = 7 To 51
                .Columns(i).VisibleIndex = -1
            Next
            .Columns(53).VisibleIndex = -1
        End With
        sIsLoaded = "Y"
    End Sub

    Private Sub LoadSignatory()
        ''Try
        cbxSignatureBy.DataSource = Nothing : cbxSignatureBy.Items.Clear()


        cbxSignatureBy.DataSource = myccInvoice.LoadSignatureBy
        cbxSignatureBy.DisplayMember = "EmpName"
        cbxSignatureBy.ValueMember = "Empcode"

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCancelReason()
        ''Try
        cbxCancel.DataSource = Nothing : cbxCancel.Items.Clear()


        cbxCancel.DataSource = myccInvoice.LoadReasonforCancel
        cbxCancel.DisplayMember = "FullName_"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Dim sIsSelected, sInvoiceGenerated, sIsSameCustomer, sIsSameOrderType, sIsSameMaterialType, sOOrderType As String
    Dim i, nInvoiceSerialNo, nInvoiceQuantity As Integer

    Private Sub cbGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGenerate.Click
        mdlSGM.sMode = "GENERATE"
        nInvoiceQuantity = 0
        mystrInvoice.InvoiceNo = ""
        nInvoiceSerialNo = 0
        nProductValue = 0
        nGSTValue = 0
        nTotalValue = 0
        nSGSTValue = 0
        nCGSTValue = 0
        nIGSTValue = 0
        CheckforSelection()
        ''Code for Checking Selected Or Not
        If sIsSelected = "N" Then : MsgBox("No Rows Selected. Cannot Generate Invoice!", MsgBoxStyle.Critical) : Exit Sub : End If
        ''Code for Checking Selected Or Not

        CheckforInvoiceStatus()
        ''Code for Checking Invoice Generated Or Not
        If sInvoiceGenerated = "Y" Then : MsgBox("Invoice Generated for one of the Row Selected", MsgBoxStyle.Critical) : Exit Sub : End If
        ''Code for Checking Invoice Generated Or Not

        CheckforSameBuyer()
        ''Code for Checking Same Customer Or Not
        If sIsSameCustomer = "N" Then : MsgBox("Multiple Customer Selected. Invoice Cannot be Generated", MsgBoxStyle.Critical) : Exit Sub : End If
        ''Code for Checking Same Customer Or Not

        If chkbxOrderType.Checked = True Then
            CheckforSameOrderType()
            ''Code for Checking Same OrderType Or Not
            If sIsSameOrderType = "N" Then : MsgBox("Multiple Order Type Selected. Invoice Cannot be Generated", MsgBoxStyle.Critical) : Exit Sub : End If
            ''Code for Checking Same OrderType Or Not
        End If




        If chkbxArticleType.Checked = True Then
            CheckforSameArticle()
            ''Code for Checking Same Article Or Not
            If sIsSameMaterialType = "N" Then : MsgBox("Multiple Article Type Selected. Invoice Cannot be Generated", MsgBoxStyle.Critical) : Exit Sub : End If
            ''Code for Checking Same Article Or Not
        End If


        CheckforOtherItems()
        If sAllInfoAvailable = "N" Then
            MsgBox("Some of the required Info Missing. Invoice Cannot be Generated", MsgBoxStyle.Critical) : Exit Sub
        End If

        i = 0

        ngrdRowCount = grdReadyToDispatchV1.RowCount

        For i = 0 To ngrdRowCount - 1
            If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                
                sSalesOrderDetailId = grdReadyToDispatchV1.GetDataRow(i).Item("SalesOrderDetailId").ToString
                sSalesOrderId = grdReadyToDispatchV1.GetDataRow(i).Item("SalesOrderId").ToString
                sOrderType = grdReadyToDispatchV1.GetDataRow(i).Item("OrderType").ToString
                sOrderTypeforHSNCode = sOrderType
                nQuantity = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity").ToString)
                sJobcardNo = grdReadyToDispatchV1.GetDataRow(i).Item("JobcardNo").ToString

                sSize01 = grdReadyToDispatchV1.GetDataRow(i).Item("Size01").ToString
                sSize02 = grdReadyToDispatchV1.GetDataRow(i).Item("Size02").ToString
                sSize03 = grdReadyToDispatchV1.GetDataRow(i).Item("Size03").ToString
                sSize04 = grdReadyToDispatchV1.GetDataRow(i).Item("Size04").ToString
                sSize05 = grdReadyToDispatchV1.GetDataRow(i).Item("Size05").ToString
                sSize06 = grdReadyToDispatchV1.GetDataRow(i).Item("Size06").ToString
                sSize07 = grdReadyToDispatchV1.GetDataRow(i).Item("Size07").ToString
                sSize08 = grdReadyToDispatchV1.GetDataRow(i).Item("Size08").ToString
                sSize09 = grdReadyToDispatchV1.GetDataRow(i).Item("Size09").ToString
                sSize10 = grdReadyToDispatchV1.GetDataRow(i).Item("Size10").ToString
                sSize11 = grdReadyToDispatchV1.GetDataRow(i).Item("Size11").ToString
                sSize12 = grdReadyToDispatchV1.GetDataRow(i).Item("Size12").ToString
                sSize13 = grdReadyToDispatchV1.GetDataRow(i).Item("Size13").ToString
                sSize14 = grdReadyToDispatchV1.GetDataRow(i).Item("Size14").ToString
                sSize15 = grdReadyToDispatchV1.GetDataRow(i).Item("Size15").ToString
                sSize16 = grdReadyToDispatchV1.GetDataRow(i).Item("Size16").ToString
                sSize17 = grdReadyToDispatchV1.GetDataRow(i).Item("Size17").ToString
                sSize18 = grdReadyToDispatchV1.GetDataRow(i).Item("Size18").ToString

                nQuantity01 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity01").ToString)
                nQuantity02 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity02").ToString)
                nQuantity03 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity03").ToString)
                nQuantity04 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity04").ToString)
                nQuantity05 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity05").ToString)
                nQuantity06 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity06").ToString)
                nQuantity07 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity07").ToString)
                nQuantity08 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity08").ToString)
                nQuantity09 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity09").ToString)
                nQuantity10 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity10").ToString)
                nQuantity11 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity11").ToString)
                nQuantity12 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity12").ToString)
                nQuantity13 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity13").ToString)
                nQuantity14 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity14").ToString)
                nQuantity15 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity15").ToString)
                nQuantity16 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity16").ToString)
                nQuantity17 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity17").ToString)
                nQuantity18 = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity18").ToString)

                sJobcardNo = grdReadyToDispatchV1.GetDataRow(i).Item("JobcardNo").ToString
                sJobcardDetailID = grdReadyToDispatchV1.GetDataRow(i).Item("JobcardDetailId").ToString
                sR2DID = grdReadyToDispatchV1.GetDataRow(i).Item("ID").ToString

                If mystrInvoice.InvoiceNo = "" Then
                    sOOrderType = grdReadyToDispatchV1.GetDataRow(i).Item("OrderType").ToString
                    GenerateInvoice()
                End If
                nInvoiceSerialNo = nInvoiceSerialNo + 1
                GenerateInvoiceDetails()

            End If
        Next

        nTotalValue = nProductValue + nGSTValue
        UpdateInvoiceValue()

        Dim daSelInvoice As New SqlDataAdapter("Select * from InvoiceDetail Where InvoiceNo = '" & mystrInvoice.InvoiceNo & "'", sConstr)
        Dim dsSelInvoice As New DataSet
        daSelInvoice.Fill(dsSelInvoice)

        If dsSelInvoice.Tables(0).Rows.Count <= 0 Then
            myccInvoice.DeleteInvoiceMain(mystrInvoice.InvoiceNo)
            MsgBox("Invoice Not Generated", MsgBoxStyle.Critical)

        Else
            MsgBox("Invoice Generated Successfully")
        End If

        LoadReadytoDispatchSummary()

    End Sub

    Private Sub CheckforSelection()
        i = 0
        sIsSelected = "N"

        ngrdRowCount = grdReadyToDispatchV1.RowCount

        For i = 0 To ngrdRowCount - 1
            If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                sIsSelected = "Y"
                Exit For
            End If
        Next
    End Sub

    Private Sub CheckforInvoiceStatus()
        i = 0
        sInvoiceGenerated = "N"

        ngrdRowCount = grdReadyToDispatchV1.RowCount

        For i = 0 To ngrdRowCount - 1
            If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                If grdReadyToDispatchV1.GetDataRow(i).Item("InvoiceNo").ToString <> "" Then
                    sInvoiceGenerated = "Y"
                    Exit For
                End If
            End If
        Next


    End Sub

    Private Sub CheckforSameBuyer()
        i = 0
        sBuyer = ""
        sIsSameCustomer = "Y"

        ngrdRowCount = grdReadyToDispatchV1.RowCount

        For i = 0 To ngrdRowCount - 1
            If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                If sBuyer = "" Then
                    sBuyer = grdReadyToDispatchV1.GetDataRow(i).Item("Buyer").ToString
                Else
                    If sBuyer <> grdReadyToDispatchV1.GetDataRow(i).Item("Buyer").ToString Then
                        sIsSameCustomer = "N"
                        Exit For
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub CheckforSameOrderType()
        i = 0
        sOrderType = ""
        sIsSameOrderType = "Y"

        ngrdRowCount = grdReadyToDispatchV1.RowCount

        For i = 0 To ngrdRowCount - 1
            If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                If sOrderType = "" Then
                    sOrderType = grdReadyToDispatchV1.GetDataRow(i).Item("OrderType").ToString
                    sOOrderType = grdReadyToDispatchV1.GetDataRow(i).Item("Type").ToString
                Else
                    If sOrderType <> grdReadyToDispatchV1.GetDataRow(i).Item("OrderType").ToString Then
                        sIsSameOrderType = "N"
                        Exit For
                    End If
                    If sOrderType = "IO" Then
                        If sOOrderType <> grdReadyToDispatchV1.GetDataRow(i).Item("Type").ToString Then
                            sIsSameOrderType = "N"
                            Exit For
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub CheckforSameArticle()
        i = 0
        sMaterialType = ""
        sIsSameMaterialType = "Y"

        ngrdRowCount = grdReadyToDispatchV1.RowCount

        For i = 0 To ngrdRowCount - 1
            If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                If sMaterialType = "" Then
                    sMaterialType = grdReadyToDispatchV1.GetDataRow(i).Item("MaterialTypeCode").ToString
                Else
                    If sMaterialType <> grdReadyToDispatchV1.GetDataRow(i).Item("MaterialTypeCode").ToString Then
                        sIsSameMaterialType = "N"
                        Exit For
                    End If
                End If
            End If
        Next
    End Sub

    Dim sAllInfoAvailable As String
    Private Sub CheckforOtherItems()
        Try
            sAllInfoAvailable = "Y"
            i = 0

            ngrdRowCount = grdReadyToDispatchV1.RowCount

            For i = 0 To ngrdRowCount - 1
                If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then

                    sSalesOrderDetailId = grdReadyToDispatchV1.GetDataRow(i).Item("SalesOrderDetailId").ToString
                    sSalesOrderId = grdReadyToDispatchV1.GetDataRow(i).Item("SalesOrderId").ToString
                    sOrderType = grdReadyToDispatchV1.GetDataRow(i).Item("OrderType").ToString
                    nQuantity = Val(grdReadyToDispatchV1.GetDataRow(i).Item("Quantity").ToString)
                    sJobcardNo = grdReadyToDispatchV1.GetDataRow(i).Item("JobcardNo").ToString

                    sJobcardNo = grdReadyToDispatchV1.GetDataRow(i).Item("JobcardNo").ToString
                    sJobcardDetailID = grdReadyToDispatchV1.GetDataRow(i).Item("JobcardDetailId").ToString
                    sR2DID = grdReadyToDispatchV1.GetDataRow(i).Item("ID").ToString

                    Dim daSelSOD As New SqlDataAdapter("Select * from SalesOrderDetails Where Id = '" & sSalesOrderDetailId & "'", sConstr)
                    Dim dsSelSOD As New DataSet
                    daSelSOD.Fill(dsSelSOD)

                    If dsSelSOD.Tables(0).Rows(0).Item("HSNGroup").ToString = "" Then
                        sAllInfoAvailable = "N"
                        MsgBox("HSN Group Not Assigned properly for one of the selected Rows. Hence Invoice Cannot be Generated", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    mystrInvoice.Marks1 = dsSelSOD.Tables(0).Rows(0).Item("HSNGroup").ToString

                    Dim daSelSO As New SqlDataAdapter("Select * from SalesOrder Where Id = '" & sSalesOrderId & "'", sConstr)
                    Dim dsSelSO As New DataSet
                    daSelSO.Fill(dsSelSO)

                    If dsSelSO.Tables(0).Rows(0).Item("Buyercode").ToString = "" Then
                        sAllInfoAvailable = "N"
                        MsgBox("Buyer Code Not Available for one of the selected Rows. Hence Invoice Cannot be Generated", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    sBuyerCode = dsSelSO.Tables(0).Rows(0).Item("Buyercode").ToString
                    Dim daSelBuyer As New SqlDataAdapter("Select * from Buyer  Where BuyerCode = '" & sBuyerCode & "'", sConstr)
                    Dim dsSelBuyer As New DataSet
                    daSelBuyer.Fill(dsSelBuyer)

                    If dsSelBuyer.Tables(0).Rows(0).Item("CountryName").ToString = "" Then
                        sAllInfoAvailable = "N"
                        MsgBox("Country Name Not Available for one of the selected Rows. Hence Invoice Cannot be Generated", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    mystrInvoice.Destination = dsSelBuyer.Tables(0).Rows(0).Item("State").ToString
                    mystrInvoice.Currency = dsSelSO.Tables(0).Rows(0).Item("Currency").ToString

                    If mystrInvoice.Currency = "INR" Then
                        mystrInvoice.CurrencyConversion = 1
                        mystrInvoice.InvoiceType = "LOCAL"
                    Else
                        Dim daSelExRate As New SqlDataAdapter("Select * from CurrencyExchangeRate Where CurrencyCode = '" & mystrInvoice.Currency & _
                                                              "' And IsActive = '1' And Type = 'CC'", sConstr)
                        Dim dsSelExRate As New DataSet
                        daSelExRate.Fill(dsSelExRate)

                        If Val(dsSelExRate.Tables(0).Rows(0).Item("ExportRate").ToString) = 0 Then
                            sAllInfoAvailable = "N"
                            MsgBox("Currency ConversionRate Not Assigned properly for one of the selected Rows. Hence Invoice Cannot be Generated", MsgBoxStyle.Critical)
                            Exit Sub
                        End If

                    End If

                    Dim daSelGST As New SqlDataAdapter("Select * from GoodsandServicesTax Where Type = '" & sOrderType & "'", sConstr)
                    Dim dsSelGST As New DataSet
                    daSelGST.Fill(dsSelGST)

                    Dim daSelHSN As New SqlDataAdapter("Select * from GSTHsnCodeDtls Where Type = '" & sOrderType & _
                                                       "' And MaterialName = '" & mystrInvoice.Marks1 & "'", sConstr)
                    Dim dsSelHSN As New DataSet
                    daSelHSN.Fill(dsSelHSN)

                    If dsSelHSN.Tables(0).Rows.Count = 0 Then
                        sAllInfoAvailable = "N"
                        MsgBox("HSN Code Not Assigned properly for one of the selected Rows. Hence Invoice Cannot be Generated", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    If dsSelHSN.Tables(0).Rows(0).Item("HSNCode").ToString = "" Then
                        sAllInfoAvailable = "N"
                        MsgBox("HSN Code Not Assigned properly for one of the selected Rows. Hence Invoice Cannot be Generated", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
            Next

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try


    End Sub
    Public Function GetFinancialYear(ByVal curDate As DateTime) As String
        Dim CurrentYear As Integer = Val(Microsoft.VisualBasic.Right(curDate.Year.ToString, 2))
        Dim PreviousYear As Integer = CurrentYear - 1 'curDate.Year - 1
        Dim NextYear As Integer = CurrentYear + 1 'curDate.Year + 1
        Dim PreYear As String = PreviousYear.ToString()
        Dim NexYear As String = NextYear.ToString()
        Dim CurYear As String = CurrentYear.ToString()

        If (curDate.Month > 3) Then
            FinYear = CurYear.ToString + NexYear.ToString
        Else
            FinYear = PreYear.ToString + CurYear.ToString
        End If
        Return FinYear
    End Function

    Dim sOrderType4Inv, sSalesOrderId, sEmployeeCode, sCustomerOrderNo, sGSTNo As String
    Dim nInvoiceCount, nOrderQuantity As Integer
    Dim FinYear As String = Nothing

    Private Sub GenerateInvoice()
        Try
            Dim daSelSOD As New SqlDataAdapter("Select * from SalesOrderDetails Where Id = '" & sSalesOrderDetailId & "'", sConstr)
            Dim dsSelSOD As New DataSet
            daSelSOD.Fill(dsSelSOD)

            mystrInvoice.Marks1 = dsSelSOD.Tables(0).Rows(0).Item("HSNGroup").ToString
            sCustomerOrderNo = dsSelSOD.Tables(0).Rows(0).Item("CustomerOrderNo").ToString

            Dim daSelSO As New SqlDataAdapter("Select * from SalesOrder Where Id = '" & sSalesOrderId & "'", sConstr)
            Dim dsSelSO As New DataSet
            daSelSO.Fill(dsSelSO)

            sBuyerCode = dsSelSO.Tables(0).Rows(0).Item("Buyercode").ToString

            Dim daSelBuyer As New SqlDataAdapter("Select * from Buyer  Where BuyerCode = '" & sBuyerCode & "'", sConstr)
            Dim dsSelBuyer As New DataSet
            daSelBuyer.Fill(dsSelBuyer)

            sGSTNo = dsSelBuyer.Tables(0).Rows(0).Item("GSTNo").ToString()

            mystrInvoice.cntryname = dsSelBuyer.Tables(0).Rows(0).Item("CountryName").ToString

            mystrInvoice.warehouse = sOrderType
            mystrInvoice.Shipper = "SSPL"
            mystrInvoice.GSTNo = sGSTNo

            GetFinancialYear(Date.Now)
            mystrInvoice.FinancialYear = FinYear

            mystrInvoice.Buyer = sBuyerCode

            If sOrderType = "IO" Then
                Dim daSelOrdType As New SqlDataAdapter("SELECT Inv.WareHouse, SOD.ActualOrderNo, ISNULL(SOD.ActualOrderType, '') AS ActualOrderType, ISNULL(SOD.Type, '') AS Type FROM dbo.InvoiceDetail AS Inv INNER JOIN dbo.CreditNoteForRejRepDetails AS CRN ON Inv.invoiceno = CRN.InvoiceNo INNER JOIN dbo.SalesOrderDetails AS SOD ON Inv.SalesOrderDetailID = SOD.ID Where crn.NoteNumber = '" & sCustomerOrderNo & "'", sConstr)
                Dim dsSelOrdType As New DataSet
                daSelOrdType.Fill(dsSelOrdType)

                If dsSelOrdType.Tables(0).Rows(0).Item("ActualOrderType").ToString = "" Then
                    sOOrderType = dsSelOrdType.Tables(0).Rows(0).Item("Type").ToString
                Else
                    sOOrderType = dsSelOrdType.Tables(0).Rows(0).Item("ActualOrderType").ToString
                End If

                sOrderTypeforHSNCode = sOOrderType
            Else
                'sOOrderType = sOrderType
            End If

            If sOrderType = "JO" Then
                sOrderType4Inv = "JW"
                sOrderTypeforHSNCode = sOrderType
            ElseIf sOrderType = "CO" Then
                sOrderType4Inv = "CS"
                sOrderTypeforHSNCode = sOrderType
            ElseIf sOrderType = "IO" Then
                If sOOrderType = "JO" Then
                    sOrderType4Inv = "JW"
                ElseIf sOOrderType = "CO" Then
                    sOrderType4Inv = "CS"
                ElseIf sOOrderType = "SO" Then
                    sOrderType4Inv = "LS"
                End If
            Else
                If mystrInvoice.cntryname.ToString.ToUpper = "INDIA" Then
                    sOrderType4Inv = "LS"
                Else
                    sOrderType4Inv = "EX"
                End If

            End If



            mystrInvoice.SerialNoPrefix = mystrInvoice.Shipper.ToString + sOrderType4Inv.ToString + FinYear.ToString


            Dim daSelInvNo As New SqlDataAdapter("Select Count(ID) As InvCount From Invoice Where SerialNoPrefix = '" & mystrInvoice.SerialNoPrefix & "'", sConstr)
            Dim dsSelInvNo As New DataSet
            daSelInvNo.Fill(dsSelInvNo)

            nInvoiceCount = Val(dsSelInvNo.Tables(0).Rows(0).Item("InvCount").ToString) + 1

            mystrInvoice.InvoiceNo = mystrInvoice.SerialNoPrefix.ToString + "/" + ((nInvoiceCount.ToString).PadLeft(5, "0"))

            mystrInvoice.InvoiceDate = Date.Now

            mystrInvoice.Account = dsSelSO.Tables(0).Rows(0).Item("Buyercode").ToString



            mystrInvoice.Origin = ""
            mystrInvoice.LCNo = ""
            'mystrInvoice.Marks1 = ""
            mystrInvoice.Marks4 = ""
            mystrInvoice.Marks7 = ""
            mystrInvoice.Marks8 = ""
            mystrInvoice.ModeOfShipment = "ROAD"
            mystrInvoice.Destination = dsSelBuyer.Tables(0).Rows(0).Item("State").ToString
            mystrInvoice.Bank = ""
            mystrInvoice.Currency = dsSelSO.Tables(0).Rows(0).Item("Currency").ToString

            If mystrInvoice.Currency = "INR" Then
                mystrInvoice.CurrencyConversion = 1
                mystrInvoice.InvoiceType = "LOCAL"
            Else
                Dim daSelExRate As New SqlDataAdapter("Select * from CurrencyExchangeRate Where CurrencyCode = '" & mystrInvoice.Currency & _
                                                      "' And IsActive = '1' And Type = 'CC'", sConstr)
                Dim dsSelExRate As New DataSet
                daSelExRate.Fill(dsSelExRate)

                mystrInvoice.CurrencyConversion = Val(dsSelExRate.Tables(0).Rows(0).Item("ExportRate").ToString)
                mystrInvoice.InvoiceType = "EXPORT"
            End If

            mystrInvoice.Nature = "NA"
            mystrInvoice.Quantity = 0
            mystrInvoice.TotalValue = 0
            mystrInvoice.Freight = 0
            mystrInvoice.Insurance = 0
            mystrInvoice.TotalPackNo = 0
            mystrInvoice.NetWeight = 0
            mystrInvoice.GrossWeight = 0
            mystrInvoice.remark1 = ""
            mystrInvoice.status = "INVOICED"
            'mystrInvoice.shipdate = ""
            mystrInvoice.cntrycode = dsSelBuyer.Tables(0).Rows(0).Item("CountryCode").ToString
            mystrInvoice.Origin = dsSelBuyer.Tables(0).Rows(0).Item("CountryName").ToString
            'mystrInvoice.printdt = ""
            mystrInvoice.percent = 0
            mystrInvoice.plusamt = 0

            mystrInvoice.days = 0
            'mystrInvoice.due_date = ""
            mystrInvoice.accounted = "GST"
            mystrInvoice.depb = ""
            mystrInvoice.depbamt = 0
            mystrInvoice.depbrcvd = 0
            'mystrInvoice.depbrcvdon = "" 'Date
            mystrInvoice.depbper = 0 'decimal
            mystrInvoice.licsoldfor = 0
            mystrInvoice.port = ""
            mystrInvoice.forbillamt = 0 'decimal

            Dim daSelCompInfo As New SqlDataAdapter("Select * from UnitMaster Where CompanyCode = '" & mystrInvoice.Shipper & "'", sConstr)
            Dim dsSelcompInfo As New DataSet
            daSelCompInfo.Fill(dsSelcompInfo)

            mystrInvoice.PAN_Number = dsSelcompInfo.Tables(0).Rows(0).Item("CompanyPAN").ToString
            mystrInvoice.VAT_TIN = dsSelcompInfo.Tables(0).Rows(0).Item("TIN").ToString
            mystrInvoice.CST_TIN = dsSelcompInfo.Tables(0).Rows(0).Item("CSTNo").ToString
            mystrInvoice.ExciseDuty = 0
            mystrInvoice.VATAmount = 0
            mystrInvoice.CessAmount = 0
            mystrInvoice.EduCessAmount = 0
            mystrInvoice.BuyerGroup = dsSelBuyer.Tables(0).Rows(0).Item("BuyerGroupCode").ToString
            mystrInvoice.MinusAmount = 0
            mystrInvoice.Shipped = 0
            mystrInvoice.IsShipped = 0
            'mystrInvoice.ShippedDate = "" 'Date
            mystrInvoice.ExciseInvoiceNo = ""

            mystrInvoice.CreatedBy = "" 'String
            mystrInvoice.CreatedDate = Date.Now 'Date
            mystrInvoice.ModifiedBy = "" 'String
            mystrInvoice.ModifiedDate = Date.Now 'Date
            mystrInvoice.EnteredOnMachineID = mdlSGM.strSystemName
            mystrInvoice.Percentage = 0 'decimal
            mystrInvoice.Amount = 0 'decimal
            mystrInvoice.ModuleName = "Invoice" 'String
            mystrInvoice.ID = System.Guid.NewGuid.ToString()
            mystrInvoice.ConsigneeCode = dsSelBuyer.Tables(0).Rows(0).Item("BuyerCode").ToString
            mystrInvoice.Notify1 = ""

            sEmployeeCode = cbxSignatureBy.SelectedValue

            Dim daSelEmp As New SqlDataAdapter("Select * from Employee Where EmpCode = '" & sEmployeeCode & "'", sConstr)
            Dim dsSelEmp As New DataSet
            daSelEmp.Fill(dsSelEmp)

            mystrInvoice.AuthSignEmpCode = sEmployeeCode
            mystrInvoice.AuthSignEmpName = dsSelEmp.Tables(0).Rows(0).Item("EmpFullName").ToString
            mystrInvoice.AuthSignDesi = dsSelEmp.Tables(0).Rows(0).Item("Designation").ToString
            mystrInvoice.CSTorVAT = 0 'decimal
            mystrInvoice.EduCess = 0 'decimal
            mystrInvoice.CESS = 0 'decimal
            mystrInvoice.Excise = 0 'decimal
            mystrInvoice.CT3No = "" 'String
            'mystrInvoice.CT3Date = "" 'Date
            mystrInvoice.ARENo = "" 'String
            'mystrInvoice.AREDate = "" 'Date
            mystrInvoice.ConveredBy = "" 'String
            mystrInvoice.ContainerSealNo = "" 'String
            mystrInvoice.GoodsDescription = "" 'String
            mystrInvoice.MarksAndNos = "" 'String
            mystrInvoice.PortDischarge = "" 'String
            mystrInvoice.DestinationCountry = "" 'String
            mystrInvoice.PaymentTerms = "" 'String
            mystrInvoice.FromPackNo = "" 'String
            mystrInvoice.ToPackNo = "" 'String
            mystrInvoice.GSPSlNo = "" 'String
            mystrInvoice.CartonDia = "" 'String
            mystrInvoice.OneCarton = "" 'String
            mystrInvoice.TotalOne = "" 'String
            mystrInvoice.FinalDestination = "" 'String
            mystrInvoice.MatType = "" 'String
            mystrInvoice.AmtOfDutyPayable = 0 'decimal
            mystrInvoice.SubTotal = 0 'decimal
            mystrInvoice.InvYear = FinYear
            mystrInvoice.InvCode = sOrderType4Inv
            mystrInvoice.LCID = "" 'String
            mystrInvoice.LCValue = 0 'decimal
            mystrInvoice.ShippedLCValue = 0 'decimal
            mystrInvoice.CGSTPercentage = 0 'decimal
            mystrInvoice.CGSTValue = 0 'decimal
            mystrInvoice.SGSTPercentage = 0 'decimal
            mystrInvoice.SGSTVlaue = 0 'decimal
            mystrInvoice.IGSTPercentage = 0 'decimal
            mystrInvoice.IGSTValue = 0 'decimal
            mystrInvoice.FreightCharges = 0 'decimal
            mystrInvoice.GSTTotalValue = 0 'decimal
            mystrInvoice.GSTInvNo = "" 'String
            mystrInvoice.InvNo2 = "" 'String
            mystrInvoice.InvNo3 = "" 'String
            mystrInvoice.FreightCGSTPer = 0 'decimal
            mystrInvoice.FreightCGSTVal = 0 'decimal
            mystrInvoice.FreightSGSTPer = 0 'decimal
            mystrInvoice.FreightSGSTVal = 0 'decimal
            mystrInvoice.FreightIGSTPer = 0 'decimal
            mystrInvoice.FreightIGSTVal = 0 'decimal
            mystrInvoice.FreightTotalVal = 0 'decimal
            mystrInvoice.GSTValue = 0 'decimal
            mystrInvoice.InternalOrder = "" 'String



            If myccInvoice.InsertInvoiceMain(mystrInvoice) = True Then
                Me.DialogResult = DialogResult.OK
                sUpdateMode = "Added"
                mdlSGM.sInvoiceNo = mystrInvoice.InvoiceNo
                sUpdateModeinAudit = "Added"
                UpdateInvoiceAuditDatabase()
            End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Private Sub GenerateInvoiceDetails()
        Try
            Dim daSelSOD1 As New SqlDataAdapter("Select * from SalesOrderDetails Where Id = '" & sSalesOrderDetailId & "'", sConstr)
            Dim dsSelSOD1 As New DataSet
            daSelSOD1.Fill(dsSelSOD1)

            nOrderQuantity = Val(dsSelSOD1.Tables(0).Rows(0).Item("OrderQuantity").ToString)
            sOrderStatus = dsSelSOD1.Tables(0).Rows(0).Item("OrderStatus").ToString
            sSalesOrderNo = dsSelSOD1.Tables(0).Rows(0).Item("SalesOrderNo").ToString


            Dim daSelSO As New SqlDataAdapter("Select * from SalesOrder Where Id = '" & sSalesOrderId & "'", sConstr)
            Dim dsSelSO As New DataSet
            daSelSO.Fill(dsSelSO)

            sBuyerCode = dsSelSO.Tables(0).Rows(0).Item("Buyercode").ToString

            Dim daSelBuyer As New SqlDataAdapter("Select * from Buyer  Where BuyerCode = '" & sBuyerCode & "'", sConstr)
            Dim dsSelBuyer As New DataSet
            daSelBuyer.Fill(dsSelBuyer)

            Dim daSelGST As New SqlDataAdapter("Select * from GoodsandServicesTax Where Type = '" & sOOrderType & "'", sConstr)
            Dim dsSelGST As New DataSet
            daSelGST.Fill(dsSelGST)

            If sOrderType = "IO" Then
                Dim daSelOrdType As New SqlDataAdapter("SELECT Inv.WareHouse, SOD.ActualOrderNo, ISNULL(SOD.ActualOrderType, '') AS ActualOrderType, ISNULL(SOD.Type, '') AS Type FROM dbo.InvoiceDetail AS Inv INNER JOIN dbo.CreditNoteForRejRepDetails AS CRN ON Inv.invoiceno = CRN.InvoiceNo INNER JOIN dbo.SalesOrderDetails AS SOD ON Inv.SalesOrderDetailID = SOD.ID Where crn.NoteNumber = '" & sCustomerOrderNo & "'", sConstr)
                Dim dsSelOrdType As New DataSet
                daSelOrdType.Fill(dsSelOrdType)

                If dsSelOrdType.Tables(0).Rows(0).Item("ActualOrderType").ToString = "" Then
                    sOOrderType = dsSelOrdType.Tables(0).Rows(0).Item("Type").ToString
                Else
                    sOOrderType = dsSelOrdType.Tables(0).Rows(0).Item("ActualOrderType").ToString
                End If

                sOrderTypeforHSNCode = sOOrderType
            End If

            Dim daSelHSN As New SqlDataAdapter("Select * from GSTHsnCodeDtls Where Type = '" & sOrderTypeforHSNCode & _
                                               "' And MaterialName = '" & mystrInvoice.Marks1 & "'", sConstr)
            Dim dsSelHSN As New DataSet
            daSelHSN.Fill(dsSelHSN)

            mystrInvoiceDetails.invoiceno = mystrInvoice.InvoiceNo
            mystrInvoiceDetails.HSNCode = dsSelHSN.Tables(0).Rows(0).Item("HSNCode").ToString
            'Dim nRowCount As Integer
            'nRowCount = dsSelHSN.Tables(0).Rows.Count

            mystrInvoiceDetails.InvoiceDate = mystrInvoice.InvoiceDate
            mystrInvoiceDetails.InvoiceSerialNo = nInvoiceSerialNo.ToString.PadLeft(2, "0")
            mystrInvoiceDetails.buyer = mystrInvoice.Buyer
            mystrInvoiceDetails.shipper = mystrInvoice.Shipper
            mystrInvoiceDetails.SalesOrderNo = dsSelSOD1.Tables(0).Rows(0).Item("SalesOrderNo").ToString
            mystrInvoiceDetails.type = "0"
            mystrInvoiceDetails.ArticleNo = dsSelSOD1.Tables(0).Rows(0).Item("Article").ToString
            mystrInvoiceDetails.rate = dsSelSOD1.Tables(0).Rows(0).Item("Price").ToString
            mystrInvoiceDetails.quantity = nQuantity
            mystrInvoiceDetails.ratioqty = 0
            mystrInvoiceDetails.currency = mystrInvoice.Currency
            mystrInvoiceDetails.CurrencyConversionRate = mystrInvoice.CurrencyConversion
            mystrInvoiceDetails.category = ""
            mystrInvoiceDetails.value = Math.Round((mystrInvoiceDetails.rate * mystrInvoiceDetails.quantity), 2)
            mystrInvoiceDetails.size1 = sSize01
            mystrInvoiceDetails.qty1 = nQuantity01
            mystrInvoiceDetails.size2 = sSize02
            mystrInvoiceDetails.qty2 = nQuantity02
            mystrInvoiceDetails.size3 = sSize03
            mystrInvoiceDetails.qty3 = nQuantity03
            mystrInvoiceDetails.size4 = sSize04
            mystrInvoiceDetails.qty4 = nQuantity04
            mystrInvoiceDetails.size5 = sSize05
            mystrInvoiceDetails.qty5 = nQuantity05
            mystrInvoiceDetails.size6 = sSize06
            mystrInvoiceDetails.qty6 = nQuantity06
            mystrInvoiceDetails.size7 = sSize07
            mystrInvoiceDetails.qty7 = nQuantity07
            mystrInvoiceDetails.size8 = sSize08
            mystrInvoiceDetails.qty8 = nQuantity08
            mystrInvoiceDetails.size9 = sSize09
            mystrInvoiceDetails.qty9 = nQuantity09
            mystrInvoiceDetails.size10 = sSize10
            mystrInvoiceDetails.qty10 = nQuantity10
            mystrInvoiceDetails.CountryCode = mystrInvoice.cntrycode
            mystrInvoiceDetails.curvalue = mystrInvoice.CurValue
            mystrInvoiceDetails.size11 = sSize11
            mystrInvoiceDetails.size12 = sSize12
            mystrInvoiceDetails.size13 = sSize13
            mystrInvoiceDetails.size14 = sSize14
            mystrInvoiceDetails.size15 = sSize15
            mystrInvoiceDetails.size16 = sSize16
            mystrInvoiceDetails.size17 = sSize17
            mystrInvoiceDetails.size18 = sSize18
            mystrInvoiceDetails.qty11 = nQuantity11
            mystrInvoiceDetails.qty12 = nQuantity12
            mystrInvoiceDetails.qty13 = nQuantity13
            mystrInvoiceDetails.qty14 = nQuantity14
            mystrInvoiceDetails.qty15 = nQuantity15
            mystrInvoiceDetails.qty16 = nQuantity16
            mystrInvoiceDetails.qty17 = nQuantity17
            mystrInvoiceDetails.qty18 = nQuantity18
            mystrInvoiceDetails.BuyerGroup = mystrInvoice.BuyerGroup
            mystrInvoiceDetails.IsShipped = 0
            mystrInvoiceDetails.burdept = ""
            mystrInvoiceDetails.CreatedBy = ""
            mystrInvoiceDetails.CreatedDate = Date.Now
            mystrInvoiceDetails.ModifiedBy = ""
            mystrInvoiceDetails.ModifiedDate = Date.Now
            mystrInvoiceDetails.EnteredOnMachineID = mdlSGM.strSystemName
            mystrInvoiceDetails.JobCardNo = sJobcardNo
            mystrInvoiceDetails.ModuleName = ""
            mystrInvoiceDetails.JobCardDetailID = sJobcardDetailID
            mystrInvoiceDetails.InvoiceID = mystrInvoice.ID
            mystrInvoiceDetails.ID = System.Guid.NewGuid.ToString()
            mystrInvoiceDetails.SalesOrderDetailID = sSalesOrderDetailId
            If mystrInvoice.Destination = "TAMIL NADU" Then
                mystrInvoiceDetails.CGSTPercentage = Val(dsSelGST.Tables(0).Rows(0).Item("CGST").ToString)
                mystrInvoiceDetails.SGSTPercentage = Val(dsSelGST.Tables(0).Rows(0).Item("SGST").ToString)
                mystrInvoiceDetails.IGSTPercentage = 0
            Else
                mystrInvoiceDetails.CGSTPercentage = 0
                mystrInvoiceDetails.SGSTPercentage = 0
                mystrInvoiceDetails.IGSTPercentage = Val(dsSelGST.Tables(0).Rows(0).Item("IGST").ToString)
            End If
            'mystrInvoiceDetails.value = Math.Round((mystrInvoiceDetails.rate * mystrInvoiceDetails.quantity), 2)


            mystrInvoiceDetails.CGSTValue = Math.Round((mystrInvoiceDetails.value * (mystrInvoiceDetails.CGSTPercentage / 100)), 2)
            mystrInvoiceDetails.SGSTValue = Math.Round((mystrInvoiceDetails.value * (mystrInvoiceDetails.SGSTPercentage / 100)), 2)
            mystrInvoiceDetails.IGSTValue = Math.Round((mystrInvoiceDetails.value * (mystrInvoiceDetails.IGSTPercentage / 100)), 2)


            mystrInvoiceDetails.WareHouse = mystrInvoice.warehouse
            mystrInvoiceDetails.FValue = 0
            mystrInvoiceDetails.Ready2DispatchID = sR2DID
            mystrInvoiceDetails.InternalOrder = dsSelSOD1.Tables(0).Rows(0).Item("RejectionId").ToString
            mystrInvoiceDetails.IsSampleOrder = Val(dsSelSOD1.Tables(0).Rows(0).Item("IsSampleOrder")) * -1
            mystrInvoiceDetails.SampleOrderType = dsSelSOD1.Tables(0).Rows(0).Item("SampleType").ToString
            mystrInvoiceDetails.NettValue = Math.Round((mystrInvoiceDetails.value + mystrInvoiceDetails.CGSTValue + mystrInvoiceDetails.SGSTValue + mystrInvoiceDetails.IGSTValue), 2)

            Dim daSelMat As New SqlDataAdapter("Select * from Materials Where Materialcode = '" & mystrInvoiceDetails.ArticleNo & "'", sConstr)
            Dim dsSelMat As New DataSet
            daSelMat.Fill(dsSelMat)


            mystrInvoiceDetails.ArticleandColor = dsSelMat.Tables(0).Rows(0).Item("MaterialName").ToString + " " + dsSelMat.Tables(0).Rows(0).Item("MaterialColorDescription").ToString

            nProductValue = nProductValue + mystrInvoiceDetails.value
            nGSTValue = nGSTValue + mystrInvoiceDetails.CGSTValue + mystrInvoiceDetails.SGSTValue + mystrInvoiceDetails.IGSTValue
            nSGSTValue = nSGSTValue + mystrInvoiceDetails.SGSTValue
            nCGSTValue = nCGSTValue + mystrInvoiceDetails.CGSTValue
            nIGSTValue = nIGSTValue + mystrInvoiceDetails.IGSTValue
            nInvoiceQuantity = nInvoiceQuantity + mystrInvoiceDetails.quantity



            If myccInvoice.InsertInvoiceDetails(mystrInvoiceDetails) = True Then
                Me.DialogResult = DialogResult.OK
                Postings()
                sUpdateMode = "Added"
                'UpdateInvoiceDetailAuditDatabase()
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
    Dim nInvTotalValue As Decimal
    Private Sub UpdateInvoiceValue()
        Try
            'mystrInvoice.TCSPercentage = 0
            'If sOrderType4Inv = "LS" Or sOrderType4Inv = "CS" Then
            '    Dim daSelInvTotalValue As New SqlDataAdapter("Select IsNull(Sum(forbillamt),0) AS ForBillAmount  from Invoice Where FinancialYear = '" & FinYear & _
            '                                             "' And GSTNO = '" & sGSTNo & "'", sConstr)
            '    Dim dsSelInvTotalValue As New DataSet
            '    daSelInvTotalValue.Fill(dsSelInvTotalValue)

            '    nInvTotalValue = Val(dsSelInvTotalValue.Tables(0).Rows(0).Item("ForBillAmount").ToString)

            '    If nTotalValue > 500000 Then
            '        mystrInvoice.TCSPercentage = "0.075"
            '    End If
            'End If



            mystrInvoice.Quantity = nInvoiceQuantity
            mystrInvoice.TotalValue = nProductValue
            mystrInvoice.CGSTPercentage = mystrInvoiceDetails.CGSTPercentage
            mystrInvoice.CGSTValue = nCGSTValue
            mystrInvoice.SGSTPercentage = mystrInvoiceDetails.SGSTPercentage
            mystrInvoice.SGSTVlaue = nSGSTValue
            mystrInvoice.IGSTPercentage = mystrInvoiceDetails.IGSTPercentage
            mystrInvoice.IGSTValue = nIGSTValue
            mystrInvoice.GSTTotalValue = nCGSTValue + nSGSTValue + nIGSTValue
            mystrInvoice.InvoiceValue = mystrInvoice.TotalValue + mystrInvoice.GSTTotalValue
            mystrInvoice.TCSValue = (mystrInvoice.InvoiceValue * mystrInvoice.TCSPercentage) / 100
            mystrInvoice.forbillamt = mystrInvoice.InvoiceValue + mystrInvoice.TCSValue

            myccInvoice.UpdateInvoiceMain(mystrInvoice)
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nShippedQuantity As Integer
    Dim sOrderStatus As String
    Private Sub Postings()
        Try
            Dim daSelShpdQtySOD As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity  from InvoiceDetail where SalesOrderDetailID = '" & sSalesOrderDetailId & "'", sConstr)
            Dim dsSelShpdQtySOD As New DataSet
            daSelShpdQtySOD.Fill(dsSelShpdQtySOD)

            nShippedQuantity = Val(dsSelShpdQtySOD.Tables(0).Rows(0).Item("Quantity").ToString)

            If nShippedQuantity >= nOrderQuantity Then
                sOrderStatus = "Shipped"
            End If

            myccInvoice.UpdateSalesOrderDetails(sSalesOrderDetailId, nShippedQuantity, sOrderStatus, mystrInvoice.InvoiceDate)

            Dim daSelShpdQtySO As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity  from InvoiceDetail where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
            Dim dsSelShpdQtySO As New DataSet
            daSelShpdQtySO.Fill(dsSelShpdQtySO)

            nShippedQuantity = Val(dsSelShpdQtySO.Tables(0).Rows(0).Item("Quantity").ToString)

            Dim daSelSO As New SqlDataAdapter("Select * from Salesorder Where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
            Dim dsSelSO As New DataSet
            daSelSO.Fill(dsSelSO)

            If nShippedQuantity >= Val(dsSelSO.Tables(0).Rows(0).Item("TotalOrderQuantity").ToString) Then
                sOrderStatus = "Shipped"
            Else
                sOrderStatus = "PartialShipped"
            End If

            myccInvoice.UpdateSalesOrder(sSalesOrderNo, nShippedQuantity, sOrderStatus)
            myccInvoice.UpdateReadyToDispatch(sR2DID, mystrInvoice.InvoiceNo, mystrInvoice.InvoiceDate)
            myccInvoice.UpdatePackingDetail(sJobcardNo, mystrInvoice.InvoiceNo, mystrInvoice.ID, FinYear)
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub grdReadyToDispatchV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdReadyToDispatchV1.RowStyle
        If sIsLoaded = "Y" And mystrInvoice.InvoiceNo <> "" Then
            If e.RowHandle > -1 Then
                If grdReadyToDispatchV1.GetRowCellValue(e.RowHandle, grdReadyToDispatchV1.Columns(5)).ToString() = mystrInvoice.InvoiceNo Then
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.ForeColor = Color.DarkRed
                    e.Appearance.Font = New System.Drawing.Font(e.Appearance.Font, FontStyle.Bold)
                End If
            End If
        End If
    End Sub

    Dim nSelecCount As Integer
    Dim sDeleteOption As string
    Private Sub cbDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDelete.Click
        Try
            mdlSGM.sMode = "DELETE"
            i = 0
            sIsSelected = "N"
            nSelecCount = 0
            ngrdRowCount = grdReadyToDispatchV1.RowCount

            For i = 0 To ngrdRowCount - 1
                If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                    sIsSelected = "Y"
                    nSelecCount = nSelecCount + 1
                End If
            Next

            ''Code for Checking Selected Or Not
            If sIsSelected = "N" Then : MsgBox("No Rows Selected. Cannot Delete Invoice!", MsgBoxStyle.Critical) : Exit Sub : End If
            If nSelecCount > 1 Then : MsgBox("Multiple Invoices Selected. Cannot Delete Invoice!", MsgBoxStyle.Critical) : Exit Sub : End If
            ''Code for Checking Selected Or Not

            CheckforInvoiceStatus()
            ''Code for Checking Invoice Generated Or Not
            If sInvoiceGenerated = "N" Then : MsgBox("Invoice Not Generated for Row Selected", MsgBoxStyle.Critical) : Exit Sub : End If
            ''Code for Checking Invoice Generated Or Not

            i = 0

            For i = 0 To ngrdRowCount - 1
                If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                    sIsSelected = "Y"
                    Exit For
                End If
            Next

            nYesNo = MsgBox("R U Sure U Want to Delete the Selected Invoice", MsgBoxStyle.YesNo)

            If nYesNo = 6 Then



                mystrInvoice.InvoiceNo = grdReadyToDispatchV1.GetDataRow(i).Item("InvoiceNo").ToString


                mystrInvoice.SerialNoPrefix = Microsoft.VisualBasic.Left(mystrInvoice.InvoiceNo, 10)

                Dim daSelInvNo As New SqlDataAdapter("Select Count(ID) As InvCount From Invoice Where SerialNoPrefix = '" & mystrInvoice.SerialNoPrefix & "'", sConstr)
                Dim dsSelInvNo As New DataSet
                daSelInvNo.Fill(dsSelInvNo)

                nInvoiceCount = Val(dsSelInvNo.Tables(0).Rows(0).Item("InvCount").ToString)

                If mystrInvoice.InvoiceNo <> mystrInvoice.SerialNoPrefix.ToString + "/" + ((nInvoiceCount.ToString).PadLeft(5, "0")) Then
                    nYesNo = MsgBox("Selected Invoice Is not the Last Invoice. Hence Cannot be Deleted. Only Cancel Can be done?", MsgBoxStyle.YesNo)

                    If nYesNo = 6 Then
                        mdlSGM.sMode = "CANCEL"
                        CancelInvoiceAuditDatabase()
                    Else
                        mystrInvoice.InvoiceNo = ""
                        Exit Sub
                    End If
                End If
                DeleteInvoiceAuditDatabase()
                DeletePostings()

                LoadReadytoDispatchSummary()
            Else
                MsgBox("No Invoice Deleted", MsgBoxStyle.Information)
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
    Private Sub CancelInvoiceAuditDatabase()
        Try


            Dim daInsInvinAudit As New SqlDataAdapter("Insert Into Invoice SELECT ID, Shipper, InvoiceNo, InvoiceDate, Buyer, Account, BuyerDepartment, Origin, LCNo, InvoiceDescription, ShippingBillNo, ShippingBillDate, Marks1, Marks2, Marks3, Marks4, Marks5, Marks6, Marks7, Marks8, ModeOfShipment, AwbBillNo, AwbBillDate, HAwbBillNo, HAwbBillDate, Destination, BanckAccount, Vessel, Bank, Currency, CurrencyConversion, Nature, Quantity, TotalValue, Freight, Insurance, AgentPercentage, Commission, DbkPercentage, DrawBack, BankCertificate, BankAmount, BankRate, CertificateDate, TotalPackNo, harmnsdco, NetWeight, GrossWeight, dbkrecd, aepccer_no, cust_clear, rbicode, cot_rayon, shpblfob, netfobamt, remark1, status, shipdate, aepcdt, drwrecddt, negdate, markrate, cntrycode, realstdt, usha, ugts, printdt, [percent], plusamt, cntryname, days, due_date, accounted, accountamt, mode, ShipRate, courier, docawb, docsentdt, advdocdt, inspdate, advdocdays, tokenno, tokendt, qlfbonus, midamount, depb, depbamt, depbrcvd, depbrcvdon, depbper, licencenr, licenceamt, licsoldon, licsoldfor, depbappldt, port, epcopydt, forwarder, forbillno, forbillamt, forrcvdt, sentforver, verifieddt, suppnr, suppbbnnr, warehouse, swiftcode, CorrespondingBank, CorrespondingBankAcount, CorrespondingBankSwiftCode, Msrks9, Marks10, STRPercentage, STRAmount, PAN_Number, VAT_TIN, CST_TIN, ExciseDuty, VATAmount, CessAmount, EduCessAmount, BuyerGroup, MinusAmount, FinancialYear, InvoiceType, Shipped, IsShipped, ShippedDate, ShippedBy, MarkToShipDoneDate, PaymentReceiveFromBuyer, PaymentReceiveDate, BankRefNo, BankRefDate, ContractNo, ContractDate, ExciseInvoiceNo, IsAdvanceReceived, AdvanceAmount, CreatedBy, CreatedDate, ModifiedBy, GetDate(), EnteredOnMachineID, HandoverDate, HangerPack, Covering, Declaration, PostDescription, LCDescription, Mark11, Mark12, DiscountUpCharge, Percentage, Amount, ShipperLC, BuyerLC, FOBValueInCurrency, TaxType, IsRecalRequired, IsApproved, ApprovedBy, ApprovedOn, ModuleName, 'Cancel', ConsigneeCode, Notify1, Notify2, Notify3, AuthSignEmpCode, AuthSignEmpName, AuthSignDesi, CSTorVAT, EduCess, CESS, Excise, CT3No, CT3Date, ARENo, AREDate, ConveredBy, Declaration1, ContainerSize, ContainerName, ContainerSealNo, GoodsDescription, MarksAndNos, NoAndKindOfPackages, PreCarriageBy, PreCarrierRecvPlace, PortDischarge, DestinationCountry, AssortmentYear, PaymentTerms, DeliveryTerms, AREDuty, AreCess, AREHCess, ContainerNo, FromPackNo, ToPackNo, EmailToCustDate, FreightFwdrPlotLetterDate, ContainerApplnDate, GSPSlNo, OriginCriterion, StuffingDate, GateOpeningDate, CutOffDate, ClosingDate, SailingVesselDetails, ShipmentType, RevShipmentType, HaltingCharges, Demurrage, VoyageNo, ETDFeederVessel, ETAFeederVessel, FeederVessel, FeederVoyageNo, ETAMotherVessel, ETDMotherVessel, InternalSealNo, CentralExciseSealNo, TypesOfBL, CustomClearence, TransportCharges, CHACharges, ClearingCharges, CFSCharges, ForwardingCharges, GSpCharges, CourierCharges, InsuranceCharges, MiscCharges, ContainerArrivalDate, Memo, DestinationArrivalDate, FreightFwdrPlotLetterExpDate, ForwarderCode, CHACode, RevVessel, RevVoyageNo, RevETDMotherVessel, RevETAMotherVessel, RevFeederVessel, RevFeederVoyageNo, RevETDFeederVessel, RevETAFeederVessel, Commodity, PremiumRate, PremiumAmount, InvoiceNoAutoGen, CourierPayment, CourierNo, CourierDate, CartonDia, OneCarton, TotalOne, FinalDestination, MatType, AmtOfDutyPayable, SubTotal, InvYear, InvCode, DispatchFrom, InoviceStatus, ExpDocDate, GoodsDescription2, RBBankName, RBAccountNo, RBSwiftCode, CurValue, AnnexureAPortOfLoading, LCID, LCValue, ShippedLCValue, CGSTPercentage, CGSTValue, SGSTPercentage, SGSTVlaue, IGSTPercentage, IGSTValue, FreightCharges, LoadingCharges, InsuranceChager, OtherCharges, Discount, GSTTotalValue, GSTInvNo, InvNo2, InvNo3, DUMMYINVDATE, GSTValue, SerialNoPrefix, InternalOrder FROM AHGroup_SSPL.dbo.INVOICE WHERE (InvoiceNo = '" & mdlSGM.sInvoiceNo & "')", sConstrAudit)
            Dim dsInsInvinAudit As New DataSet
            daInsInvinAudit.Fill(dsInsInvinAudit)
            dsInsInvinAudit.AcceptChanges()


        Catch exp As Exception
            HandleException(Me.Name, exp)
        End Try

    End Sub

    Private Sub DeleteInvoiceAuditDatabase()
        Try


            Dim daInsInvinAudit As New SqlDataAdapter("Insert Into Invoice SELECT ID, Shipper, InvoiceNo, InvoiceDate, Buyer, Account, BuyerDepartment, Origin, LCNo, InvoiceDescription, ShippingBillNo, ShippingBillDate, Marks1, Marks2, Marks3, Marks4, Marks5, Marks6, Marks7, Marks8, ModeOfShipment, AwbBillNo, AwbBillDate, HAwbBillNo, HAwbBillDate, Destination, BanckAccount, Vessel, Bank, Currency, CurrencyConversion, Nature, Quantity, TotalValue, Freight, Insurance, AgentPercentage, Commission, DbkPercentage, DrawBack, BankCertificate, BankAmount, BankRate, CertificateDate, TotalPackNo, harmnsdco, NetWeight, GrossWeight, dbkrecd, aepccer_no, cust_clear, rbicode, cot_rayon, shpblfob, netfobamt, remark1, status, shipdate, aepcdt, drwrecddt, negdate, markrate, cntrycode, realstdt, usha, ugts, printdt, [percent], plusamt, cntryname, days, due_date, accounted, accountamt, mode, ShipRate, courier, docawb, docsentdt, advdocdt, inspdate, advdocdays, tokenno, tokendt, qlfbonus, midamount, depb, depbamt, depbrcvd, depbrcvdon, depbper, licencenr, licenceamt, licsoldon, licsoldfor, depbappldt, port, epcopydt, forwarder, forbillno, forbillamt, forrcvdt, sentforver, verifieddt, suppnr, suppbbnnr, warehouse, swiftcode, CorrespondingBank, CorrespondingBankAcount, CorrespondingBankSwiftCode, Msrks9, Marks10, STRPercentage, STRAmount, PAN_Number, VAT_TIN, CST_TIN, ExciseDuty, VATAmount, CessAmount, EduCessAmount, BuyerGroup, MinusAmount, FinancialYear, InvoiceType, Shipped, IsShipped, ShippedDate, ShippedBy, MarkToShipDoneDate, PaymentReceiveFromBuyer, PaymentReceiveDate, BankRefNo, BankRefDate, ContractNo, ContractDate, ExciseInvoiceNo, IsAdvanceReceived, AdvanceAmount, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, EnteredOnMachineID, HandoverDate, HangerPack, Covering, Declaration, PostDescription, LCDescription, Mark11, Mark12, DiscountUpCharge, Percentage, Amount, ShipperLC, BuyerLC, FOBValueInCurrency, TaxType, IsRecalRequired, IsApproved, ApprovedBy, ApprovedOn, ModuleName, 'Deleted', ConsigneeCode, Notify1, Notify2, Notify3, AuthSignEmpCode, AuthSignEmpName, AuthSignDesi, CSTorVAT, EduCess, CESS, Excise, CT3No, CT3Date, ARENo, AREDate, ConveredBy, Declaration1, ContainerSize, ContainerName, ContainerSealNo, GoodsDescription, MarksAndNos, NoAndKindOfPackages, PreCarriageBy, PreCarrierRecvPlace, PortDischarge, DestinationCountry, AssortmentYear, PaymentTerms, DeliveryTerms, AREDuty, AreCess, AREHCess, ContainerNo, FromPackNo, ToPackNo, EmailToCustDate, FreightFwdrPlotLetterDate, ContainerApplnDate, GSPSlNo, OriginCriterion, StuffingDate, GateOpeningDate, CutOffDate, ClosingDate, SailingVesselDetails, ShipmentType, RevShipmentType, HaltingCharges, Demurrage, VoyageNo, ETDFeederVessel, ETAFeederVessel, FeederVessel, FeederVoyageNo, ETAMotherVessel, ETDMotherVessel, InternalSealNo, CentralExciseSealNo, TypesOfBL, CustomClearence, TransportCharges, CHACharges, ClearingCharges, CFSCharges, ForwardingCharges, GSpCharges, CourierCharges, InsuranceCharges, MiscCharges, ContainerArrivalDate, Memo, DestinationArrivalDate, FreightFwdrPlotLetterExpDate, ForwarderCode, CHACode, RevVessel, RevVoyageNo, RevETDMotherVessel, RevETAMotherVessel, RevFeederVessel, RevFeederVoyageNo, RevETDFeederVessel, RevETAFeederVessel, Commodity, PremiumRate, PremiumAmount, InvoiceNoAutoGen, CourierPayment, CourierNo, CourierDate, CartonDia, OneCarton, TotalOne, FinalDestination, MatType, AmtOfDutyPayable, SubTotal, InvYear, InvCode, DispatchFrom, InoviceStatus, ExpDocDate, GoodsDescription2, RBBankName, RBAccountNo, RBSwiftCode, CurValue, AnnexureAPortOfLoading, LCID, LCValue, ShippedLCValue, CGSTPercentage, CGSTValue, SGSTPercentage, SGSTVlaue, IGSTPercentage, IGSTValue, FreightCharges, LoadingCharges, InsuranceChager, OtherCharges, Discount, GSTTotalValue, GSTInvNo, InvNo2, InvNo3, DUMMYINVDATE, GSTValue, SerialNoPrefix, InternalOrder, SupplyType, AckNo, AckDate, QRCode, IRNNo, IsService, TableName, FreightCGSTVal, FreightCGSTPer, TCSValue, TCSPercentage, InvoiceValue, GSTNo, FreightTotalVal, FreightSGSTVal, FreightSGSTPer, FreightIGSTVal, FreightIGSTPer FROM AHGroup_SSPL.dbo.INVOICE WHERE (InvoiceNo = '" & mdlSGM.sInvoiceNo & "')", sConstrAudit)
            Dim dsInsInvinAudit As New DataSet
            daInsInvinAudit.Fill(dsInsInvinAudit)
            dsInsInvinAudit.AcceptChanges()


        Catch exp As Exception
            HandleException(Me.Name, exp)
        End Try

    End Sub
    Private Sub DeletePostings()
        Try
            myccInvoice.UpdatePackingDetailAfterDelete(sJobcardNo, mystrInvoice.InvoiceNo, mystrInvoice.ID)
            sUpdateMode = "Deleted"
            'UpdateInvoiceDetailAuditDatabase()
            myccInvoice.DeleteInvoiceDetail(mystrInvoice.InvoiceNo)
            If mdlSGM.sMode = "DELETE" Then
                sUpdateMode = "Deleted"
                'UpdateInvoiceAuditDatabase()
                myccInvoice.DeleteInvoiceMain(mystrInvoice.InvoiceNo)
            ElseIf mdlSGM.sMode = "CANCEL" Then
                myccInvoice.CancelInvoiceMain(mystrInvoice.InvoiceNo, cbxCancel.Text)
                sUpdateMode = "Modified"
                'UpdateInvoiceAuditDatabase()
            End If



            Dim daSelR2D As New SqlDataAdapter("Select * from ReadyToDispatch where InvoiceNo = '" & mystrInvoice.InvoiceNo & _
                                               "' Order by Createddate", sConstr)
            Dim dsSelR2D As New DataSet
            daSelR2D.Fill(dsSelR2D)

            Dim i As Integer = 0

            For i = 0 To dsSelR2D.Tables(0).Rows.Count - 1
                sSalesOrderDetailId = dsSelR2D.Tables(0).Rows(i).Item("SalesOrderDetailId").ToString

                Dim daSelSOD1 As New SqlDataAdapter("Select * from SalesOrderDetails Where Id = '" & sSalesOrderDetailId & "'", sConstr)
                Dim dsSelSOD1 As New DataSet
                daSelSOD1.Fill(dsSelSOD1)

                nOrderQuantity = Val(dsSelSOD1.Tables(0).Rows(0).Item("OrderQuantity").ToString)
                sOrderStatus = dsSelSOD1.Tables(0).Rows(0).Item("OrderStatus").ToString
                sSalesOrderNo = dsSelSOD1.Tables(0).Rows(0).Item("SalesOrderNo").ToString

                Dim daSelShpdQtySOD As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity  from InvoiceDetail where SalesOrderDetailID = '" & sSalesOrderDetailId & "'", sConstr)
                Dim dsSelShpdQtySOD As New DataSet
                daSelShpdQtySOD.Fill(dsSelShpdQtySOD)

                nShippedQuantity = Val(dsSelShpdQtySOD.Tables(0).Rows(0).Item("Quantity").ToString)

                If nShippedQuantity >= nOrderQuantity Then
                    sOrderStatus = "Shipped"
                Else
                    sOrderStatus = "OPEN"
                End If

                myccInvoice.UpdateSalesOrderDetails(sSalesOrderDetailId, nShippedQuantity, sOrderStatus, mystrInvoice.InvoiceDate)
                ''sSalesOrderNo = "T-18-0002"
                Dim daSelShpdQtySO As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity  from InvoiceDetail where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
                Dim dsSelShpdQtySO As New DataSet
                daSelShpdQtySO.Fill(dsSelShpdQtySO)

                nShippedQuantity = Val(dsSelShpdQtySO.Tables(0).Rows(0).Item("Quantity").ToString)

                Dim daSelSO As New SqlDataAdapter("Select * from Salesorder Where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
                Dim dsSelSO As New DataSet
                daSelSO.Fill(dsSelSO)

                If nShippedQuantity >= Val(dsSelSO.Tables(0).Rows(0).Item("TotalOrderQuantity").ToString) Then
                    sOrderStatus = "Shipped"
                Else
                    If nShippedQuantity = 0 Then
                        sOrderStatus = "OPEN"
                    Else
                        sOrderStatus = "PartialShipped"
                    End If

                End If

                myccInvoice.UpdateSalesOrder(sSalesOrderNo, nShippedQuantity, sOrderStatus)
            Next
            myccInvoice.UpdateReadyToDispatchAfterDelete(sR2DID, mystrInvoice.InvoiceNo, mystrInvoice.InvoiceDate)
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEdit.Click
        Try
            mdlSGM.sMode = "EDIT"
            i = 0
            sIsSelected = "N"
            nSelecCount = 0
            ngrdRowCount = grdReadyToDispatchV1.RowCount

            For i = 0 To ngrdRowCount - 1
                If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                    sIsSelected = "Y"
                    nSelecCount = nSelecCount + 1
                End If
            Next

            ''Code for Checking Selected Or Not
            If sIsSelected = "N" Then : MsgBox("No Rows Selected. Cannot Delete Invoice!", MsgBoxStyle.Critical) : Exit Sub : End If
            If nSelecCount > 1 Then : MsgBox("Multiple Invoices Selected. Cannot Delete Invoice!", MsgBoxStyle.Critical) : Exit Sub : End If
            ''Code for Checking Selected Or Not

            CheckforInvoiceStatus()
            ''Code for Checking Invoice Generated Or Not
            If sInvoiceGenerated = "N" Then : MsgBox("Invoice Not Generated for Row Selected", MsgBoxStyle.Critical) : Exit Sub : End If
            ''Code for Checking Invoice Generated Or Not
            i = 0

            For i = 0 To ngrdRowCount - 1
                If grdReadyToDispatchV1.GetDataRow(i).Item("Select").ToString = "True" Then
                    sIsSelected = "Y"
                    Exit For
                End If
            Next

            nYesNo = MsgBox("R U Sure U Want to Edit the Selected Invoice", MsgBoxStyle.YesNo)

            If nYesNo = 6 Then


                mdlSGM.sInvoiceNo = grdReadyToDispatchV1.GetDataRow(i).Item("InvoiceNo").ToString

                frmInvoiceEdit.Visible = True
                frmInvoiceEdit.BringToFront()


            Else
                MsgBox("No Invoice Edited", MsgBoxStyle.Information)
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub UpdateInvoiceAuditDatabase()
        Try


            Dim daInsInvinAudit As New SqlDataAdapter("Insert Into Invoice SELECT ID, Shipper, InvoiceNo, InvoiceDate, Buyer, Account, BuyerDepartment, Origin, LCNo, InvoiceDescription, ShippingBillNo, ShippingBillDate, Marks1, Marks2, Marks3, Marks4, Marks5, Marks6, Marks7, Marks8, ModeOfShipment, AwbBillNo, AwbBillDate, HAwbBillNo, HAwbBillDate, Destination, BanckAccount, Vessel, Bank, Currency, CurrencyConversion, Nature, Quantity, TotalValue, Freight, Insurance, AgentPercentage, Commission, DbkPercentage, DrawBack, BankCertificate, BankAmount, BankRate, CertificateDate, TotalPackNo, harmnsdco, NetWeight, GrossWeight, dbkrecd, aepccer_no, cust_clear, rbicode, cot_rayon, shpblfob, netfobamt, remark1, status, shipdate, aepcdt, drwrecddt, negdate, markrate, cntrycode, realstdt, usha, ugts, printdt, [percent], plusamt, cntryname, days, due_date, accounted, accountamt, mode, ShipRate, courier, docawb, docsentdt, advdocdt, inspdate, advdocdays, tokenno, tokendt, qlfbonus, midamount, depb, depbamt, depbrcvd, depbrcvdon, depbper, licencenr, licenceamt, licsoldon, licsoldfor, depbappldt, port, epcopydt, forwarder, forbillno, forbillamt, forrcvdt, sentforver, verifieddt, suppnr, suppbbnnr, warehouse, swiftcode, CorrespondingBank, CorrespondingBankAcount, CorrespondingBankSwiftCode, Msrks9, Marks10, STRPercentage, STRAmount, PAN_Number, VAT_TIN, CST_TIN, ExciseDuty, VATAmount, CessAmount, EduCessAmount, BuyerGroup, MinusAmount, FinancialYear, InvoiceType, Shipped, IsShipped, ShippedDate, ShippedBy, MarkToShipDoneDate, PaymentReceiveFromBuyer, PaymentReceiveDate, BankRefNo, BankRefDate, ContractNo, ContractDate, ExciseInvoiceNo, IsAdvanceReceived, AdvanceAmount, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, EnteredOnMachineID, HandoverDate, HangerPack, Covering, Declaration, PostDescription, LCDescription, Mark11, Mark12, DiscountUpCharge, Percentage, Amount, ShipperLC, BuyerLC, FOBValueInCurrency, TaxType, IsRecalRequired, IsApproved, ApprovedBy, ApprovedOn, ModuleName, 'Added', ConsigneeCode, Notify1, Notify2, Notify3, AuthSignEmpCode, AuthSignEmpName, AuthSignDesi, CSTorVAT, EduCess, CESS, Excise, CT3No, CT3Date, ARENo, AREDate, ConveredBy, Declaration1, ContainerSize, ContainerName, ContainerSealNo, GoodsDescription, MarksAndNos, NoAndKindOfPackages, PreCarriageBy, PreCarrierRecvPlace, PortDischarge, DestinationCountry, AssortmentYear, PaymentTerms, DeliveryTerms, AREDuty, AreCess, AREHCess, ContainerNo, FromPackNo, ToPackNo, EmailToCustDate, FreightFwdrPlotLetterDate, ContainerApplnDate, GSPSlNo, OriginCriterion, StuffingDate, GateOpeningDate, CutOffDate, ClosingDate, SailingVesselDetails, ShipmentType, RevShipmentType, HaltingCharges, Demurrage, VoyageNo, ETDFeederVessel, ETAFeederVessel, FeederVessel, FeederVoyageNo, ETAMotherVessel, ETDMotherVessel, InternalSealNo, CentralExciseSealNo, TypesOfBL, CustomClearence, TransportCharges, CHACharges, ClearingCharges, CFSCharges, ForwardingCharges, GSpCharges, CourierCharges, InsuranceCharges, MiscCharges, ContainerArrivalDate, Memo, DestinationArrivalDate, FreightFwdrPlotLetterExpDate, ForwarderCode, CHACode, RevVessel, RevVoyageNo, RevETDMotherVessel, RevETAMotherVessel, RevFeederVessel, RevFeederVoyageNo, RevETDFeederVessel, RevETAFeederVessel, Commodity, PremiumRate, PremiumAmount, InvoiceNoAutoGen, CourierPayment, CourierNo, CourierDate, CartonDia, OneCarton, TotalOne, FinalDestination, MatType, AmtOfDutyPayable, SubTotal, InvYear, InvCode, DispatchFrom, InoviceStatus, ExpDocDate, GoodsDescription2, RBBankName, RBAccountNo, RBSwiftCode, CurValue, AnnexureAPortOfLoading, LCID, LCValue, ShippedLCValue, CGSTPercentage, CGSTValue, SGSTPercentage, SGSTVlaue, IGSTPercentage, IGSTValue, FreightCharges, LoadingCharges, InsuranceChager, OtherCharges, Discount, GSTTotalValue, GSTInvNo, InvNo2, InvNo3, DUMMYINVDATE, GSTValue, SerialNoPrefix, InternalOrder, SupplyType, AckNo, AckDate, QRCode, IRNNo, IsService, TableName, FreightCGSTVal, FreightCGSTPer, TCSValue, TCSPercentage, InvoiceValue, GSTNo, FreightTotalVal, FreightSGSTVal, FreightSGSTPer, FreightIGSTVal, FreightIGSTPer FROM AHGroup_SSPL.dbo.INVOICE WHERE (InvoiceNo = '" & mdlSGM.sInvoiceNo & "')", sConstrAudit)
            Dim dsInsInvinAudit As New DataSet
            daInsInvinAudit.Fill(dsInsInvinAudit)
            dsInsInvinAudit.AcceptChanges()


        Catch exp As Exception
            HandleException(Me.Name, exp)
        End Try

    End Sub

    Private Sub UpdateInvoiceDetailAuditDatabase()
        Try
            Dim daSelInvoice As New SqlDataAdapter("Select * from InvoiceDetail Where InvoiceNo = '" & sInvoiceNo & _
                                                   "' Order by InvoiceSerialNo", sConstr)
            Dim dsSelInvoice As New DataSet
            daSelInvoice.Fill(dsSelInvoice)

            Dim i As Integer = 0
            If dsSelInvoice.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsSelInvoice.Tables(0).Rows.Count - 1
                    mystrInvoiceDetails.InvoiceSerialNo = dsSelInvoice.Tables(0).Rows(i).Item("InvoiceSerialNo").ToString
                    mystrInvoiceDetails.ID = dsSelInvoice.Tables(0).Rows(i).Item("ID").ToString
                    mystrInvoiceDetails.invoiceno = dsSelInvoice.Tables(0).Rows(i).Item("invoiceno").ToString
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("InvoiceDate")) <> True Then : mystrInvoiceDetails.InvoiceDate = dsSelInvoice.Tables(0).Rows(i).Item("InvoiceDate").ToString : End If
                    mystrInvoiceDetails.DCNo = dsSelInvoice.Tables(0).Rows(i).Item("DCNo").ToString
                    mystrInvoiceDetails.buyer = dsSelInvoice.Tables(0).Rows(i).Item("buyer").ToString
                    mystrInvoiceDetails.shipper = dsSelInvoice.Tables(0).Rows(i).Item("shipper").ToString
                    mystrInvoiceDetails.SalesOrderNo = dsSelInvoice.Tables(0).Rows(i).Item("SalesOrderNo").ToString
                    mystrInvoiceDetails.type = dsSelInvoice.Tables(0).Rows(i).Item("type").ToString
                    mystrInvoiceDetails.subordno = dsSelInvoice.Tables(0).Rows(i).Item("subordno").ToString
                    mystrInvoiceDetails.ArticleNo = dsSelInvoice.Tables(0).Rows(i).Item("ArticleNo").ToString
                    mystrInvoiceDetails.smpstyl = dsSelInvoice.Tables(0).Rows(i).Item("smpstyl").ToString
                    mystrInvoiceDetails.Color = dsSelInvoice.Tables(0).Rows(i).Item("Color").ToString
                    mystrInvoiceDetails.rate = Val(dsSelInvoice.Tables(0).Rows(i).Item("rate").ToString)
                    mystrInvoiceDetails.quantity = Val(dsSelInvoice.Tables(0).Rows(i).Item("quantity").ToString)
                    mystrInvoiceDetails.shortshp = Val(dsSelInvoice.Tables(0).Rows(i).Item("shortshp").ToString)
                    mystrInvoiceDetails.rt = dsSelInvoice.Tables(0).Rows(i).Item("rt").ToString
                    mystrInvoiceDetails.ratioqty = Val(dsSelInvoice.Tables(0).Rows(i).Item("ratioqty").ToString)
                    mystrInvoiceDetails.currency = dsSelInvoice.Tables(0).Rows(i).Item("currency").ToString
                    mystrInvoiceDetails.CurrencyConversionRate = Val(dsSelInvoice.Tables(0).Rows(i).Item("CurrencyConversionRate").ToString)
                    mystrInvoiceDetails.category = dsSelInvoice.Tables(0).Rows(i).Item("category").ToString
                    mystrInvoiceDetails.buyrdept = dsSelInvoice.Tables(0).Rows(i).Item("buyrdept").ToString
                    mystrInvoiceDetails.bankrate = Val(dsSelInvoice.Tables(0).Rows(i).Item("bankrate").ToString)
                    mystrInvoiceDetails.value = Val(dsSelInvoice.Tables(0).Rows(i).Item("value").ToString)
                    mystrInvoiceDetails.size1 = dsSelInvoice.Tables(0).Rows(i).Item("size1").ToString
                    mystrInvoiceDetails.qty1 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty1").ToString)
                    mystrInvoiceDetails.size2 = dsSelInvoice.Tables(0).Rows(i).Item("size2").ToString
                    mystrInvoiceDetails.qty2 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty2").ToString)
                    mystrInvoiceDetails.size3 = dsSelInvoice.Tables(0).Rows(i).Item("size3").ToString
                    mystrInvoiceDetails.qty3 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty3").ToString)
                    mystrInvoiceDetails.size4 = dsSelInvoice.Tables(0).Rows(i).Item("size4").ToString
                    mystrInvoiceDetails.qty4 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty4").ToString)
                    mystrInvoiceDetails.size5 = dsSelInvoice.Tables(0).Rows(i).Item("size5").ToString
                    mystrInvoiceDetails.qty5 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty5").ToString)
                    mystrInvoiceDetails.size6 = dsSelInvoice.Tables(0).Rows(i).Item("size6").ToString
                    mystrInvoiceDetails.qty6 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty6").ToString)
                    mystrInvoiceDetails.size7 = dsSelInvoice.Tables(0).Rows(i).Item("size7").ToString
                    mystrInvoiceDetails.qty7 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty7").ToString)
                    mystrInvoiceDetails.size8 = dsSelInvoice.Tables(0).Rows(i).Item("size8").ToString
                    mystrInvoiceDetails.qty8 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty8").ToString)
                    mystrInvoiceDetails.size9 = dsSelInvoice.Tables(0).Rows(i).Item("size9").ToString
                    mystrInvoiceDetails.qty9 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty9").ToString)
                    mystrInvoiceDetails.size10 = dsSelInvoice.Tables(0).Rows(i).Item("size10").ToString
                    mystrInvoiceDetails.qty10 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty10").ToString)
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("inspdate")) <> True Then : mystrInvoiceDetails.inspdate = dsSelInvoice.Tables(0).Rows(i).Item("inspdate").ToString : End If
                    mystrInvoiceDetails.cert_no = dsSelInvoice.Tables(0).Rows(i).Item("cert_no").ToString
                    mystrInvoiceDetails.period = dsSelInvoice.Tables(0).Rows(i).Item("period").ToString
                    mystrInvoiceDetails.cert_type = dsSelInvoice.Tables(0).Rows(i).Item("cert_type").ToString
                    mystrInvoiceDetails.cartalloc = dsSelInvoice.Tables(0).Rows(i).Item("cartalloc").ToString
                    mystrInvoiceDetails.annexno = dsSelInvoice.Tables(0).Rows(i).Item("annexno").ToString
                    mystrInvoiceDetails.anserial = dsSelInvoice.Tables(0).Rows(i).Item("anserial").ToString
                    mystrInvoiceDetails.awb_bl_no = dsSelInvoice.Tables(0).Rows(i).Item("awb_bl_no").ToString
                    mystrInvoiceDetails.lcno = dsSelInvoice.Tables(0).Rows(i).Item("lcno").ToString
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("shbilldt")) <> True Then : mystrInvoiceDetails.shbilldt = dsSelInvoice.Tables(0).Rows(i).Item("shbilldt").ToString : End If
                    mystrInvoiceDetails.catii = dsSelInvoice.Tables(0).Rows(i).Item("catii").ToString
                    mystrInvoiceDetails.catiiconv = Val(dsSelInvoice.Tables(0).Rows(i).Item("catiiconv").ToString)
                    mystrInvoiceDetails.BuyerOrderNo = dsSelInvoice.Tables(0).Rows(i).Item("BuyerOrderNo").ToString
                    mystrInvoiceDetails.provordnr = dsSelInvoice.Tables(0).Rows(i).Item("provordnr").ToString
                    mystrInvoiceDetails.CountryCode = dsSelInvoice.Tables(0).Rows(i).Item("CountryCode").ToString
                    mystrInvoiceDetails.smpdesc = dsSelInvoice.Tables(0).Rows(i).Item("smpdesc").ToString
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("ndate")) <> True Then : mystrInvoiceDetails.ndate = dsSelInvoice.Tables(0).Rows(i).Item("ndate").ToString : End If
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("rvdate")) <> True Then : mystrInvoiceDetails.rvdate = dsSelInvoice.Tables(0).Rows(i).Item("rvdate").ToString : End If
                    mystrInvoiceDetails.mrate = Val(dsSelInvoice.Tables(0).Rows(i).Item("mrate").ToString)
                    mystrInvoiceDetails.usha = dsSelInvoice.Tables(0).Rows(i).Item("usha").ToString
                    mystrInvoiceDetails.ugts = dsSelInvoice.Tables(0).Rows(i).Item("ugts").ToString
                    mystrInvoiceDetails.rsno = dsSelInvoice.Tables(0).Rows(i).Item("rsno").ToString
                    mystrInvoiceDetails.MaterialCode = dsSelInvoice.Tables(0).Rows(i).Item("MaterialCode").ToString
                    mystrInvoiceDetails.mode = dsSelInvoice.Tables(0).Rows(i).Item("mode").ToString
                    mystrInvoiceDetails.Group = dsSelInvoice.Tables(0).Rows(i).Item("Group").ToString
                    mystrInvoiceDetails.bank = dsSelInvoice.Tables(0).Rows(i).Item("bank").ToString
                    mystrInvoiceDetails.commpercnt = Val(dsSelInvoice.Tables(0).Rows(i).Item("commpercnt").ToString)
                    mystrInvoiceDetails.mainfab = dsSelInvoice.Tables(0).Rows(i).Item("mainfab").ToString
                    mystrInvoiceDetails.country = dsSelInvoice.Tables(0).Rows(i).Item("country").ToString
                    mystrInvoiceDetails.basestyl = dsSelInvoice.Tables(0).Rows(i).Item("basestyl").ToString
                    mystrInvoiceDetails.curvalue = Val(dsSelInvoice.Tables(0).Rows(i).Item("curvalue").ToString)
                    mystrInvoiceDetails.size11 = dsSelInvoice.Tables(0).Rows(i).Item("size11").ToString
                    mystrInvoiceDetails.size12 = dsSelInvoice.Tables(0).Rows(i).Item("size12").ToString
                    mystrInvoiceDetails.size13 = dsSelInvoice.Tables(0).Rows(i).Item("size13").ToString
                    mystrInvoiceDetails.size14 = dsSelInvoice.Tables(0).Rows(i).Item("size14").ToString
                    mystrInvoiceDetails.size15 = dsSelInvoice.Tables(0).Rows(i).Item("size15").ToString
                    mystrInvoiceDetails.size16 = dsSelInvoice.Tables(0).Rows(i).Item("size16").ToString
                    mystrInvoiceDetails.size17 = dsSelInvoice.Tables(0).Rows(i).Item("size17").ToString
                    mystrInvoiceDetails.size18 = dsSelInvoice.Tables(0).Rows(i).Item("size18").ToString
                    mystrInvoiceDetails.qty11 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty11").ToString)
                    mystrInvoiceDetails.qty12 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty12").ToString)
                    mystrInvoiceDetails.qty13 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty13").ToString)
                    mystrInvoiceDetails.qty14 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty14").ToString)
                    mystrInvoiceDetails.qty15 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty15").ToString)
                    mystrInvoiceDetails.qty16 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty16").ToString)
                    mystrInvoiceDetails.qty17 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty17").ToString)
                    mystrInvoiceDetails.qty18 = Val(dsSelInvoice.Tables(0).Rows(i).Item("qty18").ToString)
                    mystrInvoiceDetails.OrderSerialNo = dsSelInvoice.Tables(0).Rows(i).Item("OrderSerialNo").ToString
                    mystrInvoiceDetails.BuyerGroup = dsSelInvoice.Tables(0).Rows(i).Item("BuyerGroup").ToString
                    mystrInvoiceDetails.LCDiscount = Val(dsSelInvoice.Tables(0).Rows(i).Item("LCDiscount").ToString)
                    mystrInvoiceDetails.Shipped = Val(dsSelInvoice.Tables(0).Rows(i).Item("Shipped").ToString)
                    mystrInvoiceDetails.CustomerStyleName = dsSelInvoice.Tables(0).Rows(i).Item("CustomerStyleName").ToString
                    mystrInvoiceDetails.IsShipped = Val(dsSelInvoice.Tables(0).Rows(i).Item("IsShipped").ToString)
                    mystrInvoiceDetails.Status = dsSelInvoice.Tables(0).Rows(i).Item("Status").ToString
                    mystrInvoiceDetails.PaymentREceiveFromBuyer = Val(dsSelInvoice.Tables(0).Rows(i).Item("PaymentREceiveFromBuyer").ToString)
                    mystrInvoiceDetails.Store = dsSelInvoice.Tables(0).Rows(i).Item("Store").ToString
                    mystrInvoiceDetails.Season = dsSelInvoice.Tables(0).Rows(i).Item("Season").ToString
                    mystrInvoiceDetails.burdept = dsSelInvoice.Tables(0).Rows(i).Item("burdept").ToString
                    mystrInvoiceDetails.CreatedBy = dsSelInvoice.Tables(0).Rows(i).Item("CreatedBy").ToString
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("CreatedDate")) <> True Then : mystrInvoiceDetails.CreatedDate = dsSelInvoice.Tables(0).Rows(i).Item("CreatedDate").ToString : End If
                    mystrInvoiceDetails.ModifiedBy = dsSelInvoice.Tables(0).Rows(i).Item("ModifiedBy").ToString
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("ModifiedDate")) <> True Then : mystrInvoiceDetails.ModifiedDate = dsSelInvoice.Tables(0).Rows(i).Item("ModifiedDate").ToString : End If
                    mystrInvoiceDetails.EnteredOnMachineID = dsSelInvoice.Tables(0).Rows(i).Item("EnteredOnMachineID").ToString
                    mystrInvoiceDetails.sVariant = dsSelInvoice.Tables(0).Rows(i).Item("Variant").ToString
                    mystrInvoiceDetails.CustomerArticleNo = dsSelInvoice.Tables(0).Rows(i).Item("CustomerArticleNo").ToString
                    mystrInvoiceDetails.InvoiceDescription = dsSelInvoice.Tables(0).Rows(i).Item("InvoiceDescription").ToString
                    mystrInvoiceDetails.Merchandiser = dsSelInvoice.Tables(0).Rows(i).Item("Merchandiser").ToString
                    mystrInvoiceDetails.PackNo = dsSelInvoice.Tables(0).Rows(i).Item("PackNo").ToString
                    mystrInvoiceDetails.JobCardNo = dsSelInvoice.Tables(0).Rows(i).Item("JobCardNo").ToString
                    mystrInvoiceDetails.Count = dsSelInvoice.Tables(0).Rows(i).Item("Count").ToString
                    mystrInvoiceDetails.construction = dsSelInvoice.Tables(0).Rows(i).Item("construction").ToString
                    mystrInvoiceDetails.CartonNo = dsSelInvoice.Tables(0).Rows(i).Item("CartonNo").ToString
                    mystrInvoiceDetails.MRPRate = Val(dsSelInvoice.Tables(0).Rows(i).Item("MRPRate").ToString)
                    mystrInvoiceDetails.IsRecalRequired = Val(dsSelInvoice.Tables(0).Rows(i).Item("IsRecalRequired").ToString)
                    mystrInvoiceDetails.OrderPrice = Val(dsSelInvoice.Tables(0).Rows(i).Item("OrderPrice").ToString)
                    mystrInvoiceDetails.IsApproved = Val(dsSelInvoice.Tables(0).Rows(i).Item("IsApproved").ToString)
                    mystrInvoiceDetails.ApprovedBy = dsSelInvoice.Tables(0).Rows(i).Item("ApprovedBy").ToString
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("ApprovedOn")) <> True Then : mystrInvoiceDetails.ApprovedOn = dsSelInvoice.Tables(0).Rows(i).Item("ApprovedOn").ToString : End If
                    mystrInvoiceDetails.ModuleName = dsSelInvoice.Tables(0).Rows(i).Item("ModuleName").ToString
                    mystrInvoiceDetails.JobCardDetailID = dsSelInvoice.Tables(0).Rows(i).Item("JobCardDetailID").ToString
                    mystrInvoiceDetails.InvoiceID = dsSelInvoice.Tables(0).Rows(i).Item("InvoiceID").ToString
                    mystrInvoiceDetails.UpdateMode = dsSelInvoice.Tables(0).Rows(i).Item("UpdateMode").ToString
                    mystrInvoiceDetails.pcs = Val(dsSelInvoice.Tables(0).Rows(i).Item("pcs").ToString)
                    mystrInvoiceDetails.SalesOrderDetailID = dsSelInvoice.Tables(0).Rows(i).Item("SalesOrderDetailID").ToString
                    mystrInvoiceDetails.CustWorkOrderNO = dsSelInvoice.Tables(0).Rows(i).Item("CustWorkOrderNO").ToString
                    mystrInvoiceDetails.InvoiceNoAutoGen = dsSelInvoice.Tables(0).Rows(i).Item("InvoiceNoAutoGen").ToString
                    mystrInvoiceDetails.ArticleCodification = dsSelInvoice.Tables(0).Rows(i).Item("ArticleCodification").ToString
                    mystrInvoiceDetails.InvoiceQuantity = Val(dsSelInvoice.Tables(0).Rows(i).Item("InvoiceQuantity").ToString)
                    mystrInvoiceDetails.CBMSpace = Val(dsSelInvoice.Tables(0).Rows(i).Item("CBMSpace").ToString)
                    mystrInvoiceDetails.OrderQty = Val(dsSelInvoice.Tables(0).Rows(i).Item("OrderQty").ToString)
                    mystrInvoiceDetails.CurrencyConversionRate4Tally = Val(dsSelInvoice.Tables(0).Rows(i).Item("CurrencyConversionRate4Tally").ToString)
                    mystrInvoiceDetails.curvalue4Tally = Val(dsSelInvoice.Tables(0).Rows(i).Item("curvalue4Tally").ToString)
                    mystrInvoiceDetails.CGSTPercentage = Val(dsSelInvoice.Tables(0).Rows(i).Item("CGSTPercentage").ToString)
                    mystrInvoiceDetails.CGSTValue = Val(dsSelInvoice.Tables(0).Rows(i).Item("CGSTValue").ToString)
                    mystrInvoiceDetails.SGSTPercentage = Val(dsSelInvoice.Tables(0).Rows(i).Item("SGSTPercentage").ToString)
                    mystrInvoiceDetails.SGSTValue = Val(dsSelInvoice.Tables(0).Rows(i).Item("SGSTValue").ToString)
                    mystrInvoiceDetails.IGSTPercentage = Val(dsSelInvoice.Tables(0).Rows(i).Item("IGSTPercentage").ToString)
                    mystrInvoiceDetails.IGSTValue = Val(dsSelInvoice.Tables(0).Rows(i).Item("IGSTValue").ToString)
                    mystrInvoiceDetails.Discount = Val(dsSelInvoice.Tables(0).Rows(i).Item("Discount").ToString)
                    mystrInvoiceDetails.HSNCode = dsSelInvoice.Tables(0).Rows(i).Item("HSNCode").ToString
                    mystrInvoiceDetails.WareHouse = dsSelInvoice.Tables(0).Rows(i).Item("WareHouse").ToString
                    mystrInvoiceDetails.FValue = Val(dsSelInvoice.Tables(0).Rows(i).Item("FValue").ToString)
                    If IsDBNull(dsSelInvoice.Tables(0).Rows(0).Item("LastInvDate")) <> True Then : mystrInvoiceDetails.LastInvDate = dsSelInvoice.Tables(0).Rows(i).Item("LastInvDate").ToString : End If
                    mystrInvoiceDetails.Ready2DispatchID = dsSelInvoice.Tables(0).Rows(i).Item("Ready2DispatchID").ToString
                    mystrInvoiceDetails.InternalOrder = dsSelInvoice.Tables(0).Rows(i).Item("InternalOrder").ToString
                    mystrInvoiceDetails.IsSampleOrder = Val(dsSelInvoice.Tables(0).Rows(i).Item("IsSampleOrder").ToString)
                    mystrInvoiceDetails.SampleOrderType = dsSelInvoice.Tables(0).Rows(i).Item("SampleOrderType").ToString
                    mystrInvoiceDetails.ArticleandColor = dsSelInvoice.Tables(0).Rows(i).Item("ArticleandColor").ToString
                    mystrInvoiceDetails.NettValue = Val(dsSelInvoice.Tables(0).Rows(i).Item("NetValue").ToString)
                    myccInvoice.InsertInvoiceMaininAudit(mystrInvoice)
                Next
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub



End Class

