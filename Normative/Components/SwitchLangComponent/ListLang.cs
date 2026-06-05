namespace FRS.UI.Components.SwitchLang
{
    public class ListLang
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Flag;

        public ListLang(string Code, string DisplayName)
        {
            this.Code = Code;
            this.DisplayName = DisplayName;

            List<CodeFlag> _lang =
            [
                new CodeFlag() { Code = "fr", Flag = "fi fi-fr" },
                new CodeFlag() { Code = "cs", Flag = "fi fi-cz" },
                new CodeFlag() { Code = "de", Flag = "fi fi-de" },
                new CodeFlag() { Code = "gb", Flag = "fi fi-gb" },
                new CodeFlag() { Code = "us", Flag = "fi fi-us" },
            ];

            CodeFlag? codeflag = _lang.Where(w => Code.Contains(w.Code, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            string lang = "us";
            if (codeflag != null && _lang != null) {
                lang = codeflag.Code;
                Flag = _lang.FirstOrDefault(f => f.Code == codeflag.Code).Flag;
            }
            else
            {
                Flag = "fi fi-us";
            }
            
            
            
        }

        private class CodeFlag
        {
            public required string Code { get; set; }
            public required string Flag { get; set; }
        }
    }
}
