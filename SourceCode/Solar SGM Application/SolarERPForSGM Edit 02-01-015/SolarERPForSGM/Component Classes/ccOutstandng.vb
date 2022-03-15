Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strSolarOutstanding4SGM4Print

    Dim PKID As Integer
    Dim SalesOrderDetailId As String
    Dim SalesOrderDate As Date
    Dim CustomerName As String
    Dim SalesOrderNo As String
    Dim CustomerOrderNo As String
    Dim SoleCode As String
    Dim SoleName As String
    Dim Colour As String
    Dim Codification As String
    Dim OrdQty As Integer
    Dim Moulding As Integer
    Dim MouldingWIP As Integer
    Dim Finishing As Integer
    Dim FinishingWIP As Integer
    Dim Packing As Integer
    Dim InStock As Integer
    Dim Dispatch As Integer
    Dim UpdatedOn As Date
    Dim IsCompleted As Integer
    Dim IsClosed As Integer

End Structure

#End Region

Public Class ccOutstanding

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

    Dim nPKID As Integer
    Dim sSalesOrderDetailId As String
    Dim dSalesOrderDate As Date
    Dim sCustomerName As String
    Dim sSalesOrderNo As String
    Dim sCustomerOrderNo As String
    Dim sSoleCode As String
    Dim sSoleName As String
    Dim sColour As String
    Dim sCodification As String
    Dim nOrdQty As Integer
    Dim nMoulding As Integer
    Dim nMouldingWIP As Integer
    Dim nFinishing As Integer
    Dim nFinishingWIP As Integer
    Dim nPacking As Integer
    Dim nInStock As Integer
    Dim nDispatch As Integer
    Dim UdpdatedOn As Date
    Dim nIsCompleted, nIsClosed As Integer
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


    Public Function UpdateOutstanding() As DataTable

        Dim sCmd1 As New SqlCommand

        Dim daSelUpdDate, daSelAllOrder As New SqlDataAdapter
        Dim dsSelUpdDate, dsSelAllOrder As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "sgm_OrderOutstanding"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELUPDDATE"

        daSelUpdDate = New SqlDataAdapter(sCmd1)
        daSelUpdDate.Fill(dsSelUpdDate, "Date")

        Dim sCmd2 As New SqlCommand

        sCmd2.Connection = sCnn
        sCmd2.CommandText = "sgm_OrderOutstanding"
        sCmd2.CommandType = CommandType.StoredProcedure

        If dsSelUpdDate.Tables(0).Rows.Count = 0 Then
            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALLORD"
        Else
            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODORD"
            sCmd2.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.Date)).Value = Format(dsSelUpdDate.Tables(0).Rows(0).Item(0), "dd-MMM-yyyy")

        End If

        Dim i As Integer = 0

        daSelAllOrder = New SqlDataAdapter(sCmd2)
        daSelAllOrder.Fill(dsSelAllOrder, "AllOrd")

        For i = 0 To dsSelAllOrder.Tables(0).Rows.Count - 1

            sSalesOrderDetailId = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderDetailId")
            dSalesOrderDate = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderDate")
            sCustomerName = dsSelAllOrder.Tables(0).Rows(i).Item("CustomerName")
            sSalesOrderNo = dsSelAllOrder.Tables(0).Rows(i).Item("SalesOrderNo")
            sCustomerOrderNo = dsSelAllOrder.Tables(0).Rows(i).Item("CustomerOrderNo")
            sSoleCode = dsSelAllOrder.Tables(0).Rows(i).Item("SoleCode")
            sSoleName = dsSelAllOrder.Tables(0).Rows(i).Item("SoleName")
            sColour = dsSelAllOrder.Tables(0).Rows(i).Item("Color")
            sCodification = dsSelAllOrder.Tables(0).Rows(i).Item("Codification").ToString
            nOrdQty = dsSelAllOrder.Tables(0).Rows(i).Item("OrdQty")

            ''Mould Qty''
            Dim sCmd3 As New SqlCommand

            Dim daMouldQty As New SqlDataAdapter
            Dim dsMouldQty As New DataSet

            sCmd3.Connection = sCnn
            sCmd3.CommandText = "sgm_OrderOutstanding"
            sCmd3.CommandType = CommandType.StoredProcedure

            sCmd3.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELMOULDQTY"
            sCmd3.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = sSalesOrderDetailId

            daMouldQty = New SqlDataAdapter(sCmd3)
            daMouldQty.Fill(dsMouldQty, "Mould")

            ''Mould Qty''
            nMoulding = dsMouldQty.Tables(0).Rows(0).Item(0)


            ''Finish Qty''
            Dim sCmd4 As New SqlCommand

            Dim daFinishQty As New SqlDataAdapter
            Dim dsFinishQty As New DataSet

            sCmd4.Connection = sCnn
            sCmd4.CommandText = "sgm_OrderOutstanding"
            sCmd4.CommandType = CommandType.StoredProcedure

            sCmd4.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELFINISHQTY"
            sCmd4.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = sSalesOrderDetailId

            daFinishQty = New SqlDataAdapter(sCmd4)
            daFinishQty.Fill(dsFinishQty, "Finish")
            ''Finish Qty''

            nMouldingWIP = dsMouldQty.Tables(0).Rows(0).Item(0) - dsFinishQty.Tables(0).Rows(0).Item(0)
            nFinishing = dsFinishQty.Tables(0).Rows(0).Item(0)

            ''PACK Qty''
            Dim sCmd5 As New SqlCommand

            Dim daPACKQty As New SqlDataAdapter
            Dim dsPACKQty As New DataSet

            sCmd5.Connection = sCnn
            sCmd5.CommandText = "sgm_OrderOutstanding"
            sCmd5.CommandType = CommandType.StoredProcedure

            sCmd5.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPACKQTY"
            sCmd5.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = sSalesOrderDetailId

            daPACKQty = New SqlDataAdapter(sCmd5)
            daPACKQty.Fill(dsPACKQty, "PACK")
            ''PACK Qty''

            nFinishingWIP = dsFinishQty.Tables(0).Rows(0).Item(0) - dsPACKQty.Tables(0).Rows(0).Item(0)
            nPacking = dsPACKQty.Tables(0).Rows(0).Item(0)

            ''DISP Qty''
            Dim sCmd6 As New SqlCommand

            Dim daDISPQty As New SqlDataAdapter
            Dim dsDISPQty As New DataSet

            sCmd6.Connection = sCnn
            sCmd6.CommandText = "sgm_OrderOutstanding"
            sCmd6.CommandType = CommandType.StoredProcedure

            sCmd6.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELDISPQTY"
            sCmd6.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = sSalesOrderDetailId

            daDISPQty = New SqlDataAdapter(sCmd6)
            daDISPQty.Fill(dsDISPQty, "DISP")
            ''DISP Qty''

            nInStock = dsPACKQty.Tables(0).Rows(0).Item(0) - dsDISPQty.Tables(0).Rows(0).Item(0)
            nDispatch = dsDISPQty.Tables(0).Rows(0).Item(0)

            If nDispatch >= nOrdQty Then
                nIsCompleted = 1
                nIsClosed = 1
            Else
                nIsCompleted = 0
                nIsClosed = 0
            End If

            UdpdatedOn = Format(Date.Now, "dd-MMM-yyyy")

            Dim sCmd7 As New SqlCommand

            Dim daCheckStatus As New SqlDataAdapter
            Dim dsCheckStatus As New DataSet

            sCmd7.Connection = sCnn
            sCmd7.CommandText = "sgm_OrderOutstanding"
            sCmd7.CommandType = CommandType.StoredProcedure


            sCmd7.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSOD"
            sCmd7.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = sSalesOrderDetailId

            daCheckStatus = New SqlDataAdapter(sCmd7)
            daCheckStatus.Fill(dsCheckStatus, "SOD")


            Dim sCmd8 As New SqlCommand

            Dim daUpdateStatus As New SqlDataAdapter
            Dim dsUpdateStatus As New DataSet

            sCmd8.Connection = sCnn
            sCmd8.CommandText = "sgm_OrderOutstanding"
            sCmd8.CommandType = CommandType.StoredProcedure

            If dsCheckStatus.Tables(0).Rows.Count = 0 Then
                sCmd8.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSSTATUS"
            ElseIf dsCheckStatus.Tables(0).Rows.Count = 1 Then
                sCmd8.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDSTATUS"
            Else
                MsgBox("Invalid Selection", MsgBoxStyle.Critical)
            End If

            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderDetailId", SqlDbType.VarChar)).Value() = sSalesOrderDetailId
            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderDate", SqlDbType.DateTime)).Value() = dSalesOrderDate
            sCmd8.Parameters.Add(New SqlParameter("@mCustomerName", SqlDbType.VarChar)).Value() = sCustomerName
            sCmd8.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = sSalesOrderNo
            sCmd8.Parameters.Add(New SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value() = sCustomerOrderNo
            sCmd8.Parameters.Add(New SqlParameter("@mSoleCode", SqlDbType.VarChar)).Value() = sSoleCode
            sCmd8.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() =  sSoleName
            sCmd8.Parameters.Add(New SqlParameter("@mColour", SqlDbType.VarChar)).Value() = sColour
            sCmd8.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = sCodification
            sCmd8.Parameters.Add(New SqlParameter("@mOrdQty", SqlDbType.Int)).Value() = nOrdQty
            sCmd8.Parameters.Add(New SqlParameter("@mMoulding", SqlDbType.Int)).Value() = nMoulding
            sCmd8.Parameters.Add(New SqlParameter("@mMouldingWIP", SqlDbType.Int)).Value() = nMouldingWIP
            sCmd8.Parameters.Add(New SqlParameter("@mFinishing", SqlDbType.Int)).Value() = nFinishing
            sCmd8.Parameters.Add(New SqlParameter("@mFinishingWIP", SqlDbType.Int)).Value() = nFinishingWIP
            sCmd8.Parameters.Add(New SqlParameter("@mPacking", SqlDbType.Int)).Value() = nPacking
            sCmd8.Parameters.Add(New SqlParameter("@mInStock", SqlDbType.Int)).Value() = nInStock
            sCmd8.Parameters.Add(New SqlParameter("@mDispatch", SqlDbType.Int)).Value() = nDispatch
            sCmd8.Parameters.Add(New SqlParameter("@mUdpdatedOn", SqlDbType.DateTime)).Value() = Format(Date.Now, "dd-MMM-yyyy")
            sCmd8.Parameters.Add(New SqlParameter("@mIsCompleted", SqlDbType.Bit)).Value() = nIsCompleted
            sCmd8.Parameters.Add(New SqlParameter("@mIsClosed", SqlDbType.Bit)).Value() = nIsClosed

            sCnn.Open()

            Dim sRes As String = sCmd8.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                setError(Val(sRes))
            End If
            sCnn.Close()

        Next
Aa:     'TODO To Erase after testing completieon
        Dim sCmd9 As New SqlCommand

        Dim daLoadOutstanding As New SqlDataAdapter
        Dim dsLoadOutstanding As New DataSet

        sCmd9.Connection = sCnn
        sCmd9.CommandText = "sgm_OrderOutstanding"
        sCmd9.CommandType = CommandType.StoredProcedure


        If mdlSGM.sOutstandingCriteria = "All" Then
            If mdlSGM.sSelectOption = "All Articles" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTALL"
            ElseIf mdlSGM.sSelectOption = "Customers Article - Article Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTCA"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
                sCmd9.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
            ElseIf mdlSGM.sSelectOption = "Customers Article - Codification Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTCC"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
                sCmd9.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCodification
            ElseIf mdlSGM.sSelectOption = "Customers Article" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTC"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
            ElseIf mdlSGM.sSelectOption = "Article Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTAW"
                sCmd9.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
            End If
        ElseIf mdlSGM.sOutstandingCriteria = "Completed" Then
            If mdlSGM.sSelectOption = "All Articles" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTST1"
            ElseIf mdlSGM.sSelectOption = "Customers Article - Article Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTCA1"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
                sCmd9.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
            ElseIf mdlSGM.sSelectOption = "Customers Article - Codification Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTCC1"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
                sCmd9.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCodification
            ElseIf mdlSGM.sSelectOption = "Customers Article" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTC1"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
            ElseIf mdlSGM.sSelectOption = "Article Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTAW1"
                sCmd9.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
            End If
        ElseIf mdlSGM.sOutstandingCriteria = "Pending" Then
            If mdlSGM.sSelectOption = "All Articles" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTST0"
            ElseIf mdlSGM.sSelectOption = "Customers Article - Article Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTCA0"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
                sCmd9.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
            ElseIf mdlSGM.sSelectOption = "Customers Article - Codification Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTCC0"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
                sCmd9.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCodification
            ElseIf mdlSGM.sSelectOption = "Customers Article" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTC0"
                sCmd9.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
            ElseIf mdlSGM.sSelectOption = "Article Wise" Then
                sCmd9.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTAW0"
                sCmd9.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle
            End If
        End If

        sCmd9.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd9.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        daLoadOutstanding = New SqlDataAdapter(sCmd9)
        daLoadOutstanding.Fill(dsLoadOutstanding, "OutStanding")
        Return dsLoadOutstanding.Tables(0)

        dsLoadOutstanding = Nothing
        sCnn.Close()

    End Function

    Public Function DelOutstanding4Print() As Boolean
        ''aasdfas()
        Dim sCmd1 As New SqlCommand

        Dim daDelOutstanding As New SqlDataAdapter
        Dim dsDelOutstanding As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "sgm_OrderOutstanding"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "DELOUTST"

        daDelOutstanding = New SqlDataAdapter(sCmd1)
        daDelOutstanding.Fill(dsDelOutstanding, "Date")
        dsDelOutstanding.AcceptChanges()

    End Function

    Public Function InsOutstanding4Print(ByVal oNV As strSolarOutstanding4SGM4Print) As DataTable
        ''aasdfas()
        Dim sCmd2 As New SqlCommand

        Dim daInsOutstanding As New SqlDataAdapter
        Dim dsInsOutstanding As New DataSet

        sCmd2.Connection = sCnn
        sCmd2.CommandText = "sgm_OrderOutstanding"
        sCmd2.CommandType = CommandType.StoredProcedure

        sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSOUTST"

        sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderDetailId", SqlDbType.VarChar)).Value() = oNV.SalesOrderDetailId
        sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderDate", SqlDbType.DateTime)).Value() = oNV.SalesOrderDate
        sCmd2.Parameters.Add(New SqlParameter("@mCustomerName", SqlDbType.VarChar)).Value() = oNV.CustomerName
        sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
        sCmd2.Parameters.Add(New SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value() = oNV.CustomerOrderNo
        sCmd2.Parameters.Add(New SqlParameter("@mSoleCode", SqlDbType.VarChar)).Value() = oNV.SoleCode
        sCmd2.Parameters.Add(New SqlParameter("@mSoleName", SqlDbType.VarChar)).Value() = oNV.SoleName
        sCmd2.Parameters.Add(New SqlParameter("@mColour", SqlDbType.VarChar)).Value() = oNV.Colour
        sCmd2.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = oNV.Codification
        sCmd2.Parameters.Add(New SqlParameter("@mOrdQty", SqlDbType.Int)).Value() = oNV.OrdQty
        sCmd2.Parameters.Add(New SqlParameter("@mMoulding", SqlDbType.Int)).Value() = oNV.Moulding
        sCmd2.Parameters.Add(New SqlParameter("@mMouldingWIP", SqlDbType.Int)).Value() = oNV.MouldingWIP
        sCmd2.Parameters.Add(New SqlParameter("@mFinishing", SqlDbType.Int)).Value() = oNV.Finishing
        sCmd2.Parameters.Add(New SqlParameter("@mFinishingWIP", SqlDbType.Int)).Value() = oNV.FinishingWIP
        sCmd2.Parameters.Add(New SqlParameter("@mPacking", SqlDbType.Int)).Value() = oNV.Packing
        sCmd2.Parameters.Add(New SqlParameter("@mInStock", SqlDbType.Int)).Value() = oNV.InStock
        sCmd2.Parameters.Add(New SqlParameter("@mDispatch", SqlDbType.Int)).Value() = oNV.Dispatch
        sCmd2.Parameters.Add(New SqlParameter("@mUdpdatedOn", SqlDbType.DateTime)).Value() = oNV.UpdatedOn
        sCmd2.Parameters.Add(New SqlParameter("@mIsCompleted", SqlDbType.Bit)).Value() = oNV.IsCompleted
        sCmd2.Parameters.Add(New SqlParameter("@mIsClosed", SqlDbType.Bit)).Value() = oNV.IsClosed

        sCnn.Open()

        Dim sRes As String = sCmd2.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            setError(Val(sRes))
        End If
        sCnn.Close()

    End Function


    Public Function LoadCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_OrderOutstanding"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCust"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticleofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_OrderOutstanding"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedCustomer = " ALL CUSTOMERS" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadAllArt"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadArt"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCodificationofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_InvoiceDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCode"
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()
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
