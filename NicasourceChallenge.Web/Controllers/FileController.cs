using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NicasourceChallenge.Core.Dtos;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Core.Specifications.Files;
using NicasourceChallenge.SharedKernel.Interfaces;
using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private readonly IKTTheme _theme;
    private readonly IAsyncRepository<File> _fileRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public FileController(ILogger<FileController> logger, IKTTheme theme,
        IFileService fileService, IAsyncRepository<File> fileRepository,
        IMapper mapper)
    {
        _logger = logger;
        _theme = theme;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/file/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var result = await _fileService.DownloadFile(userId, id);

        if (!result.IsSuccess)
            return Problem(statusCode: 500);

        return File(result.Value.Content!, result.Value.ContentType!, result.Value.Name);
    }

    [HttpPost("/api/file/list")]
    public async Task<IActionResult> Get()
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
            await _fileService.GetFilesAsync(userId, sortColumn, sortColumnDirection, searchValue, skip,
                pageSize);

        if (!result.IsSuccess) 
            return Problem(statusCode: 500);

        var specification = new GetByUserIdSpec(userId);
        var recordsTotal = await _fileRepository.CountAsync(specification);

        var data = _mapper.Map<IEnumerable<Core.Entities.File>, IEnumerable<FileDto>>(result.Value);
        var jsonData = new {draw, recordsFiltered = recordsTotal, recordsTotal, data};

        return Ok(jsonData);

    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] BlobDto blobDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _fileService.SaveFile(User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
            blobDto.Description!, blobDto.File!);

        if (!result.IsSuccess)
            return Problem(statusCode: 500);

        return RedirectToAction("Index", "Cloud");
    }

    [HttpDelete("/api/file/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _fileService.DeleteFile(userId, id);

        if (result.IsSuccess)
            return Ok();
        return Problem(statusCode: 500);
    }
}