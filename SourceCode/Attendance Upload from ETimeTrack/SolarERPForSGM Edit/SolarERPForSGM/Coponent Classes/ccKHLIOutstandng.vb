Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

'Public Structure strKHLIOutstanding

'Dim PKID As Integer
'Dim SalesOrderDetailId As String
'Dim SalesOrderDate As Date
'Dim CustomerName As String
'Dim SalesOrderNo As String
'Dim SalesOrderDetail As String
'Dim BuyerOrderNo As String
'Dim CustomerOrderNo As String
'Dim ArticleGroup As String
'Dim ArticleName As String
'Dim Colour As String
'Dim Destination As String
'Dim AssortmentName As String
'Dim NoofCartons As Integer
'Dim AssortmentTotalPair As Integer
'Dim BuyerDeliveryDate As Date
'Dim Season As String
'Dim UserCategory As String
'Dim ProductionName As String
'Dim OrdQty As Integer
'Dim CancelQuantity As Integer
'Dim UpperAndLiningCutting As Integer
'Dim UpperAndLiningCuttingBal As Integer
'Dim PreFitting As Integer
'Dim PreFittingBal As Integer
'Dim SocksPrepration As Integer
'Dim SocksPreprationBal As Integer
'Dim UpperConveyorIn As Integer
'Dim UpperConveyorInBal As Integer
'Dim UpperConveyorOut As Integer
'Dim UpperConveyorOutBal As Integer
'Dim HandStitching As Integer
'Dim HasndStitchingBal As Integer
'Dim Forming As Integer
'Dim FormingBal As Integer
'Dim Finishing As Integer
'Dim FinishingBal As Integer
'Dim UpperPacking As Integer
'Dim UpperPackingBal As Integer
'Dim UpperDispatch As Integer
'Dim UpperDispatchBal As Integer
'Dim Feeding As Integer
'Dim FeedingBal As Integer
'Dim Kitting As Integer
'Dim KittingBal As Integer
'Dim ConveyorIn As Integer
'Dim ConveyorInBal As Integer
'Dim ConveyorOut As Integer
'Dim ConveyorOutBal As Integer
'Dim Packing As Integer
'Dim PackingBal As Integer
'Dim Dispatch As Integer
'Dim DispatchBal As Integer
'Dim UpdatedOn As Date
'Dim IsCompleted As Integer
'Dim IsClosed As Integer

'End Structure


#End Region

Public Class ccKHLIOutstanding

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

    Dim PKID As Integer
    Dim SalesOrderDetailId As String
    Dim SalesOrderDate As Date
    Dim CustomerName As String
    Dim SalesOrderNo As String
    Dim SalesOrderDetail As String
    Dim BuyerOrderNo As String
    Dim CustomerOrderNo As String
    Dim ArticleGroup As String
    Dim ArticleName As String
    Dim Colour As String
    Dim Destination As String
    Dim AssortmentName As String
    Dim NoofCartons As Integer
    Dim AssortmentTotalPair As Integer
    Dim BuyerDeliveryDate As Date
    Dim Season As String
    Dim UserCategory As String
    Dim ProductionName As String
    Dim OrdQty As Integer
    Dim CancelQuantity As Integer
    Dim UpperAndLiningCutting As Integer
    Dim UpperAndLiningCuttingBal As Integer
    Dim PreFitting As Integer
    Dim PreFittingBal As Integer
    Dim SocksPrepration As Integer
    Dim SocksPreprationBal As Integer
    Dim UpperConveyorIn As Integer
    Dim UpperConveyorInBal As Integer
    Dim UpperConveyorOut As Integer
    Dim UpperConveyorOutBal As Integer
    Dim HandStitching As Integer
    Dim HasndStitchingBal As Integer
    Dim Forming As Integer
    Dim FormingBal As Integer
    Dim Finishing As Integer
    Dim FinishingBal As Integer
    Dim UpperPacking As Integer
    Dim UpperPackingBal As Integer
    Dim UpperDispatch As Integer
    Dim UpperDispatchBal As Integer
    Dim Feeding As Integer
    Dim FeedingBal As Integer
    Dim Kitting As Integer
    Dim KittingBal As Integer
    Dim ConveyorIn As Integer
    Dim ConveyorInBal As Integer
    Dim ConveyorOut As Integer
    Dim ConveyorOutBal As Integer
    Dim Packing As Integer
    Dim PackingBal As Integer
    Dim Dispatch As Integer
    Dim DispatchBal As Integer
    Dim UpdatedOn As Date
    Dim IsCompleted As Integer
    Dim IsClosed As Integer

    Dim sStages As String
    Dim sVariant As String
    Dim ColorCode As String
    Dim LeatherCode As String

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


    Public Function UpdateKHLIOutstanding() As DataTable

        Dim sCmd1 As New SqlCommand

        Dim daSelUpdDate, daSelAllOrder As New SqlDataAdapter
        Dim dsSelUpdDate, dsSelAllOrder As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "KHLI_OrderOutstanding"

        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELUPDDATE"

        daSelUpdDate = New SqlDataAdapter(sCmd1)
        daSelUpdDate.Fill(dsSelUpdDate, "Date")

        Dim sCmd2 As New SqlCommand

        sCmd2.Connection = sCnn
        sCmd2.CommandText = "KHLI_OrderOutstanding"
        sCmd2.CommandType = CommandType.StoredProcedure

        If dsSelUpdDate.Tables(0).Rows.Count = 0 Then
            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALLORD"
        Else
            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODORD" 'TODO Coding has to be done for this selection
            sCmd2.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.Date)).Value = Format(dsSelUpdDate.Tables(0).Rows(0).Item(0), "dd-MMM-yyyy")

        End If

        Dim i As Integer = 0

        daSelAllOrder = New SqlDataAdapter(sCmd2)
        daSelAllOrder.Fill(dsSelAllOrder, "AllOrd")

        For i = 0 To dsSelAllOrder.Tables(0).Rows.Count - 1

            'PKID = dsSelAllOrder.Tables(0).Rows(i).Item("PKID").ToString
            SalesOrderDetailId = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderDetailId").ToString
            SalesOrderDate = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderDate").ToString
            CustomerName = dsSelAllOrder.Tables(0).Rows(i).Item("CustomerName").ToString
            SalesOrderNo = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderNo").ToString
            SalesOrderDetail = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderDetail").ToString
            BuyerOrderNo = dsSelAllOrder.Tables(0).Rows(i).Item("BuyerOrderNo").ToString
            CustomerOrderNo = dsSelAllOrder.Tables(0).Rows(i).Item("CustomerOrderNo").ToString
            ArticleGroup = dsSelAllOrder.Tables(0).Rows(i).Item("ArticleGroup").ToString
            ArticleName = dsSelAllOrder.Tables(0).Rows(i).Item("Article").ToString
            Colour = dsSelAllOrder.Tables(0).Rows(i).Item("Color").ToString
            Destination = dsSelAllOrder.Tables(0).Rows(i).Item("Destination").ToString
            AssortmentName = dsSelAllOrder.Tables(0).Rows(i).Item("AssortmentName").ToString
            NoofCartons = dsSelAllOrder.Tables(0).Rows(i).Item("NoofCartons").ToString
            AssortmentTotalPair = dsSelAllOrder.Tables(0).Rows(i).Item("AssortmentTotalPair").ToString
            BuyerDeliveryDate = dsSelAllOrder.Tables(0).Rows(i).Item("BuyerDeliveryDate").ToString
            Season = dsSelAllOrder.Tables(0).Rows(i).Item("Season").ToString
            UserCategory = dsSelAllOrder.Tables(0).Rows(i).Item("UserCategory").ToString
            OrdQty = dsSelAllOrder.Tables(0).Rows(i).Item("OrdQty").ToString
            CancelQuantity = dsSelAllOrder.Tables(0).Rows(i).Item("CancelQuantity").ToString
            sVariant = dsSelAllOrder.Tables(0).Rows(i).Item("Variant").ToString
            ColorCode = dsSelAllOrder.Tables(0).Rows(i).Item("ColorCode").ToString
            LeatherCode = dsSelAllOrder.Tables(0).Rows(i).Item("MainRawMaterialCode").ToString


            
            Dim sCmd21 As New SqlCommand
            Dim daSelProductionName As New SqlDataAdapter : Dim dsSelProductionName As New DataSet

            sCmd21.Connection = sCnn
            sCmd21.CommandText = "KHLI_OrderOutstanding"
            sCmd21.CommandType = CommandType.StoredProcedure

            sCmd21.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODNAME"
            sCmd21.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = ArticleName
            sCmd21.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = sVariant
            sCmd21.Parameters.Add(New SqlParameter("@mColorCode", SqlDbType.VarChar)).Value() = ColorCode
            sCmd21.Parameters.Add(New SqlParameter("@mLeatherCode", SqlDbType.VarChar)).Value() = LeatherCode

            daSelProductionName = New SqlDataAdapter(sCmd21)
            daSelProductionName.Fill(dsSelProductionName, "Stages")

            If dsSelProductionName.Tables(0).Rows.Count = 0 Then
                GoTo Aa
            End If
            ProductionName = dsSelProductionName.Tables(0).Rows(0).Item("ProductionName").ToString

            Dim sCmd3 As New SqlCommand
            Dim daSelStages As New SqlDataAdapter
            Dim dsSelStages As New DataSet


            sCmd3.Connection = sCnn
            sCmd3.CommandText = "KHLI_OrderOutstanding"
            sCmd3.CommandType = CommandType.StoredProcedure


            sCmd3.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSTAGES"
            sCmd3.Parameters.Add(New SqlParameter("@mProductionName", SqlDbType.VarChar)).Value() = ProductionName


            Dim j As Integer = 0

            daSelStages = New SqlDataAdapter(sCmd3)
            daSelStages.Fill(dsSelStages, "Stages")

            For j = 0 To dsSelStages.Tables(0).Rows.Count - 1
                sStages = dsSelStages.Tables(0).Rows(j).Item("ProductionStage").ToString

                ''Mould Qty''
                Dim sCmd4 As New SqlCommand

                Dim daSelProdQty As New SqlDataAdapter
                Dim dsSelProdQty As New DataSet

                sCmd4.Connection = sCnn
                sCmd4.CommandText = "KHLI_OrderOutstanding"
                sCmd4.CommandType = CommandType.StoredProcedure

                sCmd4.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODQTY"
                sCmd4.Parameters.Add(New SqlParameter("@mProcessName", SqlDbType.VarChar)).Value() = sStages
                sCmd4.Parameters.Add(New SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value() = SalesOrderDetail


                daSelProdQty = New SqlDataAdapter(sCmd4)
                daSelProdQty.Fill(dsSelProdQty, "Prod")

                If sStages = "ULC" Then
                    UpperAndLiningCutting = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    UpperAndLiningCuttingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "PRF" Then
                    PreFitting = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    PreFittingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "SPR" Then
                    SocksPrepration = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    SocksPreprationBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "CIN" Then
                    UpperConveyorIn = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    UpperConveyorInBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "COU" Then
                    UpperConveyorOut = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    UpperConveyorOutBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "HST" Then
                    HandStitching = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    HasndStitchingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "FOR" Then
                    Forming = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    FormingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "FIN" Then
                    Finishing = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    FinishingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "PAC" Then
                    UpperPacking = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    UpperPackingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "DES" Then
                    UpperDispatch = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    UpperDispatchBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "FED" Then
                    Feeding = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    FeedingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "KIT" Then
                    Kitting = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    KittingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "" Then
                    ConveyorIn = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    ConveyorInBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "" Then
                    ConveyorOut = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    ConveyorOutBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "" Then
                    Packing = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    PackingBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "" Then
                    Dispatch = dsSelProdQty.Tables(0).Rows(0).Item(0)
                    DispatchBal = OrdQty - dsSelProdQty.Tables(0).Rows(0).Item(0)
                ElseIf sStages = "" Then

                End If
                ''Mould Qty''

                'UpdatedOn = Format(Date.Now, "dd-MMM-yyyy:hh:mm:ss")
                IsCompleted = "0"
                IsClosed = "0"
            Next
            ' ''DISP Qty''
            'Dim sCmd6 As                                                                           New SqlCommand

            'Dim daDISPQty As New SqlDataAdapter
            'Dim dsDISPQty As New DataSet

            'sCmd6.Connection = sCnn
            'sCmd6.CommandText = "KHLI_OrderOutstanding"
            'sCmd6.CommandType = CommandType.StoredProcedure

            'sCmd6.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELDISPQTY"
            'sCmd6.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = sSalesOrderDetailId

            'daDISPQty = New SqlDataAdapter(sCmd6)
            'daDISPQty.Fill(dsDISPQty, "DISP")
            ' ''DISP Qty''

            'nInStock = dsPACKQty.Tables(0).Rows(0).Item(0) - dsDISPQty.Tables(0).Rows(0).Item(0)
            'nDispatch = dsDISPQty.Tables(0).Rows(0).Item(0)

            'If nDispatch >= nOrdQty Then
            '    nIsCompleted = 1
            '    nIsClosed = 1
            'Else
            '    nIsCompleted = 0
            '    nIsClosed = 0
            'End If
Aa:
            Dim sCmd7 As New SqlCommand

            Dim daCheckStatus As New SqlDataAdapter
            Dim dsCheckStatus As New DataSet

            sCmd7.Connection = sCnn
            sCmd7.CommandText = "KHLI_OrderOutstanding"
            sCmd7.CommandType = CommandType.StoredProcedure


            sCmd7.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSOD"
            sCmd7.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = SalesOrderDetailId

            daCheckStatus = New SqlDataAdapter(sCmd7)
            daCheckStatus.Fill(dsCheckStatus, "SOD")


            Dim sCmd8 As New SqlCommand

            Dim daUpdateStatus As New SqlDataAdapter
            Dim dsUpdateStatus As New DataSet

            sCmd8.Connection = sCnn
            sCmd8.CommandText = "KHLI_OrderOutstanding"
            sCmd8.CommandType = CommandType.StoredProcedure

            If dsCheckStatus.Tables(0).Rows.Count = 0 Then
                sCmd8.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSSTATUS"
            ElseIf dsCheckStatus.Tables(0).Rows.Count = 1 Then
                sCmd8.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDSTATUS"
            Else
                MsgBox("Invalid Selection", MsgBoxStyle.Critical)
            End If
            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderDetailId", SqlDbType.VarChar)).Value() = SalesOrderDetailId
            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderDate", SqlDbType.DateTime)).Value() = Format(SalesOrderDate, "dd-MMM-yyyy")

            sCmd8.Parameters.Add(New SqlParameter("@mCustomerName", SqlDbType.VarChar)).Value() = CustomerName
            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = SalesOrderNo
            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderDetail", SqlDbType.VarChar)).Value() = SalesOrderDetail
            sCmd8.Parameters.Add(New SqlParameter("@mBuyerOrderNo", SqlDbType.VarChar)).Value() = BuyerOrderNo
            sCmd8.Parameters.Add(New SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value() = CustomerOrderNo
            sCmd8.Parameters.Add(New SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value() = ArticleGroup
            sCmd8.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = ArticleName
            sCmd8.Parameters.Add(New SqlParameter("@mColour", SqlDbType.VarChar)).Value() = Colour
            sCmd8.Parameters.Add(New SqlParameter("@mDestination", SqlDbType.VarChar)).Value() = Destination
            sCmd8.Parameters.Add(New SqlParameter("@mAssortmentName", SqlDbType.VarChar)).Value() = AssortmentName
            sCmd8.Parameters.Add(New SqlParameter("@mNoofCartons", SqlDbType.Int)).Value() = NoofCartons
            sCmd8.Parameters.Add(New SqlParameter("@mAssortmentTotalPair", SqlDbType.Int)).Value() = AssortmentTotalPair
            sCmd8.Parameters.Add(New SqlParameter("@mBuyerDeliveryDate", SqlDbType.DateTime)).Value() = Format(BuyerDeliveryDate, "dd-MMM-yyyy")
            sCmd8.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = Season
            sCmd8.Parameters.Add(New SqlParameter("@mUserCategory", SqlDbType.VarChar)).Value() = UserCategory
            sCmd8.Parameters.Add(New SqlParameter("@mProductionName", SqlDbType.VarChar)).Value() = ProductionName
            sCmd8.Parameters.Add(New SqlParameter("@mOrdQty", SqlDbType.Int)).Value() = OrdQty
            sCmd8.Parameters.Add(New SqlParameter("@mCancelQuantity", SqlDbType.Int)).Value() = CancelQuantity
            sCmd8.Parameters.Add(New SqlParameter("@mUpperAndLiningCutting", SqlDbType.Int)).Value() = UpperAndLiningCutting
            sCmd8.Parameters.Add(New SqlParameter("@mUpperAndLiningCuttingBal", SqlDbType.Int)).Value() = UpperAndLiningCuttingBal
            sCmd8.Parameters.Add(New SqlParameter("@mPreFitting", SqlDbType.Int)).Value() = PreFitting
            sCmd8.Parameters.Add(New SqlParameter("@mPreFittingBal", SqlDbType.Int)).Value() = PreFittingBal
            sCmd8.Parameters.Add(New SqlParameter("@mSocksPrepration", SqlDbType.Int)).Value() = SocksPrepration
            sCmd8.Parameters.Add(New SqlParameter("@mSocksPreprationBal", SqlDbType.Int)).Value() = SocksPreprationBal
            sCmd8.Parameters.Add(New SqlParameter("@mUpperConveyorIn", SqlDbType.Int)).Value() = UpperConveyorIn
            sCmd8.Parameters.Add(New SqlParameter("@mUpperConveyorInBal", SqlDbType.Int)).Value() = UpperConveyorInBal
            sCmd8.Parameters.Add(New SqlParameter("@mUpperConveyorOut", SqlDbType.Int)).Value() = UpperConveyorOut
            sCmd8.Parameters.Add(New SqlParameter("@mUpperConveyorOutBal", SqlDbType.Int)).Value() = UpperConveyorOutBal
            sCmd8.Parameters.Add(New SqlParameter("@mHandStitching", SqlDbType.Int)).Value() = HandStitching
            sCmd8.Parameters.Add(New SqlParameter("@mHasndStitchingBal", SqlDbType.Int)).Value() = HandStitching
            sCmd8.Parameters.Add(New SqlParameter("@mForming", SqlDbType.Int)).Value() = Forming
            sCmd8.Parameters.Add(New SqlParameter("@mFormingBal", SqlDbType.Int)).Value() = FormingBal
            sCmd8.Parameters.Add(New SqlParameter("@mFinishing", SqlDbType.Int)).Value() = Finishing
            sCmd8.Parameters.Add(New SqlParameter("@mFinishingBal", SqlDbType.Int)).Value() = FinishingBal
            sCmd8.Parameters.Add(New SqlParameter("@mUpperPacking", SqlDbType.Int)).Value() = UpperPacking
            sCmd8.Parameters.Add(New SqlParameter("@mUpperPackingBal", SqlDbType.Int)).Value() = UpperPackingBal
            sCmd8.Parameters.Add(New SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value() = UpperDispatch
            sCmd8.Parameters.Add(New SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value() = UpperDispatchBal
            sCmd8.Parameters.Add(New SqlParameter("@mFeeding", SqlDbType.Int)).Value() = Feeding
            sCmd8.Parameters.Add(New SqlParameter("@mFeedingBal", SqlDbType.Int)).Value() = FeedingBal
            sCmd8.Parameters.Add(New SqlParameter("@mKitting", SqlDbType.Int)).Value() = Kitting
            sCmd8.Parameters.Add(New SqlParameter("@mKittingBal", SqlDbType.Int)).Value() = KittingBal
            sCmd8.Parameters.Add(New SqlParameter("@mConveyorIn", SqlDbType.Int)).Value() = ConveyorIn
            sCmd8.Parameters.Add(New SqlParameter("@mConveyorInBal", SqlDbType.Int)).Value() = ConveyorInBal
            sCmd8.Parameters.Add(New SqlParameter("@mConveyorOut", SqlDbType.Int)).Value() = ConveyorOut
            sCmd8.Parameters.Add(New SqlParameter("@mConveyorOutBal", SqlDbType.Int)).Value() = ConveyorOutBal
            sCmd8.Parameters.Add(New SqlParameter("@mPacking", SqlDbType.Int)).Value() = Packing
            sCmd8.Parameters.Add(New SqlParameter("@mPackingBal", SqlDbType.Int)).Value() = PackingBal
            sCmd8.Parameters.Add(New SqlParameter("@mDispatch", SqlDbType.Int)).Value() = Dispatch
            sCmd8.Parameters.Add(New SqlParameter("@mDispatchBal", SqlDbType.Int)).Value() = DispatchBal
            sCmd8.Parameters.Add(New SqlParameter("@mUpdatedOn", SqlDbType.VarChar)).Value() = Format(Date.Now, "dd-MMM-yyyy-hh:mm:ss:tt")
            sCmd8.Parameters.Add(New SqlParameter("@mIsCompleted", SqlDbType.Int)).Value() = IsCompleted
            sCmd8.Parameters.Add(New SqlParameter("@mIsClosed", SqlDbType.Int)).Value() = IsClosed


            sCnn.Open()

            Dim sRes As String = sCmd8.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                setError(Val(sRes))
            End If
            sCnn.Close()


            ClearQuantities()

        Next


        Dim sCmd9 As New SqlCommand

        Dim daLoadOutstanding As New SqlDataAdapter
        Dim dsLoadOutstanding As New DataSet

        sCmd9.Connection = sCnn
        sCmd9.CommandText = "KHLI_OrderOutstanding"
        sCmd9.CommandType = CommandType.StoredProcedure


        sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTST"
        sCmd9.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        'sCmd9.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        daLoadOutstanding = New SqlDataAdapter(sCmd9)
        daLoadOutstanding.Fill(dsLoadOutstanding, "OutStanding")
        Return dsLoadOutstanding.Tables(0)

        dsLoadOutstanding = Nothing
        sCnn.Close()

    End Function


    Private Sub ClearQuantities()

        UpperAndLiningCutting = "0" : UpperAndLiningCuttingBal = "0" : PreFitting = "0" : PreFittingBal = "0"
        SocksPrepration = "0" : SocksPreprationBal = "0" : UpperConveyorIn = "0" : UpperConveyorInBal = "0"
        UpperConveyorOut = "0" : UpperConveyorOutBal = "0" : HandStitching = "0" : HasndStitchingBal = "0"
        Forming = "0" : FormingBal = "0" : Finishing = "0" : FinishingBal = "0"
        UpperPacking = "0" : UpperPackingBal = "0" : UpperDispatch = "0" : UpperDispatchBal = "0"
        Feeding = "0" : FeedingBal = "0" : Kitting = "0" : KittingBal = "0"
        ConveyorIn = "0" : ConveyorInBal = "0" : ConveyorOut = "0" : ConveyorOutBal = "0"
        Packing = "0" : PackingBal = "0" : Dispatch = "0" : DispatchBal = "0"

    End Sub
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
