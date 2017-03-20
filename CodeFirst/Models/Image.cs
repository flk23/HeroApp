using System;

namespace CodeFirst.Models
{
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
