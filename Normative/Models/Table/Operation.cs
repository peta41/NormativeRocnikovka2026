using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models.Table;

public partial class Operation
{
    [Key]
    [Column("Operation_Id")]
    public int OperationId { get; set; }

    [Column("ProductLine_Id")]
    public int? ProductLineId { get; set; }

    [Column("ProductType_Id")]
    public int? ProductTypeId { get; set; }

    public int? OperationNumber { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string OperationDescription { get; set; }

    [StringLength(31)]
    [Unicode(false)]
    public string WorkCenter { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Operation")]
    public virtual ICollection<OperationStep> OperationStep { get; set; } = new List<OperationStep>();

    [ForeignKey("ProductLineId")]
    [InverseProperty("Operation")]
    public virtual ProductLine ProductLine { get; set; }

    [ForeignKey("ProductTypeId")]
    [InverseProperty("Operation")]
    public virtual ProductType ProductType { get; set; }

    //public Navigation Navigation { get; set; }
    //public ToolBar ToolBar { get; set; }
}
