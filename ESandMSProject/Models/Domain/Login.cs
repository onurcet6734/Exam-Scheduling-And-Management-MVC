namespace ESandMSProject.Models.Domain
{
    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Roles { get; set; } = "";
        public ICollection<Student> Student { get; set; }
    }
}
