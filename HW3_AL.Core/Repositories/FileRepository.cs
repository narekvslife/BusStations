using HW3_AL.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_AL.Core.Repositories
{
    public class FileRepository<T> :IRepository<T> where T : class
    {
        IEnumerable<T> Items = new List<T>();
        string FileName;
        public FileRepository()
        {

            if (this is FileRepository<Station>)
            {
                FileName = "Files/Stations.json";
            }
            else if (this is FileRepository<Route>)
            {
                FileName = "Files/Routes.json";
            }
            else if (this is FileRepository<User>)
            {
                FileName = "Files/Users.json";
            }
            Restore();
        }
        public IEnumerable<T> GetAllItems()
        {
            return Items.ToList();
        }
        public void Update()
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(JsonConvert.SerializeObject(Items, Formatting.Indented));
            }
        }
        public void Add(T item)
        {
            if (item != null)
                Items.ToList().Add(item);
            Update();
        }

        public void Remove(T item)
        {
            if (item != null)
            {
                Items.ToList().Remove(item);
                Update();
            }
        }
        private void Restore()
        {
            var json = File.ReadAllText(FileName);
            Items = JsonConvert.DeserializeObject<List<T>>(json);
        }
        public void DeleteUserFavourite(User currentUser,FavouriteStation selectedFavourite)
        {
            currentUser.FavouriteStations.Remove(selectedFavourite);
            Update();
        }
        public void AddUserFavourite(User currentUser,Station selectedStation)
        {
            currentUser.FavouriteStations.Add(new FavouriteStation(selectedStation, ""));
            Update();
        }

        public void EditUserFavourite(User currentUser,FavouriteStation favouriteStation, string description)
        {
            currentUser.FavouriteStations.FirstOrDefault(fav=>fav.Id==favouriteStation.Id).Description=description;
            Update();
        }
    }
}
