using Microsoft.EntityFrameworkCore;
using rapid_news_media_auth_api.Models;
using rapid_news_media_auth_api.Services;
using rapid_news_media_auth_api.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();

//Configure Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DBContext to the Services
builder.Services.AddDbContext<AuthDBContext>(opt => opt.UseInMemoryDatabase("AuthDB"));

// Configure dependecy injection for Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


    //Initialize Seed for In Memory Database 
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var dbcontext = scope.ServiceProvider.GetRequiredService<AuthDBContext>();
        dbcontext.Database.EnsureCreated();
    }

}



app.UseHttpsRedirection();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// custom auth middleware
app.UseMiddleware<BasicAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
