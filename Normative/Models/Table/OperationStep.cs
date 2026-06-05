using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models.Table;

public partial class OperationStep
{
    [Key]
    [Column("OperationStep_Id")]
    public int OperationStepId { get; set; }

    [Column("Operation_Id")]
    public int? OperationId { get; set; }

    [Column("ProductSize_Id")]
    public int? ProductSizeId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DrawingPosition { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; }

    public int? Sequence { get; set; }

    public int? StandardHour { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Diameter { get; set; }

    public int? PipeBending { get; set; }

    [ForeignKey("OperationId")]
    [InverseProperty("OperationStep")]
    public virtual Operation Operation { get; set; }

    [ForeignKey("ProductSizeId")]
    [InverseProperty("OperationStep")]
    public virtual ProductSize ProductSize { get; set; }
    //public Navigation Navigation { get; set; }
    //public ToolBar ToolBar { get; set; }
}
