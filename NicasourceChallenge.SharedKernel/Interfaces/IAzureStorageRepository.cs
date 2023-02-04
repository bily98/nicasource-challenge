using Microsoft.AspNetCore.Http;

namespace NicasourceChallenge.SharedKernel.Interfaces;

public interface IAzureStorageRepository<T, TResponse> where T : class
{
    /// <summary>
    ///     This method deleted a file with the specified filename
    /// </summary>
    /// <param name="blobFilename">Filename</param>
    /// <returns>Blob with status</returns>
    Task<TResponse> DeleteAsync(string blobFilename);

    /// <summary>
    ///     This method downloads a file with the specified filename
    /// </summary>
    /// <param name="blobFilename">Filename</param>
    /// <returns>Blob</returns>
    Task<T> DownloadAsync(string blobFilename);

    /// <summary>
    ///     This method returns a list of all files located in the container
    /// </summary>
    /// <returns>Blobs in a list</returns>
    Task<List<T>> ListAsync();

    /// <summary>
    ///     This method uploads a file submitted with the request
    /// </summary>
    /// <param name="file">File for upload</param>
    /// <returns>Blob with status</returns>
    Task<TResponse> UploadAsync(IFormFile file);
}