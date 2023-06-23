using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESandMSProject.Models.Domain
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SchoolNumber { get; set; }
        public virtual Class Class { get; set; }
        public int LoginId { get; set; }
        public ICollection<Scheduling> Schedulings { get; set; }
        public Login Login { get; set; }
    }
}