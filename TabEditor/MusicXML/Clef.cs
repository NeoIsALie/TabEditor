namespace MusicXml.Domain
{
    public class Clef
    {
        internal Clef()
        {
            Line = 0;
            Sign = string.Empty;
            OctaveChange = 0;   
        }

        public int Line { get; internal set; }

        public string Sign { get; internal set; }

        public int OctaveChange { get; internal set; }
    }
}
