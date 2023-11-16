namespace Endpoint.MVC.Dtos.Booth;

public class UpdateBoothViewModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IFormFile Picture { get; set; }
}
