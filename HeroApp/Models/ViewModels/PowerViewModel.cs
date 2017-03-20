using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CodeFirst.Models;

namespace HeroApp.Models.ViewModels
{
    /// <summary>
    /// Супер способность
    /// </summary>
    public class PowerViewModel
    {
        /// <summary>
        /// Id способности
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название способности
        /// </summary>
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        /// <summary>
        /// Описание способности
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Id героя для привязки способности
        /// </summary>
        [Display(Name = "Id Hero")]
        public Guid IdHero { get; set; }

        /// <summary>
        /// ССылка на изображение
        /// </summary>
        [Display(Name = "Изображение")]
        public virtual Image Image { get; set; }


        /// <summary>
        /// Список героев
        /// </summary>
        public virtual List<Hero> Heroes { get; set; }
    }
}