using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Data;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return context.Restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);
            context.SaveChanges();

            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            context.Attach(restaurant).State = EntityState.Modified;
            context.SaveChanges();

            return restaurant;
        }
    }
}
