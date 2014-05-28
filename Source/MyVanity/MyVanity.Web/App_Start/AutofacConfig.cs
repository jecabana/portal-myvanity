using System.Reflection;
using MyVanity.Common.Autofac;
using MyVanity.Domain;
using MyVanity.Model;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Services.App;
using MyVanity.Views.Repositories.UserViewsRepository;
using MyVanity.Web.Autofac;

namespace MyVanity.Web
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var assemblies = new[]
                             {
                                 Assembly.GetExecutingAssembly(),                                         // ui
                                 Assembly.GetAssembly(typeof(CommonModule)),                              // common
                                 Assembly.GetAssembly(typeof(AppContext)),                                // services
                                 Assembly.GetAssembly(typeof(ModelContainer)),                            // domain
                                 Assembly.GetAssembly(typeof(UserViewRepository<Agent, AgentEditModel>)),
                                 Assembly.GetAssembly(typeof(ModelBase))                                  // adapters
                             };

            var shell = new MvcShell(assemblies);
            shell.Register();
        }
    }
}