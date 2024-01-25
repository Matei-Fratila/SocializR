namespace SocializR.Common;

public class AppSettings
{
    public int PostsPerPage { get; set; }
    public int UsersPerPage { get; set; }
    public int CommentsPerFirstPage { get; set; }
    public int CommentsPerPage { get; set; }
    public string FileUploadLocation { get; set; }
    public string FileUploadWriteLocation { get; set; }
    public string DefaultProfilePicture { get; set; }
    public string DefaultAlbumCover { get; set; }
    public string PostsAlbumName { get; set; }
    public string ProfilePicturesAlbumName { get; set; }
}
