using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVC5.Controllers
{
    public class FakeModelView
    {
        [Required]
        public string CarName { get; set; }
        public IEnumerable<string> Cars { get; set; }
        public IEnumerable<string> AllCars
        {
            get
            { return new string[] { "Ford", "Skoda", "Subaru", "Volvo" }; }
        }
    }
}
