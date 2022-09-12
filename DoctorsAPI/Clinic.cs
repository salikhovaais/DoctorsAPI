namespace DoctorsAPI
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;

        public List<Doctor> Doctors { get; set; }



    }
}
