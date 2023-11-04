using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Address
{
    public int Id { get; set; }

    public string Province { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int Unit { get; set; }

    public virtual Customer IdNavigation { get; set; } = null!;

    public virtual Seller? Seller { get; set; }
}
