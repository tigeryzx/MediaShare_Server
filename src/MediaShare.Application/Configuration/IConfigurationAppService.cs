using System.Threading.Tasks;
using MediaShare.Configuration.Dto;

namespace MediaShare.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
