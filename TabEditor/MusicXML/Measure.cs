using System.Collections.Generic;

namespace MusicXml.Domain
{
    public class Measure
    {
        internal Measure()
        {
            Barlines = new List<Barline>();
            Width = -1;
            MeasureElements = new List<MeasureElement>();
        }

        public List<Barline> Barlines {get; internal set;}
        public decimal Width { get; internal set; }

        public List<MeasureElement> MeasureElements { get; internal set; }

        public MeasureAttributes Attributes { get; set; }

        public List<Note> Notes { get; internal set; }
    }
}
