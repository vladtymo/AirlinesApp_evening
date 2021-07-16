using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class City
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }

        // FOREIGN KEYS
        public int CountryId { get; set; }

        // NAVIGATION PROPERTIES
        public virtual Country Country { get; set; }
        public virtual ICollection<Flight> FlightsFrom { get; set; }
        public virtual ICollection<Flight> FlightsTo { get; set; }
    }
}