using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class ABCMeasure
    {
        internal ABCMeasure()
        {
            Notes = new List<Note>();
        }

        public List<Note> Notes { get; internal set; }
    }
}
