using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller //we don't need to inheritent from Controller, but this way we have access to more informations
    {
        private IRestaurantData restaurantData;
        public HomeController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            //sample methods from Controller class
            //this.BadRequest(); //creates bad request response
            //this.HttpContext.Response; // we can access request and resposne
            //this.File(); //this will return file

            //return Content("Hello from HomeController");

            var model = restaurantData.GetAll();

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
