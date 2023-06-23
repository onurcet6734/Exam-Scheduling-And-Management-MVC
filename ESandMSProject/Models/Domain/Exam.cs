using System.ComponentModel.DataAnnotations.Schema;

namespace ESandMSProject.Models.Domain
{
    public class Exam
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }
        public string Name { get; set; }
        public virtual Class Class { get; set; }
    }
}
