using System;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Microsoft.WindowsAzure.Storage;
using MyVanity.Common.Autofac;

namespace MyVanity.Web.Autofac
{
    public class MvcShell : Shell
    {
        public MvcShell(Assembly[] assemblies)
            : base(assemblies)
        {
        }

        public void Register()
        {
            var container = BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

           builder
                .Register(c => CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageAccount"].ConnectionString))
                .InstancePerHttpRequest();
        }

        protected override void SetLifetimeScope(ContainerBuilder builder, Type @interface, Type implementation, LifetimeAbbr lifetime)
        {
            var registry =
                @interface.IsGenericType && implementation.IsGenericType
                    ? (IRegistrationBuilder<object, object, object>)builder.RegisterGeneric(implementation).As(@interface)
                    : builder.RegisterType(implementation).As(@interface);

            if (lifetime != LifetimeAbbr.Transient)
                registry.InstancePerHttpRequest();

            if (lifetime == LifetimeAbbr.SingleInstace)
            {
                var methodInfo = typeof(MvcShell).GetMethod("PerHttpSafeResolveRegistry", BindingFlags.NonPublic | BindingFlags.Static);
                var genericMethod = methodInfo.MakeGenericMethod(@interface);
                genericMethod.Invoke(null, new object[] { builder });
            }
        }

        // ReSharper disable UnusedMember.Local
        private static void PerHttpSafeResolveRegistry<T>(ContainerBuilder builder)
        {
            builder.RegisterInstance(PerHttpSafeResolve<T>());
        }
        private static Func<T> PerHttpSafeResolve<T>()
        {
            return () => DependencyResolver.Current.GetService<T>();
        }
        // ReSharper restore UnusedMember.Local
    }
}
