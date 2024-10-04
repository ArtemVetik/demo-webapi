using DemoWebApi.Extentions;
using DemoWebApi.Filters;
using Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(builder.Logging, builder.Environment);
builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwager();
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demi API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();