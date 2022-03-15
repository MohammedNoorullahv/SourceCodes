Imports System.Environment
Module mdlSGM

    Public sSelectOption As String
    Public dFromDate, dToDate As Date
    Public sSelectedCustomer, sSelectedArticle, sSelectedCodification As String
    Public strIPAddress, strHostName As String

    'Public AppDataAccess As String = GetFolderPath(SpecialFolder.ApplicationData)

    Class Sample
        Public Shared Sub Main()
            ' Get the path to the Application Data folder
            Dim AppDataAccess As String = GetFolderPath(SpecialFolder.ApplicationData)

            ' Display the path
            'Console.WriteLine("App Data Folder Path: " & appData)
        End Sub
    End Class

    Public Sub ScrrenShotCapture()
        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        bounds = Screen.AllScreens(0).Bounds 'screen.PrimaryScreen.Bounds
        screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        frmKHLIOuterAndInnerScanning.PictureBox1.Image = screenshot
    End Sub

End Module
