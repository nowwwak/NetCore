using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // /about
    //[Route("about")] - this way it will be hardcoded for about
    //[Route("company/[controller]/[action]")] // I can add text in the url like company
    [Route("[controller]/[action]")] // this way I can reaname controllaer na it will be ok, I can add parameters
    public class AboutController // you don'tneed to inheritent from Controller
    {
        //[Route("")] // this is default acton for this controller
        public string Phone()
        {
            return "1+555+555+555+555";
        }

        //[Route("address")] - only for address
        //[Route("[action]")] - for action with same name as this metdhod
        public string Address()
        {
            return "USA";
        }
    }
}
