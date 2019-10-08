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
    /// Логика взаимодействия для EditUserFavourite.xaml
    /// </summary>
    public partial class EditUserFavourite : Window
    {
        FavouriteStation FavouriteStation;
        User ActiveUser;
        public EditUserFavourite(User currentUser, FavouriteStation favouriteStation)
        {
            InitializeComponent();

            textBoxDescription.Focus();
            using (var unitOfWork = new UnitOfWork())
            {
                FavouriteStation = unitOfWork.Favourites.GetAllItems().FirstOrDefault(fav=>fav.Id==favouriteStation.Id);
            }
            FavouriteStation = favouriteStation;
            ActiveUser = currentUser;

            textBlockName.Text = favouriteStation.Station.Name;

            textBoxDescription.Text = favouriteStation.Description ?? "";
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            if (textBoxDescription.Text.Count()>15)
            {
                MessageBox.Show("Your description must be less then 15 characters");
                textBoxDescription.Clear();
                return;
            }
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.Users.EditUserFavourite(ActiveUser,FavouriteStation, textBoxDescription.Text);
            }
            UserFavouriteWindow userFavouriteWindow = new UserFavouriteWindow(ActiveUser);
            userFavouriteWindow.Show();
            Close();
        }

        private void CancelCLick(object sender, RoutedEventArgs e)
        {
            UserFavouriteWindow userFavouriteWindow = new UserFavouriteWindow(ActiveUser);
            userFavouriteWindow.Show();
            Close();
        }
    }
}