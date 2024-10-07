
using CarDealerDemo.Configuration;
using CarDealerDemo.Database;
using CarDealerDemo.Filters;
using CarDealerDemo.Validators;
using FluentValidation;

namespace CarDealerDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddJwtRegistration(builder.Configuration);
            builder.Services.AddSwaggerRegistration(builder.Configuration);
            builder.Services.AddServicesRegistration();


            //Register Validators
            builder.Services.AddValidatorsFromAssemblyContaining<AddCarValidator>();



            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ValidationFilter>();
            });


            var app = builder.Build();

            // Initialize the database
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
                dbInitializer.Initialize();
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
        }
    }
}
