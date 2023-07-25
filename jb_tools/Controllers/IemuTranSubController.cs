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
        public ActionResult Index(int id = 0, int page = 1, string searchText = "")
        {
            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using(z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    PrgService.SearchText = "";
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                    Session["CurrentController"] = "IemuTranSub";

                    // Jacky 1120725 設定表格樣式
                    /* ----------------------------------------------------------------------------------------------------------- */
                    string multipleTablesNormalHead = "";
                    string multipleTablesFixedHead = "";

                    string tableShowStyle = Session["TableShowStyle"].ToString();
                    if (tableShowStyle == "")
                    {
                        tableShowStyle = "tableFixedHead";
                    }

                    if (tableShowStyle == "tableFixedHead")
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

                    vmIemuTranModel vmModel = new vmIemuTranModel();
                    vmModel.IemuTranModel = iemuTrans.GetDapperData(id);
                    vmModel.IemuTranDetailsModel = iemuTranDetails.GetDapperDataTranDetailsList(vmModel.IemuTranModel.No);
                    //var vmModel = modelData.ToPagedList(page, PrgService.PageSize);

                    // Jacky 1120725 設定表格樣式
                    /* ----------------------------------------------------------------------------------------------------------- */
                    ViewBag.MultipleTablesShowStyleNormalHead = multipleTablesNormalHead;
                    ViewBag.MultipleTablesShowStyleFixedHead = multipleTablesFixedHead;
                    /* ----------------------------------------------------------------------------------------------------------- */

                    ViewBag.SearchText = "";
                    //ViewBag.PageInfo = PrgService.SetIndex(vmModel.PageNumber, vmModel.PageCount, searchText);

                    return View(vmModel);
                }
            }
        }
    }
}