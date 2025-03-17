using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.Converter
{
    /// <summary>
    /// Конвертер, преобразующий булевы значения в значения перечисления <see cref="Visibility"/>.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует булево значение в значение перечисления <see cref="Visibility"/>.
        /// </summary>
        /// <param name="value">Булево значение, которое нужно преобразовать.</param>
        /// <param name="targetType">Тип целевого свойства (не используется).</param>
        /// <param name="parameter">Дополнительный параметр (не используется).</param>
        /// <param name="culture">Культура (не используется).</param>
        /// <returns>
        /// Возвращает <see cref="Visibility.Visible"/>, если значение <c>true</c>,
        /// и <see cref="Visibility.Collapsed"/>, если значение <c>false</c>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Преобразует значение перечисления <see cref="Visibility"/> обратно в булево значение.
        /// </summary>
        /// <param name="value">Значение перечисления <see cref="Visibility"/>.</param>
        /// <param name="targetType">Тип целевого свойства (не используется).</param>
        /// <param name="parameter">Дополнительный параметр (не используется).</param>
        /// <param name="culture">Культура (не используется).</param>
        /// <returns>
        /// Этот метод не реализован и всегда выбрасывает исключение <see cref="NotImplementedException"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// Выбрасывается всегда, так как обратное преобразование не поддерживается.
        /// </exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}