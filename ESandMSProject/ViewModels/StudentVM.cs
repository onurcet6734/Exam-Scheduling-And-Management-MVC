using ESandMSProject.Models.Domain;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESandMSProject.ViewModels
{
    public class StudentVM
    {  
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int LoginId{ get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
        public string SchoolNumber { get; set; }
    }
}
