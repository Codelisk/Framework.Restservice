
public abstract class BaseBaseIdDto : ICreatedAt
{
    public DateTime CreatedAt { get; set; }

    [GetId]
    public abstract Guid GetId();
}