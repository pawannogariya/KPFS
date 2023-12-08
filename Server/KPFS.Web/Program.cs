using KPFS.Business.Models;
using KPFS.Business.Services.Implementations;
using KPFS.Business.Services.Interfaces;
using KPFS.Common;
using KPFS.Data;
using KPFS.Data.Entities;
using KPFS.Data.Repositories;
using KPFS.Web;
using KPFS.Web.AppSettings;
using KPFS.Web.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json")
                 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                 .AddJsonFile($"appsettings.Local.json", optional: true);

var configuration = builder.Configuration;

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<KpfsDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<KpfsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(
    opts => opts.SignIn.RequireConfirmedEmail = true
    );

// logging setup
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Error);
builder.Host.UseNLog();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfigurationDto>();
builder.Services.AddSingleton(emailConfig);

builder.Services.Configure<ApplicationSettings>(configuration.GetSection("App"));
builder.Services.Configure<JwtSettings>(configuration.GetSection("JWT"));

MasterData? masterData = new MasterData();
XmlSerializer serializer = new XmlSerializer(typeof(MasterData));
using (FileStream fileStream = new FileStream("MasterData.xml", FileMode.Open))
{
    masterData = serializer.Deserialize(fileStream) as MasterData;
}

builder.Services.AddSingleton(masterData);

// services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();

// repositories
builder.Services.AddTransient<FundHouseRepository>();
builder.Services.AddTransient<FundRepository>();
builder.Services.AddTransient<BankAccountRepository>();
builder.Services.AddTransient<FundManagerRepository>();


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await DataSeeder.Initialize(scope);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
