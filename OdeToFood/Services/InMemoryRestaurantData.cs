using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant() {Id = 1, Name = "Scott's Pizza place", Cusine = CusineType.German},
                new Restaurant() {Id = 2, Name = "Tresiguels", Cusine = CusineType.Italian},
                new Restaurant() {Id = 3, Name = "King's Contrivance", Cusine = CusineType.None}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(restaurant);

            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            Restaurant old = restaurants.FirstOrDefault(r => r.Id == restaurant.Id);
            if (old != null)
            {
                restaurants.Remove(old);
                restaurants.Add(restaurant);
            }

            return restaurant;
        }
    }
}
