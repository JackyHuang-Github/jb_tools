using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jb_tools.Models.ViewModel
{
    /// <summary>
    /// Jacky 1120730
    /// List<T> 與 IPagedList<T> 的結合 Model: vmIemuTranModel (注意這裡的 ViewMode 命名是跟著 Controller，也就是單數)
    /// vmIemuTranModel 特別為分頁模式打造的 ViewModel，因為 IPagedList 無法呈現 [NotMapped] 的額外顯示部分，因此必須加上原來的 List 才行
    /// </summary>
    public class vmIemuTranModel
    {
        /// <summary>
        /// Jacky 1120730
        /// 專為分頁模式所打造的 Model
        /// </summary>
        public IPagedList<IemuTrans> IemuTransPLModel { get; set; }

        /// <summary>
        /// Jacky 1120730
        /// IemuTrans 資料呈現的 Model (多筆)
        /// </summary>
        public List<IemuTrans> IemuTransModel { get; set; }
    }
}