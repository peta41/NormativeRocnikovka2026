using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models.Table;

public partial class ProductLine
{
    [Key]
    [Column("ProductLine_Id")]
    public int ProductLineId { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("ProductLine")]
    public virtual ICollection<Operation> Operation { get; set; } = new List<Operation>();
    //public Navigation Navigation { get; set; }
    //public ToolBar ToolBar { get; set; }
}
