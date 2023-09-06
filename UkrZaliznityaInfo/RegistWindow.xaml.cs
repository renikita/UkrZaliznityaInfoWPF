using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace UkrZaliznityaInfo
{
    /// <summary>
    /// Логика взаимодействия для RegistWindow.xaml
    /// </summary>
    /// 
    public enum TranslateStatus
    {
        Ukraine,
        English
        
    }

    public partial class RegistWindow : Window
    {
        string connectionString = @"Data Source=DESKTOP-SMDODNO\SQLEXPRESS;Initial Catalog=UkrZaliznityaInfo;Integrated Security=True;";


        private TranslateStatus TStatus;
        private const string SettingsFilePath = "settings.settings";
        private Button lastClickedButton = null;
        public RegistWindow()
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

       
        public void RegShow()
        {

        }

        

        private void AmountSymbols(object sender, KeyEventArgs e)
        {
              EmailText.Visibility = MailBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            if(PassBox.Visibility == Visibility.Visible)
              PassText.Visibility = PassBox.Password.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            else
            PassText.Visibility = PassVisibleBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }
        //private bool CheckButton = false;
        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string email = MailBox.Text;
            string password;
            if (PassBox.Password != "")
            {
                password = PassBox.Password;
            }
            else
            {
                password = PassVisibleBox.Text;
            }
            // Виконання запиту до бази даних для перевірки введених даних
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); // Асинхронне відкриття підключення до бази даних

                string query = "SELECT COUNT(*), IsAdmin FROM Users WHERE Email = @Email AND PasswordHash = @PasswordHash GROUP BY IsAdmin";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", password);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) // Асинхронне виконання запиту та отримання результату
                    {
                        if (reader.Read())
                        {
                            int count = reader.GetInt32(0);
                            bool isAdmin = reader.GetBoolean(1);

                            if (count > 0)
                            {
                                // Користувач з такою поштою та паролем знайдений

                                if (isAdmin)
                                {
                                    // Відкрити додаткове меню для адміністратора
                                    AdminMenu adminMenu = new AdminMenu();
                                    MainMenu mainMenu = new MainMenu();
                                    Close();
                                    mainMenu.Show();
                                    adminMenu.Show();
                                }
                                else
                                {
                                    // Відкрити головне меню
                                    MainMenu mainMenu = new MainMenu();
                                    Close();
                                    mainMenu.Show();
                                }
                            }
                            else
                            {
                                // Помилка авторизації
                                MessageBox.Show("Неправильна пошта або пароль", "Помилка авторизації", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }




        private void ClosedMain(object sender, EventArgs e)
        {
            // Виключення всіх вікон
            
            

        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            // Увійти у реєстрацію Юзера
          RegistrationWindow registNewUser = new RegistrationWindow();
            registNewUser.Show();
            //CheckButton = true;
            Close();
            //CheckButton = false;

        }

        

        private void ClickBoxPass(object sender, RoutedEventArgs e)
        {
           // Зникнення текст "Пароль"
            PassText.Visibility = Visibility.Hidden;
        }

        private void ExitClickBoxPass(object sender, RoutedEventArgs e)
        {
            // Зникнення паролю
            if (PassBox.Visibility == Visibility.Visible)
                PassText.Visibility = PassBox.Password.Length != 0 ? Visibility.Hidden : Visibility.Visible;
            else
                PassText.Visibility = PassVisibleBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ClickBoxEmail(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = Visibility.Hidden;
        }

        private void ExitClickBoxEmail(object sender, RoutedEventArgs e)
        {
            EmailText.Visibility = MailBox.Text.Length != 0 ? Visibility.Hidden : Visibility.Visible;
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
        private void TranslateUpdate()
        {
            if(TStatus == TranslateStatus.Ukraine)
            {
                TranslateUkraine();
            }
            else if(TStatus == TranslateStatus.English)
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
            Auth_Text.Text = "Authorization";
            PassText.Text = "Passwrd";
            SignIn.Content = "Sign In";
            OR_Text.Text = "OR";
            SignUp.Content = "Sign Up";
            ForgetPass.Content = "Forgot your password? Click here to recover it";
        }
        private void TranslateUkraine()
        {
            slogan.Text = "Купуйте квитки на потяги -\n легко та швидко!";
            Auth_Text.Text = "Авторизація";
            PassText.Text = "Пароль";
            SignIn.Content = "Увійти";
            OR_Text.Text = "АБО";
            SignUp.Content = "Реєстрація";
            ForgetPass.Content = "Забули свій пароль? Нажміть тут, щоб відновити його";
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            lastClickedButton = (Button)sender; // Збереження натиснутої кнопки
            TStatus = TranslateStatus.English;
            TranslateUpdate();
            SaveLanguageSettings();
        }
        private void SaveLanguageSettings()
        {
            using (FileStream fs = new FileStream(SettingsFilePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TranslateStatus));
                serializer.Serialize(fs, TStatus);
            }
        }

        private void ForgetPass_Click(object sender, RoutedEventArgs e)
        {
            MainMenu main = new MainMenu();
            Close();
            main.Show();
        }
    }
}
