using System.Text;
using API.DTOS.Accounts;
using API.Utilities;
using Newtonsoft.Json;
using IAccountRepository = Client.Contracts.IAccountRepository;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<GetAccountsDto, string>, IAccountRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;

        public AccountRepository(string request = "Accounts/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7294/api/")
            };
            this.request = request;
        }

        public async Task<ResponseHandler<string>> Login(LoginDto entity)
        {
            ResponseHandler<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseHandler<string>> Register(RegisterDto entity)
        {
            ResponseHandler<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Register", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
            }
            return entityVM;
        }

    }
}
