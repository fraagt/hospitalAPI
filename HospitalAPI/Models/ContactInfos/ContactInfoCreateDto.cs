namespace HospitalAPI.Models.ContactInfos
{
    public class ContactInfoCreateDto
    {
        public string Label { get; set; } = null!;

        public string ValueType { get; set; } = null!;

        public string ContactValue { get; set; } = null!;
    }
}