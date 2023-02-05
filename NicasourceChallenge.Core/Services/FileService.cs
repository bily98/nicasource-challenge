using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Microsoft.AspNetCore.Http;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Core.Specifications.Files;
using NicasourceChallenge.SharedKernel.Interfaces;

namespace NicasourceChallenge.Core.Services;

public class FileService : IFileService
{
    private readonly IAzureStorageRepository<Blob, BlobResponse> _azureStorageRepository;
    private readonly IAsyncRepository<File> _fileRepository;

    public FileService(IAsyncRepository<File> fileRepository,
        IAzureStorageRepository<Blob, BlobResponse> azureStorageRepository)
    {
        _fileRepository = fileRepository;
        _azureStorageRepository = azureStorageRepository;
    }

    public async Task<Result<IEnumerable<File>>> GetFilesAsync(string userId, string sortColumn,
        string sortColumnDirection, string searchValue, int skip, int pageSize)
    {
        try
        {
            var specification =
                new GetByUserIdSpec(userId, sortColumn, sortColumnDirection, searchValue, skip, pageSize);
            var files = await _fileRepository.ListAsync(specification);

            return Result<IEnumerable<File>>.Success(files);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<File>>.Error(ex.Message);
        }
    }

    public async Task<Result<File>> SaveFile(string userId, string description, IFormFile formfile)
    {
        try
        {
            var validator = new FileValidator();

            var blobResponse = await _azureStorageRepository.UploadAsync(formfile);

            var file = new File
            {
                Name = blobResponse.Blob.Name,
                Description = description,
                Url = blobResponse.Blob.Uri,
                Size = formfile.OpenReadStream().Length,
                UserId = new Guid(userId),
                Format = System.IO.Path.GetExtension(formfile.FileName),
                Created = DateTime.Now
            };

            if (blobResponse.Error)
                return Result<File>.Error(blobResponse.Status);

            var validationResult = await validator.ValidateAsync(file);

            if (!validationResult.IsValid)
                return Result<File>.Invalid(validationResult.AsErrors());

            file = await _fileRepository.AddAsync(file);

            return Result<File>.Success(file);
        }
        catch (Exception ex)
        {
            return Result<File>.Error(ex.Message);
        }
    }

    public async Task<Result> DeleteFile(string userId, int fileId)
    {
        try
        {
            var specification = new GetByUserIdAndFileIdSpec(userId, fileId);
            var file = await _fileRepository.FirstOrDefaultAsync(specification);

            if (file == null)
                return Result.Error($"File with id {fileId} does not exist");

            await _fileRepository.DeleteAsync(file!);

            var response = await _azureStorageRepository.DeleteAsync(file!.Name!);

            return !response.Error ? Result.Success() : Result.Error(response.Status);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result<Blob>> DownloadFile(string userId, int fileId)
    {
        try
        {
            var specification = new GetByUserIdAndFileIdSpec(userId, fileId);
            var file = await _fileRepository.FirstOrDefaultAsync(specification);

            if (file == null)
                return Result.Error($"File with id {fileId} does not exist");

            var blob = await _azureStorageRepository.DownloadAsync(file!.Name!);

            return Result<Blob>.Success(blob);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}