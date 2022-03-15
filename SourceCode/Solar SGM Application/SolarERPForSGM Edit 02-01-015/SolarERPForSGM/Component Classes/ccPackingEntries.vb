Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strReadytoDispatch

    Dim ID As String
    Dim invoiceno As String
    Dim InvoiceDate As Date
    Dim InvoiceSerialNo As String
    Dim buyer As String
    Dim shipper As String
    Dim SalesOrderNo As String
    Dim ArticleNo As String
    Dim rate As Decimal
    Dim quantity As Decimal
    Dim BuyerGroup As String
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As String
    Dim ModifiedDate As Date
    Dim EnteredOnMachineID As String
    Dim PackNo As String
    Dim JobCardNo As String
    Dim IsApproved As Integer
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim ModuleName As String
    Dim JobCardDetailID As String
    Dim InvoiceID As String
    Dim SalesOrderDetailID As String
    Dim Size01 As String
    Dim Quantity01 As Decimal
    Dim Size02 As String
    Dim Quantity02 As Decimal
    Dim Size03 As String
    Dim Quantity03 As Decimal
    Dim Size04 As String
    Dim Quantity04 As Decimal
    Dim Size05 As String
    Dim Quantity05 As Decimal
    Dim Size06 As String
    Dim Quantity06 As Decimal
    Dim Size07 As String
    Dim Quantity07 As Decimal
    Dim Size08 As String
    Dim Quantity08 As Decimal
    Dim Size09 As String
    Dim Quantity09 As Decimal
    Dim Size10 As String
    Dim Quantity10 As Decimal
    Dim Size11 As String
    Dim Quantity11 As Decimal
    Dim Size12 As String
    Dim Quantity12 As Decimal
    Dim Size13 As String
    Dim Quantity13 As Decimal
    Dim Size14 As String
    Dim Quantity14 As Decimal
    Dim Size15 As String
    Dim Quantity15 As Decimal
    Dim Size16 As String
    Dim Quantity16 As Decimal
    Dim Size17 As String
    Dim Quantity17 As Decimal
    Dim Size18 As String
    Dim Quantity18 As Decimal
    Dim SpoolId As String
    Dim SpoolDt As Date

End Structure

Public Structure strMDData
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
    Dim ScanDate As Date
    Dim FileName As String
    Dim NumberofBoxes As Integer
    Dim PerfectBoxes As Integer
End Structure

Public Structure strMDDataDtls

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
    Dim HID As String
    Dim BarCode As String
    Dim Status As String
    Dim FileName As String
    Dim CartonNo As Integer

End Structure
#End Region

Public Class ccPackingEntries

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

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

    Public Function LoadCustomers() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELBUYERS"

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadReadytoDispatchSummary(ByVal sBuyerCode As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELRDYTODISSUM"
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = sBuyerCode

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadReadytoDispatchDetails(ByVal sJobcardNo As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELRDYTODISDTL"
        sCmd.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = sJobcardNo

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function


    Public Function InsertReadytoDispatch(ByVal oNV As strReadytoDispatch) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelRTD As New SqlDataAdapter
        Dim dsSelRTD As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELRDYTODIS"
        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo

        daSelRTD = New SqlDataAdapter(sCmd)
        daSelRTD.Fill(dsSelRTD)

        If dsSelRTD.Tables(0).Rows.Count = 0 Then
            Dim sCmd2 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "proc_PackingEntries"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSRDYTODIS"

            sCmd2.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
            sCmd2.Parameters.Add(New SqlParameter("@minvoiceno", SqlDbType.VarChar)).Value() = oNV.invoiceno
            sCmd2.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = oNV.InvoiceDate
            sCmd2.Parameters.Add(New SqlParameter("@mInvoiceSerialNo", SqlDbType.VarChar)).Value() = oNV.InvoiceSerialNo
            sCmd2.Parameters.Add(New SqlParameter("@mbuyer", SqlDbType.VarChar)).Value() = oNV.buyer
            sCmd2.Parameters.Add(New SqlParameter("@mshipper", SqlDbType.VarChar)).Value() = oNV.shipper
            sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
            sCmd2.Parameters.Add(New SqlParameter("@mArticleNo", SqlDbType.VarChar)).Value() = oNV.ArticleNo
            sCmd2.Parameters.Add(New SqlParameter("@mrate", SqlDbType.Decimal)).Value() = oNV.rate
            sCmd2.Parameters.Add(New SqlParameter("@mquantity", SqlDbType.Decimal)).Value() = oNV.quantity
            sCmd2.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
            sCmd2.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
            sCmd2.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
            sCmd2.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
            sCmd2.Parameters.Add(New SqlParameter("@mPackNo", SqlDbType.VarChar)).Value() = oNV.PackNo
            sCmd2.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo
            sCmd2.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
            sCmd2.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
            sCmd2.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
            sCmd2.Parameters.Add(New SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value() = oNV.JobCardDetailID
            sCmd2.Parameters.Add(New SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value() = oNV.InvoiceID
            sCmd2.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = oNV.SalesOrderDetailID
            sCmd2.Parameters.Add(New SqlParameter("@mSize01", SqlDbType.VarChar)).Value() = oNV.Size01
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value() = oNV.Quantity01
            sCmd2.Parameters.Add(New SqlParameter("@mSize02", SqlDbType.VarChar)).Value() = oNV.Size02
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value() = oNV.Quantity02
            sCmd2.Parameters.Add(New SqlParameter("@mSize03", SqlDbType.VarChar)).Value() = oNV.Size03
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value() = oNV.Quantity03
            sCmd2.Parameters.Add(New SqlParameter("@mSize04", SqlDbType.VarChar)).Value() = oNV.Size04
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value() = oNV.Quantity04
            sCmd2.Parameters.Add(New SqlParameter("@mSize05", SqlDbType.VarChar)).Value() = oNV.Size05
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value() = oNV.Quantity05
            sCmd2.Parameters.Add(New SqlParameter("@mSize06", SqlDbType.VarChar)).Value() = oNV.Size06
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value() = oNV.Quantity06
            sCmd2.Parameters.Add(New SqlParameter("@mSize07", SqlDbType.VarChar)).Value() = oNV.Size07
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value() = oNV.Quantity07
            sCmd2.Parameters.Add(New SqlParameter("@mSize08", SqlDbType.VarChar)).Value() = oNV.Size08
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value() = oNV.Quantity08
            sCmd2.Parameters.Add(New SqlParameter("@mSize09", SqlDbType.VarChar)).Value() = oNV.Size09
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value() = oNV.Quantity09
            sCmd2.Parameters.Add(New SqlParameter("@mSize10", SqlDbType.VarChar)).Value() = oNV.Size10
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value() = oNV.Quantity10
            sCmd2.Parameters.Add(New SqlParameter("@mSize11", SqlDbType.VarChar)).Value() = oNV.Size11
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value() = oNV.Quantity11
            sCmd2.Parameters.Add(New SqlParameter("@mSize12", SqlDbType.VarChar)).Value() = oNV.Size12
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value() = oNV.Quantity12
            sCmd2.Parameters.Add(New SqlParameter("@mSize13", SqlDbType.VarChar)).Value() = oNV.Size13
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value() = oNV.Quantity13
            sCmd2.Parameters.Add(New SqlParameter("@mSize14", SqlDbType.VarChar)).Value() = oNV.Size14
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value() = oNV.Quantity14
            sCmd2.Parameters.Add(New SqlParameter("@mSize15", SqlDbType.VarChar)).Value() = oNV.Size15
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value() = oNV.Quantity15
            sCmd2.Parameters.Add(New SqlParameter("@mSize16", SqlDbType.VarChar)).Value() = oNV.Size16
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value() = oNV.Quantity16
            sCmd2.Parameters.Add(New SqlParameter("@mSize17", SqlDbType.VarChar)).Value() = oNV.Size17
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value() = oNV.Quantity17
            sCmd2.Parameters.Add(New SqlParameter("@mSize18", SqlDbType.VarChar)).Value() = oNV.Size18
            sCmd2.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value() = oNV.Quantity18
            sCmd2.Parameters.Add(New SqlParameter("@mSpoolId", SqlDbType.VarChar)).Value() = oNV.SpoolId
            sCmd2.Parameters.Add(New SqlParameter("@mSpoolDt", SqlDbType.DateTime)).Value() = oNV.SpoolDt

            sCnn.Open()

            Dim sRes As String = sCmd2.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
            Else
                'setError(Val(sRes))
            End If
            sCnn.Close()
        Else
            sID = dsSelRTD.Tables(0).Rows(0).Item("ID")
            nProducedQuantity = dsSelRTD.Tables(0).Rows(0).Item("Quantity") + oNV.quantity
            Dim sCmd1 As New SqlCommand

            Dim daInsOutstanding As New SqlDataAdapter
            Dim dsInsOutstanding As New DataSet

            sCmd1.Connection = sCnn
            sCmd1.CommandText = "proc_PackingEntries"
            sCmd1.CommandType = CommandType.StoredProcedure

            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDRDYTODIS"

            sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = sID
            sCmd1.Parameters.Add(New SqlParameter("@mquantity", SqlDbType.Decimal)).Value() = oNV.quantity + dsSelRTD.Tables(0).Rows(0).Item("Quantity")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value() = oNV.Quantity01 + dsSelRTD.Tables(0).Rows(0).Item("Quantity01")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value() = oNV.Quantity02 + dsSelRTD.Tables(0).Rows(0).Item("Quantity02")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value() = oNV.Quantity03 + dsSelRTD.Tables(0).Rows(0).Item("Quantity03")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value() = oNV.Quantity04 + dsSelRTD.Tables(0).Rows(0).Item("Quantity04")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value() = oNV.Quantity05 + dsSelRTD.Tables(0).Rows(0).Item("Quantity05")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value() = oNV.Quantity06 + dsSelRTD.Tables(0).Rows(0).Item("Quantity06")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value() = oNV.Quantity07 + dsSelRTD.Tables(0).Rows(0).Item("Quantity07")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value() = oNV.Quantity08 + dsSelRTD.Tables(0).Rows(0).Item("Quantity08")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value() = oNV.Quantity09 + dsSelRTD.Tables(0).Rows(0).Item("Quantity09")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value() = oNV.Quantity10 + dsSelRTD.Tables(0).Rows(0).Item("Quantity10")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value() = oNV.Quantity11 + dsSelRTD.Tables(0).Rows(0).Item("Quantity11")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value() = oNV.Quantity12 + dsSelRTD.Tables(0).Rows(0).Item("Quantity12")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value() = oNV.Quantity13 + dsSelRTD.Tables(0).Rows(0).Item("Quantity13")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value() = oNV.Quantity14 + dsSelRTD.Tables(0).Rows(0).Item("Quantity14")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value() = oNV.Quantity15 + dsSelRTD.Tables(0).Rows(0).Item("Quantity15")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value() = oNV.Quantity16 + dsSelRTD.Tables(0).Rows(0).Item("Quantity16")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value() = oNV.Quantity17 + dsSelRTD.Tables(0).Rows(0).Item("Quantity17")
            sCmd1.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value() = oNV.Quantity18 + dsSelRTD.Tables(0).Rows(0).Item("Quantity18")


            'daInsOutstanding.Fill(dsInsOutstanding)
            'dsInsOutstanding.AcceptChanges()

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

    ''

    Public Function UpdateReadytoDispatch(ByVal sJobcardNo As String) As DataTable
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelRTD As New SqlDataAdapter
        Dim dsSelRTD As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedArticle = "SOL-LEA" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPKGSUML"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPKGSUM"
        End If

        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo

        daSelRTD = New SqlDataAdapter(sCmd)
        daSelRTD.Fill(dsSelRTD)


        Dim sCmd1 As New SqlCommand

        Dim daInsOutstanding As New SqlDataAdapter
        Dim dsInsOutstanding As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "proc_PackingEntries"
        sCmd1.CommandType = CommandType.StoredProcedure


        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDRDYTODISBYJC"

        sCmd1.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = sJobcardNo
        sCmd1.Parameters.Add(New SqlParameter("@mquantity", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("PkgStock")
        sCmd1.Parameters.Add(New SqlParameter("@mPackNo", SqlDbType.VarChar)).Value() = dsSelRTD.Tables(0).Rows(0).Item("NoofCarton")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity01")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity02")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity03")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity04")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity05")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity06")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity07")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity08")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity09")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity10")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity11")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity12")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity13")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity14")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity15")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity16")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity17")
        sCmd1.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value() = dsSelRTD.Tables(0).Rows(0).Item("Quantity18")


        'daInsOutstanding.Fill(dsInsOutstanding)
        'dsInsOutstanding.AcceptChanges()

        sCnn.Open()

        Dim sRes As String = sCmd1.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            'setError(Val(sRes))
        End If
        sCnn.Close()


    End Function

    Public Function UpdatePackingDetail(ByVal sJobcardNo As String, ByVal nCartonNo As Integer, ByVal nPackingQty As Integer) As DataTable
        ''aasdfas()

        Dim sCmd1 As New SqlCommand

        Dim daUpdPkgDtl As New SqlDataAdapter
        Dim dsUpdPkgDtl As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "proc_PackingEntries"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPKGDTL"

        sCmd1.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = sJobcardNo
        sCmd1.Parameters.Add(New SqlParameter("@mCartonNo", SqlDbType.Int)).Value() = nCartonNo
        sCmd1.Parameters.Add(New SqlParameter("@mReadyToDispatchDate", SqlDbType.DateTime)).Value() = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        sCmd1.Parameters.Add(New SqlParameter("@mPackedQuantity", SqlDbType.Int)).Value() = nPackingQty

        sCnn.Open()

        Dim sRes As String = sCmd1.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            'setError(Val(sRes))
        End If
        sCnn.Close()


    End Function


    Public Function InsertMDData(ByVal oNV As strMDData) As Boolean
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelRTD As New SqlDataAdapter
        Dim dsSelRTD As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSMDDATA"
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        'sCmd.Parameters.Add(New SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value() = oNV.ExeVersionNo
        'sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
        'sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        ''sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
        'sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mScanDate", SqlDbType.DateTime)).Value() = oNV.ScanDate
        sCmd.Parameters.Add(New SqlParameter("@mFileName", SqlDbType.VarChar)).Value() = oNV.FileName
        sCmd.Parameters.Add(New SqlParameter("@mNumberofBoxes", SqlDbType.Int)).Value() = oNV.NumberofBoxes
        sCmd.Parameters.Add(New SqlParameter("@mPerfectBoxes", SqlDbType.Int)).Value() = oNV.PerfectBoxes

        'sCnn.Close()
        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False

    End Function

    Public Function InsertMDDataDetails(ByVal oNV As strMDDataDtls) As Boolean
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelRTD As New SqlDataAdapter
        Dim dsSelRTD As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSMDDATADTLS"
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        'sCmd.Parameters.Add(New SqlParameter("@mExeVersionNo", SqlDbType.VarChar)).Value() = oNV.ExeVersionNo
        'sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
        'sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        'sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
        'sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mHID", SqlDbType.VarChar)).Value() = oNV.HID
        sCmd.Parameters.Add(New SqlParameter("@mFileName", SqlDbType.VarChar)).Value() = oNV.FileName
        sCmd.Parameters.Add(New SqlParameter("@mBarCode", SqlDbType.VarChar)).Value() = oNV.BarCode
        sCmd.Parameters.Add(New SqlParameter("@mStatus", SqlDbType.VarChar)).Value() = oNV.Status
        sCmd.Parameters.Add(New SqlParameter("@mCartonNo", SqlDbType.Int)).Value() = oNV.CartonNo

        'sCnn.Close()
        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False

    End Function

    Public Function UpdateMDData(ByVal oNV As strMDData) As Boolean
        ''aasdfas()
        Dim sCmd As New SqlCommand
        Dim daSelRTD As New SqlDataAdapter
        Dim dsSelRTD As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDMDDATA"
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mNumberofBoxes", SqlDbType.Int)).Value() = oNV.NumberofBoxes
        sCmd.Parameters.Add(New SqlParameter("@mPerfectBoxes", SqlDbType.Int)).Value() = oNV.PerfectBoxes

        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False

    End Function

    Public Function LoadMDData() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELMCDATA"
        sCmd.Parameters.Add(New SqlParameter("@mScanDate", SqlDbType.DateTime)).Value() = Date.Now

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadMDDataDtls(ByVal sHID As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_PackingEntries"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELMCDATADTLS"
        sCmd.Parameters.Add(New SqlParameter("@mHID", SqlDbType.VarChar)).Value() = sHID

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

#End Region

End Class