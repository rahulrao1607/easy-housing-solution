using System;
using System.Collections.Generic;

namespace EasyHousingSolutionWebAPI.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public int PropertyId { get; set; }

    public byte[] Image1 { get; set; } = null!;

    //public virtual Property Property { get; set; } = null!;
}
