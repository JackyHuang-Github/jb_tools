using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Jacky 1120719 為 Industries 所建構的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 產業別下拉選單
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> IndustryList()
    {
        using (z_repoIndustries industries = new z_repoIndustries())
        {
            var data = industries.repo.ReadAll()
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
