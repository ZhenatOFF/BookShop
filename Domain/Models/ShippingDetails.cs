using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ShippingDetails
    {

        [Required(ErrorMessage = "Укажите Ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Адрес доставки")]
        [Display(Name = "Первый адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
