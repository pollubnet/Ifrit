using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ifrit.Web.Models
{
    public class IotData
    {
        [Key]
        public DateTime Time { get; set; }
        public double Data { get; set; }
        public string Device { get; set; }
    }
}
