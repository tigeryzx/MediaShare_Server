using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MediaShare.Controllers
{
    public abstract class MediaShareControllerBase: AbpController
    {
        protected MediaShareControllerBase()
        {
            LocalizationSourceName = MediaShareConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
