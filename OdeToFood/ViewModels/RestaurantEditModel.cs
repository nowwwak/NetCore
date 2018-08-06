using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.ViewModels
{
    public class RestaurantEditModel
    {
        [Required, MaxLength(80)]
        [DisplayName("Restaurant name")]
        public string Name { get; set; }
        [DisplayName("Cusine")]
        public CusineType Cusine { get; set; }
    }
}
