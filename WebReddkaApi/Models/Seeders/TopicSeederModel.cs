namespace WebReddkaApi.Models.Seeders;

public class TopicSeederModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TopicChildSeederModel[]? Children { get; set; }

}
