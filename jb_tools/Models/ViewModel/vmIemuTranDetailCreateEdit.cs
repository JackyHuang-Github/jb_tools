using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jb_tools.Models.ViewModel
{
    public class vmIemuTranDetailCreateEdit
    {
        public IemuTranDetails IemuTranDetailsModel { get; set; }
        public vmDDLScIdModel vmDDLScIdModel { get; set; }
        public vmDDLDetailOrderModel vmDDLDetailOrderModel { get; set; }
    }
}