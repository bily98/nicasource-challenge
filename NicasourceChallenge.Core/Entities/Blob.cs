using System.IO;

namespace NicasourceChallenge.Core.Entities;

public class Blob
{
    public string? Uri { get; set; }
    public string? Name { get; set; }
    public string? ContentType { get; set; }
    public Stream? Content { get; set; }
}