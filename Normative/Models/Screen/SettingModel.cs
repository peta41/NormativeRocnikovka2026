using Normative.Models.Table;

namespace Normative.Models.Screen
{
    public class SettingModel
    {
        public Operation Operation { get; set; }
        public OperationStep OperationStep { get; set; }
        public ProductSize ProductSize { get; set; }
        public ProductType ProductType { get; set; }
        public ProductLine ProductLine { get; set; }
        public Preparation Preparation { get; set; }
        public PreparationType PreparationType { get; set; }

        public List<Operation> Operations { get; set; }
        public List<OperationStep> OperationSteps { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public List<ProductLine> ProductLines { get; set; }
        public List<Preparation> Preparations { get; set; }
        public List<PreparationType> PreparationTypes { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
