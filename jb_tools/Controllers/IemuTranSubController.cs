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

namespace jb_tools.Controllers
{
    public class IemuTranSubController : Controller
    {
        private const bool V = true;

        // for DropDownList ↓ -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Jacky 1120731

        /// <summary>
        /// 準備下拉選單的資料
        /// </summary>
        /// <param name="id"></param>
        //public EmptyResult PrepareDropDownList()
        private void PrepareDropDownList(int id = 0)
        {
            // 第一層下拉選單資料結構
            List<vmDDLMainCodeModel> firstLevelItems;

            // 第二層下拉選單資料結構
            List<vmDDLScIdModel> secondLevelItems;

            // 第三層下拉選單資料結構
            List<vmDDLDetailOrderModel> thirdLevelItems;

            // 第一層被選定的大分類代號
            string selectedMainCode = string.Empty;

            // 第二層被選定的中分類識別
            string selectedScId = string.Empty;

            // 第三層被選定的細分類序號
            string selectedDetailOrder = string.Empty;

            // 建立第一層原始資料來源
            using (z_repoIemuMainMenus iemuMainMenus = new z_repoIemuMainMenus())
            {
                // 第一層下拉選單資料結構
                firstLevelItems = new List<vmDDLMainCodeModel>();

                // 建立第一層 ViewModel 資料，以 IemuMainMenus 資料表的資料，來建立第一層 ViewModel 資料，作為下拉選單呈現的資料
                foreach (var row in iemuMainMenus.GetDapperDataList(""))
                {
                    firstLevelItems.Add(
                        new vmDDLMainCodeModel()
                        {
                            Id = row.Id,
                            MainCode = row.MainCode,
                            McId = row.McId,
                            Name = row.Name
                        }
                    );
                }
            }

            // 第二層原始資料來源
            using (z_repoIemuSubMenus iemuSubMenus = new z_repoIemuSubMenus())
            {
                // 第二層下拉選單資料結構
                secondLevelItems = new List<vmDDLScIdModel>();

                // 建立第二層 ViewModel 資料，以 IemuSubMenus 資料表的資料，來建立第二層 ViewModel 資料，作為下拉選單呈現的資料
                foreach (var row in iemuSubMenus.GetDapperDataList(""))
                {
                    secondLevelItems.Add(
                        new vmDDLScIdModel()
                        {
                            Id = row.Id,
                            ScId = row.ScId,
                            SubCode = row.SubCode,
                            Name = row.Name,
                            MainCode = row.MainCode
                        }
                    );
                }
            }

            // 第三層原始資料來源
            using (z_repoIemuDetailMenus iemuDetailMenus = new z_repoIemuDetailMenus())
            {
                // 第三層下拉選單資料結構
                thirdLevelItems = new List<vmDDLDetailOrderModel>();

                // 建立第三層 ViewModel 資料，以 IemuDetailMenus 資料表的資料，來建立第三層 ViewModel 資料，作為下拉選單呈現的資料
                foreach (var row in iemuDetailMenus.GetDapperDataList(""))
                {
                    thirdLevelItems.Add(
                        new vmDDLDetailOrderModel()
                        {
                            Id = row.Id,
                            DetailOrder = row.DetailOrder,
                            Program = row.Program,
                            Name = row.Name,
                            ScId = row.ScId,
                            MainCode = row.MainCode
                        }
                    );
                }
            }

            if (id == 0)
            {
                // 1. 找出第一筆當作預設的 MainCode
                selectedMainCode = (
                    from f in firstLevelItems
                    orderby f.MainCode
                    select f.MainCode
                    ).FirstOrDefault();
            }
            else
            {
                // 1. 找出 Id == id 當作預設的 MainCode
                selectedMainCode = (
                    from f in firstLevelItems
                    where f.Id == id
                    select f.MainCode
                    ).FirstOrDefault();
            }

            // 2. 用 LINQ 撈出第一層資料，用以建立 SelectListItem 實例
            var items =
                (
                    (
                        from f in firstLevelItems
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
                        from f in firstLevelItems
                        where f.MainCode != selectedMainCode
                        orderby f.MainCode
                        select new SelectListItem
                        {
                            Text = f.MainCode + SelectListService.Delimiter + f.McId + SelectListService.Delimiter + f.Name,
                            Value = f.MainCode,
                            Selected = false
                        }
                    )
                ).ToList();

            // 2. 加入「請選擇」
            //items.Insert(0, new SelectListItem { Text = "請選擇", Value = "-1" });

            // 2. 第一層資料建立的下拉選單資料，存入 ViewData
            ViewData["FirstLevelItems"] = items;

            if (id == 0)
            {
                // 3. 以 MainCode == selectedMainCode 為條件，找出第一筆的 ScId 
                selectedScId = (
                    from f in secondLevelItems
                    where f.MainCode == selectedMainCode
                    orderby f.ScId
                    select f.ScId
                    ).FirstOrDefault();
            }
            else
            {
                // 3. 找出 Id == id 當作預設的 MainCode
                selectedMainCode = (
                    from f in firstLevelItems
                    where f.Id == id
                    select f.MainCode
                    ).FirstOrDefault();
            }

            // 4. 用 LINQ 撈出第二層資料，用以建立 SelectListItem 實例
            items =
            (
                (
                    from s in secondLevelItems
                    where s.ScId == selectedScId
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId,
                        Selected = true
                    }
                )
                .Union
                (
                    from s in secondLevelItems
                    where s.ScId != selectedScId
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId,
                        Selected = false
                    }
                )
            ).ToList();

        (
                from s in secondLevelItems
                    select new SelectListItem
                    {
                        Text = s.ScId + SelectListService.Delimiter + s.SubCode + SelectListService.Delimiter + s.Name,
                        Value = s.ScId
                    }
                ).ToList();

            // 2. 加入「請選擇」
            items.Insert(0, new SelectListItem { Text = "請選擇", Value = "-1" });

            // 3. 第二層資料建立的下拉選單資料，存入 ViewData
            ViewData["SecondLevelItems"] = items;

            // 1. 用 LINQ 撈出第一層資料，用以建立 SelectListItem 實例
            //var items = (
            //    from f in firstLevelItems
            //    select new SelectListItem 
            //    { 
            //        Text = f.MainCode + SelectListService.Delimiter + f.McId + SelectListService.Delimiter + f.Name, 
            //        Value = f.MainCode 
            //    }
            //).ToList();

            //// 2. 加入「請選擇」
            //items.Insert(0, new SelectListItem { Text = "請選擇", Value = "-1" });

            //// 3. 第一層資料建立的下拉選單資料，存入 ViewData
            //ViewData["FirstLevelItems"] = items;

            //return new EmptyResult();
        }

        /// <summary>
        /// 顯示第二層 DrowDownList
        /// </summary>
        /// <param name="mainCode"></param>
        /// mainCode: 所隸屬的大分類代號
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowSecondDropDownList(string mainCode)
        {
            // 1. 用 LINQ 撈出第二層資料，用以建立 SelectListItem 實例
            var items = (
                from s in secondLevelItems
                select new SelectListItem
                {
                    Text = s.SubCode + SelectListService.Delimiter + s.ScId + SelectListService.Delimiter + s.Name,
                    Value = s.ScId
                }
            ).ToList();

            // 2. 加入「請選擇」
            items.Insert(0, new SelectListItem { Text = "請選擇", Value = "-1" });

            // 3. 第二層資料建立的下拉選單資料，存入 ViewData
            ViewData["SecondLevelItems"] = items;

            return View();
        }
        // for DropDownList ↑ -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

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

                    Session["CurrentController"] = "IemuTranSub";

                    // Jacky 1120725 設定表格樣式
                    SetTableShowStyle();

                    // Jacky 1120730 建立 List<T> 與 IPagedList<T> 的結合 Model
                    vmIemuTranSubModel vmModel = new vmIemuTranSubModel();

                    // Jacky 1120730 表頭 IemuTrans 的 Model
                    // 注意：這個 Model 一定要先做，因為需要這個表頭的單號來取表身資料
                    vmModel.IemuTransModel = iemuTrans.GetDapperData(id);

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
                PrepareDropDownList();

                SessionService.KeyValue = id;
                enAction action = (id == 0) ? enAction.Create : enAction.Edit;
                PrgService.SetAction(action, enCardSize.Small);
                ViewBag.Action = action;
                ViewBag.CardSize = PrgService.CardSize;
                var model = iemuTranDetails.repo.ReadSingle(m => m.Id == id);
                if (model == null)
                {
                    model = new IemuTranDetails();
                    // 設定新增預設值
                    using (AttributeService attr = new AttributeService())
                    {
                        //model.No = iemuTrans.GetNewNo();
                        //model.Date = DateTime.Today.Date;

                        //// Jacky 1120723
                        //// for TextBoxFor 使用，不然在新增畫面時，帶不出 [狀態碼]、[狀態名稱]
                        //model.Status = "E";     // 編輯中
                        //model.CodeName = iemuTrans.GetCodeName(model.Status);

                        //model.CuNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuNo");
                        //model.CuSale = (string)attr.GetDefaultValue<z_repoIemuTrans>("CuSale");
                        //model.IndustryNo = (string)attr.GetDefaultValue<z_repoIemuTrans>("IndustryNo");
                        //model.Remark = (string)attr.GetDefaultValue<z_repoIemuTrans>("Remark");
                    }
                }
                else
                {
                    // Jacky 1120723
                    // for TextBoxFor 使用，不然在修改畫面時，帶不出 [客戶簡稱]
                    //model.cu_na = iemuTrans.GetCustomerSimpleName(model.CuNo);
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /// Jacky 1120727
        /// <summary>
        /// 帶入標準資料
        /// no (單號)
        /// </summary>
        public ActionResult BringStandardValues(string no)
        {
            using (z_repoIemuTranDetails iemuTranDetails = new z_repoIemuTranDetails())
            {
                Session["CurrentController"] = "IemuTranSub";

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
    }
}