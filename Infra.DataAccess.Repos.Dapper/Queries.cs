
namespace Infra.DataAccess.Repos.Dapper;

public static class Queries
{
    public static string GetProductsByCategoryId { get; } =
        "SELECT p.Id, AVG(r.Rate) AS 'CalculatedRate', d.DiscountPercent, p.PersianTitle, pr.Price AS 'Price', MIN(pic.Name) AS 'MainPicturePath' FROM dbo.Categories AS c INNER JOIN dbo.ProductsCategories AS pc ON pc.CategoryId = c.Id INNER JOIN dbo.Products AS p ON p.Id = pc.ProductId AND p.IsDeleted = 0 LEFT JOIN dbo.Rates AS r ON r.ProductId = p.Id INNER JOIN dbo.Prices AS pr ON pr.ProductId = p.Id AND pr.ToDate IS NULL INNER JOIN dbo.ProductPictures AS pp ON pp.ProductId = p.Id INNER JOIN dbo.Pictures AS pic ON pic.Id = pp.PictureId INNER JOIN dbo.Discounts AS d ON d.ProductId = p.Id AND d.ToDate is NULL WHERE c.Id IN({0})  GROUP BY p.Id, p.PersianTitle, pr.Price, d.DiscountPercent;";
}
