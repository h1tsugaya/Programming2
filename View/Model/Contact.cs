using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace View.Model
{
    /// <summary>
    /// Класс, представляющий контакт с именем, номером телефона и электронной почтой.
    /// Реализует интерфейс <see cref="INotifyPropertyChanged"/> для уведомления об изменениях свойств.
    /// </summary>
    public class Contact : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Приватные поля имени, номера телефона и эл. почты.
        /// </summary>
        private string _name;
        private string _phoneNumber;
        private string _email;

        /// <summary>
        /// Событие, которое происходит при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Получает или задает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Получает или задает номер телефона контакта.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Получает или задает электронную почту контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Уведомляет об изменении значения свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Экземпляр класса Contact.
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="phoneNumber">Номер телефона.</param>
        /// <param name="email">Электронная почта.</param>
        public Contact(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        /// <summary>
        /// Конструктор класса Contact.
        /// </summary>
        public Contact()
        {
            Name = "Dave";
            PhoneNumber = "8-800-555-35-35";
            Email = "example@gmail.com";

        }

        /// <summary>
        /// Копирует значения свойств текущего объекта в другой объект <see cref="Contact"/>.
        /// </summary>
        /// <param name="otherContact">Объект, в который копируются значения.</param>
        public void CopyValues(Contact otherContact)
        {
            otherContact.Name = Name;
            otherContact.PhoneNumber = PhoneNumber;
            otherContact.Email = Email;
        }

        /// <summary>
        /// Создает новый объект <see cref="Contact"/>, который является копией текущего экземпляра.
        /// </summary>
        /// <returns>Новый объект <see cref="Contact"/>, который является копией текущего экземпляра.</returns>
        public Contact Clone()
        {
            return (Contact)MemberwiseClone();
        }

        /// <summary>
        /// Индексатор, позволяющий обращаться к экземпляру класса как к массиву или словарю,
        /// используя ключ.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            error = "Имя обязательно для заполнения.";
                        }
                        else if (Name.Length > 100)
                        {
                            error = "Имя не может превышать 100 символов.";
                        }

                        break;
                    case nameof(PhoneNumber):
                        if (string.IsNullOrEmpty(PhoneNumber))
                        {
                            error = "Номер телефона обязателен для заполнения.";
                        }
                        else if (PhoneNumber.Length > 100)
                        {
                            error = "Номер телефона не может превышать 100 символов.";
                        }
                        else if (!Regex.IsMatch(PhoneNumber, @"^[0-9+\-\(\) ]*$"))
                        {
                            error = "Номер телефона может содержать только цифры и символы \"+-()\".";
                        }

                        break;
                    case nameof(Email):
                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            error = "Электронная почта обязательна для заполнения.";
                        }
                        else if (Email.Length > 100)
                        {
                            error = "Электронная почта не может превышать 100 символов.";
                        }
                        else if (!Email.Contains("@"))
                        {
                            error = "Текст не соответствует формату электронной почты.";
                        }

                        break;
                }
                return error;
            }
        }

        /// <summary>
        /// Проверка на наличие ошибок.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool HasErrors()
        {
            return typeof(Contact).GetProperties()
                .Any(prop => !string.IsNullOrEmpty(this[prop.Name]));
        }

        public string Error => String.Empty;
    }
}
