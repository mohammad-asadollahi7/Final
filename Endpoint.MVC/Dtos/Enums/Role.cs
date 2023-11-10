using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Enums;

public enum Role
{
    [Display(Name = "مشتری")]
    Customer,

    [Display(Name = "ادمین")]
    Admin,

    [Display(Name = "فروشنده")]
    Seller,
}
