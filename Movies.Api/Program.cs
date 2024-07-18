using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Api.MappingProfiles;
using Movies.Api.Middleware;
using Movies.Api.Repositories;
using Movies.Api.Services;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Npgsql;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
dataSourceBuilder.UseNetTopologySuite();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DatabaseContext>(optitons =>
{
    optitons.UseNpgsql(dataSource,
        sqlOptions => sqlOptions.UseNetTopologySuite());
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
builder.Services.AddScoped<IMovieTheaterService, MovieTheaterService>();
builder.Services.AddScoped<IFileStorageService, StorageService>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//builder.Services.AddSingleton(provider => 
//    new MapperConfiguration(cfg =>
//    {
//        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
//        cfg.AddProfile(new MovieTheaterProfile(geometryFactory));
//        //cfg.AddProfile(new MappingProfiles.GenreProfile());
//        //cfg.AddProfile(new MappingProfiles.ActorProfile());
//    }).CreateMapper());

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactMoviesCorsPolicy", builder => 
     builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//builder.Services.AddExceptionHandler<ExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("ReactMoviesCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
