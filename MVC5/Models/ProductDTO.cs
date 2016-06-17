using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace MVC5.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string ProducerName { get; set; }
    }



    public enum Position
{ First,
    Second,
    Last,
}
}
