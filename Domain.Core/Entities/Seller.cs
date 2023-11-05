using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Seller
{
    public int Id { get; set; }

    public int ApplicationUserId { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual Booth Booth { get; set; } = null!;
}
