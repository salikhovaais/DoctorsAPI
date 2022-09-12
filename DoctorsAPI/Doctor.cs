using System.Text.Json.Serialization;

namespace DoctorsAPI
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;


        [JsonIgnore]
        public Clinic Clinic { get; set; }
        public int ClinicId { get; set; }
        public Service Service { get; set; }
        public List<Patient> Patients { get; set; }
        

  
        

    }

}
