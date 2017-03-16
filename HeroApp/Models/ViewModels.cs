using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirst;

namespace HeroApp.Models
{
    public class HeroCreationViewModel
    {
        public Hero Hero { get; set; }
        public Power Power { get; set; }
        //public HeroCreationViewModel(Hero h, List<Power> p)
        //{
        //    Hero = h;
        //    Powers = p;
        //}
    }
}