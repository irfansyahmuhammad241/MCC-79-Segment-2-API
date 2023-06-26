namespace API.DTOS.Accounts
{
    public class NewAccountsDto
    {
        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public int OTP { get; set; }

        public bool IsUsed { get; set; }

        public DateTime ExpiredDate { get; set; }
    }
}
