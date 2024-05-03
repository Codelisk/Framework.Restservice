[PrimaryKey(nameof(id))]
public class BaseDefaultIdDto : BaseBaseIdDto
{
    [Id]
    [System.ComponentModel.DataAnnotations.Key]
    [JsonPropertyName("id")]
    public Guid id { get; set; }

    public override Guid GetId()
    {
        return id;
    }
}
