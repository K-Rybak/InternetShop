using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace InternetShop.Models.ViewModel
{
    public class GameVM
    {
        public Game Game { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
        public IEnumerable<SelectListItem> PublisherSelectList { get; set; }
    }
}
