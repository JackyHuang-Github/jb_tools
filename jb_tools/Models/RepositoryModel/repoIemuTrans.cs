using Dapper;
using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class z_repoIemuTrans : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<IemuTrans> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoIemuTrans()
    {
        repo = new EFGenericRepository<IemuTrans>(new dbEntities());
    }

    /// <summary>
    /// 以 Dapper 來讀取單筆 IEMenu 交易檔(表頭) 資料
    /// </summary>
    /// <param name="id">IEMenu 交易檔(表頭) Id</param>
    /// <returns></returns>
    public IemuTrans GetDapperData(int id)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += " WHERE IemuTrans.Id = @id";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("id", id);
            var model = dp.ReadSingle<IemuTrans>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<IemuTrans> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<IemuTrans>(str_query);
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
	IemuTrans.Id, IemuTrans.No, IemuTrans.Status, IemuTrans.CuNo, IemuTrans.CuNa, IemuTrans.CuName, IemuTrans.CuEname, 
	IemuTrans.CuSale, IemuTrans.CusaleName, IemuTrans.IndustryNo, IemuTrans.IndustryName, IemuTrans.Remark
FROM IemuTrans
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
            str_query += $"IemuTrans.No LIKE '%{searchText}%' OR ";
            str_query += $"IemuTransStatus.Status LIKE '%{searchText}%' OR ";
            str_query += $"IemuTransStatus.CuNo LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CuNa LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CuName LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CuEname LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CuSale LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CusaleName LIKE '%{searchText}%' OR ";
            str_query += $"IemuTransStatus.IndustryNo LIKE '%{searchText}%' OR ";
            str_query += $"IemuTransStatus.IndustryName LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.Remark LIKE '%{searchText}%' ";
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
        return " ORDER BY No";
    }

    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(IemuTrans model)
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
