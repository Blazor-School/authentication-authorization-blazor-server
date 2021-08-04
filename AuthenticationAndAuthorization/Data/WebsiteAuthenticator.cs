using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Data
{
    public class WebsiteAuthenticator : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly SimulatedDataProviderService _dataProviderService;

        public WebsiteAuthenticator(ProtectedLocalStorage protectedLocalStorage, SimulatedDataProviderService dataProviderService)
        {
            _protectedLocalStorage = protectedLocalStorage;
            _dataProviderService = dataProviderService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();

            try
            {
                var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");

                if (storedPrincipal.Success)
                {
                    var user = JsonConvert.DeserializeObject<User>(storedPrincipal.Value);
                    var (userInDb, isLookUpSuccess) = LookUpUser(user.Username, user.Password);

                    if (isLookUpSuccess)
                    {
                        var identity = CreateIdentityFromUser(userInDb);
                        principal = new(identity);
                    }
                }
            }
            catch
            {

            }

            return new AuthenticationState(principal);
        }

        public async Task LoginAsync(LoginFormModel loginFormModel)
        {
            var (userInDatabase, isSuccess) = LookUpUser(loginFormModel.Username, loginFormModel.Password);
            var principal = new ClaimsPrincipal();

            if (isSuccess)
            {
                var identity = CreateIdentityFromUser(userInDatabase);
                principal = new ClaimsPrincipal(identity);
                await _protectedLocalStorage.SetAsync("identity", JsonConvert.SerializeObject(userInDatabase));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("identity");
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        private ClaimsIdentity CreateIdentityFromUser(User user)
        {
            var result = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, user.Username),
                new (ClaimTypes.Hash, user.Password),
                new (AdultRequirement.ClaimName, user.Age.ToString()),
                new ("IsPremiumMember", user.IsPremiumMember.ToString())
            }, "BlazorSchool");

            var roles = _dataProviderService.GetUserRoles(user);

            foreach (string role in roles)
            {
                result.AddClaim(new(ClaimTypes.Role, role));
            }

            return result;
        }

        private (User, bool) LookUpUser(string username, string password)
        {
            var result = _dataProviderService.Users.FirstOrDefault(u => username == u.Username && password == u.Password);

            return (result, result is not null);
        }
    }
}