using System;
using System.IO;
using Project2_Server.Data;

// TEMP Getting connection string, update when deploying
string DB_connectionString = GetEnvironmentVariable("MYSQLCONNSTR_Woodcutter_DB");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<INTERFACE_SQL_Customer>(sp => new SQL_Customer(DB_connectionString, sp.GetRequiredService<ILogger<SQL_Customer>>()));
builder.Services.AddSingleton<INTERFACE_SQL_Employee>(sp => new SQL_Employee(DB_connectionString, sp.GetRequiredService<ILogger<SQL_Employee>>()));
builder.Services.AddSingleton<INTERFACE_SQL_Order>(sp => new SQL_Order(DB_connectionString, sp.GetRequiredService<ILogger<SQL_Order>>()));
builder.Services.AddSingleton<INTERFACE_SQL_Project>(sp => new SQL_Project(DB_connectionString, sp.GetRequiredService<ILogger<SQL_Project>>()));
builder.Services.AddSingleton<INTERFACE_SQL_LinkingTable>(sp => new SQL_LinkingTable(DB_connectionString, sp.GetRequiredService<ILogger<SQL_LinkingTable>>()));

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
