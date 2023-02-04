using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NicasourceChallenge.Core.Dtos;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.App.Controllers;

[Authorize]
public class CloudController : Controller
{
    private readonly ILogger<CloudController> _logger;
    private readonly IKTTheme _theme;
    private readonly IDocumentService _documentService;

    public CloudController(ILogger<CloudController> logger, IKTTheme theme,
        IDocumentService documentService)
    {
        _logger = logger;
        _theme = theme;
        _documentService = documentService;
    }


    [HttpGet("/")]
    [HttpGet("/cloud")]
    public async Task<IActionResult> Index()
    {
        return View(_theme.GetPageView("Cloud", "Index.cshtml"));
    }

    [HttpPost]
    public async Task<IActionResult> Upload(BlobDto blobDto)
    {
        var result = await _documentService.SaveDocument(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, blobDto.Description!, blobDto.File!);

        return RedirectToAction("Index", "Cloud");
    }
}