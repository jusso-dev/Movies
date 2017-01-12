using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Data
{
    public class Movie
    {
        public int id { get; set; }
        public string Title { get; set; }
        [DisplayName("Main Actor")]
        public string MainActor { get; set; }
        public string Genre { get; set; }
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }
        public decimal Worth { get; set; }
        public string imageUrl { get; set; }

    }
}
