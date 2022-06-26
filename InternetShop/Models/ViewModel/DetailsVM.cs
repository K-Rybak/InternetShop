using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Models.ViewModel
{
    public class DetailsVM
    {
        public Game Game { get; set; }
        public bool IsExistsInCart { get; set; }

        public DetailsVM()
        {
            Game = new Game();
        }
    }
}
