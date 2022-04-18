using AsyncComms.ApiGateway.BusinessLogic.Services;
using Autofac;

namespace AsyncComms.ApiGateway
{
    public static class DependencyInjectorResolver2
    {
        public static void ConfigureDI(WebApplicationBuilder builder)
        {
            builder.Host.ConfigureContainer<ContainerBuilder>(b =>
                {
                    b.RegisterType<CanDoMessages>().As<ICanDoMessages>();
                }

            );
        }

    }
}
