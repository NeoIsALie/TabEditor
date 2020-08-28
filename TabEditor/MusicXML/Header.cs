using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Domain
{
    public class ABCHeader
    {
        internal ABCHeader()
        {
            Reference = 0;
            Titles = new List<string>();
            TranscriptionNotes = new List<string>();
            Composers = new List<string>();
            NoteLength = string.Empty;
            Meter = string.Empty;
            Tempo = string.Empty;
            Key = string.Empty;
            Instrument = string.Empty;
            Encoding = new ABCEncoding();
        }

        public int Reference { get; internal set; }

        public List<string> Titles { get; internal set; }

        public List<string> TranscriptionNotes { get; internal set; }

        public List<string> Composers { get; internal set; }

        public string NoteLength { get; internal set; }

        public string Meter { get; internal set; }

        public string Tempo { get; internal set; }

        public string Key { get; internal set; }

        public string Instrument { get; internal set; }

        public ABCEncoding Encoding { get; internal set; }

    }
}

