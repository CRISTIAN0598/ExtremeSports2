﻿using System.ComponentModel.DataAnnotations;

namespace ExtremeSports2.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categorías")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}