using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Normative.Models.STD.Pipe
{
    public class V_VTC_Pipe_20_30
    {
        
        [Column("ID")]
        public int? Id { get; set; }

        [StringLength(63)]
        [Unicode(false)]
        public string ProductLine { get; set; }

        [StringLength(63)]
        [Unicode(false)]
        public string ProductType { get; set; }

        public int? ProductSizeId { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        public string NameOperationStep { get; set; }

        /*OP20*/
        [Column("OperationId_20")]
        public int? OperationId_20 { get; set; }
        
        [StringLength(255)]
        [Unicode(false)]
        [Column("Note_20")]
        public string Note_20 { get; set; }

        [Column("D1800_VT3_20")]
        public int? D1800VT3_20 { get; set; }

        [Column("D1800_VT6_20")]
        public int? D1800VT6_20 { get; set; }

        [Column("D1800_VT9_20")]
        public int? D1800VT9_20 { get; set; }

        [Column("D2100_VT11_20")]
        public int? D2100VT11_20 { get; set; }

        [Column("D2100_VT16_20")]
        public int? D2100VT16_20 { get; set; }

        [Column("D2100_VT21_20")]
        public int? D2100VT21_20 { get; set; }

        [Column("D2100_VT25_20")]
        public int? D2100VT25_20 { get; set; }

        [Column("D2500_VT26_20")]
        public int? D2500VT26_20 { get; set; }

        [Column("D2500_VT31_20")]
        public int? D2500VT31_20 { get; set; }

        [Column("D2500_VT43_20")]
        public int? D2500VT43_20 { get; set; }

        [Column("D3000_VT41_20")]
        public int? D3000VT41_20 { get; set; }

        [Column("D3000_VT50_20")]
        public int? D3000VT50_20 { get; set; }

        [Column("D3000_VT60_20")]
        public int? D3000VT60_20 { get; set; }

        /*OP30*/
        [Column("OperationId_30")]
        public int? OperationId_30 { get; set; }

        [StringLength(255)]
        [Unicode(false)]
        [Column("Note_30")]
        public string Note_30 { get; set; }

        [Column("D1800_VT3_30")]
        public int? D1800VT3_30 { get; set; }

        [Column("D1800_VT6_30")]
        public int? D1800VT6_30 { get; set; }

        [Column("D1800_VT9_30")]
        public int? D1800VT9_30 { get; set; }

        [Column("D2100_VT11_30")]
        public int? D2100VT11_30 { get; set; }

        [Column("D2100_VT16_30")]
        public int? D2100VT16_30 { get; set; }

        [Column("D2100_VT21_30")]
        public int? D2100VT21_30 { get; set; }

        [Column("D2100_VT25_30")]
        public int? D2100VT25_30 { get; set; }

        [Column("D2500_VT26_30")]
        public int? D2500VT26_30 { get; set; }

        [Column("D2500_VT31_30")]
        public int? D2500VT31_30 { get; set; }

        [Column("D2500_VT43_30")]
        public int? D2500VT43_30 { get; set; }

        [Column("D3000_VT41_30")]
        public int? D3000VT41_30 { get; set; }

        [Column("D3000_VT50_30")]
        public int? D3000VT50_30 { get; set; }

        [Column("D3000_VT60_30")]
        public int? D3000VT60_30 { get; set; }


        public string Diameter { get; set; }

        public string DrawingPosition { get; set; }

        public int? PipeBending { get; set; }
    }
}
