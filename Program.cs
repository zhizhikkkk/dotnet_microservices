using Microsoft.EntityFrameworkCore;
using PlatformService;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(    opt=>
    opt.UseInMemoryDatabase("DefaultConnection"));
builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
PrepDb.PrepPopulation(app);
app.UseEndpoints(endpoints=>{
    endpoints.MapControllers();
});
app.Run();


