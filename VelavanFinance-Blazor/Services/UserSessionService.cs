namespace VelavanFinanceERP.Services
{
    public class UserSessionService
    {
        public string UserName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string AdminRights { get; set; } = string.Empty; // 'Y' for Admin, 'N' for Normal, 'S' for Super Admin
        public bool IsLoggedIn { get; set; } = false;

        // Method to set user details after login
        public void SetUserDetails(string userName, string branchName, string companyName, string adminRights)
        {
            UserName = userName;
            BranchName = branchName;
            CompanyName = companyName;
            AdminRights = adminRights;
            IsLoggedIn = true;
        }

        // Method to clear details on logout
        public void Logout()
        {
            UserName = string.Empty;
            BranchName = string.Empty;
            CompanyName = string.Empty;
            AdminRights = string.Empty;
            IsLoggedIn = false;
        }
    }
}