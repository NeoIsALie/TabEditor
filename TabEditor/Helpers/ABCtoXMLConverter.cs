using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using ABC;
using ABC.Domain;

namespace ABCConverter
{
    public class ABCtoXMLConverter
    {
      /*
      Converts score in ABC to equivalent in MusicXML
      @param score -- score in ABC
      @returns path -- path to score in MusicXML 
      */
        public static string Convert(ABCScore score)
        {
            string path = "";

            XDocument result = new XDocument();

            XElement root = new XElement("score-partwise");

            XElement movementNumber = new XElement("movement-number", score.Header.Titles.First<string>());
            XElement movementTitle = new XElement("movement-title", score.Header.Titles.First<string>());

            root.Add(movementNumber);
            root.Add(movementTitle);

            XElement work = new XElement("work");
            XElement worktitle = new XElement("work-title", score.Header.Titles.First<string>());
            work.Add(worktitle);

            XElement identification = new XElement("identification");
            foreach (string c in score.Header.Composers)
            {
                XElement creator = new XElement("creator", c);
                creator.Add(creator);
                identification.Add(creator);
            }

            XElement part = new XElement("part");
            foreach(ABCMeasure measure in score.Body.Measures)
            {
                int i = 0;
                XElement xmlmeasure = new XElement("measure", new XAttribute("number", i));
                foreach (Note note in measure.Notes)
                {
                    XElement xmlnote = new XElement("note");
                    if(note.isChord)
                        xmlnote.Add(new XElement("chord"));
                    if(note.isRest)
                    {
                        xmlnote.Add(new XElement("rest"));
                        XElement duration = new XElement("duration", note.Duration);
                        XElement type = new XElement("type", note.Type);
                        xmlnote.Add(duration, type);
                    }
                    else
                    {
                        XElement pitch = new XElement("pitch", new XElement("step", note.Pitch.Step),
                                                    new XElement("alter", note.Pitch.Alter),
                                                    new XElement("octave", note.Pitch.Octave));
                        XElement duration = new XElement("duration", note.Duration);
                        XElement accidental = new XElement("accidental", note.Accidental);
                        xmlnote.Add(pitch, duration, accidental);
                        xmlmeasure.Add(xmlnote);
                    }
                }
                i++;
                part.Add(xmlmeasure);
            }

            root.Add(work, identification, part);
            path = score.Header.Titles.FirstOrDefault<string>() + "converted.musicxml";
            result.Save(path);

            return path;
        }
    }
}
