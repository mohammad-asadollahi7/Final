namespace Endpoint.MVC.Dtos.Comment;

public class CreateCommentDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int ProductId { get; set; }

    public bool IsRecommended { get; set; }
}
