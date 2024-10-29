﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUseCases(services);
        AddPasswordEncripter(services, configuration);
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddPasswordEncripter(this IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped(options => new PasswordEncripter(additionalKey!));
    }
}
