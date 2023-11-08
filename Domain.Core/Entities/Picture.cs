using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Picture
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual Booth? Booth { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ProductPicture? ProductPicture { get; set; }
    public virtual BoothPicture? BoothPicture { get; set; }
    public virtual CategoryPicture? CategoryPicture { get; set; }

}
