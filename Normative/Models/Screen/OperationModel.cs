using Normative.Models.Table;
using System.Drawing;
using X.PagedList;

namespace Normative.Models.Screen
{
    public class OperationModel
    {
        public IPagedList<Operation> Operations { get; set; }
        //add ProductTypes && ProductLines protoze Edit u operation je vyzaduje NEFUNGUJE!!!!
        public List<ProductType> ProductTypes { get; set; }
        public List<ProductLine> ProductLines { get; set; }
        public ProductLine ProductLine { get; set; }
        public ProductType ProductType { get; set; }
        public Preparation Preparation { get; set; }
        //add end
        public Operation Operation { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
