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
        public async Task<ActionResult<List<Doctor>>> Get()
        {
            return Ok(await _context.Doctors.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Get(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);
            if (doc == null)
                return BadRequest("Doctor not found.");
            return Ok(doc);
        }

        [HttpPost]
        public async Task<ActionResult<List<Doctor>>> AddDoctor(Doctor doc)
        {
            _context.Doctors.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(await _context.Doctors.ToListAsync());
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
