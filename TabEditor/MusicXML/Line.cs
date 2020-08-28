using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer
{
    public class Line
    {
        public string Chords { get; set; }
        public string Text { get; set; }

        public Line(string chords, string text)
        {
            Chords = chords;
            Text = text;
        }
    }
}
