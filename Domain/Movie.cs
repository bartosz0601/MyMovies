
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public float? Rate { get; set; }
    }
}