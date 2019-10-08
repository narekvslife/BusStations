using HW3_AL.Core.Logic;
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

namespace HW3_AL.UI
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void RegistrationCancelClick(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void RegistrationRegClick(object sender, RoutedEventArgs e)
        {
            if (RegistrationNameBox.Text.Count()>15
                || RegistrationSurnameBox.Text.Count()>15
                || RegistrationEmailBox.Text.Count()>15)
            {
                MessageBox.Show("Your name/surname must be less then 20 characters");
                RegistrationSurnameBox.Clear();
                RegistrationNameBox.Clear();
                return;
            }
            bool flag = Registration.NewUser(RegistrationNameBox.Text, RegistrationSurnameBox.Text,
                RegistrationEmailBox.Text, RegistrationPasswordBox.Password);
            if (flag)
            {
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                Close();
            }
                
        }
    }
}

