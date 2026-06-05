using Normative.Models.Table;
using X.PagedList;

namespace Normative.Models.Screen
{
    public class PreparationsModel
    {
        public IPagedList<ProductSize> ProductSizes { get; set; }
        public ProductSize ProductSize { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
