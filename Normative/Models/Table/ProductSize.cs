using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models.Table;

public partial class ProductSize
{
    [Key]
    [Column("ProductSize_Id")]
    public int ProductSizeId { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Size { get; set; }

    [InverseProperty("ProductSize")]
    public virtual ICollection<OperationStep> OperationStep { get; set; } = new List<OperationStep>();
    //public Navigation Navigation { get; set; }
    //public ToolBar ToolBar { get; set; }
}
