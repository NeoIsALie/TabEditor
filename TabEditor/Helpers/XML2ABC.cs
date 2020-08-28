using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MusicXml;
using MusicXml.Domain;

namespace XML2ABCConverter
{
    public class XML2ABC
    {

      /*
      Convert XML score to ABC
      @param score -- score in XML
      @returns path -- path to converted file
      */
        public static string ConvertXML(Score score)
        {
            FileStream file;
            string path;
            if (score.MovementTitle != string.Empty)
            {
                path = @"J:DBCourse\DBCourse\DBCourse\" + score.MovementTitle + ".abc";
                file = File.Create(path);
            }
            else
            {
                path = @"J:DBCourse\DBCourse\DBCourse\convert.abc";
                file = File.Create(path);
            }

            StreamWriter result = new StreamWriter(path);
            ConvertWork(result, score);
            ConvertIdentification(result, score);

            foreach (Part part in score.Parts)
            {
                result.WriteLine("V: P" + part.Id + "name=" + part.Name);
                ConvertMeasures(result, score);
            }
            result.Close();

            return path;
        }
        public static void ConvertWork(StreamWriter file, Score score)
        {
            if (score.MovementNumber != -1)
                file.WriteLine("X: " + score.MovementNumber);
            if (score.Work.Number != string.Empty)
                file.WriteLine("X: " + score.Work.Number);
            if (score.MovementTitle != string.Empty)
                file.WriteLine("T: " + score.MovementTitle);
            if (score.Work.Title != string.Empty)
                file.WriteLine("T: " + score.Work.Title);
        }

        public static void ConvertIdentification(StreamWriter file, Score score)
        {
            if (score.Identification != null)
            {

                if (score.Identification.Composer != string.Empty)
                    file.WriteLine("C: " + score.Identification.Composer);
                if (score.Identification.Poet != string.Empty)
                     file.WriteLine("C: " + score.Identification.Poet);
                if (score.Identification.Transcriber != string.Empty)
                    file.WriteLine("Z: " + score.Identification.Transcriber);
                if (score.Identification.Source != string.Empty)
                    file.WriteLine("S: " + score.Identification.Source);
                if (score.Identification.Rights != string.Empty)
                    file.WriteLine("Z: " + score.Identification.Rights);
            }
        }

        public static void ConvertMeasures(StreamWriter file, Score score)
        {
            foreach(Part part in score.Parts)
            {
                file.Write("[V: %s] ", part.Id);
                foreach(Measure measure in part.Measures)
                {
                    int divisions = 1;
                    if(measure.Attributes != null)
                        divisions = measure.Attributes.Divisions;
                    barlineToABC(file, measure.Barlines.FirstOrDefault<Barline>());
                    foreach(MeasureElement m in measure.MeasureElements)
                    {
                        if(m.Type == MeasureElementType.Note)
                        {
                            bool inChord = false;
                            Note note = (Note)m.Element;
                            if(note.IsChordTone && !inChord)
                            {
                                file.Write("[");
                                inChord = true;
                            }
                            else if(!note.IsChordTone && inChord)
                            {
                                file.Write("]");
                                inChord = false;
                            }
                            string resultnote = GetNote(note);
                            if (note.Slur != null)
                            {
                                string resslur = slurToABC(note.Slur);
                                if (note.Slur.Type == "start")
                                    file.Write(resslur + resultnote);
                                else
                                    file.Write(note + resslur);
                            }
                        }
                        if(m.Type == MeasureElementType.Forward)
                        {
                            Forward fw = (Forward)m.Element;
                            file.Write("x%" + fw.Duration);
                        }
                    }
                    file.WriteLine();
                    ConvertLyrics(file, measure);
                    file.WriteLine();
                }
            }
        }

        public static string GetNote(Note note)
        {
            string resnote = "";
            if(note.IsRest)
                resnote = "z" + note.Duration.ToString();
            else
                resnote = GetAccidental(note) + GetPitch(note.Pitch) + GetLength(note);
            return resnote;
        }

        public static string GetAccidental(Note note)
        {
            string accidental = string.Empty;
            switch(note.Accidental)
            {
                case "double-sharp":
                    accidental = "^^";
                    break;
                case "sharp":
                    accidental = "^";
                    break;
                case "natural":
                    accidental = "=";
                    break;
                case "flat":
                    accidental = "_";
                    break;
                case "double-flat":
                    accidental = "__";
                    break;
                default:
                    accidental = "";
                    break;
            }
            return accidental;
        }

        public static string GetPitch(Pitch pitch)
        {
            string abcNote = "";
            char note = pitch.Step;
            if (pitch.Octave > 4)
                Char.ToLower(note);
            switch (pitch.Octave)
            {
                case 6:
                        abcNote = note.ToString() + "'";
                        break;
                case 7:
                        abcNote = note.ToString() + "''";
                        break;
                case 8:
                        abcNote = note.ToString() + "'''";
                        break;
                case 9:
                        abcNote = note.ToString() + "''''";
                        break;
                case 3:
                        abcNote = note.ToString() + ",";
                        break;
                case 2:
                        abcNote = note.ToString() + ",,";
                        break;
                case 1:
                        abcNote = note.ToString() + ",,,";
                        break;
                case 0:
                        abcNote = note.ToString() + ",,,,";
                        break;
            }
            return abcNote;
        }

        public static string GetLength(Note note)
        {
            string len = "";
            if (note.Type == string.Empty)
            {
                return len;
            }
            string durationName = note.Type;
            int numerator = 1, denominator = 1;

            switch (durationName)
            {
                case "eighth":
                    denominator = 2;
                    break;
                case "16th":
                    denominator = 4;
                    break;
                case "32nd":
                    denominator = 8;
                    break;
                case "half":
                    numerator = 2;
                    break;
                case "whole":
                    numerator = 4;
                    break;
                default:
                    denominator = 1;
                    numerator = 1;
                    break;
            }
            if (note.DotNumber > 0 && note.DotNumber == 1)
            {
                if (denominator == 1 && numerator > 1)
                {
                    numerator *= (int)1.5;
                }
                else
                {
                    numerator = 3;
                    denominator *= 2;
                }
            }
            if (numerator != 1)
            {
                len = numerator.ToString();
            }
            switch (denominator)
            {
                case 1:
                        break;
                case 2:
                        len = len + "/";
                        break;
                default:
                        len = len + "/" + denominator;
                        break;
            }
            return len;
        }

        public static void barlineToABC(StreamWriter file, Barline barline)
        {
            if (barline.Location != string.Empty)
            {
                if (barline.isSetStyle)
                {
                    if (barline.isSetRepeat && barline.Repeat == "backward")
                        file.Write(" :");
                    if (barline.isSetStyle)
                    {
                        switch(barline.Style)
                        {
                            case "light-heavy":
                                file.Write("|]");
                                break;
                            case "heavy-light":
                                file.Write("[|");
                                break;
                            case "light-light":
                                file.Write("||");
                                break;
                            case "dotted":
                                file.Write(".|");
                                break;
                            default:
                                file.Write("|");
                                break;
                        }
                    }
                }
                if (barline.Location == "left")
                {
                    if (barline.isSetRepeat && barline.Repeat == "forward")
                        file.Write(":");
                    if (barline.isSetEnding)
                        file.Write("[%s ", barline.Ending.Number);
                }
                if (barline.Location == "right" && barline.isSetEnding && barline.Ending.Type == "discontinue")
                    file.Write(" |");
            }
        }

        public static string slurToABC(Slur slur)
        {
            string resslur = "";
            switch(slur.Type)
            {
                case "start":
                    resslur = "(";
                    break;
                case "stop":
                    resslur = ")";
                    break;
            }

            return resslur;
        }

        public static void ConvertLyrics(StreamWriter file, Measure measure)
        {
            bool hasLyrics = false;
            foreach(MeasureElement m in measure.MeasureElements)
            {
                if(m.Type == MeasureElementType.Note)
                {
                    Note note = (Note)m.Element;
                    if(note.Lyric != null && hasLyrics)
                        file.Write(note.Lyric.Text);
                    else if(note.Lyric != null && !hasLyrics)
                    {
                        file.Write("w: " + note.Lyric.Text);
                        hasLyrics = true;
                    }
                }
            }
        }
    }
}
