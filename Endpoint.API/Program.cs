using Domain.AppServices;
using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Entities;
using Domain.Services;
using Infra.DataAccess.Repos.Dapper;
using Infra.DataAccess.Repos.EF;
using Infra.Db.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();

#region Register DbContext
builder.Services.AddDbContext<FinalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
#endregion


#region Register Application Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductAppService, ProductAppService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountAppService, AccountAppService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
//builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
//builder.Services.AddScoped<IAdminAppService, AdminAppService>();

builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<ISellerService, SellerService>();
//builder.Services.AddScoped<ISellerAppService, SellerAppService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartAppService, CartAppService>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();


builder.Services.AddScoped<IBoothRepository, BoothRepository>();
builder.Services.AddScoped<IBoothService, BoothService>();
builder.Services.AddScoped<IBoothAppService, BoothAppService>();


builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();


builder.Services.AddScoped<IProductDapperRepository, ProductDapperRepository>();
#endregion




#region Register Identity And JWT

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<FinalContext>()
            .AddDefaultTokenProviders().AddRoles<IdentityRole<int>>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //password options
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 2;

    //lockout options 
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters =
                                new TokenValidationParameters()
                                {
                                    ValidateIssuer = true,
                                    ValidIssuer = builder.Configuration["Issuer"],
                                    ValidateAudience = true,
                                    ValidAudience = builder.Configuration["Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                                                              (builder.Configuration["IssuerSigningKey"])),
                                    ValidateIssuerSigningKey = true,
                                };


});






builder.Services.Configure<JWTConfiguration>(builder.Configuration);

#endregion


#region Config CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyFrontEnd", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
    });

});

#endregion


#region Register Swagger
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = " Identity api",
        Description = "ASP.NET Core 7.0 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}

                }
            });

});
#endregion

#endregion 


var app = builder.Build();



#region Request and response pipline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


app.UseCors();

//app.UseExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion