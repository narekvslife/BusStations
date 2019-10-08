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
using System.IO;
using Newtonsoft.Json;
using HW3_AL.Core.Model;
using HW3_AL.Core.Migrations;
using HW3_AL.Core.Repositories;
using HW3_AL.Core;

namespace HW3_AL.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User ActiveUser;
        public MainWindow()
        {
            InitializeComponent();

            textBoxEmail.Clear();
            textBoxPassword.Clear();
            textBoxEmail.Focus();
        }

        private void ButtonLogin(object sender, RoutedEventArgs e)
        {
            ActiveUser = Authentication.AuthCheck(textBoxEmail.Text, textBoxPassword.Password);
            if (ActiveUser == null)
            {
                MessageBox.Show("Such Email & Password combination doesn't exist");
                textBoxEmail.Clear();
                textBoxPassword.Clear();
                textBoxEmail.Focus();
            }
            else
            {
                ScheduleWindow Main = new ScheduleWindow(ActiveUser);
                Main.Show();
                Close();
            }
        }

        private void ButtonRegister(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Close();
        }
    }
}

