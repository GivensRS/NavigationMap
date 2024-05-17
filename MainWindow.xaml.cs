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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NavigationMap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FloorButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int floor = int.Parse(clickedButton.Tag.ToString());
            DisplayFloorMap(floor);

            if (floor == 1)
            {
                DisplayOffices();
            }
        }
        private void DisplayFloorMap(int floor)
        {
            // Вывод картинки этажа на главный экран
            string imagePath = $"pack://application:,,,/img/Floor{floor}.jpg";
            MapImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private void DisplayBack_Click(object sender, RoutedEventArgs e)
        {
            ButtonPanel.Children.Clear();

            // Возращаем кнопки выбора этажей
            ButtonPanel.Children.Add(CreateMainMenuButton("Этаж 1", 1));
            ButtonPanel.Children.Add(CreateMainMenuButton("Этаж 2", 2));
            ButtonPanel.Children.Add(CreateMainMenuButton("Этаж 3", 3));
            ButtonPanel.Children.Add(CreateMainMenuButton("Этаж 4", 4));
            ButtonPanel.Children.Add(CreateMainMenuButton("Этаж 5", 5));
            ButtonPanel.Children.Add(CreateMainMenuButton("Этаж 6", 6));

            // Вывод по умолчанию карту первого этажа
            int floor = 1;
            string imagePath = $"pack://application:,,,/img/Floor{floor}.jpg";
            MapImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private Button CreateMainMenuButton(string floorName, int number)
        {
            Button button = new Button();
            button.Content = floorName;
            button.Margin = new Thickness(5);
            button.Click += FloorButton_Click;
            button.Tag = number;
            return button;
        }

        private void DisplayOffices()
        {
            ButtonPanel.Children.Clear();

            // Добавляем кнопки кабинетов
            ButtonPanel.Children.Add(CreateOfficeButton("Кабинет 101"));
            ButtonPanel.Children.Add(CreateOfficeButton("Кабинет 102"));
            ButtonPanel.Children.Add(CreateOfficeButton("Кабинет 103"));
            ButtonPanel.Children.Add(CreateOfficeButton("Кабинет 104"));
            ButtonPanel.Children.Add(CreateOfficeButton("Кабинет 105"));
            ButtonPanel.Children.Add(CreateBackButton());
        }

        private Button CreateOfficeButton(string officeName)
        {
            Button button = new Button();
            button.Content = officeName;
            button.Margin = new Thickness(5);
            button.Click += OfficeButton_Click;
            return button;
        }

        private void OfficeButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string officeName = clickedButton.Content.ToString();

            // Обработчик нажатия кнопки кабинета
            MessageBox.Show($"Вы выбрали {officeName}");
        }

        private Button CreateBackButton()
        {
            Button button = new Button();
            button.Content = "Назад";
            button.Margin = new Thickness(5);
            button.Click += DisplayBack_Click;
            return button;
        }
    }
}
