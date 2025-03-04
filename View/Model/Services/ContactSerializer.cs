using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using View.Model;

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
        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "contacts.json");

        /// <summary>
        /// Сохраняет объект Contact в JSON-файл.
        /// </summary>
        /// <param name="contact">Объект контакта для сохранения.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если контакт пустой.</exception>
        public static void SaveContact(Contact contact)
        {
            try
            {
                string json = JsonConvert.SerializeObject(contact, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает контакт из JSON-файла.
        /// </summary>
        /// <returns>Объект Contact, если файл существует; иначе null.</returns>
        public static Contact LoadContact()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<Contact>(json) ?? new Contact();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            }
            return new Contact();
        }
    }
}
