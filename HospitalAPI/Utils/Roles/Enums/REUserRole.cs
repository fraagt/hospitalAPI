namespace HospitalAPI.Utils.Roles.Enums
{
    [Flags]
    public enum EUserRole
    {
        Administrator = 0x0001,
        Doctor = 0x0002,
        Patient = 0x0004,
        All = Administrator | Doctor | Patient
    }
}