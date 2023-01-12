using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SoftTest.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SoftTestContextConnection") ?? throw new InvalidOperationException("Connection string 'SoftTestContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>();
//builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(opt =>
//{
//    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Description = "Standart Autjorization hearder using the bearer scheme (\"bearer {token}\")",
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });
//});
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(opt =>
//    {
//        opt.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
//            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
