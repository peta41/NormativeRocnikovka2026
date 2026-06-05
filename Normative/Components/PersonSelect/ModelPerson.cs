namespace Components
{
    public class ModelPerson
    {
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }
        public bool Enable { get; set; }
        public List<PersonRow> Rows { get; set; }
    }

    public class PersonRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
    }
}
