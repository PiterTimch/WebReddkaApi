using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPIDB.Helpers;
using WebReddkaApi.Data.Entities;
using WebReddkaApi.Models.Seeders;

namespace WebReddkaApi.Data;

public static class AppDbSeeder
{
    public static void Seed(this IApplicationBuilder applicationBuilder)
    {
        var scopeFactory = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        if (!context.Topics.Any()) 
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "JsonData", "Topics.json");
                var topicsData = File.ReadAllText(path);

                var topics = JsonConvert.DeserializeObject<List<TopicSeederModel>>(topicsData);
            
                int parentIndex = 1;

                foreach (var topic in topics!)
                {
                    var newTopic = new TopicEntity
                    {
                        Name = topic.Name, Priority = parentIndex++,
                        Description = topic.Description,
                        UrlSlug = SlugHelper.Slugify(topic.Name)
                    };

                    context.Topics.Add(newTopic);
                    context.SaveChanges();

                    int childIndex = 1;

                    foreach (var child in topic.Children!)
                    {
                        var newChildTopic = new TopicEntity
                        {
                            Name = child.Name, Priority = childIndex++,
                            UrlSlug = SlugHelper.Slugify(child.Name),
                            ParentId = newTopic.Id
                        };
                        context.Topics.Add(newChildTopic);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }
}
