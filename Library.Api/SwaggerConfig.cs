using Microsoft.OpenApi.Models;
using Library.BusinessAccess.Models.Book;
using Microsoft.OpenApi.Any;
using System.Reflection;

namespace Library.Api
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                options.MapType<BookCreateDto>(() => new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        ["isbn"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("978-3-16-148410-0") },
                        ["title"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("The Great Gatsby") },
                        ["genre"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("Fiction") },
                        ["description"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("A novel set in the Jazz Age that explores themes of wealth, love, and the American Dream.") },
                        ["authorId"] = new OpenApiSchema { Type = "integer", Example = new OpenApiInteger(1) },
                        ["imageFile"] = new OpenApiSchema { Type = "string", Format = "binary" }
                    }
                });

            });
            return services;
        }
    }
}