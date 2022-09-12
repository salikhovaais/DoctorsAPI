using System.Text.Json.Serialization;

namespace DoctorsAPI
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; } = 1000;

        [JsonIgnore]

        public Doctor Doctor { get; set; }

        public int DoctorId { get; set; }


         
        
    }
}
