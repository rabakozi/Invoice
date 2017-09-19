using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Invoice.Model;
using Invoice.Repository;

namespace Invoice.MvcApp.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            var repository = RepositoryFactory.Repository(DataBase.Cassandra);
            var invoices = repository.GetAll();
            return View(invoices);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(Model.Invoice invoiceHeader)
        {
            try
            {
                var repository = RepositoryFactory.Repository(DataBase.Cassandra);
                repository.AddHeader(invoiceHeader);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Create
        public ActionResult Details(string invoiceId)
        {
            var repository = RepositoryFactory.Repository(DataBase.Cassandra);
            var invoice = repository.Get(invoiceId);
            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult AddInvoiceItem()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult AddInvoiceItem(string invoiceId, InvoiceItem invoiceItem)
        {
            try
            {
                var repository = RepositoryFactory.Repository(DataBase.Cassandra);
                repository.AddItem(invoiceId, invoiceItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Invoice/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Invoice/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Invoice/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Invoice/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
