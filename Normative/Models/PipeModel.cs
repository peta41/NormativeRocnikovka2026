using Normative.Models.Screen;
using Normative.Models.STD.Pipe;

namespace Normative.Models
{
    public class PipeModel
    {
        public List<V_VTC_Pipe_20_30> V_VTC_Pipe_20_30 { get; set; }
        public List<PreparationType> PreparationTypes { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }    
    }
}
