using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace jb_tools.Models
{
    [MetadataType(typeof(z_metaIemuMainMenus))]
    public partial class IemuMainMenus
    {
    }
}

public abstract class z_metaIemuMainMenus
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序號碼")]
    [Column(CheckBox = false, Hidden = true, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public int SortNum { get; set; }
    [Display(Name = "大分類代號")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MainCode { get; set; }
    [Display(Name = "大分類識別")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string McId { get; set; }
    [Display(Name = "大分類名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Name { get; set; }
    [Display(Name = "英文名稱")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Ename { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
