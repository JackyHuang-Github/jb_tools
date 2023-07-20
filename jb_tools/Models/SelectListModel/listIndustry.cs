using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Jacky 1120719 為 Industries 所建構的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 產業別列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> IndustryList()
    {
        using (z_repoIndustries model = new z_repoIndustries())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.IndustryNo)
                .Select(u => new SelectListItem
                {
                    Text = u.IndustryNo + SelectListService.Delimiter + u.IndustryName,
                    Value = u.IndustryNo
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}
