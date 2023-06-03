using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Doctors;
using HospitalAPI.Models.Specialities;
using HospitalAPI.Models.WorkHistories;
using HospitalAPI.Services.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController : ControllerBase
{
    private readonly ILogger<DoctorController> _logger;
    private readonly IDoctorsService _doctorsService;
    private readonly IMapper _mapper;

    public DoctorController(ILogger<DoctorController> logger,
        IDoctorsService doctorsService,
        IMapper mapper)
    {
        _logger = logger;
        _doctorsService = doctorsService;
        _mapper = mapper;
    }

    [HttpGet("getDoctors")]
    public async Task<ActionResult<DoctorReadDto>> GetDoctors()
    {
        var doctors = await _doctorsService.GetDoctors();

        var doctorReadDtos = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);

        return Ok(doctorReadDtos);
    }

    [HttpGet("getDoctor/{id}")]
    public async Task<ActionResult<DoctorReadDto>> GetDoctor(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID");

        var doctor = await _doctorsService.GetDoctorById(id);

        if (doctor == null)
            return NotFound();

        var doctorReadDto = _mapper.Map<DoctorReadDto>(doctor);

        return Ok(doctorReadDto);
    }

    [HttpPut("changeDoctor/{id}")]
    public async Task<ActionResult> ChangeDoctor(int id, DoctorUpdateDto doctorUpdateDto)
    {
        var doctor = await _doctorsService.GetDoctorById(id);

        if (doctor == null)
            return NotFound();

        _mapper.Map(doctorUpdateDto, doctor);

        await _doctorsService.UpdateDoctor(doctor);

        return NoContent();
    }

    [HttpGet("getWorkHistories")]
    public async Task<ActionResult<WorkHistoryReadDto>> GetWorkHistories()
    {
        var workHistories = await _doctorsService.GetWorkHistories();

        var workHistoriesReadDto = _mapper.Map<IEnumerable<WorkHistoryReadDto>>(workHistories);

        return Ok(workHistoriesReadDto);
    }

    [HttpPost("createWorkHistory")]
    public async Task<ActionResult<WorkHistoryReadDto>> CreateWorkHistory(WorkHistoryCreateDto workHistoryCreateDto)
    {
        //TODO it is needed to take logined doctor first and than create a workHistory for him
        var doctorId = 1;

        var workHistory = _mapper.Map<WorkHistory>(workHistoryCreateDto);
        workHistory.IdDoctor = doctorId;

        await _doctorsService.CreateWorkHistory(workHistory);

        var workHistoryReadDto = _mapper.Map<WorkHistoryReadDto>(workHistory);

        return Created(string.Empty, workHistoryReadDto);
    }

    [HttpDelete("removeWorkHistory")]
    public async Task<ActionResult> RemoveWorkHistory(int id)
    {
        var workHistory = await _doctorsService.GetWorkHistoryById(id);

        if (workHistory == null)
            return NotFound();

        await _doctorsService.DeleteWorkHistory(workHistory);

        return NoContent();
    }

    [HttpGet("getSpecialities")]
    public async Task<ActionResult<SpecialityReadDto>> GetSpecialities()
    {
        var specialities = await _doctorsService.GetSpecialities();

        var specialitiesReadDto = _mapper.Map<IEnumerable<SpecialityReadDto>>(specialities);

        return Ok(specialitiesReadDto);
    }

    [HttpPost("createSpeciality")]
    public async Task<ActionResult<SpecialityReadDto>> CreateSpeciality(SpecialityCreateDto specialityCreateDto)
    {
        var speciality = _mapper.Map<Speciality>(specialityCreateDto);

        await _doctorsService.CreateSpeciality(speciality);

        var specialityReadDto = _mapper.Map<SpecialityReadDto>(speciality);

        return Created(string.Empty, specialityReadDto);
    }

    [HttpDelete("removeSpeciality")]
    public async Task<ActionResult> RemoveSpeciality(int id)
    {
        var speciality = await _doctorsService.GetSpecialityById(id);

        if (speciality == null)
            return NotFound();

        await _doctorsService.DeleteSpeciality(speciality);

        return NoContent();
    }

    [HttpPost("addDoctorSpeciality")]
    public async Task<ActionResult> AddDoctorSpeciality(int doctorId, int specialityId)
    {
        var doctor = await _doctorsService.GetDoctorById(doctorId);
        if (doctor == null)
            return NotFound("Doctor not found");

        if (doctor.IdSpecialities.Any(s => s.IdSpeciality == specialityId))
            return BadRequest("Doctor already has this speciality");

        var speciality = await _doctorsService.GetSpecialityById(specialityId);
        if (speciality == null)
            return NotFound("Speciality not found");

        doctor.IdSpecialities.Add(speciality);
        await _doctorsService.UpdateDoctor(doctor);

        return Ok();
    }

    [HttpDelete("removeDoctorSpeciality")]
    public async Task<ActionResult> RemoveDoctorSpeciality(int doctorId, int specialityId)
    {
        var doctor = await _doctorsService.GetDoctorById(doctorId);
        if (doctor == null)
            return NotFound("Doctor not found");

        await _doctorsService.LoadDoctorSpecialities(doctor);
        var speciality = doctor.IdSpecialities.FirstOrDefault(s => s.IdSpeciality == specialityId);

        if (speciality == null)
            return NotFound("Speciality not found");

        doctor.IdSpecialities.Remove(speciality);
        await _doctorsService.UpdateDoctor(doctor);

        return Ok();
    }
}