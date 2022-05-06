using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeContrato.API.Domain.Models
{
    [Table("Technicians")]
    public class Technician : User
    {
        public int CTechnician { get; set; }
        public string TBio { get; set; }
        public string NEducation { get; set; }
        public int Numphone { get; set; }
        public IList<Project> CProjects { get; set; }
    }
}
