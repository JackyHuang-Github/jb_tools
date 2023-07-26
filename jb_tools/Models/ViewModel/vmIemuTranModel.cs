using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jb_tools.Models.ViewModel
{
    public class vmIemuTranModel
    {
        public IemuTrans IemuTranModel { get; set; }
        //public List<IemuTranDetails> IemuTranDetailsModel { get; set; }
        // Jacky 1120726 for 分頁模式
        public IPagedList<IemuTranDetails> IemuTranDetailsModel { get; set; }
    }
}