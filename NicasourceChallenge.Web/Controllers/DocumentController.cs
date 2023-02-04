using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Core.Specifications.Documents;
using NicasourceChallenge.SharedKernel.Interfaces;
using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IAsyncRepository<Document> _documentRepository;
    private readonly IDocumentService _documentService;
    private readonly ILogger<DocumentController> _logger;
    private readonly IKTTheme _theme;

    public DocumentController(ILogger<DocumentController> logger, IKTTheme theme,
        IDocumentService documentService, IAsyncRepository<Document> documentRepository)
    {
        _logger = logger;
        _theme = theme;
        _documentService = documentService;
        _documentRepository = documentRepository;
    }

    [HttpPost]
    public async Task<IActionResult> GetDocuments()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
            .FirstOrDefault();
        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var result =
            await _documentService.GetDocumentsAsync(userId, sortColumn, sortColumnDirection, searchValue, skip,
                pageSize);

        if (result.IsSuccess)
        {
            var specification = new GetByUserIdSpec(userId);
            var recordsTotal = await _documentRepository.CountAsync(specification);

            var data = result.Value;
            var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

            return Ok(jsonData);
        }
        else
        {
            return BadRequest();
        }
    }
}