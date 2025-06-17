using Microsoft.EntityFrameworkCore;
using Emprestimos.Infra;
using Emprestimos.Infra; // onde está o GeradorDeServicos

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var serviceProvider = builder.Services.BuildServiceProvider();
GeradorDeServicos.RegistrarProvider(serviceProvider);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
