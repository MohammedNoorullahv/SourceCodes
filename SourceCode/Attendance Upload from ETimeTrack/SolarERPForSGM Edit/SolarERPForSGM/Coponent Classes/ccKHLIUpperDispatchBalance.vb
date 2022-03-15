Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure StrUpperDispatchBalance

    Dim PKID As Integer
    Dim CustomerName As String
    Dim JobcardNo As String
    Dim ArticleName As String
    Dim ArticleColour As String
    Dim Description As String
    Dim DeliveryChallanNo As String
    Dim DeliveryDate As Date
    Dim SizeName As String
    Dim Size01 As String
    Dim Size02 As String
    Dim Size03 As String
    Dim Size04 As String
    Dim Size05 As String
    Dim Size06 As String
    Dim Size07 As String
    Dim Size08 As String
    Dim Size09 As String
    Dim Size10 As String
    Dim Size11 As String
    Dim Size12 As String
    Dim Size13 As String
    Dim Size14 As String
    Dim Size15 As String
    Dim Size16 As String
    Dim Size17 As String
    Dim Size18 As String
    Dim Size19 As String
    Dim Size20 As String
    Dim Size21 As String
    Dim Size22 As String
    Dim Size23 As String
    Dim Size24 As String
    Dim Qty01 As Integer
    Dim Qty02 As Integer
    Dim Qty03 As Integer
    Dim Qty04 As Integer
    Dim Qty05 As Integer
    Dim Qty06 As Integer
    Dim Qty07 As Integer
    Dim Qty08 As Integer
    Dim Qty09 As Integer
    Dim Qty10 As Integer
    Dim Qty11 As Integer
    Dim Qty12 As Integer
    Dim Qty13 As Integer
    Dim Qty14 As Integer
    Dim Qty15 As Integer
    Dim Qty16 As Integer
    Dim Qty17 As Integer
    Dim Qty18 As Integer
    Dim Qty19 As Integer
    Dim Qty20 As Integer
    Dim Qty21 As Integer
    Dim Qty22 As Integer
    Dim Qty23 As Integer
    Dim Qty24 As Integer
    Dim TotalQty As Integer
    Dim UpdatedOn As String
End Structure


#End Region

Public Class ccKHLIUpperDispatchBalance

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

    Dim sCustomerName As String
    Dim sJobcardNo As String
    Dim sArticleName As String
    Dim sArticleColour As String
    Dim sDescription As String
    Dim sDeliveryChallanNo As String
    Dim dDeliveryDate As Date
    Dim nTotalQty As Integer

    Dim myStrUpperDispatchBalance As New StrUpperDispatchBalance
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


    Public Function UpdateKHLIUpperDispatchBalance() As Boolean

        Dim sCmd1 As New SqlCommand

        Dim daSelJCAndSize As New SqlDataAdapter
        Dim dsSelJCAndSize As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "KHLI_UpperDispatchBalance"

        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELJCANDSIZE"

        daSelJCAndSize = New SqlDataAdapter(sCmd1)
        daSelJCAndSize.Fill(dsSelJCAndSize, "JCANDSIZE")

        Dim i As Integer = 0
        sSizeName = ""

        For i = 0 To dsSelJCAndSize.Tables(0).Rows.Count - 1
            nSizeCount = 0
            sSizeName = dsSelJCAndSize.Tables(0).Rows(i).Item("SizeName")
            sJobcardNo = dsSelJCAndSize.Tables(0).Rows(i).Item("JobcardNo")

            ''Size''
            Dim sCmd2 As New SqlCommand

            Dim daSelSize As New SqlDataAdapter
            Dim dsSelSize As New DataSet

            sCmd2.Connection = sCnn
            sCmd2.CommandText = "KHLI_UpperDispatchBalance"

            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELSIZE"
            sCmd2.Parameters.Add(New SqlParameter("@mSizeName", SqlDbType.VarChar)).Value() = sSizeName

            daSelSize = New SqlDataAdapter(sCmd2)
            daSelSize.Fill(dsSelSize, "SIZE")

            nSizeRowCount = dsSelSize.Tables(0).Rows.Count

            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize01 = dsSelSize.Tables(0).Rows(0).Item("Size") : Else : sSize01 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize02 = dsSelSize.Tables(0).Rows(1).Item("Size") : Else : sSize02 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize03 = dsSelSize.Tables(0).Rows(2).Item("Size") : Else : sSize03 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize04 = dsSelSize.Tables(0).Rows(3).Item("Size") : Else : sSize04 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize05 = dsSelSize.Tables(0).Rows(4).Item("Size") : Else : sSize05 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize06 = dsSelSize.Tables(0).Rows(5).Item("Size") : Else : sSize06 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize07 = dsSelSize.Tables(0).Rows(6).Item("Size") : Else : sSize07 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize08 = dsSelSize.Tables(0).Rows(7).Item("Size") : Else : sSize08 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize09 = dsSelSize.Tables(0).Rows(8).Item("Size") : Else : sSize09 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize10 = dsSelSize.Tables(0).Rows(9).Item("Size") : Else : sSize10 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize11 = dsSelSize.Tables(0).Rows(10).Item("Size") : Else : sSize11 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize12 = dsSelSize.Tables(0).Rows(11).Item("Size") : Else : sSize12 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize13 = dsSelSize.Tables(0).Rows(12).Item("Size") : Else : sSize13 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize14 = dsSelSize.Tables(0).Rows(13).Item("Size") : Else : sSize14 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize15 = dsSelSize.Tables(0).Rows(14).Item("Size") : Else : sSize15 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize16 = dsSelSize.Tables(0).Rows(15).Item("Size") : Else : sSize16 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize17 = dsSelSize.Tables(0).Rows(16).Item("Size") : Else : sSize17 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize18 = dsSelSize.Tables(0).Rows(17).Item("Size") : Else : sSize18 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize19 = dsSelSize.Tables(0).Rows(18).Item("Size") : Else : sSize19 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize20 = dsSelSize.Tables(0).Rows(19).Item("Size") : Else : sSize20 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize21 = dsSelSize.Tables(0).Rows(20).Item("Size") : Else : sSize21 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize22 = dsSelSize.Tables(0).Rows(21).Item("Size") : Else : sSize22 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize23 = dsSelSize.Tables(0).Rows(22).Item("Size") : Else : sSize23 = "" : End If
            nSizeCount = nSizeCount + 1 : If nSizeCount <= nSizeRowCount Then : sSize24 = dsSelSize.Tables(0).Rows(23).Item("Size") : Else : sSize24 = "" : End If
            ''Size

            ''JobcardQuantity''
            Dim sCmd3 As New SqlCommand

            Dim daSelJC As New SqlDataAdapter
            Dim dsSelJC As New DataSet

            sCmd3.Connection = sCnn
            sCmd3.CommandText = "KHLI_UpperDispatchBalance"

            sCmd3.CommandType = CommandType.StoredProcedure

            sCmd3.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELJC"
            sCmd3.Parameters.Add(New SqlParameter("@mjobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo

            daSelJC = New SqlDataAdapter(sCmd3)
            daSelJC.Fill(dsSelJC, "JC")

            If sJobcardNo = "" Or Microsoft.VisualBasic.Len(sJobcardNo) = 19 Then
                GoTo Aa
            End If
            sCustomerName = dsSelJC.Tables(0).Rows(0).Item("BuyerGroupCode")
            sArticleName = dsSelJC.Tables(0).Rows(0).Item("Article")
            sArticleColour = dsSelJC.Tables(0).Rows(0).Item("ColorCode")
            sDescription = "Jobcard Qty"
            sDeliveryChallanNo = dsSelJC.Tables(0).Rows(0).Item("CustomerOrderNo")
            dDeliveryDate = dsSelJC.Tables(0).Rows(0).Item("JobcardDate")
            nTotalQty = dsSelJC.Tables(0).Rows(0).Item("Quantity")

            sJCSize01 = dsSelJC.Tables(0).Rows(0).Item("Size01").ToString : nJCQty01 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity01").ToString)
            sJCSize02 = dsSelJC.Tables(0).Rows(0).Item("Size02").ToString : nJCQty02 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity02").ToString)
            sJCSize03 = dsSelJC.Tables(0).Rows(0).Item("Size03").ToString : nJCQty03 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity03").ToString)
            sJCSize04 = dsSelJC.Tables(0).Rows(0).Item("Size04").ToString : nJCQty04 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity04").ToString)
            sJCSize05 = dsSelJC.Tables(0).Rows(0).Item("Size05").ToString : nJCQty05 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity05").ToString)
            sJCSize06 = dsSelJC.Tables(0).Rows(0).Item("Size06").ToString : nJCQty06 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity06").ToString)
            sJCSize07 = dsSelJC.Tables(0).Rows(0).Item("Size07").ToString : nJCQty07 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity07").ToString)
            sJCSize08 = dsSelJC.Tables(0).Rows(0).Item("Size08").ToString : nJCQty08 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity08").ToString)
            sJCSize09 = dsSelJC.Tables(0).Rows(0).Item("Size09").ToString : nJCQty09 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity09").ToString)
            sJCSize10 = dsSelJC.Tables(0).Rows(0).Item("Size10").ToString : nJCQty10 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity10").ToString)
            sJCSize11 = dsSelJC.Tables(0).Rows(0).Item("Size11").ToString : nJCQty11 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity11").ToString)
            sJCSize12 = dsSelJC.Tables(0).Rows(0).Item("Size12").ToString : nJCQty12 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity12").ToString)
            sJCSize13 = dsSelJC.Tables(0).Rows(0).Item("Size13").ToString : nJCQty13 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity13").ToString)
            sJCSize14 = dsSelJC.Tables(0).Rows(0).Item("Size14").ToString : nJCQty14 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity14").ToString)
            sJCSize15 = dsSelJC.Tables(0).Rows(0).Item("Size15").ToString : nJCQty15 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity15").ToString)
            sJCSize16 = dsSelJC.Tables(0).Rows(0).Item("Size16").ToString : nJCQty16 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity16").ToString)
            sJCSize17 = dsSelJC.Tables(0).Rows(0).Item("Size17").ToString : nJCQty17 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity17").ToString)
            sJCSize18 = dsSelJC.Tables(0).Rows(0).Item("Size18").ToString : nJCQty18 = Val(dsSelJC.Tables(0).Rows(0).Item("Quantity18").ToString)

            If sJCSize01 <> "" Then : MatchQuantity(sJCSize01, nJCQty01) : End If
            If sJCSize02 <> "" Then : MatchQuantity(sJCSize02, nJCQty02) : End If
            If sJCSize03 <> "" Then : MatchQuantity(sJCSize03, nJCQty03) : End If
            If sJCSize04 <> "" Then : MatchQuantity(sJCSize04, nJCQty04) : End If
            If sJCSize05 <> "" Then : MatchQuantity(sJCSize05, nJCQty05) : End If
            If sJCSize06 <> "" Then : MatchQuantity(sJCSize06, nJCQty06) : End If
            If sJCSize07 <> "" Then : MatchQuantity(sJCSize07, nJCQty07) : End If
            If sJCSize08 <> "" Then : MatchQuantity(sJCSize08, nJCQty08) : End If
            If sJCSize09 <> "" Then : MatchQuantity(sJCSize09, nJCQty09) : End If
            If sJCSize10 <> "" Then : MatchQuantity(sJCSize10, nJCQty10) : End If
            If sJCSize11 <> "" Then : MatchQuantity(sJCSize11, nJCQty11) : End If
            If sJCSize12 <> "" Then : MatchQuantity(sJCSize12, nJCQty12) : End If
            If sJCSize13 <> "" Then : MatchQuantity(sJCSize13, nJCQty13) : End If
            If sJCSize14 <> "" Then : MatchQuantity(sJCSize14, nJCQty14) : End If
            If sJCSize15 <> "" Then : MatchQuantity(sJCSize15, nJCQty15) : End If
            If sJCSize16 <> "" Then : MatchQuantity(sJCSize16, nJCQty16) : End If
            If sJCSize17 <> "" Then : MatchQuantity(sJCSize17, nJCQty17) : End If
            If sJCSize18 <> "" Then : MatchQuantity(sJCSize18, nJCQty18) : End If
            ''Jobcard Quantity''

            ''Insert Jobcard Quantity''
            'myStrUpperDispatchBalance.PKID = ""
            myStrUpperDispatchBalance.CustomerName = sCustomerName
            myStrUpperDispatchBalance.JobcardNo = sJobcardNo
            myStrUpperDispatchBalance.ArticleName = sArticleName
            myStrUpperDispatchBalance.ArticleColour = sArticleColour
            myStrUpperDispatchBalance.Description = sDescription
            myStrUpperDispatchBalance.DeliveryChallanNo = sDeliveryChallanNo
            myStrUpperDispatchBalance.DeliveryDate = dDeliveryDate
            myStrUpperDispatchBalance.SizeName = sSizeName
            myStrUpperDispatchBalance.Size01 = sSize01
            myStrUpperDispatchBalance.Size02 = sSize02
            myStrUpperDispatchBalance.Size03 = sSize03
            myStrUpperDispatchBalance.Size04 = sSize04
            myStrUpperDispatchBalance.Size05 = sSize05
            myStrUpperDispatchBalance.Size06 = sSize06
            myStrUpperDispatchBalance.Size07 = sSize07
            myStrUpperDispatchBalance.Size08 = sSize08
            myStrUpperDispatchBalance.Size09 = sSize09
            myStrUpperDispatchBalance.Size10 = sSize10
            myStrUpperDispatchBalance.Size11 = sSize11
            myStrUpperDispatchBalance.Size12 = sSize12
            myStrUpperDispatchBalance.Size13 = sSize13
            myStrUpperDispatchBalance.Size14 = sSize14
            myStrUpperDispatchBalance.Size15 = sSize15
            myStrUpperDispatchBalance.Size16 = sSize16
            myStrUpperDispatchBalance.Size17 = sSize17
            myStrUpperDispatchBalance.Size18 = sSize18
            myStrUpperDispatchBalance.Size19 = sSize19
            myStrUpperDispatchBalance.Size20 = sSize20
            myStrUpperDispatchBalance.Size21 = sSize21
            myStrUpperDispatchBalance.Size22 = sSize22
            myStrUpperDispatchBalance.Size23 = sSize23
            myStrUpperDispatchBalance.Size24 = sSize24
            myStrUpperDispatchBalance.Qty01 = nOSTQty01
            myStrUpperDispatchBalance.Qty02 = nOSTQty02
            myStrUpperDispatchBalance.Qty03 = nOSTQty03
            myStrUpperDispatchBalance.Qty04 = nOSTQty04
            myStrUpperDispatchBalance.Qty05 = nOSTQty05
            myStrUpperDispatchBalance.Qty06 = nOSTQty06
            myStrUpperDispatchBalance.Qty07 = nOSTQty07
            myStrUpperDispatchBalance.Qty08 = nOSTQty08
            myStrUpperDispatchBalance.Qty09 = nOSTQty09
            myStrUpperDispatchBalance.Qty10 = nOSTQty10
            myStrUpperDispatchBalance.Qty11 = nOSTQty11
            myStrUpperDispatchBalance.Qty12 = nOSTQty12
            myStrUpperDispatchBalance.Qty13 = nOSTQty13
            myStrUpperDispatchBalance.Qty14 = nOSTQty14
            myStrUpperDispatchBalance.Qty15 = nOSTQty15
            myStrUpperDispatchBalance.Qty16 = nOSTQty16
            myStrUpperDispatchBalance.Qty17 = nOSTQty17
            myStrUpperDispatchBalance.Qty18 = nOSTQty18
            myStrUpperDispatchBalance.Qty19 = nOSTQty19
            myStrUpperDispatchBalance.Qty20 = nOSTQty20
            myStrUpperDispatchBalance.Qty21 = nOSTQty21
            myStrUpperDispatchBalance.Qty22 = nOSTQty22
            myStrUpperDispatchBalance.Qty23 = nOSTQty23
            myStrUpperDispatchBalance.Qty24 = nOSTQty24
            myStrUpperDispatchBalance.TotalQty = nTotalQty
            myStrUpperDispatchBalance.UpdatedOn = Format(Date.Now, "dd-MMM-yyyy-hh:mm:ss:tt")

            InserUpperDispatchBalance(myStrUpperDispatchBalance)
            ''Insert Jobcard Quantity''

            ClearQuantities()

            ''Delivery Challan Quantities''
            Dim sCmd4 As New SqlCommand

            Dim daSelDCCount As New SqlDataAdapter
            Dim dsSelDCCount As New DataSet

            sCmd4.Connection = sCnn
            sCmd4.CommandText = "KHLI_UpperDispatchBalance"
            sCmd4.CommandType = CommandType.StoredProcedure

            sCmd4.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELDCCOUNT"
            sCmd4.Parameters.Add(New SqlParameter("@mjobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo

            daSelDCCount = New SqlDataAdapter(sCmd4)
            daSelDCCount.Fill(dsSelDCCount, "DCCount")

            Dim j As Integer = 0
            For j = 0 To dsSelDCCount.Tables(0).Rows.Count - 1
                sDCNo = dsSelDCCount.Tables(0).Rows(j).Item("SupplierRefNo")
                nTotalQty = dsSelDCCount.Tables(0).Rows(j).Item("DCQty")


                Dim sCmd5 As New SqlCommand

                Dim daSelDCQty As New SqlDataAdapter
                Dim dsSelDCQty As New DataSet

                sCmd5.Connection = sCnn
                sCmd5.CommandText = "KHLI_UpperDispatchBalance"
                sCmd5.CommandType = CommandType.StoredProcedure

                sCmd5.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELDCQTY"
                sCmd5.Parameters.Add(New SqlParameter("@mjobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo
                sCmd5.Parameters.Add(New SqlParameter("@mDCNO", SqlDbType.VarChar)).Value() = sDCNo

                daSelDCQty = New SqlDataAdapter(sCmd5)
                daSelDCQty.Fill(dsSelDCQty, "DCQty")

                Dim k As Integer = 0
                For k = 0 To dsSelDCQty.Tables(0).Rows.Count - 1
                    sJCSize01 = dsSelDCQty.Tables(0).Rows(k).Item("Size") : nJCQty01 = dsSelDCQty.Tables(0).Rows(k).Item("IssueQuantity")

                    If sJCSize01 <> "" Then : MatchQuantity(sJCSize01, nJCQty01) : End If

                Next
                sDescription = "DC Qty"
                sDeliveryChallanNo = sDCNo

                myStrUpperDispatchBalance.Description = sDescription
                myStrUpperDispatchBalance.DeliveryChallanNo = sDeliveryChallanNo
                myStrUpperDispatchBalance.Qty01 = nOSTQty01
                myStrUpperDispatchBalance.Qty02 = nOSTQty02
                myStrUpperDispatchBalance.Qty03 = nOSTQty03
                myStrUpperDispatchBalance.Qty04 = nOSTQty04
                myStrUpperDispatchBalance.Qty05 = nOSTQty05
                myStrUpperDispatchBalance.Qty06 = nOSTQty06
                myStrUpperDispatchBalance.Qty07 = nOSTQty07
                myStrUpperDispatchBalance.Qty08 = nOSTQty08
                myStrUpperDispatchBalance.Qty09 = nOSTQty09
                myStrUpperDispatchBalance.Qty10 = nOSTQty10
                myStrUpperDispatchBalance.Qty11 = nOSTQty11
                myStrUpperDispatchBalance.Qty12 = nOSTQty12
                myStrUpperDispatchBalance.Qty13 = nOSTQty13
                myStrUpperDispatchBalance.Qty14 = nOSTQty14
                myStrUpperDispatchBalance.Qty15 = nOSTQty15
                myStrUpperDispatchBalance.Qty16 = nOSTQty16
                myStrUpperDispatchBalance.Qty17 = nOSTQty17
                myStrUpperDispatchBalance.Qty18 = nOSTQty18
                myStrUpperDispatchBalance.Qty19 = nOSTQty19
                myStrUpperDispatchBalance.Qty20 = nOSTQty20
                myStrUpperDispatchBalance.Qty21 = nOSTQty21
                myStrUpperDispatchBalance.Qty22 = nOSTQty22
                myStrUpperDispatchBalance.Qty23 = nOSTQty23
                myStrUpperDispatchBalance.Qty24 = nOSTQty24
                myStrUpperDispatchBalance.TotalQty = nTotalQty
                myStrUpperDispatchBalance.UpdatedOn = Format(Date.Now, "dd-MMM-yyyy-hh:mm:ss:tt")

                InserUpperDispatchBalance(myStrUpperDispatchBalance)
                ClearQuantities()

                
            Next

            Dim sCmd6 As New SqlCommand

            Dim daSelBalQty As New SqlDataAdapter
            Dim dsSelBalQty As New DataSet

            sCmd6.Connection = sCnn
            sCmd6.CommandText = "KHLI_UpperDispatchBalance"
            sCmd6.CommandType = CommandType.StoredProcedure

            sCmd6.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADBALQTY"
            sCmd6.Parameters.Add(New SqlParameter("@mjobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo

            daSelBalQty = New SqlDataAdapter(sCmd6)
            daSelBalQty.Fill(dsSelBalQty, "DCQty")

            sDescription = "Bal Qty"
            sDeliveryChallanNo = ""

            myStrUpperDispatchBalance.Description = sDescription
            myStrUpperDispatchBalance.DeliveryChallanNo = sDeliveryChallanNo
            myStrUpperDispatchBalance.Qty01 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty01").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty01").ToString)
            myStrUpperDispatchBalance.Qty02 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty02").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty02").ToString)
            myStrUpperDispatchBalance.Qty03 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty03").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty03").ToString)
            myStrUpperDispatchBalance.Qty04 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty04").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty04").ToString)
            myStrUpperDispatchBalance.Qty05 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty05").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty05").ToString)
            myStrUpperDispatchBalance.Qty06 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty06").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty06").ToString)
            myStrUpperDispatchBalance.Qty07 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty07").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty07").ToString)
            myStrUpperDispatchBalance.Qty08 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty08").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty08").ToString)
            myStrUpperDispatchBalance.Qty09 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty09").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty09").ToString)
            myStrUpperDispatchBalance.Qty10 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty10").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty10").ToString)
            myStrUpperDispatchBalance.Qty11 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty11").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty11").ToString)
            myStrUpperDispatchBalance.Qty12 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty12").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty12").ToString)
            myStrUpperDispatchBalance.Qty13 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty13").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty13").ToString)
            myStrUpperDispatchBalance.Qty14 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty14").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty14").ToString)
            myStrUpperDispatchBalance.Qty15 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty15").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty15").ToString)
            myStrUpperDispatchBalance.Qty16 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty16").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty16").ToString)
            myStrUpperDispatchBalance.Qty17 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty17").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty17").ToString)
            myStrUpperDispatchBalance.Qty18 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty18").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty18").ToString)
            myStrUpperDispatchBalance.Qty19 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty19").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty19").ToString)
            myStrUpperDispatchBalance.Qty20 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty20").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty20").ToString)
            myStrUpperDispatchBalance.Qty21 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty21").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty21").ToString)
            myStrUpperDispatchBalance.Qty22 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty22").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty22").ToString)
            myStrUpperDispatchBalance.Qty23 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty23").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty23").ToString)
            myStrUpperDispatchBalance.Qty24 = Val(dsSelBalQty.Tables(0).Rows(0).Item("Qty24").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("Qty24").ToString)
            myStrUpperDispatchBalance.TotalQty = Val(dsSelBalQty.Tables(0).Rows(0).Item("TotalQty").ToString) - Val(dsSelBalQty.Tables(0).Rows(1).Item("TotalQty").ToString)
            myStrUpperDispatchBalance.UpdatedOn = Format(Date.Now, "dd-MMM-yyyy-hh:mm:ss:tt")

            InserUpperDispatchBalance(myStrUpperDispatchBalance)
            ClearQuantities()
            ''Delivery Challan Quantities''
aa:
        Next
        MsgBox("CompleteD")

    End Function

    Private Function MatchQuantity(ByVal sSize As String, ByVal nQuantity As Integer) As Boolean
        If sSize = sSize01 Then : nOSTQty01 = nQuantity
        ElseIf sSize = sSize02 Then : nOSTQty02 = nQuantity
        ElseIf sSize = sSize03 Then : nOSTQty03 = nQuantity
        ElseIf sSize = sSize04 Then : nOSTQty04 = nQuantity
        ElseIf sSize = sSize05 Then : nOSTQty05 = nQuantity
        ElseIf sSize = sSize06 Then : nOSTQty06 = nQuantity
        ElseIf sSize = sSize07 Then : nOSTQty07 = nQuantity
        ElseIf sSize = sSize08 Then : nOSTQty08 = nQuantity
        ElseIf sSize = sSize09 Then : nOSTQty09 = nQuantity
        ElseIf sSize = sSize10 Then : nOSTQty10 = nQuantity
        ElseIf sSize = sSize11 Then : nOSTQty11 = nQuantity
        ElseIf sSize = sSize12 Then : nOSTQty12 = nQuantity
        ElseIf sSize = sSize13 Then : nOSTQty13 = nQuantity
        ElseIf sSize = sSize14 Then : nOSTQty14 = nQuantity
        ElseIf sSize = sSize15 Then : nOSTQty15 = nQuantity
        ElseIf sSize = sSize16 Then : nOSTQty16 = nQuantity
        ElseIf sSize = sSize17 Then : nOSTQty17 = nQuantity
        ElseIf sSize = sSize18 Then : nOSTQty18 = nQuantity
        ElseIf sSize = sSize19 Then : nOSTQty19 = nQuantity
        ElseIf sSize = sSize20 Then : nOSTQty20 = nQuantity
        ElseIf sSize = sSize21 Then : nOSTQty21 = nQuantity
        ElseIf sSize = sSize22 Then : nOSTQty22 = nQuantity
        ElseIf sSize = sSize23 Then : nOSTQty23 = nQuantity
        ElseIf sSize = sSize24 Then : nOSTQty24 = nQuantity
        End If
    End Function

    Private Function InserUpperDispatchBalance(ByVal oNV As StrUpperDispatchBalance) As Boolean

        Dim sCmd As New SqlCommand

        Dim daInsUDB As New SqlDataAdapter
        Dim dsInsUDB As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_UpperDispatchBalance"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERT"

        'sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = oNV
        sCmd.Parameters.Add(New SqlParameter("@mCustomerName", SqlDbType.VarChar)).Value() = oNV.CustomerName
        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = oNV.JobcardNo
        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = oNV.ArticleName
        sCmd.Parameters.Add(New SqlParameter("@mArticleColour", SqlDbType.VarChar)).Value() = oNV.ArticleColour
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = oNV.Description
        sCmd.Parameters.Add(New SqlParameter("@mDeliveryChallanNo", SqlDbType.VarChar)).Value() = oNV.DeliveryChallanNo
        sCmd.Parameters.Add(New SqlParameter("@mDeliveryDate", SqlDbType.Date)).Value() = oNV.DeliveryDate
        sCmd.Parameters.Add(New SqlParameter("@mSizeName", SqlDbType.VarChar)).Value() = oNV.SizeName
        sCmd.Parameters.Add(New SqlParameter("@mSize01", SqlDbType.VarChar)).Value() = oNV.Size01
        sCmd.Parameters.Add(New SqlParameter("@mSize02", SqlDbType.VarChar)).Value() = oNV.Size02
        sCmd.Parameters.Add(New SqlParameter("@mSize03", SqlDbType.VarChar)).Value() = oNV.Size03
        sCmd.Parameters.Add(New SqlParameter("@mSize04", SqlDbType.VarChar)).Value() = oNV.Size04
        sCmd.Parameters.Add(New SqlParameter("@mSize05", SqlDbType.VarChar)).Value() = oNV.Size05
        sCmd.Parameters.Add(New SqlParameter("@mSize06", SqlDbType.VarChar)).Value() = oNV.Size06
        sCmd.Parameters.Add(New SqlParameter("@mSize07", SqlDbType.VarChar)).Value() = oNV.Size07
        sCmd.Parameters.Add(New SqlParameter("@mSize08", SqlDbType.VarChar)).Value() = oNV.Size08
        sCmd.Parameters.Add(New SqlParameter("@mSize09", SqlDbType.VarChar)).Value() = oNV.Size09
        sCmd.Parameters.Add(New SqlParameter("@mSize10", SqlDbType.VarChar)).Value() = oNV.Size10
        sCmd.Parameters.Add(New SqlParameter("@mSize11", SqlDbType.VarChar)).Value() = oNV.Size11
        sCmd.Parameters.Add(New SqlParameter("@mSize12", SqlDbType.VarChar)).Value() = oNV.Size12
        sCmd.Parameters.Add(New SqlParameter("@mSize13", SqlDbType.VarChar)).Value() = oNV.Size13
        sCmd.Parameters.Add(New SqlParameter("@mSize14", SqlDbType.VarChar)).Value() = oNV.Size14
        sCmd.Parameters.Add(New SqlParameter("@mSize15", SqlDbType.VarChar)).Value() = oNV.Size15
        sCmd.Parameters.Add(New SqlParameter("@mSize16", SqlDbType.VarChar)).Value() = oNV.Size16
        sCmd.Parameters.Add(New SqlParameter("@mSize17", SqlDbType.VarChar)).Value() = oNV.Size17
        sCmd.Parameters.Add(New SqlParameter("@mSize18", SqlDbType.VarChar)).Value() = oNV.Size18
        sCmd.Parameters.Add(New SqlParameter("@mSize19", SqlDbType.VarChar)).Value() = oNV.Size19
        sCmd.Parameters.Add(New SqlParameter("@mSize20", SqlDbType.VarChar)).Value() = oNV.Size20
        sCmd.Parameters.Add(New SqlParameter("@mSize21", SqlDbType.VarChar)).Value() = oNV.Size21
        sCmd.Parameters.Add(New SqlParameter("@mSize22", SqlDbType.VarChar)).Value() = oNV.Size22
        sCmd.Parameters.Add(New SqlParameter("@mSize23", SqlDbType.VarChar)).Value() = oNV.Size23
        sCmd.Parameters.Add(New SqlParameter("@mSize24", SqlDbType.VarChar)).Value() = oNV.Size24
        sCmd.Parameters.Add(New SqlParameter("@mQty01", SqlDbType.Int)).Value() = oNV.Qty01
        sCmd.Parameters.Add(New SqlParameter("@mQty02", SqlDbType.Int)).Value() = oNV.Qty02
        sCmd.Parameters.Add(New SqlParameter("@mQty03", SqlDbType.Int)).Value() = oNV.Qty03
        sCmd.Parameters.Add(New SqlParameter("@mQty04", SqlDbType.Int)).Value() = oNV.Qty04
        sCmd.Parameters.Add(New SqlParameter("@mQty05", SqlDbType.Int)).Value() = oNV.Qty05
        sCmd.Parameters.Add(New SqlParameter("@mQty06", SqlDbType.Int)).Value() = oNV.Qty06
        sCmd.Parameters.Add(New SqlParameter("@mQty07", SqlDbType.Int)).Value() = oNV.Qty07
        sCmd.Parameters.Add(New SqlParameter("@mQty08", SqlDbType.Int)).Value() = oNV.Qty08
        sCmd.Parameters.Add(New SqlParameter("@mQty09", SqlDbType.Int)).Value() = oNV.Qty09
        sCmd.Parameters.Add(New SqlParameter("@mQty10", SqlDbType.Int)).Value() = oNV.Qty10
        sCmd.Parameters.Add(New SqlParameter("@mQty11", SqlDbType.Int)).Value() = oNV.Qty11
        sCmd.Parameters.Add(New SqlParameter("@mQty12", SqlDbType.Int)).Value() = oNV.Qty12
        sCmd.Parameters.Add(New SqlParameter("@mQty13", SqlDbType.Int)).Value() = oNV.Qty13
        sCmd.Parameters.Add(New SqlParameter("@mQty14", SqlDbType.Int)).Value() = oNV.Qty14
        sCmd.Parameters.Add(New SqlParameter("@mQty15", SqlDbType.Int)).Value() = oNV.Qty15
        sCmd.Parameters.Add(New SqlParameter("@mQty16", SqlDbType.Int)).Value() = oNV.Qty16
        sCmd.Parameters.Add(New SqlParameter("@mQty17", SqlDbType.Int)).Value() = oNV.Qty17
        sCmd.Parameters.Add(New SqlParameter("@mQty18", SqlDbType.Int)).Value() = oNV.Qty18
        sCmd.Parameters.Add(New SqlParameter("@mQty19", SqlDbType.Int)).Value() = oNV.Qty19
        sCmd.Parameters.Add(New SqlParameter("@mQty20", SqlDbType.Int)).Value() = oNV.Qty20
        sCmd.Parameters.Add(New SqlParameter("@mQty21", SqlDbType.Int)).Value() = oNV.Qty21
        sCmd.Parameters.Add(New SqlParameter("@mQty22", SqlDbType.Int)).Value() = oNV.Qty22
        sCmd.Parameters.Add(New SqlParameter("@mQty23", SqlDbType.Int)).Value() = oNV.Qty23
        sCmd.Parameters.Add(New SqlParameter("@mQty24", SqlDbType.Int)).Value() = oNV.Qty24
        sCmd.Parameters.Add(New SqlParameter("@mTotalQty", SqlDbType.Int)).Value() = oNV.TotalQty
        sCmd.Parameters.Add(New SqlParameter("@mUpdatedOn", SqlDbType.VarChar)).Value() = oNV.UpdatedOn

        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            setError(Val(sRes))
        End If
        sCnn.Close()



    End Function
    Dim sSizeName, sDCNo As String
    Dim sSize01, sSize02, sSize03, sSize04, sSize05, sSize06, sSize07, sSize08, sSize09, sSize10, sSize11, sSize12 As String
    Dim sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize19, sSize20, sSize21, sSize22, sSize23, sSize24 As String
    Dim nSizeCount, nSizeRowCount As Integer

    Dim sJCSize01, sJCSize02, sJCSize03, sJCSize04, sJCSize05, sJCSize06, sJCSize07, sJCSize08, sJCSize09, sJCSize10, sJCSize11, sJCSize12 As String
    Dim sJCSize13, sJCSize14, sJCSize15, sJCSize16, sJCSize17, sJCSize18, sJCSize19, sJCSize20, sJCSize21, sJCSize22, sJCSize23, sJCSize24 As String

    Dim nJCQty01, nJCQty02, nJCQty03, nJCQty04, nJCQty05, nJCQty06, nJCQty07, nJCQty08, nJCQty09, nJCQty10, nJCQty11, nJCQty12 As Integer
    Dim nJCQty13, nJCQty14, nJCQty15, nJCQty16, nJCQty17, nJCQty18, nJCQty19, nJCQty20, nJCQty21, nJCQty22, nJCQty23, nJCQty24 As Integer
    Dim nJCTotalQty As Integer

    Dim nOSTQty01, nOSTQty02, nOSTQty03, nOSTQty04, nOSTQty05, nOSTQty06, nOSTQty07, nOSTQty08, nOSTQty09, nOSTQty10, nOSTQty11, nOSTQty12 As Integer
    Dim nOSTQty13, nOSTQty14, nOSTQty15, nOSTQty16, nOSTQty17, nOSTQty18, nOSTQty19, nOSTQty20, nOSTQty21, nOSTQty22, nOSTQty23, nOSTQty24 As Integer


    

    Private Sub ClearQuantities()

        nOSTQty01 = 0 : nOSTQty02 = 0 : nOSTQty03 = 0 : nOSTQty04 = 0 : nOSTQty05 = 0 : nOSTQty06 = 0 : nOSTQty07 = 0 : nOSTQty08 = 0 : nOSTQty09 = 0 : nOSTQty10 = 0 : nOSTQty11 = 0 : nOSTQty12 = 0
        nOSTQty13 = 0 : nOSTQty14 = 0 : nOSTQty15 = 0 : nOSTQty16 = 0 : nOSTQty17 = 0 : nOSTQty18 = 0 : nOSTQty19 = 0 : nOSTQty20 = 0 : nOSTQty21 = 0 : nOSTQty22 = 0 : nOSTQty23 = 0 : nOSTQty24 = 0

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
