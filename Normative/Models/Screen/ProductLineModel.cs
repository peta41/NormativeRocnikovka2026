using Normative.Models.Table;
using X.PagedList;

namespace Normative.Models.Screen
{
    public class ProductLineModel
    {
        //add
        public IPagedList<ProductLine> ProductLines { get; set; }
        //add end/
        public ProductLine ProductLine { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
