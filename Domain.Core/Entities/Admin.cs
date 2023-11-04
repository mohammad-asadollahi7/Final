using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Admin
{
    public int Id { get; set; }

    public int ApplicationUserId { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}
