using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using jb_tools.Models;

public class vmOrderModel
{
    public Orders OrderModel { get; set; }
    public List<OrderDetails> DetailModel { get; set; }   
}