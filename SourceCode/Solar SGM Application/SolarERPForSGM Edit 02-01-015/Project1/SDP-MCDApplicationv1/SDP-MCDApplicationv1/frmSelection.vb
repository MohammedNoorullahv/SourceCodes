Option Explicit On

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing
Imports Microsoft.Win32
Imports System.Data

Public Class frmSelection

    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccMCD As New ccMCD
    Dim myccOptimizerComponentUser As New ccOptimizerComponent
    Dim myOptimizerStrUserAuthentication As New StrUserAuthentication

    Dim sSpoolCode As String
    Dim nYesNo, nRowCount As Integer
    Dim FinYear As String = Nothing

    Private Sub frmSelection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadComboItems()
        LoadAccessRights()
        sSection = ""
    End Sub

 

#Region "SELECTION"

    Private Sub LoadAccessRights()
        cbMoulding.Enabled = False
        cbFinishing.Enabled = False
        cbPacking.Enabled = False
        cbDispatch.Enabled = False
        mdlSGM.sSelectedSection = ""
        Dim daSelAccessRights As New SqlDataAdapter("Select * from AppModuleAccess Where UserName = '" & mdlSGM.sUserName.ToUpper & _
                                                    "' Order by AccessModuleName", sConstr)
        Dim dsSelAccessRights As New DataSet
        daSelAccessRights.Fill(dsSelAccessRights)

        If dsSelAccessRights.Tables(0).Rows.Count = 0 Then
            cbNext.Enabled = False
        Else
            Dim i As Integer = 0

            For i = 0 To dsSelAccessRights.Tables(0).Rows.Count - 1
                If dsSelAccessRights.Tables(0).Rows(i).Item("AccessModuleName").ToString = "ALL" Then
                    cbMoulding.Enabled = True
                    cbFinishing.Enabled = True
                    cbPacking.Enabled = True
                    cbDispatch.Enabled = True
                ElseIf dsSelAccessRights.Tables(0).Rows(i).Item("AccessModuleName").ToString = "MOULDING" Then
                    cbMoulding.Enabled = True
                    mdlSGM.sSelectedSection = "MOULDING"
                ElseIf dsSelAccessRights.Tables(0).Rows(i).Item("AccessModuleName").ToString = "FINISHING" Then
                    cbFinishing.Enabled = True
                    mdlSGM.sSelectedSection = "FINISHING"
                ElseIf dsSelAccessRights.Tables(0).Rows(i).Item("AccessModuleName").ToString = "PACKING" Then
                    cbPacking.Enabled = True
                    mdlSGM.sSelectedSection = "PACKING"
                End If
            Next
        End If

    End Sub

    Private Sub cbMoulding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMoulding.Click
        If sSection = "" Or sSection = "MOULD" Then
            mdlSGM.sSelectedSection = "MOULDING"
            sSection = "MOULD"
            sProcess = "Production"
        Else
            MsgBox("Other than MOULD Section already selectd. Kindly save previous Spool and change the section", MsgBoxStyle.Information)
        End If

    End Sub


    Private Sub cbFinishing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFinishing.Click
        If sSection = "" Or sSection = "FINISH" Then
            mdlSGM.sSelectedSection = "FINISHING"
            sSection = "FINISH"
            sProcess = "Production"
        Else
            MsgBox("Other than FINISH Section already selectd. Kindly save previous Spool and change the section", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cbPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPacking.Click
        If sSection = "" Or sSection = "PACK" Then
            mdlSGM.sSelectedSection = "PACKING"
            sSection = "PACK"
            sProcess = "Packing"
        Else
            MsgBox("Other than PACK Section already selectd. Kindly save previous Spool and change the section", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cbDispatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDispatch.Click
        mdlSGM.sSelectedSection = "DISPATCH"
    End Sub

    Private Sub cbNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbNext.Click
        If mdlSGM.sSelectedSection = "" Then
            MsgBox("Section Not Selected Properly. Cannot proceed further!", MsgBoxStyle.Information)
            Exit Sub
        End If

        mdlSGM.sShiftCode = cbxShift.SelectedValue
        mdlSGM.sMachine = cbxMachine.Text
        mdlSGM.sMachinecode = cbxMachine.SelectedValue

        frmScanning.lblScanning.Text = "BARCODE SCANNING : " + mdlSGM.sSelectedSection
        

        Dim daSelSpoolHdr As New SqlDataAdapter("Select * from Spool Where Department = '" & mdlSGM.sSelectedSection & _
                                                        "' And Shift = '" & mdlSGM.sShiftCode & _
                                                        "' And UserName = '" & mdlSGM.sUserName & _
                                                        "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolHdr As New DataSet
        daSelSpoolHdr.Fill(dsSelSpoolHdr)

        If dsSelSpoolHdr.Tables(0).Rows.Count = 0 Then
            GetFinancialYear(Date.Now)
            sSpoolCode = Microsoft.VisualBasic.Left(mdlSGM.sSelectedSection, 1) + "-" + Format(Date.Now, "MM").ToString + "-" + FinYear.ToString

            Dim daSpoolcode As New SqlDataAdapter("Select * from Spool where SpoolCode = '" & sSpoolCode & "'", sConstr)
            Dim dsSpoolCode As New DataSet
            daSpoolcode.Fill(dsSpoolCode)

            nRowcount = dsSpoolCode.Tables(0).Rows.Count + 1

            sSpoolId = sSpoolCode + "-" + nRowcount.ToString.PadLeft(6, "0")
            frmSave.tbSpoolNo.Text = sSpoolId

            Dim daInsSpoolHdr As New SqlDataAdapter("Insert Into Spool(ID,CreatedBy,CreatedDate,ModuleName,SpoolID,SpoolDate,Department,Shift,UserName,BoxCount,Quantity,IsUpdated,SpoolCode,DeviceId) Values ('" & System.Guid.NewGuid.ToString() & _
                                                    "','" & mdlSGM.sUserName & "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','MCD','" & sSpoolId & _
                                                    "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','" & mdlSGM.sSelectedSection & "','" & mdlSGM.sShiftCode & _
                                                    "','" & mdlSGM.sUserName & "','0','0','0','" & sSpoolCode & "','" & mdlSGM.strIPAddress & "')", sConstr)
            Dim dsInsSpoolHdr As New DataSet
            daInsSpoolHdr.Fill(dsInsSpoolHdr)
            dsInsSpoolHdr.AcceptChanges()
        ElseIf dsSelSpoolHdr.Tables(0).Rows.Count = 1 Then
            sSpoolId = dsSelSpoolHdr.Tables(0).Rows(0).Item("SpoolId").ToString
            frmSave.tbSpoolNo.Text = sSpoolId
        Else
            MsgBox("ERROR : Multi Spool Is Active")
            Exit Sub
        End If

        frmScanning.Show()
        frmScanning.tbBarcode.Focus()
        Me.Hide()

        

    End Sub

    Private Sub cbExitSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExitSelection.Click
        Dim daSelSpoolDtl As New SqlDataAdapter("Select * from SpoolDetails Where SpoolHID = '" & sSpoolId & _
                                                "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)

        If dsSelSpoolDtl.Tables(0).Rows.Count <> 0 Then
            nYesNo = MsgBox("R U Sure U Want to Exit Without saving the scanned boxes.", MsgBoxStyle.YesNo)

            If nYesNo = 6 Then
                DeleteSpoolDtl()
                DeleteSpoolHdr()
            Else
                Exit Sub
            End If
        End If
        myOptimizerStrUserAuthentication.sUserName = mdlSGM.sUserName
        myOptimizerStrUserAuthentication.sLogoutTime = Format(Date.Now, "dd-MMM-yyyy::hh:mm:ss-tt")
        myOptimizerStrUserAuthentication.sIsActive = "0"
        If myccOptimizerComponentUser.UpdateUserAuthentication(myOptimizerStrUserAuthentication) = True Then
            Me.DialogResult = DialogResult.OK
        End If
        Application.Exit()
    End Sub

    Private Sub LoadComboItems()

        cbxShift.DataSource = myccMCD.LoadShift()
        cbxShift.DisplayMember = "ShiftInfo"
        cbxShift.ValueMember = "SHIFTCODE"

        'cbxSection.DataSource = myccMCD.LoadSection
        'cbxSection.DisplayMember = "SectionInfo"

        cbxMachine.DataSource = myccMCD.LoadMachine()
        cbxMachine.DisplayMember = "LocationName"
        cbxMachine.ValueMember = "LocationCode"

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

    Private Sub DeleteSpoolHdr()
        Dim daSelSpoolHdr As New SqlDataAdapter("Delete from Spool Where SpoolID = '" & sSpoolId & "'", sConstr)
        Dim dsSelSpoolHdr As New DataSet
        daSelSpoolHdr.Fill(dsSelSpoolHdr)
        dsSelSpoolHdr.AcceptChanges()
    End Sub

    Private Sub DeleteSpoolDtl()
        Dim daSelSpoolDtl As New SqlDataAdapter("Delete from SpoolDetails Where SpoolHID = '" & sSpoolId & "'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)
        dsSelSpoolDtl.AcceptChanges()
    End Sub


#End Region
End Class