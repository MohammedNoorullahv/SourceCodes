Option Explicit On

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing
Imports Microsoft.Win32
Imports System.Data

Public Class frmLogin

    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)


    Dim myccOptimizerComponentUser As New ccOptimizerComponent
    Dim myOptimizerStrUserAuthentication As New StrUserAuthentication
    Dim myTFEmployeeStructure As New StrEmployee
    Dim myTimeFirst As New ccOptimizerComponent

    Dim sSystemName As String


    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        'Dim sBoxNoWithError As String
        'sSpoolId = "M-12-1920-000011"
        'Dim filePath As String = String.Format("D:\" + Trim(sSpoolId) + "--" + DateTime.Today.ToString("dd-MMM-yyyy"))
        ''        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        'Using writer As New StreamWriter(filePath, True)
        '    If File.Exists(filePath) Then

        '        Dim daSelSpoolDtls As New SqlDataAdapter("Select spd.BarCode + ' :: ' + abb.FullName_ As BoxInfo From SpoolDetails As SPD, AbbrevTable As Abb Where spd.FKStatus = Cast(abb.Abbrev_ As Int)  And abb.Group_= 'scanstatus'  And spd.SpoolHID = ' " & sSpoolId & "'", sConstr)
        '        Dim dsSelSpoolDtls As New DataSet
        '        daSelSpoolDtls.Fill(dsSelSpoolDtls)

        '        Dim i As Integer = 0

        '        For i = 0 To dsSelSpoolDtls.Tables(0).Rows.Count - 1
        '            sBoxNoWithError = dsSelSpoolDtls.Tables(0).Rows(i).Item("BoxInfo").ToString
        '            writer.WriteLine(Trim(sBoxNoWithError))
        '        Next

        '    Else
        '        writer.WriteLine("Start Error Log for today")
        '    End If
        'End Using

        Application.Exit()
    End Sub

    Private Sub cbLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLogin.Click
        If Trim(tbUserName.Text) = "" Then
            MsgBox("Cannot Login Without User Name", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Trim(tbPassword.Text) = "" Then
            MsgBox("Cannot Login Without Password", MsgBoxStyle.Critical)
            Exit Sub
        End If

        myTFEmployeeStructure.userName = Trim(tbUserName.Text)

        myTimeFirst.SelectEmployee(myTFEmployeeStructure.userName)

        If Trim(mdlSGM.sUserPassword) = Trim(tbPassword.Text) Then
            LoginCheck()

        Else
            MsgBox("Invalid Password", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If sPermissionGranted = "Y" Then
            frmSelection.Show()
            Me.Hide()




            'LoadComboItems()
            'LoadAccessRights()
            'sSection = ""
        Else
            MsgBox("Invalid Credentials", MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub LoginCheck()
        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)

        myOptimizerStrUserAuthentication.sUserName = mdlSGM.sUserName
        myOptimizerStrUserAuthentication.sIPAddress = h.AddressList.GetValue(0).ToString() 'sIPAddress
        mdlSGM.strIPAddress = h.AddressList.GetValue(0).ToString()
        myOptimizerStrUserAuthentication.sSystemName = sCnn.WorkstationId
        myOptimizerStrUserAuthentication.sServer = sCnn.Database
        myOptimizerStrUserAuthentication.sLoginTime = Format(Date.Now, "dd-MMM-yyyy::hh:mm:ss-tt")
        myOptimizerStrUserAuthentication.sLogoutTime = ""
        myOptimizerStrUserAuthentication.sIsActive = "1"
        myOptimizerStrUserAuthentication.sVersion = ""

        sSystemName = sCnn.WorkstationId


        myccOptimizerComponentUser.CheckUserAlredyLogin(myOptimizerStrUserAuthentication)
        If mdlSGM.sLoggedin <> "Y" Then
            'myOptimizerComponentUser.CheckIPAddress(myOptimizerStrUserAuthentication)
        End If



        If mdlSGM.sLoggedin = "Y" Then
            mdlSGM.sPermissionGranted = "N"
            If sIPAddress = h.AddressList.GetValue(0).ToString() Then
                MsgBox("Another / Same User Already Logged In this System! " & vbCrLf & _
                 "User Name : [ " + sLoggedUser + " ] at System : [ " + sSystemName + " ]" + vbCrLf & _
                 "  You Can't login now! ", MsgBoxStyle.OkOnly)
                Me.Close()
                Exit Sub

            ElseIf sIPAddress = h.AddressList.GetValue(0).ToString() Then
                MsgBox("User Already logged In as  " & vbCrLf & _
                       "user : [ " + tbUserName.Text + " ] at System : [ " + sSystemName + " ]" + vbCrLf & _
                       "Do you want to close the previous login ?", MsgBoxStyle.YesNo)
                Exit Sub


            Else
                MsgBox("User Already Logged In As " & vbCrLf & _
                       "user : [ " + tbUserName.Text + " ] at System : [ " + sSystemName + " ]" + vbCrLf & _
                       "  You Can't login now! ", MsgBoxStyle.OkOnly)
                Me.Close()
                Exit Sub

            End If
        Else
            sPermissionGranted = "N"
            If myccOptimizerComponentUser.InsertUserAuthentication(myOptimizerStrUserAuthentication) = True Then
                Me.DialogResult = DialogResult.OK
            End If
            sPermissionGranted = "Y"


        End If

    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbServerName.Text = sCnn.DataSource
        tbDatabaseName.Text = sCnn.Database
    End Sub
End Class