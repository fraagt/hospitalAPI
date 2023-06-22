﻿using AutoMapper;
using HospitalAPI.Database;
using HospitalAPI.Models.Allergens;
using HospitalAPI.Models.Allergies;
using HospitalAPI.Models.Attachments;
using HospitalAPI.Models.Blood;
using HospitalAPI.Models.Diagnoses;
using HospitalAPI.Models.MedicalCards;
using HospitalAPI.Models.MedicalRecords;
using HospitalAPI.Models.Prescriptions;
using HospitalAPI.Services.MedicalCards;
using HospitalAPI.Utils.Roles.Attributes;
using HospitalAPI.Utils.Roles.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalCardsController : ControllerBase
    {
        private readonly IMedicalCardsService _medicalCardsService;
        private readonly IMapper _mapper;

        public MedicalCardsController(
            IMedicalCardsService medicalCardsService,
            IMapper mapper
        )
        {
            _medicalCardsService = medicalCardsService;
            _mapper = mapper;
        }

        [RoleAuthorize(EUserRole.Administrator | EUserRole.Doctor)]
        [HttpGet("getMedicalCards")]
        public async Task<ActionResult<MedicalCardReadDto>> GetMedicalCards()
        {
            var medicalCards = await _medicalCardsService.GetMedicalCards();

            var medicalCardReadDtos = medicalCards.Select(MapToReadDto);

            return Ok(medicalCardReadDtos);
        }

        private MedicalCardReadDto MapToReadDto(MedicalCard medicalCard)
        {
            return new MedicalCardReadDto
            {
                IdMedicalCard = medicalCard.IdMedicalCard,
                IdPatient = medicalCard.IdPatient,
                Blood = _mapper.Map<BloodReadDto>(medicalCard.IdBloodNavigation),
                Allergies = medicalCard.Allergies.Select(MapToReadDto),
                MedicalRecords = medicalCard.MedicalRecords.Select(MapToReadDto)
            };
        }

        private AllergyReadDto MapToReadDto(Allergy allergy)
        {
            return new AllergyReadDto
            {
                IdAllergy = allergy.IdMedicalCard,
                Allergen = _mapper.Map<AllergenReadDto>(allergy.IdAllergenNavigation),
                IdMedicalCard = allergy.IdMedicalCard,
                SeverityLevel = allergy.SeverityLevel,
                DiagnosisDate = allergy.DiagnosisDate
            };
        }

        private MedicalRecordReadDto MapToReadDto(MedicalRecord medicalRecord)
        {
            return new MedicalRecordReadDto
            {
                IdMedicalRecord = medicalRecord.IdMedicalRecord,
                IdMedicalCard = medicalRecord.IdMedicalCard,
                IdDoctor = medicalRecord.IdDoctor,
                Note = medicalRecord.Note,
                Prescriptions = _mapper.Map<IEnumerable<PrescriptionReadDto>>(medicalRecord.Prescriptions),
                Diagnoses = _mapper.Map<IEnumerable<DiagnosisReadDto>>(medicalRecord.Diagnoses),
                Attachments = _mapper.Map<IEnumerable<AttachmentReadDto>>(medicalRecord.Attachments)
            };
        }

        [HttpGet("getMedicalCard/{id}")]
        public async Task<ActionResult<MedicalCardReadDto>> GetMedicalCard(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var medicalCard = await _medicalCardsService.GetMedicalCardById(id);
            if (medicalCard == null)
                return NotFound();

            var medicalCardReadDto = MapToReadDto(medicalCard);

            return Ok(medicalCardReadDto);
        }

        [RoleAuthorize(EUserRole.Patient | EUserRole.Doctor)]
        [HttpPut("changeMedicalCard/{id}")]
        public async Task<ActionResult> ChangeMedicalCard(int id, MedicalCardUpdateDto medicalCardUpdateDto)
        {
            var medicalCard = await _medicalCardsService.GetMedicalCardById(id);

            if (medicalCard == null)
                return NotFound();

            _mapper.Map(medicalCardUpdateDto, medicalCard);

            await _medicalCardsService.UpdateMedicalCard(medicalCard);

            return NoContent();
        }

        [HttpGet("getBloodTypes")]
        public async Task<ActionResult<BloodReadDto>> GetBloodTypes()
        {
            var bloodTypes = await _medicalCardsService.GetBloods();

            var bloodTypesReadDto = _mapper.Map<IEnumerable<BloodReadDto>>(bloodTypes);

            return Ok(bloodTypesReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createBloodType")]
        public async Task<ActionResult<BloodReadDto>> CreateBloodType(BloodCreateDto bloodTypeCreateDto)
        {
            var bloodType = _mapper.Map<Blood>(bloodTypeCreateDto);

            await _medicalCardsService.CreateBlood(bloodType);

            var bloodTypeReadDto = _mapper.Map<BloodReadDto>(bloodType);

            return Created(string.Empty, bloodTypeReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeBloodType")]
        public async Task<ActionResult> RemoveBloodType(int id)
        {
            var bloodType = await _medicalCardsService.GetBloodById(id);

            if (bloodType == null)
                return NotFound();

            await _medicalCardsService.DeleteBlood(bloodType);

            return NoContent();
        }

        [HttpGet("getMedicalRecords")]
        public async Task<ActionResult<MedicalRecordReadDto>> GetMedicalRecords()
        {
            var medicalRecords = await _medicalCardsService.GetMedicalRecords();

            var medicalRecordReadDtos = _mapper.Map<IEnumerable<MedicalRecordReadDto>>(medicalRecords);

            return Ok(medicalRecordReadDtos);
        }

        [HttpGet("getMedicalRecord/{id}")]
        public async Task<ActionResult<MedicalRecordReadDto>> GetMedicalRecord(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var medicalRecord = await _medicalCardsService.GetMedicalRecordById(id);
            if (medicalRecord == null)
                return NotFound();

            var medicalRecordReadDto = MapToReadDto(medicalRecord);

            return Ok(medicalRecordReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createMedicalRecord")]
        public async Task<ActionResult<MedicalRecordReadDto>> CreateMedicalRecord(
            MedicalRecordCreateDto medicalRecordCreateDto)
        {
            //TODO take from logined user(doctor)
            var doctorId = 1;

            var medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordCreateDto);
            medicalRecord.IdDoctor = doctorId;

            await _medicalCardsService.CreateMedicalRecord(medicalRecord);

            var medicalRecordReadDto = _mapper.Map<MedicalRecordReadDto>(medicalRecord);

            return Created(string.Empty, medicalRecordReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPut("changeMedicalRecord/{id}")]
        public async Task<ActionResult> ChangeMedicalRecord(int id, MedicalRecordUpdateDto medicalRecordUpdateDto)
        {
            var medicalRecord = await _medicalCardsService.GetMedicalRecordById(id);

            if (medicalRecord == null)
                return NotFound();

            _mapper.Map(medicalRecordUpdateDto, medicalRecord);

            await _medicalCardsService.UpdateMedicalRecord(medicalRecord);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeMedicalRecord")]
        public async Task<ActionResult> RemoveMedicalRecord(int id)
        {
            var medicalRecord = await _medicalCardsService.GetMedicalRecordById(id);

            if (medicalRecord == null)
                return NotFound();

            if (medicalRecord.HasDiagnoses)
                return BadRequest("Medical record has depended diagnoses");

            if (medicalRecord.HasPrescriptions)
                return BadRequest("Medical record has depended prescriptions");

            await _medicalCardsService.DeleteMedicalRecord(medicalRecord);

            return NoContent();
        }

        [HttpGet("getPrescriptions")]
        public async Task<ActionResult<PrescriptionReadDto>> GetPrescriptions()
        {
            var prescriptions = await _medicalCardsService.GetPrescriptions();

            var prescriptionReadDtos = _mapper.Map<IEnumerable<PrescriptionReadDto>>(prescriptions);

            return Ok(prescriptionReadDtos);
        }

        [HttpGet("getPrescription/{id}")]
        public async Task<ActionResult<PrescriptionReadDto>> GetPrescription(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var prescription = await _medicalCardsService.GetPrescriptionById(id);
            if (prescription == null)
                return NotFound();

            var prescriptionReadDto = _mapper.Map<PrescriptionReadDto>(prescription);

            return Ok(prescriptionReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createPrescription")]
        public async Task<ActionResult<PrescriptionReadDto>> CreatePrescription(
            PrescriptionCreateDto prescriptionCreateDto)
        {
            var prescription = _mapper.Map<Prescription>(prescriptionCreateDto);

            await _medicalCardsService.CreatePrescription(prescription);

            var prescriptionReadDto = _mapper.Map<PrescriptionReadDto>(prescription);

            return Created(string.Empty, prescriptionReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPut("changePrescription/{id}")]
        public async Task<ActionResult> ChangePrescription(int id, PrescriptionUpdateDto prescriptionUpdateDto)
        {
            var prescription = await _medicalCardsService.GetPrescriptionById(id);

            if (prescription == null)
                return NotFound();

            _mapper.Map(prescriptionUpdateDto, prescription);

            await _medicalCardsService.UpdatePrescription(prescription);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removePrescription")]
        public async Task<ActionResult> RemovePrescription(int id)
        {
            var prescription = await _medicalCardsService.GetPrescriptionById(id);

            if (prescription == null)
                return NotFound();

            await _medicalCardsService.DeletePrescription(prescription);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpGet("getDiagnoses")]
        public async Task<ActionResult<DiagnosisReadDto>> GetDiagnoses()
        {
            var diagnoses = await _medicalCardsService.GetDiagnoses();

            var diagnosisReadDtos = _mapper.Map<IEnumerable<DiagnosisReadDto>>(diagnoses);

            return Ok(diagnosisReadDtos);
        }

        [HttpGet("getDiagnosis/{id}")]
        public async Task<ActionResult<DiagnosisReadDto>> GetDiagnosis(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var diagnosis = await _medicalCardsService.GetDiagnosisById(id);
            if (diagnosis == null)
                return NotFound();

            var diagnosisReadDto = _mapper.Map<DiagnosisReadDto>(diagnosis);

            return Ok(diagnosisReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createDiagnosis")]
        public async Task<ActionResult<DiagnosisReadDto>> CreateDiagnosis(DiagnosisCreateDto diagnosisCreateDto)
        {
            var diagnosis = _mapper.Map<Diagnosis>(diagnosisCreateDto);

            await _medicalCardsService.CreateDiagnosis(diagnosis);

            var diagnosisReadDto = _mapper.Map<DiagnosisReadDto>(diagnosis);

            return Created(string.Empty, diagnosisReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPut("changeDiagnosis/{id}")]
        public async Task<ActionResult> ChangeDiagnosis(int id, DiagnosisUpdateDto diagnosisUpdateDto)
        {
            var diagnosis = await _medicalCardsService.GetDiagnosisById(id);

            if (diagnosis == null)
                return NotFound();

            _mapper.Map(diagnosisUpdateDto, diagnosis);

            await _medicalCardsService.UpdateDiagnosis(diagnosis);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeDiagnosis")]
        public async Task<ActionResult> RemoveDiagnosis(int id)
        {
            var diagnosis = await _medicalCardsService.GetDiagnosisById(id);

            if (diagnosis == null)
                return NotFound();

            await _medicalCardsService.DeleteDiagnosis(diagnosis);

            return NoContent();
        }

        [HttpGet("getAttachments")]
        public async Task<ActionResult<AttachmentReadDto>> GetAttachments()
        {
            var attachments = await _medicalCardsService.GetAttachments();

            var attachmentReadDtos = _mapper.Map<IEnumerable<AttachmentReadDto>>(attachments);

            return Ok(attachmentReadDtos);
        }

        [HttpGet("getAttachment/{id}")]
        public async Task<ActionResult<AttachmentReadDto>> GetAttachment(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var attachment = await _medicalCardsService.GetAttachmentById(id);
            if (attachment == null)
                return NotFound();

            var attachmentReadDto = _mapper.Map<AttachmentReadDto>(attachment);

            return Ok(attachmentReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createAttachment")]
        public async Task<ActionResult<AttachmentReadDto>> CreateAttachment(AttachmentCreateDto attachmentCreateDto)
        {
            var attachment = _mapper.Map<Attachment>(attachmentCreateDto);

            await _medicalCardsService.CreateAttachment(attachment);

            var attachmentReadDto = _mapper.Map<AttachmentReadDto>(attachment);

            return Created(string.Empty, attachmentReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPut("changeAttachment/{id}")]
        public async Task<ActionResult> ChangeAttachment(int id, AttachmentUpdateDto attachmentUpdateDto)
        {
            var attachment = await _medicalCardsService.GetAttachmentById(id);

            if (attachment == null)
                return NotFound();

            _mapper.Map(attachmentUpdateDto, attachment);

            await _medicalCardsService.UpdateAttachment(attachment);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeAttachment")]
        public async Task<ActionResult> RemoveAttachment(int id)
        {
            var attachment = await _medicalCardsService.GetAttachmentById(id);

            if (attachment == null)
                return NotFound();

            await _medicalCardsService.DeleteAttachment(attachment);

            return NoContent();
        }

        [HttpGet("getAllergies")]
        public async Task<ActionResult<AllergyReadDto>> GetAllergies()
        {
            var allergies = await _medicalCardsService.GetAllergies();

            var allergyReadDtos = _mapper.Map<IEnumerable<AllergyReadDto>>(allergies);

            return Ok(allergyReadDtos);
        }

        [HttpGet("getAllergy/{id}")]
        public async Task<ActionResult<AllergyReadDto>> GetAllergy(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var allergy = await _medicalCardsService.GetAllergyById(id);
            if (allergy == null)
                return NotFound();

            var allergyReadDto = _mapper.Map<AllergyReadDto>(allergy);

            return Ok(allergyReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createAllergy")]
        public async Task<ActionResult<AllergyReadDto>> CreateAllergy(AllergyCreateDto allergyCreateDto)
        {
            var allergy = _mapper.Map<Allergy>(allergyCreateDto);

            await _medicalCardsService.CreateAllergy(allergy);

            var allergyReadDto = _mapper.Map<AllergyReadDto>(allergy);

            return Created(string.Empty, allergyReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPut("changeAllergy/{id}")]
        public async Task<ActionResult> ChangeAllergy(int id, AllergyUpdateDto allergyUpdateDto)
        {
            var allergy = await _medicalCardsService.GetAllergyById(id);

            if (allergy == null)
                return NotFound();

            _mapper.Map(allergyUpdateDto, allergy);

            await _medicalCardsService.UpdateAllergy(allergy);

            return NoContent();
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeAllergy")]
        public async Task<ActionResult> RemoveAllergy(int id)
        {
            var allergy = await _medicalCardsService.GetAllergyById(id);

            if (allergy == null)
                return NotFound();

            await _medicalCardsService.DeleteAllergy(allergy);

            return NoContent();
        }

        [HttpGet("getAllergens")]
        public async Task<ActionResult<AllergenReadDto>> GetAllergens()
        {
            var allergens = await _medicalCardsService.GetAllergens();

            var allergensReadDto = _mapper.Map<IEnumerable<AllergenReadDto>>(allergens);

            return Ok(allergensReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpPost("createAllergen")]
        public async Task<ActionResult<AllergenReadDto>> CreateAllergen(AllergenCreateDto allergenCreateDto)
        {
            var allergen = _mapper.Map<Allergen>(allergenCreateDto);

            await _medicalCardsService.CreateAllergen(allergen);

            var allergenReadDto = _mapper.Map<AllergenReadDto>(allergen);

            return Created(string.Empty, allergenReadDto);
        }

        [RoleAuthorize(EUserRole.Doctor)]
        [HttpDelete("removeAllergen")]
        public async Task<ActionResult> RemoveAllergen(int id)
        {
            var allergen = await _medicalCardsService.GetAllergenById(id);

            if (allergen == null)
                return NotFound();

            await _medicalCardsService.DeleteAllergen(allergen);

            return NoContent();
        }
    }
}