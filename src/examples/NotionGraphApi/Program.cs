using System.Text.Json.Serialization;
using NotionGraphApi;
using NotionGraphApi.Json;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Configuration.AddJsonFile("appsettings.Debug.json", true, true);
#endif

DependencyInjection.Configure(builder);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(j =>
    {
        j.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        var jsonConverters = j.JsonSerializerOptions.Converters;
        jsonConverters.Add(new FieldValueSetConverter());
        jsonConverters.Add(new ObjectFieldValueConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();