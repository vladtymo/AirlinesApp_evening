﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Country
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }

        // NAVIGATION PROPERTIES
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Airplane> Airplanes { get; set; }
    }
}