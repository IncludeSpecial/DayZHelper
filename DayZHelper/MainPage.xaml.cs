using System.Text;

namespace DayZHelper;

public partial class MainPage
{
    public MainPage()
    {
        // Инициализация главной страницы приложения
        InitializeComponent();

        // Создание пустого списка для хранения паролей
        var passwordsList = new List<string>();

        // Установка списка паролей в качестве источника данных для CollectionView
        PasswordsCollectionView.ItemsSource = passwordsList;
    }

    // Обработчик события нажатия кнопки "Сгенерировать пароли"
    private void GenerateButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Попытка преобразовать введенное количество паролей в число
            if (int.TryParse(NumberOfPasswordsEntry.Text, out var numberOfPasswords) && numberOfPasswords > 0)
            {
                // Получение выбранной длины пароля из Picker
                var passwordLength = GetSelectedPasswordLength();

                // Генерация паролей и установка их в источник данных CollectionView
                var passwords = GeneratePasswords(numberOfPasswords, passwordLength);
                PasswordsCollectionView.ItemsSource = passwords;
            }
            else
            {
                // Если введено некорректное количество паролей, выводим сообщение об ошибке
                PasswordsCollectionView.ItemsSource = new List<string>
                    { "Введите допустимое количество паролей (например, 4)." };
            }
        }
        catch (Exception ex)
        {
            // Вывод сообщения об ошибке в случае исключения
            Console.WriteLine($"Произошла ошибка при генерации паролей: {ex.Message}");
        }
    }

    // Получение выбранной длины пароля из Picker
    private int GetSelectedPasswordLength()
    {
        try
        {
            // Получение выбранного элемента из Picker и преобразование его в длину пароля
            switch (PasswordLengthPicker.SelectedItem.ToString())
            {
                case "3 знака": return 3;
                case "4 знака": return 4;
                case "6 знаков": return 6;
                default: return 4; // Значение по умолчанию
            }
        }
        catch (Exception ex)
        {
            // Вывод сообщения об ошибке, если произошло исключение
            Console.WriteLine($"Произошла ошибка при получении выбранной длины пароля: {ex.Message}");
            return 4; // Возвращение значения по умолчанию в случае ошибки
        }
    }

    // Генерация списка паролей
    private List<string> GeneratePasswords(int numberOfPasswords, int passwordLength)
    {
        try
        {
            // Создание списка для хранения сгенерированных паролей
            var passwords = new List<string>();

            // Создание генератора случайных чисел
            var random = new Random();

            // Генерация заданного количества паролей
            for (var i = 0; i < numberOfPasswords; i++)
            {
                // Генерация одного пароля и добавление его в список
                var newPassword = GenerateRandomPassword(random, passwordLength);
                passwords.Add($"Пароль #{i + 1}: {newPassword}");
            }

            // Возвращение списка сгенерированных паролей
            return passwords;
        }
        catch (Exception ex)
        {
            // Вывод сообщения об ошибке, если произошло исключение
            Console.WriteLine($"Произошла ошибка при генерации паролей: {ex.Message}");

            // Возвращение пустого списка в случае ошибки
            return new List<string>();
        }
    }

    // Очистка списка паролей
    private void ClearListWithPasswords(object sender, EventArgs eventArgs)
    {
        try
        {
            // Получение списка паролей из источника данных CollectionView
            var passwordsList = (List<string>)PasswordsCollectionView.ItemsSource;

            // Очистка списка паролей
            passwordsList?.Clear();

            // Установка источника данных CollectionView в значение null
            PasswordsCollectionView.ItemsSource = null;
        }
        catch (Exception ex)
        {
            // Вывод сообщения об ошибке, если произошло исключение
            Console.WriteLine($"Произошла ошибка при очистке списка паролей: {ex.Message}");
        }
    }

    // Генерация случайного пароля
    private string GenerateRandomPassword(Random random, int length)
    {
        // Строка, содержащая допустимые символы для пароля
        const string allowedChars = "0123456789";

        // Строка для хранения сгенерированного пароля
        var password = new StringBuilder();

        // Генерация символов пароля указанной длины
        for (var i = 0; i < length; i++)
        {
            // Получение случайного индекса из строки допустимых символов
            var index = random.Next(0, allowedChars.Length);

            // Добавление выбранного символа к паролю
            password.Append(allowedChars[index]);
        }

        // Преобразование пароля в строковый формат и возврат
        return password.ToString();
    }
}