using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class ABCScore
    {
        internal ABCScore()
        {
            Header = new ABCHeader();
            Body = new ABCBody();
        }

        public ABCHeader Header { get; internal set; }
        
        public ABCBody Body { get; internal set; } 
    }
}
