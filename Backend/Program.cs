using ControleGastos.DAO;
using ControleGastos.DAO.Interfaces;
using ControleGastos.Services;
using ControleGastos.Services.Interfaces;
using ControleGastos.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//registra o AppDbContext no sistema de DI por que vai precisar usar ele em outros momentos na aplicação.
builder.Services.AddDbContext<AppDbContext>(options =>
//aqui eu escolhi o sqlite como banco e ele cria o arquivo que garante a persistência de dados.
    options.UseSqlite("Data Source=controle_gastos.db"));

//registra os daos, sempre que alguém solicitar a interface ele vai entregar a classe certa (configurando a di)
builder.Services.AddScoped<IPessoaDao, PessoaDao>();
builder.Services.AddScoped<ITransacaoDao, TransacaoDao>();

//registra os services, segue a mesma lógica de cima.
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

builder.Services.AddCors(options => {
    options.AddPolicy("PermitirFrontend", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("PermitirFrontend");

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
