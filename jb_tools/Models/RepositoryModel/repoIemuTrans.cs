using Dapper;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing.Charts;
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
	IemuTrans.Id, IemuTrans.No, IemuTrans.Date, IemuTrans.Status, IemuTrans.CuNo, iecusuh.cu_na, IemuTrans.CuSale, 
    iepb03h.cu_snam, IemuTrans.IndustryNo, Industries.IndustryName, IemuTrans.Remark
FROM IemuTrans 
LEFT OUTER JOIN iecusuh ON IemuTrans.CuNo = iecusuh.cu_no
LEFT OUTER JOIN iepb03h ON IemuTrans.CuSale = iepb03h.cu_sale 
LEFT OUTER JOIN Industries ON IemuTrans.IndustryNo = Industries.IndustryNo
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
            str_query += $"IemuTrans.Date LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.Status LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CuNo LIKE '%{searchText}%' OR ";
            str_query += $"iecusuh.CuNa LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.CuSale LIKE '%{searchText}%' OR ";
            str_query += $"iepb03h.cu_snam LIKE '%{searchText}%' OR ";
            str_query += $"IemuTrans.IndustryNo LIKE '%{searchText}%' OR ";
            str_query += $"Industries.IndustryName LIKE '%{searchText}%' OR ";
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
        return " ORDER BY IemuTrans.No";
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
    /// <summary>
    /// 取得新單號
    /// </summary>
    /// <returns></returns>
    public string GetNewNo()
    {
        string newNo = "";
        string today = 
            string.Format(DateTime.Now.ToString("yyyy")) +
            string.Format(DateTime.Now.ToString("MM")) +
            string.Format(DateTime.Now.ToString("dd"));

        var iemuTrans = new List<IemuTrans>();
        var count = iemuTrans
            .Count(x => x.No.Substring(0, 4).Equals("2023"));
        count++;
        newNo = $"{today}{count:000}";

        //.Where(x => x.No.Substring(0, 8) == today)

        return newNo;
    }
    #endregion
}
