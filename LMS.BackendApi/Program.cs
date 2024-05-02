using LMS.BackendApi.Data;
using LMS.BackendApi.Repository.Implementation;
using LMS.BackendApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("AppCon");

builder.Services.AddDbContext<LmsDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));






builder.Services.AddScoped<IAuthor, AuthorRepo>();
builder.Services.AddScoped<IBook, BookRepo>();
builder.Services.AddScoped<IMember, MemberRepo>();
builder.Services.AddScoped<IBorrowedBooks, BorrowedBookRepo>();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
