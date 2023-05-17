using CrudInElasticSearchWithNest.Dtos.Settings;
using CrudInElasticSearchWithNest.Services;

var builder = WebApplication.CreateBuilder(args);
//-----------
builder.Services.AddScoped<IElasticsearchService, ElasticsearchService>();

//add options
builder.Services.AddOptions();
builder.Services.Configure<SiteSettings>(options => builder.Configuration.Bind(options));

//builder.Services.AddOptions<SiteSettings>().Bind(builder.Configuration.GetSection("ElasticsearchServer"));
// Add services to the container.

builder.Services.AddControllers();
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
