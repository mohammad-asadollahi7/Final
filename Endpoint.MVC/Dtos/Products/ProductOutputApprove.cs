namespace Endpoint.MVC.Dtos.Products;

public class ProductOutputApprove
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string BoothTitle { get; set; }

    public IEnumerable<string> PicturesPath { get; set; }
}
