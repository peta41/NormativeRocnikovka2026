using Normative.Models.Table;
using X.PagedList;

namespace Normative.Models.Screen
{
    public class OperationStepModel
    {
        public IPagedList<OperationStep> OperationSteps { get; set; }
        //add ProductTypes && ProductLines protoze Edit u operation je vyzaduje NEFUNGUJE!!!!
        public List<Operation> Operations { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public ProductType Operation { get; set; }
        public ProductLine ProductSize { get; set; }
        //add end
        public OperationStep OperationStep { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
