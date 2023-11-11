using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Enums;

public enum Medal
{
    [Display(Name = "برنز")]
    Bronze,

    [Display(Name = "نقره")]
    Silver,

    [Display(Name = "طلا")]
    Gold,
}
