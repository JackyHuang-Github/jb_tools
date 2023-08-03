using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

// Jacky 1120731 細分類序號的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 細分類序號下拉選單
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> DetailOrderList(string mainCode, string scId)
    {
        using (z_repoIemuDetailMenus iemuDetailMenus = new z_repoIemuDetailMenus())
        {
            var data = iemuDetailMenus.repo.ReadAll(m => m.MainCode == mainCode && m.ScId == scId)
                .OrderBy(m => m.DetailOrder)
                .Select(u => new SelectListItem
                {
                    Text = u.DetailOrder + SelectListService.Delimiter + u.Program + SelectListService.Delimiter + u.Name,
                    Value = u.DetailOrder
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}