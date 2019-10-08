using HW3_AL.Core;
using HW3_AL.Core.Model;
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
    /// Логика взаимодействия для AddFavouritesWindow.xaml
    /// </summary>
    public partial class AddFavouritesWindow : Window
    {
        User ActiveUser;
        public AddFavouritesWindow(User activeUser)
        {
            InitializeComponent();
            ActiveUser = activeUser;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ActiveUser = unitOfWork.Users.GetAllItems().FirstOrDefault(user => user.Email == ActiveUser.Email);
                listBoxAllStations.ItemsSource = unitOfWork.Stations.GetAllItems().ToList(); 
            }
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            var selectedStation = listBoxAllStations.SelectedItem as Station;

            if (selectedStation == null)
                MessageBox.Show("Select a station");
            else if (ActiveUser.FavouriteStations.Any(f => f.Station.Name == selectedStation.Name))
            {
                MessageBox.Show("Chosen Station is already in the favourites");
            }
            else
            {
                var result = MessageBox.Show($"Would you really like to add {selectedStation.Name} station to your favourites?",
                        "Confirm", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    using (UnitOfWork UOW = new UnitOfWork())
                    {
                        UOW.Users.AddUserFavourite(ActiveUser, selectedStation );
                        UOW.Complete();
                    }
                }
                UserFavouriteWindow userFavouriteWindow = new UserFavouriteWindow(ActiveUser);
                userFavouriteWindow.Show();
                Close();
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            UserFavouriteWindow userFavouriteWindow = new UserFavouriteWindow(ActiveUser);
            userFavouriteWindow.Show();
            Close();
        }
    }
}
