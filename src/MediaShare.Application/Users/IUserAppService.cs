using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MediaShare.Roles.Dto;
using MediaShare.Users.Dto;

namespace MediaShare.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
