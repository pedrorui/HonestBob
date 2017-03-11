using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HonestBobs.Business;
using HonestBobs.Data;
using HonestBobs.Persistence;
using HonestBobs.Web.Infrastructure;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace HonestBobs.Web
{
    public static class Bootstrapper
    {
        public static void ConfigureDependencies(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            Assembly assembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(assembly);
            builder.RegisterApiControllers(assembly);

            builder.Register(dependency => new HttpSessionManager()).As<ISessionManager>().InstancePerDependency();
            builder.Register(dependency => new DataAccessInitializer()).As<IDataAccessInitializer>().SingleInstance();
            builder.RegisterType<HttpCacheConfiguration>().SingleInstance();
            builder.RegisterType<HttpCache>().As<ICache>().SingleInstance();

            builder.RegisterImplementationsOf(typeof(IDataAccessProvider));
            builder.RegisterImplementationsOf(typeof(IProductRepositoryLocator));
            builder.RegisterImplementationsOf(typeof(IReadRepository<,>));

            builder.RegisterType<ProductLocatorService>();

            //builder.RegisterType<RedisCacheConfiguration>().SingleInstance();
            //builder.RegisterType<RedisCache>().As<ICache>().SingleInstance();

            IContainer container = builder.Build();

            //For MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //For WebApi
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterImplementationsOf(this ContainerBuilder containerBuilder, Type handlerType)
        {
            var predicate = handlerType.IsGenericTypeDefinition
                ? new Func<Type, bool>(
                    type =>
                        type.GetInterfaces()
                            .Any(closedType => closedType.IsClosedTypeOf(handlerType)))
                : new Func<Type, bool>(
                    type =>
                        type.GetInterfaces()
                            .Any(closedType => closedType.IsAssignableFrom(handlerType)));

            containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(predicate)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}