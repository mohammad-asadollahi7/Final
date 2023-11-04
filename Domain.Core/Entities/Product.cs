﻿using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string EnglishTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int BoothId { get; set; }

    public bool SellType { get; set; }

    public bool? IsApproved { get; set; }

    public virtual ICollection<AuctionOrder> AuctionOrders { get; set; } = new List<AuctionOrder>();

    public virtual Booth Booth { get; set; } = null!;

    public virtual ICollection<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();

    public virtual Auction IdNavigation { get; set; } = null!;

    public virtual NonAuctionPrice? NonAuctionPrice { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<ProductPicture> ProductPictures { get; set; } = new List<ProductPicture>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}