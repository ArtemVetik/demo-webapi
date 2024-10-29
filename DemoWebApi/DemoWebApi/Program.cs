using DemoWebApi.Extentions;
using DemoWebApi.Filters;
using Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(builder.Logging, builder.Environment);
builder.Services.AddControllers();
builder.Services.ConfigureSwager();
builder.Services.ConfigureCors();
builder.Services.ConfigureJwtConfig(builder.Configuration);
builder.Services.ConfigurePostgreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<MapService>();
builder.Services.AddScoped<SubscriptionFilter>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();