﻿
using System.Threading;

namespace Infra.DataAccess.Repos.Dapper;

public static class Queries
{
    public static string GetNonAuctionProductsByCategoryId { get; } =
       "SELECT b.Title AS 'BoothTitle', p.Id, pr.Discount AS 'DiscountPercent', p.PersianTitle, pr.Price AS 'Price', MIN(pic.Name) AS 'PicturesName' FROM dbo.Categories AS c INNER JOIN dbo.ProductsCategories AS pc ON pc.CategoryId = c.Id INNER JOIN dbo.Products AS p ON p.Id = pc.ProductId AND p.IsDeleted = 0 AND p.SellType = 0 AND p.IsApproved = 1 INNER JOIN dbo.NonAuctionPrices AS pr ON pr.ProductId = p.Id INNER JOIN dbo.ProductPictures AS pp ON pp.ProductId = p.Id INNER JOIN dbo.Pictures AS pic ON pic.Id = pp.PictureId INNER JOIN dbo.Booths AS b ON b.Id = p.BoothId WHERE c.Id IN({0}) GROUP BY p.Id, p.PersianTitle, pr.Price, pr.Discount, b.Title";

    public static string UpdateMedal { get; } =
        "UPDATE dbo.Booths SET Wage = {0}, Medal = {1} WHERE Id = {2}";

    public static string GetProductInventories { get; } =
        "SELECT pi.Id, pi.IsSold, pi.SellPrice, pi.Quantity, b.Id AS 'BoothId' FROM dbo.ProductInventories AS pi INNER JOIN dbo.Products AS p ON p.Id = pi.productId INNER JOIN dbo.Booths AS b ON b.Id = p.BoothId WHERE b.Id IN ({0});";
}
    
