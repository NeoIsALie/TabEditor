using System.Collections.Generic;

namespace MusicXml.Domain
{
    public class Part
    {
        internal Part()
        {
            Id = string.Empty;
            Name = string.Empty;
            Measures = new List<Measure>();
            Tempo = 0;
        }

        public string Id { get; internal set; }

        public string Name { get; internal set; }

        public List<Measure> Measures { get; internal set; }

        public int Tempo { get; internal set; }
    }
}

