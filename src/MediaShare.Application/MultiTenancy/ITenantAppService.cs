using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MediaShare.MultiTenancy.Dto;

namespace MediaShare.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
