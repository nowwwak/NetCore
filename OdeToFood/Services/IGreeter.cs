using Microsoft.Extensions.Configuration;

namespace OdeToFood.Services
{
    public interface IGreeter
    {
        string GetMessage();
    }

    public class Greeter : IGreeter
    {
        private IConfiguration configuration;
        public Greeter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetMessage()
        {
            return configuration["GreeterGetMessage"];
        }
    }
}