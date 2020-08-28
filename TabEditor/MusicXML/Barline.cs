using System.Xml;

namespace MusicXml.Domain
{
    public class Barline
    {
        internal Barline()
        {
            Style = "regular";
            Location = string.Empty;
            Repeat = string.Empty;
            Ending = new Ending();
            isSetRepeat = false;
            isSetStyle = false;
            isSetEnding = false;
        }
        public string Style { get; internal set; }

        public string Location { get; internal set; }

        public string Repeat { get; internal set; }

        public Ending Ending { get; internal set; }

        public bool isSetRepeat {get; set;}

        public bool isSetStyle {get; set;}

        public bool isSetEnding {get;set;}

    }

}
