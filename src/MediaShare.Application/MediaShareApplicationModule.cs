using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Session;
using MediaShare.Authorization;
using MediaShare.Media;
using MediaShare.Media.Dto;
using MediaShare.Media.Pic;
using MediaShare.Media.Pic.Dto;

namespace MediaShare
{
    [DependsOn(
        typeof(MediaShareCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MediaShareApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MediaShareAuthorizationProvider>();

            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Video, VideoDto>()
                    .ForMember(x => x.Favorite, options => options.Ignore())
                    .ForMember(x => x.Images, options => options.MapFrom(x => x.Images))
                    .ForMember(x => x.ViewCount, options => options.MapFrom(x => x.GetViewCount()))
                    .ForMember(x => x.PlayCount, options => options.MapFrom(x => x.GetPlayCount()));

                config.CreateMap<VideoFavRelation, FavoriteDto>()
                    .ForMember(x => x.CategoryName, options => options.MapFrom(x => x.Favorite.CategoryName))
                    .ForMember(x => x.VideoCount, options => options.MapFrom(x => x.Favorite.GetVideoCount()))
                    .ForMember(x => x.Id, options => options.MapFrom(x => x.Favorite.Id));

                config.CreateMap<Favorite, FavoriteDto>()
                    .ForMember(x => x.VideoCount, options => options.MapFrom(x => x.GetVideoCount()));

                config.CreateMap<PicTagRelation, PicTagDto>()
                    .ForMember(x => x.Name, options => options.MapFrom(x => x.Tag.Name))
                    .ForMember(x => x.Id, options => options.MapFrom(x => x.Tag.Id));

                config.CreateMap<Picture, PicDto>()
                    .ForMember(x => x.Tags, options => options.MapFrom(x => x.PicTagRelation));
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MediaShareApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );


        }
    }
}
