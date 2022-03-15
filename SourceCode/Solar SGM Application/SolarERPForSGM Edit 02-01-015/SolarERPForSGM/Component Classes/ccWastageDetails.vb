Option Explicit On
Imports System.Data.SqlClient

Public Class ccWastageDetails

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer
#End Region


#Region "Functions"

    Public Function LoadWastages(ByVal sType As String, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_WastageDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedOption = "All" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALL"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELFILTER"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mType", SqlDbType.VarChar)).Value() = sType
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadWastagesDetails(ByVal sType As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sTypeofMaterail As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_WastageDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedOption = "All" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALLDTLS"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELFILTERDTLS"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mType", SqlDbType.VarChar)).Value() = sType
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sCmd.Parameters.Add(New SqlParameter("@mTypeofMaterial", SqlDbType.VarChar)).Value() = sTypeofMaterail



        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadWastagesDetails2(ByVal sType As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sTypeofMaterail As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_WastageDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedOption = "All" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELALLDTLS2"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELFILTERDTLS"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mType", SqlDbType.VarChar)).Value() = sType
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sCmd.Parameters.Add(New SqlParameter("@mTypeofMaterial", SqlDbType.VarChar)).Value() = sTypeofMaterail



        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function RejectionIn(ByVal sOption As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sTypeofMaterail As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_WastageDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = sOption
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sCmd.Parameters.Add(New SqlParameter("@mMaterialSubTypeDescription", SqlDbType.VarChar)).Value() = sTypeofMaterail

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")

        If dsSelCustomer.Tables.Count Then
            Return dsSelCustomer.Tables(0)
        End If


        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function RejectionOut(ByVal sOption As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sTypeofMaterail As String, ByVal sROStatus As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_WastageDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = sOption
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate
        sCmd.Parameters.Add(New SqlParameter("@mMaterialSubTypeDescription", SqlDbType.VarChar)).Value() = sTypeofMaterail
        sCmd.Parameters.Add(New SqlParameter("@mROStatus", SqlDbType.VarChar)).Value() = sROStatus

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")

        If dsSelCustomer.Tables.Count Then
            Return dsSelCustomer.Tables(0)
        End If


        dsSelCustomer = Nothing
        sCnn.Close()

    End Function


    Public Function LoadMaterialType() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_WastageDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELMATTYPE"
    
        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

#End Region
End Class
