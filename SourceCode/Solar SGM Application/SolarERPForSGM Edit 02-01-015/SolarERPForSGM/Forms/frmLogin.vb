Option Explicit On

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing
Imports Microsoft.Win32

Public Class frmLoginForm

    Inherits System.Windows.Forms.Form
    Public sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup
    Dim sCnn As New SqlConnection(sConstr)

    Dim myTimeFirst As New OptimizerComponent
    Dim myTFEmployeeStructure As New StrEmployee

    Dim myOptimizerComponentUser As New OptimizerComponent
    Dim myOptimizerStrUserAuthentication As StrUserAuthentication

    Dim nRowCount As Integer

    Dim sUserName As String

    Dim dsLogin As New DataSet

    


    Dim process As System.Diagnostics.Process()

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOK.Click
        'Try
        'If sCnn.Database <> "Optimizer" Then
        '    MsgBox("Incorrect Database, U Can't Login'", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        'process = System.Diagnostics.Process.GetProcessesByName("Optimizer - ERP for KHLI")
        'If process.Length = 1 Then
        '    MsgBox("Optimizer is Already Running in your System," + vbCrLf & _
        '           "Kindly Continue with Earlier Version" + vbCrLf & _
        '           "or Log Out from the Previuos Version / User to Continue work in Current Version / User", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If
        'Dim regKey As RegistryKey
        'regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
        'regKey.CreateSubKey("MyApp")
        'regKey.Close()

        ''Dim regKey As RegistryKey
        'Dim ver As Decimal
        'regKey = Registry.LocalMachine.OpenSubKey("Software\MyApp", True)
        'regKey.SetValue("AppName", "MyRegApp")
        'ver = regKey.GetValue("Version", 0.0)
        'If ver < 1.1 Then
        '    regKey.SetValue("Version", 1.1)
        'End If
        'regKey.Close()

        If Trim(tbUserName.Text) = "" Then
            'ErrorProvider.SetError(tbUserName, "Cannot Login Without User Name")
            Form1.Show()
            Form1.cbInvoiceGeneration.Visible = False
            Me.Hide()
            Exit Sub
        End If

        If Trim(tbPassword.Text) = "" Then
            ErrorProvider.SetError(tbPassword, "Cannot Login Without Password")
            Exit Sub
        End If

        myTFEmployeeStructure.userName = Trim(tbUserName.Text)

        myTimeFirst.SelectEmployee(myTFEmployeeStructure.userName)

        If mdlSGM.sUserName = "" Then
            MsgBox("Invalid Login Name", MsgBoxStyle.Critical)
            Exit Sub
        Else

            If Trim(sUserPassword) = Trim(tbPassword.Text) Then
                'LogoPictureBox.Visible = False
                PictureBox4.Visible = True
                PictureBox4.BringToFront()

                LoginCheck()
                'frmMDI.Show()
                'frmMDI.BringToFront()
                'Me.Hide()

                ''mdlSGM.LoadYear()
                'frmMDI.WindowState = FormWindowState.Maximized


            Else
                MsgBox("Invalid Password", MsgBoxStyle.Critical)
                Exit Sub
            End If

        End If



        'Catch Exp As Exception
        '    HandleException(Me.Name, Exp)
        'End Try
    End Sub


    Private Sub LoginCheck()
        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
        'Dim sIPAddress As String
        'sIPAddress = h.AddressList.GetValue(0).ToString()

        myOptimizerStrUserAuthentication.sUserName = mdlSGM.sUserName
        myOptimizerStrUserAuthentication.sIPAddress = h.AddressList.GetValue(0).ToString() 'sIPAddress
        myOptimizerStrUserAuthentication.sSystemName = sCnn.WorkstationId
        myOptimizerStrUserAuthentication.sServer = sCnn.Database
        myOptimizerStrUserAuthentication.sLoginTime = Format(Date.Now, "dd-MMM-yyyy::hh:mm:ss-tt")
        myOptimizerStrUserAuthentication.sLogoutTime = ""
        myOptimizerStrUserAuthentication.sIsActive = "1"
        myOptimizerStrUserAuthentication.sVersion = lblVersionInfo.Text

        sSystemName = sCnn.WorkstationId

        myOptimizerComponentUser.CheckUserAlredyLogin(myOptimizerStrUserAuthentication)
        If sLoggedin <> "Y" Then
            'myOptimizerComponentUser.CheckIPAddress(myOptimizerStrUserAuthentication)
        End If



        If sLoggedin = "Y" Then
            sPermissionGranted = "N"
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
            sPermissionGranted = "Y"
            If myOptimizerComponentUser.InsertUserAuthentication(myOptimizerStrUserAuthentication) = True Then
                Me.DialogResult = DialogResult.OK
            End If


            Form1.Show() : Form1.BringToFront()
            Form1.BringToFront()
            Form1.cbInvoiceGeneration.Visible = True
            Me.Hide()


            Form1.WindowState = FormWindowState.Normal



        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCancel.Click
        Me.Close()
    End Sub

    Private Sub frmLoginForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        lblTime.Text = Format(Date.Now, "dd-MMMM-yyyy::hh:mm:ss-tt")



        Timer1.Enabled = True
        Timer1.Interval = 1000
        tbDatabase.Text = sCnn.Database

        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
        myOptimizerStrUserAuthentication.sIPAddress = h.AddressList.GetValue(0).ToString() 'sIPAddress
        myOptimizerStrUserAuthentication.sSystemName = sCnn.WorkstationId

        sSystemName = sCnn.WorkstationId

        mdlSGM.strSystemName = sCnn.WorkstationId + "-" + h.AddressList.GetValue(0).ToString()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = Format(Date.Now, "dd-MMMM-yyyy::hh:mm:ss-tt")
    End Sub




    Private Sub cblogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
End Class
