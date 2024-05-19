using System;
using System.Collections.Generic;
using System.Globalization;
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

            // Вывод по умолчанию карту второго этажа
            string imagePath = $"pack://application:,,,/img/Floor2.jpg";
            MapImage.Source = new BitmapImage(new Uri(imagePath));
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
            try
            {
                string imagePath = $"pack://application:,,,/img/Floor{floor}.jpg";
                MapImage.Source = new BitmapImage(new Uri(imagePath));
            }
            catch (Exception)
            {
                MessageBox.Show("Нету фотографий");
            }
        }

        private void DisplayBack_Click(object sender, RoutedEventArgs e)
        {
            ButtonPanel.Children.Clear();

            // Возращаем кнопки выбора этажей
            CreateTableMainMenu();

            // Вывод по умолчанию карту второго этажа
            int floor = 2;
            string imagePath = $"pack://application:,,,/img/Floor{floor}.jpg";
            MapImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private void CreateTableMainMenu()
        {
            // Создаем Grid
            Grid tableGrid = new Grid();

            // Добавляем строки
            for (int i = 0; i < 6; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star); // Высота каждой строки 1/6 Grid ButtonPanel
                tableGrid.RowDefinitions.Add(row);
            }

            // Добавляем элементы в ячейки таблицы
            for (int row = 0; row < 6; row++)
            {
                Button button = new Button();
                button.Content = $"Этаж {row + 1}";
                button.Margin = new Thickness(5);
                button.Click += FloorButton_Click;
                button.Tag = row + 1;

                Grid.SetRow(button, row);
                tableGrid.Children.Add(button);
            }

            // Добавляем Grid в основное окно
            ButtonPanel.Children.Add(tableGrid);
        }
        private void DisplayOffices()
        {
            ButtonPanel.Children.Clear();
            int back = 0;

            string room = "1А1 1А2 1А3 1А4";
            string[] libRoom = room.Split(' ');

            // Добавляем кнопки кабинетов
            Grid tableGrid = new Grid();
            Button button = new Button();
            
            for (int row = 0;row < libRoom.Length + 1; row++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                tableGrid.RowDefinitions.Add(rowDefinition);
            }

            for (int row = 0; row < libRoom.Length;row++)
            {
                button = CreateOfficeButton(libRoom[row]);

                Grid.SetRow(button, row);
                tableGrid.Children.Add(button);
                back = row;
            }

            button = CreateBackButton();
            Grid.SetRow(button, back + 1);
            tableGrid.Children.Add(button);

            ButtonPanel.Children.Add(tableGrid);
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
