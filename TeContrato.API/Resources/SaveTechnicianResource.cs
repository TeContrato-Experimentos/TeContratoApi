using System.ComponentModel.DataAnnotations;

namespace TeContrato.API.Resources
{
    public class SaveTechnicianResource : SaveUserResource
    {
        public string TBio { get; set; }
        public string NEducation { get; set; }
        public int Numphone { get; set; }
    }
}