using System;
using System.IO;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Класс команды загрузки данных контакта из файла.
    /// </summary>
    public class LoadCommand : ICommand
    {

        /// <summary>
        /// Приватное поле класса MainVM с информацией контакта.
        /// </summary>
        private readonly MainVM _viewModel;

        /// <summary>
        /// Команда загрузки.
        /// </summary>
        /// <param name="viewModel"></param>
        public LoadCommand(MainVM viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// Событие, которое вызывается при изменении условий выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Выполняет команду загрузки контакта из файла "contact_save.txt".
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            Contact contact = ContactSerializer.LoadContact();
            _viewModel.Name = contact.Name;
            _viewModel.PhoneNumber = contact.PhoneNumber;
            _viewModel.Email = contact.Email;
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
