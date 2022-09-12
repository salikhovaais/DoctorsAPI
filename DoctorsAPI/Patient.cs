using System.Text.Json.Serialization;

namespace DoctorsAPI
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Doctor> Doctors { get; set; }
         
    }
}
