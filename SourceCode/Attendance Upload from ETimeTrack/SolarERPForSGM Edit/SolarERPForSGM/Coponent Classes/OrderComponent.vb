Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strCRMCustomerOrderHeader

    Dim PKID As Integer
    Dim FKYear As Integer
    Dim Month As Integer
    Dim FKSeason As Integer
    Dim OrderQuality As Integer
    Dim OrderType As String
    Dim OrderSlNo As Integer
    Dim OrderNo As String
    Dim OrderDate As Date
    Dim OrderWK As String
    Dim RevisionNo As Integer
    Dim OrderReceivedDt As Date
    Dim OrderConfirmedDt As Date
    Dim FKCustomer As Integer
    Dim TypeofPacking As String
    Dim CustomerRef As String
    Dim CustomerRefDate As Date
    Dim FKCurrency As Integer
    Dim FKSize As Integer
    Dim OrderOption As String
    Dim TermCode As String
    Dim PayMode As String
    Dim DiscountPercentage As Decimal
    Dim DiscountValue As Decimal
    Dim AdditionalInfo As String
    Dim OrderStatus As String
    Dim TotalQuantity As Integer
    Dim IsEntryCompleted As Integer
    Dim UpperCutting As Integer
    Dim UppersProduced As Integer
    Dim UpperPacking As Integer
    Dim Feeding As Integer
    Dim Conveyor As Integer
    Dim FullShoeMaking As Integer
    Dim Packing As Integer
    Dim Dispatch As Integer
    Dim IsCompleted As Integer
    Dim IsClosed As Integer
    Dim CreatedBy As Integer
    Dim CreatedDt As String
    Dim ModifiedBy As Integer
    Dim ModifiedDt As String
    Dim DeletedBy As Integer
    Dim DeletedDt As String
    Dim JobCardGenerated As Integer
    Dim JobCardFrom As Integer
    Dim JobCardTo As Integer
    Dim TORGenerated As Integer
    Dim ReleaseforModification As Integer
    Dim ReleasedBy As Integer
    Dim ReleaseVersion As Integer
    Dim JCRegenerationRqrd As Integer
    Dim JCRegenerated As Integer
    Dim TORRegenerationRqrd As Integer
    Dim TORRegenerated As Integer
    Dim FKCustomerRef As Integer
    Dim FKOrderTo As Integer
End Structure

Public Structure strCRMCustomerOrderDetail

    Dim PKID As Integer
    Dim FKOrderHeader As Integer
    Dim CustomerOrderNo As String
    Dim FKArticle As Integer
    Dim FKModeofTransport As Integer
    Dim Destination As String
    Dim FKAssortment As Integer
    Dim PackCount As Integer
    Dim FKFactory As Integer
    Dim Price As Decimal
    Dim DeliveryDate As Date
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
    Dim Quantity19 As Integer
    Dim Quantity20 As Integer
    Dim TotalQty As Integer
    Dim UpperCutting As Integer
    Dim UppersProduced As Integer
    Dim UpperPacking As Integer
    Dim Feeding As Integer
    Dim Conveyor As Integer
    Dim FullShoeMaking As Integer
    Dim Packing As Integer
    Dim Dispatch As Integer
    Dim IsCompleted As Integer
    Dim IsClosed As Integer
    Dim IsCancelled As Integer
    Dim AdditionalInfo1 As String
    Dim AdditionalInfo2 As String
    Dim CSlNo As Integer

End Structure

#End Region

Public Class OrderComponent

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim sDaOrderHeader As New SqlDataAdapter
    Dim sDaOrderDetail As New SqlDataAdapter

    Dim nFirstJobCard, nLastJobCard As Integer
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

#Region "Functions"

    Public Function LoadOrderHeaderForPL(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsOrderHeader As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_OrderDetails"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELORDHDRFORPL"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate

        sDaOrderHeader = New SqlDataAdapter(sCmd)
        sDaOrderHeader.Fill(dsOrderHeader, "OrderHeader")
        Return dsOrderHeader.Tables(0)

        dsOrderHeader = Nothing
        sCnn.Close()

    End Function

    Public Function LoadFSOrderHeaderForPL(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsOrderHeader As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_OrderDetails"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELFSORDHDRFORPL"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate

        sDaOrderHeader = New SqlDataAdapter(sCmd)
        sDaOrderHeader.Fill(dsOrderHeader, "OrderHeader")
        Return dsOrderHeader.Tables(0)

        dsOrderHeader = Nothing
        sCnn.Close()

    End Function

    Public Function LoadOrderDetail(ByVal nFKOrderHeader As Integer) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsOrderDetail As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_OrdersRevised"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELORDDTL"
        sCmd.Parameters.Add(New SqlParameter("@mFKOrderheader", SqlDbType.Int)).Value() = nFKOrderHeader

        sDaOrderDetail = New SqlDataAdapter(sCmd)
        sDaOrderDetail.Fill(dsOrderDetail, "OrderDetail")
        Return dsOrderDetail.Tables(0)

        dsOrderDetail = Nothing
        sCnn.Close()

    End Function


#End Region

End Class


