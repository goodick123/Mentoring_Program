namespace Reflection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter full path of txt file:");
            var filePath = Console.ReadLine();

            var character = new Character(filePath, "appSettings");

            character.LoadSettings(character);

            var characterPropertiesDictionary = character.GetType().GetProperties();

            foreach (var property in characterPropertiesDictionary)
            {
                Console.WriteLine("{0}:{1}", property.Name, property.GetValue(character));
            }

            foreach (var property in characterPropertiesDictionary)
            {
                Console.WriteLine("Would you like to change value of the {0} property?\nPress 'y' to change or any other key to skip.", property.Name);

                var userChoice = Console.ReadKey(true).KeyChar;

                if (userChoice != 'y')
                {
                    continue;
                }

                Console.WriteLine("Enter new value for the {0} param:", property.Name);
                var changedValue = Console.ReadLine();

                try
                {
                    character.SetPropertyValue(property, changedValue, character);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong");
                }
            }

            character.SaveSettings(character);

            foreach (var property in characterPropertiesDictionary)
            {
                Console.WriteLine("{0}:{1}", property.Name, property.GetValue(character));
            }
        }
    }
}

