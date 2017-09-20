using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Invoice.Model;
using Invoice.Repository;

namespace Invoice.MvcApp.Controllers
{
    public class InvoiceController : ControllerBase
    {

        public ActionResult ChangeDatabase(DataBase database)
        {
            this.database = database;
            return RedirectToAction("Index");
        }
        // GET: Invoice
        public ActionResult Index()
        {
            var repository = RepositoryFactory.Repository(database);
            var invoices = repository.GetAll();
            return View(invoices);
        }

        // GET: Invoice/Create
        public ActionResult CreateHeader()
        {
            return View();
        }

        // POST: Invoice/CreateHeader
        [HttpPost]
        public ActionResult CreateHeader(Model.Invoice invoiceHeader)
        {
            try
            {
                var repository = RepositoryFactory.Repository(database);
                repository.AddHeader(invoiceHeader);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/CreateItem
        public ActionResult CreateItem()
        {
            return View();
        }

        // POST: Invoice/CreateItem
        [HttpPost]
        public ActionResult CreateItem(Model.InvoiceItem invoiceItem)
        {
            try
            {
                var repository = RepositoryFactory.Repository(database);
                repository.AddItem(invoiceItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Details
        public ActionResult Details(string invoiceId)
        {
            var repository = RepositoryFactory.Repository(database);
            var invoice = repository.Get(invoiceId);
            return View(invoice);
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
