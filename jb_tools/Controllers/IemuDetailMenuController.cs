using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuDetailMenuController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int page = 1, int pageSize = PageListService.CountPerPage, string searchText = "")
        {
            using (z_repoIemuDetailMenus detailMenus = new z_repoIemuDetailMenus())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuDetailMenu";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixedHeadHover";
                var tableShowStyle = Session["TableShowStyle"].ToString();

                // var model = detailMenus.GetDapperDataList("");
                // Jacky 1120726 for 分頁模式
                var model = detailMenus.repo.ReadAll().OrderBy(m => m.SortNum).ToPagedList(page, pageSize);
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);

                ViewBag.tableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";
                ViewBag.PageInfo = $"第 {model.PageNumber} 頁，共 {model.PageCount} 頁";

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