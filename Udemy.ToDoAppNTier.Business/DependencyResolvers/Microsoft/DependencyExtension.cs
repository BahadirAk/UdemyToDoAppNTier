using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Business.Mappings.AutoMapper;
using Udemy.ToDoAppNTier.Business.Services;
using Udemy.ToDoAppNTier.Business.ValidationRules;
using Udemy.ToDoAppNTier.DataAccess.Contexts;
using Udemy.ToDoAppNTier.DataAccess.UnitOfWork;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;

namespace Udemy.ToDoAppNTier.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ToDoContext>(opt =>
            {
                opt.UseSqlServer("");
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();

            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();
        }
    }
}
