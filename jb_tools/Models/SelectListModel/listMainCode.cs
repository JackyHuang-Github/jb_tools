using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

// Jacky 1120731 大分類代號的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 大分類代號下拉選單
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> MainCodeList()
    {
        using (z_repoIemuMainMenus iemuMainMenus = new z_repoIemuMainMenus())
        {
            var data = iemuMainMenus.repo.ReadAll()
                .OrderBy(m => m.MainCode)
                .Select(u => new SelectListItem
                {
                    Text = u.MainCode + SelectListService.Delimiter + u.McId + SelectListService.Delimiter + u.Name,
                    Value = u.MainCode
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}