using System;
using Invoice.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Invoice.Repository
{
    internal class MongoRepository : IInvoiceRepository
    {
        private readonly IMongoDatabase db;
        public MongoRepository(IMongoDatabase db)
        {
            this.db = db;
        }
        public void AddHeader(Model.Invoice invoiceHeader)
        {
            var header = new BsonDocument
            {
              {"_id", invoiceHeader.Id},
              {"invoice_date", invoiceHeader.Date},
              {"invoice_address", invoiceHeader.Address}
            };
        }

        public void AddItem(string invoiceId, InvoiceItem invoiceItem)
        {
            var item = new BsonDocument
            {
              {"_id", invoiceId},
              {"lines", new BsonArray {
                  new BsonDocument
                  {
                      { "line_id", invoiceItem.LineId },
                      { "article_name", invoiceItem.ArticleName},
                      { "article_price", invoiceItem.Price }
                  }
              }},
            };
        }

        public Model.Invoice Get(string id)
        {
            throw new NotImplementedException();
        }

        public Model.Invoice GetAll()
        {
            throw new NotImplementedException();
        }
    }

}
