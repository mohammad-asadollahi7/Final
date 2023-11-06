
namespace Infra.DataAccess.Repos.Dapper;

public static class Queries
{
    public static string GetProductsByCategoryId { get; } =
       "SELECT p.Id, pr.Discount, p.PersianTitle, pr.Price AS 'Price', MIN(pic.Name) AS 'MainPicturePath' FROM dbo.Categories AS c INNER JOIN dbo.ProductsCategories AS pc ON pc.CategoryId = c.Id INNER JOIN dbo.Products AS p ON p.Id = pc.ProductId AND p.IsDeleted = 0 AND p.SellType = 0 INNER JOIN dbo.NonAuctionPrice AS pr ON pr.ProductId = p.Id INNER JOIN dbo.ProductPictures AS pp ON pp.ProductId = p.Id INNER JOIN dbo.Pictures AS pic ON pic.Id = pp.PictureId WHERE c.Id IN({0}) GROUP BY p.Id, p.PersianTitle, pr.Price, pr.Discount";
}