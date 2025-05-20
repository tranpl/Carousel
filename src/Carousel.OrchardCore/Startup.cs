using Carousel.OrchardCore.Migrations;

using Microsoft.Extensions.DependencyInjection;

using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Carousel.OrchardCore
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, CarouselMigrations>();
        }
    }
}