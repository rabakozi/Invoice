using Cassandra;
using MongoDB.Driver;

namespace Invoice.Repository
{

    public static class RepositoryFactory
    {
        private static IInvoiceRepository mongoRepository { get; set; }
        private static IInvoiceRepository cassandraRepository { get; set; }

        public static IInvoiceRepository Repository(DataBase db)
        {
            if (db == DataBase.MongoDb)
                return mongoRepository ?? (mongoRepository = GetMongoRepository());
            else
                return cassandraRepository ?? (cassandraRepository = GetCassandraRepository());
        }

        private static IInvoiceRepository GetMongoRepository()
        {
            var client = new MongoClient();
            IMongoDatabase db = client.GetDatabase("homework");

            return new MongoRepository(db);
        }

        private static IInvoiceRepository GetCassandraRepository()
        {
            var cluster = Cluster.Builder()
           .AddContactPoints("localhost")
           .Build();
            // Connect to the nodes using a keyspace
            var session = cluster.Connect("homework");

            return new CassandraRepository(session);
        }
    }

    public enum DataBase
    {
        Cassandra,
        MongoDb
    }
}
