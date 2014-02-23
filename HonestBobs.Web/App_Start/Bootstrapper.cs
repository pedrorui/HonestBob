using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HonestBobs.Data;
using HonestBobs.Persistence;
using HonestBobs.Web.Infrastructure;

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

			builder.Register(dependency => new SessionManager()).As<ISessionManager>().InstancePerDependency();
			builder.Register(dependency => new DataAccessInitializer()).As<IDataAccessInitializer>().InstancePerDependency();
			builder.Register(dependency => new HttpCache()).As<ICache>().InstancePerDependency();

			builder.RegisterDependenciesOf(typeof(IDataAccessProvider));
			builder.RegisterDependenciesOf(typeof(IRepositoryLocator));

			IContainer container = builder.Build();

			//For MVC
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			//For WebApi
			configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}

		private static void RegisterDependenciesOf(this ContainerBuilder containerBuilder, Type handlerType)
		{
			containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
				.Where(type => type.GetInterfaces().Any(closedType => closedType.IsAssignableFrom(handlerType)))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}