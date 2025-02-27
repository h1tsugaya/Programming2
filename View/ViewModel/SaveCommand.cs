using System;
using System.IO;
using System.Windows.Input;
using View.Model;

namespace View.ViewModel
{
    /// <summary>
    /// Класс команды сохранения данных контакта в файл.
    /// </summary>
    public class SaveCommand : ICommand
    {
        /// <summary>
        /// Событие, вызываемое при изменении условий выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Выполняет команду сохранения контакта в файл "contact_save.txt".
        /// </summary>
        /// <param name="parameter">Ожидается объект типа Contact.</param>
        public void Execute(object parameter)
        {
            if (parameter is Contact contact)
            {
                string filePath = "contact_save.txt";
                try
                {
                    File.WriteAllText(filePath, $"{contact.Name}\n{contact.PhoneNumber}\n{contact.Email}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка сохранения: {ex.Message}");
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
