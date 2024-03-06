using AutoFixture;
using AutoMapper;
using Common.Interfaces;
using ExpectedObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Socializr.Models.ViewModels.Paging;
using SocializR.Common;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Models.ViewModels.Feed;
using SocializR.Services.Interfaces;
using SocializR.SPA.Server.Controllers;
using Xunit;

namespace UnitTests.PostsTests;
public class GetPostsTests
{
    private readonly IOptionsMonitor<AppSettings> _appSettings;
    private readonly IPostService _postService;
    private readonly ILogger<PostsController> _logger;
    private readonly ApplicationUnitOfWork _unitOfWork;
    private readonly IImageStorage _imageStorage;
    private readonly ILikeService _likeService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    private readonly PostsController _postsController;

    public GetPostsTests()
    {
        _appSettings = Substitute.For<IOptionsMonitor<AppSettings>>();
        _postService = Substitute.For<IPostService>();
        _logger = Substitute.For<ILogger<PostsController>>();
        //_unitOfWork = Substitute.For<ApplicationUnitOfWork>(Substitute.For<ApplicationDbContext>());
        _imageStorage = Substitute.For<IImageStorage>();
        _likeService = Substitute.For<ILikeService>();
        _mapper = Substitute.For<IMapper>();
        _fixture = new Fixture();
        _postsController = new PostsController(_logger, _appSettings, _imageStorage, _postService, _mapper, _likeService);
    }

    [Fact]
    public async Task When_GetPostsReceivesNullPagingParameters_ShouldFillUpDefaultPagingParametersAndReturnPosts()
    {
        //Arrange
        Guid userId = Guid.Empty;
        int? pageIndex = null;
        int? pageSize = null;

        var expectedPaging = new PostsPagingDto
        {
            PageIndex = 0,
            PageSize = 5
        }.ToExpectedObject();

        var posts = _fixture.CreateMany<PostViewModel>(5).ToList();
        var expectedPosts = posts.ToExpectedObject(); 

        _postService.GetPaginatedAsync(Arg.Any<PostsPagingDto>()).Returns(x => Task.FromResult(posts));

        //Act
        var result = (await _postsController.GetAsync(userId, pageIndex, pageSize)) as Ok<List<PostViewModel>>;

        //Assert
        expectedPosts.ShouldEqual(result);
    }
}
