using Invoice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Invoice.MvcApp.Controllers
{
    public class ControllerBase : Controller
    {
        protected DataBase database
        {
            get
            {
                if (Session == null)
                    return DataBase.Cassandra;
                return (DataBase)(Session["Database"] = Session["Database"] ??
                    DataBase.Cassandra);
            }
            set
            {
                if(Session != null)
                    Session["Database"] = value;
            }
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.database = database;
        }
    }

}