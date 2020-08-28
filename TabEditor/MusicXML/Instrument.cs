using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicXml.Domain
{
    public class Instrument
    {
        internal Instrument()
        {
            Name = string.Empty;
        }

        public string Name { get; internal set; }

    }
}
