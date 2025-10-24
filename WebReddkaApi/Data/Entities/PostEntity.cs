using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebReddkaApi.Data.Entities;

[Table("tblPost")]
public class PostEntity
{
    [Column("id")]
    public long Id { get; set; }
    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;
    [Column("body")]
    [StringLength(500)]
    public string Body { get; set; } = string.Empty;
    [Column("image")]
    [StringLength(100)]
    public string? Image { get; set; }
    [Column("video")]
    [StringLength(100)]
    public string? Video { get; set; }
    [Column("video_url")]
    [StringLength(255)]
    public string? VideoUrl { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("topic_id")]
    [ForeignKey(nameof(Topic))]
    public long TopicId { get; set; }
    public TopicEntity Topic { get; set; } = null!;
}
