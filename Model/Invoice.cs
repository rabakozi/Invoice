using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}

