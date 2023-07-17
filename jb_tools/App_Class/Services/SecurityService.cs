using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class SecurityService
{
    public static bool IsAdd { get; set; } = false;
    public static bool IsConfirm { get; set; } = false;
    public static bool IsDelete { get; set; } = false;
    public static bool IsDownload { get; set; } = false;
    public static bool IsEdit { get; set; } = false;
    public static bool IsInvalid { get; set; } = false;
    public static bool IsPrint { get; set; } = false;
    public static bool IsUndo { get; set; } = false;
    public static bool IsUpload { get; set; } = false;
    public static bool IsIndex { get { return (IsAdd || IsEdit || IsDelete || IsConfirm || IsDownload || IsInvalid || IsPrint || IsUndo || IsUpload) ? true : false; } }
}