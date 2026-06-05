using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Normative.Models.Table;

public partial class ProductType
{
    [Key]
    [Column("ProductType_Id")]
    public int ProductTypeId { get; set; }

    [StringLength(63)]
    [Unicode(false)]
    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("ProductType")]
    public virtual ICollection<Operation> Operation { get; set; } = new List<Operation>();
}
