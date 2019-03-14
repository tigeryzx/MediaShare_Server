using MediaShare.VideoScan.Model;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoScan.DB
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class VScanDB : DbContext
    {
        public VScanDB()
            : base("VScan")
        {
           
        }

        public DbSet<ResDir> ResDirs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Setting> MSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ResDir>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Video>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Video>()
                .HasMany(x => x.Images)
                .WithRequired(x => x.Video)
                .HasForeignKey(x => x.VideoId);




            modelBuilder
                .Entity<Image>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder
                .Entity<Image>()
                .HasRequired(x => x.Video)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.VideoId);


            modelBuilder
                .Entity<Setting>()
                .ToTable("msettings")
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
