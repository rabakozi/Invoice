using System;
using Invoice.Model;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

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
            // TODO: to get worked with update (only items without header)

            var doc = new BsonDocument
            {
              {"_id", invoiceHeader.Id},
              {"invoice_date", invoiceHeader.Date},
              {"invoice_address", invoiceHeader.Address}
            };

            var collection = db.GetCollection<BsonDocument>("invoice");
            collection.InsertOne(doc);
        }

        public void AddItem(InvoiceItem invoiceItem)
        {
            var doc = new BsonDocument
            {
                { "line_id", invoiceItem.LineId },
                { "article_name", invoiceItem.ArticleName},
                { "article_price", invoiceItem.Price }
            };

            var collection = db.GetCollection<BsonDocument>("invoice");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("_id", invoiceItem.InvoiceId);

            var update = Builders<BsonDocument>.Update
                .AddToSet("lines", doc);
            //.CurrentDate("lastModified");

            var updateResult = collection.UpdateOne(filter, update);

            // no header, yet 
            // TODO: refactor
            if (updateResult.MatchedCount == 0)
            {
                var docEmptyHeader = new BsonDocument
                {
                    {"_id", invoiceItem.InvoiceId}
                };

                collection.InsertOne(docEmptyHeader);
                collection.UpdateOne(filter, update);
            }
        }

        public Model.Invoice Get(string id)
        {
            Model.Invoice invoice = null;

            var collection = db.GetCollection<BsonDocument>("invoice");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var doc = collection.Find(filter).First();

            invoice = MapFromDbInvoice(doc);

            if (doc.Contains("lines"))
            {
                foreach (var item in doc["lines"].AsBsonArray.ToList())
                {
                    invoice.Items.Add(MapFromDbInvoiceItem(invoice.Id, item));
                }
            }

            return invoice;
        }

        public IEnumerable<Model.Invoice> GetAll()
        {
            var invoices = new List<Model.Invoice>();

            var collection = db.GetCollection<BsonDocument>("invoice");
            var result = collection.Find(new BsonDocument()).Project(Builders<BsonDocument>.Projection.Exclude("lines")).ToList();

            foreach (var doc in result)
            {
                invoices.Add(MapFromDbInvoice(doc));
            }
            return invoices;
        }

        private Model.Invoice MapFromDbInvoice(BsonDocument bsonDoc)
        {
            var invoiceId = bsonDoc["_id"].AsString;
            string invoiceAddress = null;
            DateTime invoiceDate = DateTime.MinValue;

            if (bsonDoc.Contains("invoice_address"))
                invoiceAddress = bsonDoc["invoice_address"].AsString;
            if (bsonDoc.Contains("invoice_date"))
                invoiceDate = bsonDoc["invoice_date"].ToUniversalTime();
            
            return new Model.Invoice
            {
                Id = invoiceId,
                Address = invoiceAddress,
                Date = new DateTime(invoiceDate.Year, invoiceDate.Month, invoiceDate.Day)
            };
        }

        private InvoiceItem MapFromDbInvoiceItem(string invoiceId, BsonValue bsonDoc)
        {
            var lineId = bsonDoc["line_id"].AsInt32;
            var articleName = bsonDoc["article_name"].AsString;
            var articlePrice = bsonDoc["article_price"].AsDecimal;

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
