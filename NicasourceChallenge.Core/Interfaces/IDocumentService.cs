using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Interfaces;

public interface IDocumentService
{
    /// <summary>
    ///     Use this method to get a list of documents metadata.
    /// </summary>
    /// <param name="userId">The user's id</param>
    /// <returns>List of documents</returns>
    Task<Result<IEnumerable<Document>>> GetDocumentsAsync(string userId);

    /// <summary>
    ///     Use this method to save a document
    /// </summary>
    /// <param name="description">The document description</param>
    /// <param name="file">The file to save</param>
    /// <returns>The document saved</returns>
    Task<Result<Document>> SaveDocument(string userId, string description, IFormFile file);
}