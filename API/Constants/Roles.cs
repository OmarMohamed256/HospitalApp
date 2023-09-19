namespace HospitalApp.Constants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Receptionist = "Receptionist";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
        public static bool IsValidRole(string role)
        {
            return role switch
            {
                Admin or Receptionist or Doctor or Patient => true,
                _ => false,
            };
        }
    }

    public static class Polices
    {
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireReceptionistRole = "RequireReceptionistRole";
        public const string RequireDoctorRole = "RequireDoctorRole";
    }
}