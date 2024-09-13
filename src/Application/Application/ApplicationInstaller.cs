using Application.Contracts.Persistence.Models;
using Application.Core;
using Application.Features.Orders;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddMapster();
        serviceDescriptors.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(ApplicationInstaller).Assembly));
        serviceDescriptors.AddScoped<IBizAction<PlaceOrderInDto, Order>, PlaceOrderAction>();

        return serviceDescriptors;
    }
}
