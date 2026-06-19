using Asp.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiVersioning(o =>
    {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
        o.ReportApiVersions = true;
        o.ApiVersionReader = ApiVersionReader.Combine(new MediaTypeApiVersionReader("ver"));
    })
    .AddApiExplorer(o =>
    {
        o.GroupNameFormat = "'v'VVV";
        o.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "An example of API using versioning"
    });
});

var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
.HasDeprecatedApiVersion(new ApiVersion(1, 0))
.HasApiVersion(new ApiVersion(2, 0))
.HasApiVersion(new ApiVersion(3, 0))
.ReportApiVersions()
.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapGet("api/v{version:apiVersion}/returnvalue", () =>
{
    return Results.Ok("Some value v1");
})
.WithName("ReturValueV1")
.WithApiVersionSet(apiVersionSet)
.HasApiVersion(new ApiVersion(1, 0))
;

app.MapGet("api/v{version:apiVersion}/returnvalue", () =>
{
    return Results.Ok("Some value v2");
})
.WithName("ReturValueV2")
.WithApiVersionSet(apiVersionSet)
.HasApiVersion(new ApiVersion(2, 0));

app.MapGet("api/v{version:apiVersion}/returnvalue", () =>
{
    return Results.Ok("Some value v3");
})
.WithName("ReturValueV3")
.WithApiVersionSet(apiVersionSet)
.HasApiVersion(new ApiVersion(3, 0));

app.Run();
