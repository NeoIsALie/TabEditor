using MusicXml.Domain;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using MusicXml;
using Encoding = MusicXml.Domain.Encoding;

namespace MusicXml
{
    public class XMLTransposer
    {
      /*
      Transpose score to higher or lower pitch
      @param path -- path to file
      @param direction -- number of pitches to transpose to
      */
        public static void Transpose(string path, int direction)
        {
            string steps = "CDEFGAB";
            List<string> keys = new List<string>();
            keys.Add("Cb");
            keys.Add("Gb");
            keys.Add("Db");
            keys.Add("Ab");
            keys.Add("Bb");
            keys.Add("Eb");
            keys.Add("C");
            keys.Add("D");
            keys.Add("E");
            keys.Add("F");
            keys.Add("G");
            keys.Add("A");
            keys.Add("B");
            keys.Add("C#");
            keys.Add("F#");

            int index = 0, newindex = 0;
            var document = GetXmlDocument(path);

            var partNodes = document.SelectNodes("score-partwise/part-list/score-part");
            if (partNodes != null)
            {
                foreach(XmlNode partNode in partNodes)
                {
                    string id = string.Empty;
                    if (partNode.Attributes != null)
                        id = partNode.Attributes["id"].InnerText;

                    var measuresXpath = string.Format("//part[@id='{0}']/measure", id);
                    var measureNodes = partNode.SelectNodes(measuresXpath);
                    foreach(XmlNode measure in measureNodes)
                    {
                        var attributesNode = measure.SelectSingleNode("attributes");
                        if (attributesNode != null)
                        {
                            var keyNode = attributesNode.SelectSingleNode("key");
                            if (keyNode != null)
                            {
                                var fifthsNode = keyNode.SelectSingleNode("fifths");
                                if (fifthsNode != null)
                                {
                                    int temp = Convert.ToInt32(fifthsNode.InnerText);
                                    temp = temp + direction;
                                    if (temp < -7)
                                        temp += 7;
                                    else if (temp > 7)
                                        temp -= 7;
                                    fifthsNode.InnerText = temp.ToString();
                                    System.Diagnostics.Debug.Print(fifthsNode.InnerText);
                                }
                            }
                        }
                        var noteNodes = measure.SelectNodes("note");
                        foreach (XmlNode note in noteNodes)
                        {
                            var pitch = note.SelectSingleNode("pitch");
                            if(pitch != null)
                            {
                                var step = pitch.SelectSingleNode("step");
                                if (step != null)
                                {
                                    System.Diagnostics.Debug.Print(step.InnerText);
                                    index = steps.IndexOf(step.InnerText.ToString());
                                    newindex = index + direction;
                                    System.Diagnostics.Debug.Print(index.ToString());
                                    if (newindex < 0)
                                        newindex +=7;
                                    if (newindex > 6)
                                        newindex -= 7;
                                    System.Diagnostics.Debug.Print(newindex.ToString());
                                    step.InnerText = steps.Substring(newindex, 1);
                                    System.Diagnostics.Debug.Print(step.InnerText);
                                }
                                int newOctave = 0;
                                var octave = pitch.SelectSingleNode("octave");
                                if (octave != null)
                                {
                                    if (index + direction > 6 || index + direction < 0)
                                        newOctave = Convert.ToInt32(octave.InnerText) + direction;
                                    if (newOctave > 9)
                                        newOctave = 9;
                                    else if (newOctave < 0)
                                        newOctave = 0;
                                    octave.InnerText = newOctave.ToString();
                                }
                            }
                        }
                    }
                }
            }
            document.Save(path);
        }

        public String keyFromFifths(int fifths)
        {
            String key = "C";
            switch (fifths)
            {
                case -7:
                    {
                        key = "Cb";
                        break;
                    }
                case -6:
                    {
                        key = "Gb";
                        break;
                    }
                case -5:
                    {
                        key = "Db";
                        break;
                    }
                case -4:
                    {
                        key = "Ab";
                        break;
                    }
                case -3:
                    {
                        key = "Eb";
                        break;
                    }
                case -2:
                    {
                        key = "Bb";
                        break;
                    }
                case -1:
                    {
                        key = "F";
                        break;
                    }
                case 0:
                    {
                        key = "C";
                        break;
                    }
                case 1:
                    {
                        key = "G";
                        break;
                    }
                case 2:
                    {
                        key = "D";
                        break;
                    }
                case 3:
                    {
                        key = "A";
                        break;
                    }
                case 4:
                    {
                        key = "E";
                        break;
                    }
                case 5:
                    {
                        key = "B";
                        break;
                    }
                case 6:
                    {
                        key = "F#";
                        break;
                    }
                case 7:
                    {
                        key = "C#";
                        break;
                    }
            }
            return key;
        }

        public int fifthsFromKey(string key)
        {
            int fifths = 0;
            switch(key)
            {
                case "Cb":
                    fifths = -7;
                    break;
                case "Gb":
                    fifths = -6;
                    break;
                case "Db":
                    fifths = -5;
                    break;
                case "Ab":
                    fifths = -4;
                    break;
                case "Eb":
                    fifths = -3;
                    break;
                case "Bb":
                    fifths = -2;
                    break;
                case "F":
                    fifths = -1;
                    break;
                case "C":
                    fifths = 0;
                    break;
                case "G":
                    fifths = 1;
                    break;
                case "D":
                    fifths = 2;
                    break;
                case "A":
                    fifths = 3;
                    break;
                case "E":
                    fifths = 4;
                    break;
                case "B":
                    fifths = 5;
                    break;
                case "F#":
                    fifths = 6;
                    break;
                case "C#":
                    fifths = 7;
                    break;
            }
            return fifths;
        }

        public static XmlDocument GetXmlDocument(string path)
        {
            var document = new XmlDocument();
            var xml = GetFileContents(path);
            document.XmlResolver = null;
            document.LoadXml(xml);
            return document;
        }

        public static string GetFileContents(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
