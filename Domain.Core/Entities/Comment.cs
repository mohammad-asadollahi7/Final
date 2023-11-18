using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public bool IsRecommended { get; set; }

    public bool? IsApproved { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
