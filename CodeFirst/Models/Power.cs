using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    /// <summary>
    /// Супер способность
    /// </summary>
    public class Power
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
