using HospitalAPI.Utils.Extensions;
using HospitalAPI.Utils.Roles.Enums;
using Org.BouncyCastle.Security;

namespace HospitalAPI.Utils.Roles
{
    public static class RolesExtension
    {
        public static int ToRoleId(this EUserRole userRole)
        {
            if (!userRole.IsSingleFlag())
                throw new InvalidParameterException("User role should be a single flag to get user id");

            switch (userRole)
            {
                case EUserRole.Administrator:
                    return 1;
                case EUserRole.Doctor:
                    return 2;
                case EUserRole.Patient:
                    return 3;
                default:
                    throw new ArgumentOutOfRangeException(nameof(userRole), userRole, null);
            }
        }

        public static EUserRole ToUserRole(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return EUserRole.Administrator;
                case 2:
                    return EUserRole.Doctor;
                case 3:
                    return EUserRole.Patient;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleId), roleId, null);
            }
        }
    }
}