using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class CustomAttribute
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string AttributeTitle { get; set; } = null!;

    public string AttributeValue { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
