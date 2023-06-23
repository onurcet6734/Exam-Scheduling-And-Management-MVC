using System.ComponentModel.DataAnnotations;

namespace ESandMSProject.Models.Domain
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
