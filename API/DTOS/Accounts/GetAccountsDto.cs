using API.Utilities;

namespace API.DTOS.Accounts
{
    public class GetAccountsDto
    {
        public Guid guid { get; set; }
      
        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsUsed { get; set; }


    
    }
}
