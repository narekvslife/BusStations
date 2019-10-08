using HW3_AL.Core;
using HW3_AL.Core.Model;
using HW3_AL.Core.ViewModel;
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
using System.Windows.Threading;

namespace HW3_AL.UI
{
    /// <summary>
    /// Логика взаимодействия для ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {

        User ActiveUser;

        public ScheduleWindow(User activeUser)
        {
            InitializeComponent();

            ActiveUser = activeUser;

            List<Route> AllRoutes;
            List<Station> AllStations;
            List<FavouriteStation> UserFavourites;

            using (var unitOfWork = new UnitOfWork())
            {
                User ActiveUserFromDb = unitOfWork.Users.GetAllItems().FirstOrDefault(user => user.Email == ActiveUser.Email);
                UserFavourites = unitOfWork.Favourites.GetAllItems().Where(fav => ActiveUserFromDb.FavouriteStations.Contains(fav)).ToList();

                AllStations = unitOfWork.Stations.GetAllItems().ToList();
                AllRoutes = unitOfWork.Routes.GetAllItems().ToList();
            }

            comboBoxFavouriteStations.DisplayMemberPath = "Station.Name";
            comboBoxFavouriteStations.ItemsSource = UserFavourites;

            comboBoxAllStations.ItemsSource = AllStations;

            comboBoxAllRoutes.ItemsSource = AllRoutes;

            NameBlock.Text = $"{ActiveUser.Name} {ActiveUser.Surname}";
            Time();
        }

        private void RouteSelected(object sender, SelectionChangedEventArgs e)
        {
            Route selectedRoute = comboBoxAllRoutes.SelectedItem as Route;
            using (var unitOfWork = new UnitOfWork())
            {
                var selectedRouteFromDb = unitOfWork.Routes.GetAllItems().FirstOrDefault(route => route.Number == selectedRoute.Number);

                comboBoxAllStations.ItemsSource = unitOfWork.Stations.GetAllItems().Where(station => station.Routes.Contains(selectedRouteFromDb));

                List<Station> allStations= unitOfWork.Stations.GetAllItems().ToList();

                comboBoxFavouriteStations.ItemsSource = unitOfWork.Favourites.GetAllItems().Where(fav=>
                allStations.FirstOrDefault(s=>s==fav.Station).Routes.Contains(selectedRouteFromDb)).ToList();
            }
        }

        private void StationSelected(object sender, SelectionChangedEventArgs e)
        {
            int now = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
            var selectedStation = comboBoxAllStations.SelectedItem as Station;

            var answers = Schedule.GetSchedule(selectedStation, now);
            dataGridMain.ItemsSource = answers;
        }


        private void FavouriteSelected(object sender, SelectionChangedEventArgs e)
        {
            int now = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
            var selectedFavourite = comboBoxFavouriteStations.SelectedItem as FavouriteStation;

            List<ScheduleItem> answers = new List<ScheduleItem>();
            if (selectedFavourite != null)
            {
                answers = Schedule.GetSchedule(selectedFavourite.Station, now);

                dataGridMain.ItemsSource = answers;
            }
        }


        private void EditFavouritesClick(object sender, RoutedEventArgs e)
        {
            UserFavouriteWindow userFavourite = new UserFavouriteWindow(ActiveUser);
            userFavourite.Show();
            Close();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            MainWindow Login = new MainWindow();
            Login.Show();
            Close();
        }

        private void clock_Change(object sender, EventArgs e)
        { TimeBLock.Text = $"Current time : {DateTime.Now.ToLongTimeString()}"; }
        private void Time()
        {
            DispatcherTimer clock = new DispatcherTimer();

            clock.Interval = TimeSpan.FromMilliseconds(500); clock.Tick += clock_Change;

            clock.Start();
        }
    }
}

