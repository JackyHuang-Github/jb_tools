using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuSubMenuController : Controller
    {
        // GET: IemuSubMenu
        public ActionResult Index()
        {
            using (z_repoIemuSubMenus subMenus = new z_repoIemuSubMenus())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuSubMenu";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixHead";
                var tableShowStyle = Session["TableShowStyle"].ToString();

                ViewBag.tableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";
                ViewBag.PageInfo = "第 1 頁，共 1 頁";
                var model = subMenus.GetDapperDataList("");
                return View(model);
            }
        }

        public ActionResult CreateEdit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}