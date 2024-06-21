using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.ViewModels
{
    public class CustomerReportViewModel
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerName { get; set; }
        public float NetAmount { get; set; }
        public float DiscountAmount { get; set; }
        public float TotalAmount { get; set; }
        public int PaymentMethod { get; set; }
        public string Remarks { get; set; }
    }
}
