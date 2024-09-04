using Microsoft.AspNetCore.Mvc;
using TestForHH.DTO;
using TestForHH.ORM;
using TestForHH.Services;

namespace TestForHH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : Controller
    {
        private readonly DoctorService _doctorService;
        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<ActionResult> AddDoctor(DoctorDTO doctorDto)
        {
            var doctor = new Doctor
            {
                FullName = doctorDto.FullName,
                Cabinet = new Cabinet() { Number = doctorDto.CabinetNumber },
                Specialization = new Specialization() { Name = doctorDto.Specialization },
                Area = new Area() { Number = doctorDto.AreaNumber },
            };

            await _doctorService.AddDoctorAsync(doctor);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDoctor(DoctorDTO doctorDto)
        {
            var doctor = new Doctor
            {
                FullName = doctorDto.FullName,
                Cabinet = new Cabinet() { Number = doctorDto.CabinetNumber },
                Specialization = new Specialization() { Name = doctorDto.Specialization },
                Area = new Area() { Number = doctorDto.AreaNumber },
            };

            await _doctorService.UpdateDoctorAsync(doctor);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetDoctors([FromQuery] int startIndex = 0,
                                                                            [FromQuery] int count = 10,
                                                                            [FromQuery] string sortBy = "id",
                                                                            [FromQuery] bool ascending = true)
        {
            var doctors = await _doctorService.GetDoctorsAsync(startIndex, count, sortBy, ascending);

            var doctorsDto = doctors.Select(d => new DoctorDTO()
            {
                Id = d.Id,
                FullName = d.FullName,
                CabinetNumber = d.Cabinet.Number,
                Specialization = d.Specialization.Name,
                AreaNumber = d.Area.Number
            });

            return Ok(doctorsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorUpdateDTO>> GetDoctorForUpdate(int id)
        {
            var doctor = await _doctorService.GetDoctorForUpdateAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            DoctorUpdateDTO doctorUpdate = new()
            {
                Id = doctor.Id,
                CabinetId = doctor.CabinetId,
                AreaId = doctor.AreaId,
                SpecializationId  = doctor.AreaId
            };

            return Ok(doctorUpdate);
        }
    }
}
