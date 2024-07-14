using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(optitons =>
{
    optitons.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactMoviesCorsPolicy", builder => 
     builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddExceptionHandler<ExceptionHandler>();

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

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseCors("ReactMoviesCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
