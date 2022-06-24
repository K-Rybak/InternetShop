using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Название")]
        [Required(ErrorMessage ="Поле не должно быть пустым")] //Added Validation
        public string Name { get; set; }
        [DisplayName("Display order")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Range(1, int.MaxValue, ErrorMessage ="Значение должно быть больше 0")]
        public int DisplayOrder { get; set; }
    }
}
