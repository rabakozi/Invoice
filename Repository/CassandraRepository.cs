using System;
using Invoice.Model;
using Cassandra;
using System.Collections.Generic;
using System.Linq;

namespace Invoice.Repository
{
    internal class CassandraRepository : IInvoiceRepository
    {
        private readonly ISession session;
        public CassandraRepository(ISession session)
        {
            this.session = session;
        }

        public void AddHeader(Model.Invoice invoiceHeader)
        {
            var cql = "INSERT INTO invoice (invoice_id, invoice_date, invoice_address) " +
                $"VALUES('{invoiceHeader.Id}', '{invoiceHeader.Date:yyyy-MM-dd}', '{invoiceHeader.Address}'); ";
            session.Execute(cql);
        }

        public void AddItem(InvoiceItem invoiceItem)
        {
            var cql = "INSERT INTO invoice(invoice_id, line_id, article_name, article_price) " +
                $"VALUES('{invoiceItem.InvoiceId}', {invoiceItem.LineId}, '{invoiceItem.ArticleName}', {invoiceItem.Price}); ";
            session.Execute(cql);
        }

        public Model.Invoice Get(string id)
        {
            Model.Invoice invoice = null;

            var rs = session.Execute($"SELECT * FROM invoice WHERE invoice_id = '{id}'");

            // fetch rowset
            foreach (var row in rs)
            {
                invoice = invoice ?? MapFromDbInvoice(row);
                var invoiceItems = MapFromDbInvoiceItem(row);
                if(invoiceItems != null)
                    invoice.Items.Add(invoiceItems);
            }

            return invoice;
        }

        public IEnumerable<Model.Invoice> GetAll()
        {
            var invoices = new List<Model.Invoice>();

            var rs = session.Execute("SELECT DISTINCT invoice_id, invoice_address, invoice_date FROM invoice");

            foreach (var row in rs)
            {
                invoices.Add(MapFromDbInvoice(row));
            }

            return invoices;
        }

        private Model.Invoice MapFromDbInvoice(Row row)
        {
            var invoiceId = row.GetValue<string>("invoice_id");
            var invoiceAddress = row.GetValue<string>("invoice_address");
            var invoiceDate = row.GetValue<LocalDate>("invoice_date");

            return new Model.Invoice
            {
                Id = invoiceId,
                Address = invoiceAddress,
                Date = new DateTime(invoiceDate.Year, invoiceDate.Month, invoiceDate.Day)
            };
        }

        private InvoiceItem MapFromDbInvoiceItem(Row row)
        {
            // no line_id? then no items yet; only header
            if (row.IsNull("line_id"))
                return null;

            var invoiceId = row.GetValue<string>("invoice_id");
            var lineId = row.GetValue<int>("line_id");
            var articleName = row.GetValue<string>("article_name");
            var articlePrice = row.GetValue<Decimal>("article_price");

            return new InvoiceItem
            {
                InvoiceId = invoiceId,
                LineId = lineId,
                ArticleName = articleName,
                Price = articlePrice
            };
        }
    }
}
