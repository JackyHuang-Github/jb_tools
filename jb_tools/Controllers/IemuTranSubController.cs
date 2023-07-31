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
        public ActionResult Index(int id = 0, int page = 1, int pageSize = PaginationService.CountPerPage, string searchText = "")
        {
            // Jacky 1120731 抓取 Web.config 裡的設定值
            pageSize = PaginationService.GetCountPerPage();

            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    PrgService.SearchText = "";
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                    Session["CurrentController"] = "IemuTranSub";

                    // Jacky 1120725 設定表格樣式
                    SetTableShowStyle();

                    // Jacky 1120730 建立 List<T> 與 IPagedList<T> 的結合 Model
                    vmIemuTranSubModel vmModel = new vmIemuTranSubModel();

                    // Jacky 1120730 表頭 IemuTrans 的 Model
                    // 注意：這個 Model 一定要先做，因為需要這個表頭的單號來取表身資料
                    vmModel.IemuTransModel = iemuTrans.GetDapperData(id);

                    // Jacky 1120726 for 分頁模式
                    // Jacky 1120730 IPagedList<T> 的 Model
                    IPagedList<IemuTranDetails> iemuTranDetailsPLModel = iemuTranDetails.repo.ReadAll().Where(m => m.No == vmModel.IemuTransModel.No).OrderBy(m => m.Seq).ToPagedList(page, pageSize);
                    vmModel.IemuTranDetailsPLModel = iemuTranDetailsPLModel;
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, vmModel.IemuTranDetailsPLModel.PageNumber, vmModel.IemuTranDetailsPLModel.PageCount);

                    // Jacky 1120730 List<T> 的 Model
                    // 注意：這裡另外寫這支程式是為了要搭配 IPagedList 分頁模式，且要顯示額外資料的考量，而不用 GetDapperDataList()
                    //       若像標準資料檔(大中細標準資料檔)，因為不需要使用 [NotMapped] 顯示額外資料，就只要用 IPagedList 即可。
                    // 為搭配 IPagedList 分頁模式所做的資料讀取，就不會一次把資料全部讀進來，造成嚴重 Lag
                    int startIndex = iemuTranDetailsPLModel.FirstItemOnPage - 1;
                    int endIndex = iemuTranDetailsPLModel.LastItemOnPage - 1;
                    List<IemuTranDetails> iemuTranDetailsModel = iemuTranDetails.GetDapperDataListRange(vmModel.IemuTransModel.No, page, pageSize, startIndex, endIndex);
                    vmModel.IemuTranDetailsModel = iemuTranDetailsModel;

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
        //[ValidateAntiForgeryToken]
        /// Jacky 1120727
        /// <summary>
        /// 帶入標準資料
        /// no (單號)
        /// </summary>
        public ActionResult BringStandardValues(string no)
        {
            using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
            {
                Session["CurrentController"] = "IemuTranSub";

                // Jacky 1120727 帶入標準資料
                string errorMessage = iemuTranDetails.BringStandardValues(no);
                dmJsonMessage result = new dmJsonMessage() 
                { 
                    // errorMessage 若為空則 Mode = true，否則為 false
                    Mode = string.IsNullOrEmpty(errorMessage), 
                    Message = string.IsNullOrEmpty(errorMessage) ? "已帶入標準資料至表身！" : errorMessage
                };
                return Json(result, JsonRequestBehavior.AllowGet);
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