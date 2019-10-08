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
    /// Логика взаимодействия для UserFavouriteWindow.xaml
    /// </summary>
    public partial class UserFavouriteWindow : Window
    {
        User ActiveUser;
        public UserFavouriteWindow(User activeUser)
        {
            InitializeComponent();
            List<FavouriteStation> ListViewItemSource;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ActiveUser = unitOfWork.Users.GetAllItems()
                                                .FirstOrDefault(user => user.Email == activeUser.Email);
                ListViewItemSource = unitOfWork.Favourites.GetAllItems().Where(fav => ActiveUser.FavouriteStations.Contains(fav)).ToList();
            }
            lIstViewFavourites.ItemsSource = ListViewItemSource;
        }

        private List<string> MakeListboxInfo()
        {
            List<string> listboxInfo = new List<string>();

            for (int i = 0; i < ActiveUser.FavouriteStations.Count; i++)
            {
                listboxInfo.Add($"'{ActiveUser.FavouriteStations[i].Description}' - " +
                    $"{ActiveUser.FavouriteStations[i].Station.Name}");
            }

            return listboxInfo;
        }

        private void AddFavourite(object sender, RoutedEventArgs e)
        {
            AddFavouritesWindow a1 = new AddFavouritesWindow(ActiveUser);
            a1.Show();
            Close();
        }

        private void EditFavourites(object sender, RoutedEventArgs e)
        {
            var selectedStation = lIstViewFavourites.SelectedItem as FavouriteStation;

            if (selectedStation != null)
            {
                EditUserFavourite editUserFavourite = new EditUserFavourite(ActiveUser, selectedStation);
                editUserFavourite.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Select a station from the list");
            }
        }

        private void DeleteFavourite(object sender, RoutedEventArgs e)
        {
            var selectedStation = lIstViewFavourites.SelectedItem as FavouriteStation;

            if (selectedStation == null)
            {
                MessageBox.Show("Select a station from your favourites");
            }
            else
            {
                var result = MessageBox.Show($"Would you really like to delete {selectedStation.Station.Name} station from your favourites?",
                        "Confirm", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    List<FavouriteStation> favouriteStations;
                    List<FavouriteStation> favouriteStationsWithIncludedStations;
                    using (UnitOfWork unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.Users.DeleteUserFavourite(ActiveUser, selectedStation);
                        unitOfWork.Complete();
                        favouriteStations = unitOfWork.Users.GetAllItems()
                                                           .FirstOrDefault(user => user.Email == ActiveUser.Email).FavouriteStations;
                        favouriteStationsWithIncludedStations = unitOfWork.Favourites.GetAllItems().Where(fav=>favouriteStations.Contains(fav)).ToList();
                    }
                    lIstViewFavourites.ItemsSource = favouriteStationsWithIncludedStations;
                }
            }
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            ScheduleWindow scheduleWindow = new ScheduleWindow(ActiveUser);
            scheduleWindow.Show();
            Close();
        }
    }
}

