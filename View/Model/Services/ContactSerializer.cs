using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace View.Model.Services
{
    public static class ContactSerializer
    {
        private static string _filePath;

        public static void CreateDirectory()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Contacts", "contacts.json");
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void SaveContact(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentException(nameof(contact), "Контакт не может быть пустым!");
            }

            CreateDirectory();
            var json = JsonConvert.SerializeObject(contact, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public static Contact LoadContact()
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<Contact>(json);
        }
    }
}
