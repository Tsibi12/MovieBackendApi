using System;
namespace Movies.Models
{
    public class Movie
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
        public DateTime CreateOn { get; set; }
    }
}