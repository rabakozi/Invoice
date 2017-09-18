namespace Invoice.Repository
{
    public interface IInvoiceRepository
    {
        void AddHeader(Model.Invoice invoiceHeader);
        void AddItem(string invoiceId, Model.InvoiceItem invoiceItem);
        Model.Invoice Get(string id);
        Model.Invoice GetAll();
    }
}
