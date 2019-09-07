using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Theater
    {
        public string Name { get; set; }

        [Key]
        [MaxLength(9)]
        public string Tag { get; set; }
        [MaxLength(3)]
        public string CityTag { get; set; }
    }
}
