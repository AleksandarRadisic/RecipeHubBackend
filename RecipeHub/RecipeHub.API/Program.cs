using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false,
    };
});

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
    containerBuilder.RegisterType<JwtGenerator>().As<IJwtGenerator>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
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

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAnyOrigin");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
