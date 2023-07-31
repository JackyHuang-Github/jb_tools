using PagedList;
using DocumentFormat.OpenXml.Wordprocessing;
using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using jb_tools.Models.ViewModel;

namespace jb_tools.Controllers
{
    public class IemuTranController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        //public ActionResult Index(string searchText = "")
        public ActionResult Index(int page = 1, int pageSize = PaginationService.CountPerPage, string searchText = "")
        {
            // Jacky 1120731 抓取 Web.config 裡的設定值
            pageSize = PaginationService.GetCountPerPage();

            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuTran";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixedHeadHover";
                var tableShowStyle = Session["TableShowStyle"].ToString();

                // Jacky 1120730 建立 List<T> 與 IPagedList<T> 的結合 Model
                vmIemuTranModel vmModel = new vmIemuTranModel();

                // Jacky 1120726 for 分頁模式
                // Jacky 1120730 IPagedList<T> 的 Model
                IPagedList<IemuTrans> iemuTransPLModel = iemuTrans.repo.ReadAll().OrderByDescending(m => m.No).ToPagedList(page, pageSize);
                vmModel.IemuTransPLModel = iemuTransPLModel;
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, iemuTransPLModel.PageNumber, iemuTransPLModel.PageCount);

                // Jacky 1120730 List<T> 的 Model
                // 注意：這裡另外寫這支程式是為了要搭配 IPagedList 分頁模式，且要顯示額外資料的考量，而不用 GetDapperDataList()
                //       若像標準資料檔(大中細標準資料檔)，因為不需要使用 [NotMapped] 顯示額外資料，就只要用 IPagedList 即可。
                // 為搭配 IPagedList 分頁模式所做的資料讀取，就不會一次把資料全部讀進來，造成嚴重 Lag
                int startIndex = iemuTransPLModel.FirstItemOnPage - 1;
                int endIndex = iemuTransPLModel.LastItemOnPage - 1;
                List<IemuTrans> iemuTransModel = iemuTrans.GetDapperDataListRange(page, pageSize, startIndex, endIndex);
                vmModel.IemuTransModel = iemuTransModel;

                ViewBag.TableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";

                return View(vmModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateEdit(int id = 0)
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Small);
                ViewBag.Action = action;
                ViewBag.CardSize = PrgService.CardSize;
                var model = iemuTrans.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    model = new IemuTrans();
                    // 設定新增預設值
                    using (AttributeService attr = new AttributeService())
                    {
                        model.No = iemuTrans.GetNewNo();
                        model.Date = DateTime.Today.Date;

                        // Jacky 1120723
                        // for TextBoxFor 使用，不然在新增畫面時，帶不出 [狀態碼]、[狀態名稱]
                        model.Status = "E";     // 編輯中
                        model.CodeName = iemuTrans.GetCodeName(model.Status);

                        model.CuNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuNo");
                        model.CuSale = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuSale");
                        model.IndustryNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("IndustryNo");
                        model.Remark = (string)attr.GetDefaultValue<z_repoIemuTrans>("Remark");
                    }
                }
                else
                {
                    // Jacky 1120723
                    // for TextBoxFor 使用，不然在修改畫面時，帶不出 [客戶簡稱]
                    model.cu_na = iemuTrans.GetCustomerSimpleName(model.CuNo);
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(IemuTrans model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaIemuTrans>(ModelState);
                return View(model);
            }

            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                enAction action = (model.Id == 0) ? enAction.Create : enAction.Edit;
                ViewBag.Action = action;
                iemuTrans.CreateEdit(model);
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            //檢查刪除權限
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                iemuTrans.Delete(id);
                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}