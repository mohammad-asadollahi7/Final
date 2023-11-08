using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? ParentId { get; set; }

    public int CategoryPictureId { get; set; }

    public virtual ICollection<CustomAttributesTemplate> CustomAttributesTemplates { get; set; } = new List<CustomAttributesTemplate>();

    public virtual CategoryPicture CategoryPicture { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
