using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jb_tools.Models.ViewModel
{
    public class vmDDLDetailOrderModel
    {
        /// <summary>
        /// 細分類序號
        /// </summary>
        public string DetailOrder { get; set; }
        /// <summary>
        /// 程式代號
        /// </summary>
        public string Program { get; set; }
        /// <summary>
        /// 程式名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 程式名稱
        /// </summary>
        public string ProgramPath { get; set; }
        /// <summary>
        /// 中分類識別
        /// </summary>
        public string ScId { get; set; }
        /// <summary>
        /// 大分類代號
        /// </summary>
        public string MainCode { get; set; }
    }
}