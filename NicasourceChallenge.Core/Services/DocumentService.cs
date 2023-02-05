using System.IO;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Microsoft.AspNetCore.Http;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.Core.Specifications.Documents;
using NicasourceChallenge.SharedKernel.Interfaces;

namespace NicasourceChallenge.Core.Services;

public class DocumentService : IDocumentService
{
    private readonly IAzureStorageRepository<Blob, BlobResponse> _azureStorageRepository;
    private readonly IAsyncRepository<Entities.File> _fileRepository;

    public DocumentService(IAsyncRepository<Entities.File> fileRepository,
        IAzureStorageRepository<Blob, BlobResponse> azureStorageRepository)
    {
        _fileRepository = fileRepository;
        _azureStorageRepository = azureStorageRepository;
    }

    public async Task<Result<IEnumerable<Entities.File>>> GetDocumentsAsync(string userId, string sortColumn,
        string sortColumnDirection, string searchValue, int skip, int pageSize)
    {
        try
        {
            var specification =
                new GetByUserIdSpec(userId, sortColumn, sortColumnDirection, searchValue, skip, pageSize);
            var files = await _fileRepository.ListAsync(specification);

            return Result<IEnumerable<Entities.File>>.Success(files);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Entities.File>>.Error(ex.Message);
        }
    }

    public async Task<Result<Entities.File>> SaveDocument(string userId, string description, IFormFile file)
    {
        try
        {
            var validator = new DocumentValidator();

            var blobResponse = await _azureStorageRepository.UploadAsync(file);

            var file = new Entities.File
            {
                Name = blobResponse.Blob.Name,
                Description = description,
                Url = blobResponse.Blob.Uri,
                Size = file.OpenReadStream().Length,
                UserId = new Guid(userId),
                Format = Path.GetExtension(file.FileName),
                Created = DateTime.Now
            };

            if (blobResponse.Error)
                return Result<Entities.File>.Error(blobResponse.Status);

            var validationResult = await validator.ValidateAsync(file);

            if (!validationResult.IsValid)
                return Result<Entities.File>.Invalid(validationResult.AsErrors());

            file = await _fileRepository.AddAsync(file);

            return Result<Entities.File>.Success(file);
        }
        catch (Exception ex)
        {
            return Result<Entities.File>.Error(ex.Message);
        }
    }

    public async Task<Result> DeleteDocument(string userId, int fileId)
    {
        try
        {
            var specification = new GetByUserIdAndDocumentIdSpec(userId, fileId);
            var file = await _fileRepository.FirstOrDefaultAsync(specification);

            if (file == null)
                return Result.Error($"Document with id {fileId} does not exist");

            await _fileRepository.DeleteAsync(file!);

            var response = await _azureStorageRepository.DeleteAsync(file!.Name!);

            return !response.Error ? Result.Success() : Result.Error(response.Status);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}