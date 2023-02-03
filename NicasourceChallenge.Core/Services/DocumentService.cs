using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using NicasourceChallenge.Core.Entities;
using NicasourceChallenge.Core.Interfaces;
using NicasourceChallenge.SharedKernel.Interfaces;

namespace NicasourceChallenge.Core.Services;

public class DocumentService : IDocumentService
{
    private readonly IAsyncRepository<Document> _documentRepository;

    public DocumentService(IAsyncRepository<Document> documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<Result<IEnumerable<Document>>> GetDocumentsAsync()
    {
        try
        {
            var documents = await _documentRepository.ListAsync();

            return Result<IEnumerable<Document>>.Success(documents);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Document>>.Error(ex.Message);
        }
    }

    public async Task<Result<Document>> SaveDocument(Document document)
    {
        try
        {
            var validator = new DocumentValidator();

            var result = validator.Validate(document);

            if (!result.IsValid)
                return Result<Document>.Invalid(result.AsErrors());

            document = await _documentRepository.AddAsync(document);

            return Result<Document>.Success(document);
        }
        catch (Exception ex)
        {
            return Result<Document>.Error(ex.Message);
        }
    }
}