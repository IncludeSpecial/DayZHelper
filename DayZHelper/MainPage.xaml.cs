using System;
using System.Collections.Generic;
using System.Text;

namespace DayZHelper
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GenerateButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(NumberOfPasswordsEntry.Text, out int numberOfPasswords) && numberOfPasswords > 0)
            {
                var passwords = GeneratePasswords(numberOfPasswords);
                PasswordsLabel.Text = string.Join(Environment.NewLine, passwords);
            }
            else
            {
                PasswordsLabel.Text = "Введите допустимое количество паролей:";
            }
        }

        private List<string> GeneratePasswords(int numberOfPasswords)
        {
            var passwords = new List<string>();
            var random = new Random();

            while (passwords.Count < numberOfPasswords)
            {
                string newPassword = Guid.NewGuid().ToString("N").Substring(0, 4);

                if (!passwords.Contains(newPassword))
                {
                    passwords.Add($"Пароль #{passwords.Count + 1}: {newPassword}");
                }
            }

            return passwords;
        }
    }
}