using System.Diagnostics.CodeAnalysis;
using HBSIS.ReservaMesas.Application.Services;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HBSIS.ReservaMesas.Web.DependencyInjection.Application
{
    [ExcludeFromCodeCoverage]
    public class Services
    {
        public void AddServicesDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUnityService, UnityService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IWorkstationService, WorkstationService>();
            services.AddScoped<IReservationService, ReservationService>();
        }
    }
}
