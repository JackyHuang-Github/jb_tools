using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jb_tools.Models.ViewModel
{
    /// <summary>
    /// for DropDownList use only
    /// </summary>
    public class vmDDLScIdModel
    {
        /// <summary>
        /// 中分類代號
        /// </summary>
        public string SubCode { get; set; }
        /// <summary>
        /// 中分類識別
        /// </summary>
        public string ScId { get; set; }
        /// <summary>
        /// 中分類名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 大分類代號
        /// </summary>
        public string MainCode { get; set; }
    }
}