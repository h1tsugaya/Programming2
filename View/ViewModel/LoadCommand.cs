using System;
using System.IO;
using System.Windows.Input;
using View.Model;

namespace View.ViewModel
{
    /// <summary>
    /// Класс команды загрузки данных контакта из файла.
    /// </summary>
    public class LoadCommand : ICommand
    {
        /// <summary>
        /// Событие, которое вызывается при изменении условий выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Выполняет команду загрузки контакта из файла "contact_save.txt".
        /// Если файл существует, считывает из него данные и создает объект Contact.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            string filePath = "contact_save.txt";
            if (File.Exists(filePath))
            {
                try
                {
                    string[] data = File.ReadAllLines(filePath);
                    if (data.Length >= 3)
                    {
                        Contact loadedContact = new Contact(data[0], data[1], data[2]);
                        Console.WriteLine($"Загруженные данные: {loadedContact.Name}, {loadedContact.PhoneNumber}, {loadedContact.Email}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Определяет, может ли команда выполняться.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>Всегда возвращает true.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

    }
}
