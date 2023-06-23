namespace ESandMSProject.ViewModels
{
    public class SchedulingVM
    {
        public int Id { get; set; }
        public string PaperName { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamTime { get; set; }
        public int Duration { get; set; }
        public int HallId { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }

        public DateTime ExamDateOnly => ExamDate.Date;
    }
}
