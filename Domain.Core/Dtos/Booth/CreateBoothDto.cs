using Domain.Core.Dtos.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Dtos.Booth;

public class CreateBoothDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public PictureDto PictureDto { get; set; }
}
