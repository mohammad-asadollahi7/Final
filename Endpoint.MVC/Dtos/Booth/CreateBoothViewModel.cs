
namespace Endpoint.MVC.Dtos.Booth;

public class CreateBoothViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Picture { get; set; }
}
