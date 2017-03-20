using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CodeFirst.Models;

namespace HeroApp.Models.ViewModels
{
    /// <summary>
    /// Супер герой
    /// </summary>
    public class HeroViewModel
    {
        /// <summary>
        /// Id героя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя героя
        /// </summary>
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Описание героя
        /// </summary>
        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Очки опыта
        /// </summary>
        [Display(Name = "XP")]
        public int XP { get; set; }

        /// <summary>
        /// Уровень
        /// </summary>
        [Display(Name = "Уровень")]
        public int Level { get; set; }

        /// <summary>
        /// MinXP
        /// </summary>
        [Display(Name = "MinXP")]
        public int MinXP { get; set; }

        /// <summary>
        /// MaxXP
        /// </summary>
        [Display(Name = "MaxXP")]
        public int MaxXP { get; set; }

        /// <summary>
        /// XPinPercent
        /// </summary>
        [Display(Name = "XPinPercent")]
        public int XPinPercent { get; set; }

        /// <summary>
        /// ССылка на изображение
        /// </summary>
        [Display(Name = "Изображение")]
        public virtual Image Image { get; set; }


        /// <summary>
        /// Список способностей
        /// </summary>
        public virtual List<Power> Powers { get; set; }
    }
}