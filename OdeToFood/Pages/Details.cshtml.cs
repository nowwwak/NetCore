using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Pages
{
    public class DetailsModel : PageModel
    {
        private IRestaurantData restaurantData;
        public Restaurant Restaurant { get; set; }
        public DetailsModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public void OnGet(int restaurantId)
        {
            Restaurant = restaurantData.Get(restaurantId);
            if (Restaurant == null)
                NotFound();
        }
    }
}