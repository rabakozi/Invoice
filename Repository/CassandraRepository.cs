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
                $"VALUES('{invoiceHeader.Id}', '{invoiceHeader.Date.ToString("yyyy-MM-dd")}', '{invoiceHeader.Address}'); ";
            session.Execute(cql);
        }

        public void AddItem(string invoiceId, InvoiceItem invoiceItem)
        {
            var cql = "INSERT INTO invoice(invoice_id, line_id, article_name, article_price) " +
                $"VALUES('{invoiceId}', {invoiceItem.LineId}, '{invoiceItem.ArticleName}', {invoiceItem.Price}); ";
            session.Execute(cql);
        }

        public Model.Invoice Get(string id)
        {
            var invoice = new Model.Invoice();
            var invoiceItems = new List<InvoiceItem>();

            var rs = session.Execute($"SELECT * FROM invoice WHERE invoice_id = '{id}'");

            foreach (var row in rs)
            {
                // header
                var invoiceId = row.GetValue<string>("invoice_id");
                var invoiceAddress = row.GetValue<string>("invoice_address");
                var invoiceDate = row.GetValue<LocalDate>("invoice_date");

                invoice.Id = invoiceId;
                invoice.Address = invoiceAddress;
                invoice.Date = new DateTime(invoiceDate.Year, invoiceDate.Month, invoiceDate.Day);

                // lines
                var lineId = row.GetValue<int>("line_id");
                var articleName = row.GetValue<string>("article_name");
                var articlePrice = row.GetValue<Decimal>("article_price");

                invoiceItems.Add(new InvoiceItem
                {
                    LineId = lineId,
                    ArticleName = articleName,
                    Price = articlePrice
                });
            }

            invoice.Items = invoiceItems;

            return invoice;
        }

        public IEnumerable<Model.Invoice> GetAll()
        {
            var invoices = new List<Model.Invoice>();

            var rs = session.Execute("SELECT * FROM invoice");

            foreach (var row in rs)
            {
                var invoiceId = row.GetValue<string>("invoice_id");
                var invoiceAddress = row.GetValue<string>("invoice_address");
                var invoiceDate = row.GetValue<LocalDate>("invoice_date");

                invoices.Add(new Model.Invoice
                {
                    Id = invoiceId,
                    Address = invoiceAddress,
                    Date = new DateTime(invoiceDate.Year, invoiceDate.Month, invoiceDate.Day)
                });
            }

            return invoices;
        }
    }
}
