namespace HospitalAPI.Utils.Extensions
{
    public static class EnumExtensions
    {
        public static string ConvertToString<TEnumFlags>(this TEnumFlags enumFlags, string separator)
            where TEnumFlags : struct, Enum
        {
            var roles = new List<string>();

            foreach (var eRoleValue in Enum.GetValues<TEnumFlags>())
            {
                if (enumFlags.HasFlag(eRoleValue))
                    roles.Add(eRoleValue.ToString());
            }

            return string.Join(separator, roles);
        }

        public static bool IsSingleFlag<TEnumFlags>(this TEnumFlags enumFlags)
            where TEnumFlags : struct, Enum
        {
            var enumValue = Convert.ToInt32(enumFlags);
            return (enumValue & (enumValue - 1)) == 0;
        }
    }
}