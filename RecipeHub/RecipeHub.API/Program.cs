using System.Reflection;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RecipeHub.API.AutoMapperProfiles;
using RecipeHub.API.SwaggerConfig;
using RecipeHub.ClassLib.Database.EfStructures;
using RecipeHub.ClassLib.Database.Infrastructure;
using RecipeHub.ClassLib.Database.Repository.Implementation;
using RecipeHub.ClassLib.Service;
using RecipeHub.ClassLib.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowAnyOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(SwaggerOptionsConfigurer.Configure);

using (var context = new AppDbContextFactory().CreateDbContext(new [] { builder.Configuration.GetConnectionString("connectionString") }))
{
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

List<Assembly> assemblies = new List<Assembly> { typeof(RecipeReadRepository).Assembly , typeof(RecipeProfile).Assembly };
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepositoryModule()
    {
        RepositoryAssemblies = assemblies,
        Namespace = "Repository"
    });
    containerBuilder.RegisterModule(new AppDbContextModule(builder.Configuration.GetConnectionString("connectionString")));
    containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
    containerBuilder.RegisterAutoMapper(propertiesAutowired: false, assemblies.ToArray());
    containerBuilder.RegisterType<RecipeService>().As<IRecipeService>().InstancePerLifetimeScope();
    //containerBuilder.Populate(builder.Services);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeHub v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
