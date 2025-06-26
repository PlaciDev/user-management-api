using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UseManagementApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura a ConnectionString
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilita o cacheamento
builder.Services.AddMemoryCache();

// Ignora ciclos e nulos
builder.
    Services
    .AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
})
    // Desativa a validaçao automática do ModelState 
    .ConfigureApiBehaviorOptions(options =>
        options.SuppressModelStateInvalidFilter = true);



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
