using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoTrimminger
{
    class OrderDetail
    {
        public string originalName { get; set; }
        public System.Drawing.Image originalImage { get; set; }
        public string count { get; set; }
        public int rotation { get; set; }
        public int trimmingLeft { get; set; }
        public int trimmingTop { get; set; }
        public int trimmingWidth { get; set; }
        public int trimmingHeight { get; set; }
        public System.Drawing.Image editedImage { get; set; }

    }
}
