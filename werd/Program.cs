using werd.Model;
using werd.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

[assembly: ApiController]
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; //cors�ܼ�
var builder = WebApplication.CreateBuilder(args);

//cors���]�w
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173");
                      });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<DBContext>();
builder.Services.AddTransient<HomeRepository>();
builder.Services.AddTransient<SupplierRepository>();


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

//app.UseHttpsRedirection();//�ҥ�https

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);//�s�W CORS �����n��

app.Run();
