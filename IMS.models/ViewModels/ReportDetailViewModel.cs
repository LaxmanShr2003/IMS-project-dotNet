using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.ViewModels
{
    public class ReportDetailViewModel
    {
      
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerName { get; set; }
      public string SupplierName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public float Quantity { get; set; }
        public int PaymentMethod { get; set; }
   
    }
}
