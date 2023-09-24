using System;
using System.Collections.Generic;
using System.Text;

namespace DayZHelper
{
    public partial class MainPage : ContentPage
    {
        private List<string> passwordsList;

        public MainPage()
        {
            InitializeComponent();
            passwordsList = new List<string>();
            PasswordsCollectionView.ItemsSource = passwordsList;
        }

        private void GenerateButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(NumberOfPasswordsEntry.Text, out var numberOfPasswords) && numberOfPasswords > 0)
            {
                var passwordLength = GetSelectedPasswordLength();
                var passwords = GeneratePasswords(numberOfPasswords, passwordLength);
                PasswordsCollectionView.ItemsSource = passwords;
            }
            else
            {
                PasswordsCollectionView.ItemsSource = new List<string>
                    { "Введите допустимое количество паролей (например, 4)." };
            }
        }

        private int GetSelectedPasswordLength()
        {
            switch (PasswordLengthPicker.SelectedItem.ToString())
            {
                case "3 знака": return 3;
                case "4 знака": return 4;
                case "6 знаков": return 6;
                default: return 4;
            }
        }

        private List<string> GeneratePasswords(int numberOfPasswords, int passwordLength)
        {
            var passwords = new List<string>();
            var random = new Random();

            for (var i = 0; i < numberOfPasswords; i++)
            {
                var newPassword = GenerateRandomPassword(random, passwordLength);
                passwords.Add($"Пароль #{i + 1}: {newPassword}");
            }

            return passwords;
        }

        private string GenerateRandomPassword(Random random, int length)
        {
            const string allowedChars = "0123456789";
            var password = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var index = random.Next(0, allowedChars.Length);
                password.Append(allowedChars[index]);
            }

            return password.ToString();
        }
    }
}