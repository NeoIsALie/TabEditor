using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class ABCEncoding
    {
        internal ABCEncoding()
        {
            Software = string.Empty;
            Description = string.Empty;
            EncodingDate = new DateTime();
        }

        public string Software { get; internal set; }

        public string Description { get; internal set; }

        public DateTime EncodingDate { get; internal set; }
    }
}
