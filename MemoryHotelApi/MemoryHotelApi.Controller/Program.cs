using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.Mapping;
using MemoryHotelApi.BusinessLogicLayer.Services;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.Controller.Middlewares;
using MemoryHotelApi.DataAccessLayer.Contexts;
using MemoryHotelApi.DataAccessLayer.SeedData;
using MemoryHotelApi.DataAccessLayer.UnitOfWork;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(
    opt => opt.InvalidModelStateResponseFactory = context =>
    {
        var errorMessages = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        var response = new BaseResponseDto
        {
            StatusCode = StatusCodes.Status400BadRequest,
            IsSuccess = false,
            Errors = new Error
            {
                Messages = errorMessages
            }
        };

        return new BadRequestObjectResult(response)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    });

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = null;
});

// Register IMemoryCache
builder.Services.AddMemoryCache();

builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MemoryHotelApi", Version = "v1" });

    // Define Bearer Auth
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using Bearer scheme. Example: 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Config Forwarded Headers
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

#region Configure Db connection
// Register the DbContext on the service container
//builder.Services.AddDbContext<MemoryHotelApiDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
builder.Services.AddDbContext<MemoryHotelApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)));
});
#endregion

#region Configure JWT settings
// Configure JWT settings
var jwtSettings = new
{
    Key = builder.Configuration["Jwt:Key"] ?? Environment.GetEnvironmentVariable("Jwt__Key"),
    Issuer = builder.Configuration["Jwt:Issuer"] ?? Environment.GetEnvironmentVariable("Jwt__Issuer"),
    Audience = builder.Configuration["Jwt:Audience"] ?? Environment.GetEnvironmentVariable("Jwt__Audience"),
};

if (string.IsNullOrEmpty(jwtSettings.Key) || string.IsNullOrEmpty(jwtSettings.Issuer) || string.IsNullOrEmpty(jwtSettings.Audience))
{
    throw new InvalidOperationException("JWT settings are not configured properly.");
}

// Register JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});
#endregion

#region Configure Email settings
var emailSettings = new EmailSettings
{
    SmtpServer = builder.Configuration["EmailSettings:SmtpServer"] ?? Environment.GetEnvironmentVariable("EmailSettings__SmtpServer") ?? throw new InvalidOperationException("EmailSettings:SmtpServer is not configured."),
    SmtpPort = int.TryParse(builder.Configuration["EmailSettings:SmtpPort"] ?? Environment.GetEnvironmentVariable("EmailSettings__SmtpPort"), out var port) ? port : throw new InvalidOperationException("EmailSettings:SmtpPort is not configured or invalid."),
    SenderName = builder.Configuration["EmailSettings:SenderName"] ?? Environment.GetEnvironmentVariable("EmailSettings__SenderName") ?? throw new InvalidOperationException("EmailSettings:SenderName is not configured."),
    SenderEmail = builder.Configuration["EmailSettings:SenderEmail"] ?? Environment.GetEnvironmentVariable("EmailSettings__SenderEmail") ?? throw new InvalidOperationException("EmailSettings:SenderEmail is not configured."),
    Username = builder.Configuration["EmailSettings:Username"] ?? Environment.GetEnvironmentVariable("EmailSettings__Username") ?? throw new InvalidOperationException("EmailSettings:Username is not configured."),
    Password = builder.Configuration["EmailSettings:Password"] ?? Environment.GetEnvironmentVariable("EmailSettings__Password") ?? throw new InvalidOperationException("EmailSettings:Password is not configured.")
};
builder.Services.AddSingleton(emailSettings);
builder.Services.AddScoped<EmailSender>();
#endregion

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Register the mapping profile
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Register the password hasher
builder.Services.AddScoped<BcryptUtility>();

// Register the utilities
builder.Services.AddScoped<JwtUtility>();
builder.Services.AddScoped<StringUtility>();

#region Register service classes
// Register the services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ISubTourService, SubTourService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IConvenienceService, ConvenienceService>();
builder.Services.AddScoped<IRoomCategoryService, RoomCategoryService>();
builder.Services.AddScoped<IMembershipTierService, MembershipTierService>();
builder.Services.AddScoped<IMembershipTierBenefitService, MembershipTierBenefitService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBlogWriterService, BlogWriterService>();
builder.Services.AddScoped<IFoodCategoryService, FoodCategoryService>();
builder.Services.AddScoped<ISubFoodCategoryService, SubFoodCategoryService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodOrderHistoryService, FoodOrderHistoryService>();
builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
builder.Services.AddScoped<IMotorcycleRentalHistoryService, MotorcycleRentalHistoryService>();
builder.Services.AddScoped<IMotorcycleRentalHistoryDetailService, MotorcycleRentalHistoryDetailService>();
builder.Services.AddScoped<ICarBookingHistoryService, CarBookingHistoryService>();
builder.Services.AddScoped<ICarBookingHistoryDetailService, CarBookingHistoryDetailService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddHttpClient<IZaloService, ZaloService>();
#endregion

// Register the Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<DataSeeder>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedDataAsync();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MemoryHotelApi v1"));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionMiddleware();
}

app.UseAuthentication();
app.UseAuthorization();

// Serve static files
// Create the Images directory if it doesn't exist
string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/Images"
});

// Enable Forwarded Headers
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


app.MapControllers();

// Check if development environment
if (!app.Environment.IsDevelopment())
{
    app.Urls.Add("http://0.0.0.0:5000");
}

app.Run();
