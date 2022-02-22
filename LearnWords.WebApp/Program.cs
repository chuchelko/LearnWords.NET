using LearnWords.DAL;
using LearnWords.DAL.Entities;
using LearnWords.DAL.Interfaces;
using LearnWords.DAL.Plugins;
using Microsoft.EntityFrameworkCore;
using MediatR;
using LearnWords.DAL.Core;
using LearnWords.Services.Services;
using LearnWords.Services.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(Program).Assembly, typeof(GetUser).Assembly);
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemory")));
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(AppMappingProfile).Assembly);
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Word>, Repository<Word>>();
builder.Services.AddScoped<ICoreRepository, CoreRepository>();
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
