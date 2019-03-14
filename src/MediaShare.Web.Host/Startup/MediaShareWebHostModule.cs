using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MediaShare.Configuration;

namespace MediaShare.Web.Host.Startup
{
    [DependsOn(
       typeof(MediaShareWebCoreModule))]
    public class MediaShareWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MediaShareWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MediaShareWebHostModule).GetAssembly());
        }
    }
}
