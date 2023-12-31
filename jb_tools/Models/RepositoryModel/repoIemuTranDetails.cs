﻿using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Wordprocessing;
using jb_tools.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class z_repoIemuTranDetails : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<IemuTranDetails> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repoIemuTranDetails()
    {
        repo = new EFGenericRepository<IemuTranDetails>(new dbEntities());
    }

    /// <summary>
    /// 以 Dapper 來讀取單筆 IEMenu 交易檔(表頭) 資料
    /// </summary>
    /// <param name="id">IEMenu 交易檔(表頭) Id</param>
    /// <returns></returns>
    public IemuTranDetails GetDapperData(int id)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += " WHERE IemuTranDetails.Id = @id";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("id", id);
            var model = dp.ReadSingle<IemuTranDetails>(str_query, parm);
            return model;
        }
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<IemuTranDetails> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<IemuTranDetails>(str_query);
            return model;
        }
    }

    /// <summary>
    /// 以 Dapper 來讀取資料集合(以 單號 No 來取)
    /// <summary>
    /// <param iemuTranNo>表頭 IemuTrans 的單號</param>
    /// <returns></returns>
    /// Jacky 1120726 for 分頁模式
    public List<IemuTranDetails> GetDapperDataListByNo(string iemuTranNo)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("No", iemuTranNo);
            var model = dp.ReadAll<IemuTranDetails>(str_query);
            return model;
        }
    }

    /// <summary>
    /// 取得 IemuTranDetails 資料筆數
    /// </summary>
    /// <param name="iemuTranNo"></param>
    /// <returns></returns>
    /// Jacky 1120804
    public int GetDataCount(string iemuTranNo) 
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string sql =
                @"SELECT COUNT(*) FROM IemuTranDetails" +
                " WHERE No = @no";

            dp.CommandText = sql;
            // true 為加入參數前，是否將參數全數清空
            dp.ParametersAdd("no", iemuTranNo, true);
            dp.CommandType = CommandType.Text;

            return dp.QueryScalar();
        }
    }

    /// Jacky 1120730
    /// 注意：這裡另外寫這支程式是為了要搭配 IPagedList 分頁模式，且要顯示額外資料的考量，而不用 GetDapperDataList()
    ///       若像標準資料檔(大中細標準資料檔)，因為不需要使用 [NotMapped] 顯示額外資料，就只要用 IPagedList 即可。
    /// 為搭配 IPagedList 分頁模式所做的資料讀取，就不會一次把資料全部讀進來，造成嚴重 Lag
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param iemuTranNo>表頭 IemuTrans 單號</param>
    /// <param page>頁號(1-Based)</param>
    /// <param pageSize>每頁筆數</param>
    /// <param startIndex>起始 Index(該頁起始索引)</param>
    /// <param endIndex>結束 Index(該頁結束索引)</param>
    /// <returns></returns>
    public List<IemuTranDetails> GetDapperDataListRange(string iemuTranNo, int page, int pageSize, int startIndex, int endIndex)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            int skipCount = (page - 1) * pageSize;
            int takeCount = endIndex - startIndex + 1;
            string str_query = GetSQLSelect();
            str_query += " WHERE IemuTranDetails.No = '" + iemuTranNo + "'";
            str_query += " ORDER BY IemuTranDetails.Seq";
            str_query += " OFFSET " + skipCount + " ROWS";
            str_query += " FETCH NEXT " + takeCount + " ROWS ONLY";
            var model = dp.ReadAll<IemuTranDetails>(str_query).ToList();
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
	IemuTranDetails.Id, IemuTranDetails.No, IemuTranDetails.Seq, 
	IemuTranDetails.MainCode, ISNULL(IemuMainMenus.McId, '') AS 'McId', ISNULL(IemuMainMenus.Name, '') AS 'MName', 
	IemuTranDetails.ScId, ISNULL(IemuSubMenus.Name, '') AS 'SName', 
	IemuTranDetails.DetailOrder, IemuTranDetails.Program, IemuTranDetails.Name, IemuTranDetails.Ename, IemuTranDetails.ProgramPath, 
	IemuTranDetails.Remark
FROM IemuTranDetails 
LEFT JOIN IemuMainMenus ON IemuTranDetails.MainCode = IemuMainMenus.MainCode
LEFT JOIN IemuSubMenus ON IemuTranDetails.MainCode = IemuSubMenus.MainCode AND IemuTranDetails.Scid = IemuSubMenus.ScId 
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
            str_query += $"IemuTranDetails.No LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.Seq LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.MainCode LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.ScId LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.DetailOrder LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.Program LIKE '%{searchText}%' OR ";
            str_query += $"IemuMainMenus.Name LIKE '%{searchText}%' OR ";
            str_query += $"IemuSubMenus.Name LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.Name LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.Ename LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.ProgramPath LIKE '%{searchText}%' OR ";
            str_query += $"IemuDetailMenus.PosOrPath2 LIKE '%{searchText}%' OR ";
            str_query += $"IemuTranDetails.Remark LIKE '%{searchText}%' ";
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
        return " ORDER BY IemuTranDetails.Seq";
    }

    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(IemuTranDetails model)
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
    //Jacky 1120724
    /// <summary>
    /// 以 Dapper 來讀取多筆 IEMenu 交易檔(表身) 資料
    /// </summary>
    /// <param name="iemuTransNo"></param>
    /// <returns></returns>
    public List<IemuTranDetails> GetDapperDataTranDetailsList(string iemuTransNo)
    {
        return GetDapperDataListByNo(iemuTransNo);
    }

    /// <summary>
    /// Jacky 1120727 帶入標準資料(大中細分類全部資料)
    /// </summary>
    /// <param name="no"></param>
    public string BringStandardValues(string no)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            dp.CommandType = System.Data.CommandType.StoredProcedure;
            dp.CommandText = "dbo.sp_IemuTranDetail_BringStandardValues";
            dp.ParametersAdd("no", no, true);
            dp.Execute();
            string errorMessage = "";
            if (!string.IsNullOrEmpty(dp.ErrorMessage))
            {
                errorMessage = dp.ErrorMessage.Trim();
            }
            return errorMessage;
        }
    }
    #endregion
}
