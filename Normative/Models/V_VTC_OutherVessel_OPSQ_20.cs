using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models;

[Keyless]
public partial class V_VTC_OutherVessel_OPSQ_20
{
    [StringLength(63)]
    [Unicode(false)]
    public string ProductLine { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string ProductType { get; set; }

    public int? Sequence { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string NameOperationStep { get; set; }

    [Column("Operation_Id")]
    public int? OperationId { get; set; }

    public int? D1800 { get; set; }

    public int? D2100 { get; set; }

    public int? D2500 { get; set; }

    public int? D3000 { get; set; }

    public string DrawingPosition {  get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Option { get; set; }
}
