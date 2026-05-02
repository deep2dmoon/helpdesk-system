using System.Text;
using helpdesk.Application;
using helpdesk.Controller.Auth;
using helpdesk.Controller.HelpdeskEndpoint;
using helpdesk.infrastructure;
using helpdesk.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//inject Helpdesk Service into Scoped Lifetime
builder.Services.AddSignalR();
builder.Services.AddScoped<Helpdesk>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<Messenger>();
builder.Services.AddScoped<AccountMaker>();
builder.Services.AddScoped<FileUploader>();


builder.Host.UseSerilog((context, services, config) =>
{
    config.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services);
});

builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // registering the database context here!!


builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", (options) =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetConnectionString("SecKey")!)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseSerilogRequestLogging(); // log every incoming http requests
app.UseAuthentication();
app.UseAuthorization();


app.MapHelpdeskEndpoint();
app.MapAuthenticationEndpoints();
app.Run();
