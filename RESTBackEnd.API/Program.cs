using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Configurations;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Repository;
using RESTBackEnd.API.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestBackEndDbContext>(optionsAction: options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("RESTBackEndDB"));
});

builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<RestBackEndDbContext>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Host.UseSerilog((context, logConf) => logConf.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();