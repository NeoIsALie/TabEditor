namespace MusicXml.Domain
{
    public class Ending
    {
        internal Ending()
        {
            Type = string.Empty;
            Number = 0;
        }

        public string Type { get; set; }
        public int Number { get; internal set; }
    }
}
