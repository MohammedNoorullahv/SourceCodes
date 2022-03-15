Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid


Public Class frmSCPackingStatus

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)
    Dim keyascii As Integer

    Dim sPackingID, sSalesOrderDetailsId, sJobcardNo, sFivePairJobcardNo, sSize, sBarcode, sStatus As String
    Dim nJCCount, nCartonNo, nCartonQty, nFivePairQty, nScannedQty, nBoxScannedQty, nPackStatus As Integer
    Dim sS01, sS02, sS03, sS04, sS05, sS06, sDailyPlanDtlID As String
    Dim nOpenBoxCount, nCloseBoxCount, nBoxNotStarted, nOpenPackCount, nClosePackCount, nPackNotStarted As Integer
#End Region

    Private Sub cbPackingStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPackingStatus.Click
        GeneratePackingStatus()
    End Sub

    Private Sub GeneratePackingStatus()
        nOpenBoxCount = 0
        nCloseBoxCount = 0
        nBoxNotStarted = 0
        nOpenPackCount = 0
        nClosePackCount = 0
        nPackNotStarted = 0

        Dim daSelPackingMain As New SqlDataAdapter("Select * from EUPacking Where LicenceECodeNo = '" & Trim(tbLinkNo.Text) & "'", sConstr)
        Dim dsSelPackingMain As New DataSet
        daSelPackingMain.Fill(dsSelPackingMain)

        If dsSelPackingMain.Tables(0).Rows.Count = 1 Then
            sPackingID = dsSelPackingMain.Tables(0).Rows(0).Item("ID").ToString
            sSalesOrderDetailsId = dsSelPackingMain.Tables(0).Rows(0).Item("SalesOrderDetailId").ToString
        Else
            MsgBox("Invalid Link No.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim daSelJCDetails As New SqlDataAdapter("Select * from JobcardDetail Where SalesOrderDetailId = '" & sSalesOrderDetailsId & _
                                                 "' Order By JobcardNo", sConstr)
        Dim dsSelJCDetails As New DataSet
        daSelJCDetails.Fill(dsSelJCDetails)

        nJCCount = dsSelJCDetails.Tables(0).Rows.Count

        Dim daSelPackingDtls As New SqlDataAdapter("Select * from EUPackingDetail Where HID = '" & sPackingID & _
                                                   "'  Order by CartonNo", sConstr)
        Dim dsSelPackingDtls As New DataSet
        daSelPackingDtls.Fill(dsSelPackingDtls)

        Dim i As Integer = 0

        For i = 0 To dsSelPackingDtls.Tables(0).Rows.Count - 1


            nCartonNo = Val(dsSelPackingDtls.Tables(0).Rows(i).Item("CartonNo").ToString)
            nCartonQty = Val(dsSelPackingDtls.Tables(0).Rows(i).Item("Quantity").ToString)
            nBoxScannedQty = 0

            Dim j As Integer = 0
            For j = 0 To nJCCount - 1
                sJobcardNo = dsSelJCDetails.Tables(0).Rows(j).Item("JobcardNo").ToString

                Dim daSelFPJC As New SqlDataAdapter("Select * from Fivepairjobcard Where JobCardNo = '" & sJobcardNo & _
                                                    "' And Cast(BoxNr As Int) = '" & nCartonNo & "' Order by FivePairJobCardNo", sConstr)
                Dim dsSelFPJC As New DataSet
                daSelFPJC.Fill(dsSelFPJC)

                Dim k As Integer = 0
                For k = 0 To dsSelFPJC.Tables(0).Rows.Count - 1
                    sFivePairJobcardNo = dsSelFPJC.Tables(0).Rows(k).Item("FivePairJobcardNo").ToString
                    sSize = dsSelFPJC.Tables(0).Rows(k).Item("Size").ToString
                    nFivePairQty = Val(dsSelFPJC.Tables(0).Rows(k).Item("Quantity").ToString)
                    nScannedQty = 0

                    Dim daDailyPlanDtl As New SqlDataAdapter("Select * from FSDailyPlanDetail Where FivePairJobcardNo = '" & sFivePairJobcardNo & "'", sConstr)
                    Dim dsDailyPlanDtl As New DataSet
                    daDailyPlanDtl.Fill(dsDailyPlanDtl)

                    If dsDailyPlanDtl.Tables(0).Rows.Count = 1 Then
                        sDailyPlanDtlID = dsDailyPlanDtl.Tables(0).Rows(0).Item("ID").ToString
                        If Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S01").ToString) > 0 Then
                            sBarcode = Replace(sJobcardNo, "-", "") + sSize.ToString.PadRight(3, "0") + Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S01").ToString).ToString.PadLeft(3, "0")
                            CheckUpperProduction()
                            sS01 = sStatus
                            If sStatus = "Y" Then
                                nScannedQty = nScannedQty + 1
                                nBoxScannedQty = nBoxScannedQty + 1
                            End If
                        Else
                            sS01 = "NA"
                        End If

                        If Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S02").ToString) > 0 Then
                            sBarcode = Replace(sJobcardNo, "-", "") + sSize.ToString.PadRight(3, "0") + Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S02").ToString).ToString.PadLeft(3, "0")
                            CheckUpperProduction()
                            sS02 = sStatus
                            If sStatus = "Y" Then
                                nScannedQty = nScannedQty + 1
                                nBoxScannedQty = nBoxScannedQty + 1
                            End If
                        Else
                            sS02 = "NA"
                        End If

                        If Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S03").ToString) > 0 Then
                            sBarcode = Replace(sJobcardNo, "-", "") + sSize.ToString.PadRight(3, "0") + Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S03").ToString).ToString.PadLeft(3, "0")
                            CheckUpperProduction()
                            sS03 = sStatus
                            If sStatus = "Y" Then
                                nScannedQty = nScannedQty + 1
                                nBoxScannedQty = nBoxScannedQty + 1
                            End If
                        Else
                            sS03 = "NA"
                        End If


                        If Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S04").ToString) > 0 Then
                            sBarcode = Replace(sJobcardNo, "-", "") + sSize.ToString.PadRight(3, "0") + Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S04").ToString).ToString.PadLeft(3, "0")
                            CheckUpperProduction()
                            sS04 = sStatus
                            If sStatus = "Y" Then
                                nScannedQty = nScannedQty + 1
                                nBoxScannedQty = nBoxScannedQty + 1
                            End If
                        Else

                            sS04 = "NA"
                        End If

                        If Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S05").ToString) > 0 Then
                            sBarcode = Replace(sJobcardNo, "-", "") + sSize.ToString.PadRight(3, "0") + Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S05").ToString).ToString.PadLeft(3, "0")
                            CheckUpperProduction()
                            sS05 = sStatus
                            If sStatus = "Y" Then
                                nScannedQty = nScannedQty + 1
                                nBoxScannedQty = nBoxScannedQty + 1
                            End If
                        Else
                            sS05 = "NA"
                        End If

                        If Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S06").ToString) > 0 Then
                            sBarcode = Replace(sJobcardNo, "-", "") + sSize.ToString.PadRight(3, "0") + Val(dsDailyPlanDtl.Tables(0).Rows(0).Item("S06").ToString).ToString.PadLeft(3, "0")
                            CheckUpperProduction()
                            sS06 = sStatus
                            If sStatus = "Y" Then
                                nScannedQty = nScannedQty + 1
                                nBoxScannedQty = nBoxScannedQty + 1
                            End If
                        Else
                            sS06 = "NA"
                        End If
                    Else
                        MsgBox("Daily Plan Dtl Not Available", MsgBoxStyle.Information)
                    End If

                    If nFivePairQty = nScannedQty Then
                        nPackStatus = 1
                        nClosePackCount = nClosePackCount + 1
                    Else
                        If nScannedQty > 0 Then
                            nPackStatus = 0
                            nOpenPackCount = nOpenPackCount + 1
                        Else
                            nPackNotStarted = nPackNotStarted + 1
                        End If
                    End If

                    Dim daUpdDPD As New SqlDataAdapter("Update FSDailyPlanDetail Set SS01 = '" & sS01 & _
                                                       "', SS02 = '" & sS02 & "', SS03 = '" & sS03 & _
                                                       "', SS04 = '" & sS04 & "', SS05 = '" & sS05 & _
                                                       "', SS06 = '" & sS06 & "', ReadyToPack = '" & nScannedQty & _
                                                       "', IsPackCompleted = '" & nPackStatus & _
                                                       "' Where ID = '" & sDailyPlanDtlID & "'", sConstr)
                    Dim dsUpdDPD As New DataSet
                    daUpdDPD.Fill(dsUpdDPD)
                    dsUpdDPD.AcceptChanges()

                Next

            Next
            If nCartonQty = nBoxScannedQty Then
                nCloseBoxCount = nCloseBoxCount + 1
            Else
                If nBoxScannedQty > 0 Then
                    nOpenBoxCount = nOpenBoxCount + 1
                Else
                    nBoxNotStarted = nBoxNotStarted + 1
                End If
            End If
        Next

        Dim daUpdEUPacking As New SqlDataAdapter("Update EUPacking Set OpenBoxCount = '" & nOpenBoxCount & _
                                                     "', ClosedBoxCount = '" & nCloseBoxCount & "', BoxNotStarted = '" & nBoxNotStarted & _
                                                     "', OpenPackCount = '" & nOpenPackCount & _
                                                     "', ClosedPackCount = '" & nClosePackCount & "', PackNotStarted = '" & nPackNotStarted & _
                                                     "' Where ID = '" & sPackingID & "'", sConstr)
        Dim dsUPDEUPacking As New DataSet
        daUpdEUPacking.Fill(dsUPDEUPacking)
        dsUPDEUPacking.AcceptChanges()

        MsgBox("Completed")
    End Sub

    Private Sub CheckUpperProduction()
        Try
        
            Dim daSelUpp As New SqlDataAdapter("Select UPP.Barcode,UPP.ProcessName,UPP.ProcessDate,SME.SequenceNo,IsNull(SME.IsLastStage,0) As IsLastStage From UpProduction As UPP,StagesManualEntry As SME Where UPP.ProcessName = SME.StageCode And SME.StageType = 'MOCASSIN' And upp.Barcode = '" & sBarcode & _
                                               "' Order by SequenceNo Desc", sConstr)
            Dim dsSelUpp As New DataSet
            daSelUpp.Fill(dsSelUpp)

            If dsSelUpp.Tables(0).Rows.Count = 0 Then
                sStatus = "NS"
            Else
                If Val(dsSelUpp.Tables(0).Rows(0).Item("IsLastStage")) * -1 = 1 Then
                    sStatus = "Y"
                Else
                    'sStatus = "N"
                    sStatus = dsSelUpp.Tables(0).Rows(0).Item("ProcessName").ToString
                End If
            End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
    
    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub lblBoxNo001_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class