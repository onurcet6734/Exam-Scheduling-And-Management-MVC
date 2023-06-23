namespace ESandMSProject.Models.Domain
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = ""; 
        public int Year { get; set; }
        public string Semester { get; set; } = ""; 
        public int NumberOfStudent { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Exam> Exams { get; set; }

        public Class()
        {
            Students = new HashSet<Student>();
            Exams = new HashSet<Exam>();
        }
    }
}