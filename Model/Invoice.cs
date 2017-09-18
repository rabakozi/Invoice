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
        public IEnumerable<InvoiceItem> Items { get; set; }
    }
}

//        -- header fields
//invoice_id text,
//invoice_date date STATIC,
//invoice_address text STATIC,
//-- detail fields
//line_id int,
//article_name text,
//article_price decimal,
//PRIMARY KEY(invoice_id, article_name)
//  }
