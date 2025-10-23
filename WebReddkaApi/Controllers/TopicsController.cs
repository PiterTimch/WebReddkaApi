using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebReddkaApi.Data;
using WebReddkaApi.Models.Topics;

namespace WebReddkaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController(AppDbContext context, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTopicsAsync()
    {
        var topics = await context.Topics
            .Where(t => t.ParentId == null)
            .OrderBy(t => t.Priority)
            .ProjectTo<TopicItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(topics);
    }
}
