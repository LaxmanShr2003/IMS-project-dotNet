using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.Entity
{
    public class SupplierInfo:BaseEntity
    {
        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson {  get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int StoreInfoId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }

        public virtual StoreInfo StoreInfo { get; set; }
        public virtual ICollection<ProductRateInfo> ProductRateInfos { get; set; }

        public static implicit operator SupplierInfo(CustomerInfo v)
        {
            throw new NotImplementedException();
        }
    }
}
