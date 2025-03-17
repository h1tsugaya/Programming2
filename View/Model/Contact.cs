using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    /// <summary>
    /// Класс, представляющий контакт с именем, номером телефона и электронной почтой.
    /// Реализует интерфейс <see cref="INotifyPropertyChanged"/> для уведомления об изменениях свойств.
    /// </summary>
    public class Contact : INotifyPropertyChanged
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
        /// Возвращает строковое представление объекта <see cref="Contact"/>.
        /// </summary>
        /// <returns>Имя контакта в виде строки.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
