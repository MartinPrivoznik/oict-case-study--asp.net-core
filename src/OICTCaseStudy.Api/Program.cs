using OICTCaseStudy.Api.Extensions;
using OICTCaseStudy.App;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices();
builder.Services.AddRestApiServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRestApi();

app.Run();