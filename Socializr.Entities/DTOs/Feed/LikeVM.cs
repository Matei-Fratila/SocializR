﻿namespace SocializR.Entities.DTOs.Feed;

public class LikeVM
{
    public Guid UserId { get; set; }
    public string UserPhoto { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
