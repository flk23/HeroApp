using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst
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
        /// ССылка на изображение
        /// </summary>
        [Display(Name = "Изображение")]
        public virtual Image Image { get; set; }


        /// <summary>
        /// Список способностей
        /// </summary>
        public virtual List<Power> Powers { get; set; }
    }

    /// <summary>
    /// Изображениe
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Id изображения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Img { get; set; }

        public Image()
        { }

        public Image(byte[] i)
        {
            Id = Guid.NewGuid();
            Img = i;
        }
    }
}
