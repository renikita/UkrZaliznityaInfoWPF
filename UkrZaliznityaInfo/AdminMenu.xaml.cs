using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;

namespace UkrZaliznityaInfo
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }
        private ObservableCollection<TrainInfo> trainInfoList = new ObservableCollection<TrainInfo>();
        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            // Отримання значень з текстових полів
            string trainNumber = TrainNumberTextBox.Text;
            string connection = ConnectionTextBox.Text;
            string time = TimeTextBox.Text;
            string platform = PlatformTextBox.Text;

            // Створення нового маршруту
            TrainInfo newRoute = new TrainInfo
            {
                Train = trainNumber,
                Connection = connection,
                Time = time,
                Platform = "1"
            };

            // Додавання нового маршруту до джерела даних
            trainInfoList.Add(newRoute);

            // Очистка полів після додавання
            TrainNumberTextBox.Text = "";
            ConnectionTextBox.Text = "";
            TimeTextBox.Text = "";
            PlatformTextBox.Text = "";
            UpdateDataGrid();
            // Відображення повідомлення про успішне додавання маршруту
            MessageBox.Show("Маршрут успішно додано!");
            
        }

        // Оновлення джерела даних у DataGrid
        private void UpdateDataGrid()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.TimeTable.DataContext = trainInfoList;
        }
    }
}

