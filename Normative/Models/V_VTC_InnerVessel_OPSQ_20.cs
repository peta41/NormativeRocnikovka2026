using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models;

[Keyless]
public partial class V_VTC_InnerVessel_OPSQ_20
{
    public int? Sequence { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string ProductLine { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string ProductType { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string NameOperationStep { get; set; }

    [Column("Operation_Id")]
    public int? OperationId { get; set; }

    [Column("ProductSize_Id")]
    public int? ProductSizeId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Note { get; set; }

    public int? D1350 { get; set; }

    public int? D1700 { get; set; }

    public int? D2100 { get; set; }

    public int? D2500 { get; set; }

    public string Option { get; set; }

    public string DrawingPosition { get; set; }
}
