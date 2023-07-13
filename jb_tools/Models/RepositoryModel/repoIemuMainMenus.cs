﻿using Dapper;
using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class z_repoIemuMainMenus : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<IemuMainMenus> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoIemuMainMenus()
    {
        repo = new EFGenericRepository<IemuMainMenus>(new dbEntities());
    }

    /// <summary>
    /// 以 Dapper 來讀取單筆 IEMenu 大分類 資料
    /// </summary>
    /// <param name="id">IEMenu 大分類 Id</param>
    /// <returns></returns>
    public IemuMainMenus GetDapperData(int id)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += " WHERE IemuMainMenus.Id = @id";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("id", id);
            var model = dp.ReadSingle<IemuMainMenus>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<IemuMainMenus> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<IemuMainMenus>(str_query);
            return model;
        }
    }

    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// <summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT 
	IemuMainMenus.Id, IemuMainMenus.SortNum, IemuMainMenus.MainCode, IemuMainMenus.McId, IemuMainMenus.Name, IemuMainMenus.Ename, IemuMainMenus.Remark
FROM IemuMainMenus
        ";
        return str_query;
    }

    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " WHERE ( ";
            str_query += $"IemuMainMenus.SortNum LIKE '%{searchText}%' OR ";
            str_query += $"IemuMainMenusStatus.MainCode LIKE '%{searchText}%' OR ";
            str_query += $"IemuMainMenus.McId LIKE '%{searchText}%' OR ";
            str_query += $"IemuMainMenus.Name LIKE '%{searchText}%' OR ";
            str_query += $"IemuMainMenus.Ename LIKE '%{searchText}%' OR ";
            str_query += $"IemuMainMenus.Remark LIKE '%{searchText}%' ";
            str_query += ") ";
        }
        return str_query;
    }

    /// <summary>
    /// 取得 SQL 排序
    /// <summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY IemuMainMenus";
    }

    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(IemuMainMenus model)
    {
        repo.CreateEdit(model, model.Id);
    }

    /// <summary>
    /// 刪除
    /// <summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) repo.Delete(model, true);
    }

    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    #endregion
}