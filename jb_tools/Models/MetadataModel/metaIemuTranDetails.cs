using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace jb_tools.Models
{
    [MetadataType(typeof(z_metaIemuTranDetails))]
    public partial class IemuTranDetails
    {
        [NotMapped]
        [Display(Name = "大分類識別")]
        public string McId { get; set; }
        [NotMapped]
        [Display(Name = "大分類名稱")]
        public string MName { get; set; }
        [NotMapped]
        [Display(Name = "中分類名稱")]
        public string SName { get; set; }
        //[NotMapped]
        //[Display(Name = "位置或檔案路徑二")]
        //public string PosOrPath2 { get; set; }
    }
}

public abstract class z_metaIemuTranDetails
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "單號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = true, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string No { get; set; }
    [Display(Name = "項次")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = true, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Seq { get; set; }
    [Display(Name = "大分類代號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string MainCode { get; set; }
    [Display(Name = "中分類識別")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ScId { get; set; }
    [Display(Name = "細分類序號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string DetailOrder { get; set; }
    [Display(Name = "程式代號")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Program { get; set; }
    [Display(Name = "程式名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Name { get; set; }
    [NotMapped]
    [Display(Name = "英文名稱")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Ename { get; set; }
    [NotMapped]
    [Display(Name = "程式路徑")]
    [Required(ErrorMessage = "不可空白!!")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string ProgramPath { get; set; }
    [Display(Name = "備註")]
    [Column(CheckBox = false, Hidden = false, DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string Remark { get; set; }
}
