using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Cart
{
    public int Id { get; set; }

    public int Status { get; set; }

    public DateTime OrderAt { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
