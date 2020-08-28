using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace DBCourse
{
    public class ChangeMeter
    {
        //работает
        public static void ChangeMeterXml(string path, string meter)
        {
            System.Diagnostics.Debug.Print("start");
            var document = GetXmlDocument(path);

            var partNodes = document.SelectNodes("score-partwise/part-list/score-part");
            if(partNodes != null)
            {
                foreach(XmlNode partNode in partNodes)
                {
                    string id = String.Empty;
                    if (partNode.Attributes != null)
                        id = partNode.Attributes["id"].InnerText;
                    var measuresXpath = string.Format("//part[@id='{0}']/measure", id);
                    var measureNodes = partNode.SelectNodes(measuresXpath);
                    foreach (XmlNode measure in measureNodes)
                    {
                        var attributesNode = measure.SelectSingleNode("attributes");
                        if (attributesNode != null)
                        {
                            var timeNode = attributesNode.SelectSingleNode("time");
                            if (timeNode != null)
                            {
                                var beats = timeNode.SelectSingleNode("beats");
                                if (beats != null)
                                {
                                    System.Diagnostics.Debug.Print(meter[0].ToString());
                                    System.Diagnostics.Debug.WriteLine(meter[0]);
                                    beats.InnerText = meter[0].ToString();
                                }
                                var type = timeNode.SelectSingleNode("beat-type");
                                if (type != null)
                                {
                                    System.Diagnostics.Debug.WriteLine(meter[2]);
                                    type.InnerText = meter[2].ToString();
                                }
                            }
                        }
                    }
                }
            }
            document.Save(path);
        }

        public static XmlDocument GetXmlDocument(string filename)
        {
            var document = new XmlDocument();
            var xml = GetFileContents(filename);
            document.XmlResolver = null;
            document.LoadXml(xml);
            return document;
        }

        public static string GetFileContents(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        //работает
        public static void ChangeMeterABC(string path, string meter)
        {
            string[] FileLines = File.ReadAllLines(path);
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string line in FileLines)
                {
                    if (line.StartsWith("M:"))
                        writer.WriteLine("M:" + meter);
                    else
                        writer.WriteLine(line);
                }
            }
        }
    }
}
