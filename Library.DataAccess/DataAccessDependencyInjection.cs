﻿using Library.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess.Repositories.Impl;
using Library.DataAccess.Repositories;

namespace Library.DataAccess
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddUnitOfWork(configuration);
            services.AddRepositories(configuration);
            services.AddIdentity();

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration["ConnectionString"],
                    opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
        }

        private static void AddUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<DatabaseContext>();
        }

    }
}