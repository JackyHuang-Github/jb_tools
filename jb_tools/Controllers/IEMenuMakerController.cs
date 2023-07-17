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

        //public ActionResult IemuMainMenu()
        //{
        //    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
        //    {
        //        PrgService.SearchText = "";
        //        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                
        //        Session["CurrentAction"] = "IemuMainMenu";
        //        if (Session["TableShowStyle"] == null)
        //            Session["TableShowStyle"] = "tableFixHead";
        //        var tableShowStyle = Session["tableShowStyle"].ToString();

        //        ViewBag.tableShowStyle = tableShowStyle;
        //        ViewBag.SearchText = "";
        //        ViewBag.PageInfo = "第 1 頁,共 1 頁";
        //        var model = mainMenus.GetDapperDataList("");
        //        return View(model);
        //    }
        //}

        public ActionResult IemuMainMenu_Create()
        {
            return RedirectToAction("IemuMainMenu");
        }

        //public ActionResult IemuSubMenu()
        //{
        //    using (z_repoIemuSubMenus subMenus = new z_repoIemuSubMenus())
        //    {
        //        PrgService.SearchText = "";
        //        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

        //        Session["CurrentAction"] = "IemuSubMenu";
        //        if (Session["TableShowStyle"] == null)
        //            Session["TableShowStyle"] = "tableFixHead";
        //        var tableShowStyle = Session["TableShowStyle"].ToString();

        //        ViewBag.tableShowStyle = tableShowStyle;
        //        ViewBag.SearchText = "";
        //        ViewBag.PageInfo = "第 1 頁,共 1 頁";
        //        var model = subMenus.GetDapperDataList("");
        //        return View(model);
        //    }
        //}

        public ActionResult IemuSubMenu_Create()
        {
            return PartialView("~/Views/PartialViews/_PartialToBeContinued.cshtml");
        }

        //public ActionResult IemuDetailMenu()
        //{
        //    using (z_repoIemuDetailMenus detailMenus = new z_repoIemuDetailMenus())
        //    {
        //        PrgService.SearchText = "";
        //        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

        //        Session["CurrentAction"] = "IemuDetailMenu";
        //        if (Session["TableShowStyle"] == null)
        //            Session["TableShowStyle"] = "tableFixHead";
        //        var tableShowStyle = Session["TableShowStyle"].ToString();

        //        ViewBag.tableShowStyle = tableShowStyle;
        //        ViewBag.SearchText = "";
        //        ViewBag.PageInfo = "第 1 頁,共 1 頁";
        //        var model = detailMenus.GetDapperDataList("");
        //        return View(model);
        //    }
        //}

        public ActionResult IemuDetailMenu_Create()
        {
            return PartialView("~/Views/PartialViews/_PartialToBeContinued.cshtml");
        }

        //public ActionResult IemuTran()
        //{
        //    using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
        //    {
        //        PrgService.SearchText = "";
        //        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

        //        Session["CurrentAction"] = "IemuTran";
        //        if (Session["TableShowStyle"] == null)
        //            Session["TableShowStyle"] = "tableFixHead";
        //        var tableShowStyle = Session["TableShowStyle"].ToString();

        //        ViewBag.tableShowStyle = tableShowStyle;
        //        ViewBag.SearchText = "";
        //        ViewBag.PageInfo = "第 1 頁,共 1 頁";
        //        var model = iemuTrans.GetDapperDataList("");
        //        return View(model);
        //    }
        //}

        public ActionResult IemuTran_Create()
        {
            return PartialView("~/Views/PartialViews/_PartialToBeContinued.cshtml");
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
            if (Session["CurrentAction"] == null)
                Session["CurrentAction"] = "IemuMainMenu";

            //取得 ActionName 即可得知對應的 ViewName
            var viewName = Session["CurrentAction"].ToString();
            if (viewName == "")
            {
                viewName = "IemuMainMenu";
                Session["CurrentAction"] = "IemuMainMenu";
            }

            Session["TableShowStyle"] = tableShowStyle;

            switch (viewName)
            {
                case "IemuMainMenu":
                    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁,共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = mainMenus.GetDapperDataList("");
                        return View(viewName, model);
                    }

                case "IemuSubMenu":
                    using (z_repoIemuSubMenus subMenus = new z_repoIemuSubMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁,共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = subMenus.GetDapperDataList("");
                        return View(viewName, model);
                    }

                case "IemuDetailMenu":
                    using (z_repoIemuDetailMenus detailMenus = new z_repoIemuDetailMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁,共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = detailMenus.GetDapperDataList("");
                        return View(viewName, model);
                    }

                case "IemuTran":
                    using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁,共 1 頁";
                        ViewBag.tableShowStyle = tableShowStyle;
                        var model = iemuTrans.GetDapperDataList("");
                        return View(viewName, model);
                    }

                default:
                    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁,共 1 頁";
                        ViewBag.tableShowStyle = "tableFixHead";
                        var model = mainMenus.GetDapperDataList("");
                        return View(viewName, model);
                    }
            }
        }

        public ActionResult EditIemu()
        {
            return View();
        }
    }
}