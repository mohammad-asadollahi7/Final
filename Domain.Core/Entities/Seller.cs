using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Seller
{
    public int Id { get; set; }

    public int ApplicationUserId { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual Booth IdNavigation { get; set; } = null!;
}
