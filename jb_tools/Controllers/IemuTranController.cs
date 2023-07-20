using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuTranController : Controller
    {
        [HttpGet]
        // GET: IemuTran
        public ActionResult Index()
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                PrgService.SearchText = "";
                PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                Session["CurrentController"] = "IemuTran";
                if (Session["TableShowStyle"] == null)
                    Session["TableShowStyle"] = "tableFixHead";
                var tableShowStyle = Session["TableShowStyle"].ToString();

                ViewBag.tableShowStyle = tableShowStyle;
                ViewBag.SearchText = "";
                ViewBag.PageInfo = "第 1 頁，共 1 頁";
                var model = iemuTrans.GetDapperDataList("");
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
                ViewBag.CardSize = PrgService.CardSize;
                var model = iemuTrans.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    model = new IemuTrans();
                    // 設定新增預設值
                    using (AttributeService attr = new AttributeService())
                    {
                        model.No = iemuTrans.GetNewNo();
                        model.Date = DateTime.Today;
                        model.Status = (string)attr.GetDefaultValue<z_repoIemuTrans>("Status");
                        model.CuNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuNo");
                        model.CuSale = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuSale");
                        model.IndustryNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("IndustryNo");
                        model.Remark = (string)attr.GetDefaultValue<z_repoIemuTrans>("Remark");
                    }
                }
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(IemuTrans model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaIemuTrans>(ModelState);
                return View(model);
            }

            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                iemuTrans.CreateEdit(model);
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = "" });
            }
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
    }
}