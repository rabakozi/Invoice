﻿using System.Collections.Generic;

namespace Invoice.Repository
{
    public interface IInvoiceRepository
    {
        void AddHeader(Model.Invoice invoiceHeader);
        void AddItem(Model.InvoiceItem invoiceItem);
        Model.Invoice Get(string id);
        IEnumerable<Model.Invoice> GetAll();
    }
}
