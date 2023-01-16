using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ExternalAPI
{
    public class MovieExtDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public float Rate { get; set; }
    }
}
