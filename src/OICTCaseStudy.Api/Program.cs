using OICTCaseStudy.Api.Extensions;
using OICTCaseStudy.App;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices();
builder.Services.AddRestApiServices(builder.Configuration); 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRestApi();

app.Run();