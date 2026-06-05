using Normative.Models.Table;
using X.PagedList;

namespace Normative.Models.Screen
{
    public class ProductTypeModel
    {
        public IPagedList<ProductType> ProductTypes { get; set; }
        public ProductType ProductType { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
