using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Application.Services.Abstractions;
using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IBranchService, BranchService>();
        builder.Services.AddScoped<IUserService, UserService>();
    }
}