using System;
using Invoice.Model;
using Cassandra;

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
            var rs = session.Execute("SELECT * FROM sample_table");
            // Iterate through the RowSet
            foreach (var row in rs)
            {
                var value = row.GetValue<int>("sample_int_column");
                // Do something with the value
            }

            throw new NotImplementedException();
        }

        public Model.Invoice GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
