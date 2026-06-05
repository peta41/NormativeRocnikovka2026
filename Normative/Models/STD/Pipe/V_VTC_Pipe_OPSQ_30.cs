using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models.STD.Pipe;

[Keyless]
public partial class V_VTC_Pipe_OPSQ_30
{
    [Column("ID")]
    public int? Id { get; set; }

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

    [Column("D1800_VT3")]
    public int? D1800VT3 { get; set; }

    [Column("D1800_VT6")]
    public int? D1800VT6 { get; set; }

    [Column("D1800_VT9")]
    public int? D1800VT9 { get; set; }

    [Column("D2100_VT11")]
    public int? D2100VT11 { get; set; }

    [Column("D2100_VT16")]
    public int? D2100VT16 { get; set; }

    [Column("D2100_VT21")]
    public int? D2100VT21 { get; set; }

    [Column("D2100_VT25")]
    public int? D2100VT25 { get; set; }

    [Column("D2500_VT26")]
    public int? D2500VT26 { get; set; }

    [Column("D2500_VT31")]
    public int? D2500VT31 { get; set; }

    [Column("D2500_VT43")]
    public int? D2500VT43 { get; set; }

    [Column("D3000_VT41")]
    public int? D3000VT41 { get; set; }

    [Column("D3000_VT50")]
    public int? D3000VT50 { get; set; }

    [Column("D3000_VT60")]
    public int? D3000VT60 { get; set; }

    public string Diameter { get; set; }

    public string DrawingPosition { get; set; }
    public int? PipeBending { get; set; }
}
