﻿using System.ComponentModel.DataAnnotations;

namespace BLOGAPP.API.Models.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
    }
}
