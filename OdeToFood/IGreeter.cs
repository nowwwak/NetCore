﻿using Microsoft.Extensions.Configuration;

namespace OdeToFood
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