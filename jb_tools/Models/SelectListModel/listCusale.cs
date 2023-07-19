﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Jacky 1120719 為 iepb03h 所建構的下拉選單
public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 經辦人列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> CusaleList()
    {
        using (z_repoiepb03h model = new z_repoiepb03h())
        {
            var data = model.repo.ReadAll()
                .OrderBy(m => m.cu_sale)
                .Select(u => new SelectListItem
                {
                    Text = u.cu_sale + " " + u.cu_snam,
                    Value = u.cu_sale
                }).ToList();
            return (List<SelectListItem>)data;
        }
    }
}
