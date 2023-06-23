using ESandMSProject.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESandMSProject.ViewModels
{
    public class ExamVM
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
    }
}
