namespace SafeTravelApp.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string LanguageName { get; set; } = null!;
        public string? Level { get; set; }
        public string? Degree { get; set; }

        public ICollection<Agent>? Agents{ get; set; } = new HashSet<Agent>();
    }
}
