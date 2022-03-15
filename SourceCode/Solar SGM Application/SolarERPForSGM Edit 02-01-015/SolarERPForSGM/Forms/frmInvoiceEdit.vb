Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO

Public Class frmInvoiceEdit

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrAudit As String = Global.SolarERPForSGM.My.Settings.AHGroupAudit '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnnAudit As New SqlConnection(sConstrAudit)

    Dim keyascii As Integer

    Dim myCCInvoice As New ccInvoice
    Dim mystrInvoice As strInvoice
#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        frmInvoiceGeneration.Show() : frmInvoiceGeneration.BringToFront()
    End Sub

    Private Sub frmInvoiceEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        LoadSignatory()
        LoadInvoiceInfo()
        LoadCustomer()
    End Sub
    Dim sBuyerGroup As String
    Private Sub LoadInvoiceInfo()
        Dim daSelInvoice As New SqlDataAdapter("Select * from Invoice Where InvoiceNo = '" & mdlSGM.sInvoiceNo & "'", sConstr)
        Dim dsSelInvoice As New DataSet
        daSelInvoice.Fill(dsSelInvoice)

        If dsSelInvoice.Tables(0).Rows.Count <= 0 Then
            MsgBox("Invalid Invoice No.", MsgBoxStyle.Critical)
        Else




            tbAREDate.Text = dsSelInvoice.Tables(0).Rows(0).Item("AREDate").ToString()
            tbARENo.Text = dsSelInvoice.Tables(0).Rows(0).Item("ARENo").ToString()
            tbAuthSignDesi.Text = dsSelInvoice.Tables(0).Rows(0).Item("AuthSignDesi").ToString()
            tbAuthSignEmpCode.Text = dsSelInvoice.Tables(0).Rows(0).Item("AuthSignEmpCode").ToString()
            cbxSignatureBy.SelectedValue = dsSelInvoice.Tables(0).Rows(0).Item("AuthSignEmpCode").ToString()
            tbAuthSignEmpName.Text = dsSelInvoice.Tables(0).Rows(0).Item("AuthSignEmpName").ToString()
            tbCartonDia.Text = dsSelInvoice.Tables(0).Rows(0).Item("CartonDia").ToString()
            tbContainerName.Text = dsSelInvoice.Tables(0).Rows(0).Item("ContainerName").ToString()
            tbContainerNo.Text = dsSelInvoice.Tables(0).Rows(0).Item("ContainerNo").ToString()
            tbContainerSealNo.Text = dsSelInvoice.Tables(0).Rows(0).Item("ContainerSealNo").ToString()
            tbContainerSize.Text = dsSelInvoice.Tables(0).Rows(0).Item("ContainerSize").ToString()
            tbDeclaration.Text = dsSelInvoice.Tables(0).Rows(0).Item("Declaration").ToString()
            tbDeclaration1.Text = dsSelInvoice.Tables(0).Rows(0).Item("Declaration1").ToString()
            tbDestinationCountry.Text = dsSelInvoice.Tables(0).Rows(0).Item("DestinationCountry").ToString()
            tbDiscount.Text = dsSelInvoice.Tables(0).Rows(0).Item("Discount").ToString()
            tbFinalDestination.Text = dsSelInvoice.Tables(0).Rows(0).Item("FinalDestination").ToString()
            tbFreightCharges.Text = dsSelInvoice.Tables(0).Rows(0).Item("FreightCharges").ToString()
            tbFromPackNo.Text = dsSelInvoice.Tables(0).Rows(0).Item("FromPackNo").ToString()
            tbGoodsDescription.Text = dsSelInvoice.Tables(0).Rows(0).Item("GoodsDescription").ToString()
            tbInsuranceChager.Text = dsSelInvoice.Tables(0).Rows(0).Item("InsuranceChager").ToString()
            tbLoadingCharges.Text = dsSelInvoice.Tables(0).Rows(0).Item("LoadingCharges").ToString()
            tbMarksAndNos.Text = dsSelInvoice.Tables(0).Rows(0).Item("MarksAndNos").ToString()
            tbMatType.Text = dsSelInvoice.Tables(0).Rows(0).Item("MatType").ToString()
            tbNoAndKindOfPackages.Text = dsSelInvoice.Tables(0).Rows(0).Item("NoAndKindOfPackages").ToString()
            tbOneCarton.Text = dsSelInvoice.Tables(0).Rows(0).Item("OneCarton").ToString()
            tbOtherCharges.Text = dsSelInvoice.Tables(0).Rows(0).Item("OtherCharges").ToString()
            tbPaymentTerms.Text = dsSelInvoice.Tables(0).Rows(0).Item("PaymentTerms").ToString()
            tbport.Text = dsSelInvoice.Tables(0).Rows(0).Item("port").ToString()
            tbPortDischarge.Text = dsSelInvoice.Tables(0).Rows(0).Item("PortDischarge").ToString()
            tbPreCarriageBy.Text = dsSelInvoice.Tables(0).Rows(0).Item("PreCarriageBy").ToString()
            tbPreCarrierRecvPlace.Text = dsSelInvoice.Tables(0).Rows(0).Item("PreCarrierRecvPlace").ToString()
            tbToPackNo.Text = dsSelInvoice.Tables(0).Rows(0).Item("ToPackNo").ToString()
            tbTotalOne.Text = dsSelInvoice.Tables(0).Rows(0).Item("TotalOne").ToString()
            cbxConsigneeCode.SelectedValue = dsSelInvoice.Tables(0).Rows(0).Item("ConsigneeCode").ToString()


            sBuyerGroup = dsSelInvoice.Tables(0).Rows(0).Item("BuyerGroup").ToString()


        End If
    End Sub

    Dim sEmployeeCode As String
    Dim sConsigneeCode As String
    Private Sub EditInvoice()

        'mystrInvoice.AREDate = Trim(tbAREDate.Text)
        mystrInvoice.ARENo = Trim(tbARENo.Text)
        mystrInvoice.AuthSignDesi = Trim(tbAuthSignDesi.Text)
        mystrInvoice.AuthSignEmpCode = Trim(tbAuthSignEmpCode.Text)
        mystrInvoice.AuthSignEmpName = Trim(tbAuthSignEmpName.Text)
        mystrInvoice.CartonDia = Trim(tbCartonDia.Text)
        mystrInvoice.ContainerName = Trim(tbContainerName.Text)
        mystrInvoice.ContainerNo = Trim(tbContainerNo.Text)
        mystrInvoice.ContainerSealNo = Trim(tbContainerSealNo.Text)
        mystrInvoice.ContainerSize = Trim(tbContainerSize.Text)
        mystrInvoice.Declaration = Trim(tbDeclaration.Text)
        mystrInvoice.Declaration1 = Trim(tbDeclaration1.Text)
        mystrInvoice.DestinationCountry = Trim(tbDestinationCountry.Text)
        mystrInvoice.Discount = Val(Trim(tbDiscount.Text))
        mystrInvoice.FinalDestination = Trim(tbFinalDestination.Text)
        mystrInvoice.FreightCharges = Trim(tbFreightCharges.Text)
        mystrInvoice.FromPackNo = Trim(tbFromPackNo.Text)
        mystrInvoice.GoodsDescription = Trim(tbGoodsDescription.Text)
        mystrInvoice.InsuranceChager = Val(Trim(tbInsuranceChager.Text))
        mystrInvoice.LoadingCharges = Val(Trim(tbLoadingCharges.Text))
        mystrInvoice.MarksAndNos = Trim(tbMarksAndNos.Text)
        mystrInvoice.MatType = Trim(tbMatType.Text)
        mystrInvoice.NoAndKindOfPackages = Trim(tbNoAndKindOfPackages.Text)
        mystrInvoice.OneCarton = Trim(tbOneCarton.Text)
        mystrInvoice.OtherCharges = Val(Trim(tbOtherCharges.Text))
        mystrInvoice.PaymentTerms = Trim(tbPaymentTerms.Text)
        mystrInvoice.port = Trim(tbport.Text)
        mystrInvoice.PortDischarge = Trim(tbPortDischarge.Text)
        mystrInvoice.PreCarriageBy = Trim(tbPreCarriageBy.Text)
        mystrInvoice.PreCarrierRecvPlace = Trim(tbPreCarrierRecvPlace.Text)
        mystrInvoice.ToPackNo = Trim(tbToPackNo.Text)
        mystrInvoice.TotalOne = Trim(tbTotalOne.Text)
        mystrInvoice.ModeOfShipment = Trim(tbModeofShipment.Text)

        sEmployeeCode = cbxSignatureBy.SelectedValue
        'sEmployeeCode = Trim(tbAuthSignEmpCode.Text) 'cbxSignatureBy.SelectedValue

        Dim daSelEmp As New SqlDataAdapter("Select * from Employee Where EmpCode = '" & sEmployeeCode & "'", sConstr)
        Dim dsSelEmp As New DataSet
        daSelEmp.Fill(dsSelEmp)

        mystrInvoice.AuthSignEmpCode = sEmployeeCode
        mystrInvoice.AuthSignEmpName = dsSelEmp.Tables(0).Rows(0).Item("EmpFullName").ToString
        mystrInvoice.AuthSignDesi = dsSelEmp.Tables(0).Rows(0).Item("Designation").ToString
        '------'
        sConsigneeCode = cbxConsigneeCode.SelectedValue
        'sEmployeeCode = Trim(tbAuthSignEmpCode.Text) 'cbxSignatureBy.SelectedValue

        Dim daSelCCode As New SqlDataAdapter("Select * from Buyer Where BuyerCode = '" & sConsigneeCode & "'", sConstr)
        Dim dsSelCCode As New DataSet
        daSelCCode.Fill(dsSelCCode)

        mystrInvoice.ConsigneeCode = sConsigneeCode
        mystrInvoice.cntrycode = dsSelCCode.Tables(0).Rows(0).Item("CountryCode").ToString
        mystrInvoice.cntryname = dsSelCCode.Tables(0).Rows(0).Item("CountryName").ToString
        '-----'
        mystrInvoice.InvoiceNo = mdlSGM.sInvoiceNo

        myCCInvoice.EditInvoiceMain(mystrInvoice)
        sUpdateMode = "Modified"
        UpdateInvoiceAuditDatabase()

    End Sub

    Dim sUpdateMode As String

    Private Sub cbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSave.Click
        EditInvoice()
        Me.Hide()
        frmInvoiceGeneration.Show() : frmInvoiceGeneration.BringToFront()
    End Sub

    Private Sub Clear()
        tbAREDate.Text = ""
        tbARENo.Text = ""
        tbAuthSignDesi.Text = ""
        tbAuthSignEmpCode.Text = ""
        'tbAuthSignE()
        'mpName.Text = ""
        tbCartonDia.Text = ""
        tbContainerName.Text = ""
        tbContainerNo.Text = ""
        tbContainerSealNo.Text = ""
        tbContainerSize.Text = ""
        tbDeclaration.Text = ""
        tbDeclaration1.Text = ""
        tbDestinationCountry.Text = ""
        tbDiscount.Text = ""
        tbFinalDestination.Text = ""


        tbFreightCharges.Text = ""
        tbFromPackNo.Text = ""
        tbGoodsDescription.Text = ""
        tbInsuranceChager.Text = ""
        tbLoadingCharges.Text = ""
        tbMarksAndNos.Text = ""
        tbMatType.Text = ""
        tbNoAndKindOfPackages.Text = ""
        tbOneCarton.Text = ""
        tbOtherCharges.Text = ""
        tbPaymentTerms.Text = ""
        tbport.Text = ""
        tbPortDischarge.Text = ""
        tbPreCarriageBy.Text = ""
        tbPreCarrierRecvPlace.Text = ""
        tbToPackNo.Text = ""
        tbTotalOne.Text = ""
    End Sub

    Private Sub LoadSignatory()
        ''Try
        cbxSignatureBy.DataSource = Nothing : cbxSignatureBy.Items.Clear()


        cbxSignatureBy.DataSource = myccInvoice.LoadSignatureBy
        cbxSignatureBy.DisplayMember = "EmpName"
        cbxSignatureBy.ValueMember = "Empcode"

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCustomer()
        ''Try
        cbxConsigneeCode.DataSource = Nothing : cbxConsigneeCode.Items.Clear()


        cbxConsigneeCode.DataSource = myCCInvoice.LoadConsigneeCode(sBuyerGroup)
        cbxConsigneeCode.DisplayMember = "BuyerName"
        cbxConsigneeCode.ValueMember = "BuyerCode"

        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub UpdateInvoiceAuditDatabase()
        Try


            Dim daInsInvinAudit As New SqlDataAdapter("Insert Into Invoice SELECT ID, Shipper, InvoiceNo, InvoiceDate, Buyer, Account, BuyerDepartment, Origin, LCNo, InvoiceDescription, ShippingBillNo, ShippingBillDate, Marks1, Marks2, Marks3, Marks4, Marks5, Marks6, Marks7, Marks8, ModeOfShipment, AwbBillNo, AwbBillDate, HAwbBillNo, HAwbBillDate, Destination, BanckAccount, Vessel, Bank, Currency, CurrencyConversion, Nature, Quantity, TotalValue, Freight, Insurance, AgentPercentage, Commission, DbkPercentage, DrawBack, BankCertificate, BankAmount, BankRate, CertificateDate, TotalPackNo, harmnsdco, NetWeight, GrossWeight, dbkrecd, aepccer_no, cust_clear, rbicode, cot_rayon, shpblfob, netfobamt, remark1, status, shipdate, aepcdt, drwrecddt, negdate, markrate, cntrycode, realstdt, usha, ugts, printdt, [percent], plusamt, cntryname, days, due_date, accounted, accountamt, mode, ShipRate, courier, docawb, docsentdt, advdocdt, inspdate, advdocdays, tokenno, tokendt, qlfbonus, midamount, depb, depbamt, depbrcvd, depbrcvdon, depbper, licencenr, licenceamt, licsoldon, licsoldfor, depbappldt, port, epcopydt, forwarder, forbillno, forbillamt, forrcvdt, sentforver, verifieddt, suppnr, suppbbnnr, warehouse, swiftcode, CorrespondingBank, CorrespondingBankAcount, CorrespondingBankSwiftCode, Msrks9, Marks10, STRPercentage, STRAmount, PAN_Number, VAT_TIN, CST_TIN, ExciseDuty, VATAmount, CessAmount, EduCessAmount, BuyerGroup, MinusAmount, FinancialYear, InvoiceType, Shipped, IsShipped, ShippedDate, ShippedBy, MarkToShipDoneDate, PaymentReceiveFromBuyer, PaymentReceiveDate, BankRefNo, BankRefDate, ContractNo, ContractDate, ExciseInvoiceNo, IsAdvanceReceived, AdvanceAmount, CreatedBy, CreatedDate, ModifiedBy, GetDate(), EnteredOnMachineID, HandoverDate, HangerPack, Covering, Declaration, PostDescription, LCDescription, Mark11, Mark12, DiscountUpCharge, Percentage, Amount, ShipperLC, BuyerLC, FOBValueInCurrency, TaxType, IsRecalRequired, IsApproved, ApprovedBy, ApprovedOn, ModuleName, 'Modified', ConsigneeCode, Notify1, Notify2, Notify3, AuthSignEmpCode, AuthSignEmpName, AuthSignDesi, CSTorVAT, EduCess, CESS, Excise, CT3No, CT3Date, ARENo, AREDate, ConveredBy, Declaration1, ContainerSize, ContainerName, ContainerSealNo, GoodsDescription, MarksAndNos, NoAndKindOfPackages, PreCarriageBy, PreCarrierRecvPlace, PortDischarge, DestinationCountry, AssortmentYear, PaymentTerms, DeliveryTerms, AREDuty, AreCess, AREHCess, ContainerNo, FromPackNo, ToPackNo, EmailToCustDate, FreightFwdrPlotLetterDate, ContainerApplnDate, GSPSlNo, OriginCriterion, StuffingDate, GateOpeningDate, CutOffDate, ClosingDate, SailingVesselDetails, ShipmentType, RevShipmentType, HaltingCharges, Demurrage, VoyageNo, ETDFeederVessel, ETAFeederVessel, FeederVessel, FeederVoyageNo, ETAMotherVessel, ETDMotherVessel, InternalSealNo, CentralExciseSealNo, TypesOfBL, CustomClearence, TransportCharges, CHACharges, ClearingCharges, CFSCharges, ForwardingCharges, GSpCharges, CourierCharges, InsuranceCharges, MiscCharges, ContainerArrivalDate, Memo, DestinationArrivalDate, FreightFwdrPlotLetterExpDate, ForwarderCode, CHACode, RevVessel, RevVoyageNo, RevETDMotherVessel, RevETAMotherVessel, RevFeederVessel, RevFeederVoyageNo, RevETDFeederVessel, RevETAFeederVessel, Commodity, PremiumRate, PremiumAmount, InvoiceNoAutoGen, CourierPayment, CourierNo, CourierDate, CartonDia, OneCarton, TotalOne, FinalDestination, MatType, AmtOfDutyPayable, SubTotal, InvYear, InvCode, DispatchFrom, InoviceStatus, ExpDocDate, GoodsDescription2, RBBankName, RBAccountNo, RBSwiftCode, CurValue, AnnexureAPortOfLoading, LCID, LCValue, ShippedLCValue, CGSTPercentage, CGSTValue, SGSTPercentage, SGSTVlaue, IGSTPercentage, IGSTValue, FreightCharges, LoadingCharges, InsuranceChager, OtherCharges, Discount, GSTTotalValue, GSTInvNo, InvNo2, InvNo3, DUMMYINVDATE, GSTValue, SerialNoPrefix, InternalOrder FROM AHGroup_SSPL.dbo.INVOICE WHERE (InvoiceNo = '" & mdlSGM.sInvoiceNo & "')", sConstrAudit)
            Dim dsInsInvinAudit As New DataSet
            daInsInvinAudit.Fill(dsInsInvinAudit)
            dsInsInvinAudit.AcceptChanges()

            
        Catch exp As Exception
            HandleException(Me.Name, exp)
        End Try

    End Sub
End Class