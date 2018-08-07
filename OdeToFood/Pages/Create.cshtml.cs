using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Pages
{
    public class CreateModel : PageModel
    {
        private IRestaurantData restaurantData;
        public RestaurantEditModel Restaurant { get; set; }

        public CreateModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public void OnGet()
        {
            if(restaurantData == null)
                Restaurant = new RestaurantEditModel();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Restaurant newRestaurant = new Restaurant
                {
                    Name = Restaurant.Name,
                    Cusine = Restaurant.Cusine
                };
                newRestaurant = restaurantData.Add(newRestaurant);
                return RedirectToPage("Details", new {restaurantId = newRestaurant.Id});
            }
            else
            {
                return Page();
            }
        }
    }
}