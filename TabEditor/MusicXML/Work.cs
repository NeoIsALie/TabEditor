namespace MusicXml.Domain
{
    public class Work
    {
        internal Work()
        {
            Number = string.Empty;
            Title = string.Empty;
        }

        public string Number { get; internal set; }
        public string Title { get; internal set; }
    }
}
