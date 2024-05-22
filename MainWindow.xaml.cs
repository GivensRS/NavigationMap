using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        int countNext = 1;
        string nextButtonName = string.Empty;
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
        }
        private void DisplayFloorMap(int floor)
        {
            // Вывод картинки этажа на главный экран
            try
            {
                string imagePath = $"pack://application:,,,/img/Floor{floor}.jpg";
                MapImage.Source = new BitmapImage(new Uri(imagePath));
                DisplayOffices(floor);
            }
            catch (Exception)
            {
                MessageBox.Show("Нету фотографий");
            }
        }

        private void DisplayBack_Click(object sender, RoutedEventArgs e)
        {
            Border1.Child = null;

            // Возращаем кнопки выбора этажей
            CreateTableMainMenu();
            BoxButtonPanel.Children.Clear();

            // Вывод по умолчанию карту второго этажа
            int floor = 2;
            string imagePath = $"pack://application:,,,/img/Floor{floor}.jpg";
            MapImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private void CreateTableMainMenu()
        {
            // Создание Grid, который будет содержать строки для центровки кнопок
            Grid mainMenu = new Grid();

            // Создание строк для центровки блока с кнопками
            RowDefinition rowDefinition1 = new RowDefinition();
            RowDefinition rowDefinition2 = new RowDefinition();
            RowDefinition rowDefinition3 = new RowDefinition();

            // Задание относительных высот каждого блока, вторая строка явялется центровочной для блока с кнопками
            rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
            rowDefinition2.Height = new GridLength(2, GridUnitType.Star);
            rowDefinition3.Height = new GridLength(1, GridUnitType.Star);
            mainMenu.RowDefinitions.Add(rowDefinition1);
            mainMenu.RowDefinitions.Add(rowDefinition2);
            mainMenu.RowDefinitions.Add(rowDefinition3);

            // Создание Grid с именем ButtonPanel для помещения внутри кнопок
            Grid ButtonPanel = new Grid();
            Grid.SetRow(ButtonPanel, 1);
            mainMenu.Children.Add(ButtonPanel);

            // Загрузка созданного макета в блок с границами под именем Border1
            Border1.Child = mainMenu;

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
        private void DisplayOffices(int floor)
        {
            Border1.Child = null;

            Grid roomMenu = new Grid();
            RowDefinition rowDefinition1 = new RowDefinition();
            RowDefinition rowDefinition2 = new RowDefinition();
            RowDefinition rowDefinition3 = new RowDefinition();

            rowDefinition1.Height = new GridLength(1, GridUnitType.Star);
            rowDefinition2.Height = new GridLength(6, GridUnitType.Star);
            rowDefinition3.Height = new GridLength(1, GridUnitType.Star);
            roomMenu.RowDefinitions.Add(rowDefinition1);
            roomMenu.RowDefinitions.Add(rowDefinition2);
            roomMenu.RowDefinitions.Add(rowDefinition3);

            Grid ButtonPanel = new Grid();
            Grid.SetRow(ButtonPanel, 1);
            roomMenu.Children.Add(ButtonPanel);

            Border1.Child = roomMenu;


            int roomsCount = 30;
            string[] libRoom = new string[roomsCount];
            for (int i = 1; i <= roomsCount; i++)
            {
                libRoom[i-1] = $"{floor}A{i}";
            }

            // Добавляем кнопки кабинетов
            Grid tableGrid = new Grid();
            Button button = new Button();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(1, GridUnitType.Star);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(1, GridUnitType.Star);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength (1, GridUnitType.Star);

            tableGrid.ColumnDefinitions.Add(col1);
            tableGrid.ColumnDefinitions.Add(col2);
            tableGrid.ColumnDefinitions.Add(col3);

            for (int row = 0;row < libRoom.Length/3+1; row++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                tableGrid.RowDefinitions.Add(rowDefinition);
            }

            int countRoom = 0;

            for (int row = 0; row < libRoom.Length/3+1;row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (countRoom <= libRoom.Length-1)
                    {
                        button = CreateOfficeButton(libRoom[countRoom]);
                        button.Click += OfficeButton_Click;
                    }
                    else
                    {
                        break;
                    }

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    tableGrid.Children.Add(button);
                    countRoom++;
                }
            }

            button = CreateBackButton();
            button.Margin = new Thickness(25);
            Grid.SetRow(button, 2);
            roomMenu.Children.Add(button);

            ButtonPanel.Children.Add(tableGrid);

            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Margin = new Thickness(10);
            textBlock.Text = "It test text in textBlock!";
            textBlock.FontSize = 20;

            Grid.SetRow(textBlock, 0);
            roomMenu.Children.Add(textBlock);
        }
        private Button CreateOfficeButton(string officeName)
        {
            Button button = new Button();
            button.Content = officeName;
            button.Margin = new Thickness(5);
            return button;
        }

        private Button CreateNextButton()
        {
            BoxButtonPanel.Children.Clear();

            string[] buttonName = { "Далее", "Назад" };
            Button button = new Button();
            button.Content = buttonName[countNext-1];
            button.Margin = new Thickness(5);
            button.Click += nextButton_Click;
            if(countNext == 1)
            {
                Grid.SetColumn(button, 3);
                countNext = 2;
            }
            else
            {
                Grid.SetColumn(button, 1);
                countNext = 1;
            }
            return button;
        }

        private Button CreateNextButton(string officeName)
        {
            BoxButtonPanel.Children.Clear();
            Button button = new Button();
            button.Content = "Далее";
            button.Margin = new Thickness(5);
            button.Click += nextButton_Click;
            Grid.SetColumn(button, 3);
            nextButtonName = officeName;
            try
            {
                string imagePath = $"pack://application:,,,/img/office/{nextButtonName}-{countNext}.jpg";
                MapImage.Source = new BitmapImage(new Uri(imagePath));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Картинки нету");
                BoxButtonPanel.Children.Clear();
            }

            countNext = 2;

            return button;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imagePath = $"pack://application:,,,/img/office/{nextButtonName}-{countNext}.jpg";
                MapImage.Source = new BitmapImage(new Uri(imagePath));

                Button button = new Button();
                button = CreateNextButton();
                BoxButtonPanel.Children.Add(button);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Картинки нету");
                BoxButtonPanel.Children.Clear();
            }
        }

        private void OfficeButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            string officeName = clickedButton.Content.ToString();


            Button button = new Button();
            button = CreateNextButton(officeName);
            BoxButtonPanel.Children.Add(button);
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
