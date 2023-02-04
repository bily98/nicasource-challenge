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
    private readonly IAsyncRepository<Document> _documentRepository;
    private readonly IAzureStorageRepository<Blob, BlobResponse> _azureStorageRepository;

    public DocumentService(IAsyncRepository<Document> documentRepository, IAzureStorageRepository<Blob, BlobResponse> azureStorageRepository)
    {
        _documentRepository = documentRepository;
        _azureStorageRepository = azureStorageRepository;
    }

    public async Task<Result<IEnumerable<Document>>> GetDocumentsAsync(string userId)
    {
        try
        {
            var specification = new GetByUserIdSpec(userId);
            var documents = await _documentRepository.ListAsync(specification);

            return Result<IEnumerable<Document>>.Success(documents);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Document>>.Error(ex.Message);
        }
    }

    public async Task<Result<Document>> SaveDocument(string userId, string description, IFormFile file)
    {
        try
        {
            var validator = new DocumentValidator();

            var blobResponse = await _azureStorageRepository.UploadAsync(file);

            var document = new Document
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
                return Result<Document>.Error(blobResponse.Status);

            var validationResult = await validator.ValidateAsync(document);

            if (!validationResult.IsValid)
                return Result<Document>.Invalid(validationResult.AsErrors());

            document = await _documentRepository.AddAsync(document);

            return Result<Document>.Success(document);
        }
        catch (Exception ex)
        {
            return Result<Document>.Error(ex.Message);
        }
    }
}