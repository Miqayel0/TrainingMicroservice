using Discount.API.NpgSqlConnections;
using Discount.API.Repositories;
using Discount.API.Extensions;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddScoped<INpgSqlConnection>(op =>
    new SqlConnection(op.GetRequiredService<IConfiguration>()
    .GetValue<string>("DatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MigrateDatabase<Program>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
