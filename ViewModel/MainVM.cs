using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Model;
using Model.Enums;
using Model.Services;

namespace ViewModel
{
    /// <summary>
    /// ViewModel для главного окна приложения, отвечает за взаимодействие между View и Model.
    /// </summary>
    public partial class MainVM : ObservableObject
    {
        /// <summary>
        /// Временный экземпляр контакта для восстановления.
        /// На случай, если пользователь прервёт редактирование.
        /// </summary>
        private Contact _temporaryContact = new();

        /// <summary>
        /// Модель контакта, данные которой отображаются и редактируются в View.
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemoveContactCommand))]
        private Contact? _selectedContact;

        /// <summary>
        /// Состояние приложения.
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsReadOnlyContactTextBoxes))]
        [NotifyPropertyChangedFor(nameof(IsApplyVisible))]
        [NotifyCanExecuteChangedFor(nameof(EditContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(AddContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemoveContactCommand))]
        private State _state = State.Reading;

        /// <summary>
        /// Список контактов.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        /// <summary>
        /// Команда для добавления контакта.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanAddContact))]
        public void AddContact()
        {
            SelectedContact = null;
            SelectedContact = new Contact();
            State = State.Adding;
        }

        /// <summary>
        /// Команда для редактирования контакта.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanEditContact))]
        public void EditContact()
        {
            if (SelectedContact == null) return;
            _temporaryContact = SelectedContact.Clone();
            State = State.Editing;
        }

        /// <summary>
        /// Команда для удаления контакта.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanRemoveContact))]
        public void RemoveContact()
        {
            if (SelectedContact == null) return;

            // TODO: var везде кроме циклов for здесь и по программе
            int currentIndex = Contacts.IndexOf(SelectedContact);
            Contacts.Remove(SelectedContact);

            if (Contacts.Count == 0)
            {
                SelectedContact = null;
            }
            else
            {
                int newIndex = Math.Min(currentIndex, Contacts.Count - 1);
                SelectedContact = Contacts[newIndex];
            }
        }

        /// <summary>
        /// Команда для подтверждения действия.
        /// </summary>
        [RelayCommand]
        public void Apply()
        {
            // TODO:
            if (SelectedContact == null || SelectedContact.GetErrors().Any()) return;
            if (State == State.Adding) Contacts.Add(SelectedContact);
            State = State.Reading;
        }

        /// <summary>
        /// Команда для сохранения данных контакта в файл.
        /// </summary>
        [RelayCommand]
        public void Save()
        {
            ContactSerializer.Save(Contacts);
        }

        /// <summary>
        /// Определяет, доступна ли кнопка добавления контакта.
        /// </summary>
        public bool CanAddContact => State == State.Reading;
        /// <summary>
        /// Определяет, доступна ли кнопка редактирования контакта.
        /// </summary>
        public bool CanEditContact => SelectedContact != null && State == State.Reading;
        /// <summary>
        /// Определяет, доступна ли кнопка удаления контакта.
        /// </summary>
        public bool CanRemoveContact => SelectedContact != null && State == State.Reading;
        /// <summary>
        /// Доступны ли текстовые поля редактирования контакта только для чтения.
        /// </summary>
        public bool IsReadOnlyContactTextBoxes => State == State.Reading ? true : false;
        /// <summary>
        /// Доступность кнопки Apply.
        /// </summary>
        public bool IsApplyVisible => State != State.Reading;

        /// <summary>
        /// Изменение выбранного контакта.
        /// </summary>
        /// <param name="oldValue">Старое значение.</param>
        /// <param name="newValue">Новое значение.</param>
        partial void OnSelectedContactChanged(Contact? oldValue, Contact? newValue)
        {
            if (State == State.Editing && oldValue != null)
            {
                _temporaryContact.CopyValues(oldValue);
            }
            State = State.Reading;
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public MainVM()
        {
            Contacts = ContactSerializer.Load();
        }
    }
}

