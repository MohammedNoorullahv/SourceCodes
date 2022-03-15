Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing

'Imports System

Public Class frmmdiSolarERP

    Dim Con As String = Global.Solar_ERP.My.Settings.SolarERPConnectionString
    Dim sCon As New SqlConnection(Con)


    Public cfrmReport As New frmReport

    Public sUserPic() As Byte
    Public sUserStream As New MemoryStream

    ''Reference Forms''
    ''Reference Forms''

    ''ERP Forms''
    ''ERP Forms''

    ''CRM Forms''
    ''CRM Forms''

    ''PPC Forms''
    ''PPC Forms''

    ''SCM Forms''
    ''SCM Forms''

    ''HRM Forms''

    ''HRM Forms''

    ''MRP Forms''
    ''MRP Forms''

    ''Current Form Events''
    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
    End Sub

    Private Sub frmmdiSolarERP_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        LoadUserModuleStatus()
    End Sub

    Private Sub frmmdiSolarERP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DsUnreadMessages.ERPMessageHeader' table. You can move, or remove it, as needed.
        'Me.ERPMessageHeaderTableAdapter.Fill(Me.DsUnreadMessages.ERPMessageHeader, mdlSolarERP.nUserName)

        LoadUserDetails()

        ''ParentChild Relation to all Child Forms''
        cfrmReport.MdiParent = Me
        ''ParentChild Relation to all Child Forms''

        LoadPlMain()

        Timer1.Enabled = True
        Timer1.Interval = 1000

        tbdatasource.Text = sCon.DataSource
        tbDatabase.Text = sCon.Database
        tbSystem.Text = sCon.WorkstationId

        ''Coder for I.P Address of a system''

        'To get local IP address
        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
        '

        ''And in the area for the text box:

        tbIPAddress.Text = h.AddressList.GetValue(0).ToString

        ''Coder for I.P Address of a system''

        mdlSolarERP.sLoginCompleted = "Y"
    End Sub
    ''Current Form Events''

    ''Events referring to Others Forms for Loading, from Current Form''

    Private Sub cbERP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbERP.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModuleERP = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If
        ToolsSend2Back()

    End Sub


    Private Sub cbCRM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCRM.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModuleCRM = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Timer1.Enabled = False
        ToolsSend2Back()
        

    End Sub

    Private Sub cbPPC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPPC.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModulePPC = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Timer1.Enabled = False
        ToolsSend2Back()

    End Sub

    Private Sub cbSCM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSCM.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModuleSCM = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Timer1.Enabled = False
        ToolsSend2Back()

    End Sub

    Private Sub cbFRM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFRM.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModuleFRM = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Timer1.Enabled = False
        ToolsSend2Back()

    End Sub

    Private Sub cbHRM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbHRM.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModuleHRM = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Timer1.Enabled = False
        ToolsSend2Back()

    End Sub

    Private Sub cbMRP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMRP.Click
        If mdlSolarERP.sUserType <> "A" Then
            If sModuleMRP = "N" Then
                MsgBox("Access Restricted", MsgBoxStyle.Critical)
                Exit Sub
            End If
        End If

        Timer1.Enabled = False
        ToolsSend2Back()

    End Sub
    ''Events referring to Others Forms for Loading, from Current Form''


    ''Functions''
    Public Sub ToolsSend2Back()
        plMain.Visible = False
        plHeader.Visible = False
        plInfo.Visible = False
    End Sub

    Public Sub ToolsBring2Front()
        plMain.Visible = True
        plHeader.Visible = True
        plInfo.Visible = True
    End Sub

    Public Sub LoadPlMain()
        plMain.Visible = True
        plMain.BringToFront()
        plMain.Location = New Point(0, 123)
        plMain.Width = 1016
        plMain.Height = 603
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = Format(Date.Now, "hh:mm:ss tt")
    End Sub

    Public Sub GetTime()
        'Try
        Dim TimeInString As String = ""
        Dim hour As Integer = DateTime.Now.Hour
        Dim min As Integer = DateTime.Now.Minute
        Dim sec As Integer = DateTime.Now.Second

        If hour > 12 Then
            hour = hour - 12
            TimeInString = hour.ToString + " : " + min.ToString + " : " + sec.ToString + " - PM"
            If hour = 0 Then
                TimeInString = hour.ToString + " : " + min.ToString + " : " + sec.ToString + " - AM"
            End If
        ElseIf hour = 12 Then
            TimeInString = hour.ToString + " : " + min.ToString + " : " + sec.ToString + " - PM"
        Else
            TimeInString = hour.ToString + " : " + min.ToString + " : " + sec.ToString + " - AM"
        End If
        lblTime.Text = TimeInString
        'Catch Exp As Exception
        '    HandleException(Me.Name, Exp)
        'End Try
    End Sub


    Private Sub LoadUserDetails()
        Try
            lblUnitType.Text = mdlSolarERP.sUnitType
            lblDate.Text = Format(Date.Today, "dd-MMMM-yyyy")
            lblUserName.Text = mdlSolarERP.sUserName
            lblUserDesignation.Text = mdlSolarERP.sUserDesignation : lblYear.Text = mdlSolarERP.sCurrentYear


            
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub
    Dim nRowCount As Integer
    Dim sMenuModule As String
    Dim sModuleCRM, sModuleFRM, sModuleHRM, sModuleMRP, sModulePPC, sModuleSCM, sModuleERP As String
    Private Sub LoadUserModuleStatus()
        Try
            Dim daSelUserModuleStatus As New SqlDataAdapter("Select * from ERPUserModuleStatus Where FKUser = '" & mdlSolarERP.nUserName & "'", Con)
            Dim dsSelUserModuleStatus As New DataSet
            daSelUserModuleStatus.Fill(dsSelUserModuleStatus)

            nRowCount = dsSelUserModuleStatus.Tables(0).Rows.Count

            If nRowCount = 0 Then
                sModuleCRM = "N"
                sModuleFRM = "N"
                sModuleHRM = "N"
                sModuleMRP = "N"
                sModulePPC = "N"
                sModuleSCM = "N"
                sModuleERP = "N"
            Else
                Dim i As Integer = 0
                For i = 0 To nRowCount - 1
                    sMenuModule = dsSelUserModuleStatus.Tables(0).Rows(i).Item("Modules")

                    Select Case sMenuModule
                        Case "CRM"
                            sModuleCRM = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                        Case "FRM"
                            sModuleFRM = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                        Case "HRM"
                            sModuleHRM = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                        Case "MRP"
                            sModuleMRP = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                        Case "PPC"
                            sModulePPC = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                        Case "SCM"
                            sModuleSCM = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                        Case "ERP"
                            sModuleERP = dsSelUserModuleStatus.Tables(0).Rows(i).Item("ModuleEntry")
                    End Select
                Next
            End If
            
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try

    End Sub
    ''Functions''


    ''Coding of Analog Clock''
    '' '' ''Private Sub plClock_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles plClock.Paint
    '' '' ''    'LoadClock()

    '' '' ''    Dim g As Graphics = e.Graphics
    '' '' ''    Dim myPen As New Pen(Brushes.Tomato, 1)
    '' '' ''    Dim myBigPen As New Pen(Brushes.White, 4)

    '' '' ''    g.DrawArc(myPen, Integer.Parse(plClock.Width / 2) - 200, Integer.Parse(plClock.Height / 2) - 200, 400, 400, 0, 360)

    '' '' ''    g.DrawString("3", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 170, Integer.Parse(plClock.Height / 2) - 20)
    '' '' ''    g.DrawString("2", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 147, Integer.Parse(plClock.Height / 2) - 30 - 80)
    '' '' ''    g.DrawString("1", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 80, Integer.Parse(plClock.Height / 2) - 35 - 138)
    '' '' ''    g.DrawString("12", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 20, Integer.Parse(plClock.Height / 2) - 20 - 180)
    '' '' ''    g.DrawString("11", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 114, Integer.Parse(plClock.Height / 2) - 30 - 138)
    '' '' ''    g.DrawString("10", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 177, Integer.Parse(plClock.Height / 2) - 20 - 80)
    '' '' ''    g.DrawString("9", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 190, Integer.Parse(plClock.Height / 2) - 20)
    '' '' ''    g.DrawString("8", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 177, Integer.Parse(plClock.Height / 2) - 20 + 80)
    '' '' ''    g.DrawString("7", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 114, Integer.Parse(plClock.Height / 2) - 20 + 150)
    '' '' ''    g.DrawString("6", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 20, Integer.Parse(plClock.Height / 2) - 20 + 180)
    '' '' ''    g.DrawString("5", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 20 + 94, Integer.Parse(plClock.Height / 2) - 20 + 158)
    '' '' ''    g.DrawString("4", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 147, Integer.Parse(plClock.Height / 2) - 30 + 105)


    '' '' ''    myxAngel = ((90 - (6 * mySec)) * Math.PI) / 180
    '' '' ''    myxLength = Math.Cos(myxAngel) * 160
    '' '' ''    myyAngel = ((90 - (6 * mySec)) * Math.PI) / 180
    '' '' ''    myyLength = Math.Sin(myyAngel) * 160

    '' '' ''    g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxLength), Integer.Parse(plClock.Height / 2) - CInt(myyLength))

    '' '' ''    If FirstDamnedTick = True Then
    '' '' ''        myxHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''        myxHourLength = Math.Cos(myxHourAngel) * 90
    '' '' ''        myyHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''        myyHourLength = Math.Sin(myyHourAngel) * 90
    '' '' ''        g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxHourLength), Integer.Parse(plClock.Height / 2) - CInt(myyHourLength))
    '' '' ''        FirstDamnedTick = False
    '' '' ''    End If

    '' '' ''    If MinTick = True Then
    '' '' ''        myxMinAngel = ((90 - (6 * myMin)) * Math.PI) / 180
    '' '' ''        myxMinLength = Math.Cos(myxMinAngel) * 140
    '' '' ''        myyMinAngel = ((90 - (6 * myMin)) * Math.PI) / 180
    '' '' ''        myyMinLength = Math.Sin(myyMinAngel) * 140
    '' '' ''        g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxMinLength), Integer.Parse(plClock.Height / 2) - CInt(myyMinLength))
    '' '' ''        MinTick = False
    '' '' ''    End If

    '' '' ''    If HourTick = True Then
    '' '' ''        myxHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''        myxHourLength = Math.Cos(myxHourAngel) * 90
    '' '' ''        myyHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''        myyHourLength = Math.Sin(myyHourAngel) * 90
    '' '' ''        g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxHourLength), Integer.Parse(plClock.Height / 2) - CInt(myyHourLength))
    '' '' ''        HourTick = False
    '' '' ''    End If

    '' '' ''    g.DrawLine(myBigPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxMinLength), Integer.Parse(plClock.Height / 2) - CInt(myyMinLength))
    '' '' ''    g.DrawLine(myBigPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxHourLength), Integer.Parse(plClock.Height / 2) - CInt(myyHourLength))

    '' '' ''End Sub

    '' '' ''Private Sub LoadClock()
    '' '' ''    'Dim g As Graphics ' = e.Graphics
    '' '' ''    'Dim myPen As New Pen(Brushes.Tomato, 1)
    '' '' ''    'Dim myBigPen As New Pen(Brushes.White, 4)

    '' '' ''    'g.DrawArc(myPen, Integer.Parse(plClock.Width / 2) - 200, Integer.Parse(plClock.Height / 2) - 200, 400, 400, 0, 360)

    '' '' ''    'g.DrawString("3", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 170, Integer.Parse(plClock.Height / 2) - 20)
    '' '' ''    'g.DrawString("2", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 147, Integer.Parse(plClock.Height / 2) - 30 - 80)
    '' '' ''    'g.DrawString("1", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 80, Integer.Parse(plClock.Height / 2) - 35 - 138)
    '' '' ''    'g.DrawString("12", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 20, Integer.Parse(plClock.Height / 2) - 20 - 180)
    '' '' ''    'g.DrawString("11", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 114, Integer.Parse(plClock.Height / 2) - 30 - 138)
    '' '' ''    'g.DrawString("10", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 177, Integer.Parse(plClock.Height / 2) - 20 - 80)
    '' '' ''    'g.DrawString("9", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 190, Integer.Parse(plClock.Height / 2) - 20)
    '' '' ''    'g.DrawString("8", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 177, Integer.Parse(plClock.Height / 2) - 20 + 80)
    '' '' ''    'g.DrawString("7", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 114, Integer.Parse(plClock.Height / 2) - 20 + 150)
    '' '' ''    'g.DrawString("6", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 20, Integer.Parse(plClock.Height / 2) - 20 + 180)
    '' '' ''    'g.DrawString("5", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) - 20 + 94, Integer.Parse(plClock.Height / 2) - 20 + 158)
    '' '' ''    'g.DrawString("4", Label1.Font, Brushes.Tomato, Integer.Parse(plClock.Width / 2) + 147, Integer.Parse(plClock.Height / 2) - 30 + 105)


    '' '' ''    'myxAngel = ((90 - (6 * mySec)) * Math.PI) / 180
    '' '' ''    'myxLength = Math.Cos(myxAngel) * 160
    '' '' ''    'myyAngel = ((90 - (6 * mySec)) * Math.PI) / 180
    '' '' ''    'myyLength = Math.Sin(myyAngel) * 160

    '' '' ''    'g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxLength), Integer.Parse(plClock.Height / 2) - CInt(myyLength))

    '' '' ''    'If FirstDamnedTick = True Then
    '' '' ''    '    myxHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''    '    myxHourLength = Math.Cos(myxHourAngel) * 90
    '' '' ''    '    myyHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''    '    myyHourLength = Math.Sin(myyHourAngel) * 90
    '' '' ''    '    g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxHourLength), Integer.Parse(plClock.Height / 2) - CInt(myyHourLength))
    '' '' ''    '    FirstDamnedTick = False
    '' '' ''    'End If

    '' '' ''    'If MinTick = True Then
    '' '' ''    '    myxMinAngel = ((90 - (6 * myMin)) * Math.PI) / 180
    '' '' ''    '    myxMinLength = Math.Cos(myxMinAngel) * 140
    '' '' ''    '    myyMinAngel = ((90 - (6 * myMin)) * Math.PI) / 180
    '' '' ''    '    myyMinLength = Math.Sin(myyMinAngel) * 140
    '' '' ''    '    g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxMinLength), Integer.Parse(plClock.Height / 2) - CInt(myyMinLength))
    '' '' ''    '    MinTick = False
    '' '' ''    'End If

    '' '' ''    'If HourTick = True Then
    '' '' ''    '    myxHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''    '    myxHourLength = Math.Cos(myxHourAngel) * 90
    '' '' ''    '    myyHourAngel = ((90 - ((6 * (myMin - (myMin Mod (12))) / 12) + 30 * myHour)) * Math.PI) / 180
    '' '' ''    '    myyHourLength = Math.Sin(myyHourAngel) * 90
    '' '' ''    '    g.DrawLine(myPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxHourLength), Integer.Parse(plClock.Height / 2) - CInt(myyHourLength))
    '' '' ''    '    HourTick = False
    '' '' ''    'End If

    '' '' ''    'g.DrawLine(myBigPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxMinLength), Integer.Parse(plClock.Height / 2) - CInt(myyMinLength))
    '' '' ''    'g.DrawLine(myBigPen, Integer.Parse(plClock.Width / 2), Integer.Parse(plClock.Height / 2), Integer.Parse(plClock.Width / 2) + CInt(myxHourLength), Integer.Parse(plClock.Height / 2) - CInt(myyHourLength))

    '' '' ''End Sub

    '' '' ''Private Sub tmrSec_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrSec.Tick
    '' '' ''    mySec = mySec + 1
    '' '' ''    lblSec.Text = mySec
    '' '' ''    plClock.Refresh()

    '' '' ''    If mySec > 59 Then
    '' '' ''        mySec = 0
    '' '' ''        lblSec.Text = 0
    '' '' ''        MinTick = True
    '' '' ''        If FirstMin = True Then
    '' '' ''            myMin = myMin + 1
    '' '' ''            lblMin.Text = myMin
    '' '' ''            tmrMin.Enabled = True
    '' '' ''            FirstMin = False
    '' '' ''        End If
    '' '' ''    End If

    '' '' ''End Sub

    '' '' ''Private Sub tmrMin_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrMin.Tick
    '' '' ''    myMin = myMin + 1
    '' '' ''    lblMin.Text = myMin

    '' '' ''    If myMin Mod (12) = 0 Then
    '' '' ''        HourTick = True
    '' '' ''    End If

    '' '' ''    If myMin > 59 Then
    '' '' ''        myMin = 0
    '' '' ''        lblMin.Text = 0
    '' '' ''        If FirstHour = True Then
    '' '' ''            myHour = myHour + 1
    '' '' ''            lblHour.Text = myHour
    '' '' ''            tmrHour.Enabled = True
    '' '' ''            FirstHour = False
    '' '' ''        End If
    '' '' ''        If myHour > 12 Then
    '' '' ''            myHour = myHour Mod 12
    '' '' ''            lblHour.Text = myHour
    '' '' ''        End If
    '' '' ''    End If

    '' '' ''End Sub

    '' '' ''Private Sub tmrHour_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrHour.Tick
    '' '' ''    myHour = myHour + 1
    '' '' ''    lblHour.Text = myHour

    '' '' ''    If myHour > 12 Then
    '' '' ''        myHour = 1
    '' '' ''        lblHour.Text = 1
    '' '' ''    End If

    '' '' ''End Sub

    '''''''''Code For Converting String to Date'''''''''
    'dp.Value = Convert.ToDateTime(tb.Text)
    '''''''''Code For Converting String to Date'''''''''


    Private Sub plERP_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles plERP.MouseMove
        pl1.Visible = False
    End Sub

    Private Sub plERP_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles plERP.Paint

    End Sub

    Private Sub cbCRM_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCRM.MouseHover
        lblImageCRM.BringToFront()
    End Sub

    Private Sub cbCRM_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbCRM.MouseMove
        pl1.Visible = True
        lblCRM.BringToFront()

    End Sub

    Private Sub cbPPC_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPPC.MouseHover
        lblImagePPC.BringToFront()
    End Sub

    Private Sub cbPPC_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbPPC.MouseMove
        pl1.Visible = True
        lblPPC.BringToFront()
    End Sub

    Private Sub cbSCM_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbSCM.MouseHover
        lblImageSCM.BringToFront()
    End Sub

    Private Sub cbSCM_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbSCM.MouseMove
        pl1.Visible = True
        lblSCM.BringToFront()

    End Sub

    Private Sub cbERP_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbERP.MouseHover
        lblImageERP.BringToFront()
    End Sub

    Private Sub cbERP_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbERP.MouseMove
        pl1.Visible = True
        lblERP.BringToFront()
    End Sub

    Private Sub cbFRM_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFRM.MouseHover
        lblImageFRM.BringToFront()
    End Sub

    Private Sub cbFRM_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbFRM.MouseMove
        pl1.Visible = True
        lblImageFRM.BringToFront()
    End Sub

    Private Sub cbHRM_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbHRM.MouseHover
        lblImageHRM.BringToFront()
    End Sub

    Private Sub cbHRM_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbHRM.MouseMove
        pl1.Visible = True
        lblFRM.BringToFront()
    End Sub

    Private Sub cbMRP_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbMRP.MouseHover
        lblImageMRP.BringToFront()
    End Sub

    Private Sub cbMRP_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbMRP.MouseMove
        pl1.Visible = True
        lblMRP.BringToFront()
    End Sub


    
    Private Sub cbComposeMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbComposeMessage.Click
        Try
            UpdateStatus()
            Exit Sub
            cbSendMessage.Enabled = True
            GeneratePKID()


            LoadAvailableUsers()
            plMessage.Visible = True
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try


    End Sub

    Dim nFKOrderDetail, nOrderQuantity, nFKJobCard As Integer
    Dim nMouldQty, nMouldBal, nFinishQty, nFinishBal, nPackQty, nPackBal, nDispQty, nDispBal As Integer
    Private Sub UpdateStatus()
        Try
            Dim daSelOrdDtl As New SqlDataAdapter("Select * from CRMCustomerOrderDetail Where FKArticle > '0' Order by PKID", Con)
            Dim dsSelOrdDtl As New DataSet
            daSelOrdDtl.Fill(dsSelOrdDtl)

            nRowCount = dsSelOrdDtl.Tables(0).Rows.Count

            Dim i As Integer = 0

            For i = 0 To nRowCount - 1
                If i = 1001 Then
                    MsgBox("1000")
                End If
                nFKOrderDetail = dsSelOrdDtl.Tables(0).Rows(i).Item("PKID")
                nOrderQuantity = dsSelOrdDtl.Tables(0).Rows(i).Item("TotalQuantity")
                Dim daSelJobCard As New SqlDataAdapter("Select * from CRMJobCard where FKCustomerOrderDetail = '" & nFKOrderDetail & "'", Con)
                Dim dsSelJobCard As New DataSet
                daSelJobCard.Fill(dsSelJobCard)
                If dsSelJobCard.Tables(0).Rows.Count = 0 Then
                    GoTo Aa
                End If
                'nFKJobCard = dsSelJobCard.Tables(0).Rows(0).Item("PKID")

                Dim daSelMouldQty As New SqlDataAdapter("Select IsNull(Sum(TotalQuantity),0) from PPCDailyProduction Where Section = '1' And FKProductionType = '1' And FKJobCard in (Select PKID From CRMJobCard Where FKCustomerOrderDetail = '" & nFKOrderDetail & "')", Con)
                Dim dsSelMouldQty As New DataSet
                daSelMouldQty.Fill(dsSelMouldQty)

                nMouldQty = dsSelMouldQty.Tables(0).Rows(0).Item(0)

                nMouldBal = nOrderQuantity - nMouldQty

                Dim daSelFinishQty As New SqlDataAdapter("Select IsNull(Sum(TotalQuantity),0) from PPCDailyProduction Where Section = '2' And FKProductionType = '1' And FKJobCard in (Select PKID From CRMJobCard Where FKCustomerOrderDetail = '" & nFKOrderDetail & "')", Con)
                Dim dsSelFinishQty As New DataSet
                daSelFinishQty.Fill(dsSelFinishQty)

                nFinishQty = dsSelFinishQty.Tables(0).Rows(0).Item(0)

                nFinishBal = nOrderQuantity - nFinishQty

                Dim daSelPackQty As New SqlDataAdapter("Select IsNull(Sum(TotalQuantity),0) from PPCDailyProduction Where Section = '2' And PackingVerified  = 'Y' And FKProductionType = '1' And FKJobCard in (Select PKID From CRMJobCard Where FKCustomerOrderDetail = '" & nFKOrderDetail & "')", Con)
                Dim dsSelPackQty As New DataSet
                daSelPackQty.Fill(dsSelPackQty)

                nPackQty = dsSelFinishQty.Tables(0).Rows(0).Item(0)

                nPackBal = nOrderQuantity - nPackQty

                Dim daSelDispatchQty As New SqlDataAdapter("Select IsNull(Sum(TotalQuantity),0) from PPCDailyProduction Where Section = '2' And Dispatched = 'Y' And FKProductionType = '1' And FKJobCard in (Select PKID From CRMJobCard Where FKCustomerOrderDetail = '" & nFKOrderDetail & "')", Con)
                Dim dsSelDispatchQty As New DataSet
                daSelDispatchQty.Fill(dsSelDispatchQty)

                nDispQty = dsSelDispatchQty.Tables(0).Rows(0).Item(0)

                nDispBal = nOrderQuantity - nDispQty

                Dim daUpdOrderDetail As New SqlDataAdapter("Update CRMCustomerOrderDetail Set MouldingQuantity = '" & nMouldQty & _
                                                           "', MouldingBalance = '" & nMouldBal & _
                                                           "', FinishingQuantity = '" & nFinishQty & _
                                                           "', FinishingBalance = '" & nFinishBal & _
                                                           "', PackingQuantity = '" & nPackQty & _
                                                           "', PackingBalance = '" & nPackBal & _
                                                           "', DispatchQuantity = '" & nDispQty & _
                                                           "', DispatchBalance =  '" & nDispBal & _
                                                           "' Where PKID = '" & nFKOrderDetail & "'", Con)


                Dim dsUPdOrderDetail As New DataSet
                daUpdOrderDetail.Fill(dsUPdOrderDetail)
                dsUPdOrderDetail.AcceptChanges()

Aa:
            Next
            MsgBox("Completed Successfully")
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
    Private Sub LoadAvailableUsers()
        Try

            Dim dsSelUsers As New DataSet
            Dim daSelUsers As New SqlDataAdapter("Select a.PKID,'' As [Select],c.FirmName As Firms,a.UserName,b.Description As Designation From ERPUserHeader a, ERPLookUpMaster b, ERPFirms c Where a.FKDesignation = b.PKID And a.FKFirm = c.PKID And a.PKID <> '" & mdlSolarERP.nUserName & _
                                                   "' Order By a.PKID", Con)
            daSelUsers.Fill(dsSelUsers)

            Dim dvmSelUsers As New DataViewManager
            dvmSelUsers = New DataViewManager(dsSelUsers)

            Dim dvSelUsers As New DataView
            dvSelUsers = dvmSelUsers.CreateDataView(dsSelUsers.Tables(0))

            
            ''Coding for Loading PO in the Grid''

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbHideMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbHideMessage.Click
        Try

            plMessage.Visible = False
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub GeneratePKID()
        Try
            Dim daSelMessageHeader As New SqlDataAdapter("Select IsNull(Max(PKID),0) From ERPMessageHeader", Con)
            Dim dsSelMessageHeader As New DataSet
            daSelMessageHeader.Fill(dsSelMessageHeader)

            tbMessageNo.Text = dsSelMessageHeader.Tables(0).Rows(0).Item(0) + 1
            nPKIDMessageHdr = dsSelMessageHeader.Tables(0).Rows(0).Item(0) + 1
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nPKIDMessageHdr, nPKIDMessageReceipent As Integer

    Private Sub GenerateReceipentPKID()
        Try
            Dim daSelMessageReceipent As New SqlDataAdapter("Select IsNull(Max(PKID),0) From ERPMessageReceipents", Con)
            Dim dsSelMessageReceipent As New DataSet
            daSelMessageReceipent.Fill(dsSelMessageReceipent)

            nPKIDMessageReceipent = dsSelMessageReceipent.Tables(0).Rows(0).Item(0) + 1
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim ngrdRowNo As Integer
    Dim sReceipentSelected As String

   Dim nFKUser As Integer
    Dim nFKSender As Integer

 
    Private Sub LoadMessages()
        Try
            SelectUnreadMessages()

            DisplayLoadMessage()
            plMessage.Visible = True
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim nUnreadMessageCount As Integer
    Private Sub SelectUnreadMessages()
        Try
            Dim daSelUnreadMessages As New SqlDataAdapter("Select Distinct(a.PKID) From ERPMessageHeader a, ERPMessageReceipentS b Where b.FKMessage = a.PKID And a.CreatedBy = '" & nFKSender & _
                                                          "' And b.FKUser = '" & mdlSolarERP.nUserName & _
                                                          "' And IsViewed = 'N' Order by a.PKID", Con)
            Dim dsSelUnreadMessages As New DataSet
            daSelUnreadMessages.Fill(dsSelUnreadMessages)

            nUnreadMessageCount = dsSelUnreadMessages.Tables(0).Rows.Count
            nPKIDMessageHdr = dsSelUnreadMessages.Tables(0).Rows(0).Item(0)

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub DisplayLoadMessage()
        Try
            Dim daSelMessage As New SqlDataAdapter("Select * from ERPMessageHeader Where PKID = '" & nPKIDMessageHdr & "'", Con)
            Dim dsSelMessage As New DataSet
            daSelMessage.Fill(dsSelMessage)

            tbMessageNo.Text = dsSelMessage.Tables(0).Rows(0).Item("PKID")
            dpDate.Value = dsSelMessage.Tables(0).Rows(0).Item("Date")
            tbSubject.Text = dsSelMessage.Tables(0).Rows(0).Item("Subject")
            tbMessage.Text = dsSelMessage.Tables(0).Rows(0).Item("Message")

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub


    Dim keyascii As Integer
    
    Private Sub tb1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tb1.KeyPress
        keyAscii = AscW(e.KeyChar)
        If keyascii = 13 Then
            If Val(tb1.Text) > 0 Then
                Dim daUpdOrdHeader As New SqlDataAdapter("Update CRMCustomerOrderHeader Set IsClosed = 'Y' Where PKID = '" & Val(tb1.Text) & "'", Con)
                Dim dsUpdOrdHeader As New DataSet
                daUpdOrdHeader.Fill(dsUpdOrdHeader)
                dsUpdOrdHeader.AcceptChanges()

                tb1.Clear()
            End If
        End If

    End Sub

    
    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
