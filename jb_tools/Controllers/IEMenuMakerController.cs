using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IEMenuMakerController : Controller
    {
        // GET: IEMenuMaker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IemuMainMenu()
        {
            //using (dbEntities db = new dbEntities())
            //{
            //    var model = db.IemuMainMenus;
            //    return View(model);
            //}

            dbEntities db = new dbEntities();
            var model = db.IemuMainMenus;
            return View(model);
        }

        public ActionResult IemuSubMenu()
        {
            //using (dbEntities db = new dbEntities())
            //{
            //    var model = db.IemuSubMenus;
            //    return View(model);
            //}

            dbEntities db = new dbEntities();
            var model = db.IemuSubMenus;
            return View(model);
        }

        public ActionResult IemuDetailMenu()
        {
            //using (dbEntities db = new dbEntities())
            //{
            //    var model = db.IemuDetailMenus;
            //    return View(model);
            //}

            dbEntities db = new dbEntities();
            var model = db.IemuDetailMenus;
            return View(model);
        }

        public ActionResult EditIemu()
        {
            return View();
        }
    }
}