using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Класс, представляющий контакт.
    /// </summary>
    public partial class Contact : ObservableValidator
    {
        /// <summary>
        /// Имя.
        /// </summary>
        private string _name = string.Empty;
        /// <summary>
        /// Номер телефона.
        /// </summary>
        private string _phoneNumber = string.Empty;
        /// <summary>
        /// Электронная почта.
        /// </summary>
        private string _email = string.Empty;

        /// <summary>
        /// Имя.
        /// </summary>
        [Required(ErrorMessage = "Имя обязательно для заполнения.")]
        [MaxLength(100, ErrorMessage = "Имя не может быть длиннее 100 символов.")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, true);
        }
        /// <summary>
        /// Номер телефона.
        /// </summary>
        [Required(ErrorMessage = "Номер телефона обязателен для заполнения.")]
        [MaxLength(100, ErrorMessage = "Номер телефона не может быть длиннее 100 символов.")]
        [Phone(ErrorMessage = "Некорректный формат номера телефона.")]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value, true);
        }
        /// <summary>
        /// Электронная почта.
        /// </summary>
        [Required(ErrorMessage = "Электронная почта обязательна для заполнения.")]
        [MaxLength(100, ErrorMessage = "Электронная почта не может быть длиннее 100 символов.")]
        [EmailAddress(ErrorMessage = "Текст не соответствует формату электронной почты.")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, true);
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Конструктор класса с параметрами.
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
        /// Копировать значения свойств из исходного объекта в переданный объект.
        /// </summary>
        /// <param name="otherContact">Другой объект.</param>
        public void CopyValues(Contact otherContact)
        {
            otherContact.Name = Name;
            otherContact.PhoneNumber = PhoneNumber;
            otherContact.Email = Email;
        }

        /// <summary>
        /// Получить клон экземпляра класса.
        /// </summary>
        /// <returns>Новый экземпляр класса.</returns>
        public Contact Clone() => (Contact)MemberwiseClone();
    }
}
