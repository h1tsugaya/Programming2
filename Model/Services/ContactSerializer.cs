using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace Model.Services
{
    /// <summary>
    /// Класс для сериализации и десериализации контактов в JSON-файл.
    /// </summary>
    public static class ContactSerializer
    {
        // xml
        private static JsonSerializerSettings Settings { get; } = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// Путь до каталога с сохранением.
        /// </summary>
        private static string AppFolderPath { get; } = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.MyDocuments), "Contacts");

        /// <summary>
        /// Сохранить данные.
        /// </summary>
        /// <param name="contact"></param>
        public static void Save(ObservableCollection<Contact> contact)
        {
            File.WriteAllText(AppFolderPath + @"\contacts.json",
                JsonConvert.SerializeObject(contact, Formatting.Indented, Settings));
        }

        /// <summary>
        /// Загрузить данные.
        /// </summary>
        public static ObservableCollection<Contact> Load()
        {
            ObservableCollection<Contact> contact = new ObservableCollection<Contact>();
            if (!Directory.Exists(AppFolderPath))
            {
                Directory.CreateDirectory(AppFolderPath);
            }

            if (File.Exists(AppFolderPath + @"\contacts.json"))
            {
                try
                {
                    ObservableCollection<Contact> data = JsonConvert.DeserializeObject
                        <ObservableCollection<Contact>>
                        (File.ReadAllText(AppFolderPath + @"\contacts.json"), Settings)!;
                    if (data != null) contact = data;
                }
                catch
                {
                    Console.WriteLine();
                }
            }
            return contact;
        }
    }
}
