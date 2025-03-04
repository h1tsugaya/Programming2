using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    /// <summary>
    /// Класс контакта.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; } = "Dave";

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; } = "example@gmail.com";

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; set; } = "8-800-555-35-35";

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
        /// Конструктор класса Contact. По умолчанию пустой.
        /// </summary>
        public Contact()
        {
            Name = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;

        }
    }
}
