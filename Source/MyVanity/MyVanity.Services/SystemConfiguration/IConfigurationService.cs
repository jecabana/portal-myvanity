using MyVanity.Common.Autofac;

namespace MyVanity.Services.SystemConfiguration
{
    public interface IConfigurationService : IPerRequestDependency
    {
        string GetConfigurationString(string name);

        int? GetConfigurationInt(string name);

        string[] GetConfigurationArray(string name);
    }
}
