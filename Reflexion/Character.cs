namespace Reflection
{
    public class Character : ConfigurationComponentBase
    {
        public Character(string settingFilePath, string appSettingSectionName) : base(settingFilePath,
            appSettingSectionName)
        {
        }

        [ConfigurationItem(ProviderTypes.Config, "HealthParam")]
        public int Health { get; set; }

        [ConfigurationItem(ProviderTypes.Config, "DamageParam")]
        public float Damage { get; set; }

        [ConfigurationItem(ProviderTypes.Config, "NameParam")]
        public string Name { get; set; }

        [ConfigurationItem(ProviderTypes.Config, "CreationDateParam")]
        public TimeSpan CreatedDate { get; set; }

        [ConfigurationItem(ProviderTypes.File, "ArmorParam")]
        public int Armor { get; set; }

        [ConfigurationItem(ProviderTypes.File, "DamageAmplificationParam")]
        public float DamageAmplification { get; set; }

        [ConfigurationItem(ProviderTypes.File, "HeroDescriptionParam")]
        public string HeroDescription { get; set; }

        [ConfigurationItem(ProviderTypes.File, "UpdatedTimeParam")]
        public TimeSpan UpdatedDate { get; set; }
    }
}
