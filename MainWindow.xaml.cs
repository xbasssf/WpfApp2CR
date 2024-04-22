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

namespace WpfApp2CR
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

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Пожалуйста, введите корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double weight = double.Parse(weightTextBox.Text);
            double height = double.Parse(heightTextBox.Text);
            int age = int.Parse(ageTextBox.Text);

            double bmr = 0;

            if (genderComboBox.SelectedIndex == 0) // Мужской
            {
                bmr = 66 + (13.7 * weight) + (5 * height) - (6.8 * age);
            }
            else // Женский
            {
                bmr = 655 + (9.6 * weight) + (1.8 * height) - (4.7 * age);
            }

            double activityFactor = GetActivityFactor(activityComboBox.SelectedIndex);
            double tdee = bmr * activityFactor;

            resultTextBlock.Text = $"BMR: {bmr:F2} калорий/сутки\nTDEE: {tdee:F2} калорий/сутки";
        }

        private double GetActivityFactor(int index)
        {
            switch (index)
            {
                case 0: return 1.2; // Сидячий
                case 1: return 1.375; // Немного активный
                case 2: return 1.55; // Средняя активность
                case 3: return 1.725; // Большая активность
                case 4: return 1.9; // Экстра нагрузка
                default: return 1.2; // По умолчанию считаем сидячий
            }
        }

        private bool ValidateInput()
        {
            bool valid = true;

            if (!int.TryParse(ageTextBox.Text, out int age) || age < 10 || age > 100)
            {
                valid = false;
                MessageBox.Show("Пожалуйста, введите корректный возраст (10-100 лет).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!double.TryParse(weightTextBox.Text, out double weight) || weight < 10 || weight > 300)
            {
                valid = false;
                MessageBox.Show("Пожалуйста, введите корректный вес (10-300 кг).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!double.TryParse(heightTextBox.Text, out double height) || height < 100 || height > 210)
            {
                valid = false;
                MessageBox.Show("Пожалуйста, введите корректный рост (100-210 см).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return valid;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ageTextBox.Text = "0";
            weightTextBox.Text = "0";
            heightTextBox.Text = "0";
            resultTextBlock.Text = "0";
            genderComboBox.SelectedIndex = 0;
            activityComboBox.SelectedIndex = 0;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
