using jb_tools.Models;
using jb_tools.Models.ViewModel;
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
            return ChoiceActionResult("tableFixedHeadHover");
        }

        public ActionResult TableShowStyle_BorderShadow()
        {
            return ChoiceActionResult("tableFixedHeadBorderShadow");
        }

        private ActionResult ChoiceActionResult(string tableShowStyle)
        {
            if (Session["CurrentController"] == null)
                Session["CurrentController"] = "IemuMainMenu";

            var controllerName = Session["CurrentController"].ToString();

            // Jacky 1120725 為 IemuTranSub 的多表格做額外判斷
            if (controllerName == "IemuTranSub")
            {
                string multipleTablesNormalHead = "";
                string multipleTablesFixedHead = "";

                if (tableShowStyle == "tableFixedHeadHover")
                {
                    multipleTablesNormalHead = "tableMultipleNormalHead";
                    multipleTablesFixedHead = "tableMultipleFixedHead";
                }
                if (tableShowStyle == "tableFixedHeadBorderShadow")
                {
                    multipleTablesNormalHead = "tableMultipleNormalHeadBorderShadow";
                    multipleTablesFixedHead = "tableMultipleFixedHeadBorderShadow";
                }

                Session["MultipleTablesShowStyleNormalHead"] = multipleTablesNormalHead;
                Session["MultipleTablesShowStyleFixedHead"] = multipleTablesFixedHead;
            }
            else
            {
                Session["TableShowStyle"] = tableShowStyle;
            }

            switch (controllerName)
            {
                case "IemuMainMenu":
                    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.TableShowStyle = tableShowStyle;
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
                        ViewBag.TableShowStyle = tableShowStyle;
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
                        ViewBag.TableShowStyle = tableShowStyle;
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
                        ViewBag.TableShowStyle = tableShowStyle;
                        var model = iemuTrans.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuTran", model);
                    }

                default:
                    using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
                    {
                        PrgService.SearchText = "";
                        PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
                        ViewBag.SearchText = "";
                        ViewBag.PageInfo = "第 1 頁，共 1 頁";
                        ViewBag.TableShowStyle = "tableFixedHeadHover";
                        var model = mainMenus.GetDapperDataList("");
                        return RedirectToAction("Index", "IemuMainMenu", model);
                    }
            }
        }
    }
}