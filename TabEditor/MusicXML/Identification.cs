namespace MusicXml.Domain
{
    public class Identification
    {
        internal Identification()
        {
            Composer = string.Empty;
            Poet = string.Empty;
            Lyricist = string.Empty;
            Transcriber = string.Empty;
            Source = string.Empty;
            Rights = string.Empty;
            Encoding = new Encoding();
        }

        public string Composer { get; internal set; }

        public string Poet { get; internal set; }

        public string Lyricist { get; internal set; }

        public string Transcriber { get; internal set; }

        public string Source { get; internal set; }

        public string Rights { get; internal set; }

        public Encoding Encoding { get; internal set; }
    }
}