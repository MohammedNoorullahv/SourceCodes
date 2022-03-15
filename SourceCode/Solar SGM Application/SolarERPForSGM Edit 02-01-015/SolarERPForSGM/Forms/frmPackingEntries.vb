Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO

Public Class frmPackingEntries

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)
    Dim keyascii As Integer

    Dim myccPackingEntries As New ccPackingEntries
    Dim mystrReadytoDispatch As New strReadytoDispatch
    Dim mystrMDData As New strMDData
    Dim mystrMDDataDtls As New strMDDataDtls

    Dim sSalesOrderNo, sArticle, sBuyerCode, sBuyer, sJobcardDetailID As String
    Dim sSize01, sSize02, sSize03, sSize04, sSize05, sSize06, sSize07, sSize08, sSize09, sSize10 As String
    Dim sSize11, sSize12, sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize, sPartQtySize As String
    Dim sShipper, sBuyerGroup, sPackNo, sSalesOrderDetailId As String

    Dim nQuantity01, nQuantity02, nQuantity03, nQuantity04, nQuantity05, nQuantity06, nQuantity07, nQuantity08, nQuantity09, nQuantity10 As Decimal
    Dim nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18 As Decimal
    Dim nQuantity As Decimal

    Dim ngrdRowCount, nStringLength, nBoxNo, nBoxnoLength, ngrdRowNo, nYesNo As Integer
    Dim sJobcardNo, sProcess, sBeginsWith, sDescription, sIsLoaded, sCustomercode, sID As String

#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        'End
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        MsgBox("Export Completed")
    End Sub

    Private Sub LoadBuyers()
        sIsLoaded = "N"
        Dim i As Integer = 0

        grdBuyers.BringToFront()

Ab:
        ngrdRowCount = grdBuyersV1.RowCount
        For i = 0 To ngrdRowCount
            grdBuyersV1.DeleteRow(i)
        Next
        ngrdRowCount = grdBuyersV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        grdBuyers.DataSource = myccPackingEntries.LoadCustomers

        With grdBuyersV1

            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1

            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        End With
        sIsLoaded = "Y"
    End Sub

    Private Sub LoadReadytoDispatchSummary()
        Dim i As Integer = 0

        grdRTDSummary.BringToFront()

Ab:
        ngrdRowCount = grdRTDSummaryV1.RowCount
        For i = 0 To ngrdRowCount
            grdRTDSummaryV1.DeleteRow(i)
        Next
        ngrdRowCount = grdRTDSummaryV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        grdRTDSummary.DataSource = myccPackingEntries.LoadReadytoDispatchSummary(sBuyerCode)

        With grdRTDSummaryV1

            .Columns(0).VisibleIndex = -1


            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        End With
    End Sub

    Private Sub LoadReadytoDispatchDetails()
        Dim i As Integer = 0

        grdRTDDetails.BringToFront()

Ab:
        ngrdRowCount = grdRTDDetailsV1.RowCount
        For i = 0 To ngrdRowCount
            grdRTDDetailsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdRTDDetailsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        grdRTDDetails.DataSource = myccPackingEntries.LoadReadytoDispatchDetails(sJobcardNo)

        With grdRTDDetailsV1

            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1

            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            i = 3
            For i = 3 To 39
                .Columns(i).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                i = i + 1
            Next

            'i = 4
            'For i = 4 To 38
            '    .Columns(i).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max
            '    i = i + 1
            'Next

        End With
    End Sub

    Private Sub frmPackingEntries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadBuyers()
    End Sub

    Dim sStatus As String
    Private Sub tbBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbBarcode.KeyPress

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then

            If Trim(tbBarcode.Text) = "EXIT" Then
                End
            End If
            tbLastScannedBarcode.Clear()

            ngrdRowNo = grdBuyersV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sCustomercode = grdBuyersV1.GetDataRow(ngrdRowNo).Item("BuyerCode").ToString
                sBuyerCode = grdBuyersV1.GetDataRow(ngrdRowNo).Item("BuyerCode").ToString
            Else
                MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                sStatus = "Customer Not Selected Properly"
                UpdateNotePad() : tbBarcode.Clear()
                Exit Sub
            End If

            nStringLength = Microsoft.VisualBasic.Len(tbBarcode.Text)

            If nStringLength < 15 Or nStringLength > 17 Then
                MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                sStatus = "Invalid Barcode"
                UpdateNotePad() : tbBarcode.Clear()
                Exit Sub
            End If

            sJobcardNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 13)
            sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 9)

            nBoxnoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
            nBoxNo = Microsoft.VisualBasic.Right(tbBarcode.Text, nBoxnoLength - 1)
Aa:
            Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where Buyercode = '" & sCustomercode & _
                                              "' And JobcardNo = '" & sJobcardNo & "' And CartonNo = '" & nBoxNo & "'", sConstr)
            Dim dsSelPkgDtl As New DataSet
            daSelPkgDtl.Fill(dsSelPkgDtl)

            If dsSelPkgDtl.Tables(0).Rows.Count = 0 Then
                MsgBox("This Carton Does not belong to this customer", MsgBoxStyle.Critical)
                sStatus = "This Carton Does not belong to this customer"
                UpdateNotePad() : tbBarcode.Clear()
                Exit Sub
            End If

            If Microsoft.VisualBasic.Left(dsSelPkgDtl.Tables(0).Rows(0).Item("Article").ToString, 7) = "SOL-LEA" Then
                Dim daSelWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsSelWIP As New DataSet
                daSelWIP.Fill(dsSelWIP)

                If dsSelWIP.Tables(0).Rows.Count = 0 Then
                    MsgBox("Jobcard WIP Not Created!", MsgBoxStyle.Critical)
                    sStatus = "Jobcard WIP Not Created!"
                    UpdateNotePad() : tbBarcode.Clear()
                    Exit Sub
                End If

                mdlSGM.sSelectedArticle = Microsoft.VisualBasic.Left(dsSelPkgDtl.Tables(0).Rows(0).Item("Article").ToString, 7)
                If dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch").ToString = "" Then
                    Dim daUpdPkgDtl As New SqlDataAdapter("Update PackingDetail Set ReadytoDispatch = '0' Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdPkgDtl As New DataSet
                    daUpdPkgDtl.Fill(dsUpdPkgDtl)
                    dsUpdPkgDtl.AcceptChanges()
                    GoTo Aa
                End If
            Else
                If dsSelPkgDtl.Tables(0).Rows(0).Item("WIPLocation").ToString <> "PACKING" Then
                    MsgBox("This Carton is Not Received at Packing Section", MsgBoxStyle.Critical)
                    sStatus = "This Carton is Not Received at Packing Section"
                    UpdateNotePad() : tbBarcode.Clear()
                    Exit Sub
                End If
            End If

            'If IsDBNull(dsSelPkgDtl.Tables(0).Rows(0).Item("FinishScanDate").ToString) = True Then
            '    MsgBox("This Carton is Not Received at Packing Section", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If

            If Val(dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch")) * -1 = 1 Then
                MsgBox("This Carton Already Included for Invoice Generation", MsgBoxStyle.Critical)
                sStatus = "This Carton Already Included for Invoice Generation"
                UpdateNotePad() : tbBarcode.Clear()
                Exit Sub
            End If

            UpdatePacking()
            LoadReadytoDispatchSummary()

            tbLastScannedBarcode.Text = tbBarcode.Text
            UpdateNotePad() : tbBarcode.Clear()

        End If
    End Sub

    Dim sFrom As String
    Private Sub UpdatePacking()

        Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where Buyercode = '" & sCustomercode & _
                                              "' And JobcardNo = '" & sJobcardNo & "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelPkgDtl As New DataSet
        daSelPkgDtl.Fill(dsSelPkgDtl)

        If dsSelPkgDtl.Tables(0).Rows.Count = 0 Then
            MsgBox("This Carton Does not belong to this customer", MsgBoxStyle.Critical)
            sStatus = "This Carton Does not belong to this customer"
            UpdateNotePad() : tbBarcode.Clear()
            Exit Sub
        End If

        ' If IsDBNull(dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch").ToString) = True Then
        'If Val(dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch").ToString) = 1 Then
        '    MsgBox("This Carton Already Included for Invoice Generation", MsgBoxStyle.Critical)
        '    'tbStatus.Text = "Mould 2 Finish Not Done"
        '    'tbStatus.ForeColor = Color.Red
        '    'UpdateNotePad() : tbBarcode.Clear()
        '    Exit Sub
        'End If

        'MsgBox((dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch").ToString))
        nQuantity = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity").ToString)
        nQuantity01 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity01").ToString)
        nQuantity02 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity02").ToString)
        nQuantity03 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity03").ToString)
        nQuantity04 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity04").ToString)
        nQuantity05 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity05").ToString)
        nQuantity06 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity06").ToString)
        nQuantity07 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity07").ToString)
        nQuantity08 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity08").ToString)
        nQuantity09 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity09").ToString)
        nQuantity10 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity10").ToString)
        nQuantity11 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity11").ToString)
        nQuantity12 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity12").ToString)
        nQuantity13 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity13").ToString)
        nQuantity14 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity14").ToString)
        nQuantity15 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity15").ToString)
        nQuantity16 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity16").ToString)
        nQuantity17 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity17").ToString)
        nQuantity18 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity18").ToString)

        sFrom = "Barcode Device"
        JobcardVerification()

        mystrReadytoDispatch.ID = System.Guid.NewGuid.ToString() 'AsString
        mystrReadytoDispatch.invoiceno = "" 'AsString
        mystrReadytoDispatch.InvoiceDate = Date.Now 'AsDate
        mystrReadytoDispatch.InvoiceSerialNo = "" 'AsString
        mystrReadytoDispatch.buyer = sBuyer 'AsString
        mystrReadytoDispatch.shipper = sShipper 'AsString
        mystrReadytoDispatch.SalesOrderNo = sSalesOrderNo 'AsString
        mystrReadytoDispatch.ArticleNo = sArticle 'AsString
        mystrReadytoDispatch.rate = 0 'AsDecimal
        mystrReadytoDispatch.quantity = nQuantity 'AsDecimal
        mystrReadytoDispatch.BuyerGroup = sBuyerCode 'AsString
        mystrReadytoDispatch.CreatedBy = "" 'AsString
        mystrReadytoDispatch.CreatedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrReadytoDispatch.ModifiedBy = ""
        mystrReadytoDispatch.ModifiedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrReadytoDispatch.EnteredOnMachineID = mdlSGM.strSystemName
        mystrReadytoDispatch.PackNo = sPackNo 'AsString
        mystrReadytoDispatch.JobCardNo = sJobcardNo 'AsString
        mystrReadytoDispatch.IsApproved = 0 'AsInteger
        mystrReadytoDispatch.ApprovedBy = "" 'AsString
        mystrReadytoDispatch.ApprovedOn = Date.Now 'AsDate
        mystrReadytoDispatch.ModuleName = "Barcode Scanning" 'AsString
        mystrReadytoDispatch.JobCardDetailID = sJobcardDetailID 'AsString
        mystrReadytoDispatch.InvoiceID = "" 'AsString
        mystrReadytoDispatch.SalesOrderDetailID = sSalesOrderDetailId 'AsString
        mystrReadytoDispatch.Size01 = sSize01 'AsString
        mystrReadytoDispatch.Quantity01 = nQuantity01 'AsDecimal
        mystrReadytoDispatch.Size02 = sSize02 'AsString
        mystrReadytoDispatch.Quantity02 = nQuantity02 'AsDecimal
        mystrReadytoDispatch.Size03 = sSize03 'AsString
        mystrReadytoDispatch.Quantity03 = nQuantity03 'AsDecimal
        mystrReadytoDispatch.Size04 = sSize04 'AsString
        mystrReadytoDispatch.Quantity04 = nQuantity04 'AsDecimal
        mystrReadytoDispatch.Size05 = sSize05 'AsString
        mystrReadytoDispatch.Quantity05 = nQuantity05 'AsDecimal
        mystrReadytoDispatch.Size06 = sSize06 'AsString
        mystrReadytoDispatch.Quantity06 = nQuantity06 'AsDecimal
        mystrReadytoDispatch.Size07 = sSize07 'AsString
        mystrReadytoDispatch.Quantity07 = nQuantity07 'AsDecimal
        mystrReadytoDispatch.Size08 = sSize08 'AsString
        mystrReadytoDispatch.Quantity08 = nQuantity08 'AsDecimal
        mystrReadytoDispatch.Size09 = sSize09 'AsString
        mystrReadytoDispatch.Quantity09 = nQuantity09 'AsDecimal
        mystrReadytoDispatch.Size10 = sSize10 'AsString
        mystrReadytoDispatch.Quantity10 = nQuantity10 'AsDecimal
        mystrReadytoDispatch.Size11 = sSize11 'AsString
        mystrReadytoDispatch.Quantity11 = nQuantity11 'AsDecimal
        mystrReadytoDispatch.Size12 = sSize12 'AsString
        mystrReadytoDispatch.Quantity12 = nQuantity12 'AsDecimal
        mystrReadytoDispatch.Size13 = sSize13 'AsString
        mystrReadytoDispatch.Quantity13 = nQuantity13 'AsDecimal
        mystrReadytoDispatch.Size14 = sSize14 'AsString
        mystrReadytoDispatch.Quantity14 = nQuantity14 'AsDecimal
        mystrReadytoDispatch.Size15 = sSize15 'AsString
        mystrReadytoDispatch.Quantity15 = nQuantity15 'AsDecimal
        mystrReadytoDispatch.Size16 = sSize16 'AsString
        mystrReadytoDispatch.Quantity16 = nQuantity16 'AsDecimal
        mystrReadytoDispatch.Size17 = sSize17 'AsString
        mystrReadytoDispatch.Quantity17 = nQuantity17 'AsDecimal
        mystrReadytoDispatch.Size18 = sSize18 'AsString
        mystrReadytoDispatch.Quantity18 = nQuantity18 'AsDecimal

        Dim daSelSOrdQty As New SqlDataAdapter("Select * from ReadyToDispatch Where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
        Dim dsSelSOrdQty As New DataSet
        daSelSOrdQty.Fill(dsSelSOrdQty)

        nRowCount = dsSelSOrdQty.Tables(0).Rows.Count + 1

        sSpoolId = nRowCount.ToString
        sSpoolId = sSpoolId.PadLeft(3, "0")
        sSpoolId = sSalesOrderNo + "-" + sSpoolId

        mystrReadytoDispatch.SpoolId = sSpoolId
        mystrReadytoDispatch.SpoolDt = Format(Date.Now, "dd-MMM-yyyy")

        myccPackingEntries.InsertReadytoDispatch(mystrReadytoDispatch)
        myccPackingEntries.UpdatePackingDetail(sJobcardNo, nBoxNo, nQuantity)
        myccPackingEntries.UpdateReadytoDispatch(sJobcardNo)

    End Sub
    Dim nRowCount As Integer
    Dim sSpoolId As String

    Private Sub JobcardVerification()

        If sFrom = "Barcode Device" Then
            sJobcardNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 13)
            sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 9)
        Else
            sJobcardNo = Microsoft.VisualBasic.Left(Trim(Trim(currentField)), 13)
            sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(Trim(currentField)), 9)
        End If
        
        Dim daSelJobCardInfo As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo = '" & sJobcardNo & "'", sConstr)
        Dim dsSelJobcardInfo As New DataSet
        daSelJobCardInfo.Fill(dsSelJobcardInfo)



        sArticle = dsSelJobcardInfo.Tables(0).Rows(0).Item("Article").ToString
        sBuyerCode = dsSelJobcardInfo.Tables(0).Rows(0).Item("BuyerCode").ToString
        sBuyer = dsSelJobcardInfo.Tables(0).Rows(0).Item("BuyerGroupCode").ToString
        sShipper = dsSelJobcardInfo.Tables(0).Rows(0).Item("Shipper").ToString

        sSize01 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size01").ToString
        sSize02 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size02").ToString
        sSize03 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size03").ToString
        sSize04 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size04").ToString
        sSize05 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size05").ToString
        sSize06 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size06").ToString
        sSize07 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size07").ToString
        sSize08 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size08").ToString
        sSize09 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size09").ToString
        sSize10 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size10").ToString
        sSize11 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size11").ToString
        sSize12 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size12").ToString
        sSize13 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size13").ToString
        sSize14 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size14").ToString
        sSize15 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size15").ToString
        sSize16 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size16").ToString
        sSize17 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size17").ToString
        sSize18 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size18").ToString

        'Dim daSelBuyer As New SqlDataAdapter("Select * from Buyer where BuyerCode = '" & sBuyerCode & "'", sConstr)
        'Dim dsSelBuyer As New DataSet
        'daSelBuyer.Fill(dsSelBuyer)

        'sBuyerGroup = dsSelBuyer.Tables(0).Rows(0).Item("BuyerName").ToString


        sJobcardDetailID = dsSelJobcardInfo.Tables(0).Rows(0).Item("ID").ToString

        sSalesOrderDetailId = dsSelJobcardInfo.Tables(0).Rows(0).Item("SalesOrderDetailID").ToString




    End Sub

    'Private Sub grdBuyersV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdBuyersV1.FocusedRowChanged
    '    If sIsLoaded = "Y" Then
    '        ngrdRowNo = grdBuyersV1.FocusedRowHandle

    '        If ngrdRowNo >= 0 Then
    '            sBuyerCode = grdBuyersV1.GetDataRow(ngrdRowNo).Item("BuyerCode").ToString
    '            LoadReadytoDispatchSummary()
    '        Else
    '            MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
    '            Exit Sub
    '        End If
    '    End If
    'End Sub

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

        e.Graphics.MeasureString(Mid(tbBarcode.Text, intCurrentChar + 1), font, New SizeF(PrintAreaWidth, PrintAreaHeight), fmt, intCharsFitted, intLinesFilled)

        e.Graphics.DrawString(Mid(tbBarcode.Text, intCurrentChar + 1), font, Brushes.Black, rectPrintingArea, fmt)

        intCurrentChar += intCharsFitted
    End Sub

    Private Sub grdBuyersV1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBuyersV1.Click
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdBuyersV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sBuyerCode = grdBuyersV1.GetDataRow(ngrdRowNo).Item("BuyerCode").ToString
                sIsLoaded = "N"
                LoadReadytoDispatchSummary()
                sIsLoaded = "Y"
            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub grdBuyersV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdBuyersV1.FocusedRowChanged
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdBuyersV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sBuyerCode = grdBuyersV1.GetDataRow(ngrdRowNo).Item("BuyerCode").ToString
                sIsLoaded = "N"
                LoadReadytoDispatchSummary()
                sIsLoaded = "Y"
            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub grdRTDSummaryV1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRTDSummaryV1.Click
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdRTDSummaryV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sJobcardNo = grdRTDSummaryV1.GetDataRow(ngrdRowNo).Item("JobcardNo").ToString
                sIsLoaded = "N"
                LoadReadytoDispatchDetails()
                sIsLoaded = "Y"
            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub grdRTDSummaryV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdRTDSummaryV1.FocusedRowChanged
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdRTDSummaryV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sJobcardNo = grdRTDSummaryV1.GetDataRow(ngrdRowNo).Item("JobcardNo").ToString
                sIsLoaded = "N"
                LoadReadytoDispatchDetails()
                sIsLoaded = "Y"
            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub grdRTDDetailsV1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRTDDetailsV1.DoubleClick
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdRTDDetailsV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                If grdRTDDetailsV1.RowCount = 1 Then
                    MsgBox("Single Box Details Cannot be Deleted", MsgBoxStyle.Critical)
                    sStatus = "Single Box Details Cannot be Deleted"
                    UpdateNotePad() : tbBarcode.Clear()
                    Exit Sub
                End If
                sID = grdRTDDetailsV1.GetDataRow(ngrdRowNo).Item("ID").ToString

                nYesNo = MsgBox("R U Sure U Want to Remove this Box", MsgBoxStyle.YesNo)


                If nYesNo = 6 Then
                    Dim daUpdPkgDtl As New SqlDataAdapter("Update PackingDetail Set ReadyToDispatch = '0', PackedQuantity = '0' Where Id = '" & sID & "'", sConstr)
                    Dim dsUpdPkgDtl As New DataSet
                    daUpdPkgDtl.Fill(dsUpdPkgDtl)
                    myccPackingEntries.UpdateReadytoDispatch(sJobcardNo)
                    sIsLoaded = "N"
                    LoadReadytoDispatchSummary()
                    LoadReadytoDispatchDetails()
                    sIsLoaded = "Y"
                End If

            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub UpdateNotePad()

        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then
                writer.WriteLine(Trim(tbBarcode.Text) + " | " + sStatus + " | " & DateTime.Now)
            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub

    Private Sub tbBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBarcode.TextChanged

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        LoadBuyers()
    End Sub
    Dim currentField, Path As String
    Dim nTotalBox, nCorrectBox, nWrongBox As Integer

    Private Sub cbFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFetch.Click
        'PasswordEncryption()
        'Exit Sub
        nTotalBox = 0
        nCorrectBox = 0
        nWrongBox = 0
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        Path = OpenFileDialog1.FileName

        If Path = "OpenFileDialog1" Then
            Exit Sub
        End If

        If Microsoft.VisualBasic.Right(Path.ToUpper, 3) <> "CSV" Then
            MsgBox("Wrong File Selected", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Dim i As Integer = 1
        'Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser("C:\Users\erpadmin\Desktop\BarcodeDtls1020.csv")
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    currentField = ""
                    For Each currentField In currentRow
                        'MsgBox(currentField)
                        If i = 1 Then
                            If Trim(currentField.ToUpper) <> "BARCODE" Then
                                MsgBox("Wrong CSV Selected", MsgBoxStyle.Critical)
                                Exit Sub
                            End If
                            GenerateMDData()
                        Else
                            UpdateReadyToDispatch()
                            If sStatus = "Successfully Updated" Then
                                nCorrectBox = nCorrectBox + 1
                            Else
                                nWrongBox = nWrongBox + 1
                            End If
                            GenerateMDDataDetails()
                            nTotalBox = nTotalBox + 1
                        End If
                        i = i + 1

                    Next
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line  & ex.Message is not valid and will be skipped.")
                End Try
            End While
            LoadReadytoDispatchSummary()
            UpdateMDData()
            MsgBox("Import Completed")
        End Using
    End Sub

    Dim sHeaderId As String
    Private Sub GenerateMDData()

        mystrMDData.ID = System.Guid.NewGuid.ToString()
        sHeaderId = mystrMDData.ID
        mystrMDData.CreatedBy = ""
        mystrMDData.CreatedDate = Date.Now
        mystrMDData.ModifiedBy = ""
        mystrMDData.ModifiedDate = Date.Now
        mystrMDData.ScanDate = Date.Now
        mystrMDData.FileName = Path
        mystrMDData.NumberofBoxes = 0
        mystrMDData.PerfectBoxes = 0

        myccPackingEntries.InsertMDData(mystrMDData)
    End Sub

    Dim nLen As Integer
    Private Sub UpdateReadyToDispatch()
        nLen = Microsoft.VisualBasic.Len(currentField)

        sStatus = ""
        nStringLength = Microsoft.VisualBasic.Len(Trim(currentField))

        If nStringLength < 15 Or nStringLength > 17 Then
            sStatus = "Invalid Barcode"
            UpdateNotePad()
            Exit Sub
        End If

        sJobcardNo = Microsoft.VisualBasic.Left(Trim(Trim(currentField)), 13)
        sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(Trim(currentField)), 9)

        nBoxnoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(Trim(currentField), nBoxnoLength - 1)
Aa:
        Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where JobcardNo = '" & sJobcardNo & _
                                              "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelPkgDtl As New DataSet
        daSelPkgDtl.Fill(dsSelPkgDtl)

        If dsSelPkgDtl.Tables(0).Rows.Count = 0 Then
            sStatus = "This Carton Does not belong to this Packing Detail"
            UpdateNotePad()
            Exit Sub
        End If

        If Microsoft.VisualBasic.Left(dsSelPkgDtl.Tables(0).Rows(0).Item("Article").ToString, 7) = "SOL-LEA" Then
            Dim daSelWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelWIP As New DataSet
            daSelWIP.Fill(dsSelWIP)

            If dsSelWIP.Tables(0).Rows.Count = 0 Then
                sStatus = "Jobcard WIP Not Created!"
                UpdateNotePad()
                Exit Sub
            End If

            mdlSGM.sSelectedArticle = Microsoft.VisualBasic.Left(dsSelPkgDtl.Tables(0).Rows(0).Item("Article").ToString, 7)
            If dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch").ToString = "" Then
                Dim daUpdPkgDtl As New SqlDataAdapter("Update PackingDetail Set ReadytoDispatch = '0' Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdPkgDtl As New DataSet
                daUpdPkgDtl.Fill(dsUpdPkgDtl)
                dsUpdPkgDtl.AcceptChanges()
                GoTo Aa
            End If
        Else
            If dsSelPkgDtl.Tables(0).Rows(0).Item("WIPLocation").ToString <> "PACKING" Then
                sStatus = "This Carton is Not Received at Packing Section"
                UpdateNotePad()
                Exit Sub
            End If
        End If

        If Val(dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch")) * -1 = 1 Then
            sStatus = "This Carton Already Included for Invoice Generation"
            UpdateNotePad()
            Exit Sub
        End If

        UpdatePackingFromMD()
        sStatus = "Successfully Updated"
        UpdateNotePad()



    End Sub

    Private Sub GenerateMDDataDetails()
        mystrMDDataDtls.ID = System.Guid.NewGuid.ToString()
        mystrMDDataDtls.CreatedBy = ""
        mystrMDDataDtls.CreatedDate = Date.Now
        mystrMDDataDtls.ModifiedBy = ""
        mystrMDDataDtls.ModifiedDate = Date.Now
        mystrMDDataDtls.ExeVersionNo = ""
        mystrMDDataDtls.IsApproved = 0
        mystrMDDataDtls.ApprovedBy = ""
        mystrMDDataDtls.ApprovedOn = Date.Now
        mystrMDDataDtls.ModuleName = ""
        mystrMDDataDtls.HID = mystrMDData.ID
        mystrMDDataDtls.BarCode = currentField
        mystrMDDataDtls.Status = sStatus
        mystrMDDataDtls.FileName = mystrMDData.FileName
        mystrMDDataDtls.CartonNo = nBoxNo

        myccPackingEntries.InsertMDDataDetails(mystrMDDataDtls)
    End Sub

    Private Sub UpdateMDData()

        mystrMDData.NumberofBoxes = nTotalBox
        mystrMDData.PerfectBoxes = nCorrectBox

        myccPackingEntries.UpdateMDData(mystrMDData)
    End Sub

    Private Sub UpdatePackingFromMD()

        Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where JobcardNo = '" & sJobcardNo & "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelPkgDtl As New DataSet
        daSelPkgDtl.Fill(dsSelPkgDtl)

        If dsSelPkgDtl.Tables(0).Rows.Count = 0 Then
            sStatus = "This Carton Does not belong to this customer"
            UpdateNotePad()
            Exit Sub
        End If

        nQuantity = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity").ToString)
        nQuantity01 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity01").ToString)
        nQuantity02 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity02").ToString)
        nQuantity03 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity03").ToString)
        nQuantity04 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity04").ToString)
        nQuantity05 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity05").ToString)
        nQuantity06 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity06").ToString)
        nQuantity07 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity07").ToString)
        nQuantity08 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity08").ToString)
        nQuantity09 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity09").ToString)
        nQuantity10 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity10").ToString)
        nQuantity11 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity11").ToString)
        nQuantity12 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity12").ToString)
        nQuantity13 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity13").ToString)
        nQuantity14 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity14").ToString)
        nQuantity15 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity15").ToString)
        nQuantity16 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity16").ToString)
        nQuantity17 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity17").ToString)
        nQuantity18 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity18").ToString)

        sFrom = "Mobile Computer"
        JobcardVerification()

        mystrReadytoDispatch.ID = System.Guid.NewGuid.ToString() 'AsString
        mystrReadytoDispatch.invoiceno = "" 'AsString
        mystrReadytoDispatch.InvoiceDate = Date.Now 'AsDate
        mystrReadytoDispatch.InvoiceSerialNo = "" 'AsString
        mystrReadytoDispatch.buyer = sBuyer 'AsString
        mystrReadytoDispatch.shipper = sShipper 'AsString
        mystrReadytoDispatch.SalesOrderNo = sSalesOrderNo 'AsString
        mystrReadytoDispatch.ArticleNo = sArticle 'AsString
        mystrReadytoDispatch.rate = 0 'AsDecimal
        mystrReadytoDispatch.quantity = nQuantity 'AsDecimal
        mystrReadytoDispatch.BuyerGroup = sBuyerCode 'AsString
        mystrReadytoDispatch.CreatedBy = "" 'AsString
        mystrReadytoDispatch.CreatedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrReadytoDispatch.ModifiedBy = ""
        mystrReadytoDispatch.ModifiedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrReadytoDispatch.EnteredOnMachineID = mdlSGM.strSystemName
        mystrReadytoDispatch.PackNo = sPackNo 'AsString
        mystrReadytoDispatch.JobCardNo = sJobcardNo 'AsString
        mystrReadytoDispatch.IsApproved = 0 'AsInteger
        mystrReadytoDispatch.ApprovedBy = "" 'AsString
        mystrReadytoDispatch.ApprovedOn = Date.Now 'AsDate
        mystrReadytoDispatch.ModuleName = "Barcode Scanning" 'AsString
        mystrReadytoDispatch.JobCardDetailID = sJobcardDetailID 'AsString
        mystrReadytoDispatch.InvoiceID = "" 'AsString
        mystrReadytoDispatch.SalesOrderDetailID = sSalesOrderDetailId 'AsString
        mystrReadytoDispatch.Size01 = sSize01 'AsString
        mystrReadytoDispatch.Quantity01 = nQuantity01 'AsDecimal
        mystrReadytoDispatch.Size02 = sSize02 'AsString
        mystrReadytoDispatch.Quantity02 = nQuantity02 'AsDecimal
        mystrReadytoDispatch.Size03 = sSize03 'AsString
        mystrReadytoDispatch.Quantity03 = nQuantity03 'AsDecimal
        mystrReadytoDispatch.Size04 = sSize04 'AsString
        mystrReadytoDispatch.Quantity04 = nQuantity04 'AsDecimal
        mystrReadytoDispatch.Size05 = sSize05 'AsString
        mystrReadytoDispatch.Quantity05 = nQuantity05 'AsDecimal
        mystrReadytoDispatch.Size06 = sSize06 'AsString
        mystrReadytoDispatch.Quantity06 = nQuantity06 'AsDecimal
        mystrReadytoDispatch.Size07 = sSize07 'AsString
        mystrReadytoDispatch.Quantity07 = nQuantity07 'AsDecimal
        mystrReadytoDispatch.Size08 = sSize08 'AsString
        mystrReadytoDispatch.Quantity08 = nQuantity08 'AsDecimal
        mystrReadytoDispatch.Size09 = sSize09 'AsString
        mystrReadytoDispatch.Quantity09 = nQuantity09 'AsDecimal
        mystrReadytoDispatch.Size10 = sSize10 'AsString
        mystrReadytoDispatch.Quantity10 = nQuantity10 'AsDecimal
        mystrReadytoDispatch.Size11 = sSize11 'AsString
        mystrReadytoDispatch.Quantity11 = nQuantity11 'AsDecimal
        mystrReadytoDispatch.Size12 = sSize12 'AsString
        mystrReadytoDispatch.Quantity12 = nQuantity12 'AsDecimal
        mystrReadytoDispatch.Size13 = sSize13 'AsString
        mystrReadytoDispatch.Quantity13 = nQuantity13 'AsDecimal
        mystrReadytoDispatch.Size14 = sSize14 'AsString
        mystrReadytoDispatch.Quantity14 = nQuantity14 'AsDecimal
        mystrReadytoDispatch.Size15 = sSize15 'AsString
        mystrReadytoDispatch.Quantity15 = nQuantity15 'AsDecimal
        mystrReadytoDispatch.Size16 = sSize16 'AsString
        mystrReadytoDispatch.Quantity16 = nQuantity16 'AsDecimal
        mystrReadytoDispatch.Size17 = sSize17 'AsString
        mystrReadytoDispatch.Quantity17 = nQuantity17 'AsDecimal
        mystrReadytoDispatch.Size18 = sSize18 'AsString
        mystrReadytoDispatch.Quantity18 = nQuantity18 'AsDecimal

        Dim daSelSOrdQty As New SqlDataAdapter("Select * from ReadyToDispatch Where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
        Dim dsSelSOrdQty As New DataSet
        daSelSOrdQty.Fill(dsSelSOrdQty)

        nRowCount = dsSelSOrdQty.Tables(0).Rows.Count + 1

        sSpoolId = nRowCount.ToString
        sSpoolId = sSpoolId.PadLeft(3, "0")
        sSpoolId = sSalesOrderNo + "-" + sSpoolId

        mystrReadytoDispatch.SpoolId = sSpoolId
        mystrReadytoDispatch.SpoolDt = Format(Date.Now, "dd-MMM-yyyy")

        myccPackingEntries.InsertReadytoDispatch(mystrReadytoDispatch)
        myccPackingEntries.UpdatePackingDetail(sJobcardNo, nBoxNo, nQuantity)
        myccPackingEntries.UpdateReadytoDispatch(sJobcardNo)
    End Sub

    Private Sub chkbxShow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShow.CheckedChanged
        If chkbxShow.Checked = True Then
            plMCData.Visible = True
            LoadMCData()
            LoadMCDataDetails()
        Else
            plMCData.Visible = False
        End If
    End Sub

    Private Sub LoadMCData()

        sIsLoaded = "N"
        Dim i As Integer = 0

        grdMCData.BringToFront()

Ab:
        ngrdRowCount = grdMCDataV1.RowCount
        For i = 0 To ngrdRowCount
            grdMCDataV1.DeleteRow(i)
        Next
        ngrdRowCount = grdMCDataV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        grdMCData.DataSource = myccPackingEntries.LoadMDData

        With grdMCDataV1

            .Columns(0).VisibleIndex = -1

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

        End With
        sIsLoaded = "Y"
    End Sub

    Private Sub grdMCDataV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdMCDataV1.FocusedRowChanged
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdMCDataV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sID = grdMCDataV1.GetDataRow(ngrdRowNo).Item("ID").ToString
                sIsLoaded = "N"
                LoadMCDataDetails()
                sIsLoaded = "Y"
            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub grdMCDataV1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMCDataV1.Click
        If sIsLoaded = "Y" Then
            ngrdRowNo = grdMCDataV1.FocusedRowHandle

            If ngrdRowNo >= 0 Then
                sID = grdMCDataV1.GetDataRow(ngrdRowNo).Item("ID").ToString
                sIsLoaded = "N"
                LoadMCDataDetails()
                sIsLoaded = "Y"
            Else
                'MsgBox("Customer Not Selected Properly", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub LoadMCDataDetails()
        Dim i As Integer = 0


        grdMCDataDtls.BringToFront()

Ab:
        ngrdRowCount = grdMCDataDtlsV1.RowCount
        For i = 0 To ngrdRowCount
            grdMCDataDtlsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdMCDataDtlsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        grdMCDataDtls.DataSource = myccPackingEntries.LoadMDDataDtls(sID)

        With grdMCDataDtlsV1

            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count


        End With

    End Sub

    Private Sub PasswordEncryption()
        Dim sUserName, sPassword As String
        sUserName = "Suheb"
        sPassword = "123"
        Dim Bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(sUserName)
        Dim HashofBytes() As Byte = New System.Security.Cryptography.SHA1Managed().ComputeHash(Bytes)
        Dim StrHash As String = Convert.ToBase64String(HashofBytes)

        'Using con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""G:\Program\X\Database1.mdf"";Integrated Security=True")

        'con.Open()
        sCnn.Open()

        Dim query As String = "SELECT COUNT(*) FROM User_GroupDetails WHERE UserId=@Username AND Password=@Password"
        Dim cmd As New SqlCommand(query, sCnn)
        cmd.Parameters.Add(New SqlParameter("@Username", sUserName))
        cmd.Parameters.Add(New SqlParameter("@Password", StrHash))

        Try

            If cmd.ExecuteScalar() = 1 Then

                'frmOverview.ShowDialog()

                Me.Hide()

            Else

                MsgBox("You have entered an invalid username or password")

            End If

        Catch ex As SqlException
            MsgBox(ex.Message.ToString())
        End Try

        'End Using

    End Sub

    Private Sub grdMCDataDtls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMCDataDtls.Click

    End Sub
End Class
