using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        [Display(Name="Название")]
        public string Name { get; set; }
        [Display(Name ="Описание")]
        public string Description { get; set; }
        [Display(Name ="Цена")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Range(1, int.MaxValue, ErrorMessage ="Значение должно быть больше нуля")]
        public double Price { get; set; }
        public string Image { get; set; }
        
        [Display(Name = "Жанр")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name ="Издатель")]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }
    }
}
