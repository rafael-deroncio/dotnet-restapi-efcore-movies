using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    string connection = builder.Configuration.GetConnectionString("MovieManiaConnection");
    options.UseNpgsql(connection);
});


WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.Run();
