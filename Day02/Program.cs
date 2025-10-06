using Day02.Repository.Implementation;
using Day02.Repository.Interfaces;
using Day02.Services.Implementation;
using Day02.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add connection string
builder.Services.AddDbContext<Day02.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the Repository
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
// Register the image management service
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                                               Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
