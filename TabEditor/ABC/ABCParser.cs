using ABC.Domain;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ABC
{
    public  class ABCParser
    {
        public ABCScore GetABCScore(String filename)
        {
            ABCScore score = new ABCScore();
            score.Header = GetHeader(filename);
           // score.Body = GetBody(score.Header.Key, filename);

            return score; 
        }

        public  ABCHeader GetHeader(string filename)
        {
            StreamReader sr = File.OpenText(filename);
            ABCHeader header = new ABCHeader();
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                if (s.StartsWith("X:"))
                {
                    header.Reference = Convert.ToInt32(s.Remove(0, 2));
                    System.Diagnostics.Debug.Print(header.Reference.ToString());
                }
                else if (s.StartsWith("C:"))
                {
                    header.Composers.Add(s.Remove(0, 2));
                    System.Diagnostics.Debug.Print(header.Composers.Count.ToString());
                    System.Diagnostics.Debug.Print(header.Composers[0]);
                }
                else if (s.StartsWith("T:"))
                {
                    header.Titles.Add(s.Remove(0, 2));
                    System.Diagnostics.Debug.Print(header.Titles.Count.ToString());
                }
                else if (s.StartsWith("Z:"))
                {
                    header.TranscriptionNotes.Add(s.Remove(0, 2));
                    System.Diagnostics.Debug.Print(header.TranscriptionNotes.Count.ToString());
                }
                else if (s.StartsWith("L:"))
                {
                    header.NoteLength = s.Remove(0, 2);
                    System.Diagnostics.Debug.Print(header.NoteLength);
                }
                else if (s.StartsWith("M:"))
                {
                    header.Meter = s.Remove(0, 2);
                    System.Diagnostics.Debug.Print(header.Meter);
                }
                else if (s.StartsWith("Q:"))
                {
                    header.Tempo = s.Remove(0, 3);
                    System.Diagnostics.Debug.Print(header.Tempo);
                }
                else if (s.StartsWith("K:"))
                {
                    header.Key = s.Remove(0, 2);
                    System.Diagnostics.Debug.Print(header.Key);
                }
                else
                    continue;
            }
            sr.Close();
            return header;
        }

        /*
        public  ABCBody GetBody(string key, string filename)
        {
            var tuneBody = new ABCBody();
            StreamReader sr = File.OpenText(filename);
            Regex measuresrx = new Regex("[^|:]*[_=^]*[a-zA-G]*[0-9]?", RegexOptions.Compiled);
            string text = "";
            string words = "";
            while ((text = sr.ReadLine()) != null)
            {
                if(!(text.StartsWith("w: ")) || !(text.StartsWith("W: ")))
                {
                    MatchCollection measures = measuresrx.Matches(text);
                    foreach (Match match in measures)
                    {
                        ABCMeasure measure = GetMeasure(key, match);
                        tuneBody.Measures.Add(measure);
                    }
                }
                else
                    words = words + text;
            }
            tuneBody.Words = words;
            return tuneBody;
        }
        */
        /*
        public  ABCMeasure GetMeasure(string key, Match match)
        {
            ABCMeasure measure = new ABCMeasure();
            bool isChord = false;
            Regex notesrx = new Regex("[_=^]*[a-gzA-G]{1}[,']*[0-9]?[/]*", RegexOptions.Compiled);
            MatchCollection matches = notesrx.Matches(match.ToString());

            foreach(Match elem in matches)
            {
                if ((match.ToString().Contains("[")) || (match.ToString().Contains("]")))
                    isChord = true;
                Note note = GetNote(key, isChord, match);
                measure.Notes.Add(note);
            }

            return measure;
        }
        public  Note GetNote(string key, bool isChord, Match match)
        {
            string m = match.Value.ToString();
            System.Diagnostics.Debug.Print(m);
            Note note = new Note();
            note.Accidental = GetAccidental(m);
            if (note.Accidental == "double-sharp" || note.Accidental == "double-flat")
                m.Remove(0, 2);
            else if (note.Accidental == "sharp" || note.Accidental == "flat" || note.Accidental == "natural")
                m.Remove(0, 1);

            note.Name = m[0];
            m.Remove(0, 1);
            note.isChord = isChord;

            if (note.Name == 'z')
                note.isRest = true;

            note.Pitch = GetPitch(note.Name, m);
            GetType(m, note);

            return note;

        }

        public  void GetType(string match, Note note)
        {
            Regex typerx = new Regex("[0-9]?[/]*", RegexOptions.Compiled);
            Match result = typerx.Match(match);
            switch(result.Value)
            {
                case "2":
                    note.Type = "half";
                    note.Duration = 240;
                    break;
                case "3":
                    note.Type = "half";
                    note.Duration = 360;
                    break;
                case "4":
                    note.Type = "whole";
                    note.Duration = 480;
                    break;
                case "/":
                    note.Type = "eigth";
                    note.Duration = 60;
                    break;
                case "//":
                    note.Type = "sixteenth";
                    note.Duration = 30;
                    break;
                case "3/":
                    note.Type = "half";
                    note.Duration = 180;
                    break;
                default:
                    note.Type = "quarter";
                    note.Duration = 120;
                    break;
            }
        }
        public  string GetAccidental(string match)
        {
            string accidental = string.Empty;

            if (match.StartsWith("^^"))
                accidental = "double-sharp";
            else if (match.StartsWith("^"))
                accidental = "sharp";
            else if (match.StartsWith("__"))
                accidental = "double-flat";
            else if (match.StartsWith("_"))
                accidental = "flat";
            else if(match.StartsWith("="))
                accidental = "natural";

            return accidental;
        }

        public  ABCPitch GetPitch(char name, string note)
        {
            Regex alterrx = new Regex("[,']*", RegexOptions.Compiled);
            Match result = alterrx.Match(note);
            ABCPitch pitch = new ABCPitch();
            pitch.Step = name;
            if (result.ToString().Contains(","))
                pitch.Octave -= result.Value.Length;
            else if (result.ToString().Contains("'"))
                pitch.Octave += result.Value.Length;
            if (char.IsLower(name))
                pitch.Octave += 1;
            
            return pitch;
        }
        */
    }
}
