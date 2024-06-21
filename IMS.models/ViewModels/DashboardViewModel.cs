using IMS.models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<StockInfo> StockInfos { get; set; }
        public IEnumerable<DashboardList> DashboardListInfos { get; set; }
        public DashboardIndex DashboardIndex { get; set; }
        public IEnumerable<DashboardIndex> DashboardIndexList { get; set; }
    }
}
