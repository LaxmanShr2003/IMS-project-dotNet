using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.ViewModels
{
    public class ReportViewModel
    {
        public SearchCriteria search { get; set; }
        public IEnumerable<CustomerReportViewModel> customerReportViewModels { get; set; }
        public IEnumerable<ReportDetailViewModel> ReportDetailViewModels { get; set; }

    }
}
