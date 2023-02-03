using Ardalis.Result;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Interfaces;

public interface IDocumentService
{
    /// <summary>
    ///     Use this method to get a list of documents metadata.
    /// </summary>
    /// <returns>Returns a list of documents</returns>
    Task<Result<IEnumerable<Document>>> GetDocumentsAsync();

    /// <summary>
    ///     Use this method to save a document metadata
    /// </summary>
    /// <param name="document">The document metadata</param>
    /// <returns>Returns the document saved</returns>
    Task<Result<Document>> SaveDocument(Document document);
}