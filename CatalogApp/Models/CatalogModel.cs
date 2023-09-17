﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogApp.Models
{
	public class CatalogModel
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string ParentalDirectoryName { get; set; }
    }
}
