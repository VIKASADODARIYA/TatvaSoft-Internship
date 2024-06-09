using Business_logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.JWTService;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(db => db.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddScoped<BALLogin>();
builder.Services.AddScoped<DALLogin>();
builder.Services.AddScoped<BALAdminUser>();
builder.Services.AddScoped<DALAdminUser>();
builder.Services.AddScoped<DALMissionSkill>();
builder.Services.AddScoped<BALMissionSkill>();
builder.Services.AddScoped<DALMissionTheme>();
builder.Services.AddScoped<BALMissionTheme>();
builder.Services.AddScoped<DALMission>();
builder.Services.AddScoped<BALMission>();
builder.Services.AddScoped<DALCommon>();
builder.Services.AddScoped<BALCommon>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
var app = builder.Build();
var env = app.Services.GetService<IWebHostEnvironment>();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        System.IO.Path.Combine(app.Environment.ContentRootPath, "WWWRoot", "UploadMissionImage", "Mission")),
    RequestPath = "/UploadMissionImage/Mission"
});

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
