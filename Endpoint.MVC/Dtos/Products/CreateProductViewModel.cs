﻿
using System.Security.Principal;

namespace Endpoint.MVC.Dtos.Products;


public class CreateProductViewModel
{
    public string PersianTitle { get; set; }


    public string EnglishTitle { get; set; }


    public string Description { get; set; }


    public int FirstQuantity { get; set; }


    public decimal FirstPrice { get; set; }

    public int FirstDiscount { get; set; }


    public int CategoryId { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }


    public IFormFile Picture { get; set; }

}
