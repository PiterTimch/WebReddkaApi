namespace WebReddkaApi.Models.Posts;

public class PostCreateModel
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public IFormFile? Video { get; set; }
    public string? VideoUrl { get; set; }
    public long TopicId { get; set; }
}
