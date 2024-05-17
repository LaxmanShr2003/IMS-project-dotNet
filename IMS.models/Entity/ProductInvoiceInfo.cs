using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.Entity
{
    public class ProductInvoiceInfo:BaseEntity
    {
        public int PaymentMethod { get; set; }
        public string InvoiceNo { get; set; }
     
        public int CustomerInfoId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double NetAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }
        public string Remarks { get; set; }
        public int BillStatus { get; set; }
        public int CancellationRemarks {  get; set; }
        public int ProductInfoId { get; set; }
        public int UnitInfoId { get; set; }
        public int ProductRateInfoId { get; set; }
        public int StoreInfoId { get; set; }
        public float Rate { get; set; }
        public double Amount { get; set; }
        public float Quantity { get; set; }

        public virtual ICollection<ProductDetailInfo> ProductDetailInfos { get; set; }

        public virtual CategoryInfo CategoryInfo { get; set; }
        public virtual CustomerInfo CustomerInfo { get; set; }
        public virtual ProductInfo  ProductInfo { get; set; }
        public virtual UnitInfo UnitInfo { get;set; }
        public virtual StoreInfo StoreInfo { get; set; }
        public virtual productRateInfo ProductRateInfo { get; set; }


    }
}
