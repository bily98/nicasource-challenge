using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using NicasourceChallenge.Core.Entities;

namespace NicasourceChallenge.Core.Interfaces;

public interface IDocumentService
{
    /// <summary>
    ///     Use this method to get a list of files metadata.
    /// </summary>
    /// <param name="userId">The user id</param>
    /// <param name="sortColumn">The sort column</param>
    /// <param name="sortColumnDirection">The sort column direction</param>
    /// <param name="searchValue">The value to search</param>
    /// <param name="skip">The number of files to skip</param>
    /// <param name="pageSize">The number of files to take</param>
    /// <returns>List of files</returns>
    Task<Result<IEnumerable<Entities.File>>> GetDocumentsAsync(string userId, string sortColumn, string sortColumnDirection,
        string searchValue, int skip, int pageSize);

    /// <summary>
    ///     Use this method to save a file
    /// </summary>
    /// <param name="description">The file description</param>
    /// <param name="file">The file to save</param>
    /// <returns>The file saved</returns>
    Task<Result<Entities.File>> SaveDocument(string userId, string description, IFormFile file);

    /// <summary>
    ///     Use this method to delete a file
    /// </summary>
    /// <param name="userId">The user id</param>
    /// <param name="fileId">The file id</param>
    /// <returns>The result of action</returns>
    Task<Result> DeleteDocument(string userId, int fileId);
}