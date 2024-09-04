using Microsoft.AspNetCore.Mvc;
using TestForHH.DTO;
using TestForHH.ORM;
using TestForHH.Services;

namespace TestForHH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private readonly PatientService _patientService;

        public PatientsController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult> AddPatient(PatientDTO patientDTO)
        {
            Patient patient = new()
            {
                Name = patientDTO.Name,
                SecondName = patientDTO.Name,
                Patronymic = patientDTO.Patronymic,
                DateOfBirth = patientDTO.DateOfBirth,
                Sex = patientDTO.Sex,
                Address = patientDTO.Address,
                Area = new Area() { Number = patientDTO.AreaNumber },
            };

            await _patientService.AddPatientAsync(patient);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePatient(PatientDTO patientDto)
        {
            var patient = new Patient
            {
                Id = patientDto.Id,
                SecondName = patientDto.SecondName,
                Name = patientDto.Name,
                Patronymic = patientDto.Patronymic,
                Address = patientDto.Address,
                DateOfBirth = patientDto.DateOfBirth,
                Sex = patientDto.Sex,
                Area = new Area() { Number = patientDto.AreaNumber }
            };

            await _patientService.UpdatePatientAsync(patient);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatients([FromQuery] int startIndex = 0,
                                                                             [FromQuery] int count = 10,
                                                                             [FromQuery] string sortBy = "id",
                                                                             [FromQuery] bool ascending = true)
        {
            var patients = await _patientService.GetPatientsAsync(startIndex, count, sortBy, ascending);

            var patientsDto = patients
                .Select(p => new PatientDTO
                {
                    Id = p.Id,
                    SecondName = p.SecondName,
                    Name = p.Name,
                    Patronymic = p.Patronymic,
                    Address = p.Address,
                    DateOfBirth = p.DateOfBirth,
                    Sex = p.Sex,
                    AreaNumber = p.Area.Number
                });

            return Ok(patientsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientUpdateDTO>> GetPatientForUpdate(int id)
        {
            var patient = await _patientService.GetPatientForUpdateAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var patientUpdate = new PatientUpdateDTO
            {
                Id = patient.Id,
                AreaId = patient.AreaId
            };

            return Ok(patientUpdate);
        }


    }
}
