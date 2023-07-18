using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuMainMenuController : Controller
    {
        // GET: IemuMainMenu
        public ActionResult Index()
        {
            using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuMainMenu";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixHead";
                var tableShowStyle = Session["tableShowStyle"].ToString();

                ViewBag.tableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";
                ViewBag.PageInfo = "第 1 頁，共 1 頁";
                var model = mainMenus.GetDapperDataList("");
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