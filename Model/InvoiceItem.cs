using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Model
{
    public class InvoiceItem
    {
        public string InvoiceId { get; set; }
        public int LineId { get; set; }
        public string ArticleName { get; set; }
        public Decimal Price { get; set; }
    }
}
