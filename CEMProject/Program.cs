using AllAboutLogging;
using APIs.Dependencies;
using AutoMapper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServicesLayer.ServiceHelper;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

AddDependencies.AddDependecy(builder);                                      // Dependencies Added

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);    // To Remove Cycle From Serilization 

var automapper = new MapperConfiguration(item => item.AddProfile(new Mapping()));   //   Adding AutoMaper Services

IMapper mapper = automapper.CreateMapper();

builder.Services.AddSingleton(mapper);

Auth0Authentication.AddAuthentication(builder);        // Adding Authentication Using JWT
Auth0Authentication.AddLocker(builder);



LogInfo.InitializeLoggers(builder.Configuration);


builder.Host.UseSerilog();
builder.Logging.AddSerilog();


builder.Services.AddControllers().AddJsonOptions(option =>
        option.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter()));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);





builder.Services.AddDbContext<CemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("databaseconn")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<LogMiddleware>();

app.Run();
