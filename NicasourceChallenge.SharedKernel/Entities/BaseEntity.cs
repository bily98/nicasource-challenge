using Newtonsoft.Json;

namespace NicasourceChallenge.SharedKernel.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public string? PartitionKey { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}