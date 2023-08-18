using DocumentFormat.OpenXml.Wordprocessing;
using jb_tools.Models;
using jb_tools.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using PagedList;
using System.Drawing.Printing;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Ajax.Utilities;

namespace jb_tools.Controllers
{
    public class IemuTranDetailController : Controller
    {
        // for DropDownList ↓ -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Jacky 1120731

        // 第一層下拉選單原始資料結構
        List<vmDDLMainCodeModel> firstLevelRawItems = new List<vmDDLMainCodeModel>();
        public List<vmDDLMainCodeModel> FirstLevelRawItems { get => firstLevelRawItems; set => firstLevelRawItems = value; }
        
        // 第二層下拉選單原始資料結構
        List<vmDDLScIdModel> secondLevelRawItems = new List<vmDDLScIdModel>();
        public List<vmDDLScIdModel> SecondLevelRawItems { get => secondLevelRawItems; set => secondLevelRawItems = value; }

        // 第三層下拉選單原始資料結構
        List<vmDDLDetailOrderModel> thirdLevelRawItems = new List<vmDDLDetailOrderModel>();
        public List<vmDDLDetailOrderModel> ThirdLevelRawItems { get => thirdLevelRawItems; set => thirdLevelRawItems = value; }

        /// <summary>
        /// 準備下拉選單的資料
        /// </summary>
        /// <param name="id"></param>
        private void PrepareDropDownList(int id = 0)
        {
            // 第一層被選定的大分類代號
            string selectedMainCode = string.Empty;

            // 第二層被選定的中分類識別
            string selectedScId = string.Empty;

            // 第三層被選定的細分類序號
            string selectedDetailOrder = string.Empty;

            // -------------------------------------------------- ↓ 建立原始資料來源 ↓ --------------------------------------------------
            // 建立第一層原始資料來源
            using (z_repoIemuMainMenus iemuMainMenus = new z_repoIemuMainMenus())
            {
                // 建立第一層 ViewModel 資料，以 IemuMainMenus 資料表的資料，來建立第一層 ViewModel 資料，作為下拉選單呈現的資料
                foreach (var row in iemuMainMenus.GetDapperDataList(""))
                {
                    FirstLevelRawItems.Add(
                        new vmDDLMainCodeModel()
                        {
                            MainCode = row.MainCode,
                            McId = row.McId,
                            Name = row.Name,
                            SortNum = row.SortNum
                        }
                    );
                }

                // Jacky 1120806 儲存第一層原始資料來源
                Session["FirstLevelRawItems"] = FirstLevelRawItems;
            }

            // 第二層原始資料來源
            using (z_repoIemuSubMenus iemuSubMenus = new z_repoIemuSubMenus())
            {
                // 建立第二層 ViewModel 資料，以 IemuSubMenus 資料表的資料，來建立第二層 ViewModel 資料，作為下拉選單呈現的資料
                foreach (var row in iemuSubMenus.GetDapperDataList(""))
                {
                    SecondLevelRawItems.Add(
                        new vmDDLScIdModel()
                        {
                            SubCode = row.SubCode,
                            ScId = row.ScId,
                            Name = row.Name,
                            MainCode = row.MainCode,
                            SortNum = row.SortNum
                        }
                    );
                }

                // Jacky 1120806 儲存第二層原始資料來源
                Session["SecondLevelRawItems"] = SecondLevelRawItems;
            }

            // 第三層原始資料來源
            using (z_repoIemuDetailMenus iemuDetailMenus = new z_repoIemuDetailMenus())
            {
                // 建立第三層 ViewModel 資料，以 IemuDetailMenus 資料表的資料，來建立第三層 ViewModel 資料，作為下拉選單呈現的資料
                foreach (var row in iemuDetailMenus.GetDapperDataList(""))
                {
                    ThirdLevelRawItems.Add(
                        new vmDDLDetailOrderModel()
                        {
                            DetailOrder = row.DetailOrder,
                            Program = row.Program,
                            Name = row.Name,
                            ProgramPath = row.ProgramPath,
                            ScId = row.ScId,
                            MainCode = row.MainCode,
                            SortNum = row.SortNum
                        }
                    );
                }

                // Jacky 1120806 儲存第三層原始資料來源
                Session["ThirdLevelRawItems"] = ThirdLevelRawItems;
            }
            // -------------------------------------------------- ↑ 建立原始資料來源 ↑ --------------------------------------------------

            // -------------------------------------------------- ↓ 大分類 ↓ --------------------------------------------------
            if (id == 0)
            {
                // 1. 找出第一筆當作預設的 MainCode
                selectedMainCode = (
                    from f in FirstLevelRawItems
                    orderby f.SortNum
                    select f.MainCode
                    ).FirstOrDefault();
            }
            else
            {
                // 1. 找出表身 IemuTranDetails 該筆資料的 Id 之 MainCode、ScId、DetailOrder
                using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    var data = iemuTranDetails.GetDapperData(id);
                    selectedMainCode = data.MainCode;
                    selectedScId = data.ScId;
                    selectedDetailOrder = data.DetailOrder;
                }
            }

            // 2. 用 LINQ 撈出第一層原始資料來源，用以建立 SelectListItem 實例
            var items =
                (
                    (
                        // 設定 [預設選取] 的下拉選單內容 
                        from f in FirstLevelRawItems
                        where f.MainCode == selectedMainCode
                        select new SelectListItem
                        {
                            Text = f.MainCode + SelectListService.Delimiter + f.McId + SelectListService.Delimiter + f.Name,
                            Value = f.MainCode,
                            Selected = true
                        }
                    )
                    .Union
                    (
                        // 設定 [非預設選取] 的下拉選單內容 
                        from f in FirstLevelRawItems
                        where f.MainCode != selectedMainCode
                        orderby f.SortNum
                        select new SelectListItem
                        {
                            Text = f.MainCode + SelectListService.Delimiter + f.McId + SelectListService.Delimiter + f.Name,
                            Value = f.MainCode,
                            Selected = false
                        }
                    )
                ).ToList();

            // 3. 第一層原始資料來源建立的下拉選單資料，存入 ViewData
            Session["FirstLevelItems"] = items;
            // -------------------------------------------------- ↑ 大分類 ↑ --------------------------------------------------

            // -------------------------------------------------- ↓ 中分類 ↓ --------------------------------------------------
            if (id == 0)
            {
                // 1. 以 MainCode == selectedMainCode 為條件，找出第一筆的 ScId 
                selectedScId = (
                    from s in SecondLevelRawItems
                    where s.MainCode == selectedMainCode
                    orderby s.SortNum
                    select s.ScId
                    ).FirstOrDefault();
            }
            else
            {
                // 1. 已於第一層取得 selectedScid
            }

            // 2. 用 LINQ 撈出第二層原始資料來源，用以建立 SelectListItem 實例
            items =
            (
                (
                    // 設定 [預設選取] 的下拉選單內容 
                    from s in SecondLevelRawItems
                    where s.ScId == selectedScId && s.MainCode == selectedMainCode
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId,
                        Selected = true
                    }
                )
                .Union
                (
                    // 設定 [非預設選取] 的下拉選單內容 
                    from s in SecondLevelRawItems
                    where s.ScId != selectedScId && s.MainCode == selectedMainCode
                    orderby s.SortNum
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId,
                        Selected = false
                    }
                )
            ).ToList();

            // 3.a. 第二層原始資料來源建立的下拉選單資料，存入 ViewData
            ViewData["SecondLevelItems"] = items;
            // 3.b. 記錄預設的 ScId
            ViewBag.SelectedScId = selectedScId;
            // -------------------------------------------------- ↑ 中分類 ↑ --------------------------------------------------

            // -------------------------------------------------- ↓ 細分類 ↓ --------------------------------------------------
            if (id == 0)
            {
                // 1. 以 MainCode == selectedMainCode + ScId == selectedScId 為條件，找出第一筆的 DetailOrder
                selectedDetailOrder = (
                    from t in ThirdLevelRawItems
                    where t.ScId == selectedScId && t.MainCode == selectedMainCode
                    orderby t.SortNum
                    select t.DetailOrder
                    ).FirstOrDefault();
            }
            else
            {
                // 1. 已於第一層取得 selectedDetailOrder
            }

            // 2. 用 LINQ 撈出第三層原始資料來源，用以建立 SelectListItem 實例
            items =
            (
                (
                    // 設定 [預設選取] 的下拉選單內容 
                    from t in ThirdLevelRawItems
                    where t.DetailOrder == selectedDetailOrder && t.ScId == selectedScId && t.MainCode == selectedMainCode
                    select new SelectListItem
                    {
                        Text = t.DetailOrder + SelectListService.Delimiter + t.Program + SelectListService.Delimiter + t.Name + SelectListService.Delimiter + t.ProgramPath,
                        Value = t.DetailOrder,
                        Selected = true
                    }
                )
                .Union
                (
                    // 設定 [非預設選取] 的下拉選單內容 
                    from t in ThirdLevelRawItems
                    where t.DetailOrder != selectedDetailOrder && t.ScId == selectedScId && t.MainCode == selectedMainCode
                    orderby t.SortNum
                    select new SelectListItem
                    {
                        Text = t.DetailOrder + SelectListService.Delimiter + t.Program + SelectListService.Delimiter + t.Name + SelectListService.Delimiter + t.ProgramPath,
                        Value = t.DetailOrder,
                        Selected = false
                    }
                )
            ).ToList();

            // 3.a. 第三層原始資料來源建立的下拉選單資料，存入 ViewData
            ViewData["ThirdLevelItems"] = items;
            // 3.b. 記錄預設的 DetailOrder
            ViewBag.SelectedDetailOrder = selectedDetailOrder;
            // -------------------------------------------------- ↑ 細分類 ↑ --------------------------------------------------
        }

        /// <summary>
        /// 設定第二層 DrowDownList
        /// </summary>
        /// <param name ="mainCode"></ param >
        /// <param name="scId"></param>
        /// mainCode: 所隸屬的大分類代號
        /// scId: 所隸屬的中分類識別
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetSecondDropDownList(string mainCode, string scId)
        {
            // Jacky 1120806 取得第二層原始資料來源
            List<vmDDLScIdModel> SecondLevelRawItems = (List<vmDDLScIdModel>)Session["SecondLevelRawItems"];

            // 1. 指定被選擇的中分類識別，即為所傳進來的 scId
            string selectedScId = scId;

            // 2. 用 LINQ 撈出第二層原始資料來源，用以建立 SelectListItem 實例
            List<SelectListItem> items = 
            (
                (
                    // 設定 [預設選取] 的下拉選單內容 
                    from s in SecondLevelRawItems
                    where s.ScId == selectedScId && s.MainCode == mainCode
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId,
                        Selected = true
                    }
                )
                .Union
                (
                    // 設定 [非預設選取] 的下拉選單內容 
                    from s in SecondLevelRawItems
                    where s.ScId != selectedScId && s.MainCode == mainCode
                    orderby s.SortNum
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId,
                        Selected = false
                    }
                )
            ).ToList();

            // 3.a. 第二層原始資料來源建立的下拉選單資料，存入 ViewData
            ViewData["SecondLevelItems"] = items;
            // 3.b. 記錄被選擇的 ScId
            ViewBag.SelectedScId = selectedScId;

            // Jacky 1120807 以 PartialView 的形式傳回資料
            return PartialView("~/Views/PartialViews/_PartialSetSecondDropDownList.cshtml");
        }
        /// <summary>
        /// 設定第三層 DrowDownList
        /// </summary>
        /// <param name="mainCode"></param>
        /// <param name="scId"></param>
        /// <param name="detailOrder"></param>
        /// mainCode: 所隸屬的大分類代號
        /// scId: 所隸屬的中分類識別
        /// detailOrder: 所隸屬的細分類序號
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetThirdDropDownList(string mainCode, string scId, string detailOrder)
        {
            // Jacky 1120806 取得第三層原始資料來源
            List<vmDDLDetailOrderModel> ThirdLevelRawItems = (List<vmDDLDetailOrderModel>)Session["ThirdLevelRawItems"];

            // 1. 指定被選擇的細分類序號，即為所傳進來的 detailOrder
            string selectedDetailOrder = detailOrder;

            // 2. 用 LINQ 撈出第三層原始資料來源，用以建立 SelectListItem 實例
            List<SelectListItem> items =
            (
                (
                    // 設定 [預設選取] 的下拉選單內容 
                    from t in ThirdLevelRawItems
                    where t.DetailOrder == selectedDetailOrder && t.ScId == scId && t.MainCode == mainCode
                    select new SelectListItem
                    {
                        Text = t.DetailOrder + SelectListService.Delimiter + t.Program + SelectListService.Delimiter + t.Name + SelectListService.Delimiter + t.ProgramPath,
                        Value = t.DetailOrder,
                        Selected = true
                    }
                )
                .Union
                (
                    // 設定 [非預設選取] 的下拉選單內容 
                    from t in ThirdLevelRawItems
                    where t.DetailOrder != selectedDetailOrder && t.ScId == scId && t.MainCode == mainCode
                    orderby t.SortNum
                    select new SelectListItem
                    {
                        Text = t.DetailOrder + SelectListService.Delimiter + t.Program + SelectListService.Delimiter + t.Name + SelectListService.Delimiter + t.ProgramPath,
                        Value = t.DetailOrder,
                        Selected = false
                    }
                )
            ).ToList();

            // 3.a. 第三層原始資料來源建立的下拉選單資料，存入 ViewData
            ViewData["ThirdLevelItems"] = items;
            // 3.b. 記錄預設的 DetailOrder
            ViewBag.SelectedDetailOrder = selectedDetailOrder;

            // Jacky 1120807 以 PartialView 的形式傳回資料
            return PartialView("~/Views/PartialViews/_PartialSetThirdDropDownList.cshtml");
        }
        // for DropDownList ↑ -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 以 Select Action 呈現交易單表頭及表身資料
        /// 注意：這裡的 id 是交易單表頭(IemuTrans)的 Id，因為要配合 Select/id，避免與 CreateEdit/id 和 Index/id 衝突，特別設計 Select Action 以資區別
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Select(int id = 0, int page = 1, int pageSize = PaginationService.CountPerPage, string searchText = "")
        {
            // Jacky 1120731 抓取 Web.config 裡的設定值
            pageSize = PaginationService.GetCountPerPage();

            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    PrgService.SearchText = "";
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                    Session["CurrentController"] = "IemuTranDetail";

                    // Jacky 1120725 設定表格樣式
                    SetTableShowStyle();

                    // Jacky 1120730 建立 List<T> 與 IPagedList<T> 的結合 Model
                    vmIemuTranDetailModel vmModel = new vmIemuTranDetailModel();

                    // Jacky 1120730 表頭 IemuTrans 的 Model
                    // 注意：這個 Model 一定要先做，因為需要這個表頭的單號來取表身資料
                    vmModel.IemuTransModel = iemuTrans.GetDapperData(id);
                    // Jacky 1120805 預先記錄表頭的單號
                    Session["IemuTranDetail_IemuTrans_No"] = vmModel.IemuTransModel.No;

                    // Jacky 1120726 for 分頁模式
                    // Jacky 1120730 IPagedList<T> 的 Model
                    IPagedList<IemuTranDetails> iemuTranDetailsPLModel = iemuTranDetails.repo.ReadAll()
                        .Where(m => m.No == vmModel.IemuTransModel.No)
                        .OrderBy(m => m.Seq)
                        .ToPagedList(page, pageSize);
                    vmModel.IemuTranDetailsPLModel = iemuTranDetailsPLModel;
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, vmModel.IemuTranDetailsPLModel.PageNumber, vmModel.IemuTranDetailsPLModel.PageCount);

                    // Jacky 1120730 List<T> 的 Model
                    // 注意：這裡另外寫這支程式是為了要搭配 IPagedList 分頁模式，且要顯示額外資料的考量，而不用 GetDapperDataList()
                    //       若像標準資料檔(大中細標準資料檔)，因為不需要使用 [NotMapped] 顯示額外資料，就只要用 IPagedList 即可。
                    // 為搭配 IPagedList 分頁模式所做的資料讀取，就不會一次把資料全部讀進來，造成嚴重 Lag
                    int startIndex = iemuTranDetailsPLModel.FirstItemOnPage - 1;
                    int endIndex = iemuTranDetailsPLModel.LastItemOnPage - 1;
                    List<IemuTranDetails> iemuTranDetailsModel = iemuTranDetails.GetDapperDataListRange(vmModel.IemuTransModel.No, page, pageSize, startIndex, endIndex);
                    vmModel.IemuTranDetailsModel = iemuTranDetailsModel;

                    return View(vmModel);
                }
            }
        }

        /// <summary>
        /// 以 Index Action 呈現呈現交易單表頭及表身資料
        /// 注意：這裡的 id 是交易單表身(IemuTranDetails)的 Id，因為要配合 CreateEdit/id 與 Index/id，這裡的 id 是以 IemuTranDetails 為主角做 CRUD
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int id = 0, int page = 1, int pageSize = PaginationService.CountPerPage, string searchText = "")
        {
            // Jacky 1120731 抓取 Web.config 裡的設定值
            pageSize = PaginationService.GetCountPerPage();

            using (z_repoIemuTrans iemuTrans = new z_repoIemuTrans())
            {
                using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
                {
                    PrgService.SearchText = "";
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, 1, 1);

                    Session["CurrentController"] = "IemuTranDetail";

                    // Jacky 1120725 設定表格樣式
                    SetTableShowStyle();

                    // Jacky 1120730 建立 List<T> 與 IPagedList<T> 的結合 Model
                    vmIemuTranDetailModel vmModel = new vmIemuTranDetailModel();

                    // Jacky 1120730 表頭 IemuTrans 的 Model
                    // 注意：這個 Model 一定要先做，因為需要這個表頭的單號來取表身資料
                    // Jacky 1120805 必須以原先表頭的單號為單號
                    string no = Session["IemuTranDetail_IemuTrans_No"].ToString();
                    vmModel.IemuTransModel = iemuTrans.GetDapperDataByNo(no);

                    // Jacky 1120726 for 分頁模式
                    // Jacky 1120730 IPagedList<T> 的 Model
                    IPagedList<IemuTranDetails> iemuTranDetailsPLModel = iemuTranDetails.repo.ReadAll()
                        .Where(m => m.No == vmModel.IemuTransModel.No)
                        .OrderBy(m => m.Seq)
                        .ToPagedList(page, pageSize);
                    vmModel.IemuTranDetailsPLModel = iemuTranDetailsPLModel;
                    PrgService.SetAction(ActionService.IndexName, enCardSize.Max, vmModel.IemuTranDetailsPLModel.PageNumber, vmModel.IemuTranDetailsPLModel.PageCount);

                    // Jacky 1120730 List<T> 的 Model
                    // 注意：這裡另外寫這支程式是為了要搭配 IPagedList 分頁模式，且要顯示額外資料的考量，而不用 GetDapperDataList()
                    //       若像標準資料檔(大中細標準資料檔)，因為不需要使用 [NotMapped] 顯示額外資料，就只要用 IPagedList 即可。
                    // 為搭配 IPagedList 分頁模式所做的資料讀取，就不會一次把資料全部讀進來，造成嚴重 Lag
                    int startIndex = iemuTranDetailsPLModel.FirstItemOnPage - 1;
                    int endIndex = iemuTranDetailsPLModel.LastItemOnPage - 1;
                    List<IemuTranDetails> iemuTranDetailsModel = iemuTranDetails.GetDapperDataListRange(vmModel.IemuTransModel.No, page, pageSize, startIndex, endIndex);
                    vmModel.IemuTranDetailsModel = iemuTranDetailsModel;

                    return View(vmModel);
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateEdit(int id = 0)
        {
            using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
            {
                // 準備下拉選單的相關資訊
                PrepareDropDownList();

                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Small);
                ViewBag.Action = action;
                ViewBag.CardSize = PrgService.CardSize;

                vmIemuTranDetailCreateEdit vmModel = new vmIemuTranDetailCreateEdit();
                vmModel.IemuTranDetailsModel = iemuTranDetails.repo.ReadSingle(m => m.Id == id);
                if (vmModel.IemuTranDetailsModel == null)
                {
                    vmModel.IemuTranDetailsModel = new IemuTranDetails();
                    //// 設定新增預設值
                    //using (AttributeService attr = new AttributeService())
                    //{
                    //    model.No = iemuTrans.GetNewNo();
                    //    model.Date = DateTime.Today.Date;

                    //    // Jacky 1120723
                    //    // for TextBoxFor 使用，不然在新增畫面時，帶不出 [狀態碼]、[狀態名稱]
                    //    model.Status = "E";     // 編輯中
                    //    model.CodeName = iemuTrans.GetCodeName(model.Status);

                    //    model.CuNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuNo");
                    //    model.CuSale = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuSale");
                    //    model.IndustryNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("IndustryNo");
                    //    model.Remark = (string)attr.GetDefaultValue<z_repoIemuTrans>("Remark");
                    //}
                }
                else
                {
                    // Jacky 1120723
                    // for TextBoxFor 使用，不然在修改畫面時，帶不出 [客戶簡稱]
                    //model.cu_na = iemuTrans.GetCustomerSimpleName(model.CuNo);
                }
                return View(vmModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(vmIemuTranDetailCreateEdit vmModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = ActionService.SetErrorMessage<z_metaIemuTranDetails>(ModelState);
                return View(vmModel);
            }

            using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
            {
                enAction action = (vmModel.IemuTranDetailsModel.Id == 0) ? enAction.Create : enAction.Edit;
                ViewBag.Action = action;
                iemuTranDetails.CreateEdit(vmModel.IemuTranDetailsModel);
                return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = "" });
            }
        }

        [HttpPost]
        /// Jacky 1120727
        /// <summary>
        /// 帶入標準資料(IemuMainMenus、IemuSubMenus、IemuDetailMenus)到表身資料表 IemuTranDetails
        /// no (單號)
        /// </summary>
        public ActionResult BringStandardValues(string no)
        {
            using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
            {
                Session["CurrentController"] = "IemuTranDetail";

                // Jacky 1120727 帶入標準資料
                string errorMessage = iemuTranDetails.BringStandardValues(no);
                dmJsonMessage result = new dmJsonMessage()
                {
                    // errorMessage 若為空則 Mode = true，否則為 false
                    Mode = string.IsNullOrEmpty(errorMessage),
                    Message = string.IsNullOrEmpty(errorMessage) ? "已帶入標準資料至表身！" : errorMessage
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 設定表格樣式
        /// </summary>
        private void SetTableShowStyle()
        {
            string multipleTablesNormalHead = "";
            string multipleTablesFixedHead = "";

            if (Session["TableShowStyle"] == null)
            {
                Session["TableShowStyle"] = "tableFixedHeadHover";
            }
            string tableShowStyle = Session["TableShowStyle"].ToString();
            if (tableShowStyle == "")
            {
                tableShowStyle = "tableFixedHeadHover";
            }

            if (tableShowStyle == "tableFixedHeadHover")
            {
                multipleTablesNormalHead = "tableMultipleNormalHead";
                multipleTablesFixedHead = "tableMultipleFixedHead";
            }
            if (tableShowStyle == "tableFixedHeadBorderShadow")
            {
                multipleTablesNormalHead = "tableMultipleNormalHeadBorderShadow";
                multipleTablesFixedHead = "tableMultipleFixedHeadBorderShadow";
            }

            Session["MultipleTablesShowStyleNormalHead"] = multipleTablesNormalHead;
            Session["MultipleTablesShowStyleFixedHead"] = multipleTablesFixedHead;

            ViewBag.MultipleTablesShowStyleNormalHead = multipleTablesNormalHead;
            ViewBag.MultipleTablesShowStyleFixedHead = multipleTablesFixedHead;
        }

        [HttpGet]
        [AllowAnonymous]
        /// Jacky 1120818
        /// <summary>
        /// 動態編輯
        /// </summary>
        /// <returns></returns>
        public ActionResult DynamicEdit()
        {
            using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
            {
                Session["CurrentController"] = "IemuTranDetail";

                var model = iemuTranDetails.repo.ReadAll();
                return View(model);
            }
        }
    }
}