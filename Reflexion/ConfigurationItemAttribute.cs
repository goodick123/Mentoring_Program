namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        public ProviderTypes ProviderType { get; }

        public readonly string SettingName;

        public ConfigurationItemAttribute(ProviderTypes providerType, string settingName)
        {
            ProviderType = providerType;
            SettingName = !string.IsNullOrEmpty(settingName) ? settingName : throw new ArgumentException("Setting name was not specified.");
        }
    }
}
