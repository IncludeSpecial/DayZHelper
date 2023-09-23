using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DayZHelper
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<string> passwordsCollection;

        public MainPage()
        {
            InitializeComponent();
            passwordsCollection = new ObservableCollection<string>();
            PasswordsCollectionView.ItemsSource = passwordsCollection;
        }

        private void GenerateButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(NumberOfPasswordsEntry.Text, out int numberOfPasswords) && numberOfPasswords > 0)
            {
                var passwords = GeneratePasswords(numberOfPasswords);
                PasswordsCollectionView.ItemsSource = passwords;
            }
            else
            {
                PasswordsCollectionView.ItemsSource = new List<string> { "Введите допустимое количество паролей:" };
            }
        }


        private List<string> GeneratePasswords(int numberOfPasswords)
        {
            var passwords = new List<string>();
            var random = new Random();

            int leftColumnCount = numberOfPasswords / 2; // Количество элементов для левой колонки
            int rightColumnCount = numberOfPasswords - leftColumnCount; // Количество элементов для правой колонки

            for (int i = 0; i < Math.Max(leftColumnCount, rightColumnCount); i++)
            {
                if (i < leftColumnCount)
                {
                    string leftPassword = random.Next(10000).ToString("D4");
                    passwords.Add($"Пароль #{i + 1}: {leftPassword}");
                }

                if (i < rightColumnCount)
                {
                    string rightPassword = random.Next(10000).ToString("D4");
                    passwords.Add($"Пароль #{leftColumnCount + i + 1} : {rightPassword}");
                }
            }

            return passwords;
        }
    }
}