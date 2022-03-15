Public Class frmInvoiceVer2

    Dim myccInvoicesWithDetails As New ccInvoicesWithDetails
    Dim myccInvoicesWithDetailsVer2 As New ccInvoicesWithDetailsVer2
    Dim myccInvoicesWithDetailsWithCustomer As New ccInvoicesWithDetailsWithCustomer
    Dim myccInvoicesWithDetailsWithCustomerArticle As New ccInvoicesWithDetailsWithCustomerArticle
    Dim myccInvoicesWithDetailsWithCustomerCodification As New ccInvoicesWithDetailsWithCustomerCodification

    Dim mystrSolarInvoice4SGM4Print As New strSolarInvoice4SGM4Print

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try
        If chkbxAllOrder.Checked = False And chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = False Then
            MsgBox("Type of Order is not selected", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If chkbxAllOrder.Checked = True Then
            If chkbxAll.Checked = True Or (chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True) Then
                mdlSGM.sSelectOption = "A-A" ' - 1 . A-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                    ' - 2 . A-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-H"

                    ' - 3 . A-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-E"

                    ' - 4 . A-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-EH"

                    ' - 5 . A-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-3"

                    ' - 6 . A-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-3H"

                    ' - 7 . A-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-3E"

                    ' - 8 . A-3EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-3EH"

                    ' - 9 . A-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-C"

                    ' - 10 . A-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-CH"

                    ' - 11 . A-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-CE"

                    ' - 12 . A-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-CEH"

                    ' - 13 . A-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-C3"

                    ' - 14 . A-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then
                    mdlSGM.sSelectOption = "A-C3H"

                    ' - 15 . A-C3E
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = "A-C3E"

                End If


            End If


        ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = True Then
            ' - 16 . G-A

            If chkbxAll.Checked = True Then
                mdlSGM.sSelectOption = "G-A" ' - 16 . G-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-H" ' - 17 . G-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-E" ' - 18 . G-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-EH" ' - 19 . G-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-3" ' - 20 . G-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-3H" ' - 21 . G-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-3E" ' - 22 . G-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-3EH" ' - 23 . G-3EH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-C" ' - 24 . G-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-CH" ' - 25 . G-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-CE" ' - 26 . G-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-CEH" ' - 27 . G-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-C3" ' - 28 . G-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "G-C3H" ' - 29 . G-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "G-C3E" ' - 30 . G-C3E
                End If
            End If


        ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = True And chkbxGeneral.Checked = False Then


            If chkbxAll.Checked = True Then
                mdlSGM.sSelectOption = "J-A" ' - 31 . J-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-H" ' - 32 . J-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-E" ' - 33 . J-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-EH" ' - 34 . J-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-3" ' - 35 . J-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-3H" ' - 36 . J-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-3E" ' - 37 . J-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-3EH" ' - 38 . J-3EH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-C" ' - 39 . J-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-CH" ' - 40 . J-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-CE" ' - 41 . J-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-CEH" ' - 42 . J-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-C3" ' - 43 . J-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "J-C3H" ' - 44 . J-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "J-C3E" ' - 45 . J-C3E

                End If


            End If

        ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = True And chkbxGeneral.Checked = True Then

            If chkbxAll.Checked = True Then
                mdlSGM.sSelectOption = "JG-A" ' - 46 . JG-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-H" ' - 47 . JG-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-E" ' - 48 . JG-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-EH" ' - 49 . JG-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-3" ' - 50 . JG-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-3H" ' - 51 . JG-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-3E" ' - 52 . JG-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-3EH" ' - 53 . JG-3EH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-C" ' - 54 . JG-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-CH" ' - 55 . JG-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-CE" ' - 56 . JG-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-CEH" ' - 57 . JG-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-C3" ' - 58 . JG-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "JG-C3H" ' - 59 . JG-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "JG-C3E" ' - 60 . JG-C3E

                End If

            End If

        ElseIf chkbxSalesOrder.Checked = True And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = False Then

            If chkbxAll.Checked = True Then
                mdlSGM.sSelectOption = "S-A" ' - 61 . S-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-H" ' - 62 . S-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-E" ' - 63 . S-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-EH" ' - 64 . S-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-3" ' - 65 . S-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-3H" ' - 66 . S-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-3E" ' - 67 . S-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-3EH" ' - 68 . S-3EH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-C" ' - 69 . S-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-CH" ' - 70 . S-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-CE" ' - 71 . S-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-CEH" ' - 72 . S-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-C3" ' - 73 . S-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "S-C3H" ' - 74 . S-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "S-C3E" ' - 75 . S-C3E

                End If

            End If

        ElseIf chkbxSalesOrder.Checked = True And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = True Then

            If chkbxAll.Checked = True Then
                mdlSGM.sSelectOption = "SG-A" ' - 76 . SG-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-H" ' - 77 . SG-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-E" ' - 78 . SG-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-EH" ' - 79 . SG-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-3" ' - 80 . SG-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-3H" ' - 81 . SG-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-3E" ' - 82 . SG-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-3EH" ' - 83 . SG-3EH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-C" ' - 84 . SG-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-CH" ' - 85 . SG-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-CE" ' - 86 . SG-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-CEH" ' - 87 . SG-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-C3" ' - 88 . SG-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SG-C3H" ' - 89 . SG-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SG-C3E" ' - 90 . SG-C3E

                End If

            End If

        ElseIf chkbxSalesOrder.Checked = True And chkbxJobWorkOrder.Checked = True And chkbxGeneral.Checked = False Then


            If chkbxAll.Checked = True Then
                mdlSGM.sSelectOption = "SJ-A" ' - 91 . SJ-A
            Else
                If chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then
                    mdlSGM.sSelectOption = ""
                    MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                    Exit Sub
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-H" ' - 92 . SJ-H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-E" ' - 93 . SJ-E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-EH" ' - 94 . SJ-EH
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-3" ' - 95 . SJ-3
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-3H" ' - 96 . SJ-3H
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-3E" ' - 97 . SJ-3E
                ElseIf chkbxFormC.Checked = False And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-3EH" ' - 98 . SJ-3EH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-C" ' - 99 . SJ-C
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-CH" ' - 100 . SJ-CH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-CE" ' - 101 . SJ-CE
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = False And chkbxExport.Checked = True And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-CEH" ' - 102 . SJ-CEH
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-C3" ' - 103 . SJ-C3
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = False And chkbxFormH.Checked = True Then : mdlSGM.sSelectOption = "SJ-C3H" ' - 104 . SJ-C3H
                ElseIf chkbxFormC.Checked = True And chkbxCT3.Checked = True And chkbxExport.Checked = True And chkbxFormH.Checked = False Then : mdlSGM.sSelectOption = "SJ-C3E" ' - 105 . SJ-C3E

                End If

            End If



        End If




        
        LoadAllInvoices()

        ''Catch ex As Exception

        ''End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub

    Private Sub rbCodification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If rbCodification.Checked = True Then
                cbxCodification.Enabled = True
                cbxArticleName.Enabled = False
                LoadCodification()
            Else
                cbxArticleName.Enabled = True
                cbxCodification.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim ngrdRowCount As Integer
    Private Sub LoadAllInvoices()
        ''Try

        'GoTo Ac
        Dim i As Integer = 0
        Dim j As Integer = 19

        grdInvoices.BringToFront()

        grdInvoices.DataSource = Nothing
        grdInvoicesV1.Columns.Clear()
        'grdInvoices.DataBind();

Ab:
        'ngrdRowCount = grdInvoicesV1.RowCount
        'For i = 0 To ngrdRowCount
        '    grdInvoicesV1.DeleteRow(i)
        'Next
        'ngrdRowCount = grdInvoicesV1.RowCount
        'If ngrdRowCount > 0 Then
        '    GoTo Ab
        'End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If cbxCustomer.Text = " ALL CUSTOMERS" And cbxArticleName.Text = " ALL ARTICLES" Then
            mdlSGM.sInvoiceFilterOption = "ACAA"
        ElseIf cbxCustomer.Text = " ALL CUSTOMERS" And cbxArticleName.Text <> " ALL ARTICLES" Then
            mdlSGM.sInvoiceFilterOption = "ACSA"
        ElseIf cbxCustomer.Text <> " ALL CUSTOMERS" And cbxArticleName.Text = " ALL ARTICLES" Then
            mdlSGM.sInvoiceFilterOption = "SCAA"
        ElseIf cbxCustomer.Text <> " ALL CUSTOMERS" And cbxArticleName.Text <> " ALL ARTICLES" Then
            mdlSGM.sInvoiceFilterOption = "SCSA"
        End If

        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.sSelectedArticle = cbxArticleName.Text

        grdInvoices.DataSource = myccInvoicesWithDetailsVer2.LoadAllInvoicesACAA()


        With grdInvoicesV1

            .Columns(1).VisibleIndex = -1
            .Columns(3).VisibleIndex = -1
            .Columns(4).VisibleIndex = -1
            .Columns(5).VisibleIndex = -1
            .Columns(6).VisibleIndex = -1
            .Columns(7).VisibleIndex = -1
            .Columns(11).VisibleIndex = -1
            .Columns(12).VisibleIndex = -1
            .Columns(23).VisibleIndex = -1
            .Columns(25).VisibleIndex = -1
            .Columns(29).VisibleIndex = -1

            .Columns(0).VisibleIndex = 30
            .Columns(8).VisibleIndex = 4

            .Columns(15).VisibleIndex = 31
            .Columns(16).VisibleIndex = 32
            .Columns(17).VisibleIndex = 33
            .Columns(36).VisibleIndex = 7

            .Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            .Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            .Columns(2).Width = 150
            .Columns(14).Width = 100

            .Columns(32).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            '.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(9).DisplayFormat.FormatString = "dd-MMM-yyyy"

            For j = 19 To 32
                .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(j).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                .Columns(j).DisplayFormat.FormatString = "0.00"
            Next
            .Columns(19).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Columns(19).DisplayFormat.FormatString = "0"

        End With

        MsgBox("Loading Completed", MsgBoxStyle.Information)
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub
    'Public Function LoadCustomer() As DataTable

    '    Dim sCmd As New SqlCommand
    '    Dim daSelCustomer As New SqlDataAdapter
    '    Dim dsSelCustomer As New DataSet

    '    sCmd.Connection = sCnn
    '    sCmd.CommandText = "sgm_InvoiceDetails"
    '    sCmd.CommandType = CommandType.StoredProcedure

    '    sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCust"
    '    sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
    '    sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


    '    dsSelCustomer.Clear()
    '    daSelCustomer = New SqlDataAdapter(sCmd)
    '    daSelCustomer.Fill(dsSelCustomer, "Customer")
    '    Return dsSelCustomer.Tables(0)

    '    dsSelCustomer = Nothing
    '    sCnn.Close()

    'End Function
    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()
        cbxCustomer.DataSource = myccInvoicesWithDetails.LoadCustomer
        cbxCustomer.DisplayMember = "BuyerName" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub LoadArticle()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxArticleName.DataSource = Nothing : cbxArticleName.Items.Clear()
        cbxArticleName.DataSource = myccInvoicesWithDetails.LoadArticleofCustomer
        cbxArticleName.DisplayMember = "SoleName" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCodification()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCodification.DataSource = Nothing : cbxCodification.Items.Clear()
        cbxCodification.DataSource = myccInvoicesWithDetails.LoadCodificationofCustomer
        cbxCodification.DisplayMember = "CodificationNew" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        LoadCustomer()
        LoadArticle()
    End Sub

    Private Sub rbArticleName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbArticleName.Checked = True Then
            LoadArticle()
        End If
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdInvoices.ExportToXlsx("E:\InvoiceDetails.xlsx")
        MsgBox("Export Completed")

    End Sub

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim i As Integer = 0


        myccInvoicesWithDetails.DelInvoice4Print()

        For i = 0 To grdInvoicesV1.RowCount - 1

            'mystrSolarInvoice4SGM4Print.PKID = grdInvoicesV1.GetDataRow(i).Item("").ToString 'As Integer
            mystrSolarInvoice4SGM4Print.BuyerGroup = grdInvoicesV1.GetDataRow(i).Item("BuyerGroup").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerCode = grdInvoicesV1.GetDataRow(i).Item("BuyerCode").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerName = grdInvoicesV1.GetDataRow(i).Item("BuyerName").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerAddress = grdInvoicesV1.GetDataRow(i).Item("BuyerAddress").ToString 'As String
            mystrSolarInvoice4SGM4Print.ConsigneeName = grdInvoicesV1.GetDataRow(i).Item("ConsigneeName").ToString 'As String
            mystrSolarInvoice4SGM4Print.ConsigneeAdress = grdInvoicesV1.GetDataRow(i).Item("ConsigneeAdress").ToString 'As String
            mystrSolarInvoice4SGM4Print.City = grdInvoicesV1.GetDataRow(i).Item("City").ToString 'As String
            mystrSolarInvoice4SGM4Print.Pincode = grdInvoicesV1.GetDataRow(i).Item("Pincode").ToString 'As String
            mystrSolarInvoice4SGM4Print.InvoiceNo = grdInvoicesV1.GetDataRow(i).Item("InvoiceNo").ToString 'As String
            mystrSolarInvoice4SGM4Print.InvDate = grdInvoicesV1.GetDataRow(i).Item("InvDate").ToString 'As Date
            mystrSolarInvoice4SGM4Print.InvType = grdInvoicesV1.GetDataRow(i).Item("InvType").ToString 'As String
            mystrSolarInvoice4SGM4Print.CT3 = grdInvoicesV1.GetDataRow(i).Item("CT3").ToString 'As String
            mystrSolarInvoice4SGM4Print.Accounted = grdInvoicesV1.GetDataRow(i).Item("Accounted").ToString 'As String
            mystrSolarInvoice4SGM4Print.Code = grdInvoicesV1.GetDataRow(i).Item("Code").ToString 'As String
            mystrSolarInvoice4SGM4Print.ArticleName = grdInvoicesV1.GetDataRow(i).Item("Sole").ToString 'As String
            mystrSolarInvoice4SGM4Print.Colour = grdInvoicesV1.GetDataRow(i).Item("Colour").ToString 'As String
            mystrSolarInvoice4SGM4Print.OldCodification = grdInvoicesV1.GetDataRow(i).Item("OldCodification").ToString 'As String
            mystrSolarInvoice4SGM4Print.CodificationNew = grdInvoicesV1.GetDataRow(i).Item("CodificationNew").ToString 'As String
            mystrSolarInvoice4SGM4Print.Quantity = grdInvoicesV1.GetDataRow(i).Item("Quantity").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.Rate = grdInvoicesV1.GetDataRow(i).Item("Rate").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.Value = grdInvoicesV1.GetDataRow(i).Item("Value").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.ExcisePercentage = grdInvoicesV1.GetDataRow(i).Item("ExcisePercentage").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.DWExciseDuty = grdInvoicesV1.GetDataRow(i).Item("DWExciseDuty").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.CessPercentage = grdInvoicesV1.GetDataRow(i).Item("CessPercentage").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DWCessAmount = grdInvoicesV1.GetDataRow(i).Item("DWCessAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.EduCessPercentage = grdInvoicesV1.GetDataRow(i).Item("EduCessPercentage").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DWEduCessAmount = grdInvoicesV1.GetDataRow(i).Item("DWEduCessAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DutyPayable = grdInvoicesV1.GetDataRow(i).Item("DutyPayable").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.SubTotal = Val(grdInvoicesV1.GetDataRow(i).Item("SubTotal").ToString) 'As Decimal
            mystrSolarInvoice4SGM4Print.CSTorVat = grdInvoicesV1.GetDataRow(i).Item("CSTorVat").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.CSTorVATAmount = grdInvoicesV1.GetDataRow(i).Item("CSTorVATAmount").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.InvAmount = Val(grdInvoicesV1.GetDataRow(i).Item("InvAmount").ToString) 'As Decimal

            'myccArticleMaster.InsOutstanding4Print(mystrstrSolarArticleMaster4SGM4Print)
            myccInvoicesWithDetails.InsInvoice4Print(mystrSolarInvoice4SGM4Print)

        Next
        mdlSGM.sReportType = "Invoice"
        frmReport.Show()
    End Sub

    Private Sub chkbxSalesOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If chkbxSalesOrder.Checked = True Then
        '    If chkbxAll.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False Then
        '        chkbxAll.Checked = True
        '    End If
        '    chkbxGeneral.Checked = False
        'Else
        '    chkbxAll.Checked = False : chkbxCT3.Checked = False : chkbxExport.Checked = False : chkbxFormC.Checked = False : chkbxFormH.Checked = False
        'End If
    End Sub

   
    Private Sub chkbxAllOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxAllOrder.CheckedChanged
        If chkbxAllOrder.Checked = True Then
            chkbxSalesOrder.Checked = False : chkbxSalesOrder.Enabled = False
            chkbxJobWorkOrder.Checked = False : chkbxJobWorkOrder.Enabled = False
            chkbxGeneral.Checked = False : chkbxGeneral.Enabled = False
        Else
            chkbxSalesOrder.Enabled = True
            chkbxJobWorkOrder.Enabled = True
            chkbxGeneral.Enabled = True
        End If
    End Sub

    Private Sub chkbxAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxAll.CheckedChanged
        If chkbxAll.Checked = True Then
            chkbxFormC.Checked = False : chkbxFormC.Enabled = False
            chkbxCT3.Checked = False : chkbxCT3.Enabled = False
            chkbxExport.Checked = False : chkbxExport.Enabled = False
            chkbxFormH.Checked = False : chkbxFormH.Enabled = False
        Else
            chkbxFormC.Enabled = True
            chkbxCT3.Enabled = True
            chkbxExport.Enabled = True
            chkbxFormH.Enabled = True
        End If
    End Sub


    Private Sub chkbxGeneral_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxGeneral.CheckedChanged
        If chkbxGeneral.Checked = True Then
            If chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = False Then
                chkbxAll.Checked = True : chkbxAll.Text = "General"
            End If
        Else
            chkbxAll.Text = "All"
        End If
    End Sub

    Private Sub cbxArticleName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxArticleName.GotFocus
        LoadArticle()
    End Sub

    Private Sub cbxCustomer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCustomer.GotFocus
        LoadCustomer()
    End Sub
End Class