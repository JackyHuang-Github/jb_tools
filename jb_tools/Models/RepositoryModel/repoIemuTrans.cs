using Dapper;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Wordprocessing;
using jb_tools.Models;
using Microsoft.Ajax.Utilities;
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
	IemuTrans.Id, IemuTrans.No, IemuTrans.Date, IemuTrans.Status, CodeDatas.CodeName, IemuTrans.CuNo, iecusuh.cu_na, 
    IemuTrans.CuSale, iepb03h.cu_snam, IemuTrans.IndustryNo, Industries.IndustryName, IemuTrans.Remark
FROM IemuTrans 
LEFT OUTER JOIN CodeDatas ON IemuTrans.Status = CodeDatas.CodeNo AND CodeDatas.BaseNo = 'Status_IemuTrans'
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
            str_query += $"IemuTrans.CodeName LIKE '%{searchText}%' OR ";
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
        return " ORDER BY IemuTrans.No DESC";
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
    // Jacky 1120721
    /// <summary>
    /// 取得新單號
    /// </summary>
    /// <returns></returns>
    public string GetNewNo()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            int count = 0;
            string newNo = "";
            string today =
                string.Format(DateTime.Now.ToString("yyyy")) +
                string.Format(DateTime.Now.ToString("MM")) +
                string.Format(DateTime.Now.ToString("dd"));
            string sql =
                @"SELECT COUNT(*) FROM IemuTrans" + 
                " WHERE SUBSTRING(No, 1, 8) = @today";

            // for testing
            // today = "20230720";

            // 寫法一：
            dp.CommandText = sql;
            // true 為加入參數前，是否將參數全數清空
            dp.ParametersAdd("today", today, true);
            dp.CommandType = System.Data.CommandType.Text;
            count = dp.QueryScalar();

            // 寫法二：
            // DynamicParameters parm = new DynamicParameters();
            // parm.Add("today", today);
            // count = dp.QueryScalar(sql, parm, System.Data.CommandType.Text);

            // count 加一            
            count++;

            // 寫法一：
            newNo = $"{today}{count:000}";

            // 寫法二：
            // newNo = today + count.ToString().PadLeft(3, '0');

            return newNo;
        }
    }

    // Jacky 1120723
    // for TextBoxFor 使用，不然在新增畫面時，帶不出 [狀態碼]、[狀態名稱]
    /// <summary>
    /// 取得狀態名稱
    /// </summary>
    /// <returns></returns>
    public string GetCodeName(string codeNo)
    {
        using (z_repoCodeDatas codeDatas = new z_repoCodeDatas())
        {
            const string baseNo = "Status_IemuTrans";
            var data = codeDatas.repo.ReadSingle(m => m.BaseNo == baseNo && m.CodeNo == codeNo);
            return data.CodeName;
        }
    }

    // Jacky 1120723
    // for TextBoxFor 使用，不然在修改畫面時，帶不出 [客戶簡稱]
    /// <summary>
    /// 取得客戶簡稱
    /// </summary>
    /// <param name="cuNo"></param>
    /// <returns></returns>
    public string GetCustomerSimpleName(string cuNo)
    {
        using (z_repoiecusuh iecusuh = new z_repoiecusuh())
        {
            var data = iecusuh.repo.ReadSingle(m => m.cu_no == cuNo);
            return data.cu_na;
        }
    }

    #endregion
}
