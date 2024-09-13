using Application.Contracts.Persistence;
using Application.Contracts.Persistence.DbAccess;
using Application.Contracts.Persistence.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Books;
using Persistence.DataAccess;
using Persistence.DbAccess;

namespace Persistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyFirstEfCoreDb;Trusted_Connection=True");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine);
        }, ServiceLifetime.Scoped);

        serviceDescriptors.AddScoped<ISimpleBookQueryHandler, SimpleBookQueryHandler>();
        serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceDescriptors.AddScoped(typeof(IRunnerWriteDbAsync<,>), typeof(RunnerWriteDb<,>));
        serviceDescriptors.AddScoped<IPlaceOrderDbAccess, PlaceOrderDbAccess>();

        return serviceDescriptors;
    }
}
