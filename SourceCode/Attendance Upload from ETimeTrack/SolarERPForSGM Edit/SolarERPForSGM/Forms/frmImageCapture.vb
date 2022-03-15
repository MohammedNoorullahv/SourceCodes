Option Explicit On
Option Compare Binary

Imports System.Drawing
Imports System.Windows
Imports System.Windows.Forms

Public Class frmImageCapture
    Inherits System.Windows.Forms.Form

    'Load Webcam Device List
    Private Sub LoadDeviceList()
        On Error Resume Next
        Dim strName As String = Space(100)
        Dim strVer As String = Space(100)
        Dim bReturn As Boolean
        Dim x As Integer = 0

        Do
            bReturn = modWebcam.capGetDriverDescriptionA(x, strName, 100, strVer, 100)
            If bReturn Then lst1.Items.Add(strName.Trim)
            x += 1
            Application.DoEvents()
        Loop Until bReturn = False

    End Sub

    'Open View
    Private Sub OpenPreviewWindow()
        On Error Resume Next

        Dim iHeight As Integer = pview.Height
        Dim iWidth As Integer = pview.Width

        '
        ' Open Preview window in picturebox
        '
        hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE Or WS_CHILD, 0, 0, 640, _
            480, pView.Handle.ToInt32, 0)

        '
        ' Connect to device
        '
        If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then
            '
            'Set the preview scale
            '
            SendMessage(hHwnd, WM_CAP_SET_SCALE, True, 0)

            '
            'Set the preview rate in milliseconds
            '
            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)

            '
            'Start previewing the image from the camera
            '
            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, True, 0)

            '
            ' Resize window to fit in picturebox
            '
            SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, pview.Width, pview.Height, _
                    SWP_NOMOVE Or SWP_NOZORDER)

            cmd1.Enabled = False
            cmd2.Enabled = True
            cmd3.Enabled = True
        Else
            '
            ' Error connecting to device close window
            ' 
            DestroyWindow(hHwnd)

            cmd1.Enabled = True
            cmd2.Enabled = False
            cmd3.Enabled = False

            pview.Image = Nothing
            pview.SizeMode = PictureBoxSizeMode.StretchImage
            pview.BackColor = Color.Black
            pview.BackgroundImage = Nothing
            pview.BackgroundImageLayout = ImageLayout.None
            pview.Refresh()

            pConverted.Image = Nothing
            pConverted.SizeMode = PictureBoxSizeMode.StretchImage
            pConverted.BackColor = Color.Black
            pConverted.BackgroundImage = Nothing
            pConverted.BackgroundImageLayout = ImageLayout.None
            pConverted.Refresh()
        End If
    End Sub

    Private Sub ClosePreviewWindow()
        On Error Resume Next
        '
        ' Disconnect from device
        '
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)

        '
        ' close window
        '

        DestroyWindow(hHwnd)

        cmd1.Enabled = True
        cmd2.Enabled = False
        cmd3.Enabled = False

        pView.Image = Nothing
        pview.SizeMode = PictureBoxSizeMode.StretchImage
        pview.BackColor = Color.Black
        pview.BackgroundImage = Nothing
        pview.BackgroundImageLayout = ImageLayout.None
        pView.Refresh()

        pConverted.Image = Nothing
        pConverted.SizeMode = PictureBoxSizeMode.StretchImage
        pConverted.BackColor = Color.Black
        pConverted.BackgroundImage = Nothing
        pConverted.BackgroundImageLayout = ImageLayout.None
        pConverted.Refresh()
    End Sub

    'If Form Closed
    Private Sub frmImageCapture_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        On Error Resume Next
        'If Played the Stopped
        If cmd2.Enabled Then
            ClosePreviewWindow()
        End If
    End Sub

    'Set Object To Default Value
    Private Sub ClearAllObject()
        On Error Resume Next
        opt1.Checked = False
        opt2.Checked = True
        lst1.Items.Clear()
        lst1.Refresh()

        pView.BackColor = Color.Black
        pView.BackgroundImageLayout = ImageLayout.Stretch
        pView.Image = Nothing
        pView.SizeMode = PictureBoxSizeMode.StretchImage
        pView.Refresh()

        pConverted.Image = Nothing
        pConverted.SizeMode = PictureBoxSizeMode.StretchImage
        pConverted.BackColor = Color.Black
        pConverted.BackgroundImage = Nothing
        pConverted.BackgroundImageLayout = ImageLayout.None
        pConverted.Refresh()

        cmd1.Enabled = True
        cmd2.Enabled = False
        cmd3.Enabled = False
        'Load Device List
        Call LoadDeviceList()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        'Load Object Value TO Default
        ClearAllObject()

        Timer1.Enabled = False
        Timer2.Enabled = False
        lMovements.Text = 0
    End Sub

    Private Sub cmd1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd1.Click
        On Error Resume Next
        'Device
        iDevice = lst1.SelectedIndex

        'Load And Capture Device
        OpenPreviewWindow()

        'Open Conveted Image
        Timer1.Enabled = True

        If chb1.Checked = True Then
            Timer2.Enabled = True
        End If
    End Sub

    Private Sub cmd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd2.Click
        On Error Resume Next
        'Stop Device Capture
        ClosePreviewWindow()
        'Close Conveted Image
        Timer1.Enabled = False
        'Close Controled Movements
        Timer2.Enabled = False

        lMovements.Text = 0
    End Sub

    Private Sub cmd3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd3.Click
        On Error Resume Next

        Dim data As IDataObject
        Dim bmap As Image

        '
        ' Copy image to clipboard
        '
        SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)

        '
        ' Get image from clipboard and convert it to a bitmap
        '
        data = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            bmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Image)
            pView.Image = bmap

            'Stop Device Capture
            ClosePreviewWindow()

            'Set Button
            cmd3.Enabled = False
            cmd2.Enabled = False
            cmd1.Enabled = True

            'Set Save Dialog
            sfdImages.FileName = ""
            sfdImages.Title = "Save Picture"
            sfdImages.Filter = "Bitmap|*.bmp|Jpeg|*.jpg|GIF|*.gif|PNG|*.png"

            'If File Name Not Equal "" then Save The File
            If sfdImages.ShowDialog = DialogResult.OK Then
                Select Case Microsoft.VisualBasic.Right$(sfdImages.FileName, 3)
                    Case Is = "bmp"
                        bmap.Save(sfdImages.FileName, Imaging.ImageFormat.Bmp)
                    Case Is = "jpg"
                        bmap.Save(sfdImages.FileName, Imaging.ImageFormat.Jpeg)
                    Case Is = "gif"
                        bmap.Save(sfdImages.FileName, Imaging.ImageFormat.Gif)
                    Case Is = "png"
                        bmap.Save(sfdImages.FileName, Imaging.ImageFormat.Png)
                End Select
            End If

        End If

        data = Nothing
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If opt1.Checked = True Then
            pConverted.Image = InvertPicturesFromCapturedWindow()
        End If
        If opt2.Checked = True Then
            pConverted.Image = pView.Image
        End If
        If opt3.Checked = True Then
            pConverted.Image = GrayScalePicture()
        End If
        If opt4.Checked = True Then
            pConverted.Image = SephiaRed()
        End If

        Application.DoEvents()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        lMovements.Text = DetectMovement()
        Application.DoEvents()
    End Sub

End Class