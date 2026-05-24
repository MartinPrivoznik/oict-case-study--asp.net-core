using OICTCaseStudy.Api.Extensions;
using OICTCaseStudy.App;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddRestApiServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseRestApi();

app.Run();