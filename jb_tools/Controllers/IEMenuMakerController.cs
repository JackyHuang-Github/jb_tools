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

        public ActionResult TableShowStyle_TableHover()
        {
            return ChoiceActionResult("tableFixHead");
        }

        public ActionResult TableShowStyle_BorderShadow()
        {
            return ChoiceActionResult("tableFixHeadBorderShadow");
        }

        private ActionResult ChoiceActionResult(string tableShowStyle)
        {
            if (Session["CurrentController"] == null)
                Session["CurrentController"] = "IemuMainMenu";

            var controllerName = Session["CurrentController"].ToString();
            if (controllerName == "")
            {
                controllerName = "IemuMainMenu";
                Session["CurrentController"] = "IemuMainMenu";
            }

            Session["TableShowStyle"] = tableShowStyle;

            switch (controllerName)
            {
                case "IemuMainMenu":
                    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = mainMenus.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuMainMenu", model);
                    }

                case "IemuSubMenu":
                    using (z_repoIemuSubMenus subMenus = new z_repoIemuSubMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = subMenus.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuSubMenu", model);
                    }

                case "IemuDetailMenu":
                    using (z_repoIemuDetailMenus detailMenus = new z_repoIemuDetailMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = detailMenus.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuDetailMenu", model);
                    }

                case "IemuTran":
                    using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = iemuTrans.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuTrun", model);
                    }

                default:
                    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.tableShowStyle = "tableFixHead";
                        var model = mainMenus.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuMainMenu", model);
                    }
            }
        }
    }
}