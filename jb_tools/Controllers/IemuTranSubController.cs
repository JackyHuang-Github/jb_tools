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
                    SetTableShowStyle();

                    vmIemuTranModel vmModel = new vmIemuTranModel();
                    vmModel.IemuTranModel = iemuTrans.GetDapperData(id);

                    // Jacky 1120726 for 分頁模式
                    IPagedList<IemuTranDetails> model = iemuTranDetails.repo.ReadAll().Where(m => m.No == vmModel.IemuTranModel.No).OrderBy(m => m.Seq).ToPagedList(page, pageSize);
                    // Jacky 1120727
                    // 注意：這裡必須再將 model 強迫轉型成 IPagedList<IemuTranDetails> 型態，不然 Compiler 會持續報錯，且下次程式會不斷從這裡開始
                    vmModel.IemuTranDetailsModel = (IPagedList<IemuTranDetails>)model;
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);

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
        /// Jacky 1120727
        /// <summary>
        /// 帶入標準資料
        /// no (單號)
        /// </summary>
        public ActionResult BringStandardValues(string no, int page = PageListService.CountPerPage, int pageSize = PageListService.CountPerPage, string searchText = "")
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    Session["CurrentController"] = "IemuTranSub";

                    vmIemuTranModel vmModel = new vmIemuTranModel();
                    vmModel.IemuTranModel = iemuTrans.GetDapperDataByNo(no);

                    // Jacky 1120727 帶入標準資料
                    string errorMessage = iemuTranDetails.BringStandardValues(no);
                    if (!string.IsNullOrEmpty(errorMessage)) 
                    {
                        string errorMessage2 = "";
                        errorMessage2 = errorMessage;
                    }

                    // Jacky 1120726 for 分頁模式
                    IPagedList<IemuTranDetails> model = iemuTranDetails.repo.ReadAll().Where(m => m.No == no).OrderBy(m => m.Seq).ToPagedList(page, pageSize);
                    // Jacky 1120727
                    // 注意：這裡必須再將 model 強迫轉型成 IPagedList<IemuTranDetails> 型態，不然 Compiler 會持續報錯，且下次程式會不斷從這裡開始
                    vmModel.IemuTranDetailsModel = (IPagedList<IemuTranDetails>)model;
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, model.PageNumber, model.PageCount);

                    return View(vmModel);
                }
            }
        }

        /// <summary>
        /// 設定表格樣式
        /// </summary>
        private void SetTableShowStyle()
        {
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

            ViewBag.MultipleTablesShowStyleNormalHead = multipleTablesNormalHead;
            ViewBag.MultipleTablesShowStyleFixedHead = multipleTablesFixedHead;
        }
    }
}