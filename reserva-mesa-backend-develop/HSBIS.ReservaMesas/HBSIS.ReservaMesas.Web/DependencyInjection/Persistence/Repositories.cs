using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace HBSIS.ReservaMesas.Web.DependencyInjection.Persistence
{
    [ExcludeFromCodeCoverage]
    public class Repositories
    {
        public void AddRepositoriesDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUnityRepository, UnityRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IWorkstationRepository, WorkstationRepository>();
        }
    }
}
