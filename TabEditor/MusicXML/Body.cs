using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class ABCBody
    {
        internal ABCBody()
        {
            List<ABCMeasure> Measures = new List<ABCMeasure>();
            string Words = string.Empty;
        }

        public List<ABCMeasure> Measures { get; internal set; }

        public string Words { get; internal set; }
    }
}
