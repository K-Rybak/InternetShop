using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Название")]
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        public string Name { get; set; }
    }
}
