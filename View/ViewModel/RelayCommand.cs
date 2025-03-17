using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.ViewModel
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Поле, содержащее делегат Action, который будет выполнен при вызове команды.
        /// Делегат принимает параметр типа object?, 
        /// который может быть использован для передачи данных в команду.
        /// </summary>
        private readonly Action<object?> _execute;

        /// <summary>
        /// Поле, содержащее делегат Func, который будет выполнен для проверки возможности вызова команды.
        /// </summary>
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// Событие изменения доступности комманды.
        /// </summary>
        private event EventHandler? CanExecuteChangedInternal;

        /// <summary>
        /// Метод, реализующий интерфейс ICommand. 
        /// Выполняет делегат Action, переданный в конструкторе.
        /// </summary>
        /// <param name="parameter">Параметр, передаваемый в команду из View. Может быть null, 
        /// если параметр не требуется.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Метод, реализующий интерфейс ICommand. 
        /// Определяет, может ли команда быть выполнена в текущем состоянии приложения.
        /// </summary>
        /// <param name="parameter">
        /// Параметр, передаваемый в команду из View. 
        /// Используется для определения возможности выполнения команды.
        /// </param>
        /// <returns>Возвращает true, так как команда всегда может быть выполнена.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Событие, реализующее интерфейс ICommand. 
        /// Уведомляет элементы управления, привязанные к команде, 
        /// об изменении возможности выполнения команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CanExecuteChangedInternal += value; }
            remove { CanExecuteChangedInternal -= value; }
        }

        /// <summary>
        /// Сообщить об изменениях доступности комманды.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="execute">Делегат Action, 
        /// который будет выполнен при вызове команды.</param>
        /// <param name="canExecute">Делегат Func, который будет выполнен для 
        /// проверки возможности вызова команды.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, 
        /// если переданный делегат execute равен null.</exception>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
    }
}
