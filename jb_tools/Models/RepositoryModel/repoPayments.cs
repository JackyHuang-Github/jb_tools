using Dapper;
using jb_tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Payments CRUD
/// </summary>
public class z_repoPayments : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<Payments> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoPayments()
    {
        repo = new EFGenericRepository<Payments>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<Payments> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<Payments>(str_query);
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
Id, PaymentNo, PaymentName, Remark FROM Payments 
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
            str_query += " WHERE (";
            str_query += $"PaymentNo LIKE '%{searchText}%'  OR ";
            str_query += $"PaymentName LIKE '%{searchText}%'  OR ";
            str_query += $"Remark LIKE '%{searchText}%'  ";
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
        return " ORDER BY  PaymentNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Payments model)
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
    /// 取得名稱
    /// <summary>
    /// <param name="dataNo">編號</param>
    /// <returns></returns>
    public string GetDataName(string dataNo)
    {
        string str_value = "";
        var model = repo.ReadSingle(m => m.PaymentNo == dataNo);
        if (model != null) str_value = model.PaymentName;
        return str_value;
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
