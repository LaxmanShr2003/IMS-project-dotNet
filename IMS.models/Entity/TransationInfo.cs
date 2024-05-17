using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.Entity
{
    public class TransationInfo:BaseEntity
    {
        public string TransactionType { get; set; }
        public int CategroyInfoId { get; set; }
        public int ProductInfoId { get; set; }
        public int UnitInfoId { get; set; }
        public int ProductRateInfoId { get; set; }

        public int StoreInfoId { get; set; }
        public float Rate { get; set; }
        public double Amount { get; set; }
       
        public float Quantity { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public String ModifiedBy { get; set; }

        public virtual CategoryInfo CategoryInfo { get; set; }
        public virtual ProductInfo ProductInfo { get; set; }

        public virtual UnitInfo UnitInfo { get; set; }
        public virtual productRateInfo ProductRateInfo { get; set; }
        public virtual StoreInfo StoreInfo { get; set; }

    }
}
