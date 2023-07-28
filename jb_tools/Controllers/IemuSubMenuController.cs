using DocumentFormat.OpenXml.Wordprocessing;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuSubMenuController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int page = 1, int pageSize = PageListService.CountPerPage, string searchText = "")
        {
            using (z_repoIemuSubMenus subMenus = new z_repoIemuSubMenus())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuSubMenu";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixedHeadHover";
                var tableShowStyle = Session["TableShowStyle"].ToString();

                // var model = subMenus.GetDapperDataList("");
                // Jacky 1120726 for 分頁模式
                var model = subMenus.repo.ReadAll().OrderBy(m => m.SortNum).ToPagedList(page, pageSize);
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);

                ViewBag.TableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";

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