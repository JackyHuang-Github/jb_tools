using jb_tools.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jb_tools.Controllers
{
    public class IemuTranSubController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int id, int page = 1, string searchText = "")
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using(z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    PrgService.SearchText = "";
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                    Session["CurrentController"] = "IemuTranSub";
                    if (Session["TableShowStyle"] == null)
                        Session["TableShowStyle"] = "tableFixHead";
                    var tableShowStyle = Session["TableShowStyle"].ToString();

                    vmIemuTranModel vmModel = new vmIemuTranModel();
                    vmModel.IemuTranModel = iemuTrans.GetDapperData(id);
                    vmModel.IemuTranDetailsModel = iemuTranDetails.GetDapperDataTranDetailsList(vmModel.IemuTranModel.No);
                    //var vmModel = modelData.ToPagedList(page, PrgService.PageSize);
                    ViewBag.tableShowStyle = tableShowStyle;
                    ViewBag.SearchText = "";
                    //ViewBag.PageInfo = PrgService.SetIndex(vmModel.PageNumber, vmModel.PageCount, searchText);

                    return View(vmModel);
                }
            }
        }
    }
}