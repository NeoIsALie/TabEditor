using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class ABCPitch
    {
        internal ABCPitch()
        {
            Alter = 0;
            Octave = 4;
            Step = new Char();
        }

        public int Alter { get; internal set; }

        public int Octave { get; internal set; }

        public char Step { get; internal set; }
    }
}
