using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    /// <summary>
    /// Супер герой
    /// </summary>
    public class Hero
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
