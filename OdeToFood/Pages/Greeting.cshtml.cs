using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Services;

namespace OdeToFood.Pages
{
    public class GreetingModel : PageModel
    {
        private IGreeter greeter2;

        public string CurrentGreeting { get; set; }

        public GreetingModel(IGreeter greeter)
        {
            this.greeter2 = greeter;
        }

        public void OnGet(string name)
        {
            CurrentGreeting = greeter2.GetMessage() + $" You passed parameter {name}";

        }
    }
}