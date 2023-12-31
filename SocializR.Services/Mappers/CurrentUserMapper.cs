﻿namespace SocializR.Services.Mappers;

public class CurrentUserMapper : Profile
{
    public CurrentUserMapper()
    {
        CreateMap<User, CurrentUser>().ForMember(u => u.ProfilePhoto, u => u.MapFrom(u => u.ProfilePhoto.FileName));
    }
}
