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
    /// <param name="sortColumn">The sort column</param>
    /// <param name="sortColumnDirection">The sort column direction</param>
    /// <param name="searchValue">The value to search</param>
    /// <param name="skip">The number of documents to skip</param>
    /// <param name="pageSize">The number of documents to take</param>
    /// <returns>List of documents</returns>
    Task<Result<IEnumerable<Document>>> GetDocumentsAsync(string userId, string sortColumn, string sortColumnDirection, string searchValue, int skip, int pageSize);

    /// <summary>
    ///     Use this method to save a document
    /// </summary>
    /// <param name="description">The document description</param>
    /// <param name="file">The file to save</param>
    /// <returns>The document saved</returns>
    Task<Result<Document>> SaveDocument(string userId, string description, IFormFile file);
}