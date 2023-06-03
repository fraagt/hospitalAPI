using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Doctors;
using HospitalAPI.Models.WorkHistories;
using HospitalAPI.Services.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController : ControllerBase
{
    private readonly ILogger<DoctorController> _logger;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public DoctorController(ILogger<DoctorController> logger,
        IDoctorService doctorService,
        IMapper mapper)
    {
        _logger = logger;
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet("getDoctors")]
    public async Task<ActionResult<DoctorReadDto>> GetDoctors()
    {
        var doctors = await _doctorService.GetDoctors();

        var doctorReadDtos = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);

        return Ok(doctorReadDtos);
    }

    [HttpGet("getDoctor/{id}")]
    public async Task<ActionResult<DoctorReadDto>> GetDoctors(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID");

        var doctor = await _doctorService.GetDoctorById(id);

        if (doctor == null)
            return NotFound();

        var doctorReadDto = _mapper.Map<DoctorReadDto>(doctor);

        return Ok(doctorReadDto);
    }

    [HttpPut("changeDoctor/{id}")]
    public async Task<ActionResult> ChangeDoctor(int id, DoctorUpdateDto doctorUpdateDto)
    {
        var doctor = await _doctorService.GetDoctorById(id);

        if (doctor == null)
            return NotFound();
        
        _mapper.Map(doctorUpdateDto, doctor);

        await _doctorService.UpdateDoctor(doctor);

        return NoContent();
    }

    [HttpGet("getWorkHistories")]
    public async Task<ActionResult<WorkHistoryReadDto>> GetWorkHistories()
    {
        var workHistories = await _doctorService.GetWorkHistories();

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
        
        await _doctorService.CreateWorkHistory(workHistory);

        var workHistoryReadDto = _mapper.Map<WorkHistoryReadDto>(workHistory);

        return Created(string.Empty, workHistoryReadDto);
    }
}