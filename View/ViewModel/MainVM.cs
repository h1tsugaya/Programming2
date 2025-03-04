using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Model;

namespace View.ViewModel
{
    /// <summary>
    /// Главная модель управления контактом (ViewModel).
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Приватное поле контакта.
        /// </summary>
        private Contact _contact;

        /// <summary>
        /// Конструктор класса MainVM.
        /// Инициализирует объект Contact с пустыми значениями.
        /// </summary>
        public MainVM()
        {
            _contact = new Contact(string.Empty, string.Empty, string.Empty);
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
        }

        /// <summary>
        /// Свойство для комманды сохранения.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Свойство для команды загрузки.
        /// </summary>
        public ICommand LoadCommand { get; }

        /// <summary>
        /// Событие, уведомляющее об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Метод, вызывающий событие PropertyChanged при изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Свойство имени контакта.
        /// </summary>
        public string Name
        {
            get { return _contact.Name; }
            set
            {
                if (_contact.Name != value)
                {
                    _contact.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Свойство номера телефона контакта.
        /// </summary>
        public string PhoneNumber
        {
            get { return _contact.PhoneNumber; }
            set
            {
                if (_contact.PhoneNumber != value)
                {
                    _contact.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Свойство электронной почты контакта.
        /// </summary>
        public string Email
        {
            get { return _contact.Email; }
            set
            {
                if (_contact.Email != value)
                {
                    _contact.Email = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
