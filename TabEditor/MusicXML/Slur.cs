using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicXml.Domain
{
    public class Slur
    {
        internal Slur()
        {
        }

        public string Type { get; set; }

        public int Number { get; set; }

        public string Placement { get; set; }
    }
}
