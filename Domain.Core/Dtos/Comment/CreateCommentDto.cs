
namespace Domain.Core.Dtos.Comment;

public class CreateCommentDto
{
    public int Id { get; set; }

    public string Title { get; set; } 

    public string Description { get; set; } 

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public bool IsRecommended { get; set; }
}
