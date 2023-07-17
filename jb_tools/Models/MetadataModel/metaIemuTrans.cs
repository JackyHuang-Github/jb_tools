using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace jb_tools.Models
{
    [MetadataType(typeof(z_metaIemuTrans))]
    public partial class IemuTrans
    {
    }
}

public abstract class z_metaIemuTrans
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "單號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = true, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int No { get; set; }
    [Display(Name = "狀態碼")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Status { get; set; }
    [Display(Name = "客戶代號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CuNo { get; set; }
    [Display(Name = "客戶簡稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CuNa { get; set; }
    [Display(Name = "公司全稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CuName { get; set; }
    [Display(Name = "英文全稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CuEname { get; set; }
    [Display(Name = "經辦人代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CuSale { get; set; }
    [Display(Name = "經辦人姓名")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string CusaleName { get; set; }
    [Display(Name = "產業別代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string IndustryNo { get; set; }
    [Display(Name = "產業別名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string IndustryName { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
