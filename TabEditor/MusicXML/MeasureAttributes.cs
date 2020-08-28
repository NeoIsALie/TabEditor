namespace MusicXml.Domain
{
    public class MeasureAttributes
    {
        internal MeasureAttributes()
        {
            Divisions = 1;
            Key = new Key();
        }

        public int Divisions { get; internal set; }

        public Key Key { get; internal set; }

        public Time Time { get; internal set; }

        public Clef Clef { get; internal set; }
    }
}