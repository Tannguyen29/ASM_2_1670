namespace ASM_2_1670.Areas.Admin.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }

        public string? UserPhone { get; set; }

        public string? UserAddress { get; set; }
        public string? UserRole { get; set; }
    }
}
