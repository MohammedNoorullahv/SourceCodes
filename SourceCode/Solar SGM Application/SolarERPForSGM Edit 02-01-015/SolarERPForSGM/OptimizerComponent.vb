Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure StrEmployee

    Dim FKUser As Long
    Dim FKFirm As Long
    Dim FirmName As String
    Dim UnitType As String
    Dim userName As String
    Dim Designation As String
    Dim LoginName As String
    Dim Password As String
    Dim UserType As String

End Structure

Public Structure StrLookUpHeader

    Dim PKID As Long
    Dim Description As String
    Dim TotalDtls As Integer

End Structure

Public Structure StrLookUpMaster

    Dim PKID As Long
    Dim FKLookUpHdr As Long
    Dim Code As String
    Dim Description As String
    Dim ShortDescription As String
    Dim IsDefault As String
    Dim IsDeleted As String
    Dim CreatedBy As Long
    Dim CreatedDt As String
    Dim ModifiedBy As Long
    Dim ModifiedDt As String
    Dim DeletedBy As Long
    Dim DeletedDt As String
    Dim FKUser As Long


End Structure

Public Structure StrERPSeason

    Dim PKID As Integer
    Dim seasoncode As String
    Dim SeasonDesc As String
    Dim StartDate As Date
    Dim Enddate As Date

End Structure

Public Structure StrTORTOGRN
    Dim PKID As Integer
    Dim OrderNo As String
    Dim FKItem As Integer
    Dim TORQuantity As Decimal
    Dim IndentQuantity As Decimal
    Dim POQuantity As Decimal
    Dim GRNQty As Decimal

End Structure

Public Structure StrUserAuthentication
    Dim nPKID As Integer
    Dim sUserName As String
    Dim sIPAddress As String
    Dim sSystemName As String
    Dim sServer As String
    Dim sLoginTime As String
    Dim sLogoutTime As String
    Dim sIsActive As Integer
    Dim sReason As String
    Dim sVersion As String


End Structure

Public Structure strUserInsertHeader
    Dim PKID As Integer
    Dim FKFirm As Integer
    Dim UserName As String
    Dim FKDesgination As Integer
    Dim LoginName As String
    Dim PassWord As String
    Dim UserType As String
    'Dim UserPhoto As Image
    Dim IsDeleted As String
    Dim CreatedBy As Long
    Dim CreatedDt As String
    Dim ModifiedBy As Long
    Dim ModifiedDt As String
    Dim DeletedBy As Long
    Dim DeletedDt As String
End Structure

Public Structure StrUserInsertDetail
    Dim PKID As Integer
    Dim FKUser As Integer
    Dim FKMenu As Integer
    Dim AllFunctions As String
    Dim Adding As String
    Dim Editing As String
    Dim Deleting As String
    Dim Viewing As String
    Dim NoEntry As String
End Structure

#End Region

Public Class OptimizerComponent

#Region "Declarations"


    Inherits System.ComponentModel.Component

    Public sDaEmpcode, sDaEmp, sDaDesig As New SqlDataAdapter
    Public sDaLookUpHdr As New SqlDataAdapter
    Public sDaLookUpMaster, sDaItemMaster As New SqlDataAdapter
    Public sDaUser As New SqlDataAdapter
    Private sErrMsg As String
    Private sErrCode As String

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.Optimizer
    Dim sCnn As New SqlConnection(sConstr)

    Public dsLookUpHdr As New DataSet
    Public dsLookUpMaster As New DataSet
    Public dsUserRight As New DataSet
    Public dsTOR As New DataSet
    Public dsTORTOGRN As New DataSet
    Public sDsUser As New DataSet

    Dim dsdetail, dsLoadDtl, dsLoadDtl1 As New DataSet
    Public dsGroup, dsType, dsSize, dsColour, dsUOM, dsSource, dsCurrency As New DataSet

#End Region

#Region "Properties"

    ReadOnly Property ErrorCode() As String
        Get
            Return sErrCode
        End Get
    End Property

    ReadOnly Property ErrorMessage() As String
        Get
            Return sErrMsg
        End Get
    End Property

#End Region

#Region "Functions"


    Public Function SelectEmployee(ByVal sUserName As String) As StrEmployee

        'Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim dsEmp As New DataSet

        Dim myRec As StrEmployee

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Login"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALL"
        sCmd.Parameters.Add(New SqlParameter("@mLoginName", SqlDbType.VarChar)).Value() = sUserName

        sDaEmp = New SqlDataAdapter(sCmd)
        sDaEmp.Fill(dsEmp, "Employee")

        If dsEmp.Tables("Employee").Rows.Count > 0 Then

            myRec.userName = dsEmp.Tables("Employee").Rows(0).Item("UserId") & ""
            myRec.Password = dsEmp.Tables("Employee").Rows(0).Item("sPassword").ToString
            mdlSGM.sUserPassword = dsEmp.Tables("Employee").Rows(0).Item("sPassword").ToString

            mdlSGM.nUnitId = 0 'dsEmp.Tables("Employee").Rows(0).Item("nFKFirm")
            mdlSGM.sUserName = myRec.userName '1 'dsEmp.Tables("Employee").Rows(0).Item("nUserId")
            mdlSGM.sUnitName = "" 'dsEmp.Tables("Employee").Rows(0).Item("sFirmName")
            mdlSGM.sUnitType = "" 'dsEmp.Tables("Employee").Rows(0).Item("sUnitType")
            'mdlSGM.sUserName = "" 'dsEmp.Tables("Employee").Rows(0).Item("sUserName")
            mdlSGM.sUserDesignation = "" 'dsEmp.Tables("Employee").Rows(0).Item("sDesignation")
            mdlSGM.sUserType = "" 'dsEmp.Tables("Employee").Rows(0).Item("sUserType")
            'mdlTFErp.nEnteringForm = -1
            'mdlKHLIERP.VerifyAccess()
            'mdlKHLIERP.LoadYear()

            'frmMdiKHLIErp.Show()
            'frmMdiKHLIErp.BringToFront()
            'sErrMsg = "No such order record."
            Return myRec
        Else
            'sErrMsg = "No such order record."
            mdlSGM.sUserName = 0
            Return Nothing
            'mdlTFErp.nUserName = 0
        End If

        'dsEmp.Clear()
        dsEmp = Nothing
        sCnn.Close()

    End Function

    Public Function LoadLookUpHdr() As DataTable

        'Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand


        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_LookUpMaster"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELLOOKUPHEADER"


        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "LookUpHdr")
        Return dsLookUpHdr.Tables(0)




        'dsLookUpHdr.Clear()
        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function LoadLookUpMaster(ByVal nFKLookUpHdr As Long) As DataTable

        Dim sCmd As New SqlCommand

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_LookUpMaster"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELLOOKUPMASTER"
        sCmd.Parameters.Add(New SqlParameter("@mFKLookUpHdr", SqlDbType.BigInt)).Value() = nFKLookUpHdr

        sDaLookUpMaster = New SqlDataAdapter(sCmd)
        sDaLookUpMaster.Fill(dsLookUpMaster, "LookUpMaster")
        Return dsLookUpMaster.Tables(0)

        dsLookUpMaster = Nothing
        sCnn.Close()

    End Function

    Public Function SelectLookUpCode(ByVal nFKLookUpHdr As Long, ByVal sLookUPCode As String) As StrLookUpMaster

        'Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim dsCode As New DataSet

        Dim myRec As StrLookUpMaster

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_LookUpMaster"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELCODE"
        sCmd.Parameters.Add(New SqlParameter("@mFKLookUpHdr", SqlDbType.BigInt)).Value() = nFKLookUpHdr
        sCmd.Parameters.Add(New SqlParameter("@mCode", SqlDbType.VarChar)).Value() = sLookUPCode

        sDaLookUpMaster = New SqlDataAdapter(sCmd)
        sDaLookUpMaster.Fill(dsCode, "LookUpMaster")

        If dsCode.Tables("LookUpMaster").Rows.Count > 0 Then

            mdlSGM.sLookUPCode = dsCode.Tables("LookUpMaster").Rows(0).Item("Code")
            Return myRec
        Else

            mdlSGM.sLookUPCode = ""
            Return Nothing

        End If


        dsCode = Nothing
        sCnn.Close()

    End Function

    Public Function SelectLookUpMasterCode(ByVal nFKLookUPMaster As Long, ByVal nFKLookUpHdr As Long, ByVal sLookUPCode As String) As StrLookUpMaster

        'Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim dsCode As New DataSet

        Dim myRec As StrLookUpMaster

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_LookUpMaster"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELLOOKUPMASTERCODE"
        sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.BigInt)).Value() = nFKLookUPMaster
        sCmd.Parameters.Add(New SqlParameter("@mFKLookUpHdr", SqlDbType.BigInt)).Value() = nFKLookUpHdr
        sCmd.Parameters.Add(New SqlParameter("@mCode", SqlDbType.VarChar)).Value() = sLookUPCode

        sDaLookUpMaster = New SqlDataAdapter(sCmd)
        sDaLookUpMaster.Fill(dsCode, "LookUpMaster")

        If dsCode.Tables("LookUpMaster").Rows.Count > 0 Then

            mdlSGM.sLookUPCode = dsCode.Tables("LookUpMaster").Rows(0).Item("Code")
            Return myRec
        Else

            mdlSGM.sLookUPCode = ""
            Return Nothing

        End If


        dsCode = Nothing
        sCnn.Close()

    End Function

    Public Function Insert(ByVal oNV As StrLookUpMaster) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsLookUpMaster As New DataSet

            Dim myRec As StrLookUpMaster

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_LookUpMaster"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERT"
            sCmd.Parameters.Add(New SqlParameter("@mFKLookUpHdr", SqlDbType.BigInt)).Value = oNV.FKLookUpHdr
            sCmd.Parameters.Add(New SqlParameter("@mCode", SqlDbType.VarChar)).Value = oNV.Code
            sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value = oNV.Description
            sCmd.Parameters.Add(New SqlParameter("@mShortDescription", SqlDbType.VarChar)).Value = oNV.ShortDescription
            sCmd.Parameters.Add(New SqlParameter("@mIsDefault", SqlDbType.VarChar)).Value = oNV.IsDefault
            sCmd.Parameters.Add(New SqlParameter("@mIsDeleted", SqlDbType.VarChar)).Value = oNV.IsDeleted
            sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.BigInt)).Value = oNV.CreatedBy
            sCmd.Parameters.Add(New SqlParameter("@mCreatedDt", SqlDbType.VarChar)).Value = oNV.CreatedDt
            sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.BigInt)).Value = oNV.ModifiedBy
            sCmd.Parameters.Add(New SqlParameter("@mModifiedDt", SqlDbType.VarChar)).Value = oNV.ModifiedDt
            sCmd.Parameters.Add(New SqlParameter("@mDeletedBy", SqlDbType.BigInt)).Value = oNV.DeletedBy
            sCmd.Parameters.Add(New SqlParameter("@mDeletedDt", SqlDbType.VarChar)).Value = oNV.DeletedDt

            'sDaLookUpMaster = New SqlDataAdapter(sCmd)
            'sDaLookUpMaster.Fill(dsLookUpMaster, "LookUpMaster")
            'dsLookUpMaster.AcceptChanges()


            'sCnn.Open()
            'sCmd.ExecuteNonQuery()
            'sCnn.Close()

            sCnn.Open()

            ''sCmd.ExecuteNonQuery()

            Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function Update(ByVal oNV As StrLookUpMaster) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsLookUpMaster As New DataSet

            Dim myRec As StrLookUpMaster

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_LookUpMaster"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "Update"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.BigInt)).Value = oNV.PKID
            sCmd.Parameters.Add(New SqlParameter("@mFKLookUpHdr", SqlDbType.BigInt)).Value = oNV.FKLookUpHdr
            sCmd.Parameters.Add(New SqlParameter("@mCode", SqlDbType.VarChar)).Value = oNV.Code
            sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value = oNV.Description
            sCmd.Parameters.Add(New SqlParameter("@mShortDescription", SqlDbType.VarChar)).Value = oNV.ShortDescription
            sCmd.Parameters.Add(New SqlParameter("@mIsDefault", SqlDbType.VarChar)).Value = oNV.IsDefault
            sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.BigInt)).Value = oNV.ModifiedBy
            sCmd.Parameters.Add(New SqlParameter("@mModifiedDt", SqlDbType.VarChar)).Value = oNV.ModifiedDt

            'sDaLookUpMaster = New SqlDataAdapter(sCmd)
            'sDaLookUpMaster.Fill(dsLookUpMaster, "LookUpMaster")
            'dsLookUpMaster.AcceptChanges()


            'sCnn.Open()
            'sCmd.ExecuteNonQuery()
            'sCnn.Close()

            sCnn.Open()

            ''sCmd.ExecuteNonQuery()

            Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function LoadSeason() As DataTable

        'Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand


        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_SizeMaster"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "Load Season"
        sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.BigInt)).Value() = 0 'nFKSize

        dsSize.Clear()
        sDaLookUpMaster = New SqlDataAdapter(sCmd)
        sDaLookUpMaster.Fill(dsSize, "LookUpMaster")
        Return dsSize.Tables(0)

        dsLookUpMaster = Nothing
        sCnn.Close()

    End Function

 
    Public Function LoadCustPOtoSP(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELCUSTPOTOSP"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "CustPOtoSP")
        Return dsLookUpHdr.Tables(0)

        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function LoadOrderPOtoTOR(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELORDERPOTOTOR"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "OrderTOTOR")
        Return dsLookUpHdr.Tables(0)

        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function LoadFullDeliveryCompliance(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELFULLDLYCMPNCE"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "FULLSHOEDLYCOMPLINCE")
        Return dsLookUpHdr.Tables(0)

        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function LoadSpCompPO(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSPCOMPPO"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "SUPCOMPPO")
        Return dsLookUpHdr.Tables(0)

        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function LoadUpperDeliveryCompliance(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELUPPERDLYCMPNCE"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "UPPERDLYCOMPLINCE")
        Return dsLookUpHdr.Tables(0)

        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function SelectUnitType(ByVal nUnitType As Integer) As Boolean

        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim dsunittype As New DataSet
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELUNITTYPE"
        sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = nUnitType

        sDaUser = New SqlDataAdapter(sCmd)
        sDaUser.Fill(dsunittype, "UnitType")
        'frmMDI.cfrmUser.tbUnitType.Text = dsunittype.Tables(0).Rows(0).Item("Description")

    End Function

    Public Function SelectDesignation() As DataSet

        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand


        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELDESGINATION"

        sDaUser = New SqlDataAdapter(sCmd)
        sDaUser.Fill(sDsUser, "Designation")
        Dim keys(0) As DataColumn
        keys(0) = sDsUser.Tables("Designation").Columns(0)
        'sDsUser.Tables("Designation").PrimaryKey = keys
        Return sDsUser

        sCnn.Close()

    End Function

    Public Function LoadUserRight() As DataTable

        Dim sCmd As New SqlCommand

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMENUITEM"
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsUserRight, "UserRight")
        Return dsUserRight.Tables(0)
        dsUserRight = Nothing
        sCnn.Close()

    End Function

    Public Function SelectUserModule() As DataSet
        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMODULE"
        sDaUser = New SqlDataAdapter(sCmd)
        sDaUser.Fill(sDsUser, "UserModule")
        Dim keys(0) As DataColumn
        keys(0) = sDsUser.Tables("UserModule").Columns(0)
        sDsUser.Tables("UserModule").PrimaryKey = keys
        Return sDsUser
        'frmMDI.cfrmUser.CbxModuleName.DataSource = Nothing
        '' ''frmMDI.cfrmUser.CbxModuleDetail.Items.Clear()
        'frmMDI.cfrmUser.CbxModuleName.DisplayMember = Nothing
        'frmMDI.cfrmUser.CbxModuleName.Items.Clear()
        'frmMDI.cfrmUser.CbxModuleName.Items.Remove(frmMDI.cfrmUser.CbxModuleName.SelectedValue)
        sCnn.Close()
    End Function

    Public Function SelectUserMenuName(ByVal nMenuItem As Integer) As DataSet
        Dim daSelBUser As New SqlDataAdapter
        Dim dsSelUser As New DataSet
        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMENUITEMS"
        sCmd.Parameters.Add(New SqlParameter("@mFunctionsId", SqlDbType.Int)).Value() = nMenuItem

        daSelBUser = New SqlDataAdapter(sCmd)
        daSelBUser.Fill(dsSelUser, "MenuItem")

        Dim keys(0) As DataColumn
        keys(0) = dsSelUser.Tables("MenuItem").Columns(0)
        dsSelUser.Tables("MenuItem").PrimaryKey = keys
        Return dsSelUser
        sCnn.Close()
    End Function

    Dim nRowCount As Integer
  
    Public Function InsertUserHeader(ByVal oNV As strUserInsertHeader) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsInvHeader As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTUSERHEADER"
            sCmd.Parameters.Add(New SqlParameter("@mFKFirm", SqlDbType.Int)).Value = oNV.FKFirm
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNV.UserName
            sCmd.Parameters.Add(New SqlParameter("@mFKDesignation", SqlDbType.Int)).Value = oNV.FKDesgination
            sCmd.Parameters.Add(New SqlParameter("@mLoginName", SqlDbType.VarChar)).Value = oNV.LoginName
            sCmd.Parameters.Add(New SqlParameter("@mPassWord", SqlDbType.VarChar)).Value = oNV.PassWord
            sCmd.Parameters.Add(New SqlParameter("@mUserType", SqlDbType.VarChar)).Value = oNV.UserType
            'sCmd.Parameters.Add(New SqlParameter("@mUserPhoto", SqlDbType.Image)).Value = oNV.UserPhoto
            sCmd.Parameters.Add(New SqlParameter("@mIsDeleted", SqlDbType.VarChar)).Value = oNV.IsDeleted
            sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value = oNV.CreatedBy
            sCmd.Parameters.Add(New SqlParameter("@mCreatedDt", SqlDbType.VarChar)).Value = oNV.CreatedDt
            sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value = oNV.ModifiedBy
            sCmd.Parameters.Add(New SqlParameter("@mModifedDt", SqlDbType.VarChar)).Value = oNV.ModifiedDt
            sCmd.Parameters.Add(New SqlParameter("@mDeletedBY", SqlDbType.VarChar)).Value = oNV.DeletedBy
            sCmd.Parameters.Add(New SqlParameter("@mDeletedDt", SqlDbType.VarChar)).Value = oNV.DeletedDt
            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()



        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function

    Public Function InsertUserDetail(ByVal oNV As StrUserInsertDetail) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsInvHeader As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTUSERDETAIL"
            sCmd.Parameters.Add(New SqlParameter("@mFKUser", SqlDbType.Int)).Value = oNV.FKUser
            sCmd.Parameters.Add(New SqlParameter("@mFKMenu", SqlDbType.Int)).Value = oNV.FKMenu
            sCmd.Parameters.Add(New SqlParameter("@mAllFunctions", SqlDbType.VarChar)).Value = oNV.AllFunctions
            sCmd.Parameters.Add(New SqlParameter("@mAdding", SqlDbType.VarChar)).Value = oNV.Adding
            sCmd.Parameters.Add(New SqlParameter("@mEditing", SqlDbType.VarChar)).Value = oNV.Editing
            sCmd.Parameters.Add(New SqlParameter("@mDeleting", SqlDbType.VarChar)).Value = oNV.Deleting
            sCmd.Parameters.Add(New SqlParameter("@mViewing", SqlDbType.VarChar)).Value = oNV.Viewing
            sCmd.Parameters.Add(New SqlParameter("@mNoEntry", SqlDbType.VarChar)).Value = oNV.NoEntry

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()



        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function


    Dim nFKMenuCheck As Integer
    Dim ntRowCount As Integer

    Public Function CheckAlreadyMenuItem(ByVal UserHeaderPKID As Integer) As Boolean

        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim DsMenuCheck As New DataSet
        Dim DaMenuCheck As New SqlDataAdapter
        sCmd.Connection = sCnn
        sCnn.Open()
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "CHECKSAMEMENU"
        sCmd.Parameters.Add(New SqlParameter("@mFKUser", SqlDbType.Int)).Value() = UserHeaderPKID
        DaMenuCheck = New SqlDataAdapter(sCmd)
        DaMenuCheck.Fill(DsMenuCheck, "ALREADYMENUITEM")
        'ntRowCount = DsMenuCheck.Tables(0).Rows.Count
        If DsMenuCheck.Tables(0).Rows.Count > 0 Then
            Dim i As Integer = 0
            For i = 0 To ntRowCount
                'For i = 0 To DsMenuCheck.Tables(0).Rows.Count - 1
                nFKMenuCheck = DsMenuCheck.Tables(0).Rows(i).Item("FKMenu")
                'SelectUserModule1(nFKMenuCheck)
            Next
        End If

    End Function

    'Public Function SelectUserModule1(ByVal UserHeaderPKID As Integer) As DataSet
    '    Dim sCnn As New SqlConnection(sConstr)
    '    Dim sCmd As New SqlCommand
    '    Dim sda As SqlDataAdapter = Nothing

    '    sCmd.Connection = sCnn
    '    sCmd.CommandText = "Op_Modules"
    '    sCmd.CommandType = CommandType.StoredProcedure
    '    sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMODULEDETAIL"
    '    sCmd.Parameters.Add(New SqlParameter("@mFKUser", SqlDbType.Int)).Value() = UserHeaderPKID
    '    'sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = nFKMenu
    '    sda = New SqlDataAdapter(sCmd)
    '    sda.Fill(dsdetail, "USERMENUDETAIL")
    '    'frmMDI.cfrmUser.CbxModuleDetail.DataSource = 
    '    frmMDI.cfrmUser.CbxModuleDetail.DataSource = Nothing
    '    frmMDI.cfrmUser.CbxModuleDetail.DisplayMember = Nothing
    '    'frmMDI.cfrmUser.CbxModuleDetail.Items.Remove("jkhjk")
    '    'frmMDI.cfrmUser.CbxModuleDetail.DataSource = dsdetail
    '    'frmMDI.cfrmUser.CbxModuleDetail.DisplayMember = dsdetail
    '    'frmMDI.cfrmUser.CbxModuleDetail.ValueMember = "UserPKID"
    '    Dim keys(0) As DataColumn
    '    keys(0) = dsdetail.Tables("USERMENUDETAIL").Columns(0)
    '    dsdetail.Tables("USERMENUDETAIL").PrimaryKey = keys
    '    Return dsdetail
    '    'dsdetail = Nothing
    '    sCnn.Close()
    '    '"MenuNames"

    '    'cboBreed.DisplayMember = tblBreed.Columns(0).ColumnName


    '    'Dim keys(0) As DataColumn
    '    'keys(0) = dsSelUser.Tables("MenuItem").Columns(0)
    '    'dsSelUser.Tables("MenuItem").PrimaryKey = keys
    '    'Return dsSelUser
    'End Function
    Public Function SelectUserModule1(ByVal UserHeaderPKID As Integer) As DataSet
        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim sda As SqlDataAdapter = Nothing

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMODULEDETAIL"
        sCmd.Parameters.Add(New SqlParameter("@mFKUser", SqlDbType.Int)).Value() = UserHeaderPKID
        sda = New SqlDataAdapter(sCmd)
        dsdetail.Clear()
        sda.Fill(dsdetail, "USERMENUDETAIL")
        Dim keys(0) As DataColumn
        keys(0) = dsdetail.Tables("USERMENUDETAIL").Columns(0)
        dsdetail.Tables("USERMENUDETAIL").PrimaryKey = keys
        Return dsdetail
        sCnn.Close()
        '"MenuNames"
        'frmMDI.cfrmUser.CbxModuleDetail.DataSource = 

        ' ''frmMDI.cfrmUser.CbxModuleDetail.Items.Clear()


        'frmMDI.cfrmUser.CbxModuleName.Items.Clear()
        'frmMDI.cfrmUser.CbxModuleName.Items.Remove(frmMDI.cfrmUser.CbxModuleName.SelectedValue)

        'cboBreed.DisplayMember = tblBreed.Columns(0).ColumnName


        'Dim keys(0) As DataColumn
        'keys(0) = dsSelUser.Tables("MenuItem").Columns(0)
        'dsSelUser.Tables("MenuItem").PrimaryKey = keys
        'Return dsSelUser
    End Function

    'Public Function SelectBuy(ByVal nFKSeason As Integer, ByVal nFKCustomer As Integer) As DataSet

    '    Dim daSelBuy As New SqlDataAdapter
    '    Dim dsSelBuy As New DataSet
    '    Dim sCnn As New SqlConnection(sConstr)
    '    Dim sCmd As New SqlCommand
    '    Dim myRec As strSeason

    '    sCmd.Connection = sCnn
    '    sCmd.CommandText = "Op_Orders"
    '    sCmd.CommandType = CommandType.StoredProcedure

    '    sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELBUY1"
    '    sCmd.Parameters.Add(New SqlParameter("@mFKSeason", SqlDbType.Int)).Value() = nFKSeason
    '    sCmd.Parameters.Add(New SqlParameter("@mFKCustomer", SqlDbType.Int)).Value() = nFKCustomer

    '    daSelBuy = New SqlDataAdapter(sCmd)
    '    daSelBuy.Fill(dsSelBuy, "Buy")

    '    Dim keys(0) As DataColumn
    '    keys(0) = dsSelBuy.Tables("Buy").Columns(0)
    '    dsSelBuy.Tables("Buy").PrimaryKey = keys
    '    Return dsSelBuy

    '    sCnn.Close()

    'End Function
    Public Function SelectUnitName() As DataSet

        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand


        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELUNITNAME"

        sDaUser = New SqlDataAdapter(sCmd)
        sDaUser.Fill(sDsUser, "UnitName")
        Dim keys(0) As DataColumn
        keys(0) = sDsUser.Tables("UnitName").Columns(0)
        sDsUser.Tables("UnitName").PrimaryKey = keys
        Return sDsUser

        sCnn.Close()

    End Function

    Public Function LoadUserInfo() As DataTable
        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim sda As SqlDataAdapter = Nothing

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADUSERHDR"

        sda = New SqlDataAdapter(sCmd)
        sda.Fill(dsdetail, "LoadUserHdr")
        Return dsdetail.Tables(0)
        dsdetail = Nothing
        sCnn.Close()
    End Function

    Public Function LoadUserDtl(ByVal nPKID As Integer) As DataTable
        Dim sCnn As New SqlConnection(sConstr)
        Dim sCmd As New SqlCommand
        Dim sda As SqlDataAdapter = Nothing

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADUSERDTL"
        sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = nPKID
        dsLoadDtl1.Clear()

        sda = New SqlDataAdapter(sCmd)
        sda.Fill(dsLoadDtl1, "LoadUserDtl")
        Return dsLoadDtl1.Tables(0)
        dsLoadDtl1 = Nothing
        sCnn.Close()
    End Function


    Public Function UpdateUserDetail(ByVal oNv As StrUserInsertDetail) As Boolean
        Try
            Dim sCnn As New SqlConnection(sConstr)
            Dim sCmd As New SqlCommand
            Dim sda As SqlDataAdapter = Nothing

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDDTL"
            sCmd.Parameters.Add(New SqlParameter("@mFKUser", SqlDbType.Int)).Value() = oNv.FKUser
            sCmd.Parameters.Add(New SqlParameter("@mFKMenu", SqlDbType.Int)).Value = oNv.FKMenu
            sCmd.Parameters.Add(New SqlParameter("@mAllFunctions", SqlDbType.VarChar)).Value() = oNv.AllFunctions
            sCmd.Parameters.Add(New SqlParameter("@mAdding", SqlDbType.VarChar)).Value() = oNv.Adding
            sCmd.Parameters.Add(New SqlParameter("@mEditing", SqlDbType.VarChar)).Value() = oNv.Editing
            sCmd.Parameters.Add(New SqlParameter("@mDeleting", SqlDbType.VarChar)).Value() = oNv.Deleting
            sCmd.Parameters.Add(New SqlParameter("@mViewing", SqlDbType.VarChar)).Value() = oNv.Viewing
            sCmd.Parameters.Add(New SqlParameter("@mNoEntry", SqlDbType.VarChar)).Value() = oNv.NoEntry
            sCnn.Open()

            ''sCmd.ExecuteNonQuery()

            Dim sRes As String = sCmd.ExecuteScalar  '.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function

    Public Function UpdateUserPassword(ByVal nPKID As Integer, ByRef sPassword As String) As Boolean
        Try
            Dim sCnn As New SqlConnection(sConstr)
            Dim sCmd As New SqlCommand
            Dim sda As SqlDataAdapter = Nothing

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPASSWORD"
            sCmd.Parameters.Add(New SqlParameter("@mPassword", SqlDbType.VarChar)).Value() = sPassword
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = nPKID

            sCnn.Open()

            ''sCmd.ExecuteNonQuery()

            Dim sRes As String = sCmd.ExecuteScalar  '.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function

    Public Function DeleteUserRight(ByVal nPKID As Integer) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsDeleteUser As New DataSet


            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "DELETEUSERRIGHT"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value = nPKID

            'sCnn.Close()
            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function DeleteUser(ByVal nPKID As Integer) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsDeleteUser As New DataSet


            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "DELETEUSER"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value = nPKID

            'sCnn.Close()
            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function SelectTORTOGRN(ByRef sorderNoGRN As String) As Boolean


        Dim sCmd As New SqlCommand

        Dim sDaTOR As New SqlDataAdapter

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_TOR"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELTORTOGRN1"
        sCmd.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value() = sorderNoGRN

        sDaTOR = New SqlDataAdapter(sCmd)
        sDaTOR.Fill(dsTORTOGRN, "TORTOGRN")
        'Return dsTORTOGRN.Tables(0)

        'dsTORTOGRN = Nothing
        sCnn.Close()


    End Function

    Public Function InsertTempGRN(ByVal oNV As StrTORTOGRN) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsOutwardHeader As New DataSet



            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_TOR"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTGRN"
            sCmd.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value = oNV.OrderNo
            sCmd.Parameters.Add(New SqlParameter("@mFKItem", SqlDbType.Int)).Value = oNV.FKItem
            sCmd.Parameters.Add(New SqlParameter("@mTORQuantity", SqlDbType.Decimal)).Value = oNV.TORQuantity
            sCmd.Parameters.Add(New SqlParameter("@mIndentQuantity", SqlDbType.Decimal)).Value = oNV.IndentQuantity
            sCmd.Parameters.Add(New SqlParameter("@mPOQuantity", SqlDbType.Decimal)).Value = oNV.POQuantity
            sCmd.Parameters.Add(New SqlParameter("@mGRNQuantity", SqlDbType.Decimal)).Value = oNV.GRNQty


            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Dim IndentQuantity As Decimal
    Dim nPKIDGRN As Integer

    Public Function UpdateTempGRN(ByRef sOrderNoGRN As String) As Boolean


        Dim sCmd As New SqlCommand
        Dim dsInsertGRN As New DataSet
        Dim sDaGRN As New SqlDataAdapter



        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_TOR"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "SELINDENTTOGRN1"
        sCmd.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value = sOrderNoGRN
        sDaGRN = New SqlDataAdapter(sCmd)
        sDaGRN.Fill(dsInsertGRN, "INDENTTOGRN")
        If dsInsertGRN.Tables(0).Rows.Count > 0 Then
            Dim i As Integer = 0

            For i = 0 To dsInsertGRN.Tables(0).Rows.Count - 1
                nPKIDGRN = dsInsertGRN.Tables(0).Rows(i).Item("FKItem")
                IndentQuantity = dsInsertGRN.Tables(0).Rows(i).Item("IndentQuantity")
                UpdateIndentTemp(nPKIDGRN, IndentQuantity)
            Next
        End If


    End Function

    Public Function UpdateIndentTemp(ByVal nPKID As Integer, ByVal Indent As Decimal) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsOrderDetail As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_TOR"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEINDENTTOGRN"
            sCmd.Parameters.Add(New SqlParameter("@mFKItem", SqlDbType.Int)).Value = nPKID
            sCmd.Parameters.Add(New SqlParameter("@mIndentQuantity", SqlDbType.Decimal)).Value = Indent
            'sCnn.Close()
            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function

    Dim POQuantity As Decimal
    Dim GRNQuantity As Decimal

    Public Function UpdateTempPOtoGRN(ByRef sOrderNoGRN As String) As Boolean

        Dim sCmd As New SqlCommand
        Dim dsInsertGRN As New DataSet
        Dim sDaGRN As New SqlDataAdapter



        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_TOR"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "SELPOTOGRN1"
        sCmd.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value = sOrderNoGRN

        sDaGRN = New SqlDataAdapter(sCmd)
        sDaGRN.Fill(dsInsertGRN, "POTOGRN")
        If dsInsertGRN.Tables(0).Rows.Count > 0 Then
            Dim i As Integer = 0

            For i = 0 To dsInsertGRN.Tables(0).Rows.Count - 1
                nPKIDGRN = dsInsertGRN.Tables(0).Rows(i).Item("FKItem")
                POQuantity = dsInsertGRN.Tables(0).Rows(i).Item("POQuantity")
                GRNQuantity = dsInsertGRN.Tables(0).Rows(i).Item("GRNQuantity")
                UpdatePOTemp(nPKIDGRN, POQuantity, GRNQuantity)
            Next
        End If


    End Function

    Public Function UpdatePOTemp(ByVal nPKID As Integer, ByVal PO As Decimal, ByVal GRN As Decimal) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsOrderDetail As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_TOR"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEPOTOGRN"
            sCmd.Parameters.Add(New SqlParameter("@mFKItem", SqlDbType.Int)).Value = nPKID
            sCmd.Parameters.Add(New SqlParameter("@mPOQuantity", SqlDbType.Decimal)).Value = PO
            sCmd.Parameters.Add(New SqlParameter("@mGRNQuantity", SqlDbType.Decimal)).Value = GRN
            'sCnn.Close()
            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function

    Public Function DeleteTemp() As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsOrderDetail As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_TOR"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "DELETEGRN"
            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try

    End Function

    Public Function SelectTORTOGRNTEMP() As DataTable


        Dim sCmd As New SqlCommand
        Dim sDaTOR As New SqlDataAdapter

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_TOR"
        sCmd.CommandType = CommandType.StoredProcedure
        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELCUSTTOGRNTEMP"
        sDaTOR = New SqlDataAdapter(sCmd)
        sDaTOR.Fill(dsTORTOGRN, "TORTOGRNTemp")
        Return dsTORTOGRN.Tables(0)

        dsTORTOGRN = Nothing
        sCnn.Close()

    End Function
    Public Function InsertUserAuthentication(ByVal oNv As StrUserAuthentication) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsUserAuthen As New DataSet
            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTUSERAUTH"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value = oNv.nPKID
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNv.sUserName
            sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.Char)).Value = oNv.sIPAddress
            sCmd.Parameters.Add(New SqlParameter("@mSystemName", SqlDbType.Char)).Value = oNv.sSystemName
            sCmd.Parameters.Add(New SqlParameter("@mServer", SqlDbType.Char)).Value = oNv.sServer
            sCmd.Parameters.Add(New SqlParameter("@mLoginTime", SqlDbType.Char)).Value = oNv.sLoginTime
            sCmd.Parameters.Add(New SqlParameter("@mLogoutTime", SqlDbType.Char)).Value = oNv.sLogoutTime
            sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive
            sCmd.Parameters.Add(New SqlParameter("@mVersion", SqlDbType.VarChar)).Value = oNv.sVersion

            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function UpdateUserAuthentication(ByVal oNv As StrUserAuthentication) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsUserAuthen As New DataSet
            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEUSERAUTH"
            sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNv.sUserName
            sCmd.Parameters.Add(New SqlParameter("@mLogoutTime", SqlDbType.Char)).Value = oNv.sLogoutTime
            sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive





            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function CheckUserAlredyLogin(ByVal oNv As StrUserAuthentication) As Boolean


        Dim sCmd As New SqlCommand
        Dim SdaInsUserAuthen As New SqlDataAdapter
        Dim dsInsUserAuthen As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKUSERALRDYLOG"
        sCmd.Parameters.Add(New SqlParameter("@mUserName", SqlDbType.VarChar)).Value = oNv.sUserName
        'sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive

        SdaInsUserAuthen = New SqlDataAdapter(sCmd)
        SdaInsUserAuthen.Fill(dsInsUserAuthen)

        If dsInsUserAuthen.Tables(0).Rows.Count = 0 Then
            mdlSGM.sLoggedin = "N"
        Else
            mdlSGM.nUAId = dsInsUserAuthen.Tables(0).Rows(0).Item("PKID")
            mdlSGM.sLoggedin = "Y"
            mdlSGM.sIPAddress = dsInsUserAuthen.Tables(0).Rows(0).Item("IPAddress")
        End If



    End Function

    Public Function CheckIPAddress(ByVal oNv As StrUserAuthentication) As Boolean

        Dim sCmd As New SqlCommand
        Dim SdaInsUserAuthen As New SqlDataAdapter
        Dim dsInsUserAuthen As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKIPADDRLOG"
        ' sCmd.Parameters.Add(New SqlParameter("@mFKUserName", SqlDbType.Int)).Value = oNv.nFKUserName
        sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.Char)).Value = oNv.sIPAddress
        ' sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive

        SdaInsUserAuthen = New SqlDataAdapter(sCmd)
        SdaInsUserAuthen.Fill(dsInsUserAuthen)

        If dsInsUserAuthen.Tables(0).Rows.Count = 0 Then
            mdlSGM.sLoggedin = "N"
        Else
            mdlSGM.nUAId = dsInsUserAuthen.Tables(0).Rows(0).Item("PKID")
            mdlSGM.sLoggedin = "Y"
            mdlSGM.sLoggedUser = dsInsUserAuthen.Tables(0).Rows(0).Item("UserName")
            mdlSGM.sIPAddress = dsInsUserAuthen.Tables(0).Rows(0).Item("IPAddress")
        End If


    End Function

    Public Function CheckForSameIPAddress(ByVal oNv As StrUserAuthentication) As Boolean

        Dim sCmd As New SqlCommand
        Dim SdaInsUserAuthen As New SqlDataAdapter
        Dim dsInsUserAuthen As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKSAMEIP"
        'sCmd.Parameters.Add(New SqlParameter("@mFKUserName", SqlDbType.Int)).Value = oNv.nFKUserName
        sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.Char)).Value = oNv.sIPAddress
        ' sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive

        SdaInsUserAuthen = New SqlDataAdapter(sCmd)
        SdaInsUserAuthen.Fill(dsInsUserAuthen)

        If dsInsUserAuthen.Tables(0).Rows.Count = 0 Then
            mdlSGM.sLoggedin = "N"
        Else
            mdlSGM.sLoggedin = "Y"
            mdlSGM.nUAId = dsInsUserAuthen.Tables(0).Rows(0).Item("PKID")
            mdlSGM.sLoggedUser = dsInsUserAuthen.Tables(0).Rows(0).Item("UserName")
            mdlSGM.sIPAddress = dsInsUserAuthen.Tables(0).Rows(0).Item("IPAddress")
        End If


    End Function

    Public Function UpdateWithReason(ByVal oNv As StrUserAuthentication) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsInsUserAuthen As New DataSet
            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEUSER"
            sCmd.Parameters.Add(New SqlParameter("@mPKID ", SqlDbType.Int)).Value = oNv.nPKID
            sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Int)).Value = oNv.sIsActive
            sCmd.Parameters.Add(New SqlParameter("@mReason", SqlDbType.VarChar)).Value = oNv.sReason

            sCnn.Open()
            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function LoadBOMComplianceReport(ByVal dFromDate As String, ByVal dToDate As String) As DataTable

        Dim sCmd As New SqlCommand
        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELBOMCOMP"
        sCmd.Parameters.Add(New SqlParameter("@mDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mDate1", SqlDbType.DateTime)).Value() = dToDate
        sDaLookUpHdr = New SqlDataAdapter(sCmd)
        sDaLookUpHdr.Fill(dsLookUpHdr, "BOMCOMP")
        Return dsLookUpHdr.Tables(0)

        dsLookUpHdr = Nothing
        sCnn.Close()

    End Function

    Public Function LoadActiveUsers() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelActiveUser As New SqlDataAdapter
        Dim dsSelActiveUser As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Modules"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELACTIVEUSER"


        daSelActiveUser = New SqlDataAdapter(sCmd)
        daSelActiveUser.Fill(dsSelActiveUser, "ActiveUser")
        Return dsSelActiveUser.Tables(0)

        dsSelActiveUser = Nothing
        sCnn.Close()

    End Function

    Public Function ReleaseActiveUser(ByVal nPKID As Integer, ByVal sReason As String) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsOrderDetail As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Modules"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEUSER"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value = nPKID
            sCmd.Parameters.Add(New SqlParameter("@mReason", SqlDbType.VarChar)).Value = sReason
            sCmd.Parameters.Add(New SqlParameter("@mIsActive", SqlDbType.Bit)).Value = 0

            'sCnn.Close()
            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

  
    Private Sub setError(ByVal nNo As Integer)
        If nNo = 1 Then
            sErrCode = "NOREQPARAM"
            sErrMsg = "Required parameters value not specified"
        ElseIf nNo = 2 Then
            sErrCode = "INVALIDPARAM"
            sErrMsg = "Invalid parameters value specified"
        ElseIf nNo = 3 Then
            sErrCode = "SQLERROR"
            sErrMsg = "Internal SQL Server Error"
        ElseIf nNo = 4 Then
            sErrCode = "NOITEM"
            sErrMsg = "No Such Contact"
        ElseIf nNo = 5 Then
            sErrCode = "ACTIVEITEM"
            sErrMsg = "User is active and cannot be deleted"
        End If
    End Sub

#End Region

End Class


