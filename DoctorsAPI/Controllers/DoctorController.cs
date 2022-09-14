using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

           
        private readonly DataContext _context;

        public DoctorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> Get(int clinicId)
        {
            var doctors = await _context.Doctors
                .Where(c => c.ClinicId == clinicId)
                .Include(c => c.Service)
                .Include(c => c.Patients)
                
                .ToListAsync();

            return doctors;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Doctor>> GetName(string name)
        {

            var doc = await _context.Doctors
                .Where(c => c.Name == name)
                .Include(c => c.Service)
                .Include(c => c.Patients)

                .ToListAsync(); 
            
            
            if (doc == null)
                return BadRequest("Doctor not found.");
            return Ok(doc);
        }

        [HttpPost]
        public async Task<ActionResult<List<Doctor>>> AddDoctor(CreateDoctor request)
        {
            var clinic = await _context.Clinics.FindAsync(request.ClinicId);
            if (clinic == null)
                return NotFound();

            var newDoctor = new Doctor{
                Name = request.Name,
                Clinic = clinic
            };

            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            return await Get(newDoctor.ClinicId);
        }


        [HttpPost("service")]
        public async Task<ActionResult<Doctor>> AddService(AddService request)
        {
            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            if (doctor == null)
                return NotFound();

            var newService = new Service
            {
                Name = request.Name,
                Price = request.Price,
                Doctor = doctor
            };

            _context.Services.Add(newService);
            await _context.SaveChangesAsync();

            return doctor;
        }

        [HttpPost("patient")]
        public async Task<ActionResult<Doctor>> AddDoctorAndPatient(AddDoctorPatient request)
        {
            var doctor = await _context.Doctors
                .Where(c => c.Id == request.DoctorId)
                .Include(c => c.Patients)
                .FirstOrDefaultAsync();

            if (doctor == null)
                return NotFound();

            var patient = await _context.Patients.FindAsync(request.PatientId);
            if (patient == null)
                return NotFound();

            doctor.Patients.Add(patient);

            await _context.SaveChangesAsync();

            return doctor;
        }


        

        [HttpPut]
        public async Task<ActionResult<List<Doctor>>> UpdateDoctor(Doctor request)
        {
            var dbDoc = await _context.Doctors.FindAsync(request.Id);
            if (dbDoc == null)
                return BadRequest("Doctor not found.");

            dbDoc.Name = request.Name;
            dbDoc.FirstName = request.FirstName;
            dbDoc.LastName = request.LastName;
            dbDoc.Place = request.Place;

            //_context.Doctors.Add(dbDoc);
            await _context.SaveChangesAsync();

            return Ok(await _context.Doctors.ToListAsync());
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<List<Doctor>>> Delete(int id)
        {
            var dbDoc = await _context.Doctors.FindAsync(id);
            if (dbDoc == null)
                return BadRequest("Doctor not found.");
            
            _context.Doctors.Remove(dbDoc);
            await _context.SaveChangesAsync();
            return Ok(await _context.Doctors.ToListAsync());
        }
    }
}
