using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace UkrZaliznityaInfo
{
    public partial class RegistrationWindow : Window
    {
        private TranslateStatus TStatus;
        private const string SettingsFilePath = "settings.settings";
        private Button lastClickedButton = null;
        string connectionString = @"Data Source=DESKTOP-SMDODNO\SQLEXPRESS;Initial Catalog=UkrZaliznityaInfo;Integrated Security=True;";
        public RegistrationWindow()
        {
            InitializeComponent();
            LoadLanguageSettings();

        }
        private void LoadLanguageSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TranslateStatus));
                    TStatus = (TranslateStatus)serializer.Deserialize(fs);
                }
            }
            else
            {
                TStatus = TranslateStatus.Ukraine; // Значення за замовчуванням
            }

            TranslateUpdate();
        }

        private void SaveLanguageSettings()
        {
            using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TranslateStatus));
                serializer.Serialize(fs, TStatus);
            }
        }

      
        private void ExitClickBoxPass(object sender, RoutedEventArgs e)
        {
            // Зникнення тексту
            var n1 = PassBox;
            var n2 = PassVisibleBox;
            if (n1.Password.Length != 0 || n2.Text.Length != 0)
            {
                PassText.Visibility = Visibility.Hidden;
            }
            else
            {
                PassText.Visibility = Visibility.Visible;
            }
        }

        private void AmountSymbols(object sender, KeyEventArgs e)
        {
            // перевірка по кількості букв в слові
            
                // перевірка по кількості букв в слові
                EmailText.Visibility = MailBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
                PassText.Visibility = PassBox.Password.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            if (PassBox.Visibility == Visibility.Visible)
                PassText.Visibility = PassBox.Password.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            else
                PassText.Visibility = PassVisibleBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            NameText.Visibility = NameBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
                SurnameText.Visibility = SurnameBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
                PhoneText.Visibility = PhoneBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            
            
        }

        private void ClickBoxPass(object sender, RoutedEventArgs e)
        {
            // Зникнення текст "Пароль"
            PassText.Visibility = Visibility.Hidden;
        }
        private void VisibleButton_Click(object sender, RoutedEventArgs e)
        {
            if (PassBox.IsEnabled == true)
            {
                PassVisibleBox.Text = PassBox.Password;
                PassBox.Password = null;
                PassBox.Visibility = Visibility.Hidden;
                PassBox.IsEnabled = false;

                PassVisibleBox.IsEnabled = true;
                PassVisibleBox.Visibility = Visibility.Visible;
            }
            else
            {
                PassBox.Password = PassVisibleBox.Text;
                PassVisibleBox.Text = null;
                PassVisibleBox.Visibility = Visibility.Hidden;
                PassVisibleBox.IsEnabled = false;

                PassBox.IsEnabled = true;
                PassBox.Visibility = Visibility.Visible;
            }
        }

        private void Ukraine_Click(object sender, RoutedEventArgs e)
        {
            lastClickedButton = (Button)sender; // Збереження натиснутої кнопки
            TStatus = TranslateStatus.Ukraine;
            TranslateUpdate();
            SaveLanguageSettings();
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            lastClickedButton = (Button)sender; // Збереження натиснутої кнопки
            TStatus = TranslateStatus.English;
            TranslateUpdate();
            SaveLanguageSettings();
        }

        private void TranslateUpdate()
        {
            if (TStatus == TranslateStatus.Ukraine)
            {
                TranslateUkraine();
            }
            else if (TStatus == TranslateStatus.English)
            {
                TranslateEnglish();
            }
            else
            {
                MessageBox.Show("Помилка: невідомий статус.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void TranslateEnglish()
        {
            slogan.Text = "Buy train tickets -\n easily and quickly!";
            NameText.Text = "Name";
            SurnameText.Text = "Surname";
            Reg_Text.Text = "Registration";
            PassText.Text = "Password";
            SignIn.Content = "Sign In";
            OR_Text.Text = "OR";
            SignUp.Content = "Sign Up";
        }
        private void TranslateUkraine()
        {
            slogan.Text = "Купуйте квитки на потяги -\n легко та швидко!";
            NameText.Text = "Ім'я";
            SurnameText.Text = "Прізвище";
            Reg_Text.Text = "Реєстрація";
            PassText.Text = "Пароль";
            SignIn.Content = "Увійти";
            OR_Text.Text = "АБО";
            SignUp.Content = "Авторизація";  
        }

        private void ClickBoxName(object sender, RoutedEventArgs e)
        {
            NameText.Visibility = Visibility.Hidden;
        }

        private void ClickBoxNumber(object sender, RoutedEventArgs e)
        {
            PhoneText.Visibility = Visibility.Hidden;
        }

        private void ClickBoxEmail(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Hidden;
        }

        private void ExitClickBoxPassword(object sender, RoutedEventArgs e)
        {
            if (PassBox.Visibility == Visibility.Visible)
                PassText.Visibility = PassBox.Password.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            else
                PassText.Visibility = PassVisibleBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ExitClickBoxEmail(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = MailBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ExitClickBoxNumber(object sender, RoutedEventArgs e)
        {
            PhoneText.Visibility = PhoneBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ExitClickBoxName(object sender, RoutedEventArgs e)
        {
            NameText.Visibility = NameBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ExitClickBoxSName(object sender, RoutedEventArgs e)
        {
            SurnameText.Visibility = SurnameBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ClickBoxSName(object sender, RoutedEventArgs e)
        {
            SurnameText.Visibility = Visibility.Hidden;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegistWindow regist = new RegistWindow();
            regist.Show();
            //CheckButton = true;
            Close();
            //CheckButton = false;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Отримуємо значення полів з форми реєстрації
                string firstName = NameBox.Text;
                string lastName = SurnameBox.Text;
                string email = MailBox.Text;
                string password;    
                password = PassBox.Password + PassVisibleBox.Text;      
                
                // Перевірка, чи всі поля заповнені
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Будь ласка, заповніть всі поля", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Перевірка, чи відповідає номер телефону встановленому формату
                string phoneNumber = PhoneBox.Text;
                Regex phoneRegex = new Regex(@"^\+38\(0\d{2}\)\d{7}$"); // Формат: +38(0xx)xxxxxxx
                if (!phoneRegex.IsMatch(phoneNumber))
                {
                    MessageBox.Show("Номер телефону введено некоректно. Використовуйте формат: +38(0xx)xxxxxxx", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Перевірка, чи відповідає пароль встановленому формату
                Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"); // Формат: мінімум 8 символів, містить хоча б одну малу літеру, хоча б одну велику літеру та хоча б одну цифру
                if (!passwordRegex.IsMatch(password))
                {
                    MessageBox.Show("Пароль повинен містити мінімум 8 символів, хоча б одну малу літеру, хоча б одну велику літеру та хоча б одну цифру", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Перевірка, чи відповідає пошта встановленому формату
                Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"); // Проста перевірка на наявність символу '@' та крапки для електронної пошти
                if (!emailRegex.IsMatch(email))
                {
                    MessageBox.Show("Електронна пошта введена некоректно", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Виконуємо запит до бази даних для додавання користувача
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (FirstName, LastName, Email, PasswordHash, IsAdmin) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @IsAdmin)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", password);
                        command.Parameters.AddWithValue("@IsAdmin", 0); // Встановлюємо значення 0 для IsAdmin, щоб позначити звичайного користувача

                        command.ExecuteNonQuery();
                    }
                }

                // Перехід до головного меню після успішної реєстрації
                MainMenu main = new MainMenu();
                Close();
                main.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при реєстрації: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}
