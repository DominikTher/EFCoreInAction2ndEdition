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

        serviceDescriptors.AddScoped<IBizAction<PlaceOrder2InDto, Features.Orders.PlaceOrder.PlaceLineItemsQuery>, Features.Orders.PlaceOrder.PlaceOrderAction>();
        serviceDescriptors
            .AddScoped<IBizAction<Features.Orders.PlaceOrder.PlaceLineItemsQuery, Order>, Features.Orders.PlaceOrder.PlaceLineItemsAction>();

        return serviceDescriptors;
    }
}
