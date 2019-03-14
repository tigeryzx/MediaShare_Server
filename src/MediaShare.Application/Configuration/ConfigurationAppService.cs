using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MediaShare.Configuration.Dto;

namespace MediaShare.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MediaShareAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
