using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public static class PaginationService
{
    public const int CountPerPage = 40;

    /// Jacky 1120731
    /// <summary>
    /// 定義每頁的資料筆數於 Web.config
    /// </summary>
    public static int GetCountPerPage()
    {
        string configName = "Pagination_CountPerPage";
        string countPerPage = "40";

        if (string.IsNullOrEmpty(SessionService.GetValue(configName)))
        {
            object obj_pagination = WebConfigurationManager.AppSettings[configName];
            countPerPage = (obj_pagination == null) ? countPerPage : obj_pagination.ToString();
            SessionService.SetValue(configName, countPerPage);
        }

        return Convert.ToInt32(SessionService.GetValue(configName));
    }

}
