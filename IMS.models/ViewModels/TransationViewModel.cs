using IMS.models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.ViewModels
{
    public class TransationViewModel
    {
        public ProductInvoiceInfo ProductInvoiceInfo { get; set; }
        public IEnumerable<ProductRateInfo> ProductRateInfos { get; set; }
        public ProductInvoiceDetailInfo ProductInvoiceDetailInfo { get; set; }

        public IEnumerable<ProductInvoiceDetailInfo> ProductInvoiceDetailInfos { get; set; }

        public CustomerInfo CustomerInfo { get; set; }
        public StoreInfo StoreInfo { get; set; }
        public int PaymentMethod { get; set; }
        public string InvoiceNo { get; set; }
        public int StoreinfoId { get; set; }
        public int CustomerInfoId { get; set; }
        public DateTime TransactionDate { get; set; }
        public float NetAmount { get; set; }
        public float DiscountAmount { get; set; }
        public float TotalAmount { get; set; }

        public string Remarks { get; set; }
        public int BillStatus { get; set; }
        public string CancellationRemarks { get; set; }


        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
