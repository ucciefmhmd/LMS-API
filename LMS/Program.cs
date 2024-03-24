
using LMS.BL.Interface;
using LMS.BL.Mapper;
using LMS.BL.Repository;
using LMS.DAL.Database;
using LMS.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // DataBase
            builder.Services.AddDbContext<LMSContext>(a =>
                a.UseSqlServer(builder.Configuration.GetConnectionString("con")));

            // Dependency Injection
            builder.Services.AddScoped<IStudentRep , StudentRep>();
            builder.Services.AddScoped<IInstructorRep , InstructorRep>();
            builder.Services.AddScoped<ICourseRep , CourseRep>();
            builder.Services.AddScoped<IEventRep , EventRep>();
            builder.Services.AddScoped<IExamRep , ExamRep>();
            builder.Services.AddScoped<IQuestionRep , QuestionRep>();
            builder.Services.AddScoped<IUserRep , UserRep>();

            // Add Auto Mapper
            builder.Services.AddAutoMapper(typeof(DomainProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Authentication
            var jwtOptions = builder.Configuration.GetSection("jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(jwtOptions);
            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                    };
                });

            // Add authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("InstructorPolicy", policy =>
                    policy.Requirements.Add(new RoleRequirement("instructor")));

                options.AddPolicy("StudentPolicy", policy =>
                    policy.RequireRole("student"));

                options.AddPolicy("StudentAndInstructorPolicy", policy =>
                    policy.RequireRole("student", "instructor"));

                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("admin"));

                options.AddPolicy("SubAdminPolicy", policy =>
                    policy.RequireRole("subadmin"));

                options.AddPolicy("AdminAndSubAdminPolicy", policy =>
                    policy.RequireRole("admin", "subadmin"));
            });

            builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("Allownce", crosPolicy =>
                {
                    crosPolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("Allownce");

            app.MapControllers();

            app.Run();
        }
    }
}
