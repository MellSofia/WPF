using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика_17._09
{
    public class Fuel
    {
        public string Purpose { get; set; }
        public Fuel() { }
        public Fuel(string text)
        {
            Purpose = text;
        }
    }
}
