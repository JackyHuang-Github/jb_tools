using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jb_tools.Models.ViewModel
{

    /// <summary>
    /// for DropDownList use only
    /// </summary>
    public class vmDDLMainCodeModel
    {
        /// <summary>
        /// 大分類代號
        /// </summary>
        public string MainCode { get; set; }
        /// <summary>
        /// 大分類識別
        /// </summary>
        public string McId { get; set; }
        /// <summary>
        /// 大分類名稱
        /// </summary>
        public string Name { get; set; }
    }
}