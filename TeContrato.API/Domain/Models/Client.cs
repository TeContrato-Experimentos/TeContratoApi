using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeContrato.API.Domain.Models
{
    [Table("Clients")]
    public class Client : User
    {
        public string TBio { get; set; }
        public string TAddress { get; set; }
        public City CCity { get; set; }
        public int CityId { get; set; }
        public IList<Project> CProjects { get; set; }
        
    }
}
