
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace LMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
