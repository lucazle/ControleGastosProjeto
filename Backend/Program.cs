using ControleGastos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>      //registra o AppDbContext no sistema de DI por que vai precisar usar ele em outros momentos na aplicação.
    options.UseSqlite("Data Source=controle_gastos.db"));   //aqui eu escolhi o sqlite como banco e ele cria o arquivo que garante a persistência de dados.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
