
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SafeTravelApp.Configuration;
using SafeTravelApp.Data;
using SafeTravelApp.Helpers;
using SafeTravelApp.Repositories;
using SafeTravelApp.Services;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace SafeTravelApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SafeTravelAppDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddScoped<IApplicationService, ApplicationService>();
            builder.Services.AddRepositories();

            builder.Services.AddScoped(provider =>
              new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile(new MapperConfig());
              })
          .CreateMapper());

            //Add Authentication
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.IncludeErrorDetails = true;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = false,
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        RequireExpirationTime = false,
            //        ValidateLifetime = false,
            //        //return new JsonWebToken(token); in .NET 8
            //        /// Override the default token signature validation an do NOT validate the signature
            //        /// Just return the token
            //        SignatureValidator = (token, validator) => { return new JwtSecurityToken(token); }
            //    };
            //});


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //key from "Authentication" as has een defined by appsetting.json : "SecretKey"
                var jwtSettings = builder.Configuration.GetSection("Authentication");

                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = "https://localhost:5000",

                    ValidateAudience = false,
                    ValidAudience = "https://localhost:4200",

                    ValidateLifetime = true, // ensure not expired

                    ValidateIssuerSigningKey = true,

                    // US2BlUEkNFMy8yl0t6subj3cJKhAm7kQ7Asg7-mSwq0
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    //.GetBytes("US2BlUEkNFMy8yl0t6subj3cJKhAm7kQ7Asg7-mSwq0"))
                    .GetBytes(jwtSettings["SecretKey"]!)),
                    //.GetBytes(builder.Configuration["Authentication: SecretKey"]!))
                    RoleClaimType = ClaimTypes.Role
                };
            });

            // Add services to the container.

            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });

            builder.Services.AddCors(options => {
                options.AddPolicy("AngularClient",
                    b => b.WithOrigins("http://localhost:4200") // Assuming Angular runs on localhost:4200
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            /// System.Text.JSON
            /*builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                // Adding a converter for string enums
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });*/

            // If NewtonSoft would be used for json serialization / deserialization
            // We have to add the NuGet dependencies and the following config

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            ///Swagger
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            /*For Testing, like Postman*/
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            /*For Documentation*/
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Safe Travel API", Version = "v1" });
                // Non-nullable reference are properly documented
                options.SupportNonNullableReferenceTypes();
                //extra config for authorization comments
                //Not only what but also who can do AND if can not what error receives
                options.OperationFilter<AuthorizeOperationFilter>();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT"
                    });
            });

            //builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseExceptionHandler();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.MapControllers();

            app.Run(); ;
        }
    }
}
