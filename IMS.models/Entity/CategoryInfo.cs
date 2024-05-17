﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.Entity
{
    public class CategoryInfo: BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int StoreInfoId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual StoreInfo StoreInfo { get; set; }
        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
        public virtual ICollection<StockInfo> StockInfos { get; set; }

        public virtual ICollection<TransationInfo> TransationInfos { get; set; }
       

        public virtual ICollection<productRateInfo> ProductRateInfos { get; set; }


        

    }
}
