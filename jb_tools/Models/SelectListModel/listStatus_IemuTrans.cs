using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

// Jacky 1120719 為 IemuTrans 的 Status(狀態碼) 所建構的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 狀態列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> Status_IemuTransList()
    {
        using (z_repoCodeDatas model = new z_repoCodeDatas())
        {
            const string baseNo = "Status_IemuTrans";
            var data = model.repo.ReadAll(m => m.BaseNo == baseNo)
                .OrderBy(m => m.SortNo)
                .ThenBy(m => m.CodeNo)
                .Select(u => new SelectListItem
                {
                    Text = u.CodeNo + SelectListService.Delimiter + u.CodeName,
                    Value = u.CodeNo
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}