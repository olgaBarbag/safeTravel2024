using SafeTravelApp.Core.Enums;

namespace SafeTravelApp.Data
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;   //Non - nullable
        public string Email { get; set; } = null!;      //Non - nullable
        public string Password { get; set; } = null!;   //Non - nullable
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public UserRole? UserRole { get; set; } = null!;

        //Non - nullable: If citizen/agent deleted then user too.
        public virtual Citizen Citizen { get; set; } = null!;
        public virtual Agent Agent { get; set; } = null!;

        //Nullable: if details delete turns to null
        public virtual UserDetails? Details { get; set; }
        public virtual ICollection<Recommendation>? Recommendations { get; set; } = new HashSet<Recommendation>();
    }
}
