﻿using Endpoint.MVC.Dtos.Pictures;

namespace Endpoint.MVC.Dtos.Products;

public class CreateAuctionProductDto
{
    public string PersianTitle { get; set; }

    public string EnglishTitle { get; set; }

    public string Description { get; set; }

    public int BoothId { get; set; }
    public int CategoryId { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal MinPrice { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }

    public PictureDto PictureDto { get; set; }
}
