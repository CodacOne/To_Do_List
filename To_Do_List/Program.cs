using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using To_Do_List.Middlware;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapping;
using TodoApp.Application.Services;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;



var builder = WebApplication.CreateBuilder(args);


var jwtSettings = builder.Configuration.GetSection("Jwt");
Console.WriteLine(jwtSettings["Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});
//builder.Services.Configure<JWT>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<ITokenService, TokenService>();
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Todo API", Version = "v1" });

    // إضافة تعريف أمان Bearer JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "أدخل توكن JWT في هذا الحقل (يجب أن تبدأ بكلمة Bearer متبوعة بمسافة)",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // تعيين متطلبات الأمان على جميع الـ endpoints المحمية
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // عرض السجلات في الـ Console
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLogging();     // ✅ Middleware مخصص

app.UseAuthentication();     // ✅ JWT Authentication أولاً
app.UseAuthorization();      // ✅ ثم Authorization

app.MapControllers();

app.Run();

