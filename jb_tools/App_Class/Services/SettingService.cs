using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class SettingService
{
    /// <summary>
    /// Table (表格) 的顯示列表
    /// </summary>
    public enum EnumTableShowStyle
    {
        /// <summary>
        // 固定表頭(一畫面僅有一個表格 Table)
        /// </summary>
        TableFixedHead,
        /// <summary>
        /// 陰影立體效果(一畫面僅有一個表格 Table)
        /// </summary>
        TableFixedHeadBorderShadow,
        /// <summary>
        /// 固定表頭(一畫面有多個 Tables)
        /// </summary>
        TableMultipleFixedHead,
        /// <summary>
        /// 陰影立體效果(一畫面有多個 Tables)
        /// </summary>
        TabMultipleFixedBorderShadow
    }

    /// <summary>
    /// 設定表格的顯示方式
    /// </summary>
    public static void SetTableShowStyle(EnumTableShowStyle enTableShowStyle)
    {
        //if (Session["CurrentController"] == null)
        //    Session["CurrentController"] = "IemuMainMenu";

        //var controllerName = Session["CurrentController"].ToString();
        //if (controllerName == "")
        //{
        //    controllerName = "IemuMainMenu";
        //    Session["CurrentController"] = "IemuMainMenu";
        //}

        var controllerName = SessionService.GetValue("CurrentController", "IemuMainMenu");

        // Jacky 1120725 為 IemuTran Multiple Table 額外判斷
        //if (controllerName == "IemuTranSub")
        //{
        //    string tableShowStyleForNormal = "";

        //    if (enTableShowStyle == EnumTableShowStyle.TableFixedHead)
        //    {
        //        enTableShowStyle = EnumTableShowStyle.TableMultipleFixedHead;

        //        tableShowStyle = "tableMultipleFixHead";
        //        tableShowStyleForNormal = "tableMultipleNormalHead";
        //    }
        //    if (tableShowStyle == "tableFixedHeadBorderShadow")
        //    {
        //        tableShowStyle = "tableMultipleFixHeadBorderShadow";
        //        tableShowStyleForNormal = "tableMultipleNormalHeadBorderShadow";
        //    }

        //    Session["TableShowStyleForNormal"] = tableShowStyleForNormal;
        //    SessionService.SetValue("TableShowStyleForNormal", )
        //}

        //Session["TableShowStyle"] = tableShowStyle;
        //SessionService.SetValue("TableShowStyle", tableShowStyle);

        //switch (controllerName)
        //{
        //    case "IemuMainMenu":
        //        using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
        //        {
        //            PrgService.SearchText = "";
        //            PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
        //            ViewBag.SearchText = "";
        //            ViewBag.PageInfo = "第 1 頁，共 1 頁";
        //            ViewBag.TableShowStyle = tableShowStyle;
        //            var model = mainMenus.GetDapperDataList("");
        //            return RedirectToAction("Index", "IemuMainMenu", model);
        //        }

        //    case "IemuSubMenu":
        //        using (z_repoIemuSubMenus subMenus = new z_repoIemuSubMenus())
        //        {
        //            PrgService.SearchText = "";
        //            PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
        //            ViewBag.SearchText = "";
        //            ViewBag.PageInfo = "第 1 頁，共 1 頁";
        //            ViewBag.TableShowStyle = tableShowStyle;
        //            var model = subMenus.GetDapperDataList("");
        //            return RedirectToAction("Index", "IemuSubMenu", model);
        //        }

        //    case "IemuDetailMenu":
        //        using (z_repoIemuDetailMenus detailMenus = new z_repoIemuDetailMenus())
        //        {
        //            PrgService.SearchText = "";
        //            PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
        //            ViewBag.SearchText = "";
        //            ViewBag.PageInfo = "第 1 頁，共 1 頁";
        //            ViewBag.TableShowStyle = tableShowStyle;
        //            var model = detailMenus.GetDapperDataList("");
        //            return RedirectToAction("Index", "IemuDetailMenu", model);
        //        }

        //    case "IemuTran":
        //        using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
        //        {
        //            PrgService.SearchText = "";
        //            PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
        //            ViewBag.SearchText = "";
        //            ViewBag.PageInfo = "第 1 頁，共 1 頁";
        //            ViewBag.TableShowStyle = tableShowStyle;
        //            var model = iemuTrans.GetDapperDataList("");
        //            return RedirectToAction("Index", "IemuTran", model);
        //        }

        //    default:
        //        using (z_repoIemuMainMenus mainMenus = new z_repoIemuMainMenus())
        //        {
        //            PrgService.SearchText = "";
        //            PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);
        //            ViewBag.SearchText = "";
        //            ViewBag.PageInfo = "第 1 頁，共 1 頁";
        //            ViewBag.TableShowStyle = "tableFixedHead";
        //            var model = mainMenus.GetDapperDataList("");
        //            return RedirectToAction("Index", "IemuMainMenu", model);
        //        }
        //}
    }
}
