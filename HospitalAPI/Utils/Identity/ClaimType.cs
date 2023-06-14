namespace HospitalAPI.Utils.Identity
{
    public class ClaimType
    {
        private const string DEFAULT_BASE_NAMESPACE = "hospital/identity/claims/";

        public static ClaimType IdPatient = new("idPatient");
        public static ClaimType IdDoctor = new("idDoctor");

        private ClaimType(string type)
        {
            _type = type;
        }
        
        private readonly string _type;

        public override string ToString()
        {
            return DEFAULT_BASE_NAMESPACE + _type;
        }

        public static implicit operator string(ClaimType claimType)
        {
            return claimType.ToString();
        }
    }
}