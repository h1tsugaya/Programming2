using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Model;
using View.Model.Enums;
using View.Model.Services;

namespace View.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Модель контакта, данные которой отображаются и редактируются в View.
        /// </summary>
        private Contact? _selectedContact { get; set; }

        /// <summary>
        /// Временный экземпляр контакта для восстановления.
        /// На случай, если пользователь прервёт редактирование.
        /// </summary>
        private Contact _temporaryContact { get; set; }

        /// <summary>
        /// Состояние приложения.
        /// </summary>
        private State _state = State.Reading;

        /// <summary>
        /// Список контактов.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        /// <summary>
        /// Команда для добавления контакта.
        /// </summary>
        public RelayCommand AddContactCommand { get; }

        /// <summary>
        /// Команда для редактирования контакта.
        /// </summary>
        public RelayCommand EditContactCommand { get; }

        /// <summary>
        /// Команда для подтверждения действия.
        /// </summary>
        public RelayCommand ApplyCommand { get; }

        /// <summary>
        /// Команда для удаления контакта.
        /// </summary>
        public RelayCommand RemoveContactCommand { get; }

        /// <summary>
        /// Команда для сохранения данных контакта в файл.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Событие, которое необходимо вызвать для уведомления View об изменениях свойств ViewModel.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Состояние приложения.
        /// </summary>
        private State State
        {
            get { return _state; }
            set
            {
                if (_state == value) return;

                _state = value;
                NotifyPropertyChanged(nameof(IsReadOnlyContactTextBoxes));
                NotifyPropertyChanged(nameof(IsApplyVisible));
                EditContactCommand.RaiseCanExecuteChanged();
                AddContactCommand.RaiseCanExecuteChanged();
                RemoveContactCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Имя контакта, связанное с соответствующим полем в View.
        /// </summary>
        public string Name
        {
            get { return SelectedContact != null ? SelectedContact.Name : ""; }
            set
            {
                if (SelectedContact != null && SelectedContact.Name != value)
                {
                    SelectedContact.Name = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(IsApplyEnabled));
                }
            }
        }

        /// <summary>
        /// Номер телефона контакта, связанный с соответствующим полем в View.
        /// </summary>
        public string PhoneNumber
        {
            get { return SelectedContact != null ? SelectedContact.PhoneNumber : ""; }
            set
            {
                if (SelectedContact != null && SelectedContact.PhoneNumber != value)
                {
                    SelectedContact.PhoneNumber = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(IsApplyEnabled));
                }
            }
        }

        /// <summary>
        /// Адрес электронной почты контакта, связанный с соответствующим полем в View.
        /// </summary>
        public string Email
        {
            get { return SelectedContact != null ? SelectedContact.Email : ""; }
            set
            {
                if (SelectedContact != null && SelectedContact.Email != value)
                {
                    SelectedContact.Email = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(IsApplyEnabled));
                }
            }
        }

        /// <summary>
        /// Доступны ли текстовые поля редактирования контакта только для чтения.
        /// </summary>
        public bool IsReadOnlyContactTextBoxes
        {
            get
            {
                return State == State.Reading ? true : false;
            }
        }

        /// <summary>
        /// Определяет, доступна ли кнопка добавления контакта.
        /// </summary>
        public bool CanAddContact
        {
            get
            {
                return State == State.Reading;
            }
        }

        /// <summary>
        /// Определяет, доступна ли кнопка редактирования контакта.
        /// </summary>
        public bool CanEditContact
        {
            get
            {
                return _selectedContact != null && State == State.Reading;
            }
        }

        /// <summary>
        /// Определяет, доступна ли кнопка удаления контакта.
        /// </summary>
        public bool CanRemoveContact
        {
            get
            {
                return _selectedContact != null && State == State.Reading;
            }
        }

        /// <summary>
        /// Доступность кнопки Apply.
        /// </summary>
        public bool IsApplyVisible
        {
            get => State != State.Reading;
        }

        public bool IsApplyEnabled
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Name) &&
                       !string.IsNullOrWhiteSpace(PhoneNumber) &&
                       !string.IsNullOrWhiteSpace(Email) &&
                       !HasValidationErrors;
            }
        }

        private bool _hasValidationErrors;
        public bool HasValidationErrors
        {
            get => _hasValidationErrors;
            set
            {
                if (_hasValidationErrors != value)
                {
                    _hasValidationErrors = value;
                    NotifyPropertyChanged(nameof(IsApplyEnabled));
                }
            }
        }

        /// <summary>
        /// Выбранный контакт.
        /// </summary>
        public Contact? SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (State == State.Editing && SelectedContact != null)
                {
                    _temporaryContact.CopyValues(SelectedContact);
                }

                if (_selectedContact != null)
                    _selectedContact.PropertyChanged -= SelectedContact_PropertyChanged;

                if (value != _selectedContact)
                {
                    State = State.Reading;
                    _selectedContact = value;
                    NotifyPropertyChanged("");
                    EditContactCommand.RaiseCanExecuteChanged();
                    RemoveContactCommand.RaiseCanExecuteChanged();

                    if (_selectedContact != null)
                        _selectedContact.PropertyChanged += SelectedContact_PropertyChanged;
                    ApplyCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Обработчик изменения свойств в SelectedContact.
        /// </summary>
        private void SelectedContact_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ApplyCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Метод для уведомления View об изменении значения свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства 
        /// (автоматически подставляется компилятором).</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Name) ||propertyName == nameof(PhoneNumber) || propertyName == nameof(Email))
            {
                NotifyPropertyChanged(nameof(IsApplyEnabled));
            }
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public MainVM()
        {
            Contacts = ContactSerializer.LoadContacts();
            NotifyPropertyChanged("");

            AddContactCommand = new RelayCommand(
                _ => {
                    SelectedContact = null;
                    SelectedContact = new Contact();
                    State = State.Adding;
                },
                _ => CanAddContact
            );
            EditContactCommand = new RelayCommand(
                _ => {
                    if (SelectedContact == null) return;
                    _temporaryContact = SelectedContact.Clone();
                    State = State.Editing;
                },
                _ => CanEditContact
            );
            ApplyCommand = new RelayCommand(
                _ => {
                    if (SelectedContact == null) return;
                    if (State == State.Adding) Contacts.Add(SelectedContact);
                    State = State.Reading;
                },
                _ => SelectedContact != null && !SelectedContact.HasErrors()
            );
            RemoveContactCommand = new RelayCommand(
                _ => {
                    if (SelectedContact == null) return;

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
                },
                _ => CanRemoveContact
            );

            SaveCommand = new RelayCommand(_ =>
            {
                ContactSerializer.SaveContacts(Contacts);
            });
        }
    }
}

