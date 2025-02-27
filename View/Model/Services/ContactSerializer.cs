using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace View.Model.Services
{
    /// <summary>
    /// Класс для сериализации и десериализации контактов в JSON-файл.
    /// </summary>
    public static class ContactSerializer
    {
        /// <summary>
        /// Поле, содержащее путь к файлу JSON, где хранятся контакты.
        /// </summary>
        private static string _filePath;

        /// <summary>
        /// Создает директорию для хранения файла, если она отсутствует.
        /// </summary>
        public static void CreateDirectory()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Contacts", "contacts.json");
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Сохраняет объект Contact в JSON-файл.
        /// </summary>
        /// <param name="contact">Объект контакта для сохранения.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если контакт пустой.</exception>
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

        /// <summary>
        /// Загружает контакт из JSON-файла.
        /// </summary>
        /// <returns>Объект Contact, если файл существует; иначе null.</returns>
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
