using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.DTO.Citizen
{
    public class CitizenReadOnlyDTO
    {
        public int Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Occupation { get; set; }
        

    }
}
