namespace WebReddkaApi.Models.Topics;

public class TopicItemModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string UrlSlug { get; set; } = null!;
    public string? Description { get; set; }
    public int Priority { get; set; }
    public long? ParentId { get; set; }
    public List<TopicItemModel>? Children { get; set; }
}
