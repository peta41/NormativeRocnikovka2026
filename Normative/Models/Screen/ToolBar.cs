namespace Normative.Models.Screen
{
    public class ToolBar
    {
        public List<ToolBarActionLink> Action { get; set; }
        public Dictionary<string, string> Filter { get; set; }
    }

    public class ToolBarActionLink
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string JavaScript { get; set; }
        public bool Visible { get; set; } = true;
        public string Parameter { get; set; } 
    }
}
