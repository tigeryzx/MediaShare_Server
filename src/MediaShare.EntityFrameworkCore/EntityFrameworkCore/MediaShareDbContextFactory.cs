using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MediaShare.Configuration;
using MediaShare.Web;

namespace MediaShare.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MediaShareDbContextFactory : IDesignTimeDbContextFactory<MediaShareDbContext>
    {
        public MediaShareDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MediaShareDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MediaShareDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MediaShareConsts.ConnectionStringName));

            return new MediaShareDbContext(builder.Options);
        }
    }
}
