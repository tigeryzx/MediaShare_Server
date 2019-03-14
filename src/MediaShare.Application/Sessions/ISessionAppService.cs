using System.Threading.Tasks;
using Abp.Application.Services;
using MediaShare.Sessions.Dto;

namespace MediaShare.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
