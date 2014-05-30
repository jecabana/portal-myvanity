using System;
using System.Linq;
using MyVanity.Domain.Repositories.Base;
using SysConfiguration = MyVanity.Domain.SystemConfiguration;

namespace MyVanity.Services.SystemConfiguration.Impl
{
    public class ConfigurationServices : IConfigurationService
    {
        private readonly IRepository<SysConfiguration> _repository;

        public ConfigurationServices(IRepository<SysConfiguration> repository)
        {
            _repository = repository;
        }

        public string GetConfigurationString(string name)
        {
            var configValue = _repository.Get().FirstOrDefault(conf => conf.Name.ToLower() == name.ToLower());

            if (configValue == null)
                return null;

            return configValue.Value;
        }

        public int? GetConfigurationInt(string name)
        {
            var configValue = GetConfigurationString(name);

            if (configValue == null)
                return null;

            return Convert.ToInt32(configValue);
        }

        public string[] GetConfigurationArray(string name)
        {
            var configValue = GetConfigurationString(name);

            return configValue == null ? null : configValue.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
