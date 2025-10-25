using Microsoft.EntityFrameworkCore;
using WebReddkaApi.Data;
using WebReddkaApi.Helpers;
using WebReddkaApi.Interfaces;
using WebReddkaApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IMediaService, MediaService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

await app.InitializeAppAsync();

app.Seed();

app.Run();
