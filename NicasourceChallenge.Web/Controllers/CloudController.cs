using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Core.Specifications.Documents;
using NicasourceChallenge.SharedKernel.Interfaces;
using NicasourceChallenge.Web._keenthemes.libs;
using System.Security.Claims;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.App.Controllers;

[Authorize]
public class CloudController : Controller
{
    private readonly ILogger<CloudController> _logger;
    private readonly IKTTheme _theme;
    private readonly IAsyncRepository<File> _fileRepository;

    public CloudController(ILogger<CloudController> logger, IKTTheme theme,
        IAsyncRepository<File> fileRepository)
    {
        _logger = logger;
        _theme = theme;
        _fileRepository = fileRepository;
    }


    [HttpGet("/")]
    [HttpGet("/cloud")]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var specification = new GetByUserIdSpec(userId);
        ViewBag.TotalFiles = await _fileRepository.CountAsync(specification);

        return View(_theme.GetPageView("Cloud", "Index.cshtml"));
    }
}