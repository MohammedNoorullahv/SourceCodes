using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptimizerBeta3.Models.GeneralTables;
using OptimizerBeta3.Models.HRTables;
using OptimizerBeta3.Models.MasterTables;
using OptimizerBeta3.Models.TransactionTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptimizerBeta3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region MASTERTABLES
        public DbSet<LookUpCategory> lookUpCategories { get; set; }
        public DbSet<LookUpMaster> lookUpMasters { get; set; }
        public DbSet<CompanyInfo> companyInfos { get; set; }
        public DbSet<PartyInfo>  partyInfos { get; set; }
        public DbSet<PartyInfoDtls> partyInfoDtls { get; set; }
        public DbSet<CustomerPerson> customerPerson { get; set; }
        public DbSet<Season> seasons { get; set; }
        public DbSet<SizeMaster> sizeMasters { get; set; }
        public DbSet<SizeAssortment> sizeAssortments { get; set; }
        public DbSet<Offers> offers { get; set; }
        public DbSet<OfferDetails> offerDetails { get; set; }
        public DbSet<PaymentOption> paymentOptions { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<ColorMaster> colorMasters { get; set; }
        public DbSet<UnitMaster> unitMasters { get; set; }
        public DbSet<Materials> materials { get; set; }
        public DbSet<MaterialDetails> materialDetails { get; set; }
        public DbSet<ArticleGroup> articleGroups { get; set; }
        public DbSet<ArticleDetail> articleDetails { get; set; }
        public DbSet<Locations> locations { get; set; }
        public DbSet<HSNCodeMaster> HSNCodeMasters { get; set; }
        public DbSet<MdlStateMaster> StateMasters { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<SalesPromotionOffer> SalesPromotionOffers { get; set; }
        public DbSet<LeatherGoodsGroup> LeatherGoodsGroups { get; set; }
        public DbSet<LeatherGoodsDetail> leatherGoodsDetails { get; set; }
        public DbSet<SizeMasterforLeatherGoods> SizeMasterforLeatherGoods { get; set; }
        public DbSet<OfferDtlVenueMapping> OfferDtlVenueMappings { get; set; }
        public DbSet<OfferDtlStockMapping> OfferDtlStockMappings { get; set; }
        public DbSet<TempOfferStockMapping> TempOfferStockMappings { get; set; }
        public DbSet<AreaLookUpMaster> AreaLookUpMasters { get; set; }
        public DbSet<AreaMaster> AreaMasters { get; set; }
        #endregion

        #region TRANSACTIONTABLES
        public DbSet<PurchaseOrder> purchaseOrders { get; set; }
        public DbSet<PurchaseOrderMain> purchaseOrderMains { get; set; }
        public DbSet<PurchaseOrderDetails> purchaseOrderDetails { get; set; }

        public DbSet<LGPurchaseOrder> LGPurchaseOrders { get; set; }
        public DbSet<LGPurchaseOrderMain> LGPurchaseOrderMains { get; set; }
        public DbSet<LGPurchaseOrderDetails> LGPurchaseOrderDetails { get; set; }

        public DbSet<MaterialPurchaseOrder> materialPurchaseOrders { get; set; }
        public DbSet<MaterialPurchaseOrderDetails> materialPurchaseOrderDetails { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Inward> inwards { get; set; }
        public DbSet<InwardDetails> inwardDetails { get; set; }
        public DbSet<StockWithArticle> stockWithArticles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceToPerson> InvoiceToPersons { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<InvoiceToPersonDetail> InvoiceToPersonDetails { get; set; }
        public DbSet<AllTransaction> AllTransactions { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
        public DbSet<StockTransferDetail> StockTransferDetails { get; set; }
        public DbSet<DeliveryChallan> DeliveryChallans { get; set; }
        public DbSet<DeliveryChallanDetail> DeliveryChallanDetails { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<EstimateDetail> EstimateDetails { get; set; }
        #endregion

        #region GENERALTABLES
        public DbSet<CarouselSlider> CarouselSlider { get; set; }
        public DbSet<MdlTempArticalArrivalEANCode> TempArticalArrivalEANCodes { get; set; }
        public DbSet<MdlTempInwardDtls> TempInwardDtls { get; set; }
        public DbSet<TempInvoiceDtl> TempInvoiceDtls { get; set; }
        public DbSet<TempInvoiceDtlEANCode> TempInvoiceDtlEANCodes { get; set; }
        public DbSet<TempStockForOfferMapping> TempStockForOfferMappings { get; set; }
        public DbSet<TempStockView> TempStockViews { get; set; }
        public DbSet<TempTransferDtl> TempTransferDtls { get; set; }
        public DbSet<TempTransferDtlEANCode> TempTransferDtlEANCodes { get; set; }
        public DbSet<TempFootWearOrderImport> TempFootWearOrderImports { get; set; }
        public DbSet<POMainDefault> POMainDefaults { get; set; }
        public DbSet<ComplaintLookUpMaster> ComplaintLookUpMasters { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<TempOfferVenueMapping> TempOfferVenueMappings { get; set; }
        public DbSet<TempStockWithArticle> TempStockWithArticles { get; set; }
        public DbSet<TempOfferforInvoice> TempOfferforInvoices { get; set; }
        public DbSet<TempOfferDtlforInvoice> TempOfferDtlforInvoices { get; set; }
        public DbSet<TempOffersBillWise> TempOffersBillWises { get; set; }
        #endregion

        #region HR TABLES
        public DbSet<Employee> Employees { get; set; }
        #endregion

    }
}
