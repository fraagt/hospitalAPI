namespace HospitalAPI.Models.ContactInfos
{
    public class ContactInfoReadDto
    {
        public int IdContactInfo { get; set; }

        public string Label { get; set; } = null!;

        public string ValueType { get; set; } = null!;

        public string ContactValue { get; set; } = null!;
    }
}