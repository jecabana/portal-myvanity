using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace MyVanity.Common.Autofac
{
    public abstract class Shell
    {
        private readonly IEnumerable<Module> _modules;
        private readonly IEnumerable<Type> _types;

        protected Shell(Assembly[] assemblies)
        {
            _modules = assemblies.SelectMany(LoadModules);
            _types = assemblies.SelectMany(a => a.GetTypes());
        }

        protected Shell(IEnumerable<Type> moduleTypes, IEnumerable<Type> types)
        {
            _modules = LoadModules(moduleTypes);
            _types = types;
        }

        protected abstract void SetLifetimeScope(ContainerBuilder containerBuilder, Type @interface, Type implementation, LifetimeAbbr lifetime);

        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {
            foreach (var module in _modules)
            {
                builder.RegisterModule(module);
            }

            var bindings = 
                LoadBindings<IPerRequestDependency>(LifetimeAbbr.PerWebRequest).Concat(
                LoadBindings<ISingleInstanceDependency>(LifetimeAbbr.SingleInstace).Concat(
                LoadBindings<ITransientDependency>(LifetimeAbbr.Transient)));

            foreach (var binding in bindings)
                SetLifetimeScope(builder, binding.Interface, binding.Implementation, binding.Lifetime);
        }

        public virtual IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);

            containerBuilder.RegisterModule<ShellModule>();
            var container = containerBuilder.Build();
            return container;
        }

        private IEnumerable<Module> LoadModules(Assembly assembly)
        {
            var modules = assembly.GetTypes().Where(t => typeof(Module).IsAssignableFrom(t));
            return LoadModules(modules);
        }
        private IEnumerable<Module> LoadModules(IEnumerable<Type> types)
        {
            return types.Select(Activator.CreateInstance).Cast<Module>();
        }

        private IEnumerable<Binding> LoadBindings<TInterface>(LifetimeAbbr lifetime)
        {
            var implementations = 
                _types
                    .Where(t => t.IsClass &&
                                !t.IsAbstract &&
                                typeof(TInterface).IsAssignableFrom(t) && 
                                typeof(TInterface) != t);

            foreach (var implementation in implementations)
            {
                yield return
                    new Binding
                        {
                            Interface = implementation,
                            Implementation = implementation,
                            Lifetime = lifetime
                        };

                var interfaces =
                    implementation
                        .GetInterfaces()
                        .Where(t => typeof (TInterface).IsAssignableFrom(t) &&
                                    typeof (TInterface) != t);

                foreach (var @interface in interfaces)
                    yield return 
                        new Binding
                            {
                                Interface = @interface,
                                Implementation = implementation,
                                Lifetime = lifetime
                            };
            }

            //foreach (var @interface in interfaces)
            //{
            //    // Direct Implementations
            //    var implementations = 
            //        _types
            //            .Where(t => !t.IsAbstract)
            //            .Where(t => @interface.IsAssignableFrom(t));

            //    // Generic Interface
            //    if (@interface.IsGenericType)
            //    {
            //        // By Generic type
            //        implementations =
            //            implementations.Concat(
            //                _types
            //                    .Where(t => !t.IsAbstract)
            //                    .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface)));

            //        // By Specific type
            //        foreach (var implementation in  _types.Where(t => !t.IsAbstract && !t.IsGenericType))
            //        {
            //            var interfaces = 
            //        }
            //        var genericInterfaceImplementation =
                       
            //                .SelectMany(t => t.GetInterfaces())
            //                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == @interface)
            //    }
                        
            //    foreach (var implementation in implementations)
            //        yield return 
            //            new Binding
            //                {
            //                    Interface = @interface,
            //                    Implementation = implementation,
            //                    Lifetime = lifetime
            //                };

            //}
        }
    }
}