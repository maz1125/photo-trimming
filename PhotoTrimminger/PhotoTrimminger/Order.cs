using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoTrimminger
{
    class Order
    {
        public string orderNo { get; set; }
        public string size { get; set; }
        public string targetFolderPath { get; set; }
        public string saveFolderPath { get; set; }
        public string targetOrderText { get; set; }
        public List<OrderDetail> details { get; set; }
    }
}
