Option Explicit On

Imports System.Configuration
Imports System.Data.SqlClient

Imports System.Net.Mail

Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle



Public Class frmFSPackingListandlabels

#Region "Declaration"

    Inherits System.Windows.Forms.Form

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)


    Dim myOrderComponent As New OrderComponent
    Dim myPackingList As New PackingList

    Dim myOptimizerstrPPCProductionPackingListHeader As New strPPCProductionPackingListHeader
    Dim myUpperPackingListDetail As New strUpperPackingDetail


    Dim dcfLoginTime As Date
    Dim ncfTotalLoginHours As Integer
    Dim ncfTotalLoginMinutes As Integer
    Dim ncfTotalLoginSeconds As Integer

    Dim nRowCount As Integer

    Dim sUserName As String

    Dim dsLogin As New DataSet

    Dim sIsLoaded As String = "N"
    Dim dsSeason As New DataSet()
    Dim dsCurrency As New DataSet
    Dim dsFirms As New DataSet
    Dim dsShipment As New DataSet


    Dim ngrdRowNo, ngrdRowNumber As Integer

    Dim keyascii As Integer

    Public nFKCustomer As Integer
    Public nFKSize As Integer
    Dim nFKSeason As Integer

    Dim ngrdRowCount As Integer

    Public sPackingListGenerated As String
    Dim nFKAssortment As Integer

#End Region

#Region "Common Functions"

    Private Sub frmInvoicePackingListandlabels_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dcfLoginTime = Format(Date.Now, "hh:mm:ss tt")

            sIsLoaded = "N"
            Timer1.Enabled = True
            LoadUserDetails()

            LoadOrderHeader()

            sIsLoaded = "Y"
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Private Sub cbRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRefresh.Click
        LoadOrderHeader()
    End Sub




    Dim sSOID, sSODID, sSalesOrderNo As String

    Private Sub cbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCancel.Click
        Try
            ClearAll()
            LoadOrderHeader()
            plDtls.Visible = False
            plMain.Visible = True
            plMain.BringToFront()
            Me.CancelButton = cbExit

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Timer1.Enabled = False
        'Me.Hide()
        End
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = Format(Date.Now, "tt-hh:mm:ss")
        ncfTotalLoginHours = Date.Now.Subtract(dcfLoginTime).Hours
        ncfTotalLoginMinutes = Date.Now.Subtract(dcfLoginTime).Minutes
        ncfTotalLoginSeconds = Date.Now.Subtract(dcfLoginTime).Seconds

        lblTimeDifference.Text = "UT-" + Trim(Str(ncfTotalLoginHours)).PadLeft(2, CChar(CStr(0))) + ":" + Trim(Str(ncfTotalLoginMinutes)).PadLeft(2, CChar(CStr(0))) + ":" + Trim(Str(ncfTotalLoginSeconds)).PadLeft(2, CChar(CStr(0)))

    End Sub

    Private Sub LoadUserDetails()
        Try
            'lblUnitType.Text = mdlOptimizer.sUnitType
            'lblDate.Text = Format(Date.Today, "dddd - dd - MMMM - yyyy")
            'lblUserName.Text = mdlOptimizer.sUserName
            'lblUserDesignation.Text = mdlOptimizer.sUserDesignation : lblYear.Text = mdlOptimizer.sCurrentYear
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
#End Region

    Dim sTypeofPacking As String
    Dim sBoxTypeofPacking As String
    Dim nOrderTotalQuantity As Integer
    Dim sAlreadyPacked As String = "N"
#Region "Command Button Functions"

    Private Sub cbInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGenerate.Click
        Try

            'If mdlOptimizer.sUserType <> "A" Then
            '    'mdlOptimizer.nEnteringForm = 23
            '    'mdlOptimizer.VerifyAccess()
            '    'If mdlOptimizer.sMenuAdding = "N" Then
            '    '    MsgBox("Access Restricted", MsgBoxStyle.Critical)
            '    '    Exit Sub
            '    'End If
            'End If
            'mdlOptimizer.sMode = "ADD"


            If rbMultipleSizePacking.Checked = True Then
                sBoxTypeofPacking = "M" 'For Multiple Size Packing in one Box'
                sTypeofPacking = "M"
            ElseIf rbSingleSizePacking.Checked = True Then
                sBoxTypeofPacking = "S" 'For Single Size Packing in One Box'
            ElseIf rbManualPacking.Checked = True Then
                sBoxTypeofPacking = "O" 'For Manual/Optional Packing in One Box'
            End If
            ngrdRowNo = grdOrderHeaderV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Orders available for Generating Production Packing List No", MsgBoxStyle.Information)
                Exit Sub
            Else
                sSOID = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("SalesOrderId").ToString
                sSODID = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("ID").ToString
                sSalesOrderNo = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("CustWorkOrderNo").ToString

                Dim daSelPL As New SqlDataAdapter("Select * from PackingDetail where SalesOrderDetailId = '" & sSODID & "'", sConstr)
                Dim dsSelPL As New DataSet
                daSelPL.Fill(dsSelPL)
                If dsSelPL.Tables(0).Rows.Count > 0 Then
                    MsgBox("Packing List for this Sales Order Already Generated", MsgBoxStyle.Information)
                    Exit Sub
                End If

                If sTypeofPacking = "M" Then
                    If Val(tbBoxQuantity.Text) <= 0 Then
                        MsgBox("Packing Box Quantity is not Entered, Hence Cannot Generate Packing List")
                        Exit Sub
                    End If
                End If
                nOrderTotalQuantity = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("OrderQuantity")
                'myPackingList.VerifyProductionPackingListStatus(sSODID)
                Dim daSel5Pr As New SqlDataAdapter("Select * from FivePairJobcard Where ISNULL(PONr,'') <> '' And JobCardDetailID in (Select ID From JobcardDetail Where SalesOrderDetailID = '" & sSODID & _
                                                   "' And ComponentGroup = 'UPPER')", sConstr)
                Dim dsSel5Pr As New DataSet
                daSel5Pr.Fill(dsSel5Pr)

                If dsSel5Pr.Tables(0).Rows.Count > 0 Then
                    MsgBox("Packing Updated for this Salesorder", MsgBoxStyle.Information)
                    Exit Sub
                End If


                InsertProductionPackingListDetail()

                'InsertOuterCartonPackingBarcodeOption2()
                Exit Sub

                If sPackingListGenerated = "Y" Then
                    If sBoxTypeofPacking = "O" Then
                        sAlreadyPacked = "Y"

                        nPPLPKID = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKPPLPKID")
                        GoTo Aa
                    End If
                Else
                    sAlreadyPacked = "N"
                    InsertProductionPackingListDetail()
                    If sBoxTypeofPacking <> "O" Then

                        GeneratePackingListNo()
                        InsertProductionPackingListHeader()
                        MsgBox("Production Packing List Generated Sucessfully", MsgBoxStyle.Information)
                    Else
                        GeneratePackingListNo()

Aa:
                        'myPackingList.DeleteTempPackingListDetail()
                        ClearAll()

                        nFullQty = 0
                        nPartQty = 0

                        sIsLoaded = "Y"
                        LoadOrderInfo()

                        sNewOrder = "N"

                        plMain.Visible = False
                        plDtls.Visible = True
                        plDtls.BringToFront()
                        sIsLoaded = "N"

                        cbSave.Visible = True
                        cbInclude.Visible = True
                        cbRemove.Visible = True




                        Me.CancelButton = cbCancel
                    End If

                End If

            End If



        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUpdate.Click
        Try
            'If mdlOptimizer.sUserType <> "A" Then
            '    mdlOptimizer.nEnteringForm = 77
            '    mdlOptimizer.VerifyAccess()
            '    If mdlOptimizer.sMenuEditing = "N" Then
            '        MsgBox("Access Restricted", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If
            'mdlOptimizer.sMode = "EDIT"

            UploadDatafromExcelFile()
            Exit Sub

            If sIsTORGenerated = "Y" Then
                If sReleaseforModification = "Y" Then
                Else
                    MsgBox("TOR is Generated, Hence Cannot modify the order with the release the order for modification", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If
            ClearAll()



            sIsLoaded = "Y"
            LoadOrderInfo()

            sNewOrder = "N"

            plMain.Visible = False
            plDtls.Visible = True
            plDtls.BringToFront()
            sIsLoaded = "N"

            cbSave.Visible = True
            cbInclude.Visible = True
            cbRemove.Visible = True

            Me.CancelButton = cbCancel

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSave.Click
        Try
            If sAlreadyPacked <> "Y" Then
                InsertProductionPackingListHeader()
            Else
                InsertManualProductionPackingListDetail()
            End If
            'nPPLPKID()


            MsgBox("Production Packing List Generated Sucessfully", MsgBoxStyle.Information)
            ClearAll()
            LoadOrderHeader()
            plDtls.Visible = False
            plMain.Visible = True
            plMain.BringToFront()
            Me.CancelButton = cbExit

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

#End Region

#Region "Tools Validation"

    Private Sub tbPairs01_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs01.GotFocus
        Try
            tbPairs01.SelectionStart = 0
            tbPairs01.SelectionLength = tbPairs01.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs02_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs02.GotFocus
        Try
            tbPairs02.SelectionStart = 0
            tbPairs02.SelectionLength = tbPairs02.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs03_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs03.GotFocus
        Try
            tbPairs03.SelectionStart = 0
            tbPairs03.SelectionLength = tbPairs03.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs04_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs04.GotFocus
        Try
            tbPairs04.SelectionStart = 0
            tbPairs04.SelectionLength = tbPairs04.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs05_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs05.GotFocus
        Try
            tbPairs05.SelectionStart = 0
            tbPairs05.SelectionLength = tbPairs05.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs06_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs06.GotFocus
        Try
            tbPairs06.SelectionStart = 0
            tbPairs06.SelectionLength = tbPairs06.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs07_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs07.GotFocus
        Try
            tbPairs07.SelectionStart = 0
            tbPairs07.SelectionLength = tbPairs07.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs08_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs08.GotFocus
        Try
            tbPairs08.SelectionStart = 0
            tbPairs08.SelectionLength = tbPairs08.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs09_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs09.GotFocus
        Try
            tbPairs09.SelectionStart = 0
            tbPairs09.SelectionLength = tbPairs09.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs10_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs10.GotFocus
        Try
            tbPairs10.SelectionStart = 0
            tbPairs10.SelectionLength = tbPairs10.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs11_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs11.GotFocus
        Try
            tbPairs11.SelectionStart = 0
            tbPairs11.SelectionLength = tbPairs11.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs12_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs12.GotFocus
        Try
            tbPairs12.SelectionStart = 0
            tbPairs12.SelectionLength = tbPairs12.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs13_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs13.GotFocus
        Try
            tbPairs13.SelectionStart = 0
            tbPairs13.SelectionLength = tbPairs13.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs14_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs14.GotFocus
        Try
            tbPairs14.SelectionStart = 0
            tbPairs14.SelectionLength = tbPairs14.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs15_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs15.GotFocus
        Try
            tbPairs15.SelectionStart = 0
            tbPairs15.SelectionLength = tbPairs15.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs16_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs16.GotFocus
        Try
            tbPairs16.SelectionStart = 0
            tbPairs16.SelectionLength = tbPairs16.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs17_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs17.GotFocus
        Try
            tbPairs17.SelectionStart = 0
            tbPairs17.SelectionLength = tbPairs17.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs18_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs18.GotFocus
        Try
            tbPairs18.SelectionStart = 0
            tbPairs18.SelectionLength = tbPairs18.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs19_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs19.GotFocus
        Try
            tbPairs19.SelectionStart = 0
            tbPairs19.SelectionLength = tbPairs19.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs20_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbPairs20.GotFocus
        Try
            tbPairs20.SelectionStart = 0
            tbPairs20.SelectionLength = tbPairs20.Text.Length
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs01_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbPairs01.KeyPress, tbPairs02.KeyPress, tbPairs03.KeyPress, tbPairs04.KeyPress, tbPairs05.KeyPress, tbPairs06.KeyPress, tbPairs07.KeyPress, tbPairs08.KeyPress, tbPairs09.KeyPress, tbPairs10.KeyPress, tbPairs11.KeyPress, tbPairs12.KeyPress, tbPairs13.KeyPress, tbPairs14.KeyPress, tbPairs15.KeyPress, tbPairs16.KeyPress, tbPairs17.KeyPress, tbPairs18.KeyPress, tbPairs19.KeyPress, tbPairs20.KeyPress

        'MsgBox(Asc(e.KeyChar))
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Asc(e.KeyChar) = 8 Then
            e.Handled = False
        ElseIf Asc(e.KeyChar) = 46 Then
            e.Handled = True
            MsgBox("Invalid Integer")
        Else
            e.Handled = True
            MsgBox("Invalid Integer")
        End If

    End Sub

    Private Sub tbPairs01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs01.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs02_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs02.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs03_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs03.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs04_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs04.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs05_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs05.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs06_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs06.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs07_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs07.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs08_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs08.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs09_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs09.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs10.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub


    Private Sub tbPairs11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs11.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs12.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs13.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs14_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs14.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs15.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs16.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs17_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs17.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs18_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs18.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs19_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs19.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPairs20_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPairs20.TextChanged
        Try
            CalculateTotal()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub



#End Region

#Region "Functions"

    Private Sub ClearSizeInfos()
        Try
            tbPairs01.Clear() : tbPairs02.Clear() : tbPairs03.Clear() : tbPairs04.Clear() : tbPairs05.Clear()
            tbPairs06.Clear() : tbPairs07.Clear() : tbPairs08.Clear() : tbPairs09.Clear() : tbPairs10.Clear()
            tbPairs11.Clear() : tbPairs12.Clear() : tbPairs13.Clear() : tbPairs14.Clear() : tbPairs15.Clear()
            tbPairs16.Clear() : tbPairs17.Clear() : tbPairs18.Clear() : tbPairs19.Clear() : tbPairs12.Clear()
            tbPairsTotal.Clear()

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub ClearAll()
        Try
            'GenerateOrderNo()

            'nFKCurrentYear
            'nMonth
            nFKSeason = 0
            'sOrderType
            nOrderTypeSlNo = 0
            tbOrderNo.Clear()
            dpOrderDate.Value = Date.Today
            tbWeekNo.Clear()
            'OrderReceivedDt
            'OrderConfirmedDt
            tbCustomer.Clear()
            nFKCustomer = 0
            tbCustomerRefNo.Clear()
            'CustomerRefDate
            tbSizeInfo.Clear()
            nFKSize = 0
            'OrderOption
            tbPriceTerm.Clear()
            tbPayMode.Clear()
            tbDiscountPercentage.Clear()
            tbDiscountValue.Clear()
            'OrderStatus = ""
            tbTotalQuantity.Clear()

            cbInclude.Enabled = True
            cbxSeason.Enabled = True
            dpOrderDate.Enabled = True
            plTypeofPacking.Enabled = True
            plHeaderInfo.Enabled = True

            tbPairs01.Clear()
            tbPairs02.Clear()
            tbPairs03.Clear()
            tbPairs04.Clear()
            tbPairs05.Clear()
            tbPairs06.Clear()
            tbPairs07.Clear()
            tbPairs08.Clear()
            tbPairs09.Clear()
            tbPairs10.Clear()
            tbPairs11.Clear()
            tbPairs12.Clear()
            tbPairs13.Clear()
            tbPairs14.Clear()
            tbPairs15.Clear()
            tbPairs16.Clear()
            tbPairs17.Clear()
            tbPairs18.Clear()
            tbPairs19.Clear()
            tbPairs20.Clear()
            tbPairsTotal.Clear()

            ClearDetails()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

#End Region



    Dim sNewOrder, sNewCancel As String
    Dim nYesNo As Integer
    Private Sub cbInclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbInclude.Click
        Try
            If rbSingleBox.Checked = True Then
                'nPartQty = 0
                'InsertTempPackingList()
                ClearPackingQuantity()
            ElseIf rbMultipleBox.Checked = True Then
                'InsertTempPackingList()
            End If

            myPackingList.LoadPackedQuantity(nFKOrderDetail)
            UpdateQuantities()

            LoadTempPLDetails()

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Public nMonth, nOrderTypeSlNo, nMonthSlNo, nYearSlno, nSeasonSlNo As Integer
    Dim sOrderType, sOrderTypeSlNo, sSeason As String

    Public nPPLPKID, nPPLNo As Integer

    Dim sPPLNo As String
    ''--|\/|--''
    Private Sub GeneratePackingListNo()
        Try


            ngrdRowNo = grdOrderHeaderV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Orders available for Printing Work Order", MsgBoxStyle.Information)
                Exit Sub
            End If
            nFKSeason = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKSeason")
            sSeason = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("Season")

            nMonth = Format(dpPPLDate.Value, "MM")

            sOrderType = "FS"
            'myPackingList.ProductionPackingListNo(mdlOptimizer.nFKCurrentYear, nFKSeason, sOrderType)
            sPPLNo = Trim(Val(nPPLNo))
            sPPLNo = sPPLNo.PadLeft(3, CChar(CStr(0)))
            tbProductionPackingListNo.Text = Microsoft.VisualBasic.Left(sSeason, 2) + Microsoft.VisualBasic.Right(sSeason, 2) + sPPLNo

            'tbPPLWeek.Text = Str(Format(dpPPLDate.Value, "yy")) + Trim(Str(mdlOptimizer.GetWeekNumber(dpPPLDate.Value))).PadLeft(2, CChar(CStr(0)))


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim sOrderHeaderInserted As String


    Private Sub LoadOrderDetails()
        Try
            grdOrderDetails.Visible = True
            grdOrderDetails.BringToFront()

            Dim i As Integer = 0

Ab:
            ngrdRowCount = grdOrderDetailsV1.RowCount
            For i = 0 To ngrdRowCount
                grdOrderDetailsV1.DeleteRow(i)
            Next
            ngrdRowCount = grdOrderDetailsV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If

            'grdOrderDetails.DataSource = myOrderComponent.LoadOrderDetail(nFKCRMOrderHeader)

            With grdOrderDetailsV1
                .Columns(0).VisibleIndex = -1
                .Columns(3).VisibleIndex = -1
                .Columns(11).VisibleIndex = -1
                .Columns(14).VisibleIndex = -1
                .Columns(15).VisibleIndex = -1
                '.Columns(15).VisibleIndex = -1

                .Columns(42).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                .Columns(1).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                .Columns(41).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
                .Columns(39).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

                .Columns(18).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Columns(18).DisplayFormat.FormatString = "dd-MMM-yyyy"

                .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                i = 20
                For i = 20 To 39
                    .Columns(i).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                Next

            End With




        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub LoadOrderHeader()
        Try

            Dim i As Integer = 0

Ab:
            ngrdRowCount = grdOrderHeaderV1.RowCount
            For i = 0 To ngrdRowCount
                grdOrderHeaderV1.DeleteRow(i)
            Next
            ngrdRowCount = grdOrderHeaderV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If


            If rbFullShoe.Checked = True Then
                grdOrderHeader.DataSource = myOrderComponent.LoadFSOrderHeaderForPL(Format(dpDisplayDateFrom.Value, "dd-MMM-yyyy"), Format(dpDisplayDateTo.Value, "dd-MMM-yyyy"))
            Else
                grdOrderHeader.DataSource = myOrderComponent.LoadOrderHeaderForPL(Format(dpDisplayDateFrom.Value, "dd-MMM-yyyy"), Format(dpDisplayDateTo.Value, "dd-MMM-yyyy"))
            End If


            With grdOrderHeaderV1
                '.Columns(0).VisibleIndex = -1
                '.Columns(1).VisibleIndex = -1
                '.Columns(7).VisibleIndex = -1
                '.Columns(10).VisibleIndex = -1
                '.Columns(12).VisibleIndex = -1
                '.Columns(19).VisibleIndex = -1

                '.Columns(8).Width = 200
                '.Columns(8).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                '.Columns(21).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
                '.Columns(20).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

                '.Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '.Columns(4).DisplayFormat.FormatString = "dd-MMM-yyyy"

                '.Columns(21).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '.Columns(21).DisplayFormat.FormatString = "dd-MMM-yyyy"

                '.Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                '.Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            End With
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nFKArticle As Integer
    Dim sLoadingData As String

    ''--|\/|--''

    Private Sub CalculateTotal()
        Try
            tbPairsTotal.Text = Val(tbPairs01.Text) + Val(tbPairs02.Text) + Val(tbPairs03.Text) + Val(tbPairs04.Text) + Val(tbPairs05.Text) + Val(tbPairs06.Text) + Val(tbPairs07.Text) + Val(tbPairs08.Text) + Val(tbPairs09.Text) + Val(tbPairs10.Text) + Val(tbPairs11.Text) + Val(tbPairs12.Text) + Val(tbPairs13.Text) + Val(tbPairs14.Text) + Val(tbPairs15.Text) + Val(tbPairs16.Text) + Val(tbPairs17.Text) + Val(tbPairs18.Text) + Val(tbPairs19.Text) + Val(tbPairs20.Text)
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub ClearDetails()
        Try
            'tbCustomerOrderNo.Clear()
            nFKArticle = 0
            'myOptimizerSTCRMCustomerOrderDetail.FKModeofTransport = cbxModeofTransport.SelectedValue
            tbCustomer.Clear()
            'tbDestination.Clear()
            'myOptimizerSTCRMCustomerOrderDetail.FKFactory = cbxFactory.SelectedValue
            'tbPrice.Clear()

            tbPairs01.Clear()
            tbPairs02.Clear()
            tbPairs03.Clear()
            tbPairs04.Clear()
            tbPairs05.Clear()
            tbPairs06.Clear()
            tbPairs07.Clear()
            tbPairs08.Clear()
            tbPairs09.Clear()
            tbPairs10.Clear()
            tbPairs11.Clear()
            tbPairs12.Clear()
            tbPairs13.Clear()
            tbPairs14.Clear()
            tbPairs15.Clear()
            tbPairs16.Clear()
            tbPairs17.Clear()
            tbPairs18.Clear()
            tbPairs19.Clear()
            tbPairs20.Clear()
            tbPairsTotal.Clear()



        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub dpOrderDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpOrderDate.ValueChanged
        'tbWeekNo.Text = Str(Format(dpOrderDate.Value, "yy")) + Trim(Str(mdlOptimizer.GetWeekNumber(dpOrderDate.Value))).PadLeft(2, CChar(CStr(0)))
    End Sub


    Dim nSAQuantity01, nSAQuantity02, nSAQuantity03, nSAQuantity04, nSAQuantity05, nSAQuantity06, nSAQuantity07, nSAQuantity08, nSAQuantity09, nSAQuantity10 As Integer
    Dim nSAQuantity11, nSAQuantity12, nSAQuantity13, nSAQuantity14, nSAQuantity15, nSAQuantity16, nSAQuantity17, nSAQuantity18, nSAQuantity19, nSAQuantity20 As Integer
    Dim nSATotalQuantity As Integer

    Dim nOrdQuantity01, nOrdQuantity02, nOrdQuantity03, nOrdQuantity04, nOrdQuantity05, nOrdQuantity06, nOrdQuantity07, nOrdQuantity08, nOrdQuantity09, nOrdQuantity10 As Integer
    Dim nOrdQuantity11, nOrdQuantity12, nOrdQuantity13, nOrdQuantity14, nOrdQuantity15, nOrdQuantity16, nOrdQuantity17, nOrdQuantity18, nOrdQuantity19, nOrdQuantity20 As Integer
    Dim nOrdTotalQuantity As Integer

    Dim nBoxQuantity01, nBoxQuantity02, nBoxQuantity03, nBoxQuantity04, nBoxQuantity05, nBoxQuantity06, nBoxQuantity07, nBoxQuantity08, nBoxQuantity09, nBoxQuantity10 As Integer
    Dim nBoxQuantity11, nBoxQuantity12, nBoxQuantity13, nBoxQuantity14, nBoxQuantity15, nBoxQuantity16, nBoxQuantity17, nBoxQuantity18, nBoxQuantity19, nBoxQuantity20 As Integer
    Dim nBoxTotalQuantity, nPLQty As Integer

    Public nInsQuantity01, nInsQuantity02, nInsQuantity03, nInsQuantity04, nInsQuantity05, nInsQuantity06, nInsQuantity07, nInsQuantity08, nInsQuantity09, nInsQuantity10 As Integer
    Public nInsQuantity11, nInsQuantity12, nInsQuantity13, nInsQuantity14, nInsQuantity15, nInsQuantity16, nInsQuantity17, nInsQuantity18, nInsQuantity19, nInsQuantity20 As Integer
    Public nInsTotalQuantity As Integer

    Private Sub LockPairsEntry()
        Try
            tbPairs01.ReadOnly = True : tbPairs02.ReadOnly = True : tbPairs03.ReadOnly = True : tbPairs04.ReadOnly = True : tbPairs05.ReadOnly = True
            tbPairs06.ReadOnly = True : tbPairs07.ReadOnly = True : tbPairs08.ReadOnly = True : tbPairs09.ReadOnly = True : tbPairs10.ReadOnly = True
            tbPairs11.ReadOnly = True : tbPairs12.ReadOnly = True : tbPairs13.ReadOnly = True : tbPairs14.ReadOnly = True : tbPairs15.ReadOnly = True
            tbPairs16.ReadOnly = True : tbPairs17.ReadOnly = True : tbPairs18.ReadOnly = True : tbPairs19.ReadOnly = True : tbPairs20.ReadOnly = True

            tbPairs01.Clear() : tbPairs02.Clear() : tbPairs03.Clear() : tbPairs04.Clear() : tbPairs05.Clear()
            tbPairs06.Clear() : tbPairs07.Clear() : tbPairs08.Clear() : tbPairs09.Clear() : tbPairs10.Clear()
            tbPairs11.Clear() : tbPairs12.Clear() : tbPairs13.Clear() : tbPairs14.Clear() : tbPairs15.Clear()
            tbPairs16.Clear() : tbPairs17.Clear() : tbPairs18.Clear() : tbPairs19.Clear() : tbPairs20.Clear()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub UnLockPairsEntry()
        Try
            tbPairs01.ReadOnly = False : tbPairs02.ReadOnly = False : tbPairs03.ReadOnly = False : tbPairs04.ReadOnly = False : tbPairs05.ReadOnly = False
            tbPairs06.ReadOnly = False : tbPairs07.ReadOnly = False : tbPairs08.ReadOnly = False : tbPairs09.ReadOnly = False : tbPairs10.ReadOnly = False
            tbPairs11.ReadOnly = False : tbPairs12.ReadOnly = False : tbPairs13.ReadOnly = False : tbPairs14.ReadOnly = False : tbPairs15.ReadOnly = False
            tbPairs16.ReadOnly = False : tbPairs17.ReadOnly = False : tbPairs18.ReadOnly = False : tbPairs19.ReadOnly = False : tbPairs20.ReadOnly = False
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub LoadOrderInfo()
        Try
            'ngrdRowCount = grdOrderHeaderV1.RowCount
            ngrdRowNo = grdOrderHeaderV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Pending Orders available for Generating Job Card", MsgBoxStyle.Information)
                Exit Sub
            Else
                'nFKCRMOrderHeader = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("PKID")
                nFKSeason = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKSeason")
                'tbSeason.Text = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("Season")
                cbxSeason.SelectedValue = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKSeason")
                tbOrderNo.Text = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("OrderNo")
                nFKCustomer = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKCustomer")
                tbCustomer.Text = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("TradeName")
                tbCustomerRefNo.Text = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("CustomerRef")
                'tbCurrency.Text = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("Currency")
                cbxCurrency.SelectedValue = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKCurrency")
                nFKSize = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("FKSize")
                tbSizeInfo.Text = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("Size")
                If grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("TypeofPacking") = "N" Then
                    rbNormalPacking.Checked = True
                Else
                    rbAssortmentPacking.Checked = True
                End If

                LoadOrderDetails()

            End If




        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nPKIDCustomerOrderDetail As Integer

    Dim sIsTORGenerated, sReleaseforModification As String

    Private Sub cbDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDelete.Click
        Try
            'Dim oMsg As System.Web.Mail.MailMessage = New System.Web.Mail.MailMessage()

            'oMsg.From = "noone@nobody.com"
            'oMsg.To = "someone@somewhere.com"
            'oMsg.Subject = "Email with Attachment Demo"
            'oMsg.Body = "This is the main body of the email"
            'Dim oAttch As MailAttachment = New MailAttachment("D:\myattachment.zip")
            'oMsg.Attachments.Add(oAttch)
            'SmtpMail.Send(oMsg)

            ''Option 01''
            Dim SMTPServer As New SmtpClient()
            'SMTPServer.Host = "smtp.gmail.com"
            SMTPServer.Host = "rainmail.ahindia.com"

            'SMTPServer.Port = 587
            SMTPServer.Port = 25

            'SMTPServer.EnableSsl = True

            SMTPServer.UseDefaultCredentials = False

            'SMTPServer.Credentials = New System.Net.NetworkCredential("noor2677@gmail.com", "77")
            SMTPServer.Credentials = New System.Net.NetworkCredential("erp", "yarabbi786")
            Dim MailMessage As New MailMessage()

            MailMessage.To.Add(New MailAddress("ppc@ahindia.com"))
            MailMessage.[To].Add(New MailAddress("erp@ahindia.com"))

            MailMessage.From = New MailAddress("erp@ahindia.com", "ERP", System.Text.Encoding.UTF8)

            MailMessage.Subject = "E. Mail Testing - Mail Test"

            MailMessage.Body = "This is the test text for Gmail email"


            SMTPServer.Send(MailMessage)
            Exit Sub
            ''Option 01''

            ''Dim mail As New MailMessage()
            ''Dim SmtpServer As New SmtpClient("rainmail.ahindia.com")
            ''mail.From = New MailAddress("erp@ahindia.com")
            ''mail.To.Add("ppc@ahindia.com")
            ' ''mail.[To].Add("to_address")
            ''mail.Subject = "Test Mail - 1"
            ''mail.Body = "mail with attachment"

            '' ''Dim attachment As System.Net.Mail.Attachment
            '' ''attachment = New System.Net.Mail.Attachment("your attachment file")
            '' ''mail.Attachments.Add(attachment)

            ''SmtpServer.Port = 25 '587
            ''SmtpServer.Credentials = New System.Net.NetworkCredential("erp", "yarabbi786")
            ''SmtpServer.EnableSsl = True

            ''SmtpServer.Send(mail)
            ''MessageBox.Show("mail Send")

            '' ''Dim mail As New MailMessage()
            ' '' ''Dim att = New Attachment("J:\vb\RizRandom\RizRandom\Results\UBM.xlsx")
            '' ''Dim SmtpServer As New SmtpClient()


            '' ''Dim Smtp As SmtpClient = New SmtpClient("smtp.gmail.com", 587)


            '' ''Smtp.UseDefaultCredentials = False
            '' ''Smtp.Credentials = New Net.NetworkCredential("erp", "yarabbi786")
            '' ''Smtp.EnableSsl = True
            ' '' ''            mail.Attachments.Add(att)

            '' ''SmtpServer.Host = "smtp.gmail.com"
            '' ''mail = New MailMessage()
            '' ''mail.From = New MailAddress("Noor2677@gmail.com")
            '' ''mail.To.Add("erp@ahindia.com")
            '' ''mail.Subject = "Test Mail"
            '' ''mail.Body = "This is for testing SMTP mail from GMAIL"

            '' ''SmtpServer.Send(mail)
            '' ''MsgBox("mail send")

            Exit Sub

            'If mdlOptimizer.sUserType <> "A" Then
            '    mdlOptimizer.nEnteringForm = 77
            '    mdlOptimizer.VerifyAccess()
            '    If mdlOptimizer.sMenuDeleting = "N" Then
            '        MsgBox("Access Restricted", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If
            'mdlOptimizer.sMode = "CANCEL ORDER"
            sNewCancel = "Y"

            sIsLoaded = "Y"
            LoadOrderInfo()

            sNewOrder = "N"

            plMain.Visible = False
            plDtls.Visible = True
            plDtls.BringToFront()
            sIsLoaded = "N"

            cbSave.Visible = False
            cbInclude.Visible = False
            cbRemove.Visible = False

            Me.CancelButton = cbCancel


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbCancelOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            ngrdRowNo = grdOrderDetailsV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Pending Orders available for Generating Job Card", MsgBoxStyle.Information)
                Exit Sub
            Else

                nYesNo = MsgBox("R U Sure U want to cancel the selected Order Detail?", MsgBoxStyle.YesNo)

                If nYesNo = 6 Then
                    nPKIDCustomerOrderDetail = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("PKID")

                    LoadOrderDetails()

                    MsgBox("Details Successfully Deleted", MsgBoxStyle.Information)
                    grdOrderDetailsV1.FocusedRowHandle = ngrdRowNo
                End If

            End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub dpDeliveryDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        sIsLoaded = "N"
    End Sub

    Private Sub InsertProductionPackingListHeader()
        Try

            myOptimizerstrPPCProductionPackingListHeader.PKID = nPPLPKID
            'myOptimizerstrPPCProductionPackingListHeader.FKYear = mdlOptimizer.nFKCurrentYear
            myOptimizerstrPPCProductionPackingListHeader.Month = Format(dpPPLDate.Value, "MM")
            myOptimizerstrPPCProductionPackingListHeader.FKSection = ""
            myOptimizerstrPPCProductionPackingListHeader.OrderType = sOrderType
            myOptimizerstrPPCProductionPackingListHeader.FKSeason = nFKSeason
            myOptimizerstrPPCProductionPackingListHeader.PPLNo = nPPLNo
            myOptimizerstrPPCProductionPackingListHeader.PPLDate = Format(dpPPLDate.Value, "dd-MMM-yyyy")
            myOptimizerstrPPCProductionPackingListHeader.PPLWeek = Trim(tbPPLWeek.Text)
            myOptimizerstrPPCProductionPackingListHeader.RevisionNo = 0
            'myOptimizerstrPPCProductionPackingListHeader.FKOrderHeader = nFKCRMOrderHeader
            myOptimizerstrPPCProductionPackingListHeader.TypeofPacking = sTypeofPacking
            myOptimizerstrPPCProductionPackingListHeader.FKAssortment = nFKAssortment
            myOptimizerstrPPCProductionPackingListHeader.BoxQuantity = Val(tbBoxQuantity.Text)
            myOptimizerstrPPCProductionPackingListHeader.BoxPackingType = sBoxTypeofPacking
            myOptimizerstrPPCProductionPackingListHeader.TotalBoxes = grdTempPlQtyV1.RowCount
            myOptimizerstrPPCProductionPackingListHeader.Remarks = ""
            'myOptimizerstrPPCProductionPackingListHeader.CreatedBy = mdlOptimizer.nUserName
            myOptimizerstrPPCProductionPackingListHeader.CreatedDt = Format(Date.Now, "dd-MMM-yy:hh:mm:ss-t")
            myOptimizerstrPPCProductionPackingListHeader.ModifiedBy = 0
            myOptimizerstrPPCProductionPackingListHeader.ModifiedDt = ""
            myOptimizerstrPPCProductionPackingListHeader.DeletedBy = 0
            myOptimizerstrPPCProductionPackingListHeader.DeletedDt = 0

            If myPackingList.InsertProductionPackingListHeader(myOptimizerstrPPCProductionPackingListHeader) = True Then
                Me.DialogResult = DialogResult.OK

                If sBoxTypeofPacking = "O" Then
                    InsertManualProductionPackingListDetail()
                Else
                    InsertProductionPackingListDetail()
                End If


            Else
                MsgBox(myOrderComponent.ErrorMessage)
                Exit Sub
            End If


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub rbProductionPackingList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbProductionPackingList.CheckedChanged
        Try
            If rbProductionPackingList.Checked = True Then
                'GeneratePackingListNo()

            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
    Dim nFKOrderDetail As Integer
    Private Sub InsertProductionPackingListDetail()
        Try
            'LoadOrderDetails()

            ngrdRowCount = grdOrderHeaderV1.RowCount

            ngrdRowNo = grdOrderHeaderV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Orders available for Generating Production Packing List No", MsgBoxStyle.Information)
                Exit Sub
            Else
                sSODID = grdOrderHeaderV1.GetDataRow(ngrdRowNo).Item("SalesOrderId")
            End If

            Dim i As Integer = ngrdRowNo '0

            'For i = 0 To ngrdRowCount - 1

            'nFKOrderDetail = grdOrderHeaderV1.GetDataRow(i).Item("PKID")
            sSODID = grdOrderHeaderV1.GetDataRow(i).Item("ID")
            nOrdQuantity01 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity01").ToString
            nOrdQuantity02 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity02").ToString
            nOrdQuantity03 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity03").ToString
            nOrdQuantity04 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity04").ToString
            nOrdQuantity05 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity05").ToString
            nOrdQuantity06 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity06").ToString
            nOrdQuantity07 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity07").ToString
            nOrdQuantity08 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity08").ToString
            nOrdQuantity09 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity09").ToString
            nOrdQuantity10 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity10").ToString
            nOrdQuantity11 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity11").ToString
            nOrdQuantity12 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity12").ToString
            nOrdQuantity13 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity13").ToString
            nOrdQuantity14 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity14").ToString
            nOrdQuantity15 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity15").ToString
            nOrdQuantity16 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity16").ToString
            nOrdQuantity17 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity17").ToString
            nOrdQuantity18 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity18").ToString
            nOrdQuantity19 = 0
            nOrdQuantity20 = 0
            nOrdTotalQuantity = grdOrderHeaderV1.GetDataRow(i).Item("OrderQuantity")

            nBoxQuantity01 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity01").ToString
            nBoxQuantity02 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity02").ToString
            nBoxQuantity03 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity03").ToString
            nBoxQuantity04 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity04").ToString
            nBoxQuantity05 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity05").ToString
            nBoxQuantity06 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity06").ToString
            nBoxQuantity07 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity07").ToString
            nBoxQuantity08 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity08").ToString
            nBoxQuantity09 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity09").ToString
            nBoxQuantity10 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity10").ToString
            nBoxQuantity11 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity11").ToString
            nBoxQuantity12 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity12").ToString
            nBoxQuantity13 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity13").ToString
            nBoxQuantity14 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity14").ToString
            nBoxQuantity15 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity15").ToString
            nBoxQuantity16 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity16").ToString
            nBoxQuantity17 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity17").ToString
            nBoxQuantity18 = grdOrderHeaderV1.GetDataRow(i).Item("Quantity18").ToString
            nBoxQuantity19 = 0
            nBoxQuantity20 = 0
            nBoxTotalQuantity = grdOrderHeaderV1.GetDataRow(i).Item("OrderQuantity")

            'If sTypeofPacking = "N" Then
            nTotalBoxes = Math.Ceiling(nOrdTotalQuantity / Val(tbBoxQuantity.Text))
            nBoxQuantity = Val(tbBoxQuantity.Text)
            'ElseIf sTypeofPacking = "A" Then
            '    nFKAssortment = grdOrderHeaderV1.GetDataRow(i).Item("FKAssortment")
            '    nTotalBoxes = grdOrderHeaderV1.GetDataRow(i).Item("PackCount")
            'End If

            sFullQuantity = "Y"
            nFullQty = 0
            nPartQty = 0
            InsertStickers()

            'Next
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nTotalBoxes As Integer
    Dim sFullQuantity As String
    Dim nBoxQuantity, nTotalQty As Integer
    Dim nFullQty, nPartQty As Integer
    Dim nBoxNo As Integer

    Private Sub InsertStickers()
        Try
            If sTypeofPacking = "A" Then


                myPackingList.LoadAssortmentDetails(nFKAssortment)
                nTotalQty = nInsTotalQuantity

                Dim j As Integer = 1
                For j = 1 To nTotalBoxes

                    myUpperPackingListDetail.ID = System.Guid.NewGuid.ToString()
                    myUpperPackingListDetail.SODID = sSODID
                    myUpperPackingListDetail.Quantity = nTotalQty
                    myUpperPackingListDetail.Quantity01 = nInsQuantity01
                    myUpperPackingListDetail.Quantity02 = nInsQuantity02
                    myUpperPackingListDetail.Quantity03 = nInsQuantity03
                    myUpperPackingListDetail.Quantity04 = nInsQuantity04
                    myUpperPackingListDetail.Quantity05 = nInsQuantity05
                    myUpperPackingListDetail.Quantity06 = nInsQuantity06
                    myUpperPackingListDetail.Quantity07 = nInsQuantity07
                    myUpperPackingListDetail.Quantity08 = nInsQuantity08
                    myUpperPackingListDetail.Quantity09 = nInsQuantity09
                    myUpperPackingListDetail.Quantity10 = nInsQuantity10
                    myUpperPackingListDetail.Quantity11 = nInsQuantity11
                    myUpperPackingListDetail.Quantity12 = nInsQuantity12
                    myUpperPackingListDetail.Quantity13 = nInsQuantity13
                    myUpperPackingListDetail.Quantity14 = nInsQuantity14
                    myUpperPackingListDetail.Quantity15 = nInsQuantity15
                    myUpperPackingListDetail.Quantity16 = nInsQuantity16
                    myUpperPackingListDetail.Quantity17 = nInsQuantity17
                    myUpperPackingListDetail.Quantity18 = nInsQuantity18


                    If myPackingList.InsertProductionPackingListDetail(myUpperPackingListDetail) = True Then
                        Me.DialogResult = DialogResult.OK

                    Else
                        MsgBox(myOrderComponent.ErrorMessage)
                        Exit Sub
                    End If

                    'If sFullQuantity = "Y" Then
                Next
                nInsQuantity01 = "0" : nInsQuantity02 = "0" : nInsQuantity03 = "0" : nInsQuantity04 = "0" : nInsQuantity05 = "0"
                nInsQuantity06 = "0" : nInsQuantity07 = "0" : nInsQuantity08 = "0" : nInsQuantity09 = "0" : nInsQuantity10 = "0"
                nInsQuantity11 = "0" : nInsQuantity12 = "0" : nInsQuantity13 = "0" : nInsQuantity14 = "0" : nInsQuantity15 = "0"
                nInsQuantity16 = "0" : nInsQuantity17 = "0" : nInsQuantity18 = "0" : nInsQuantity19 = "0" : nInsQuantity20 = "0"
                nTotalQty = "0"
            Else
                'sFullQuantity = "Y"
                Dim j As Integer = 1
                For j = 1 To nTotalBoxes
                    nBoxNo = j


                    If sFullQuantity = "Y" Then
                        nFullQty = nFullQty + 1
                        '01'
                        If nBoxQuantity01 > 0 Then
                            If nBoxQuantity01 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity01 = nBoxQuantity
                                nBoxQuantity01 = nBoxQuantity01 - nInsQuantity01
                                nTotalQty = nInsQuantity01

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '01'

                        '02'
                        If nBoxQuantity02 > 0 Then
                            If nBoxQuantity02 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity02 = nBoxQuantity
                                nBoxQuantity02 = nBoxQuantity02 - nInsQuantity02
                                nTotalQty = nInsQuantity02

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '02'

                        '03'
                        If nBoxQuantity03 > 0 Then
                            If nBoxQuantity03 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity03 = nBoxQuantity
                                nBoxQuantity03 = nBoxQuantity03 - nInsQuantity03
                                nTotalQty = nInsQuantity03

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '03'

                        '04'
                        If nBoxQuantity04 > 0 Then
                            If nBoxQuantity04 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity04 = nBoxQuantity
                                nBoxQuantity04 = nBoxQuantity04 - nInsQuantity04
                                nTotalQty = nInsQuantity04

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '04'

                        '05'
                        If nBoxQuantity05 > 0 Then
                            If nBoxQuantity05 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity05 = nBoxQuantity
                                nBoxQuantity05 = nBoxQuantity05 - nInsQuantity05
                                nTotalQty = nInsQuantity05

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '05'

                        '06'
                        If nBoxQuantity06 > 0 Then
                            If nBoxQuantity06 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity06 = nBoxQuantity
                                nBoxQuantity06 = nBoxQuantity06 - nInsQuantity06
                                nTotalQty = nInsQuantity06

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '06'

                        '07'
                        If nBoxQuantity07 > 0 Then
                            If nBoxQuantity07 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity07 = nBoxQuantity
                                nBoxQuantity07 = nBoxQuantity07 - nInsQuantity07
                                nTotalQty = nInsQuantity07

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '07'

                        '08'
                        If nBoxQuantity08 > 0 Then
                            If nBoxQuantity08 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity08 = nBoxQuantity
                                nBoxQuantity08 = nBoxQuantity08 - nInsQuantity08
                                nTotalQty = nInsQuantity08

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '08'

                        '09'
                        If nBoxQuantity09 > 0 Then
                            If nBoxQuantity09 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity09 = nBoxQuantity
                                nBoxQuantity09 = nBoxQuantity09 - nInsQuantity09
                                nTotalQty = nInsQuantity09

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '09'

                        '10'
                        If nBoxQuantity10 > 0 Then
                            If nBoxQuantity10 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity10 = nBoxQuantity
                                nBoxQuantity10 = nBoxQuantity10 - nInsQuantity10
                                nTotalQty = nInsQuantity10

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '10'

                        '11'
                        If nBoxQuantity11 > 0 Then
                            If nBoxQuantity11 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity11 = nBoxQuantity
                                nBoxQuantity11 = nBoxQuantity11 - nInsQuantity11
                                nTotalQty = nInsQuantity11

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '11'

                        '12'
                        If nBoxQuantity12 > 0 Then
                            If nBoxQuantity12 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity12 = nBoxQuantity
                                nBoxQuantity12 = nBoxQuantity12 - nInsQuantity12
                                nTotalQty = nInsQuantity12

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '12'

                        '13'
                        If nBoxQuantity13 > 0 Then
                            If nBoxQuantity13 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity13 = nBoxQuantity
                                nBoxQuantity13 = nBoxQuantity13 - nInsQuantity13
                                nTotalQty = nInsQuantity13

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '13'

                        '14'
                        If nBoxQuantity14 > 0 Then
                            If nBoxQuantity14 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity14 = nBoxQuantity
                                nBoxQuantity14 = nBoxQuantity14 - nInsQuantity14
                                nTotalQty = nInsQuantity14

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '14'

                        '15'
                        If nBoxQuantity15 > 0 Then
                            If nBoxQuantity15 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity15 = nBoxQuantity
                                nBoxQuantity15 = nBoxQuantity15 - nInsQuantity15
                                nTotalQty = nInsQuantity15

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '15'

                        '16'
                        If nBoxQuantity16 > 0 Then
                            If nBoxQuantity16 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity16 = nBoxQuantity
                                nBoxQuantity16 = nBoxQuantity16 - nInsQuantity16
                                nTotalQty = nInsQuantity16

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '16'

                        '17'
                        If nBoxQuantity17 > 0 Then
                            If nBoxQuantity17 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity17 = nBoxQuantity
                                nBoxQuantity17 = nBoxQuantity17 - nInsQuantity17
                                nTotalQty = nInsQuantity17

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '17'

                        '18'
                        If nBoxQuantity18 > 0 Then
                            If nBoxQuantity18 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity18 = nBoxQuantity
                                nBoxQuantity18 = nBoxQuantity18 - nInsQuantity18
                                nTotalQty = nInsQuantity18

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '18'

                        '19'
                        If nBoxQuantity19 > 0 Then
                            If nBoxQuantity19 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity19 = nBoxQuantity
                                nBoxQuantity19 = nBoxQuantity19 - nInsQuantity19
                                nTotalQty = nInsQuantity19

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '19'

                        '20'
                        If nBoxQuantity20 > 0 Then
                            If nBoxQuantity20 >= nBoxQuantity Then
                                sFullQuantity = "Y"
                                nInsQuantity20 = nBoxQuantity
                                nBoxQuantity20 = nBoxQuantity20 - nInsQuantity20
                                nTotalQty = nInsQuantity20

                                GoTo Aa
                            Else
                                nTotalQty = 0
                                sFullQuantity = "N"
                                'GoTo A2
                            End If
                        End If
                        '20'
                        nFullQty = nFullQty - 1
                    End If

                    If sFullQuantity = "N" Then
                        nPartQty = nPartQty + 1
                        '01'
                        If nBoxQuantity01 > 0 Then
                            nInsQuantity01 = nBoxQuantity01
                            nBoxQuantity01 = nBoxQuantity01 - nInsQuantity01
                            nTotalQty = nInsQuantity01

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '01'

                        '02'
                        If nBoxQuantity02 > 0 Then
                            If nTotalQty + nBoxQuantity02 > nBoxQuantity Then
                                nInsQuantity02 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity02 = nBoxQuantity02
                            End If
                            nBoxQuantity02 = nBoxQuantity02 - nInsQuantity02
                            nTotalQty = nTotalQty + nInsQuantity02

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '02'

                        '03'
                        If nBoxQuantity03 > 0 Then
                            If nTotalQty + nBoxQuantity03 > nBoxQuantity Then
                                nInsQuantity03 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity03 = nBoxQuantity03
                            End If
                            nBoxQuantity03 = nBoxQuantity03 - nInsQuantity03
                            nTotalQty = nTotalQty + nInsQuantity03

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '03'

                        '04'
                        If nBoxQuantity04 > 0 Then
                            If nTotalQty + nBoxQuantity04 > nBoxQuantity Then
                                nInsQuantity04 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity04 = nBoxQuantity04
                            End If
                            nBoxQuantity04 = nBoxQuantity04 - nInsQuantity04
                            nTotalQty = nTotalQty + nInsQuantity04

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '04'

                        '05'
                        If nBoxQuantity05 > 0 Then
                            If nTotalQty + nBoxQuantity05 > nBoxQuantity Then
                                nInsQuantity05 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity05 = nBoxQuantity05
                            End If
                            nBoxQuantity05 = nBoxQuantity05 - nInsQuantity05
                            nTotalQty = nTotalQty + nInsQuantity05

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '05'

                        '03'
                        If nBoxQuantity06 > 0 Then
                            If nTotalQty + nBoxQuantity06 > nBoxQuantity Then
                                nInsQuantity06 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity06 = nBoxQuantity06
                            End If
                            nBoxQuantity06 = nBoxQuantity06 - nInsQuantity06
                            nTotalQty = nTotalQty + nInsQuantity06

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '06'

                        '07'
                        If nBoxQuantity07 > 0 Then
                            If nTotalQty + nBoxQuantity07 > nBoxQuantity Then
                                nInsQuantity07 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity07 = nBoxQuantity07
                            End If
                            nBoxQuantity07 = nBoxQuantity07 - nInsQuantity07
                            nTotalQty = nTotalQty + nInsQuantity07

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '07'

                        '08'
                        If nBoxQuantity08 > 0 Then
                            If nTotalQty + nBoxQuantity08 > nBoxQuantity Then
                                nInsQuantity08 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity08 = nBoxQuantity08
                            End If
                            nBoxQuantity08 = nBoxQuantity08 - nInsQuantity08
                            nTotalQty = nTotalQty + nInsQuantity08

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '08'

                        '09'
                        If nBoxQuantity09 > 0 Then
                            If nTotalQty + nBoxQuantity09 > nBoxQuantity Then
                                nInsQuantity09 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity09 = nBoxQuantity09
                            End If
                            nBoxQuantity09 = nBoxQuantity09 - nInsQuantity09
                            nTotalQty = nTotalQty + nInsQuantity09

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '09'

                        '10'
                        If nBoxQuantity10 > 0 Then
                            If nTotalQty + nBoxQuantity10 > nBoxQuantity Then
                                nInsQuantity10 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity10 = nBoxQuantity10
                            End If
                            nBoxQuantity10 = nBoxQuantity10 - nInsQuantity10
                            nTotalQty = nTotalQty + nInsQuantity10

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '10'

                        '11'
                        If nBoxQuantity11 > 0 Then
                            If nTotalQty + nBoxQuantity11 > nBoxQuantity Then
                                nInsQuantity11 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity11 = nBoxQuantity11
                            End If
                            nBoxQuantity11 = nBoxQuantity11 - nInsQuantity11
                            nTotalQty = nTotalQty + nInsQuantity11

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '11'

                        '12'
                        If nBoxQuantity12 > 0 Then
                            If nTotalQty + nBoxQuantity12 > nBoxQuantity Then
                                nInsQuantity12 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity12 = nBoxQuantity12
                            End If
                            nBoxQuantity12 = nBoxQuantity12 - nInsQuantity12
                            nTotalQty = nTotalQty + nInsQuantity12

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '12'

                        '13'
                        If nBoxQuantity13 > 0 Then
                            If nTotalQty + nBoxQuantity13 > nBoxQuantity Then
                                nInsQuantity13 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity13 = nBoxQuantity13
                            End If
                            nBoxQuantity13 = nBoxQuantity13 - nInsQuantity13
                            nTotalQty = nTotalQty + nInsQuantity13

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '13'

                        '14'
                        If nBoxQuantity14 > 0 Then
                            If nTotalQty + nBoxQuantity14 > nBoxQuantity Then
                                nInsQuantity14 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity14 = nBoxQuantity14
                            End If
                            nBoxQuantity14 = nBoxQuantity14 - nInsQuantity14
                            nTotalQty = nTotalQty + nInsQuantity14

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '14'

                        '15'
                        If nBoxQuantity15 > 0 Then
                            If nTotalQty + nBoxQuantity15 > nBoxQuantity Then
                                nInsQuantity15 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity15 = nBoxQuantity15
                            End If
                            nBoxQuantity15 = nBoxQuantity15 - nInsQuantity15
                            nTotalQty = nTotalQty + nInsQuantity15

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '15'

                        '13'
                        If nBoxQuantity16 > 0 Then
                            If nTotalQty + nBoxQuantity16 > nBoxQuantity Then
                                nInsQuantity16 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity16 = nBoxQuantity16
                            End If
                            nBoxQuantity16 = nBoxQuantity16 - nInsQuantity16
                            nTotalQty = nTotalQty + nInsQuantity16

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '16'

                        '17'
                        If nBoxQuantity17 > 0 Then
                            If nTotalQty + nBoxQuantity17 > nBoxQuantity Then
                                nInsQuantity17 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity17 = nBoxQuantity17
                            End If
                            nBoxQuantity17 = nBoxQuantity17 - nInsQuantity17
                            nTotalQty = nTotalQty + nInsQuantity17

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '17'

                        '18'
                        If nBoxQuantity18 > 0 Then
                            If nTotalQty + nBoxQuantity18 > nBoxQuantity Then
                                nInsQuantity18 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity18 = nBoxQuantity18
                            End If
                            nBoxQuantity18 = nBoxQuantity18 - nInsQuantity18
                            nTotalQty = nTotalQty + nInsQuantity18

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '18'

                        '19'
                        If nBoxQuantity19 > 0 Then
                            If nTotalQty + nBoxQuantity19 > nBoxQuantity Then
                                nInsQuantity19 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity19 = nBoxQuantity19
                            End If
                            nBoxQuantity19 = nBoxQuantity19 - nInsQuantity19
                            nTotalQty = nTotalQty + nInsQuantity19

                            If nTotalQty = nBoxQuantity Then
                                GoTo Aa
                            End If
                        End If
                        '19'

                        '20'
                        If nBoxQuantity20 > 0 Then
                            If nTotalQty + nBoxQuantity20 > nBoxQuantity Then
                                nInsQuantity20 = nBoxQuantity - nTotalQty
                            Else
                                nInsQuantity20 = nBoxQuantity20
                            End If
                            nBoxQuantity20 = nBoxQuantity20 - nInsQuantity20
                            nTotalQty = nTotalQty + nInsQuantity20

                        End If
                        '20'
                    End If

                    If nTotalQty = 0 Then
                        GoTo Ab
                    End If


Aa:
                    If nBoxNo = nTotalBoxes - 1 Then

                        If nBoxQuantity01 + nBoxQuantity02 + nBoxQuantity03 + nBoxQuantity04 + nBoxQuantity05 + nBoxQuantity06 + nBoxQuantity07 + nBoxQuantity08 + nBoxQuantity09 + nBoxQuantity10 + nBoxQuantity11 + nBoxQuantity12 + nBoxQuantity13 + nBoxQuantity14 + nBoxQuantity15 + nBoxQuantity16 + nBoxQuantity17 + nBoxQuantity18 < 3 Then


                            nInsQuantity01 = nInsQuantity01 + nBoxQuantity01
                            nInsQuantity02 = nInsQuantity02 + nBoxQuantity02
                            nInsQuantity03 = nInsQuantity03 + nBoxQuantity03
                            nInsQuantity04 = nInsQuantity04 + nBoxQuantity04
                            nInsQuantity05 = nInsQuantity05 + nBoxQuantity05
                            nInsQuantity06 = nInsQuantity06 + nBoxQuantity06
                            nInsQuantity07 = nInsQuantity07 + nBoxQuantity07
                            nInsQuantity08 = nInsQuantity08 + nBoxQuantity08
                            nInsQuantity09 = nInsQuantity09 + nBoxQuantity09
                            nInsQuantity10 = nInsQuantity10 + nBoxQuantity10
                            nInsQuantity11 = nInsQuantity11 + nBoxQuantity11
                            nInsQuantity12 = nInsQuantity12 + nBoxQuantity12
                            nInsQuantity13 = nInsQuantity13 + nBoxQuantity13
                            nInsQuantity14 = nInsQuantity14 + nBoxQuantity14
                            nInsQuantity15 = nInsQuantity15 + nBoxQuantity15
                            nInsQuantity16 = nInsQuantity16 + nBoxQuantity16
                            nInsQuantity17 = nInsQuantity17 + nBoxQuantity17
                            nInsQuantity18 = nInsQuantity18 + nBoxQuantity18



                            nTotalQty = nInsQuantity01 + nInsQuantity02 + nInsQuantity03 + nInsQuantity04 + nInsQuantity05 + nInsQuantity06 + nInsQuantity07 + nInsQuantity08 + nInsQuantity09 + nInsQuantity10 + nInsQuantity11 + nInsQuantity12 + nInsQuantity13 + nInsQuantity14 + nInsQuantity15 + nInsQuantity16 + nInsQuantity17 + nInsQuantity18

                            nBoxQuantity01 = 0
                            nBoxQuantity02 = 0
                            nBoxQuantity03 = 0
                            nBoxQuantity04 = 0
                            nBoxQuantity05 = 0
                            nBoxQuantity06 = 0
                            nBoxQuantity07 = 0
                            nBoxQuantity08 = 0
                            nBoxQuantity09 = 0
                            nBoxQuantity10 = 0
                            nBoxQuantity11 = 0
                            nBoxQuantity12 = 0
                            nBoxQuantity13 = 0
                            nBoxQuantity14 = 0
                            nBoxQuantity15 = 0
                            nBoxQuantity16 = 0
                            nBoxQuantity17 = 0
                            nBoxQuantity18 = 0

                        End If
                    End If

                    myUpperPackingListDetail.ID = System.Guid.NewGuid.ToString()
                    myUpperPackingListDetail.SODID = sSODID
                    myUpperPackingListDetail.BoxNo = nBoxNo
                    myUpperPackingListDetail.Quantity = nTotalQty
                    myUpperPackingListDetail.Quantity01 = nInsQuantity01
                    myUpperPackingListDetail.Quantity02 = nInsQuantity02
                    myUpperPackingListDetail.Quantity03 = nInsQuantity03
                    myUpperPackingListDetail.Quantity04 = nInsQuantity04
                    myUpperPackingListDetail.Quantity05 = nInsQuantity05
                    myUpperPackingListDetail.Quantity06 = nInsQuantity06
                    myUpperPackingListDetail.Quantity07 = nInsQuantity07
                    myUpperPackingListDetail.Quantity08 = nInsQuantity08
                    myUpperPackingListDetail.Quantity09 = nInsQuantity09
                    myUpperPackingListDetail.Quantity10 = nInsQuantity10
                    myUpperPackingListDetail.Quantity11 = nInsQuantity11
                    myUpperPackingListDetail.Quantity12 = nInsQuantity12
                    myUpperPackingListDetail.Quantity13 = nInsQuantity13
                    myUpperPackingListDetail.Quantity14 = nInsQuantity14
                    myUpperPackingListDetail.Quantity15 = nInsQuantity15
                    myUpperPackingListDetail.Quantity16 = nInsQuantity16
                    myUpperPackingListDetail.Quantity17 = nInsQuantity17
                    myUpperPackingListDetail.Quantity18 = nInsQuantity18

                    If myPackingList.InsertProductionPackingListDetail(myUpperPackingListDetail) = True Then
                        Me.DialogResult = DialogResult.OK

                    Else
                        MsgBox(myOrderComponent.ErrorMessage)
                        Exit Sub
                    End If

                    'If sFullQuantity = "Y" Then
                    nInsQuantity01 = "0" : nInsQuantity02 = "0" : nInsQuantity03 = "0" : nInsQuantity04 = "0" : nInsQuantity05 = "0"
                    nInsQuantity06 = "0" : nInsQuantity07 = "0" : nInsQuantity08 = "0" : nInsQuantity09 = "0" : nInsQuantity10 = "0"
                    nInsQuantity11 = "0" : nInsQuantity12 = "0" : nInsQuantity13 = "0" : nInsQuantity14 = "0" : nInsQuantity15 = "0"
                    nInsQuantity16 = "0" : nInsQuantity17 = "0" : nInsQuantity18 = "0" : nInsQuantity19 = "0" : nInsQuantity20 = "0"
                    nTotalQty = "0"
                    'End If
                    'ClearQuantityAndSizes()
Ab:

                Next
                MsgBox("Completed")
                myPackingList.UpdatePPLDetailFullAndPartQty(nPPLPKID, nFKOrderDetail, nFullQty, nPartQty)
                sFullQuantity = "Y"
            End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub plHeaderInfo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plHeaderInfo.Enter

    End Sub

    Private Sub grdOrderDetailsV1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdOrderDetailsV1.DoubleClick
        Try
            ngrdRowNo = grdOrderDetailsV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Pending Orders available for Generating Job Card", MsgBoxStyle.Information)
                Exit Sub
            Else

                nFKOrderDetail = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("PKID")
                nFKArticle = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("FKArticle")

                nFKAssortment = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("FKAssortment")

                tbPairs01.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity01")
                tbPairs02.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity02")
                tbPairs03.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity03")
                tbPairs04.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity04")
                tbPairs05.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity05")
                tbPairs06.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity06")
                tbPairs07.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity07")
                tbPairs08.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity08")
                tbPairs09.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity09")
                tbPairs10.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity10")
                tbPairs11.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity11")
                tbPairs12.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity12")
                tbPairs13.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity13")
                tbPairs14.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity14")
                tbPairs15.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity15")
                tbPairs16.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity16")
                tbPairs17.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity17")
                tbPairs18.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity18")
                tbPairs19.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity19")
                tbPairs20.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Quantity20")
                tbPairsTotal.Text = grdOrderDetailsV1.GetDataRow(ngrdRowNo).Item("TotalQty")

                If tbPairs01.Text = "0" Then : tbPairs01.Clear() : End If
                If tbPairs02.Text = "0" Then : tbPairs02.Clear() : End If
                If tbPairs03.Text = "0" Then : tbPairs03.Clear() : End If
                If tbPairs04.Text = "0" Then : tbPairs04.Clear() : End If
                If tbPairs05.Text = "0" Then : tbPairs05.Clear() : End If
                If tbPairs06.Text = "0" Then : tbPairs06.Clear() : End If
                If tbPairs07.Text = "0" Then : tbPairs07.Clear() : End If
                If tbPairs08.Text = "0" Then : tbPairs08.Clear() : End If
                If tbPairs09.Text = "0" Then : tbPairs09.Clear() : End If
                If tbPairs10.Text = "0" Then : tbPairs10.Clear() : End If
                If tbPairs11.Text = "0" Then : tbPairs11.Clear() : End If
                If tbPairs12.Text = "0" Then : tbPairs12.Clear() : End If
                If tbPairs13.Text = "0" Then : tbPairs13.Clear() : End If
                If tbPairs14.Text = "0" Then : tbPairs14.Clear() : End If
                If tbPairs15.Text = "0" Then : tbPairs15.Clear() : End If
                If tbPairs16.Text = "0" Then : tbPairs16.Clear() : End If
                If tbPairs17.Text = "0" Then : tbPairs17.Clear() : End If
                If tbPairs18.Text = "0" Then : tbPairs18.Clear() : End If
                If tbPairs19.Text = "0" Then : tbPairs19.Clear() : End If
                If tbPairs20.Text = "0" Then : tbPairs20.Clear() : End If
            End If

            grdOrderDetails.Visible = False
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub




    Private Sub tbPlGeneratedQty01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPlGeneratedQty01.TextChanged, tbPlGeneratedQty02.TextChanged, tbPlGeneratedQty03.TextChanged, tbPlGeneratedQty04.TextChanged, tbPlGeneratedQty05.TextChanged, tbPlGeneratedQty06.TextChanged, tbPlGeneratedQty07.TextChanged, tbPlGeneratedQty08.TextChanged, tbPlGeneratedQty09.TextChanged, tbPlGeneratedQty10.TextChanged, tbPlGeneratedQty11.TextChanged, tbPlGeneratedQty12.TextChanged, tbPlGeneratedQty13.TextChanged, tbPlGeneratedQty14.TextChanged, tbPlGeneratedQty15.TextChanged, tbPlGeneratedQty16.TextChanged, tbPlGeneratedQty17.TextChanged, tbPlGeneratedQty18.TextChanged, tbPlGeneratedQty19.TextChanged, tbPlGeneratedQty20.TextChanged
        Try
            tbPLBalQty01.Text = Val(tbPairs01.Text) - Val(tbPlGeneratedQty01.Text)
            tbPLBalQty02.Text = Val(tbPairs02.Text) - Val(tbPlGeneratedQty02.Text)
            tbPLBalQty03.Text = Val(tbPairs03.Text) - Val(tbPlGeneratedQty03.Text)
            tbPLBalQty04.Text = Val(tbPairs04.Text) - Val(tbPlGeneratedQty04.Text)
            tbPLBalQty05.Text = Val(tbPairs05.Text) - Val(tbPlGeneratedQty05.Text)
            tbPLBalQty06.Text = Val(tbPairs06.Text) - Val(tbPlGeneratedQty06.Text)
            tbPLBalQty07.Text = Val(tbPairs07.Text) - Val(tbPlGeneratedQty07.Text)
            tbPLBalQty08.Text = Val(tbPairs08.Text) - Val(tbPlGeneratedQty08.Text)
            tbPLBalQty09.Text = Val(tbPairs09.Text) - Val(tbPlGeneratedQty09.Text)
            tbPLBalQty10.Text = Val(tbPairs10.Text) - Val(tbPlGeneratedQty10.Text)

            tbPLBalQty11.Text = Val(tbPairs11.Text) - Val(tbPlGeneratedQty11.Text)
            tbPLBalQty12.Text = Val(tbPairs12.Text) - Val(tbPlGeneratedQty12.Text)
            tbPLBalQty13.Text = Val(tbPairs13.Text) - Val(tbPlGeneratedQty13.Text)
            tbPLBalQty14.Text = Val(tbPairs14.Text) - Val(tbPlGeneratedQty14.Text)
            tbPLBalQty15.Text = Val(tbPairs15.Text) - Val(tbPlGeneratedQty15.Text)
            tbPLBalQty16.Text = Val(tbPairs16.Text) - Val(tbPlGeneratedQty16.Text)
            tbPLBalQty17.Text = Val(tbPairs17.Text) - Val(tbPlGeneratedQty17.Text)
            tbPLBalQty18.Text = Val(tbPairs18.Text) - Val(tbPlGeneratedQty18.Text)
            tbPLBalQty19.Text = Val(tbPairs19.Text) - Val(tbPlGeneratedQty19.Text)
            tbPLBalQty20.Text = Val(tbPairs20.Text) - Val(tbPlGeneratedQty20.Text)

            tbPLBalQtyTotal.Text = Val(tbPairsTotal.Text) - Val(tbPlGeneratedQtyTotal.Text)

            If Val(tbPLBalQty01.Text) <= 0 Then : tbPLBalQty01.Clear() : End If
            If Val(tbPLBalQty02.Text) <= 0 Then : tbPLBalQty02.Clear() : End If
            If Val(tbPLBalQty03.Text) <= 0 Then : tbPLBalQty03.Clear() : End If
            If Val(tbPLBalQty04.Text) <= 0 Then : tbPLBalQty04.Clear() : End If
            If Val(tbPLBalQty05.Text) <= 0 Then : tbPLBalQty05.Clear() : End If
            If Val(tbPLBalQty06.Text) <= 0 Then : tbPLBalQty06.Clear() : End If
            If Val(tbPLBalQty07.Text) <= 0 Then : tbPLBalQty07.Clear() : End If
            If Val(tbPLBalQty08.Text) <= 0 Then : tbPLBalQty08.Clear() : End If
            If Val(tbPLBalQty09.Text) <= 0 Then : tbPLBalQty09.Clear() : End If
            If Val(tbPLBalQty10.Text) <= 0 Then : tbPLBalQty10.Clear() : End If

            If Val(tbPLBalQty11.Text) <= 0 Then : tbPLBalQty11.Clear() : End If
            If Val(tbPLBalQty12.Text) <= 0 Then : tbPLBalQty12.Clear() : End If
            If Val(tbPLBalQty13.Text) <= 0 Then : tbPLBalQty13.Clear() : End If
            If Val(tbPLBalQty14.Text) <= 0 Then : tbPLBalQty14.Clear() : End If
            If Val(tbPLBalQty15.Text) <= 0 Then : tbPLBalQty15.Clear() : End If
            If Val(tbPLBalQty16.Text) <= 0 Then : tbPLBalQty16.Clear() : End If
            If Val(tbPLBalQty17.Text) <= 0 Then : tbPLBalQty17.Clear() : End If
            If Val(tbPLBalQty18.Text) <= 0 Then : tbPLBalQty18.Clear() : End If
            If Val(tbPLBalQty19.Text) <= 0 Then : tbPLBalQty19.Clear() : End If
            If Val(tbPLBalQty20.Text) <= 0 Then : tbPLBalQty20.Clear() : End If

            If Val(tbPLBalQtyTotal.Text) <= 0 Then : tbPLBalQtyTotal.Clear() : End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub tbPLQty01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPLQty01.TextChanged, tbPLQty02.TextChanged, tbPLQty03.TextChanged, tbPLQty04.TextChanged, tbPLQty05.TextChanged, tbPLQty06.TextChanged, tbPLQty07.TextChanged, tbPLQty08.TextChanged, tbPLQty09.TextChanged, tbPLQty10.TextChanged, tbPLQty11.TextChanged, tbPLQty12.TextChanged, tbPLQty13.TextChanged, tbPLQty14.TextChanged, tbPLQty15.TextChanged, tbPLQty16.TextChanged, tbPLQty17.TextChanged, tbPLQty18.TextChanged, tbPLQty19.TextChanged, tbPLQty20.TextChanged

        tbPLQtyTotal.Text = Val(tbPLQty01.Text) + Val(tbPLQty02.Text) + Val(tbPLQty03.Text) + Val(tbPLQty04.Text) + Val(tbPLQty05.Text) + Val(tbPLQty06.Text) + Val(tbPLQty07.Text) + Val(tbPLQty08.Text) + Val(tbPLQty09.Text) + Val(tbPLQty10.Text)
        tbPLQtyTotal.Text = Val(tbPLQtyTotal.Text) + Val(tbPLQty11.Text) + Val(tbPLQty12.Text) + Val(tbPLQty13.Text) + Val(tbPLQty14.Text) + Val(tbPLQty15.Text) + Val(tbPLQty16.Text) + Val(tbPLQty17.Text) + Val(tbPLQty18.Text) + Val(tbPLQty19.Text) + Val(tbPLQty20.Text)

        tbBalQty01.Text = Val(tbPLBalQty01.Text) - Val(tbPLQty01.Text)
        tbBalQty02.Text = Val(tbPLBalQty02.Text) - Val(tbPLQty02.Text)
        tbBalQty03.Text = Val(tbPLBalQty03.Text) - Val(tbPLQty03.Text)
        tbBalQty04.Text = Val(tbPLBalQty04.Text) - Val(tbPLQty04.Text)
        tbBalQty05.Text = Val(tbPLBalQty05.Text) - Val(tbPLQty05.Text)
        tbBalQty06.Text = Val(tbPLBalQty06.Text) - Val(tbPLQty06.Text)
        tbBalQty07.Text = Val(tbPLBalQty07.Text) - Val(tbPLQty07.Text)
        tbBalQty08.Text = Val(tbPLBalQty08.Text) - Val(tbPLQty08.Text)
        tbBalQty09.Text = Val(tbPLBalQty09.Text) - Val(tbPLQty09.Text)
        tbBalQty10.Text = Val(tbPLBalQty10.Text) - Val(tbPLQty10.Text)

        tbBalQty11.Text = Val(tbPLBalQty11.Text) - Val(tbPLQty11.Text)
        tbBalQty12.Text = Val(tbPLBalQty12.Text) - Val(tbPLQty12.Text)
        tbBalQty13.Text = Val(tbPLBalQty13.Text) - Val(tbPLQty13.Text)
        tbBalQty14.Text = Val(tbPLBalQty14.Text) - Val(tbPLQty14.Text)
        tbBalQty15.Text = Val(tbPLBalQty15.Text) - Val(tbPLQty15.Text)
        tbBalQty16.Text = Val(tbPLBalQty16.Text) - Val(tbPLQty16.Text)
        tbBalQty17.Text = Val(tbPLBalQty17.Text) - Val(tbPLQty17.Text)
        tbBalQty18.Text = Val(tbPLBalQty18.Text) - Val(tbPLQty18.Text)
        tbBalQty19.Text = Val(tbPLBalQty19.Text) - Val(tbPLQty19.Text)
        tbBalQty20.Text = Val(tbPLBalQty20.Text) - Val(tbPLQty20.Text)


        If Val(tbBalQty01.Text) < 0 Then : MsgBox("Packing of Quantity01 is Higher than the Balance Qty! Hence Qty01 will erased", MsgBoxStyle.Information) : tbPLQty01.Clear() : End If
        If Val(tbBalQty02.Text) < 0 Then : MsgBox("Packing of Quantity02 is Higher than the Balance Qty! Hence Qty02 will erased", MsgBoxStyle.Information) : tbPLQty02.Clear() : End If
        If Val(tbBalQty03.Text) < 0 Then : MsgBox("Packing of Quantity03 is Higher than the Balance Qty! Hence Qty03 will erased", MsgBoxStyle.Information) : tbPLQty03.Clear() : End If
        If Val(tbBalQty04.Text) < 0 Then : MsgBox("Packing of Quantity04 is Higher than the Balance Qty! Hence Qty04 will erased", MsgBoxStyle.Information) : tbPLQty04.Clear() : End If
        If Val(tbBalQty05.Text) < 0 Then : MsgBox("Packing of Quantity05 is Higher than the Balance Qty! Hence Qty05 will erased", MsgBoxStyle.Information) : tbPLQty05.Clear() : End If
        If Val(tbBalQty06.Text) < 0 Then : MsgBox("Packing of Quantity06 is Higher than the Balance Qty! Hence Qty06 will erased", MsgBoxStyle.Information) : tbPLQty06.Clear() : End If
        If Val(tbBalQty07.Text) < 0 Then : MsgBox("Packing of Quantity07 is Higher than the Balance Qty! Hence Qty07 will erased", MsgBoxStyle.Information) : tbPLQty07.Clear() : End If
        If Val(tbBalQty08.Text) < 0 Then : MsgBox("Packing of Quantity08 is Higher than the Balance Qty! Hence Qty08 will erased", MsgBoxStyle.Information) : tbPLQty08.Clear() : End If
        If Val(tbBalQty09.Text) < 0 Then : MsgBox("Packing of Quantity09 is Higher than the Balance Qty! Hence Qty09 will erased", MsgBoxStyle.Information) : tbPLQty09.Clear() : End If
        If Val(tbBalQty10.Text) < 0 Then : MsgBox("Packing of Quantity10 is Higher than the Balance Qty! Hence Qty10 will erased", MsgBoxStyle.Information) : tbPLQty10.Clear() : End If

        If Val(tbBalQty11.Text) < 0 Then : MsgBox("Packing of Quantity11 is Higher than the Balance Qty! Hence Qty11 will erased", MsgBoxStyle.Information) : tbPLQty11.Clear() : End If
        If Val(tbBalQty12.Text) < 0 Then : MsgBox("Packing of Quantity12 is Higher than the Balance Qty! Hence Qty12 will erased", MsgBoxStyle.Information) : tbPLQty12.Clear() : End If
        If Val(tbBalQty13.Text) < 0 Then : MsgBox("Packing of Quantity13 is Higher than the Balance Qty! Hence Qty13 will erased", MsgBoxStyle.Information) : tbPLQty13.Clear() : End If
        If Val(tbBalQty14.Text) < 0 Then : MsgBox("Packing of Quantity14 is Higher than the Balance Qty! Hence Qty14 will erased", MsgBoxStyle.Information) : tbPLQty14.Clear() : End If
        If Val(tbBalQty15.Text) < 0 Then : MsgBox("Packing of Quantity15 is Higher than the Balance Qty! Hence Qty15 will erased", MsgBoxStyle.Information) : tbPLQty15.Clear() : End If
        If Val(tbBalQty16.Text) < 0 Then : MsgBox("Packing of Quantity16 is Higher than the Balance Qty! Hence Qty16 will erased", MsgBoxStyle.Information) : tbPLQty16.Clear() : End If
        If Val(tbBalQty17.Text) < 0 Then : MsgBox("Packing of Quantity17 is Higher than the Balance Qty! Hence Qty17 will erased", MsgBoxStyle.Information) : tbPLQty17.Clear() : End If
        If Val(tbBalQty18.Text) < 0 Then : MsgBox("Packing of Quantity18 is Higher than the Balance Qty! Hence Qty18 will erased", MsgBoxStyle.Information) : tbPLQty18.Clear() : End If
        If Val(tbBalQty19.Text) < 0 Then : MsgBox("Packing of Quantity19 is Higher than the Balance Qty! Hence Qty19 will erased", MsgBoxStyle.Information) : tbPLQty19.Clear() : End If
        If Val(tbBalQty20.Text) < 0 Then : MsgBox("Packing of Quantity20 is Higher than the Balance Qty! Hence Qty20 will erased", MsgBoxStyle.Information) : tbPLQty20.Clear() : End If

        If Val(tbBalQty01.Text) = 0 Then : tbBalQty01.Clear() : End If
        If Val(tbBalQty02.Text) = 0 Then : tbBalQty02.Clear() : End If
        If Val(tbBalQty03.Text) = 0 Then : tbBalQty03.Clear() : End If
        If Val(tbBalQty04.Text) = 0 Then : tbBalQty04.Clear() : End If
        If Val(tbBalQty05.Text) = 0 Then : tbBalQty05.Clear() : End If
        If Val(tbBalQty06.Text) = 0 Then : tbBalQty06.Clear() : End If
        If Val(tbBalQty07.Text) = 0 Then : tbBalQty07.Clear() : End If
        If Val(tbBalQty08.Text) = 0 Then : tbBalQty08.Clear() : End If
        If Val(tbBalQty09.Text) = 0 Then : tbBalQty09.Clear() : End If
        If Val(tbBalQty10.Text) = 0 Then : tbBalQty10.Clear() : End If

        If Val(tbBalQty11.Text) = 0 Then : tbBalQty11.Clear() : End If
        If Val(tbBalQty12.Text) = 0 Then : tbBalQty12.Clear() : End If
        If Val(tbBalQty13.Text) = 0 Then : tbBalQty13.Clear() : End If
        If Val(tbBalQty14.Text) = 0 Then : tbBalQty14.Clear() : End If
        If Val(tbBalQty15.Text) = 0 Then : tbBalQty15.Clear() : End If
        If Val(tbBalQty16.Text) = 0 Then : tbBalQty16.Clear() : End If
        If Val(tbBalQty17.Text) = 0 Then : tbBalQty17.Clear() : End If
        If Val(tbBalQty18.Text) = 0 Then : tbBalQty18.Clear() : End If
        If Val(tbBalQty19.Text) = 0 Then : tbBalQty19.Clear() : End If
        If Val(tbBalQty20.Text) = 0 Then : tbBalQty20.Clear() : End If

    End Sub

    Private Sub ClearPackingQuantity()
        Try
            tbPLQty01.Clear() : tbPLQty02.Clear() : tbPLQty03.Clear() : tbPLQty04.Clear() : tbPLQty05.Clear()
            tbPLQty06.Clear() : tbPLQty07.Clear() : tbPLQty08.Clear() : tbPLQty09.Clear() : tbPLQty10.Clear()

            tbPLQty11.Clear() : tbPLQty12.Clear() : tbPLQty13.Clear() : tbPLQty14.Clear() : tbPLQty15.Clear()
            tbPLQty16.Clear() : tbPLQty17.Clear() : tbPLQty18.Clear() : tbPLQty19.Clear() : tbPLQty20.Clear()

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub LoadTempPLDetails()
        Try
            Dim i As Integer = 0

Ab:
            ngrdRowCount = grdTempPlQtyV1.RowCount
            For i = 0 To ngrdRowCount
                grdTempPlQtyV1.DeleteRow(i)
            Next
            ngrdRowCount = grdTempPlQtyV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If

            grdTempPlQty.DataSource = myPackingList.LoadTempPL(nFKOrderDetail)

            With grdTempPlQtyV1
                .Columns(0).VisibleIndex = -1
                .Columns(1).VisibleIndex = -1
                .Columns(2).VisibleIndex = -1
                .Columns(3).VisibleIndex = -1
                .Columns(5).VisibleIndex = -1

                .Columns(27).VisibleIndex = -1
                .Columns(28).VisibleIndex = -1
                .Columns(29).VisibleIndex = -1
                .Columns(30).VisibleIndex = -1

                i = 6
                For i = 6 To 26
                    .Columns(i).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                Next

            End With




        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub UpdateQuantities()
        Try
            If Val(tbPlGeneratedQty01.Text) <= 0 Then : tbPlGeneratedQty01.Clear() : End If
            If Val(tbPlGeneratedQty02.Text) <= 0 Then : tbPlGeneratedQty02.Clear() : End If
            If Val(tbPlGeneratedQty03.Text) <= 0 Then : tbPlGeneratedQty03.Clear() : End If
            If Val(tbPlGeneratedQty04.Text) <= 0 Then : tbPlGeneratedQty04.Clear() : End If
            If Val(tbPlGeneratedQty05.Text) <= 0 Then : tbPlGeneratedQty05.Clear() : End If
            If Val(tbPlGeneratedQty06.Text) <= 0 Then : tbPlGeneratedQty06.Clear() : End If
            If Val(tbPlGeneratedQty07.Text) <= 0 Then : tbPlGeneratedQty07.Clear() : End If
            If Val(tbPlGeneratedQty08.Text) <= 0 Then : tbPlGeneratedQty08.Clear() : End If
            If Val(tbPlGeneratedQty09.Text) <= 0 Then : tbPlGeneratedQty09.Clear() : End If
            If Val(tbPlGeneratedQty10.Text) <= 0 Then : tbPlGeneratedQty10.Clear() : End If

            If Val(tbPlGeneratedQty11.Text) <= 0 Then : tbPlGeneratedQty11.Clear() : End If
            If Val(tbPlGeneratedQty12.Text) <= 0 Then : tbPlGeneratedQty12.Clear() : End If
            If Val(tbPlGeneratedQty13.Text) <= 0 Then : tbPlGeneratedQty13.Clear() : End If
            If Val(tbPlGeneratedQty14.Text) <= 0 Then : tbPlGeneratedQty14.Clear() : End If
            If Val(tbPlGeneratedQty15.Text) <= 0 Then : tbPlGeneratedQty15.Clear() : End If
            If Val(tbPlGeneratedQty16.Text) <= 0 Then : tbPlGeneratedQty16.Clear() : End If
            If Val(tbPlGeneratedQty17.Text) <= 0 Then : tbPlGeneratedQty17.Clear() : End If
            If Val(tbPlGeneratedQty18.Text) <= 0 Then : tbPlGeneratedQty18.Clear() : End If
            If Val(tbPlGeneratedQty19.Text) <= 0 Then : tbPlGeneratedQty19.Clear() : End If
            If Val(tbPlGeneratedQty20.Text) <= 0 Then : tbPlGeneratedQty20.Clear() : End If

            tbPlGeneratedQtyTotal.Text = Val(tbPlGeneratedQty01.Text) + Val(tbPlGeneratedQty02.Text) + Val(tbPlGeneratedQty03.Text) + Val(tbPlGeneratedQty04.Text) + Val(tbPlGeneratedQty05.Text) + Val(tbPlGeneratedQty06.Text) + Val(tbPlGeneratedQty07.Text) + Val(tbPlGeneratedQty08.Text) + Val(tbPlGeneratedQty09.Text) + Val(tbPlGeneratedQty10.Text)
            tbPlGeneratedQtyTotal.Text = Val(tbPlGeneratedQtyTotal.Text) + Val(tbPlGeneratedQty11.Text) + Val(tbPlGeneratedQty12.Text) + Val(tbPlGeneratedQty13.Text) + Val(tbPlGeneratedQty14.Text) + Val(tbPlGeneratedQty15.Text) + Val(tbPlGeneratedQty16.Text) + Val(tbPlGeneratedQty17.Text) + Val(tbPlGeneratedQty18.Text) + Val(tbPlGeneratedQty19.Text) + Val(tbPlGeneratedQty20.Text)

            tbPLBalQty01.Text = Val(tbPairs01.Text) - Val(tbPlGeneratedQty01.Text)
            tbPLBalQty02.Text = Val(tbPairs02.Text) - Val(tbPlGeneratedQty02.Text)
            tbPLBalQty03.Text = Val(tbPairs03.Text) - Val(tbPlGeneratedQty03.Text)
            tbPLBalQty04.Text = Val(tbPairs04.Text) - Val(tbPlGeneratedQty04.Text)
            tbPLBalQty05.Text = Val(tbPairs05.Text) - Val(tbPlGeneratedQty05.Text)
            tbPLBalQty06.Text = Val(tbPairs06.Text) - Val(tbPlGeneratedQty06.Text)
            tbPLBalQty07.Text = Val(tbPairs07.Text) - Val(tbPlGeneratedQty07.Text)
            tbPLBalQty08.Text = Val(tbPairs08.Text) - Val(tbPlGeneratedQty08.Text)
            tbPLBalQty09.Text = Val(tbPairs09.Text) - Val(tbPlGeneratedQty09.Text)
            tbPLBalQty10.Text = Val(tbPairs10.Text) - Val(tbPlGeneratedQty10.Text)

            tbPLBalQty11.Text = Val(tbPairs11.Text) - Val(tbPlGeneratedQty11.Text)
            tbPLBalQty12.Text = Val(tbPairs12.Text) - Val(tbPlGeneratedQty12.Text)
            tbPLBalQty13.Text = Val(tbPairs13.Text) - Val(tbPlGeneratedQty13.Text)
            tbPLBalQty14.Text = Val(tbPairs14.Text) - Val(tbPlGeneratedQty14.Text)
            tbPLBalQty15.Text = Val(tbPairs15.Text) - Val(tbPlGeneratedQty15.Text)
            tbPLBalQty16.Text = Val(tbPairs16.Text) - Val(tbPlGeneratedQty16.Text)
            tbPLBalQty17.Text = Val(tbPairs17.Text) - Val(tbPlGeneratedQty17.Text)
            tbPLBalQty18.Text = Val(tbPairs18.Text) - Val(tbPlGeneratedQty18.Text)
            tbPLBalQty19.Text = Val(tbPairs19.Text) - Val(tbPlGeneratedQty19.Text)
            tbPLBalQty20.Text = Val(tbPairs20.Text) - Val(tbPlGeneratedQty20.Text)

            tbPLBalQtyTotal.Text = Val(tbPairsTotal.Text) - Val(tbPlGeneratedQtyTotal.Text)

            If Val(tbPLBalQty01.Text) <= 0 Then : tbPLBalQty01.Clear() : End If
            If Val(tbPLBalQty02.Text) <= 0 Then : tbPLBalQty02.Clear() : End If
            If Val(tbPLBalQty03.Text) <= 0 Then : tbPLBalQty03.Clear() : End If
            If Val(tbPLBalQty04.Text) <= 0 Then : tbPLBalQty04.Clear() : End If
            If Val(tbPLBalQty05.Text) <= 0 Then : tbPLBalQty05.Clear() : End If
            If Val(tbPLBalQty06.Text) <= 0 Then : tbPLBalQty06.Clear() : End If
            If Val(tbPLBalQty07.Text) <= 0 Then : tbPLBalQty07.Clear() : End If
            If Val(tbPLBalQty08.Text) <= 0 Then : tbPLBalQty08.Clear() : End If
            If Val(tbPLBalQty09.Text) <= 0 Then : tbPLBalQty09.Clear() : End If
            If Val(tbPLBalQty10.Text) <= 0 Then : tbPLBalQty10.Clear() : End If

            If Val(tbPLBalQty11.Text) <= 0 Then : tbPLBalQty11.Clear() : End If
            If Val(tbPLBalQty12.Text) <= 0 Then : tbPLBalQty12.Clear() : End If
            If Val(tbPLBalQty13.Text) <= 0 Then : tbPLBalQty13.Clear() : End If
            If Val(tbPLBalQty14.Text) <= 0 Then : tbPLBalQty14.Clear() : End If
            If Val(tbPLBalQty15.Text) <= 0 Then : tbPLBalQty15.Clear() : End If
            If Val(tbPLBalQty16.Text) <= 0 Then : tbPLBalQty16.Clear() : End If
            If Val(tbPLBalQty17.Text) <= 0 Then : tbPLBalQty17.Clear() : End If
            If Val(tbPLBalQty18.Text) <= 0 Then : tbPLBalQty18.Clear() : End If
            If Val(tbPLBalQty19.Text) <= 0 Then : tbPLBalQty19.Clear() : End If
            If Val(tbPLBalQty20.Text) <= 0 Then : tbPLBalQty20.Clear() : End If

            If Val(tbPLBalQtyTotal.Text) <= 0 Then : tbPLBalQtyTotal.Clear() : End If

            tbPLQtyTotal.Text = Val(tbPLQty01.Text) + Val(tbPLQty02.Text) + Val(tbPLQty03.Text) + Val(tbPLQty04.Text) + Val(tbPLQty05.Text) + Val(tbPLQty06.Text) + Val(tbPLQty07.Text) + Val(tbPLQty08.Text) + Val(tbPLQty09.Text) + Val(tbPLQty10.Text)
            tbPLQtyTotal.Text = Val(tbPLQtyTotal.Text) + Val(tbPLQty11.Text) + Val(tbPLQty12.Text) + Val(tbPLQty13.Text) + Val(tbPLQty14.Text) + Val(tbPLQty15.Text) + Val(tbPLQty16.Text) + Val(tbPLQty17.Text) + Val(tbPLQty18.Text) + Val(tbPLQty19.Text) + Val(tbPLQty20.Text)

            tbBalQty01.Text = Val(tbPLBalQty01.Text) - Val(tbPLQty01.Text)
            tbBalQty02.Text = Val(tbPLBalQty02.Text) - Val(tbPLQty02.Text)
            tbBalQty03.Text = Val(tbPLBalQty03.Text) - Val(tbPLQty03.Text)
            tbBalQty04.Text = Val(tbPLBalQty04.Text) - Val(tbPLQty04.Text)
            tbBalQty05.Text = Val(tbPLBalQty05.Text) - Val(tbPLQty05.Text)
            tbBalQty06.Text = Val(tbPLBalQty06.Text) - Val(tbPLQty06.Text)
            tbBalQty07.Text = Val(tbPLBalQty07.Text) - Val(tbPLQty07.Text)
            tbBalQty08.Text = Val(tbPLBalQty08.Text) - Val(tbPLQty08.Text)
            tbBalQty09.Text = Val(tbPLBalQty09.Text) - Val(tbPLQty09.Text)
            tbBalQty10.Text = Val(tbPLBalQty10.Text) - Val(tbPLQty10.Text)

            tbBalQty11.Text = Val(tbPLBalQty11.Text) - Val(tbPLQty11.Text)
            tbBalQty12.Text = Val(tbPLBalQty12.Text) - Val(tbPLQty12.Text)
            tbBalQty13.Text = Val(tbPLBalQty13.Text) - Val(tbPLQty13.Text)
            tbBalQty14.Text = Val(tbPLBalQty14.Text) - Val(tbPLQty14.Text)
            tbBalQty15.Text = Val(tbPLBalQty15.Text) - Val(tbPLQty15.Text)
            tbBalQty16.Text = Val(tbPLBalQty16.Text) - Val(tbPLQty16.Text)
            tbBalQty17.Text = Val(tbPLBalQty17.Text) - Val(tbPLQty17.Text)
            tbBalQty18.Text = Val(tbPLBalQty18.Text) - Val(tbPLQty18.Text)
            tbBalQty19.Text = Val(tbPLBalQty19.Text) - Val(tbPLQty19.Text)
            tbBalQty20.Text = Val(tbPLBalQty20.Text) - Val(tbPLQty20.Text)


            If Val(tbBalQty01.Text) < 0 Then : MsgBox("Packing of Quantity01 is Higher than the Balance Qty! Hence Qty01 will erased", MsgBoxStyle.Information) : tbPLQty01.Clear() : End If
            If Val(tbBalQty02.Text) < 0 Then : MsgBox("Packing of Quantity02 is Higher than the Balance Qty! Hence Qty02 will erased", MsgBoxStyle.Information) : tbPLQty02.Clear() : End If
            If Val(tbBalQty03.Text) < 0 Then : MsgBox("Packing of Quantity03 is Higher than the Balance Qty! Hence Qty03 will erased", MsgBoxStyle.Information) : tbPLQty03.Clear() : End If
            If Val(tbBalQty04.Text) < 0 Then : MsgBox("Packing of Quantity04 is Higher than the Balance Qty! Hence Qty04 will erased", MsgBoxStyle.Information) : tbPLQty04.Clear() : End If
            If Val(tbBalQty05.Text) < 0 Then : MsgBox("Packing of Quantity05 is Higher than the Balance Qty! Hence Qty05 will erased", MsgBoxStyle.Information) : tbPLQty05.Clear() : End If
            If Val(tbBalQty06.Text) < 0 Then : MsgBox("Packing of Quantity06 is Higher than the Balance Qty! Hence Qty06 will erased", MsgBoxStyle.Information) : tbPLQty06.Clear() : End If
            If Val(tbBalQty07.Text) < 0 Then : MsgBox("Packing of Quantity07 is Higher than the Balance Qty! Hence Qty07 will erased", MsgBoxStyle.Information) : tbPLQty07.Clear() : End If
            If Val(tbBalQty08.Text) < 0 Then : MsgBox("Packing of Quantity08 is Higher than the Balance Qty! Hence Qty08 will erased", MsgBoxStyle.Information) : tbPLQty08.Clear() : End If
            If Val(tbBalQty09.Text) < 0 Then : MsgBox("Packing of Quantity09 is Higher than the Balance Qty! Hence Qty09 will erased", MsgBoxStyle.Information) : tbPLQty09.Clear() : End If
            If Val(tbBalQty10.Text) < 0 Then : MsgBox("Packing of Quantity10 is Higher than the Balance Qty! Hence Qty10 will erased", MsgBoxStyle.Information) : tbPLQty10.Clear() : End If

            If Val(tbBalQty11.Text) < 0 Then : MsgBox("Packing of Quantity11 is Higher than the Balance Qty! Hence Qty11 will erased", MsgBoxStyle.Information) : tbPLQty11.Clear() : End If
            If Val(tbBalQty12.Text) < 0 Then : MsgBox("Packing of Quantity12 is Higher than the Balance Qty! Hence Qty12 will erased", MsgBoxStyle.Information) : tbPLQty12.Clear() : End If
            If Val(tbBalQty13.Text) < 0 Then : MsgBox("Packing of Quantity13 is Higher than the Balance Qty! Hence Qty13 will erased", MsgBoxStyle.Information) : tbPLQty13.Clear() : End If
            If Val(tbBalQty14.Text) < 0 Then : MsgBox("Packing of Quantity14 is Higher than the Balance Qty! Hence Qty14 will erased", MsgBoxStyle.Information) : tbPLQty14.Clear() : End If
            If Val(tbBalQty15.Text) < 0 Then : MsgBox("Packing of Quantity15 is Higher than the Balance Qty! Hence Qty15 will erased", MsgBoxStyle.Information) : tbPLQty15.Clear() : End If
            If Val(tbBalQty16.Text) < 0 Then : MsgBox("Packing of Quantity16 is Higher than the Balance Qty! Hence Qty16 will erased", MsgBoxStyle.Information) : tbPLQty16.Clear() : End If
            If Val(tbBalQty17.Text) < 0 Then : MsgBox("Packing of Quantity17 is Higher than the Balance Qty! Hence Qty17 will erased", MsgBoxStyle.Information) : tbPLQty17.Clear() : End If
            If Val(tbBalQty18.Text) < 0 Then : MsgBox("Packing of Quantity18 is Higher than the Balance Qty! Hence Qty18 will erased", MsgBoxStyle.Information) : tbPLQty18.Clear() : End If
            If Val(tbBalQty19.Text) < 0 Then : MsgBox("Packing of Quantity19 is Higher than the Balance Qty! Hence Qty19 will erased", MsgBoxStyle.Information) : tbPLQty19.Clear() : End If
            If Val(tbBalQty20.Text) < 0 Then : MsgBox("Packing of Quantity20 is Higher than the Balance Qty! Hence Qty20 will erased", MsgBoxStyle.Information) : tbPLQty20.Clear() : End If

            If Val(tbBalQty01.Text) = 0 Then : tbBalQty01.Clear() : End If
            If Val(tbBalQty02.Text) = 0 Then : tbBalQty02.Clear() : End If
            If Val(tbBalQty03.Text) = 0 Then : tbBalQty03.Clear() : End If
            If Val(tbBalQty04.Text) = 0 Then : tbBalQty04.Clear() : End If
            If Val(tbBalQty05.Text) = 0 Then : tbBalQty05.Clear() : End If
            If Val(tbBalQty06.Text) = 0 Then : tbBalQty06.Clear() : End If
            If Val(tbBalQty07.Text) = 0 Then : tbBalQty07.Clear() : End If
            If Val(tbBalQty08.Text) = 0 Then : tbBalQty08.Clear() : End If
            If Val(tbBalQty09.Text) = 0 Then : tbBalQty09.Clear() : End If
            If Val(tbBalQty10.Text) = 0 Then : tbBalQty10.Clear() : End If

            If Val(tbBalQty11.Text) = 0 Then : tbBalQty11.Clear() : End If
            If Val(tbBalQty12.Text) = 0 Then : tbBalQty12.Clear() : End If
            If Val(tbBalQty13.Text) = 0 Then : tbBalQty13.Clear() : End If
            If Val(tbBalQty14.Text) = 0 Then : tbBalQty14.Clear() : End If
            If Val(tbBalQty15.Text) = 0 Then : tbBalQty15.Clear() : End If
            If Val(tbBalQty16.Text) = 0 Then : tbBalQty16.Clear() : End If
            If Val(tbBalQty17.Text) = 0 Then : tbBalQty17.Clear() : End If
            If Val(tbBalQty18.Text) = 0 Then : tbBalQty18.Clear() : End If
            If Val(tbBalQty19.Text) = 0 Then : tbBalQty19.Clear() : End If
            If Val(tbBalQty20.Text) = 0 Then : tbBalQty20.Clear() : End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    'Private Sub InsertManualProductionPackingListDetail()
    '    Try
    '        ngrdRowCount = grdTempPlQtyV1.RowCount

    '        Dim i As Integer = 0

    '        For i = 0 To ngrdRowCount - 1

    '            myOptimizerstrPPCProductionPackingListDetail.FKProdPLHeader = nPPLPKID ' - 1
    '            myOptimizerstrPPCProductionPackingListDetail.FKYear = grdTempPlQtyV1.GetDataRow(i).Item("FKYear") 'mdlOptimizer.nFKCurrentYear
    '            myOptimizerstrPPCProductionPackingListDetail.FKSeason = grdTempPlQtyV1.GetDataRow(i).Item("FKSeason") 'nFKSeason
    '            myOptimizerstrPPCProductionPackingListDetail.FKOrderDetail = grdTempPlQtyV1.GetDataRow(i).Item("FKOrderDetail") 'nFKOrderDetail
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity01 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity01") 'Val(tbPLQty01.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity02 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity02") 'Val(tbPLQty02.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity03 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity03") ' Val(tbPLQty03.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity04 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity04") 'Val(tbPLQty04.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity05 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity05") 'Val(tbPLQty05.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity06 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity06") 'Val(tbPLQty06.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity07 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity07") 'Val(tbPLQty07.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity08 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity08") 'Val(tbPLQty08.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity09 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity09") 'Val(tbPLQty09.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity10 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity10") 'Val(tbPLQty10.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity11 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity11") 'Val(tbPLQty11.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity12 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity12") 'Val(tbPLQty12.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity13 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity13") 'Val(tbPLQty13.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity14 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity14") 'Val(tbPLQty14.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity15 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity15") 'Val(tbPLQty15.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity16 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity16") 'Val(tbPLQty16.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity17 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity17") 'Val(tbPLQty17.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity18 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity18") 'Val(tbPLQty18.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity19 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity19") 'Val(tbPLQty19.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.Quantity20 = grdTempPlQtyV1.GetDataRow(i).Item("Quantity20") 'Val(tbPLQty20.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.TotalQty = grdTempPlQtyV1.GetDataRow(i).Item("TotalQty") 'Val(tbPLBalQty01.Text)
    '            myOptimizerstrPPCProductionPackingListDetail.FullQty = grdTempPlQtyV1.GetDataRow(i).Item("FullQty") '0
    '            myOptimizerstrPPCProductionPackingListDetail.PartQty = grdTempPlQtyV1.GetDataRow(i).Item("PartQty") 'nPartQty + 1

    '            If myPackingList.InsertProductionPackingListDetail(myOptimizerstrPPCProductionPackingListDetail) = True Then
    '                Me.DialogResult = DialogResult.OK

    '            Else
    '                MsgBox(myOrderComponent.ErrorMessage)
    '                Exit Sub
    '            End If

    '        Next
    '        MsgBox("Successfully Created")
    '    Catch Exp As Exception
    '        HandleException(Me.Name, Exp)
    '    End Try
    'End Sub
    Private Sub InsertManualProductionPackingListDetail()
        Try
            ngrdRowCount = grdOrderDetailsV1.RowCount  'grdTempPlQtyV1.RowCount

            Dim i As Integer = 0

            For i = 0 To ngrdRowCount - 1

                'myOptimizerstrPPCProductionPackingListDetail.FKProdPLHeader = nPPLPKID ' - 1
                'myOptimizerstrPPCProductionPackingListDetail.FKYear = mdlOptimizer.nFKCurrentYear 'grdTempPlQtyV1.GetDataRow(i).Item("FKYear") 
                'myOptimizerstrPPCProductionPackingListDetail.FKSeason = nFKSeason 'grdTempPlQtyV1.GetDataRow(i).Item("FKSeason") 
                'myOptimizerstrPPCProductionPackingListDetail.FKOrderDetail = grdOrderDetailsV1.GetDataRow(i).Item("PKID") 'nFKOrderDetail 'grdTempPlQtyV1.GetDataRow(i).Item("FKOrderDetail") 
                nFKOrderDetail = grdOrderDetailsV1.GetDataRow(i).Item("PKID")
                nOrdQuantity01 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity01")
                nOrdQuantity02 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity02")
                nOrdQuantity03 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity03")
                nOrdQuantity04 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity04")
                nOrdQuantity05 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity05")
                nOrdQuantity06 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity06")
                nOrdQuantity07 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity07")
                nOrdQuantity08 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity08")
                nOrdQuantity09 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity09")
                nOrdQuantity10 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity10")
                nOrdQuantity11 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity11")
                nOrdQuantity12 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity12")
                nOrdQuantity13 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity13")
                nOrdQuantity14 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity14")
                nOrdQuantity15 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity15")
                nOrdQuantity16 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity16")
                nOrdQuantity17 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity17")
                nOrdQuantity18 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity18")
                nOrdQuantity19 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity19")
                nOrdQuantity20 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity20")
                nOrdTotalQuantity = grdOrderDetailsV1.GetDataRow(i).Item("TotalQty")

                nBoxQuantity01 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity01")
                nBoxQuantity02 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity02")
                nBoxQuantity03 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity03")
                nBoxQuantity04 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity04")
                nBoxQuantity05 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity05")
                nBoxQuantity06 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity06")
                nBoxQuantity07 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity07")
                nBoxQuantity08 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity08")
                nBoxQuantity09 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity09")
                nBoxQuantity10 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity10")
                nBoxQuantity11 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity11")
                nBoxQuantity12 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity12")
                nBoxQuantity13 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity13")
                nBoxQuantity14 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity14")
                nBoxQuantity15 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity15")
                nBoxQuantity16 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity16")
                nBoxQuantity17 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity17")
                nBoxQuantity18 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity18")
                nBoxQuantity19 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity19")
                nBoxQuantity20 = grdOrderDetailsV1.GetDataRow(i).Item("Quantity20")
                nBoxTotalQuantity = grdOrderDetailsV1.GetDataRow(i).Item("TotalQty")
                'myOptimizerstrPPCProductionPackingListDetail.FullQty = 0 'grdTempPlQtyV1.GetDataRow(i).Item("FullQty") '
                'myOptimizerstrPPCProductionPackingListDetail.PartQty = nPartQty + 1 'grdTempPlQtyV1.GetDataRow(i).Item("PartQty") '
                nPLQty = grdOrderDetailsV1.GetDataRow(i).Item("PLQty")

                If sTypeofPacking = "N" Then
                    nTotalBoxes = Math.Ceiling(nOrdTotalQuantity / nPLQty)
                    nBoxQuantity = grdOrderDetailsV1.GetDataRow(i).Item("PLQty")
                ElseIf sTypeofPacking = "A" Then
                    nFKAssortment = grdOrderDetailsV1.GetDataRow(i).Item("FKAssortment")
                    nTotalBoxes = grdOrderDetailsV1.GetDataRow(i).Item("PackCount")
                End If

                sFullQuantity = "Y"
                nFullQty = 0
                nPartQty = 0
                InsertStickers()


            Next
            MsgBox("Successfully Created")
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim sFileName, sOuterFileName, sSAPAvailable As String
    Dim curFile As String
    Dim excelapp As New Application
    Dim sCustomerMaterialCode, sCustomerMaterialDescription, sMaterialCode, sMaterialDescription As String
    Dim sContent, sStatus As String
    Private Sub UploadDatafromExcelFile()
        'Try
        Dim objOCExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objOCWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objOCWorksheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim OCData As String

        ''Outer Carton''
        sFileName = "241023"

        objOCExcel = CreateObject("Excel.Application")

        excelapp = GetObject(, "Excel.Application")
        If Err.Number <> 0 Then
            Err.Clear()
            'If no existing Excel app running, launch one
            excelapp = CreateObject("Excel.Application")
            If Err.Number <> 0 Then
                MsgBox("Cannot start Excel.")
                Exit Sub
            End If
        End If

        excelapp.Visible = True

        Dim WkBook As Workbook

        WkBook = excelapp.Workbooks.Open("\\ahserver\To day only\ECCO Upper BOM\" + sFileName + ".xls")

        objOCWorksheet = WkBook.Worksheets(1)

        Dim i As Integer = 2

        Do

            If Len(objOCWorksheet.Cells(i, 3).Value) = 0 Then
                i = i + 1
                sContent = ""
                GoTo aa
            End If

            sCustomerMaterialCode = objOCWorksheet.Cells(i, 3).Value.ToString()
            sCustomerMaterialDescription = objOCWorksheet.Cells(i, 7).Value.ToString()
            sContent = sCustomerMaterialCode
            i = i + 1

            If sCustomerMaterialCode = "Comp" Then
                i = i + 1
                GoTo Aa
            End If
            If sCustomerMaterialCode <> "" Then
                Dim daSelMaterials As New SqlDataAdapter("Select * from Materials where Description like '%'+ '" & sCustomerMaterialCode & _
                                                         "' + '%'", sCnn)
                Dim dsSelMaterials As New DataSet
                daSelMaterials.Fill(dsSelMaterials)

                If dsSelMaterials.Tables(0).Rows.Count = 0 Then
                    sMaterialCode = "Not Available"
                    sMaterialDescription = ""
                    sStatus = "N.A"
                    InsertItem()
                Else
                    Dim j As Integer = 0

                    For j = 0 To dsSelMaterials.Tables(0).Rows.Count - 1
                        sMaterialCode = dsSelMaterials.Tables(0).Rows(j).Item("MaterialCode")
                        sMaterialDescription = dsSelMaterials.Tables(0).Rows(j).Item("Description")

                        If dsSelMaterials.Tables(0).Rows.Count = 1 Then
                            sStatus = "Available"
                        Else
                            sStatus = "Multiple Matches"
                        End If
                        InsertItem()
                    Next
                    'If dsSelMaterials.Tables(0).Rows.Count = 1 Then
                    '                        ````    sMaterialCode = dsSelMaterials.Tables(0).Rows(0).Item("MaterialCode")
                    '    sStatus = "Available"
                    'Else
                    '    sMaterialCode = "Multiple Code Exists"
                    '    sStatus = "Multiple Matches"
                    'End If
                End If
                'Dim daInsBomCompare As New SqlDataAdapter("Insert Into UpperBOMComparision Values('" & sFileName & "','" & sCustomerMaterialCode & _
                '                                          "','" & sCustomerMaterialDescription & "','" & sMaterialCode & _
                '                                          "','" & sMaterialDescription & "'" & sStatus & "')", sConstr)
                'Dim dsInsBomCompare As New DataSet
                'daInsBomCompare.Fill(dsInsBomCompare)
                'dsInsBomCompare.AcceptChanges()
            Else
                GoTo Aa
            End If

            'Dim daInsOuterCartonSAP As New SqlDataAdapter("Insert Into OuterCartonToImport Values ('" & sFKJobcardId & _
            '                                               "','" & nOUBBoxSlNo & "','" & sOUBCartonNumber & _
            '                                               "','" & sOUBSalesOrder & "','" & sOUBArticle & _
            '                                               "','" & nOUBQuantity & "','" & sOUBSizes & _
            '                                               "','','0','0','0','0','0','','" & Format(Date.Now, "dd-MMM-yyyy:hh:mm:ss") & _
            '                                               "','','','','')", sCnn)
            'Dim dsInsOuterCartonSAP As New DataSet
            'daInsOuterCartonSAP.Fill(dsInsOuterCartonSAP)
            'dsInsOuterCartonSAP.AcceptChanges()
Aa:

        Loop Until (sContent = "E.O.F" Or i >= 200)
        MsgBox("Completed")
        ''Outer Carton''

        ''Inner Box''
        'Catch ex As Exception

        'End Try
    End Sub

    Dim nSumfor13, nSumfor20 As Integer
    Dim sStringfor13, sStringfor19, sStringfor20 As String
    Dim nRoundofffor13, nMultiplyfor13, nCheckDigitFor13, nRoundofffor20, nMultiplyfor20, nCheckDigitFor20 As Integer

    Private Sub InsertItem()
        Dim daInsBomCompare As New SqlDataAdapter("Insert Into UpperBOMComparision Values('" & sFileName & "','" & sCustomerMaterialCode & _
                                                         "','" & sCustomerMaterialDescription & "','" & sMaterialCode & _
                                                         "','" & sMaterialDescription & "','" & sStatus & "')", sConstr)
        Dim dsInsBomCompare As New DataSet
        daInsBomCompare.Fill(dsInsBomCompare)
        dsInsBomCompare.AcceptChanges()
    End Sub


    Dim sPONr, sSLNr, sBoxNr, sBarcode19, sBarcode20, sBarcode13, sSize, sFullQty, sIsDuplicate As String
    Dim nQty, nBalQty, n5prJCQty, n5PairQty, nInnerPackCount As Integer
    Dim s5PRPQAid As String
    Dim n5PRPQAQty, n5PRPQCFQty As Integer

    Private Sub InsertOuterCartonPackingBarcode()
        Try
Ab:
            Dim daDelOCPB As New SqlDataAdapter("Delete from PackingBarcode Where SalesOrderDetlsID = '" & sSODID & "'", sConstr)
            Dim dsDelOCPB As New DataSet
            daDelOCPB.Fill(dsDelOCPB)
            dsDelOCPB.AcceptChanges()

            Dim daUpd5PJCPONR As New SqlDataAdapter("Update FivePairJobCard Set PONr = '' Where JobCardNo like  '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsUpd5PJCPONR As New DataSet
            daUpd5PJCPONR.Fill(dsUpd5PJCPONR)
            dsUpd5PJCPONR.AcceptChanges()

            Dim daSelJCDtls As New SqlDataAdapter("Select * from JobcardDetail Where SalesOrderDetailID = '" & sSODID & "' And ComponentGroup = 'UPPER'", sConstr)
            Dim dsSelJCDtls As New DataSet
            daSelJCDtls.Fill(dsSelJCDtls)

            n5PairQty = Val(dsSelJCDtls.Tables(0).Rows(0).Item("PairQuantity").ToString)

            Dim daSelOCPB As New SqlDataAdapter("Select SODID,CustomerOrderNo,BoxNo,CONVERT(float, d .Size) AS Size,  d .Qty, d .Size AS SizeinString from (Select upd.sodid,jd.CustomerOrderNo,upd.BoxNo,jd.Size01,upd.Quantity01, jd.Size02,upd.Quantity02, jd.Size03,upd.Quantity03, jd.Size04,upd.Quantity04, jd.Size05,upd.Quantity05, jd.Size06,upd.Quantity06, jd.Size07,upd.Quantity07, jd.Size08,upd.Quantity08, jd.Size09,upd.Quantity09, jd.Size10,upd.Quantity10, jd.Size11,upd.Quantity11, jd.Size12,upd.Quantity12, jd.Size13,upd.Quantity13, jd.Size14,upd.Quantity14, jd.Size15,upd.Quantity15, jd.Size16,upd.Quantity16, jd.Size17,upd.Quantity17, jd.Size18,upd.Quantity18 From SalesOrderDetails As JD , UpperPackingDetail As UPD Where JD.id = UPD.SODID) AS Tbl CROSS Apply (SELECT     Size01, Quantity01 UNION ALL SELECT     Size02, Quantity02 UNION ALL SELECT     Size03, Quantity03 UNION ALL SELECT     Size04, Quantity04 UNION ALL SELECT     Size05, Quantity05 UNION ALL SELECT     Size06, Quantity06 UNION ALL SELECT     Size07, Quantity07 UNION ALL SELECT     Size08, Quantity08 UNION ALL SELECT     Size09, Quantity09 UNION ALL SELECT     Size10, Quantity10 UNION ALL SELECT     Size11, Quantity11 UNION ALL SELECT     Size12, Quantity12 UNION ALL SELECT     Size13, Quantity13 UNION ALL SELECT     Size14, Quantity14 UNION ALL SELECT     Size15, Quantity15 UNION ALL SELECT     Size16, Quantity16 UNION ALL SELECT    Size17, Quantity17 UNION ALL SELECT     Size18, Quantity18) d (Size, Qty) WHERE     d .Qty > 0 And sodid = '" & sSODID & "' Order by Tbl.BoxNo, d.Size", sConstr)
            Dim dsSelOCPB As New DataSet
            daSelOCPB.Fill(dsSelOCPB)

            nRowCount = dsSelOCPB.Tables(0).Rows.Count

            Dim i As Integer = 0
            nInnerPackCount = 0

            For i = 0 To nRowCount - 1
                sPONr = dsSelOCPB.Tables(0).Rows(i).Item("CustomerOrderNo").ToString
                sSLNr = dsSelOCPB.Tables(0).Rows(i).Item("BoxNo").ToString.PadLeft(3, CChar(CStr(0)))
                sBoxNr = dsSelOCPB.Tables(0).Rows(i).Item("BoxNo").ToString.PadLeft(2, CChar(CStr(0)))
                'If sBoxNr = "15" Then
                '    MsgBox("Box")
                'End If
                sSize = dsSelOCPB.Tables(0).Rows(i).Item("Size").ToString
                If sSize = "45" Then
                    MsgBox("Box")
                End If
                nQty = Val(dsSelOCPB.Tables(0).Rows(i).Item("Qty").ToString)
                nBalQty = Val(dsSelOCPB.Tables(0).Rows(i).Item("Qty").ToString)
                sFullQty = "Y"
                sIsDuplicate = "N"

                nSumfor13 = 0
                sStringfor13 = Trim(sPONr) + Trim(sBoxNr)

                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 1, 1)) * 1)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 2, 1)) * 3)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 3, 1)) * 1)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 4, 1)) * 3)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 5, 1)) * 1)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 6, 1)) * 3)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 7, 1)) * 1)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 8, 1)) * 3)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 9, 1)) * 1)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 10, 1)) * 3)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 11, 1)) * 1)
                nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 12, 1)) * 3)


                nRoundofffor13 = Math.Ceiling((nSumfor13 / 10))

                nMultiplyfor13 = nRoundofffor13 * 10

                nCheckDigitFor13 = nMultiplyfor13 - nSumfor13

                sBarcode13 = Trim(sPONr) + Trim(sBoxNr) + nCheckDigitFor13.ToString


                sStringfor19 = Trim(sPONr) + Trim(sBoxNr) + "00" + Trim(sSize) + "0" + Trim(sBoxNr)


                sBarcode19 = sStringfor19


                nSumfor20 = 0
                sStringfor20 = sStringfor19

                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 1, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 2, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 3, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 4, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 5, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 6, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 7, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 8, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 9, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 10, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 11, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 12, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 13, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 14, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 15, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 16, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 17, 1)) * 3)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 18, 1)) * 1)
                nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 19, 1)) * 3)

                nRoundofffor20 = Math.Ceiling((nSumfor20 / 10))

                nMultiplyfor20 = nRoundofffor20 * 10

                nCheckDigitFor20 = nMultiplyfor20 - nSumfor20

                sBarcode20 = sStringfor19 + nCheckDigitFor20.ToString

                sID = System.Guid.NewGuid.ToString()
                Dim daInsPB As New SqlDataAdapter("Insert Into PackingBarcode(ID,SalesOrderDetlsID,Size,Quantity,PONr,SLNr,BoxNr,BarCode19,BarCode20,BarCode13) Values ('" & sID & _
                                                  "','" & sSODID & "','" & sSize & "','" & nQty & "','" & sPONr & "','" & sSLNr & "','" & sBoxNr & _
                                                  "','" & sBarcode19 & "','" & sBarcode20 & "','" & sBarcode13 & "')", sConstr)
                Dim dsInsPB As New DataSet
                daInsPB.Fill(dsInsPB)
                dsInsPB.AcceptChanges()
Aa:
                Dim dsSel5PairJC As New DataSet


                If sFullQty = "Y" Then
                    Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                       "' And ISNULL(Ponr,'') = ''  And Quantity = '" & n5PairQty & "' Order by FivePairJobCardNo", sConstr)
                    daSel5PairJC.Fill(dsSel5PairJC)
                Else
                    Dim daSel5PairJC1 As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                                            "' And ISNULL(Ponr,'') = ''  And Quantity = '" & nBalQty & "' Order by FivePairJobCardNo", sConstr)
                    Dim dsSel5PairJC1 As New DataSet
                    daSel5PairJC1.Fill(dsSel5PairJC1)

                    If dsSel5PairJC1.Tables(0).Rows.Count = 1 Then
                        Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                       "' And ISNULL(Ponr,'') = ''  And Quantity = '" & nBalQty & "' Order by FivePairJobCardNo", sConstr)
                        daSel5PairJC.Fill(dsSel5PairJC)
                    Else
                        Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                       "' And ISNULL(Ponr,'') = ''  And Quantity <> '" & n5PairQty & "' Order by FivePairJobCardNo", sConstr)
                        daSel5PairJC.Fill(dsSel5PairJC)
                    End If



                End If


                Dim j As Integer = 0

                For j = 0 To dsSel5PairJC.Tables(0).Rows.Count - 1
                    s5PairID = dsSel5PairJC.Tables(0).Rows(j).Item("ID").ToString
                    n5prJCQty = Val(dsSel5PairJC.Tables(0).Rows(j).Item("Quantity").ToString)

                    If nBalQty >= n5prJCQty Then

                        nInnerPackCount = nInnerPackCount + 1
                        'sSLNr = dsSel5PairJC.Tables(0).Rows(j).Item("SerialNo").ToString
                        sSLNr = nInnerPackCount.ToString.PadLeft(3, CChar(CStr(0)))

                        nSumfor13 = 0
                        sStringfor13 = Trim(sPONr) + Trim(sBoxNr)

                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 1, 1)) * 1)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 2, 1)) * 3)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 3, 1)) * 1)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 4, 1)) * 3)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 5, 1)) * 1)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 6, 1)) * 3)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 7, 1)) * 1)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 8, 1)) * 3)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 9, 1)) * 1)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 10, 1)) * 3)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 11, 1)) * 1)
                        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 12, 1)) * 3)


                        nRoundofffor13 = Math.Ceiling((nSumfor13 / 10))

                        nMultiplyfor13 = nRoundofffor13 * 10

                        nCheckDigitFor13 = nMultiplyfor13 - nSumfor13


                        sBarcode13 = Trim(sPONr) + Trim(sBoxNr) + nCheckDigitFor13.ToString


                        'sStringfor19 = Trim(sPONr) + Trim(sBoxNr) + "00" + Trim(sSize) + Trim(sSLNr)
                        sStringfor19 = Trim(sPONr) + "0000" + Trim(sSize) + Trim(sSLNr)


                        sBarcode19 = sStringfor19


                        nSumfor20 = 0
                        sStringfor20 = sStringfor19

                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 1, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 2, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 3, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 4, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 5, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 6, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 7, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 8, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 9, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 10, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 11, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 12, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 13, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 14, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 15, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 16, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 17, 1)) * 3)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 18, 1)) * 1)
                        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 19, 1)) * 3)

                        nRoundofffor20 = Math.Ceiling((nSumfor20 / 10))

                        nMultiplyfor20 = nRoundofffor20 * 10

                        nCheckDigitFor20 = nMultiplyfor20 - nSumfor20

                        sBarcode20 = sStringfor19 + nCheckDigitFor20.ToString


                        If nBalQty >= n5prJCQty Then
                            sInvoiceNo = n5prJCQty.ToString
                        Else
                            sInvoiceNo = nBalQty.ToString
                        End If
                        Dim daUpd5PairJC As New SqlDataAdapter("Update FivePairJobCard Set PONr	= '" & sPONr & "',  SLNr = '" & sSLNr & _
                                                               "', BoxNr = '" & sBoxNr & "', BarCode19 = '" & sBarcode19 & _
                                                               "', BarCode20 = '" & sBarcode20 & "', BarCode13 = '" & sBarcode13 & _
                                                               "', InvoiceNr = '" & sInvoiceNo & "' Where ID = '" & s5PairID & "'", sConstr)
                        Dim dsUpd5PairJC As New DataSet
                        daUpd5PairJC.Fill(dsUpd5PairJC)
                        dsUpd5PairJC.AcceptChanges()
                        nBalQty = nBalQty - n5prJCQty
                    Else
                        If nBalQty <> 0 And sFullQty = "Y" Then
                            sFullQty = "N"
                            GoTo Aa
                        ElseIf nBalQty <> 0 And sFullQty = "N" Then
Ac:
                            Dim daSel5PairJCPQ As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                       "' And ISNULL(Ponr,'') = ''  And Quantity <> '" & n5PairQty & "' Order by FivePairJobCardNo", sConstr)
                            Dim dsSel5PairJCPQ As New DataSet
                            daSel5PairJCPQ.Fill(dsSel5PairJCPQ)



                            If dsSel5PairJCPQ.Tables(0).Rows.Count > 0 Then
                                If dsSel5PairJCPQ.Tables(0).Rows.Count = 1 Then
                                    s5PRPQAid = dsSel5PairJCPQ.Tables(0).Rows(0).Item("ID").ToString
                                    n5PRPQAQty = Val(dsSel5PairJCPQ.Tables(0).Rows(0).Item("Quantity").ToString)

                                    Dim daDupl5PJC As New SqlDataAdapter("Insert Into FivePairJobCard  Select 	NEWID(),JobCardDetailID,	JobCardNo,	ProcessDate,	OrderNo,	SalesOrderNo,	Size,	FivePairJobCardNo,	Quantity,	Article,	Variant,	MainRawMaterialCode,	ColorCode,	Shipper,	SerialNo,	ArticleGroup,	BuyerGroupCode,	BuyerCode,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate,	EnteredOnMachineID,	ExeVersionNo,	ModuleName,	PONr,	SLNr,	BoxNr,	BarCode19,	BarCode20,	BarCode13,	InvoiceNr, IsScanned, ScannedOn,ID  from FivePairJobCard where ID = '" & s5PRPQAid & "'", sConstr)
                                    Dim dsDupl5PJC As New DataSet
                                    daDupl5PJC.Fill(dsDupl5PJC)
                                    dsDupl5PJC.AcceptChanges()
                                    sIsDuplicate = "Y"

                                    Dim daUPD5PairJCPQ As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & nBalQty & "' where id = '" & s5PRPQAid & "'", sConstr)
                                    Dim dsUpd5PairJCPQ As New DataSet
                                    daUPD5PairJCPQ.Fill(dsUpd5PairJCPQ)
                                    dsUpd5PairJCPQ.AcceptChanges()

                                    n5PRPQAQty = n5PRPQAQty - nBalQty

                                    Dim daUPD5PairJCPQ1 As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & n5PRPQAQty & "' where oid = '" & s5PRPQAid & "'", sConstr)
                                    Dim dsUpd5PairJCPQ1 As New DataSet
                                    daUPD5PairJCPQ1.Fill(dsUpd5PairJCPQ1)
                                    dsUpd5PairJCPQ1.AcceptChanges()

                                    GoTo Ab

                                ElseIf dsSel5PairJCPQ.Tables(0).Rows.Count = 2 Then
                                    s5PRPQAid = dsSel5PairJCPQ.Tables(0).Rows(0).Item("ID").ToString
                                    n5PRPQAQty = Val(dsSel5PairJCPQ.Tables(0).Rows(0).Item("Quantity").ToString)
                                    n5PRPQCFQty = n5PRPQAQty - nBalQty
                                    n5PRPQAQty = nBalQty
                                    Update5PairJobcard()

                                    s5PRPQAid = dsSel5PairJCPQ.Tables(0).Rows(1).Item("ID").ToString
                                    If sIsDuplicate = "Y" Then
                                        n5PRPQAQty = n5PRPQAQty + n5PRPQCFQty
                                    Else
                                        n5PRPQAQty = Val(dsSel5PairJCPQ.Tables(0).Rows(1).Item("Quantity").ToString)
                                        n5PRPQAQty = n5PRPQAQty + n5PRPQCFQty
                                    End If


                                    Update5PairJobcard()

                                    s5PRPQAid = ""
                                    n5PRPQAQty = 0
                                    n5PRPQCFQty = 0

                                    sIsDuplicate = "N"
                                    GoTo Ab
                                End If

                            End If

                        Else
                            Exit For
                        End If

                    End If
                Next

                If nBalQty <> 0 Then
                    sFullQty = "N"
                    GoTo Aa
                End If

            Next
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Dim nRowNumber, n5PRJCCount As Decimal
    Dim nFirstNo, nLastNo As Integer
    Dim sNew5PRJCNo, sJobcardNo As String
    Private Sub InsertOuterCartonPackingBarcodeOption1()
        Try
Ab:

            Dim daDelOCPB As New SqlDataAdapter("Delete from PackingBarcode Where SalesOrderDetlsID = '" & sSODID & "'", sConstr)
            Dim dsDelOCPB As New DataSet
            daDelOCPB.Fill(dsDelOCPB)
            dsDelOCPB.AcceptChanges()

            Dim daUpd5PJCPONR As New SqlDataAdapter("Update FivePairJobCard Set PONr = '' Where JobCardNo like  '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsUpd5PJCPONR As New DataSet
            daUpd5PJCPONR.Fill(dsUpd5PJCPONR)
            dsUpd5PJCPONR.AcceptChanges()

            Dim daSelJCDtls As New SqlDataAdapter("Select * from JobcardDetail Where SalesOrderDetailID = '" & sSODID & "' And ComponentGroup = 'UPPER'", sConstr)
            Dim dsSelJCDtls As New DataSet
            daSelJCDtls.Fill(dsSelJCDtls)

            n5PairQty = Val(dsSelJCDtls.Tables(0).Rows(0).Item("PairQuantity").ToString)

            Dim daSelOCPB As New SqlDataAdapter("Select SODID,CustomerOrderNo,BoxNo,CONVERT(float, d .Size) AS Size,  d .Qty, d .Size AS SizeinString from (Select upd.sodid,jd.CustomerOrderNo,upd.BoxNo,jd.Size01,upd.Quantity01, jd.Size02,upd.Quantity02, jd.Size03,upd.Quantity03, jd.Size04,upd.Quantity04, jd.Size05,upd.Quantity05, jd.Size06,upd.Quantity06, jd.Size07,upd.Quantity07, jd.Size08,upd.Quantity08, jd.Size09,upd.Quantity09, jd.Size10,upd.Quantity10, jd.Size11,upd.Quantity11, jd.Size12,upd.Quantity12, jd.Size13,upd.Quantity13, jd.Size14,upd.Quantity14, jd.Size15,upd.Quantity15, jd.Size16,upd.Quantity16, jd.Size17,upd.Quantity17, jd.Size18,upd.Quantity18 From SalesOrderDetails As JD , UpperPackingDetail As UPD Where JD.id = UPD.SODID) AS Tbl CROSS Apply (SELECT     Size01, Quantity01 UNION ALL SELECT     Size02, Quantity02 UNION ALL SELECT     Size03, Quantity03 UNION ALL SELECT     Size04, Quantity04 UNION ALL SELECT     Size05, Quantity05 UNION ALL SELECT     Size06, Quantity06 UNION ALL SELECT     Size07, Quantity07 UNION ALL SELECT     Size08, Quantity08 UNION ALL SELECT     Size09, Quantity09 UNION ALL SELECT     Size10, Quantity10 UNION ALL SELECT     Size11, Quantity11 UNION ALL SELECT     Size12, Quantity12 UNION ALL SELECT     Size13, Quantity13 UNION ALL SELECT     Size14, Quantity14 UNION ALL SELECT     Size15, Quantity15 UNION ALL SELECT     Size16, Quantity16 UNION ALL SELECT    Size17, Quantity17 UNION ALL SELECT     Size18, Quantity18) d (Size, Qty) WHERE     d .Qty > 0 And sodid = '" & sSODID & "' Order by Tbl.BoxNo, d.Size", sConstr)
            Dim dsSelOCPB As New DataSet
            daSelOCPB.Fill(dsSelOCPB)

            nRowCount = dsSelOCPB.Tables(0).Rows.Count

            Dim i As Integer = 0
            nInnerPackCount = 0
            nRowNumber = 0
            For i = 0 To nRowCount - 1
                sPONr = dsSelOCPB.Tables(0).Rows(i).Item("CustomerOrderNo").ToString
                sSLNr = dsSelOCPB.Tables(0).Rows(i).Item("BoxNo").ToString.PadLeft(3, CChar(CStr(0)))
                sBoxNr = dsSelOCPB.Tables(0).Rows(i).Item("BoxNo").ToString.PadLeft(2, CChar(CStr(0)))
                'If sBoxNr = "25" Then
                '    MsgBox("Box")
                'End If
                sSize = dsSelOCPB.Tables(0).Rows(i).Item("Size").ToString
                If sSize = "42" Then
                    'MsgBox(sSize)
                End If
                nQty = Val(dsSelOCPB.Tables(0).Rows(i).Item("Qty").ToString)
                nBalQty = Val(dsSelOCPB.Tables(0).Rows(i).Item("Qty").ToString)
                sFullQty = "Y"
                sIsDuplicate = "N"

                OuterBoxBarcodeGeneration()


Aa:

                Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                                       "' And ISNULL(Ponr,'') = '' Order by FivePairJobCardNo", sConstr)
                Dim dsSel5PairJC As New DataSet
                daSel5PairJC.Fill(dsSel5PairJC)


                'If sFullQty = "Y" Then
                '    Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                '                       "' And ISNULL(Ponr,'') = ''  And Quantity = '" & n5PairQty & "' Order by FivePairJobCardNo", sConstr)
                '    daSel5PairJC.Fill(dsSel5PairJC)
                'Else
                '    Dim daSel5PairJC1 As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                '                                            "' And ISNULL(Ponr,'') = ''  And Quantity = '" & nBalQty & "' Order by FivePairJobCardNo", sConstr)
                '    Dim dsSel5PairJC1 As New DataSet
                '    daSel5PairJC1.Fill(dsSel5PairJC1)

                '    If dsSel5PairJC1.Tables(0).Rows.Count = 1 Then
                '        Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                '                       "' And ISNULL(Ponr,'') = ''  And Quantity = '" & nBalQty & "' Order by FivePairJobCardNo", sConstr)
                '        daSel5PairJC.Fill(dsSel5PairJC)
                '    Else
                '        Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                '                       "' And ISNULL(Ponr,'') = ''  And Quantity <> '" & n5PairQty & "' Order by FivePairJobCardNo", sConstr)
                '        daSel5PairJC.Fill(dsSel5PairJC)
                '    End If



                'End If

                If dsSel5PairJC.Tables(0).Rows.Count = 0 Then
                    'MsgBox("No Rows", MsgBoxStyle.Information)

                    Dim daSel5PairJCForCopying As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                                                     "' Order by FivePairJobCardNo Desc", sConstr)
                    Dim dsSel5PairJCForCopying As New DataSet
                    daSel5PairJCForCopying.Fill(dsSel5PairJCForCopying)

                    s5PRPQAid = dsSel5PairJCForCopying.Tables(0).Rows(0).Item("ID").ToString
                    sJobcardNo = dsSel5PairJCForCopying.Tables(0).Rows(0).Item("JobCardNo").ToString

                    Dim daSel5PRJCCount As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And JobcardNo = '" & sJobcardNo & _
                                                              "' Order by FivePairJobCardNo Desc", sConstr)
                    Dim dsSel5PRJCCount As New DataSet
                    daSel5PRJCCount.Fill(dsSel5PRJCCount)

                    n5PRJCCount = dsSel5PRJCCount.Tables(0).Rows.Count + 1

                    sNew5PRJCNo = dsSel5PRJCCount.Tables(0).Rows(0).Item("FivePairJobCardNo").ToString

                    sNew5PRJCNo = Microsoft.VisualBasic.Left(sNew5PRJCNo, 23) + n5PRJCCount.ToString.PadLeft(3, "0")

                    Dim daDupl5PJC As New SqlDataAdapter("Insert Into FivePairJobCard  Select 	NEWID(),JobCardDetailID,	JobCardNo,	ProcessDate,	OrderNo,	SalesOrderNo,	Size,	FivePairJobCardNo,	Quantity,	Article,	Variant,	MainRawMaterialCode,	ColorCode,	Shipper,	SerialNo,	ArticleGroup,	BuyerGroupCode,	BuyerCode,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate,	EnteredOnMachineID,	ExeVersionNo,	ModuleName,	'',	SLNr,	BoxNr,	BarCode19,	BarCode20,	BarCode13,	InvoiceNr, IsScanned, ScannedOn,ID,Weight,RowNumber  from FivePairJobCard where ID = '" & s5PRPQAid & "'", sConstr)
                    Dim dsDupl5PJC As New DataSet
                    daDupl5PJC.Fill(dsDupl5PJC)
                    dsDupl5PJC.AcceptChanges()

                    Dim daUPD5PairJCPQ1 As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & nBalQty & "', FivePairJobcardNo = '" & sNew5PRJCNo & "' where oid = '" & s5PRPQAid & _
                                                              "' And PONr = ''", sConstr)
                    Dim dsUpd5PairJCPQ1 As New DataSet
                    daUPD5PairJCPQ1.Fill(dsUpd5PairJCPQ1)
                    dsUpd5PairJCPQ1.AcceptChanges()
                    GoTo Aa
                End If
                Dim j As Integer = 0

                For j = 0 To dsSel5PairJC.Tables(0).Rows.Count - 1
                    s5PairID = dsSel5PairJC.Tables(0).Rows(j).Item("ID").ToString
                    n5prJCQty = Val(dsSel5PairJC.Tables(0).Rows(j).Item("Quantity").ToString)
                    nRowNumber = nRowNumber + 1
                    'If j = dsSel5PairJC.Tables(0).Rows.Count - 1 Then
                    '    n5prJCQty = nBalQty
                    'End If
                    If (nBalQty < n5PairQty) Then
                        n5prJCQty = nBalQty
                    End If

                    If n5prJCQty > n5PairQty Then
                        n5prJCQty = n5PairQty
                    End If
                    nInnerPackCount = nInnerPackCount + 1
                    sSLNr = nInnerPackCount.ToString.PadLeft(3, CChar(CStr(0)))

                    InnerPackageBarcodeGeneration()


                    nBalQty = nBalQty - n5prJCQty
                    If nBalQty = 0 Then
                        Exit For
                    ElseIf nBalQty > 0 And (j = dsSel5PairJC.Tables(0).Rows.Count - 1) Then
                        GoTo Aa
                    End If
                    'End If
                Next
            Next

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Private Sub InsertOuterCartonPackingBarcodeOption2()
        Try
Ab:

            Dim daDelOCPB As New SqlDataAdapter("Delete from PackingBarcode Where SalesOrderDetlsID = '" & sSODID & "'", sConstr)
            Dim dsDelOCPB As New DataSet
            daDelOCPB.Fill(dsDelOCPB)
            dsDelOCPB.AcceptChanges()

            Dim daUpd5PJCPONR As New SqlDataAdapter("Update FivePairJobCard Set PONr = '' Where JobCardNo like  '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsUpd5PJCPONR As New DataSet
            daUpd5PJCPONR.Fill(dsUpd5PJCPONR)
            dsUpd5PJCPONR.AcceptChanges()

            Dim daSelJCDtls As New SqlDataAdapter("Select * from JobcardDetail Where SalesOrderDetailID = '" & sSODID & "' And ComponentGroup = 'UPPER'", sConstr)
            Dim dsSelJCDtls As New DataSet
            daSelJCDtls.Fill(dsSelJCDtls)

            n5PairQty = Val(dsSelJCDtls.Tables(0).Rows(0).Item("PairQuantity").ToString)

            Dim daSelOCPB As New SqlDataAdapter("Select SODID,CustomerOrderNo,BoxNo,CONVERT(float, d .Size) AS Size,  d .Qty, d .Size AS SizeinString from (Select upd.sodid,jd.CustomerOrderNo,upd.BoxNo,jd.Size01,upd.Quantity01, jd.Size02,upd.Quantity02, jd.Size03,upd.Quantity03, jd.Size04,upd.Quantity04, jd.Size05,upd.Quantity05, jd.Size06,upd.Quantity06, jd.Size07,upd.Quantity07, jd.Size08,upd.Quantity08, jd.Size09,upd.Quantity09, jd.Size10,upd.Quantity10, jd.Size11,upd.Quantity11, jd.Size12,upd.Quantity12, jd.Size13,upd.Quantity13, jd.Size14,upd.Quantity14, jd.Size15,upd.Quantity15, jd.Size16,upd.Quantity16, jd.Size17,upd.Quantity17, jd.Size18,upd.Quantity18 From SalesOrderDetails As JD , UpperPackingDetail As UPD Where JD.id = UPD.SODID) AS Tbl CROSS Apply (SELECT     Size01, Quantity01 UNION ALL SELECT     Size02, Quantity02 UNION ALL SELECT     Size03, Quantity03 UNION ALL SELECT     Size04, Quantity04 UNION ALL SELECT     Size05, Quantity05 UNION ALL SELECT     Size06, Quantity06 UNION ALL SELECT     Size07, Quantity07 UNION ALL SELECT     Size08, Quantity08 UNION ALL SELECT     Size09, Quantity09 UNION ALL SELECT     Size10, Quantity10 UNION ALL SELECT     Size11, Quantity11 UNION ALL SELECT     Size12, Quantity12 UNION ALL SELECT     Size13, Quantity13 UNION ALL SELECT     Size14, Quantity14 UNION ALL SELECT     Size15, Quantity15 UNION ALL SELECT     Size16, Quantity16 UNION ALL SELECT    Size17, Quantity17 UNION ALL SELECT     Size18, Quantity18) d (Size, Qty) WHERE     d .Qty > 0 And sodid = '" & sSODID & "' Order by Tbl.BoxNo, d.Size", sConstr)
            Dim dsSelOCPB As New DataSet
            daSelOCPB.Fill(dsSelOCPB)

            nRowCount = dsSelOCPB.Tables(0).Rows.Count

            Dim i As Integer = 0
            nInnerPackCount = 0
            nRowNumber = 0
            For i = 0 To nRowCount - 1
                sPONr = dsSelOCPB.Tables(0).Rows(i).Item("CustomerOrderNo").ToString
                sSLNr = dsSelOCPB.Tables(0).Rows(i).Item("BoxNo").ToString.PadLeft(3, CChar(CStr(0)))
                sBoxNr = dsSelOCPB.Tables(0).Rows(i).Item("BoxNo").ToString.PadLeft(2, CChar(CStr(0)))
                If sBoxNr = "09" Then
                    MsgBox(sBoxNr)
                End If
                sSize = dsSelOCPB.Tables(0).Rows(i).Item("Size").ToString
                If sSize = "42" And sBoxNr = "14" Then
                    MsgBox(sSize)
                End If
                nQty = Val(dsSelOCPB.Tables(0).Rows(i).Item("Qty").ToString)
                nBalQty = Val(dsSelOCPB.Tables(0).Rows(i).Item("Qty").ToString)
                sFullQty = "Y"
                sIsDuplicate = "N"
                nRowNumber = Val(sSize) * 10
                OuterBoxBarcodeGeneration()


Aa:

                Dim daSel5PairJC As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                                       "' And ISNULL(Ponr,'') = '' Order by FivePairJobCardNo", sConstr)
                Dim dsSel5PairJC As New DataSet
                daSel5PairJC.Fill(dsSel5PairJC)

                If dsSel5PairJC.Tables(0).Rows.Count = 0 Then
                    MsgBox("InComplete Packing List Details Stored! Alert ERP Dept", MsgBoxStyle.Information)
                    Exit Sub

                    Dim daSel5PairJCForCopying As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And Size = '" & sSize & _
                                                                     "' Order by FivePairJobCardNo Desc", sConstr)
                    Dim dsSel5PairJCForCopying As New DataSet
                    daSel5PairJCForCopying.Fill(dsSel5PairJCForCopying)

                    s5PRPQAid = dsSel5PairJCForCopying.Tables(0).Rows(0).Item("ID").ToString
                    sJobcardNo = dsSel5PairJCForCopying.Tables(0).Rows(0).Item("JobCardNo").ToString

                    Dim daSel5PRJCCount As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And JobcardNo = '" & sJobcardNo & _
                                                              "' Order by FivePairJobCardNo Desc", sConstr)
                    Dim dsSel5PRJCCount As New DataSet
                    daSel5PRJCCount.Fill(dsSel5PRJCCount)

                    n5PRJCCount = dsSel5PRJCCount.Tables(0).Rows.Count + 1

                    sNew5PRJCNo = dsSel5PRJCCount.Tables(0).Rows(0).Item("FivePairJobCardNo").ToString

                    sNew5PRJCNo = Microsoft.VisualBasic.Left(sNew5PRJCNo, 23) + n5PRJCCount.ToString.PadLeft(3, "0")

                    Dim daDupl5PJC As New SqlDataAdapter("Insert Into FivePairJobCard  Select 	NEWID(),JobCardDetailID,	JobCardNo,	ProcessDate,	OrderNo,	SalesOrderNo,	Size,	FivePairJobCardNo,	Quantity,	Article,	Variant,	MainRawMaterialCode,	ColorCode,	Shipper,	SerialNo,	ArticleGroup,	BuyerGroupCode,	BuyerCode,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate,	EnteredOnMachineID,	ExeVersionNo,	ModuleName,	'',	SLNr,	BoxNr,	BarCode19,	BarCode20,	BarCode13,	InvoiceNr, IsScanned, ScannedOn,ID,Weight,RowNumber,0,0  from FivePairJobCard where ID = '" & s5PRPQAid & "'", sConstr)
                    Dim dsDupl5PJC As New DataSet
                    daDupl5PJC.Fill(dsDupl5PJC)
                    dsDupl5PJC.AcceptChanges()

                    Dim daUPD5PairJCPQ1 As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & nBalQty & "', FivePairJobcardNo = '" & sNew5PRJCNo & "' where oid = '" & s5PRPQAid & _
                                                              "' And PONr = ''", sConstr)
                    Dim dsUpd5PairJCPQ1 As New DataSet
                    daUPD5PairJCPQ1.Fill(dsUpd5PairJCPQ1)
                    dsUpd5PairJCPQ1.AcceptChanges()
                    GoTo Aa
                End If
                Dim j As Integer = 0

                For j = 0 To dsSel5PairJC.Tables(0).Rows.Count - 1
                    s5PairID = dsSel5PairJC.Tables(0).Rows(j).Item("ID").ToString
                    n5prJCQty = Val(dsSel5PairJC.Tables(0).Rows(j).Item("Quantity").ToString)

                    'If j = dsSel5PairJC.Tables(0).Rows.Count - 1 Then
                    '    n5prJCQty = nBalQty
                    'End If
                    If (nBalQty < n5PairQty) Then

                        If n5prJCQty > nBalQty Then
                            Dim nNew5PairQty As Integer
                            nNew5PairQty = n5prJCQty - nBalQty
                            n5prJCQty = nBalQty

                            sJobcardNo = dsSel5PairJC.Tables(0).Rows(j).Item("JobcardNo").ToString
                            Dim daSel5PRJCCount As New SqlDataAdapter("Select * from FivePairJobCard where JobCardNo like  '" & sSalesOrderNo & "' + '%' And JobcardNo = '" & sJobcardNo & _
                                                                  "' Order by FivePairJobCardNo Desc", sConstr)
                            Dim dsSel5PRJCCount As New DataSet
                            daSel5PRJCCount.Fill(dsSel5PRJCCount)

                            n5PRJCCount = dsSel5PRJCCount.Tables(0).Rows.Count + 1

                            sNew5PRJCNo = dsSel5PRJCCount.Tables(0).Rows(0).Item("FivePairJobCardNo").ToString

                            sNew5PRJCNo = Microsoft.VisualBasic.Left(sNew5PRJCNo, 23) + n5PRJCCount.ToString.PadLeft(3, "0")

                            Dim daDupl5PJC As New SqlDataAdapter("Insert Into FivePairJobCard  Select 	NEWID(),JobCardDetailID,	JobCardNo,	ProcessDate,	OrderNo,	SalesOrderNo,	Size,	FivePairJobCardNo,	Quantity,	Article,	Variant,	MainRawMaterialCode,	ColorCode,	Shipper,	SerialNo,	ArticleGroup,	BuyerGroupCode,	BuyerCode,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate,	EnteredOnMachineID,	ExeVersionNo,	ModuleName,	'',	SLNr,	BoxNr,	BarCode19,	BarCode20,	BarCode13,	InvoiceNr, IsScanned, ScannedOn,ID,Weight,RowNumber,0,0  from FivePairJobCard where ID = '" & s5PairID & "'", sConstr)
                            Dim dsDupl5PJC As New DataSet
                            daDupl5PJC.Fill(dsDupl5PJC)
                            dsDupl5PJC.AcceptChanges()


                            Dim daUPD5PairJCPQ1 As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & nNew5PairQty & "', FivePairJobcardNo = '" & sNew5PRJCNo & "' where oid = '" & s5PairID & _
                                                                      "' And PONr = ''", sConstr)
                            Dim dsUpd5PairJCPQ1 As New DataSet
                            daUPD5PairJCPQ1.Fill(dsUpd5PairJCPQ1)
                            dsUpd5PairJCPQ1.AcceptChanges()

                        Else
                            'n5prJCQty = nBalQty
                        End If


                    End If

                    If n5prJCQty > n5PairQty Then
                        n5prJCQty = n5PairQty
                    End If
                    nInnerPackCount = nInnerPackCount + 1
                    sSLNr = nInnerPackCount.ToString.PadLeft(3, CChar(CStr(0)))

                    InnerPackageBarcodeGeneration()


                    nBalQty = nBalQty - n5prJCQty
                    If nBalQty = 0 Then
                        Exit For
                    ElseIf nBalQty > 0 And (j = dsSel5PairJC.Tables(0).Rows.Count - 1) Then
                        GoTo Aa
                    End If
                    'End If
                Next
            Next

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub

    Dim s5PairID, sID, sInvoiceNo As String

    Private Sub Update5PairJobcard()
        Try
            Dim daUPD5PairJCPQ As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & n5PRPQAQty & "' where id = '" & s5PRPQAid & "'", sConstr)
            Dim dsUpd5PairJCPQ As New DataSet
            daUPD5PairJCPQ.Fill(dsUpd5PairJCPQ)
            dsUpd5PairJCPQ.AcceptChanges()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub OuterBoxBarcodeGeneration()
        Try
            nSumfor13 = 0
            sStringfor13 = Trim(sPONr) + Trim(sBoxNr)

            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 1, 1)) * 1)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 2, 1)) * 3)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 3, 1)) * 1)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 4, 1)) * 3)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 5, 1)) * 1)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 6, 1)) * 3)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 7, 1)) * 1)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 8, 1)) * 3)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 9, 1)) * 1)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 10, 1)) * 3)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 11, 1)) * 1)
            nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 12, 1)) * 3)


            nRoundofffor13 = Math.Ceiling((nSumfor13 / 10))

            nMultiplyfor13 = nRoundofffor13 * 10

            nCheckDigitFor13 = nMultiplyfor13 - nSumfor13

            sBarcode13 = Trim(sPONr) + Trim(sBoxNr) + nCheckDigitFor13.ToString


            sStringfor19 = Trim(sPONr) + Trim(sBoxNr) + "00" + Trim(sSize) + "0" + Trim(sBoxNr)


            sBarcode19 = sStringfor19


            nSumfor20 = 0
            sStringfor20 = sStringfor19

            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 1, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 2, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 3, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 4, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 5, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 6, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 7, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 8, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 9, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 10, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 11, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 12, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 13, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 14, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 15, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 16, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 17, 1)) * 3)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 18, 1)) * 1)
            nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 19, 1)) * 3)

            nRoundofffor20 = Math.Ceiling((nSumfor20 / 10))

            nMultiplyfor20 = nRoundofffor20 * 10

            nCheckDigitFor20 = nMultiplyfor20 - nSumfor20

            sBarcode20 = sStringfor19 + nCheckDigitFor20.ToString

            sID = System.Guid.NewGuid.ToString()
            Dim daInsPB As New SqlDataAdapter("Insert Into PackingBarcode(ID,SalesOrderDetlsID,Size,Quantity,PONr,SLNr,BoxNr,BarCode19,BarCode20,BarCode13) Values ('" & sID & _
                                              "','" & sSODID & "','" & sSize & "','" & nQty & "','" & sPONr & "','" & sSLNr & "','" & sBoxNr & _
                                              "','" & sBarcode19 & "','" & sBarcode20 & "','" & sBarcode13 & "')", sConstr)
            Dim dsInsPB As New DataSet
            daInsPB.Fill(dsInsPB)
            dsInsPB.AcceptChanges()
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub InnerPackageBarcodeGeneration()
        nSumfor13 = 0
        sStringfor13 = Trim(sPONr) + Trim(sBoxNr)

        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 1, 1)) * 1)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 2, 1)) * 3)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 3, 1)) * 1)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 4, 1)) * 3)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 5, 1)) * 1)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 6, 1)) * 3)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 7, 1)) * 1)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 8, 1)) * 3)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 9, 1)) * 1)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 10, 1)) * 3)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 11, 1)) * 1)
        nSumfor13 = nSumfor13 + (Val(Microsoft.VisualBasic.Mid(sStringfor13, 12, 1)) * 3)


        nRoundofffor13 = Math.Ceiling((nSumfor13 / 10))

        nMultiplyfor13 = nRoundofffor13 * 10

        nCheckDigitFor13 = nMultiplyfor13 - nSumfor13

        sBarcode13 = Trim(sPONr) + Trim(sBoxNr) + nCheckDigitFor13.ToString


        'sStringfor19 = Trim(sPONr) + Trim(sBoxNr) + "00" + Trim(sSize) + Trim(sSLNr)
        sStringfor19 = Trim(sPONr) + "0000" + Trim(sSize) + Trim(sSLNr)


        sBarcode19 = sStringfor19


        nSumfor20 = 0
        sStringfor20 = sStringfor19

        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 1, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 2, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 3, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 4, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 5, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 6, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 7, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 8, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 9, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 10, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 11, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 12, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 13, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 14, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 15, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 16, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 17, 1)) * 3)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 18, 1)) * 1)
        nSumfor20 = nSumfor20 + (Val(Microsoft.VisualBasic.Mid(sStringfor19, 19, 1)) * 3)

        nRoundofffor20 = Math.Ceiling((nSumfor20 / 10))

        nMultiplyfor20 = nRoundofffor20 * 10

        nCheckDigitFor20 = nMultiplyfor20 - nSumfor20

        sBarcode20 = sStringfor19 + nCheckDigitFor20.ToString


        If nBalQty >= n5prJCQty Then
            sInvoiceNo = n5prJCQty.ToString
        Else
            sInvoiceNo = nBalQty.ToString
        End If
        Dim daSel5PJC As New SqlDataAdapter("Select * from FivePairJobCard WHERE JobCardNo In (Select JobcardNo From FivePairJobcard Where ID = '" & s5PairID & _
                                            "') And Size = '" & sSize & "' And IsNull(PONr,'') <> ''", sConstr)
        Dim dsSel5PJC As New DataSet
        daSel5PJC.Fill(dsSel5PJC)
        nRowNumber = Val(sSize) * 10
        nRowNumber = nRowNumber + ((dsSel5PJC.Tables(0).Rows.Count + 1) / 100)

        Dim daUpd5PairJC As New SqlDataAdapter("Update FivePairJobCard Set Quantity = '" & n5prJCQty & "', PONr	= '" & sPONr & "',  SLNr = '" & sSLNr & _
                                               "', BoxNr = '" & sBoxNr & "', BarCode19 = '" & sBarcode19 & _
                                               "', BarCode20 = '" & sBarcode20 & "', BarCode13 = '" & sBarcode13 & _
                                               "', InvoiceNr = '" & sInvoiceNo & "', RowNumber = '" & nRowNumber & _
                                               "' Where ID = '" & s5PairID & "'", sConstr)
        Dim dsUpd5PairJC As New DataSet
        daUpd5PairJC.Fill(dsUpd5PairJC)
        dsUpd5PairJC.AcceptChanges()
    End Sub


End Class
