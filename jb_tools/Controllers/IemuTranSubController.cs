using DocumentFormat.OpenXml.Wordprocessing;
using jb_tools.Models;
using jb_tools.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using PagedList;
using System.Drawing.Printing;

namespace jb_tools.Controllers
{
    public class IemuTranSubController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int id = 0, int page = PageListService.CountPerPage, int pageSize = PageListService.CountPerPage, string searchText = "")
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    PrgService.SearchText = "";
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                    Session["CurrentController"] = "IemuTranSub";

                    // Jacky 1120725 設定表格樣式
                    /* ----------------------------------------------------------------------------------------------------------- */
                    string multipleTablesNormalHead = "";
                    string multipleTablesFixedHead = "";

                    if (Session["TableShowStyle"] == null)
                    {
                        Session["TableShowStyle"] = "tableFixedHeadHover";
                    }
                    string tableShowStyle = Session["TableShowStyle"].ToString();
                    if (tableShowStyle == "")
                    {
                        tableShowStyle = "tableFixedHeadHover";
                    }

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
                    /* ----------------------------------------------------------------------------------------------------------- */

                    // Jacky 1120726 for 分頁模式
                    vmIemuTranModel vmModel = new vmIemuTranModel();
                    vmModel.IemuTranModel = iemuTrans.GetDapperData(id);
                    IPagedList<IemuTranDetails> model = iemuTranDetails.repo.ReadAll().Where(m => m.No == vmModel.IemuTranModel.No).OrderBy(m => m.Seq).ToPagedList(page, pageSize);
                    vmModel.IemuTranDetailsModel = (IPagedList<IemuTranDetails>)model;
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);

                    // Jacky 1120725 設定表格樣式
                    /* ----------------------------------------------------------------------------------------------------------- */
                    ViewBag.MultipleTablesShowStyleNormalHead = multipleTablesNormalHead;
                    ViewBag.MultipleTablesShowStyleFixedHead = multipleTablesFixedHead;
                    /* ----------------------------------------------------------------------------------------------------------- */
                    ViewBag.SearchText = "";
                    ViewBag.PageInfo = $"第 {model.PageNumber} 頁，共 {model.PageCount} 頁";

                    return View(vmModel);
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateEdit(int id = 0)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BringStandardValues(string no) 
        { 
            return View(); 
        }
    }
}