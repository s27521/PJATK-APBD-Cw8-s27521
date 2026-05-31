using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw8_s27521.Infrastructure;
using PJATK_APBD_Cw8_s27521.Repository;
using PJATK_APBD_Cw8_s27521.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MasterContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();

builder.Services.AddScoped<IHospitalService,  HospitalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();