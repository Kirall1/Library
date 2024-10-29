using System.Text;
using Library.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Library.Api.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Library.BusinessAccess.MappingProfiles;
using Library.BusinessAccess.Models.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Library.Api;
using Library.BusinessAccess;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.User;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddAutoMapper(typeof(IMapperMarker));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<IValidationMarker>();
builder.Services.AddBALDependecyInjections();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
        
});
builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };

});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", "public,max-age=600");
        var corsService = ctx.Context.RequestServices.GetRequiredService<ICorsService>();
        var corsPolicyProvider = ctx.Context.RequestServices.GetRequiredService<ICorsPolicyProvider>();
        var policy = corsPolicyProvider.GetPolicyAsync(ctx.Context, "CorsPolicy")
                        .ConfigureAwait(false)
                        .GetAwaiter().GetResult();

        var corsResult = corsService.EvaluatePolicy(ctx.Context, policy);

        corsService.ApplyResult(corsResult, ctx.Context.Response);
    }
});


using var scope = app.Services.CreateScope();
await SeedData.SeedDatabaseAsync(scope.ServiceProvider.GetRequiredService<DatabaseContext>(),
    scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(),
    scope.ServiceProvider.GetRequiredService<IUnitOfWork>());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();