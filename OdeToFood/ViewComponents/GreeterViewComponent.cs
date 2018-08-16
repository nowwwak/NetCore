using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;

namespace OdeToFood.ViewComponents
{
    // there is a naming convetion for view componets. They must end with ViewComponet - this is similar to Controllers
    // I will refere to this view componetn as Greeter
    // I can inherit from ViewComponent
    public class GreeterViewComponent : ViewComponent
    {
        private IGreeter greeter;

        public GreeterViewComponent(IGreeter greeter)
        {
            this.greeter = greeter;
        }

        // this method can accept parameters
        public IViewComponentResult Invoke()
        {
            var model = greeter.GetMessage();
            // If you pass string as first parameter to View() it will assume that this is view name
            // so we can't do it this way: View(model)
            // we must specifiy view name explicite 
            // We want to return Default view.
            // View components must be put inside folder hierarchy. This view must be put inside Components\Greeter\Default.cshtml
            return View("Default", model);
        }
    }
}
