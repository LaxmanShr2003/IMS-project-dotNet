using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.Entity
{
    public class SupplierInfo:BaseEntity
    {
        public string SupplierName { get; set; }
        public string ContactPerson {  get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int StoreInfoId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }

        public virtual StoreInfo StoreInfo { get; set; }
        public virtual ICollection<ProductRateInfo> ProductRateInfos { get; set; }
    }
}
