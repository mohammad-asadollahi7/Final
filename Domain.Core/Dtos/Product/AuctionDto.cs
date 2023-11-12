

using Domain.Core.Enums;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Core.Dtos.Product;

public class AuctionDto
{
    public int Id { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal MinPrice { get; set; }

    public bool IsActive { get; set; }
}
