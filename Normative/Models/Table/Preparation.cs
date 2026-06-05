namespace Normative.Models.Table
{
    public class Preparation
    {
        public int Id { get; set; }
        public ProductSize ProductSize { get; set; }
        public int ProductSizeId { get; set; }
        public PreparationType PreparationType { get; set; }
        public int PreparationTypeId { get; set; }
        public decimal Fitter { get; set; }

        public decimal Welder { get; set; }

        //pridano
        public bool IsDeleted { get; set; }
        
        
    }
}
