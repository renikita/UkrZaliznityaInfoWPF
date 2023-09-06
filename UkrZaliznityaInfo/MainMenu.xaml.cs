using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using UkrZaliznityaInfo;

namespace UkrZaliznityaInfo
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 

    


    public partial class MainMenu : Window
    {
       
        public enum SystemStatus
        {
            MainMenu,
            BuyMenu,
            TimeTableMenu,
            HistoryMenu,
            AccMenu,
            NewsMenu
        }
        private bool ShowTutorial = false;
        private SystemStatus status;
        private TranslateStatus TStatus;
        private Button lastClickedButton = null;
        string connectionString = @"Data Source=DESKTOP-SMDODNO\SQLEXPRESS;Initial Catalog=UkrZaliznityaInfo;Integrated Security=True;";
        private const string SettingsFilePath = "settings.settings";
       
        public MainMenu()
        {
            InitializeComponent();

            UpdateStatus();
            LoadLanguageSettings();

            FromComboBox.SelectionChanged += ComboBox_SelectionChanged;
            ToComboBox.SelectionChanged += ComboBox_SelectionChanged;
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

      


        private void MMButton_Click(object sender, RoutedEventArgs e)
        {
            status = SystemStatus.MainMenu;
            
            UpdateStatus();

        }

       private void DeleteElMenu()
        {
            BANNER1.Visibility = Visibility.Collapsed;
            BANNER2.Visibility = Visibility.Collapsed;
            BANNER3.Visibility = Visibility.Collapsed;
            BANNER4.Visibility = Visibility.Collapsed;
            BANNER5.Visibility = Visibility.Collapsed;
            BANNER6.Visibility = Visibility.Collapsed;
            PLACEHOLDER1.Visibility = Visibility.Collapsed;
            PLACEHOLDER2.Visibility = Visibility.Collapsed;
            PLACEHOLDER3.Visibility = Visibility.Collapsed;
            PLACEHOLDER4.Visibility = Visibility.Collapsed;
            PLACEHOLDER5.Visibility = Visibility.Collapsed;
            PLACEHOLDER6.Visibility = Visibility.Collapsed;
            LINEBANNER1.Visibility = Visibility.Collapsed;
            LINEBANNER2.Visibility = Visibility.Collapsed;
            LINEBANNER3.Visibility = Visibility.Collapsed;
            LINEBANNER4.Visibility = Visibility.Collapsed;
            LINEBANNER5.Visibility = Visibility.Collapsed;
            LINEBANNER6.Visibility = Visibility.Collapsed;
            MTextBanner1.Visibility = Visibility.Collapsed;
            LTextBanner1.Visibility = Visibility.Collapsed;
            DateBanner1.Visibility = Visibility.Collapsed;
            MTextBanner2.Visibility = Visibility.Collapsed;
            LTextBanner2.Visibility = Visibility.Collapsed;
            DateBanner2.Visibility = Visibility.Collapsed;
            MTextBanner3.Visibility = Visibility.Collapsed;
            LTextBanner3.Visibility = Visibility.Collapsed;
            DateBanner3.Visibility = Visibility.Collapsed;
            MTextBanner4.Visibility = Visibility.Collapsed;
            LTextBanner4.Visibility = Visibility.Collapsed;
            DateBanner4.Visibility = Visibility.Collapsed;
            MTextBanner5.Visibility = Visibility.Collapsed;
            LTextBanner5.Visibility = Visibility.Collapsed;
            DateBanner5.Visibility = Visibility.Collapsed;
            MTextBanner6.Visibility = Visibility.Collapsed;
            LTextBanner6.Visibility = Visibility.Collapsed;
            DateBanner6.Visibility = Visibility.Collapsed;
            BANNER_BTN1.Visibility = Visibility.Collapsed;
            BANNER_BTN2.Visibility = Visibility.Collapsed;
            BANNER_BTN3.Visibility = Visibility.Collapsed;
            BANNER_BTN4.Visibility = Visibility.Collapsed;
            BANNER_BTN5.Visibility = Visibility.Collapsed;
            BANNER_BTN6.Visibility = Visibility.Collapsed;
        }

        private void DeleteElBuyMenu()
        {
            Rout_Image.Visibility = Visibility.Collapsed;
            FromWB.Visibility = Visibility.Collapsed;
            ToWB.Visibility = Visibility.Collapsed;
            DateWB.Visibility = Visibility.Collapsed;
            ZuruckWB.Visibility = Visibility.Collapsed;
            ArrowFrom.Visibility = Visibility.Collapsed;
            ArrowTo.Visibility = Visibility.Collapsed;
            ArrowDate.Visibility = Visibility.Collapsed;
            ArrowDirection.Visibility = Visibility.Collapsed;
            PlusBtn.Visibility = Visibility.Collapsed;
            PlusBtn_Btn.Visibility = Visibility.Collapsed;
            Underline_BuyMenu.Visibility = Visibility.Collapsed;
            Image_Btn.Visibility = Visibility.Collapsed;
            FindBT.Visibility = Visibility.Collapsed;
            FromText.Visibility = Visibility.Collapsed;
            ToText.Visibility = Visibility.Collapsed;
            DataText.Visibility = Visibility.Collapsed;
            ZuruckText.Visibility = Visibility.Collapsed;
            FromComboBox.Visibility = Visibility.Collapsed;
            ToComboBox.Visibility = Visibility.Collapsed;
            DateComboBox.Visibility = Visibility.Collapsed;
            TutorialForm.Visibility = Visibility.Collapsed;
            Tutoriali.Visibility = Visibility.Collapsed;
            TutorialText.Visibility = Visibility.Collapsed;
        }

        private void DeleteElNewsMenu()
        {

        }

        private void DeleteElHistoryMenu()
        {
            HistoryTable.Visibility = Visibility.Collapsed;
        }

        private void DeleteElAccMenu()
        {
            AccInfoText.Visibility = Visibility.Collapsed;
            NameAndSurname.Visibility = Visibility.Collapsed;
            NameAndSurnameBD.Visibility = Visibility.Collapsed;
            Email.Visibility = Visibility.Collapsed;
            EmailBD.Visibility = Visibility.Collapsed;
            Password.Visibility = Visibility.Collapsed;
            Image_Btn2.Visibility = Visibility.Collapsed;
            ChangePassBT.Visibility = Visibility.Collapsed;
            Status.Visibility = Visibility.Collapsed;
            StatusUser.Visibility = Visibility.Collapsed;
            Image_CreditBtn.Visibility = Visibility.Collapsed;
            CreditBTN.Visibility = Visibility.Collapsed;
        }

        // UPDATE STATUS UPDATE STATUS UPDATE STATUS UPDATE STATUS UPDATE STATUS UPDATE STATUS UPDATE STATUS UPDATE STATUS 
        private void UpdateStatus()
        {
            DeleteElMenu();
            DeleteElBuyMenu();
            DeleteElNewsMenu();
            DeleteElAccMenu();
            DeleteElHistoryMenu();
            
            TimeTable.Visibility = Visibility.Collapsed;
            if (status == SystemStatus.MainMenu)
            {
                MainMenuUpdate();
            }
            else if(status == SystemStatus.BuyMenu)
            {
                BuyMenuUpdate();
            }
            else if(status == SystemStatus.TimeTableMenu)
            {
                TimeTableMenuUpdate();
            }
            else if (status == SystemStatus.HistoryMenu)
            {
                HistoryMenuUpdate();
            }
            else if(status == SystemStatus.AccMenu)
            {
                AccMenuUpdate();
            }
            else if(status == SystemStatus.NewsMenu)
            {
                NewsMenuUpdate(); 
            }
            else
            {
                MessageBox.Show("Помилка: невідомий статус.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void AccMenuUpdate()
        {
            var LINEUPDElements = new[] { LINEUPD2, LINEUPD1, LINEUPD3, LINEUPD4 };

            foreach (var element in LINEUPDElements)
            {
                element.Visibility = Visibility.Hidden;
            }

            LINEUPD5.Visibility = Visibility.Visible;

            string firstName = "";
            string lastName = "";
            string email = "";

            // Виконати запит до бази даних та отримати значення імені та прізвища
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TOP (1) [FirstName], [LastName], [Email] FROM [UkrZaliznityaInfo].[dbo].[Users]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            firstName = reader.GetString(0);
                            lastName = reader.GetString(1);
                            email = reader.GetString(2);
                        }
                    }
                }
            

            // Встановити значення імені, прізвища та пошти у відповідні TextBlock
            NameAndSurnameBD.Text = $"{firstName} {lastName}";
            EmailBD.Text = email;
            AddElAccMenu();
            }
        }

       private void AddElAccMenu()
        {
            AccInfoText.Visibility = Visibility.Visible;
            NameAndSurname.Visibility = Visibility.Visible;
            NameAndSurnameBD.Visibility = Visibility.Visible;
            Email.Visibility = Visibility.Visible;
            EmailBD.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Visible;
            Image_Btn2.Visibility = Visibility.Visible;
            ChangePassBT.Visibility = Visibility.Visible;
            Status.Visibility = Visibility.Visible;
            StatusUser.Visibility = Visibility.Visible;
            Image_CreditBtn.Visibility = Visibility.Visible;
            CreditBTN.Visibility = Visibility.Visible;
        }

        private void NewsMenuUpdate()
        {
            DeleteElMenu();
            AddElNewsMenu();
        }

        private void AddElNewsMenu()
        {
            string description = string.Empty;

            switch (buttonName)
            {
                case "BANNER_BTN1":
                    description = GetNewsDescription(0);
                    break;
                case "BANNER_BTN2":
                    description = GetNewsDescription(1);
                    break;
                case "BANNER_BTN3":
                    description = GetNewsDescription(2);
                    break;
                case "BANNER_BTN4":
                    description = GetNewsDescription(3);
                    break;
                case "BANNER_BTN5":
                    description = GetNewsDescription(4);
                    break;
                case "BANNER_BTN6":
                    description = GetNewsDescription(5);
                    break;
                default:
                    break;
            }

            Discription.Text = description;

        }

        private string GetNewsDescription(int index)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Description2 FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNum, Description2 FROM News) AS SubQuery WHERE RowNum = @Index";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Index", index);

                string description = string.Empty;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        description = reader["Description2"].ToString();
                    }
                }

                connection.Close();

                return description;
            }
        }
        private void HistoryMenuUpdate()
        {
            var LINEUPDElements = new[] { LINEUPD2, LINEUPD1, LINEUPD3, LINEUPD5 };

            foreach (var element in LINEUPDElements)
            {
                element.Visibility = Visibility.Hidden;
            }

            LINEUPD4.Visibility = Visibility.Visible;
            HistoryTable.Visibility = Visibility.Visible;
        }
    
        private void TimeTableMenuUpdate()
        {
            var LINEUPDElements = new[] { LINEUPD2, LINEUPD1, LINEUPD4, LINEUPD5 };

            foreach (var element in LINEUPDElements)
            {
                element.Visibility = Visibility.Hidden;
            }

            LINEUPD3.Visibility = Visibility.Visible;
            TimeTable.Visibility = Visibility.Visible;
        }

       private void MainMenuUpdate()
        {
            var LINEUPDElements = new[] { LINEUPD2, LINEUPD3, LINEUPD4, LINEUPD5 };

            foreach (var element in LINEUPDElements)
            {
                element.Visibility = Visibility.Hidden;
            }

            LINEUPD1.Visibility = Visibility.Visible;

            AddElMenu();
        }
        private void AddElMenu()
        {
            BANNER1.Visibility = Visibility.Visible;
            BANNER2.Visibility = Visibility.Visible;
            BANNER3.Visibility = Visibility.Visible;
            BANNER4.Visibility = Visibility.Visible;
            BANNER5.Visibility = Visibility.Visible;
            BANNER6.Visibility = Visibility.Visible;
            PLACEHOLDER1.Visibility = Visibility.Visible;
            PLACEHOLDER2.Visibility = Visibility.Visible;
            PLACEHOLDER3.Visibility = Visibility.Visible;
            PLACEHOLDER4.Visibility = Visibility.Visible;
            PLACEHOLDER5.Visibility = Visibility.Visible;
            PLACEHOLDER6.Visibility = Visibility.Visible;
            LINEBANNER1.Visibility = Visibility.Visible;
            LINEBANNER2.Visibility = Visibility.Visible;
            LINEBANNER3.Visibility = Visibility.Visible;
            LINEBANNER4.Visibility = Visibility.Visible;
            LINEBANNER5.Visibility = Visibility.Visible;
            LINEBANNER6.Visibility = Visibility.Visible;
            MTextBanner1.Visibility = Visibility.Visible;
            LTextBanner1.Visibility = Visibility.Visible;
            DateBanner1.Visibility = Visibility.Visible;
            MTextBanner2.Visibility = Visibility.Visible;
            LTextBanner2.Visibility = Visibility.Visible;
            DateBanner2.Visibility = Visibility.Visible;
            MTextBanner3.Visibility = Visibility.Visible;
            LTextBanner3.Visibility = Visibility.Visible;
            DateBanner3.Visibility = Visibility.Visible;
            MTextBanner4.Visibility = Visibility.Visible;
            LTextBanner4.Visibility = Visibility.Visible;
            DateBanner4.Visibility = Visibility.Visible;
            MTextBanner5.Visibility = Visibility.Visible;
            LTextBanner5.Visibility = Visibility.Visible;
            DateBanner5.Visibility = Visibility.Visible;
            MTextBanner6.Visibility = Visibility.Visible;
            LTextBanner6.Visibility = Visibility.Visible;
            DateBanner6.Visibility = Visibility.Visible;
            BANNER_BTN1.Visibility = Visibility.Visible;
            BANNER_BTN2.Visibility = Visibility.Visible; 
            BANNER_BTN3.Visibility = Visibility.Visible;
            BANNER_BTN4.Visibility = Visibility.Visible;
            BANNER_BTN5.Visibility = Visibility.Visible;
            BANNER_BTN6.Visibility = Visibility.Visible;
        }

        private bool ignoreComboBoxSelectionChanged = false;
        private void BuyMenuUpdate()
        {
            var LINEUPDElements = new[] { LINEUPD1, LINEUPD3, LINEUPD4, LINEUPD5 };

            foreach (var element in LINEUPDElements)
            {
                element.Visibility = Visibility.Hidden;
            }

            LINEUPD2.Visibility = Visibility.Visible;

            ignoreComboBoxSelectionChanged = true;
            LoadDBStation();
            ignoreComboBoxSelectionChanged = false;

            Tutorial();
            AddElBuyMenu();
        }

        private void AddElBuyMenu()
        {
            Rout_Image.Visibility = Visibility.Visible;
            FromWB.Visibility = Visibility.Visible;
            ToWB.Visibility = Visibility.Visible;
            DateWB.Visibility = Visibility.Visible;
            ZuruckWB.Visibility = Visibility.Visible;
            ArrowFrom.Visibility = Visibility.Visible;
            ArrowTo.Visibility = Visibility.Visible;
            ArrowDate.Visibility = Visibility.Visible;
            ArrowDirection.Visibility = Visibility.Visible;
            PlusBtn.Visibility = Visibility.Visible;
            PlusBtn_Btn.Visibility = Visibility.Visible;
            Underline_BuyMenu.Visibility = Visibility.Visible;
            Image_Btn.Visibility = Visibility.Visible;
            FindBT.Visibility = Visibility.Visible;
            FromText.Visibility = Visibility.Visible;
            ToText.Visibility = Visibility.Visible;
            DataText.Visibility = Visibility.Visible;
            ZuruckText.Visibility = Visibility.Visible;
            FromComboBox.Visibility = Visibility.Visible;
            ToComboBox.Visibility = Visibility.Visible;
            DateComboBox.Visibility = Visibility.Visible;
        }

        private void Tutorial()
        {
            if (ShowTutorial == false)
            {
                TutorialForm.Visibility = Visibility.Visible;
                Tutoriali.Visibility = Visibility.Visible;
                TutorialText.Visibility = Visibility.Visible;
            }
        }

        private void LoadDBStation()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (UkrainianL == true)
                    {
                        string query = "SELECT Name FROM Stations";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Очищення Combobox перед заповненням
                    FromComboBox.Items.Clear();
                    ToComboBox.Items.Clear();

                  
                        // Додавання значень до Combobox
                        while (reader.Read())
                        {
                            string stationName = reader["Name"].ToString();
                            FromComboBox.Items.Add(stationName);
                            ToComboBox.Items.Add(stationName);

                        }
                        reader.Close();
                    }
                    else
                    {
                        string query = "SELECT EnglishName FROM Stations";
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        FromComboBox.Items.Clear();
                        ToComboBox.Items.Clear();
                        // Додавання значень до Combobox
                        while (reader.Read())
                        {
                            string stationName = reader["EnglishName"].ToString();
                            FromComboBox.Items.Add(stationName);
                            ToComboBox.Items.Add(stationName);
                        }
                        reader.Close();
                    }

                    // Встановлення початкових значень вибраних елементів
                    if (FromComboBox.Items.Count > 0)
                        FromComboBox.SelectedIndex = 0;
                    if (ToComboBox.Items.Count > 1)
                        ToComboBox.SelectedIndex = 1;

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час запиту до бази даних: {ex.Message}");
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            status = SystemStatus.BuyMenu;
            UpdateStatus();
        }

        private void TimeTableButton_Click(object sender, RoutedEventArgs e)
        {
            status = SystemStatus.TimeTableMenu;
            UpdateStatus();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            status = SystemStatus.HistoryMenu;
            UpdateStatus();
        }

        private void AccButton_Click(object sender, RoutedEventArgs e)
        {
            status = SystemStatus.AccMenu;
            UpdateStatus();
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
        private bool UkrainianL = true;
        private void TranslateEnglish()
        {
            UkrainianL = false;
            MMText.Text = "Home";
            BuyText.Text = "Buy Ticket";
            TimeTableText.Text = "Train Schedule";
            HistoryText.Text = "Trip History";
            AccText.Text = "Account";
            TutorialText.Text = "To select or cancel a return ticket\n click the '+' button.";
            FromText.Text = "From";
            ToText.Text = "To";
            DataText.Text = "Date";
            ZuruckText.Text = "Return Ticket";
            FindBT.Content = "Find";
        }

        private void TranslateUkraine()
        {
            UkrainianL = true;
            MMText.Text = "Головна";
            BuyText.Text = "Придбати квиток";
            TimeTableText.Text = "Розклад потягів";
            HistoryText.Text = "Історія поїздок";
            AccText.Text = "Обліковий запис";
            TutorialText.Text = "Щоб вибрати або скасувати\n зворотний квиток натисніть кнопку '+'";
            FromText.Text = "Звідки";
            ToText.Text = "Куди";
            DataText.Text = "Дата";
            ZuruckText.Text = "Зворотний квиток";
            FindBT.Content = "Знайти";

        }
            private void English_Click(object sender, RoutedEventArgs e)
        {
            lastClickedButton = (Button)sender; // Збереження натиснутої кнопки
            TStatus = TranslateStatus.English;
            TranslateUpdate();
            SaveLanguageSettings();
        }

        private void bannerbtn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetNews(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT TOP 6 Title FROM News";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    List<string> titles = new List<string>();
                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        title = title.Replace("\\n", Environment.NewLine); // Замінити символи нового рядка
                        titles.Add(title);
                    }

                    reader.Close();

                    for (int i = 0; i < titles.Count; i++)
                    {
                        TextBlock mTextBanner = (TextBlock)this.FindName($"MTextBanner{i + 1}");
                        if (mTextBanner != null)
                        {
                            mTextBanner.Text = titles[i];
                        }
                    }

                    query = "SELECT TOP 6 Description1 FROM News";
                    command = new SqlCommand(query, connection);
                    SqlDataReader secondReader = command.ExecuteReader();

                    List<string> descr = new List<string>();
                    while (secondReader.Read())
                    {
                        string description = secondReader["Description1"].ToString();
                        description = description.Replace("\\n", Environment.NewLine); // Замінити символи нового рядка
                        descr.Add(description);
                    }

                    secondReader.Close();

                    for (int i = 0; i < descr.Count; i++)
                    {
                        TextBlock LTextBanner = (TextBlock)this.FindName($"LTextBanner{i + 1}");
                        if (LTextBanner != null)
                        {
                            LTextBanner.Text = descr[i];
                        }
                    }

                    query = "SELECT TOP 6 TimeDate FROM News";
                    command = new SqlCommand(query, connection);
                    SqlDataReader thirdReader = command.ExecuteReader();

                    List<string> timed = new List<string>();
                    while (thirdReader.Read())
                    {
                        string timedate = thirdReader["TimeDate"].ToString();
                        timedate = timedate.Replace("\\n", Environment.NewLine); // Замінити символи нового рядка
                        timed.Add(timedate);
                    }

                    thirdReader.Close();

                    for (int i = 0; i < timed.Count; i++)
                    {
                        TextBlock DateBanner = (TextBlock)this.FindName($"DateBanner{i + 1}");
                        if (DateBanner != null)
                        {
                            DateBanner.Text = timed[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час запиту до бази даних: {ex.Message}");
            }






            //var text = new[] { MTextBanner1, MTextBanner2, MTextBanner3, MTextBanner4, MTextBanner5, MTextBanner6};

            //foreach (var element in text)
            // {
            //element.Text = 
            //  }


        }

        

       
     
       
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ignoreComboBoxSelectionChanged)
                return;
            if (FromComboBox.SelectedItem is ComboBoxItem selectedFromItem &&
                ToComboBox.SelectedItem is ComboBoxItem selectedToItem)
            {
                if (selectedFromItem.Content.ToString() == selectedToItem.Content.ToString())
                {
                    // Показати повідомлення про помилку
                    MessageBox.Show("Помилка: Обрані значення дублюються.");

                    // Скинути значення до початкового стану
                    ignoreComboBoxSelectionChanged = true;
                    FromComboBox.SelectedItem = FromComboBox.Items[0];
                    ToComboBox.SelectedItem = ToComboBox.Items[1];
                    ignoreComboBoxSelectionChanged = false;
                }
            }
        }

        private void CreateDateComboBox(object sender, EventArgs e)
        {
            // Очищення ComboBox перед заповненням
            DateComboBox.Items.Clear();

            // Отримання поточної дати
            DateTime currentDate = DateTime.Now;

            // Визначення дати початку і кінця для вибору
            DateTime startDate = currentDate.AddDays(1); // Починаємо з наступного дня
            DateTime endDate = currentDate.AddMonths(2); // Додаємо 2 місяці до поточної дати

            // Додавання дат до ComboBox від початкової дати до кінцевої дати
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                DateComboBox.Items.Add(date.ToShortDateString());
            }

            // Встановлення початкового значення вибраного елемента
            if (DateComboBox.Items.Count > 0)
                DateComboBox.SelectedIndex = 0;
        }

        private void KillTextDate(object sender, RoutedEventArgs e)
        {
            DataText.Visibility = Visibility.Collapsed;
        }

        private bool KeepPlusActive = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(KeepPlusActive == false)
            {
                PlusBtn.Opacity = 1;
                ZuruckText.TextDecorations = null;
                KeepPlusActive = true;
            }
            else
            {
                PlusBtn.Opacity = 0.3;
                ZuruckText.TextDecorations = TextDecorations.Strikethrough;
                KeepPlusActive = false;
            }
        }

        private void KillAllTutorial(object sender, MouseButtonEventArgs e)
        {
            
            TutorialForm.Visibility = Visibility.Collapsed;
            Tutoriali.Visibility = Visibility.Collapsed;
            TutorialText.Visibility = Visibility.Collapsed;
            ShowTutorial = true;
        }

        private string buttonName;

        private void NewsClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender; 

           buttonName = clickedButton.Name;

            status = SystemStatus.NewsMenu;

            UpdateStatus();
        }

        private void ChangePassBT_Click(object sender, RoutedEventArgs e)
        {
            string message = "Акаунт був зареєстрований 2 місяці тому. Відказано.";
            MessageBox.Show(message);
        }

        private void CreditBTN_Click(object sender, RoutedEventArgs e)
        {
            string message = "Павлик Нікіта:\n\n" +
"- Front-end розробка: відповідальний за розробку користувацького інтерфейсу та клієнтської частини програми.\n" +
"- Back-end розробка: відповідальний за розробку серверної частини програми, бази даних та логіки взаємодії з ними.\n" +
"- Ідея проекту: знайшов концепцію та визначив основні функціональні можливості проєкту.\n" +
"- Головний у проєкті: керування командою, координація робіт та прийняття стратегічних рішень.\n\n" +
"Оксана Щепінська:\n\n" +
"- Дизайн: відповідальна за створення естетичного та привабливого дизайну користувацького інтерфейсу програми.\n\n" +
"Владислав Лисенко:\n\n" +
"- Замовник проекту: особа, що виразила потребу у розробці програмного продукту, здійснює контроль за виконанням вимог та фінансовими питаннями.";
            MessageBox.Show(message);
        }

        private void FindBT_Click(object sender, RoutedEventArgs e)
        {
            string recipient = "";

            // Виконати запит до бази даних для отримання адреси отримувача за його ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [Email] FROM [UkrZaliznityaInfo].[dbo].[Users] WHERE [ID] = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", 5);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipient = reader.GetString(0);
                        }
                    }
                }
            }
            string subject = "Привіт!";
            string body = "Вітаю з успішним входом у систему.";

            SendEmail(recipient, subject, body);
        }

        private void SendEmail(string recipient, string subject, string body)
        {
            string senderEmail = "rtcrazynik@gmail.com"; // Ваша електронна пошта
            string senderPassword = "Renault220"; // Ваш пароль електронної пошти

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.example.com"); // Адреса SMTP-сервера

                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = body;

                smtpServer.Port = 587; // Порт SMTP-сервера (може відрізнятися)
                smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);

                MessageBox.Show("Повідомлення успішно відправлено.", "Успішно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при відправленні повідомлення: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }


}
