namespace Normative.Models.Screen
{
    public class OutherVessel
    {
        public List<V_VTC_OutherVessel_OPSQ_10> Op10 { get; set; }
        public List<V_VTC_OutherVessel_OPSQ_20> Op20 { get; set; }
        public List<V_VTC_OutherVessel_OPSQ_40> Op40 { get; set; }
        public Navigation Navigation { get; set; }
        public ToolBar ToolBar { get; set; }
    }
}
