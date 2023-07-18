using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuTranController : Controller
    {
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

        public ActionResult CreateEdit(int id = 0)
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Small);
                ViewBag.CardSize = PrgService.CardSize;
                var model = iemuTrans.repo.ReadSingle(m => m.Id == id);
                return View(model);
            }
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}