using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MediaShare.EntityFrameworkCore
{
    public static class MediaShareDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MediaShareDbContext> builder, string connectionString)
        {
            builder
                .UseLazyLoadingProxies()
                .UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MediaShareDbContext> builder, DbConnection connection)
        {
            builder
                .UseLazyLoadingProxies()
                .UseMySql(connection);
        }
    }
}
