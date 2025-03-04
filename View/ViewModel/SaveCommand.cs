using System;
using System.IO;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Класс команды сохранения данных контакта в файл.
    /// </summary>
    public class SaveCommand : ICommand
    {

        /// <summary>
        /// Приватное поле класса MainVM с информацией контакта.
        /// </summary>
        private readonly MainVM _viewModel;

        /// <summary>
        /// Команда сохранения.
        /// </summary>
        /// <param name="viewModel"></param>
        public SaveCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }

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
            Contact contact = new Contact(_viewModel.Name, _viewModel.PhoneNumber, _viewModel.Email);
            ContactSerializer.SaveContact(contact);
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
