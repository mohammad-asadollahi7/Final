using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class CustomAttributesTemplate
{
    public int Id { get; set; }

    public string AttributeTitle { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
