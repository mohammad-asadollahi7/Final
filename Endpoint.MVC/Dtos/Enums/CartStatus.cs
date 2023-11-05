using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Endpoint.MVC.Dtos.Enums;

public enum CartStatus
{
    [Display(Name = "فعال")]
    In_Progress,

    [Display(Name = "تحویل داده شده")]
    Delivered,

    [Display(Name = "لغو شده")]
    cancelled
}
