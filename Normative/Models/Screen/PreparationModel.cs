using Normative.Models.Table;
using X.PagedList;

namespace Normative.Models.Screen
{
    public class PreparationModel
    {
        public IPagedList<V_Preparation> PreparationsList { get; set; }
        public List<PreparationType> PreparationsType { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public Preparation Preparation { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
