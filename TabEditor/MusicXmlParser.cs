using MusicXml.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Encoding = MusicXml.Domain.Encoding;

namespace MusicXml
{
    public class MusicXmlParser
    {
        public  Score GetScore(String filename)
        {
            XmlDocument document = GetXmlDocument(filename);

            var score = new Score();

            score.Work = GetWork(document);

            var movementNumberNode = document.SelectSingleNode("score-partwise/movement-number");
            if(movementNumberNode != null)
                score.MovementNumber = Convert.ToInt32(movementNumberNode.InnerText);

            var movementTitleNode = document.SelectSingleNode("score-partwise/movement-title");
            if (movementTitleNode != null)
                score.MovementTitle = movementTitleNode.InnerText;

            score.Identification = GetIdentification(document);

            var partNodes = document.SelectNodes("score-partwise/part-list/score-part");

            if (partNodes != null)
            {
                foreach (XmlNode partNode in partNodes)
                {
                    var part = new Part();
                    score.Parts.Add(part);

                    if (partNode.Attributes != null)
                        part.Id = partNode.Attributes["id"].InnerText;

                    var partNameNode = partNode.SelectSingleNode("part-name");

                    if (partNameNode != null)
                        part.Name = partNameNode.InnerText;

                    var partSoundNode = partNode.SelectSingleNode("sound");

                    if (partSoundNode != null)
                        part.Tempo = Convert.ToInt32(partSoundNode.Attributes["tempo"]);

                    var measuresXpath = string.Format("//part[@id='{0}']/measure", part.Id);

                    var measureNodes = partNode.SelectNodes(measuresXpath);

                    if (measureNodes != null)
                    {
                        foreach (XmlNode measureNode in measureNodes)
                        {
                            var measure = new Measure();

                            if (measureNode.Attributes != null)
                            {
                                var measureWidthAttribute = measureNode.Attributes["width"];
                                decimal w;
                                if (measureWidthAttribute != null && decimal.TryParse(measureWidthAttribute.InnerText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out w))
                                    measure.Width = w;
                            }

                            var attributesNode = measureNode.SelectSingleNode("attributes");

                            if (attributesNode != null)
                            {
                                measure.Attributes = new MeasureAttributes();

                                var divisionsNode = attributesNode.SelectSingleNode("divisions");
                                if (divisionsNode != null)
                                    measure.Attributes.Divisions = Convert.ToInt32(divisionsNode.InnerText);

                                var keyNode = attributesNode.SelectSingleNode("key");

                                if (keyNode != null)
                                {
                                    measure.Attributes.Key = new Key();

                                    var fifthsNode = keyNode.SelectSingleNode("fifths");
                                    if (fifthsNode != null)
                                        measure.Attributes.Key.Fifths = Convert.ToInt32(fifthsNode.InnerText);

                                    var modeNode = keyNode.SelectSingleNode("mode");
                                    if (modeNode != null)
                                        measure.Attributes.Key.Mode = modeNode.InnerText;
                                }

                                measure.Attributes.Time = GetTime(attributesNode);

                                measure.Attributes.Clef = GetClef(attributesNode);
                            }
                            /*
                            var childNodes = measureNode.ChildNodes;

                            foreach (XmlNode node in childNodes)
                            {
                                MeasureElement measureElement = null;

                                if (node.Name == "barline")
                                {
                                    var barline = GetBarline(node);
                                    measure.Barlines.Add(barline);
                                }
                                
                                if (node.Name == "note")
                                {
                                    var newNote = GetNote(node);
                                    measureElement = new MeasureElement { Type = MeasureElementType.Note, Element = newNote };
                                }
                                else if (node.Name == "forward")
                                {
                                    measureElement = new MeasureElement { Type = MeasureElementType.Forward, Element = GetForwardElement(node) };
                                }

                                if (measureElement != null)
                                    measure.MeasureElements.Add(measureElement);
                            }*/

                            part.Measures.Add(measure);
                        }
                    }
                }
            }

            return score;
        }

        public  Work GetWork(XmlNode node)
        {
            var work = new Work();

            var workNode = node.SelectSingleNode("work");

            if (workNode != null)
            {
                work.Number = Convert.ToString(workNode.InnerText);
                work.Title = Convert.ToString(workNode.InnerText);
            }

            return work;
        }

        private  Instrument GetInstrument(XmlNode node)
        {
            var instrument = new Instrument();

            var instrumentNode = node.SelectSingleNode("instrument-name");

            if (instrumentNode != null)
            {
                instrument.Name = Convert.ToString(instrumentNode.InnerText);
            }

            return instrument;
        }


        private   Forward GetForwardElement(XmlNode node)
        {
            var forward = new Forward();

            var forwardNode = node.SelectSingleNode("duration");

            if (forwardNode != null)
            {
                forward.Duration = Convert.ToInt32(forwardNode.InnerText);
            }

            return forward;
        }

        private   Barline GetBarline(XmlNode barlineNode)
        {
            var barline = new Barline();

            var styleNode = barlineNode.SelectSingleNode("barline-style");
            if(styleNode != null)
                barline.Style = styleNode.InnerText;

            string location = barlineNode.Attributes["location"].ToString();
            if(location != string.Empty)
                barline.Location = location;

            var ending = barlineNode.SelectSingleNode("ending");
            if(ending != null)
            {
                string endingType = ending.Attributes["type"].ToString();
                barline.Ending.Type = endingType;
                barline.isSetEnding = true;
            }

            var repeat = barlineNode.SelectSingleNode("repeat");
            if(repeat != null)
            {
                string direction = repeat.Attributes["direction"].ToString();
                barline.isSetRepeat = true;
                if(direction != string.Empty)
                    barline.Repeat = direction;
            }

            return barline;    
        }
        private   Note GetNote(XmlNode noteNode)
        {
            var note = new Note();

            var typeNode = noteNode.SelectSingleNode("type");
            if (typeNode != null)
                note.Type = typeNode.InnerText;

            var voiceNode = noteNode.SelectSingleNode("voice");
            if (voiceNode != null)
                note.Voice = Convert.ToInt32(voiceNode.InnerText);

            var durationNode = noteNode.SelectSingleNode("duration");
            if (durationNode != null)
                note.Duration = Convert.ToInt32(durationNode.InnerText);

            var accidental = noteNode.SelectSingleNode("accidental");
            if (accidental != null)
                note.Accidental = accidental.InnerText;

            note.Lyric = GetLyric(noteNode);

            note.Pitch = GetPitch(noteNode);

            var staffNode = noteNode.SelectSingleNode("staff");
            if (staffNode != null)
                note.Staff = Convert.ToInt32(staffNode.InnerText);

            var chordNode = noteNode.SelectSingleNode("chord");
            if (chordNode != null)
                note.IsChordTone = true;

            var restNode = noteNode.SelectSingleNode("rest");
            if (restNode != null)
                note.IsRest = true;

            var dotNode = noteNode.SelectNodes("dot");
            note.DotNumber = dotNode.Count;

            var slurNode = noteNode.SelectSingleNode("notations").SelectSingleNode("slur");
            if(slurNode != null)
            {
                Slur slur = new Slur();
                slur.Type = slurNode.Attributes["type"].ToString();
                slur.Number = Convert.ToInt32(slurNode.Attributes["number"]);
                slur.Placement = slurNode.Attributes["placement"].ToString();
            }

            return note;
        }

        private   Pitch GetPitch(XmlNode noteNode)
        {
            var pitch = new Pitch();
            var pitchNode = noteNode.SelectSingleNode("pitch");
            if (pitchNode != null)
            {
                var stepNode = pitchNode.SelectSingleNode("step");
                if (stepNode != null)
                    pitch.Step = stepNode.InnerText[0];

                var alterNode = pitchNode.SelectSingleNode("alter");
                if (alterNode != null)
                    pitch.Alter = Convert.ToInt32(alterNode.InnerText);

                var octaveNode = pitchNode.SelectSingleNode("octave");
                if (octaveNode != null)
                    pitch.Octave = Convert.ToInt32(octaveNode.InnerText);
            }
            else
            {
                return null;
            }

            return pitch;
        }

        private   Lyric GetLyric(XmlNode noteNode)
        {
            var lyric = new Lyric();

            var lyricNode = noteNode.SelectSingleNode("lyric");
            if (lyricNode != null)
            {
                var syllabicNode = lyricNode.SelectSingleNode("syllabic");

                var syllabicText = string.Empty;

                if (syllabicNode != null)
                    syllabicText = syllabicNode.InnerText;

                switch (syllabicText)
                {
                    case "":
                        lyric.Syllabic = Syllabic.None;
                        break;
                    case "begin":
                        lyric.Syllabic = Syllabic.Begin;
                        break;
                    case "single":
                        lyric.Syllabic = Syllabic.Single;
                        break;
                    case "end":
                        lyric.Syllabic = Syllabic.End;
                        break;
                    case "middle":
                        lyric.Syllabic = Syllabic.Middle;
                        break;
                }

                var textNode = lyricNode.SelectSingleNode("text");
                if (textNode != null)
                    lyric.Text = textNode.InnerText;
            }
            return lyric;
        }

        private   Clef GetClef(XmlNode attributesNode)
        {
            var clef = new Clef();

            var clefNode = attributesNode.SelectSingleNode("clef");

            if (clefNode != null)
            {
                var lineNode = clefNode.SelectSingleNode("line");
                if (lineNode != null)
                    clef.Line = Convert.ToInt32(lineNode.InnerText);

                var signNode = clefNode.SelectSingleNode("sign");
                if (signNode != null)
                    clef.Sign = signNode.InnerText;

                var change = clefNode.SelectSingleNode("clef-octave-change");
                if (change != null)
                    clef.OctaveChange = Convert.ToInt32(change.InnerText);
            }
            return clef;
        }

        private  Time GetTime(XmlNode attributesNode)
        {
            var time = new Time();

            var timeNode = attributesNode.SelectSingleNode("time");
            if (timeNode != null)
            {
                var beatsNode = timeNode.SelectSingleNode("beats");

                if (beatsNode != null)
                    time.Beats = Convert.ToInt32(beatsNode.InnerText);

                var beatTypeNode = timeNode.SelectSingleNode("beat-type");

                if (beatTypeNode != null)
                    time.Mode = beatTypeNode.InnerText;

                var symbol = TimeSymbol.Normal;

                if (timeNode.Attributes != null)
                {
                    var symbolAttribute = timeNode.Attributes["symbol"];

                    if (symbolAttribute != null)
                    {
                        switch (symbolAttribute.InnerText)
                        {
                            case "common":
                                symbol = TimeSymbol.Common;
                                break;
                            case "cut":
                                symbol = TimeSymbol.Cut;
                                break;
                            case "single-number":
                                symbol = TimeSymbol.SingleNumber;
                                break;
                        }
                    }
                }

                time.Symbol = symbol;
            }
            return time;
        }

        private   Identification GetIdentification(XmlNode document)
        {
            var identificationNode = document.SelectSingleNode("score-partwise/identification");
            var identification = new Identification();
            System.Diagnostics.Debug.Print(identification.Composer + identification.Poet);
            if (identificationNode != null)
            {

                var composerNode = identificationNode.SelectSingleNode("creator[@type='composer']");
                identification.Composer = (composerNode != null) ? composerNode.InnerText : string.Empty;
                System.Diagnostics.Debug.Print(identification.Composer);

                var poetNode = identificationNode.SelectSingleNode("creator[@type='poet']");
                identification.Poet = poetNode != null ? poetNode.InnerText : string.Empty;

                var lyricistNode = identificationNode.SelectSingleNode("creator[@type='lyricist']");
                identification.Lyricist = lyricistNode != null ? lyricistNode.InnerText : string.Empty;
                System.Diagnostics.Debug.Print(identification.Lyricist);
                var transcriberNode = identificationNode.SelectSingleNode("creator[@type='transcriber']");
                identification.Transcriber = transcriberNode != null ? transcriberNode.InnerText : string.Empty;

                var sourceNode = identificationNode.SelectSingleNode("source");
                identification.Source = sourceNode != null ? sourceNode.InnerText : string.Empty;

                var rightsNode = identificationNode.SelectSingleNode("rights");
                identification.Rights = rightsNode != null ? rightsNode.InnerText : string.Empty;

                identification.Encoding = GetEncoding(identificationNode);

                return identification;
            }

            return identification;
        }

        private   Encoding GetEncoding(XmlNode identificationNode)
        {
            var encodingNode = identificationNode.SelectSingleNode("encoding");

            var encoding = new Encoding();

            if (encodingNode != null)
            {
                encoding.Software = GetInnerTextOfChildTag(encodingNode, "software");

                encoding.Description = GetInnerTextOfChildTag(encodingNode, "encoding-description");

                var encodingDate = encodingNode.SelectSingleNode("encoding-date");
                if (encodingDate != null)
                    encoding.EncodingDate = Convert.ToDateTime(encodingDate.InnerText);
            }

            return encoding;
        }

        private   string GetInnerTextOfChildTag(XmlNode encodingNode, string tagName)
        {
            var softwareStringBuilder = new StringBuilder();

            var encodingSoftwareNodes = encodingNode.SelectNodes(tagName);

            if (encodingSoftwareNodes != null)
            {
                foreach (XmlNode node in encodingSoftwareNodes)
                {
                    softwareStringBuilder.AppendLine(node.InnerText);
                }
            }

            return softwareStringBuilder.ToString();
        }

        public XmlDocument GetXmlDocument(string filename)
        {
            var document = new XmlDocument();
            var xml = GetFileContents(filename);
            document.XmlResolver = null;
            document.LoadXml(xml);
            return document;
        }


        public string GetFileContents(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
