using ApiFornecedores.Repositories;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Fornecedores",
        Version = "v1"
    });
});

#endregion

#region Injeções de dependência

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped(map => new FornecedorRepository(connectionString!));

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
