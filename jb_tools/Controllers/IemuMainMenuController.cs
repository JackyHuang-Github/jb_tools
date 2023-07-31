using DocumentFormat.OpenXml.Wordprocessing;
using jb_tools.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuMainMenuController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int page = 1, int pageSize = PaginationService.CountPerPage, string searchText = "")
        {
            // Jacky 1120731 抓取 Web.config 裡的設定值
            pageSize = PaginationService.GetCountPerPage();

            using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuMainMenu";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixedHeadHover";
                var tableShowStyle = Session["tableShowStyle"].ToString();

                // var model = mainMenus.GetDapperDataList("");
                // Jacky 1120726 for 分頁模式
                var model = mainMenus.repo.ReadAll().OrderBy(m => m.SortNum).ToPagedList(page, pageSize);
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);

                ViewBag.TableShowStyle = tableShowStyle;
                ViewBag.SearchText = searchText;

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