namespace SocializR.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class UserController : BaseController
{

    private readonly AdminService userAdminService;
    private readonly IHostEnvironment hostingEnvironment;
    private readonly AppSettings configuration;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;

    public UserController(IOptions<AppSettings> configuration, IMapper mapper, AdminService userAdminService, IHostEnvironment hostingEnvironment,
        UserManager<User> userManager, RoleManager<Role> roleManager)
        : base(mapper)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
        this.hostingEnvironment = hostingEnvironment;
        this.userAdminService = userAdminService;
        this.configuration = configuration.Value;
    }

    [HttpGet]
    public IActionResult Index(int? page)
    {
        var pageIndex = (page ?? 1) - 1;

        var users = userAdminService.GetAllUsers(pageIndex, configuration.UsersPerPage, out int totalUserCount);
        var model = new StaticPagedList<UserVM>(users, pageIndex + 1, configuration.UsersPerPage, totalUserCount);

        return View();
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
        var result = userAdminService.DeleteUser(id);

        if (!result)
        {
            return ForbidView();
        }

        return RedirectToAction("Index", new { page = 1 });
    }

    [HttpPost]
    public IActionResult Activate(string id)
    {
        var result = userAdminService.ActivateUser(id);

        if (!result)
        {
            return ForbidView();
        }

        return RedirectToAction("Index", new { page = 1 });
    }
}