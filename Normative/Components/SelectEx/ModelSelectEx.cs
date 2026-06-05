namespace Components
{
    public class ModelSelectEx
    {
        public string DisplayName { get; set; }
        public string Identificator { get; set; }
        public bool IsRequired { get; set; }
        public int? DefaultValue { get; set; }
        public bool Enable { get; set; }
        public List<SelectExRow> Rows { get; set; }
    }

    public class SelectExRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
