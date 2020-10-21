using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RatHutWebsite.Models
{
    /// <summary>
    /// Represents a generic menu item
    /// </summary>
    public class Product
    {   [Key]
        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public Boolean IsVegan { get; set; }

        public Boolean IsVegetarian { get; set; }
    }
}
