namespace GeomancyWebUI.Client.Models
{
    /// <summary>
    /// Reader-centric Company of Houses glossary entry from
    /// databank/PerfectionDirectory/CompanyTypeData.json.
    /// </summary>
    public class CompanyTypeEntry
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ShortLabel { get; set; } = string.Empty;
        public string ListLabel { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string DetectionRule { get; set; } = string.Empty;
        public string MechanismSummary { get; set; } = string.Empty;
        public string CoReads { get; set; } = string.Empty;
        public List<string> InterpretationParagraphs { get; set; } = new();
        public List<CompanyTypeVariant> Variants { get; set; } = new();
    }

    public class CompanyTypeVariant
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string MechanismSummary { get; set; } = string.Empty;
    }

    public class CompanyTypeOverview
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string MechanismSummary { get; set; } = string.Empty;
        public string CoReads { get; set; } = string.Empty;
        public List<string> InterpretationParagraphs { get; set; } = new();
    }

    public class CompanyTypeDirectory
    {
        public CompanyTypeOverview? Overview { get; set; }
        public List<CompanyTypeEntry> CompanyTypes { get; set; } = new();
    }
}
