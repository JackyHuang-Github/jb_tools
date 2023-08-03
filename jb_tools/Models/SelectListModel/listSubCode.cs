using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

// Jacky 1120731 中分類代號的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 中分類代號下拉選單
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> SubCodeList(string mainCode)
    {
        using (z_repoIemuSubMenus iemuSubMenus = new z_repoIemuSubMenus())
        {
            var data = iemuSubMenus.repo.ReadAll(m => m.MainCode == mainCode)
                .OrderBy(m => m.SubCode)
                .Select(u => new SelectListItem
                {
                    Text = u.SubCode + SelectListService.Delimiter + u.ScId + SelectListService.Delimiter + u.Name,
                    Value = u.ScId
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}