using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESandMSProject.Models.Domain
{
    public class Scheduling
    {
        public int Id { get; set; }
        public string PaperName { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public int Duration { get; set; }
         
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }
        
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }

        public int StudentId { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual Exam Exam{ get; set; }

        public virtual Student Student { get; set; }

    }
}
