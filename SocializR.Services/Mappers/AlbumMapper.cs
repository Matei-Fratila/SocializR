﻿namespace SocializR.Services.Mappers;

public class AlbumMapper : Profile
{
    public AlbumMapper()
    {
        CreateMap<Media, MediaViewModel>();

        CreateMap<Album, AlbumViewModel>()
            .ForMember(a => a.NrOfImages, opt => opt.MapFrom(u => u.Media.Count))
            .ForMember(a => a.CoverId, opt => opt.MapFrom(u => u.Media.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault()))
            .ForMember(a => a.CoverFilePath, opt => opt.MapFrom(u => u.Media.OrderByDescending(c => c.Id).Select(c => c.FileName).FirstOrDefault()))
            .ForMember(a => a.CreatedDate, opt => opt.MapFrom(u => u.CreatedDate.TimeAgo()))
            .ForMember(a => a.LastModifiedDate, opt => opt.MapFrom(u => u.LastModifiedDate.HasValue ? u.LastModifiedDate.Value.TimeAgo() : String.Empty));
    }
}
