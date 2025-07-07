using Application.Interfaces.IServices;
using Application.Services;
using Infrastructure.Repository.Common;
using Microsoft.Data.SqlClient;
using RepoDb.DbHelpers;
using RepoDb.DbSettings;
using RepoDb.StatementBuilders;
using RepoDb;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Application.Interfaces.IRepository.Common;
using Application.Interfaces.IRepository;
using Infrastructure.Repository;
using Infrastructure.Service;
using Domain.Interface.IAuth;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudentManagementApi.Middleware;
using System.Text.Json;
using Infrastructure.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Configuración de RepoDB
RepoDb.GlobalConfiguration.Setup().UseSqlServer();

var dbSetting = new SqlServerDbSetting();

DbSettingMapper.Add<SqlConnection>(dbSetting, true);
DbHelperMapper.Add<SqlConnection>(new SqlServerDbHelper(), true);
StatementBuilderMapper.Add<SqlConnection>(new SqlServerStatementBuilder(dbSetting), true);
#endregion

#region Inyeccion de dependencias
builder.Services.AddControllers().AddDomainConverters();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

#endregion

#region Configuracion CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion

#region Configuracion Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Student Management API",
        Description = "API for managing students and subjects",
        Contact = new OpenApiContact
        {
            Name = "Tu nombre",
            Email = "tu-correo@example.com",
            Url = new Uri("https://tusitio.com"),
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
#endregion

var app = builder.Build();

#region Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Management API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAnyOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

