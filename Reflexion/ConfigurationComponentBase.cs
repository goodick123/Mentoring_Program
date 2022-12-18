using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Reflection
{
    public class ConfigurationComponentBase
    {
        private readonly string _filePath;
        private readonly string _appSettingSectionName;

        protected ConfigurationComponentBase(string settingsFilePath, string appSettingSectionName)
        {
            _filePath = File.Exists(settingsFilePath) ? settingsFilePath : 
                throw new FileNotFoundException("File does not exist.");

            _appSettingSectionName = !string.IsNullOrEmpty(appSettingSectionName) ? appSettingSectionName : 
                throw new ArgumentException("App setting section name was null or empty.");
        }

        public void SaveSettings<T>(T item)
        {
            var itemProperties = item?.GetType().GetProperties();

            try
            {
                var stringBuilder = new StringBuilder();
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                foreach (var property in itemProperties!)
                {
                    var attr = property.GetCustomAttribute<ConfigurationItemAttribute>(true);

                    switch (attr!.ProviderType)
                    {
                        case ProviderTypes.File:
                        {
                            stringBuilder.AppendLine($"{attr.SettingName}:{property.GetValue(item)}");

                            break;
                        }
                        case ProviderTypes.Config:
                        {
                            var settings = configFile.AppSettings.Settings;

                            if (settings.Count == 0 | settings[attr.SettingName] == null)
                            {
                                settings.Add(attr.SettingName, property.GetValue(item)?.ToString());
                            }
                            else
                            {
                                settings[attr.SettingName].Value = property.GetValue(item)?.ToString();
                            }

                            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                            
                            break;
                        }
                        default:
                        {
                            throw new ArgumentException("Provider type is wrong.");
                        }
                    }
                }

                configFile.Save(ConfigurationSaveMode.Modified);

                File.WriteAllText(_filePath, stringBuilder.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong");
            }
        }

        public void LoadSettings<T>(T item)
        {
            var itemProperties = item?.GetType().GetProperties();
            var fileSettings = File.ReadLines(_filePath).SelectMany(x => x.Split(':'))
                .Select((v, i) => new { Index = i, Value = v })
                .GroupBy(p => p.Index / 2)
                .ToDictionary(g => g.First().Value, g => g.Last().Value)!;

            if (ConfigurationManager.GetSection(_appSettingSectionName) is not NameValueCollection applicationSettingsFromConfig || applicationSettingsFromConfig.Count == 0)
            {
                Console.WriteLine("Settings are not defined");
            }

            try
            {
                foreach (var property in itemProperties!)
                {
                    var attr = property.GetCustomAttribute<ConfigurationItemAttribute>(true);

                    var settingValue = attr!.ProviderType switch
                    {
                        ProviderTypes.File => fileSettings[attr.SettingName],
                        ProviderTypes.Config => applicationSettingsFromConfig?[attr.SettingName],
                        _ => throw new ArgumentException("Provider type is wrong.")
                    };

                    SetPropertyValue(property, settingValue, item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SetPropertyValue<T>(PropertyInfo property, string? settingValue, T item)
        {
            if (string.IsNullOrEmpty(settingValue))
            {
                throw new KeyNotFoundException("Name was not found");
            }

            var converter = TypeDescriptor.GetConverter(property.PropertyType);
            var result = converter.ConvertFrom(settingValue);
            property.SetValue(item, result);
        }
    }
}
