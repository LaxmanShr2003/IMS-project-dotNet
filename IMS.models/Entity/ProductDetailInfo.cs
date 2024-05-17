using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.Entity
{
    public class ProductDetailInfo:BaseEntity
    {
        public int ProductInvoiceInfoId { get; set; }
        public int ProductRate { get; set; }
        public float Rate { get; set; }
        public float Quantity { get; set; }
        public double Amount { get; set; }

 
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ProductInvoiceInfo ProductInvoiceInfo { get; set; }


    }
}
