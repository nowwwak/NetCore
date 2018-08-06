using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller //we don't need to inheritent from Controller, but this way we have access to more informations
    {
        private IRestaurantData restaurantData;
        private IGreeter greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            this.restaurantData = restaurantData;
            this.greeter = greeter;
        }
        public IActionResult Details(int id)
        {
            var restaurant = restaurantData.Get(id);
            if (restaurant == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Index));
            }

            return View(restaurant);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Add this to POST request in order
        //this is implemented with hidden input in order to make sure that the form that users is sending to us
        //was created by us
        public IActionResult Create(RestaurantEditModel model)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cusine = model.Cusine;

                newRestaurant = restaurantData.Add(newRestaurant);

                // this way if users refresh browser it will post same request again
                //return View("Details", newRestaurant);

                //TO FIX we need to use
                //POST Redirect GET Pattern and force uses to send get request, this way user will be able to refresh new page without resending data
                return RedirectToAction(nameof(Details), new {id = newRestaurant.Id});
            }
            else
            {
                return View();
            }
        }

        public IActionResult Index()
        {
            //sample methods from Controller class
            //this.BadRequest(); //creates bad request response
            //this.HttpContext.Response; // we can access request and resposne
            //this.File(); //this will return file

            //return Content("Hello from HomeController");

            var model = new HomeIndexViewModel();

            model.Restaurants = restaurantData.GetAll();
            model.CurrentMessge = greeter.GetMessage();

            return View(model);//it will be later decided if this is going to be JSON, XML...
        }

        public IActionResult IndexReturnOject()
        {
            var model = new Restaurant { Id = 1, Name = "Alas restaurant" };

            return new ObjectResult(model);//it will be later decided if this is going to be JSON, XML...
        }

        public string MyMessage()
        {
            return "My message";
        }


    }
}
