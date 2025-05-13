using Lindmark.Api.Brreg;
using Lindmark.Api.Brreg.Repository;
using Lindmark.Api.Brreg.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var apiSection = builder.Configuration.GetSection("ApiOptions");
var apiOptions = apiSection.Get<ApiOptions>()!;
builder.Services.Configure<ApiOptions>(options => apiSection.Bind(options));
builder.Services.AddSingleton(_ => apiOptions);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ICompanyClient, CompanyClient>(client =>
{
    client.BaseAddress = new Uri(apiOptions.BrregUrl);
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddScoped<ICompanyService, CompanyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();