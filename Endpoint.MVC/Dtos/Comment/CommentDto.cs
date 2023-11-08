namespace Endpoint.MVC.Dtos.Comment;


public class CommentDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CustomerName { get; set; }

    public string ProductTitle { get; set; }

    public DateTime SubmittedDate { get; set; }

    public bool IsRecommended { get; set; }

}
