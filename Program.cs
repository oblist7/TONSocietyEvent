using Microsoft.EntityFrameworkCore;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new VisualStudioCredential());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("DefaultConnection"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/event", async (ApplicationDbContext dbContext) =>
{
    // Получаем данные событий из базы данных
    var events = await dbContext.Events.ToListAsync();
    return Results.Ok(events);
})
.WithName("GetEvents")
.WithOpenApi();

app.Run();
