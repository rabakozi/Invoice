using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Model
{
    public class Invoice
    {
        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}

