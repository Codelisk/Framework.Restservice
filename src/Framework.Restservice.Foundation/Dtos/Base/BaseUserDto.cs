public class BaseUserDto : BaseDefaultIdDto
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }

    public bool IsUser(object userId)
    {
        return (Guid)userId == UserId;
    }
}