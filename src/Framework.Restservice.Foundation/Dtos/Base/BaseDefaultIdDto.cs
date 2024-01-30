[PrimaryKey(nameof(id))]
public class BaseDefaultIdDto : BaseBaseIdDto
{
    [Id]
    [Key]
    [JsonPropertyName("id")]
    public Guid id { get; set; }

    public override Guid GetId()
    {
        return id;
    }
}
