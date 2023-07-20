using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Jacky 1120719 為 iecusuh 所建構的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 客戶列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> CustomerList()
    {
        using (z_repoiecusuh model = new z_repoiecusuh())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.cu_no)
                .Select(u => new SelectListItem
                {
                    Text = u.cu_no + SelectListService.Delimiter + u.cu_na,
                    Value = u.cu_no
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}
