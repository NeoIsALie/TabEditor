using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class Note
    {
        internal Note()
        {
            Type = string.Empty;
            Name = new Char();
            Pitch = new ABCPitch();
            Duration = 120;
            Accidental = string.Empty;
            isChord = false;
            isRest = false;
            Slur = null;
        }

        public string Type { get; internal set; }

        public char Name { get; internal set; }
        public ABCPitch Pitch { get; internal set; }
        public int Duration { get; internal set; }
        public string Accidental { get; internal set; }

        public bool isChord { get; internal set; }

        public bool isRest { get; internal set; }

        public ABCSlur Slur {get; set; }
       
    }
}
