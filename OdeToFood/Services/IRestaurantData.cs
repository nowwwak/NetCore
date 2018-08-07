using System;
using System.Collections.Generic;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant restaurant);
        void Update(Restaurant restaurant);

    }
}
