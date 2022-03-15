Option Explicit On
Imports System.Data.SqlClient
Imports System.Data

#Region "Object Structures"

Public Structure strProductionByProcess
    Dim ID As String
    Dim ProcessName As String
    Dim ProcessDate As Date
    Dim ShiftNo As String
    Dim MachineNo As String
    Dim SalesOrderNo As String
    Dim Article As String
    Dim sVariant As String
    Dim ArticleGroup As String
    Dim ArticleGroupCode As String
    Dim MaterialCode As String
    Dim Size As String
    Dim Pcs As Decimal
    Dim Quantity As Decimal
    Dim Unit As String
    Dim Price As Decimal
    Dim Value As Decimal
    Dim CompanyCode As String
    Dim JobberCode As String
    Dim LotNo As String
    Dim Color As String
    Dim WorkOrderNo As String
    Dim MaterialColor As String
    Dim LocationName As String
    Dim BuyerCode As String
    Dim Buyer As String
    Dim SupplierCode As String
    Dim BrandCode As String
    Dim SupplierMaterialCode As String
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As String
    Dim ModifiedDate As Date
    Dim EnteredOnMachineID As String
    Dim ExeVersionNo As String
    Dim IsApproved As Integer
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim ModuleName As String
    Dim JobCardDetailID As String
    Dim EmployeeCode As String
    Dim ProductionID As String
    Dim Location As String
    Dim Station As String
    Dim RejectPcs As Decimal
    Dim FromLocation As String
    Dim SeqNo As Integer
    Dim CurrentQuantity As Decimal
    Dim LossQuantity As Decimal
    Dim CurrentPcs As Decimal
    Dim LossPcs As Decimal
    Dim SkinType As String
    Dim FromStage As String
    Dim OldFromLocation As String
    Dim OldLocation As String
    Dim IsHybrid As Integer
    Dim ComponentGroup As String
    Dim IsLastStage As Integer
    Dim OldJobCardNo As String
    Dim LeatherCode As String
    Dim ArticleDetailId As String

    ''Product Stock Field
    Dim Stage As String
    Dim ArticleNo As String
    Dim Weight As Decimal
    Dim TotValue As Decimal
    Dim BaseStyle As String
    Dim CustomerArtilceNo As String
    Dim SizeSequenceNo As String
    Dim RsNo As String
    Dim BuyerGroup As String
    Dim Merchandiser As String
    Dim TransactionType As String

End Structure

Public Structure strPartquantityproduction

    Dim ID As String
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As String
    Dim ModifiedDate As Date
    Dim ExeVersionNo As String
    Dim IsApproved As Integer
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim ModuleName As String
    Dim JobCardNo As String
    Dim BoxQunatity As Integer
    Dim CartonNo As Integer
    Dim Status As String
    Dim PartQuantity As Integer
    Dim Quantity01 As Integer
    Dim Quantity02 As Integer
    Dim Quantity03 As Integer
    Dim Quantity04 As Integer
    Dim Quantity05 As Integer
    Dim Quantity06 As Integer
    Dim Quantity07 As Integer
    Dim Quantity08 As Integer
    Dim Quantity09 As Integer
    Dim Quantity10 As Integer
    Dim Quantity11 As Integer
    Dim Quantity12 As Integer
    Dim Quantity13 As Integer
    Dim Quantity14 As Integer
    Dim Quantity15 As Integer
    Dim Quantity16 As Integer
    Dim Quantity17 As Integer
    Dim Quantity18 As Integer
    Dim ProcessName As String
    Dim IsSingleSize As Integer
    Dim Size As String

End Structure

Public Structure strPackingDetail

    Dim ID As String
    Dim JobCardNo As String
    Dim PackingDate As Date
    Dim BuyerGroupCode As String
    Dim BuyerCode As String
    Dim Shipper As String
    Dim InvoiceNo As String
    Dim ArticleGroup As String
    Dim Article As String
    Dim ColorCode As String
    Dim LeatherCode As String
    Dim CartonNo As Integer
    Dim Quantity As Decimal
    Dim Unit As String
    Dim Weight As Decimal
    Dim Size01 As String
    Dim Quantity01 As Integer
    Dim Size02 As String
    Dim Quantity02 As Integer
    Dim Size03 As String
    Dim Quantity03 As Integer
    Dim Size04 As String
    Dim Quantity04 As Integer
    Dim Size05 As String
    Dim Quantity05 As Integer
    Dim Size06 As String
    Dim Quantity06 As Integer
    Dim Size07 As String
    Dim Quantity07 As Integer
    Dim Size08 As String
    Dim Quantity08 As Integer
    Dim Size09 As String
    Dim Quantity09 As Integer
    Dim Size10 As String
    Dim Quantity10 As Integer
    Dim Size11 As String
    Dim Quantity11 As Integer
    Dim Size12 As String
    Dim Quantity12 As Integer
    Dim Size13 As String
    Dim Quantity13 As Integer
    Dim Size14 As String
    Dim Quantity14 As Integer
    Dim Size15 As String
    Dim Quantity15 As Integer
    Dim Size16 As String
    Dim Quantity16 As Integer
    Dim Size17 As String
    Dim Quantity17 As Integer
    Dim Size18 As String
    Dim Quantity18 As Integer
    Dim EnteredOnMachineID As String
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As String
    Dim ModifiedDate As Date
    Dim sVariant As String
    Dim CustomerStyleNo As String
    Dim ExeVersionNo As String
    Dim IsApproved As Integer
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim ModuleName As String
    Dim IsPacked As Integer
    Dim DCCartonNo As Integer
    Dim UpdateMode As String
    Dim PackingNo As String
    Dim Location As String
    Dim PackingListNo As String
    Dim JobCardDetailsID As String
    Dim SalesOrderDetailID As String
    Dim AssortmentID As String
    Dim OrderNo As String
    Dim SalesOrderNo As String
    Dim InvoiceID As String
    Dim IsAssorted As Integer
    Dim MaterialCode As String
    Dim CartonCBM As Decimal
    Dim BarCode As String
    Dim MouldScanDate As Date
    Dim FinishScanDate As Date
    Dim IsMouldUpdate As Integer
    Dim IsFinishUpdate As Integer
    Dim MtoFScanDate As Date
    Dim FtoPScanDate As Date
    Dim WIPLocation As String
    Dim ReadyToDispatch As Integer
    Dim ReadyToDispatchDate As Date

End Structure
#End Region

Public Class ccProductionByProcess

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrAudit As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL_Audit;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnnAudit As New SqlConnection(sConstrAudit)

    Private sErrMsg As String
    Private sErrCode As String

    'Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    'Dim sCnn As New SqlConnection(sConstr)

    'Dim sConstrAudit As String = Global.SolarERPForSGM.My.Settings.AHGroupAudit
    'Dim sCnnAudit As New SqlConnection(sConstrAudit)

    'Private sErrMsg As String
    'Private sErrCode As String

    Dim nRowCount As Integer
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

    Dim sID As String
    Dim nProducedQuantity As Integer
#Region "Functions"

    Public Function InsertProductionByProcess(ByVal oNV As strProductionByProcess) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelOutstanding As New SqlDataAdapter
        Dim dsSelOutstanding As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_ProductionByProcess"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODUCTION"
        sCmd.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName
        sCmd.Parameters.Add(New SqlParameter("@mProcessDate", SqlDbType.DateTime)).Value() = oNV.ProcessDate
        sCmd.Parameters.Add(New SqlParameter("@mShiftNo", SqlDbType.VarChar)).Value() = oNV.ShiftNo
        sCmd.Parameters.Add(New SqlParameter("@mMachineNo", SqlDbType.VarChar)).Value() = oNV.MachineNo
        sCmd.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = oNV.Size
        sCmd.Parameters.Add(New SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value() = oNV.JobCardDetailID
        sCmd.Parameters.Add(New SqlParameter("@mStation", SqlDbType.VarChar)).Value() = oNV.Station

        'daLoadArticles = New SqlDataAdapter(sCmd)
        daSelOutstanding = New SqlDataAdapter(sCmd)
        daSelOutstanding.Fill(dsSelOutstanding)
        If dsSelOutstanding.Tables(0).Rows.Count = 0 Then
            Dim sCmd2 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "proc_ProductionByProcess"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSPRODUCTION"

            sCmd2.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
            sCmd2.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName
            sCmd2.Parameters.Add(New SqlParameter("@mProcessDate", SqlDbType.DateTime)).Value() = oNV.ProcessDate
            sCmd2.Parameters.Add(New SqlParameter("@mShiftNo", SqlDbType.VarChar)).Value() = oNV.ShiftNo
            sCmd2.Parameters.Add(New SqlParameter("@mMachineNo", SqlDbType.VarChar)).Value() = oNV.MachineNo
            sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
            sCmd2.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = oNV.Article
            sCmd2.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = oNV.sVariant
            sCmd2.Parameters.Add(New SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value() = oNV.ArticleGroup
            sCmd2.Parameters.Add(New SqlParameter("@mArticleGroupCode", SqlDbType.VarChar)).Value() = oNV.ArticleGroupCode
            sCmd2.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.MaterialCode
            sCmd2.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = oNV.Size
            sCmd2.Parameters.Add(New SqlParameter("@mPcs", SqlDbType.Decimal)).Value() = oNV.Pcs
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
            sCmd2.Parameters.Add(New SqlParameter("@mUnit", SqlDbType.VarChar)).Value() = oNV.Unit
            sCmd2.Parameters.Add(New SqlParameter("@mPrice", SqlDbType.Decimal)).Value() = oNV.Price
            sCmd2.Parameters.Add(New SqlParameter("@mValue", SqlDbType.Decimal)).Value() = oNV.Value
            sCmd2.Parameters.Add(New SqlParameter("@mCompanyCode", SqlDbType.VarChar)).Value() = oNV.CompanyCode
            sCmd2.Parameters.Add(New SqlParameter("@mJobberCode", SqlDbType.VarChar)).Value() = oNV.JobberCode
            sCmd2.Parameters.Add(New SqlParameter("@mLotNo", SqlDbType.VarChar)).Value() = oNV.LotNo
            sCmd2.Parameters.Add(New SqlParameter("@mColor", SqlDbType.VarChar)).Value() = oNV.Color
            sCmd2.Parameters.Add(New SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value() = oNV.WorkOrderNo
            sCmd2.Parameters.Add(New SqlParameter("@mMaterialColor", SqlDbType.VarChar)).Value() = oNV.MaterialColor
            sCmd2.Parameters.Add(New SqlParameter("@mLocationName", SqlDbType.VarChar)).Value() = oNV.LocationName
            sCmd2.Parameters.Add(New SqlParameter("@mBuyerCode", SqlDbType.VarChar)).Value() = oNV.BuyerCode
            sCmd2.Parameters.Add(New SqlParameter("@mBuyer", SqlDbType.VarChar)).Value() = oNV.Buyer
            sCmd2.Parameters.Add(New SqlParameter("@mSupplierCode", SqlDbType.VarChar)).Value() = oNV.SupplierCode
            sCmd2.Parameters.Add(New SqlParameter("@mBrandCode", SqlDbType.VarChar)).Value() = oNV.BrandCode
            sCmd2.Parameters.Add(New SqlParameter("@mSupplierMaterialCode", SqlDbType.VarChar)).Value() = oNV.SupplierMaterialCode
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
            sCmd2.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
            sCmd2.Parameters.Add(New SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value() = oNV.ExeVersionNo
            sCmd2.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Int)).Value() = oNV.IsApproved
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
            sCmd2.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
            sCmd2.Parameters.Add(New SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value() = oNV.JobCardDetailID
            sCmd2.Parameters.Add(New SqlParameter("@mEmployeeCode", SqlDbType.VarChar)).Value() = oNV.EmployeeCode
            sCmd2.Parameters.Add(New SqlParameter("@mProductionID", SqlDbType.VarChar)).Value() = oNV.ProductionID
            sCmd2.Parameters.Add(New SqlParameter("@mLocation", SqlDbType.VarChar)).Value() = oNV.Location
            sCmd2.Parameters.Add(New SqlParameter("@mStation", SqlDbType.VarChar)).Value() = oNV.Station
            sCmd2.Parameters.Add(New SqlParameter("@mRejectPcs", SqlDbType.Decimal)).Value() = oNV.RejectPcs
            sCmd2.Parameters.Add(New SqlParameter("@mFromLocation", SqlDbType.VarChar)).Value() = oNV.FromLocation
            sCmd2.Parameters.Add(New SqlParameter("@mSeqNo", SqlDbType.Int)).Value() = oNV.SeqNo
            sCmd2.Parameters.Add(New SqlParameter("@mCurrentQuantity", SqlDbType.Decimal)).Value() = oNV.CurrentQuantity
            sCmd2.Parameters.Add(New SqlParameter("@mLossQuantity", SqlDbType.Decimal)).Value() = oNV.LossQuantity
            sCmd2.Parameters.Add(New SqlParameter("@mCurrentPcs", SqlDbType.Decimal)).Value() = oNV.CurrentPcs
            sCmd2.Parameters.Add(New SqlParameter("@mLossPcs", SqlDbType.Decimal)).Value() = oNV.LossPcs
            sCmd2.Parameters.Add(New SqlParameter("@mSkinType", SqlDbType.VarChar)).Value() = oNV.SkinType
            sCmd2.Parameters.Add(New SqlParameter("@mFromStage", SqlDbType.VarChar)).Value() = oNV.FromStage
            sCmd2.Parameters.Add(New SqlParameter("@mOldFromLocation", SqlDbType.VarChar)).Value() = oNV.FromLocation
            sCmd2.Parameters.Add(New SqlParameter("@mOldLocation", SqlDbType.VarChar)).Value() = oNV.OldLocation
            sCmd2.Parameters.Add(New SqlParameter("@mIsHybrid", SqlDbType.Int)).Value() = oNV.IsHybrid
            sCmd2.Parameters.Add(New SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value() = oNV.ComponentGroup
            sCmd2.Parameters.Add(New SqlParameter("@mIsLastStage", SqlDbType.Int)).Value() = oNV.IsLastStage
            sCmd2.Parameters.Add(New SqlParameter("@mOldJobCardNo", SqlDbType.VarChar)).Value() = oNV.OldJobCardNo
            sCmd2.Parameters.Add(New SqlParameter("@mLeatherCode", SqlDbType.VarChar)).Value() = oNV.LeatherCode
            sCmd2.Parameters.Add(New SqlParameter("@mArticleDetailId", SqlDbType.VarChar)).Value() = oNV.ArticleDetailId

            sCnn.Open()

            Dim sRes As String = sCmd2.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        Else
            sID = dsSelOutstanding.Tables(0).Rows(0).Item("ID")
            nProducedQuantity = dsSelOutstanding.Tables(0).Rows(0).Item("Quantity") + oNV.Quantity
            Dim sCmd1 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd1.Connection = sCnn
            sCmd1.CommandText = "proc_ProductionByProcess"
            sCmd1.CommandType = CommandType.StoredProcedure

            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPRODUCTION"

            sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = sID
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = nProducedQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mCurrentQuantity", SqlDbType.Decimal)).Value() = nProducedQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate


            sCnn.Open()

            Dim sRes As String = sCmd1.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        End If

    End Function

    Public Function InsertProductStock(ByVal oNV As strProductionByProcess) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelOutstanding As New SqlDataAdapter
        Dim dsSelOutstanding As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_ProductionByProcess"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODUCTSTOCK"
        sCmd.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName
        sCmd.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = oNV.Size
        sCmd.Parameters.Add(New SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value() = oNV.WorkOrderNo

        'daLoadArticles = New SqlDataAdapter(sCmd)
        daSelOutstanding = New SqlDataAdapter(sCmd)
        daSelOutstanding.Fill(dsSelOutstanding)
        If dsSelOutstanding.Tables(0).Rows.Count = 0 Then
            Dim sCmd2 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "proc_ProductionByProcess"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSPRODUCSTOCK"

            sCmd2.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = System.Guid.NewGuid.ToString()
            sCmd2.Parameters.Add(New SqlParameter("@mLocation", SqlDbType.VarChar)).Value() = oNV.MachineNo
            sCmd2.Parameters.Add(New SqlParameter("@mStage", SqlDbType.VarChar)).Value() = oNV.ProcessName
            sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
            sCmd2.Parameters.Add(New SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value() = oNV.WorkOrderNo
            sCmd2.Parameters.Add(New SqlParameter("@mArticleNo", SqlDbType.VarChar)).Value() = oNV.Article
            sCmd2.Parameters.Add(New SqlParameter("@mWeight", SqlDbType.Decimal)).Value() = oNV.Weight
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
            sCmd2.Parameters.Add(New SqlParameter("@mUnit", SqlDbType.VarChar)).Value() = oNV.Unit
            sCmd2.Parameters.Add(New SqlParameter("@mPrice", SqlDbType.Decimal)).Value() = oNV.Price
            sCmd2.Parameters.Add(New SqlParameter("@mTotValue", SqlDbType.Decimal)).Value() = oNV.TotValue
            sCmd2.Parameters.Add(New SqlParameter("@mCompanyCode", SqlDbType.VarChar)).Value() = oNV.CompanyCode
            sCmd2.Parameters.Add(New SqlParameter("@mJobberCode", SqlDbType.VarChar)).Value() = oNV.JobberCode
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
            sCmd2.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
            sCmd2.Parameters.Add(New SqlParameter("@mBaseStyle", SqlDbType.VarChar)).Value() = oNV.BaseStyle
            sCmd2.Parameters.Add(New SqlParameter("@mColor", SqlDbType.VarChar)).Value() = oNV.Color
            sCmd2.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = oNV.Size
            sCmd2.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = oNV.sVariant
            sCmd2.Parameters.Add(New SqlParameter("@mCustomerArtilceNo", SqlDbType.VarChar)).Value() = oNV.CustomerArtilceNo
            sCmd2.Parameters.Add(New SqlParameter("@mSizeSequenceNo", SqlDbType.VarChar)).Value() = oNV.SizeSequenceNo
            sCmd2.Parameters.Add(New SqlParameter("@mRsNo", SqlDbType.VarChar)).Value() = oNV.RsNo
            sCmd2.Parameters.Add(New SqlParameter("@mLocationName", SqlDbType.VarChar)).Value() = oNV.LocationName
            sCmd2.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = Microsoft.VisualBasic.Left(oNV.Buyer, 10)
            sCmd2.Parameters.Add(New SqlParameter("@mMerchandiser", SqlDbType.VarChar)).Value() = oNV.Merchandiser
            sCmd2.Parameters.Add(New SqlParameter("@mTransactionType", SqlDbType.VarChar)).Value() = oNV.TransactionType
            sCmd2.Parameters.Add(New SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value() = oNV.ExeVersionNo
            sCmd2.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
            sCmd2.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
            sCmd2.Parameters.Add(New SqlParameter("@mBuyer", SqlDbType.VarChar)).Value() = oNV.BuyerCode
            sCmd2.Parameters.Add(New SqlParameter("@mOldLocation", SqlDbType.VarChar)).Value() = oNV.OldLocation
            sCmd2.Parameters.Add(New SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value() = oNV.ComponentGroup
            sCmd2.Parameters.Add(New SqlParameter("@mLeatherCode", SqlDbType.VarChar)).Value() = oNV.LeatherCode
            sCmd2.Parameters.Add(New SqlParameter("@mSupplierCode", SqlDbType.VarChar)).Value() = oNV.SupplierCode
            sCmd2.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.MaterialCode

            sCnn.Open()

            Dim sRes As String = sCmd2.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        Else
            sID = dsSelOutstanding.Tables(0).Rows(0).Item("ID")
            nProducedQuantity = dsSelOutstanding.Tables(0).Rows(0).Item("Quantity") + oNV.Quantity
            Dim sCmd1 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd1.Connection = sCnn
            sCmd1.CommandText = "proc_ProductionByProcess"
            sCmd1.CommandType = CommandType.StoredProcedure

            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPRODUCTSTOCK"

            sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = sID
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = nProducedQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mCurrentQuantity", SqlDbType.Decimal)).Value() = nProducedQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate


            sCnn.Open()

            Dim sRes As String = sCmd1.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        End If

    End Function

    Public Function UpdateProductStock(ByVal oNV As strProductionByProcess) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelOutstanding As New SqlDataAdapter
        Dim dsSelOutstanding As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_ProductionByProcess"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODUCTSTOCK"
        sCmd.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName
        sCmd.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = oNV.Size
        sCmd.Parameters.Add(New SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value() = oNV.WorkOrderNo

        'daLoadArticles = New SqlDataAdapter(sCmd)
        daSelOutstanding = New SqlDataAdapter(sCmd)
        daSelOutstanding.Fill(dsSelOutstanding)
        If dsSelOutstanding.Tables(0).Rows.Count = 0 Then

        Else
            sID = dsSelOutstanding.Tables(0).Rows(0).Item("ID")
            nProducedQuantity = dsSelOutstanding.Tables(0).Rows(0).Item("Quantity") - oNV.Quantity
            Dim sCmd1 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd1.Connection = sCnn
            sCmd1.CommandText = "proc_ProductionByProcess"
            sCmd1.CommandType = CommandType.StoredProcedure

            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPRODUCTSTOCK"

            sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = sID
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = nProducedQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mCurrentQuantity", SqlDbType.Decimal)).Value() = nProducedQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate


            sCnn.Open()

            Dim sRes As String = sCmd1.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        End If

    End Function

    Public Function InsertPartProduction(ByVal oNV As strPartquantityproduction) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelOutstanding As New SqlDataAdapter
        Dim dsSelOutstanding As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_ProductionByProcess"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPARTPROD"
        sCmd.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName
        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo
        sCmd.Parameters.Add(New SqlParameter("@mCartonNO", SqlDbType.Int)).Value() = oNV.CartonNo

        daSelOutstanding = New SqlDataAdapter(sCmd)
        daSelOutstanding.Fill(dsSelOutstanding)
        If dsSelOutstanding.Tables(0).Rows.Count = 0 Then
            Dim sCmd2 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "proc_ProductionByProcess"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSPARTPROD"

            sCmd2.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
            sCmd2.Parameters.Add(New SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value() = oNV.ExeVersionNo
            sCmd2.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Int)).Value() = oNV.IsApproved
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
            sCmd2.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
            sCmd2.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Int)).Value() = oNV.BoxQunatity
            sCmd2.Parameters.Add(New SqlParameter("@mCartonNo", SqlDbType.Int)).Value() = oNV.CartonNo
            sCmd2.Parameters.Add(New SqlParameter("@mStatus", SqlDbType.VarChar)).Value() = oNV.Status
            sCmd2.Parameters.Add(New SqlParameter("@mPartQuantity", SqlDbType.Int)).Value() = oNV.PartQuantity
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Int)).Value() = oNV.Quantity01
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Int)).Value() = oNV.Quantity02
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Int)).Value() = oNV.Quantity03
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Int)).Value() = oNV.Quantity04
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Int)).Value() = oNV.Quantity05
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Int)).Value() = oNV.Quantity06
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Int)).Value() = oNV.Quantity07
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Int)).Value() = oNV.Quantity08
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Int)).Value() = oNV.Quantity09
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Int)).Value() = oNV.Quantity10
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Int)).Value() = oNV.Quantity11
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Int)).Value() = oNV.Quantity12
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Int)).Value() = oNV.Quantity13
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Int)).Value() = oNV.Quantity14
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Int)).Value() = oNV.Quantity15
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Int)).Value() = oNV.Quantity16
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Int)).Value() = oNV.Quantity17
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Int)).Value() = oNV.Quantity18
            sCmd2.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName
            sCmd2.Parameters.Add(New SqlParameter("@mIsSingleSize", SqlDbType.Bit)).Value() = oNV.IsSingleSize
            sCmd2.Parameters.Add(New SqlParameter("@mSize", SqlDbType.VarChar)).Value() = oNV.Size

            sCnn.Open()

            Dim sRes As String = sCmd2.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        Else
            sID = dsSelOutstanding.Tables(0).Rows(0).Item("ID")
            Dim sCmd1 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd1.Connection = sCnn
            sCmd1.CommandText = "proc_ProductionByProcess"
            sCmd1.CommandType = CommandType.StoredProcedure

            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPARTPROD"

            sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = sID
            sCmd1.Parameters.Add(New SqlParameter("@mStatus", SqlDbType.VarChar)).Value() = oNV.Status
            sCmd1.Parameters.Add(New SqlParameter("@mPartQuantity", SqlDbType.Int)).Value() = oNV.PartQuantity
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Int)).Value() = oNV.Quantity01
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Int)).Value() = oNV.Quantity02
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Int)).Value() = oNV.Quantity03
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Int)).Value() = oNV.Quantity04
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Int)).Value() = oNV.Quantity05
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Int)).Value() = oNV.Quantity06
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Int)).Value() = oNV.Quantity07
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Int)).Value() = oNV.Quantity08
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Int)).Value() = oNV.Quantity09
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Int)).Value() = oNV.Quantity10
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Int)).Value() = oNV.Quantity11
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Int)).Value() = oNV.Quantity12
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Int)).Value() = oNV.Quantity13
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Int)).Value() = oNV.Quantity14
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Int)).Value() = oNV.Quantity15
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Int)).Value() = oNV.Quantity16
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Int)).Value() = oNV.Quantity17
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Int)).Value() = oNV.Quantity18
            sCmd1.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = oNV.ProcessName

            sCnn.Open()

            Dim sRes As String = sCmd1.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        End If

    End Function

    Public Function INSPKGDTLINAUDIT(ByVal oNV As strPackingDetail) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelOutstanding As New SqlDataAdapter
        Dim dsSelOutstanding As New DataSet

        sCmd.Connection = sCnnAudit
        sCmd.CommandText = "proc_ProductionByProcess"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSPKGDTL"

        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo
        sCmd.Parameters.Add(New SqlParameter("@mPackingDate", SqlDbType.DateTime)).Value() = oNV.PackingDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value() = oNV.BuyerGroupCode
        sCmd.Parameters.Add(New SqlParameter("@mBuyerCode", SqlDbType.VarChar)).Value() = oNV.BuyerCode
        sCmd.Parameters.Add(New SqlParameter("@mShipper", SqlDbType.VarChar)).Value() = oNV.Shipper
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value() = oNV.ArticleGroup
        sCmd.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = oNV.Article
        sCmd.Parameters.Add(New SqlParameter("@mColorCode", SqlDbType.VarChar)).Value() = oNV.ColorCode
        sCmd.Parameters.Add(New SqlParameter("@mLeatherCode", SqlDbType.VarChar)).Value() = oNV.LeatherCode
        sCmd.Parameters.Add(New SqlParameter("@mCartonNo", SqlDbType.Int)).Value() = oNV.CartonNo
        sCmd.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
        sCmd.Parameters.Add(New SqlParameter("@mUnit", SqlDbType.VarChar)).Value() = oNV.Unit
        sCmd.Parameters.Add(New SqlParameter("@mWeight", SqlDbType.Decimal)).Value() = oNV.Weight
        sCmd.Parameters.Add(New SqlParameter("@mSize01", SqlDbType.VarChar)).Value() = oNV.Size01
        sCmd.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Int)).Value() = oNV.Quantity01
        sCmd.Parameters.Add(New SqlParameter("@mSize02", SqlDbType.VarChar)).Value() = oNV.Size02
        sCmd.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Int)).Value() = oNV.Quantity02
        sCmd.Parameters.Add(New SqlParameter("@mSize03", SqlDbType.VarChar)).Value() = oNV.Size03
        sCmd.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Int)).Value() = oNV.Quantity03
        sCmd.Parameters.Add(New SqlParameter("@mSize04", SqlDbType.VarChar)).Value() = oNV.Size04
        sCmd.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Int)).Value() = oNV.Quantity04
        sCmd.Parameters.Add(New SqlParameter("@mSize05", SqlDbType.VarChar)).Value() = oNV.Size05
        sCmd.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Int)).Value() = oNV.Quantity05
        sCmd.Parameters.Add(New SqlParameter("@mSize06", SqlDbType.VarChar)).Value() = oNV.Size06
        sCmd.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Int)).Value() = oNV.Quantity06
        sCmd.Parameters.Add(New SqlParameter("@mSize07", SqlDbType.VarChar)).Value() = oNV.Size07
        sCmd.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Int)).Value() = oNV.Quantity07
        sCmd.Parameters.Add(New SqlParameter("@mSize08", SqlDbType.VarChar)).Value() = oNV.Size08
        sCmd.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Int)).Value() = oNV.Quantity08
        sCmd.Parameters.Add(New SqlParameter("@mSize09", SqlDbType.VarChar)).Value() = oNV.Size09
        sCmd.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Int)).Value() = oNV.Quantity09
        sCmd.Parameters.Add(New SqlParameter("@mSize10", SqlDbType.VarChar)).Value() = oNV.Size10
        sCmd.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Int)).Value() = oNV.Quantity10
        sCmd.Parameters.Add(New SqlParameter("@mSize11", SqlDbType.VarChar)).Value() = oNV.Size11
        sCmd.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Int)).Value() = oNV.Quantity11
        sCmd.Parameters.Add(New SqlParameter("@mSize12", SqlDbType.VarChar)).Value() = oNV.Size12
        sCmd.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Int)).Value() = oNV.Quantity12
        sCmd.Parameters.Add(New SqlParameter("@mSize13", SqlDbType.VarChar)).Value() = oNV.Size13
        sCmd.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Int)).Value() = oNV.Quantity13
        sCmd.Parameters.Add(New SqlParameter("@mSize14", SqlDbType.VarChar)).Value() = oNV.Size14
        sCmd.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Int)).Value() = oNV.Quantity14
        sCmd.Parameters.Add(New SqlParameter("@mSize15", SqlDbType.VarChar)).Value() = oNV.Size15
        sCmd.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Int)).Value() = oNV.Quantity15
        sCmd.Parameters.Add(New SqlParameter("@mSize16", SqlDbType.VarChar)).Value() = oNV.Size16
        sCmd.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Int)).Value() = oNV.Quantity16
        sCmd.Parameters.Add(New SqlParameter("@mSize17", SqlDbType.VarChar)).Value() = oNV.Size17
        sCmd.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Int)).Value() = oNV.Quantity17
        sCmd.Parameters.Add(New SqlParameter("@mSize18", SqlDbType.VarChar)).Value() = oNV.Size18
        sCmd.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Int)).Value() = oNV.Quantity18
        sCmd.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = oNV.sVariant
        sCmd.Parameters.Add(New SqlParameter("@mCustomerStyleNo", SqlDbType.VarChar)).Value() = oNV.CustomerStyleNo
        sCmd.Parameters.Add(New SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value() = oNV.ExeVersionNo
        sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Int)).Value() = oNV.IsApproved
        sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.ApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mIsPacked", SqlDbType.Int)).Value() = oNV.IsPacked
        sCmd.Parameters.Add(New SqlParameter("@mDCCartonNo", SqlDbType.Int)).Value() = oNV.DCCartonNo
        sCmd.Parameters.Add(New SqlParameter("@mUpdateMode", SqlDbType.VarChar)).Value() = oNV.UpdateMode
        sCmd.Parameters.Add(New SqlParameter("@mPackingNo", SqlDbType.VarChar)).Value() = oNV.PackingNo
        sCmd.Parameters.Add(New SqlParameter("@mLocation", SqlDbType.VarChar)).Value() = oNV.Location
        sCmd.Parameters.Add(New SqlParameter("@mPackingListNo", SqlDbType.VarChar)).Value() = oNV.PackingListNo
        sCmd.Parameters.Add(New SqlParameter("@mJobCardDetailsID", SqlDbType.VarChar)).Value() = oNV.JobCardDetailsID
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = oNV.SalesOrderDetailID
        sCmd.Parameters.Add(New SqlParameter("@mAssortmentID", SqlDbType.VarChar)).Value() = oNV.AssortmentID
        sCmd.Parameters.Add(New SqlParameter("@mOrderNo", SqlDbType.VarChar)).Value() = oNV.OrderNo
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value() = oNV.InvoiceID
        sCmd.Parameters.Add(New SqlParameter("@mIsAssorted", SqlDbType.Int)).Value() = oNV.IsAssorted
        sCmd.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.MaterialCode
        sCmd.Parameters.Add(New SqlParameter("@mCartonCBM", SqlDbType.Decimal)).Value() = oNV.CartonCBM
        sCmd.Parameters.Add(New SqlParameter("@mBarCode", SqlDbType.VarChar)).Value() = oNV.BarCode
        sCmd.Parameters.Add(New SqlParameter("@mMouldScanDate", SqlDbType.DateTime)).Value() = oNV.MouldScanDate
        If oNV.FinishScanDate = "#12:00:00 AM#" Then
            sCmd.Parameters.Add(New SqlParameter("@mFinishScanDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        Else
            sCmd.Parameters.Add(New SqlParameter("@mFinishScanDate", SqlDbType.DateTime)).Value() = oNV.FinishScanDate
        End If
        sCmd.Parameters.Add(New SqlParameter("@mIsMouldUpdate", SqlDbType.Int)).Value() = oNV.IsMouldUpdate
        sCmd.Parameters.Add(New SqlParameter("@mIsFinishUpdate", SqlDbType.Int)).Value() = oNV.IsFinishUpdate
        If oNV.MtoFScanDate = "#12:00:00 AM#" Then
            sCmd.Parameters.Add(New SqlParameter("@mMtoFScanDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        Else
            sCmd.Parameters.Add(New SqlParameter("@mMtoFScanDate", SqlDbType.DateTime)).Value() = oNV.MtoFScanDate
        End If
        'sCmd.Parameters.Add(New SqlParameter("@mMtoFScanDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.MtoFScanDate
        If oNV.FtoPScanDate = "#12:00:00 AM#" Then
            sCmd.Parameters.Add(New SqlParameter("@mFtoPScanDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        Else
            sCmd.Parameters.Add(New SqlParameter("@mFtoPScanDate", SqlDbType.DateTime)).Value() = oNV.FtoPScanDate
        End If
        'sCmd.Parameters.Add(New SqlParameter("@mFtoPScanDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.FtoPScanDate
        sCmd.Parameters.Add(New SqlParameter("@mWIPLocation", SqlDbType.VarChar)).Value() = oNV.WIPLocation
        sCmd.Parameters.Add(New SqlParameter("@mReadyToDispatch", SqlDbType.Int)).Value() = oNV.ReadyToDispatch
        If oNV.ReadyToDispatchDate = "#12:00:00 AM#" Then
            sCmd.Parameters.Add(New SqlParameter("@mReadyToDispatchDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        Else
            sCmd.Parameters.Add(New SqlParameter("@mReadyToDispatchDate", SqlDbType.DateTime)).Value() = oNV.ReadyToDispatchDate
        End If
        'sCmd.Parameters.Add(New SqlParameter("@mReadyToDispatchDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.ReadyToDispatchDate


        sCnnAudit.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnnAudit.Close()
        Else
            'setError(Val(sRes))
        End If
        sCnnAudit.Close()


    End Function

#End Region

End Class
