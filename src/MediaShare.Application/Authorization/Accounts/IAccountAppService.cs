using System.Threading.Tasks;
using Abp.Application.Services;
using MediaShare.Authorization.Accounts.Dto;

namespace MediaShare.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
