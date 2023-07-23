using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace jb_tools.Controllers
{
    public class IemuTranController : Controller
    {
        [HttpGet]
        // GET: IemuTran
        public ActionResult Index(int page = 1, string searchText = "")
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuTran";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixHead";
                var tableShowStyle = Session["TableShowStyle"].ToString();

                var model = iemuTrans.GetDapperDataList(searchText);
                //var model = modelData.ToPagedList(page, PrgService.PageSize);
                ViewBag.tableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";
                //ViewBag.PageInfo = PrgService.SetIndex(model.PageNumber, model.PageCount, searchText);

                return View(model);
            }
        }

        [HttpGet]
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