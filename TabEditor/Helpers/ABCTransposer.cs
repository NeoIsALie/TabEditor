using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace ABC
{
    public class ABCTransposer
    {
      /*
      Transpose ABC score to higher or lower pitch
      @param originalFile -- path to original file
      @param outputFile -- path to modified File
      @param direction -- number of pitches to transpose to
      */
        public static void Transpose(string originalFile, string outputFile, int direction)
        {

            Regex noterx = new Regex("([a-gA-G]{1})[0-9]?([,`]*)", RegexOptions.Compiled);
            string tempLineValue, newNote = string.Empty, oldNote = string.Empty;
            int currentIndex = 0, sourceIndex = 0;
            using (FileStream inputStream = File.OpenRead(originalFile))
            {
                using (StreamReader inputReader = new StreamReader(inputStream))
                {
                    using (StreamWriter outputWriter = File.AppendText(outputFile))
                    {
                        while((tempLineValue = inputReader.ReadLine()) != null)
                        {
                            if (tempLineValue.StartsWith("K: "))
                            {
                                tempLineValue = changeKey(tempLineValue, direction);
                            }
                            if (tempLineValue.Contains("|"))
                            {
                                System.Diagnostics.Debug.Print(tempLineValue);
                                currentIndex = 0;
                                string[] notes = noterx.GetGroupNames();
                                foreach (string note in notes)
                                {
                                    oldNote = note;
                                    System.Diagnostics.Debug.Print(oldNote);
                                    newNote = GetNewTune(oldNote, direction);
                                    sourceIndex = tempLineValue.IndexOf(oldNote, currentIndex);
                                    tempLineValue.Remove(currentIndex, oldNote.Length).Insert(currentIndex, newNote);
                                    currentIndex += newNote.Length;
                                }
                            }
                            outputWriter.WriteLine(outputFile);
                        }
                    }
                }
            }
        }

        /*
        Change key of score
        @param header -- score header
        @param direction -- number of pitches to transpose to
        @returns transposed key
        */

        public static string changeKey(string header, int direction)
        {
            string keys = "CDEFGAB";
            int index = 0, newindex = 0;
            header.Trim(' ');
            System.Diagnostics.Debug.Print(header);
            System.Diagnostics.Debug.Print(header[header.Length - 1].ToString());
            index = keys.IndexOf(header[header.Length - 1].ToString());
            newindex = index + direction;
            if(newindex > 6)
                newindex -= 6;
            else if(newindex < 0)
                newindex += 6;

            return "K:" + keys[newindex].ToString();
        }

        /*
        Changes tune according to ABC notation rules
        @param note -- note to transpose
        @direction -- number of pitches to transpose to
        */

        public static string GetNewTune(string note, int direction)
        {
            System.Diagnostics.Debug.Print(note);
            Regex octaverx = new Regex("[,']", RegexOptions.Compiled);
            int index = 0, newindex = 0, temp;
            string notes = "CDEFGAB";
            string notesUp = "cdefgab";
            string newNote = "";
            string tempOctave = string.Empty;
            bool toLower = false;

            Match octave = octaverx.Match(note);
            if (octave != null)
            {
                tempOctave = octave.ToString();
            }
            temp = notesUp.IndexOf(note[0].ToString());
            if (temp == -1)
                index = notes.IndexOf(note);
                newindex = index + direction;
                if(newindex > 6)
                    newindex -= 7;
                else if(newindex < 0)
                    newindex += 7;
                newNote = newNote + notesUp.Substring(newindex, 1);
                index = notes.IndexOf(note[0].ToString());
                newindex = index + direction;
                if(newindex > 6)
                {
                    toLower = true;
                    newindex -= 7;
                }
                else
                    newindex += 7;
                if(toLower)
                    newNote = newNote + notesUp.Substring(newindex, 1);
                else
                    newNote = newNote + notesUp.Substring(index, 1);


            if (index == 6)
                newNote += "'";
            else if (index == 0)
                newNote += ",";

            return newNote + tempOctave;
        }

        public static void ChangeKeyABC(string originalfile, string path, int direction)
        {
            string[] FileLines = File.ReadAllLines(originalfile);
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string line in FileLines)
                {
                    if (line.StartsWith("K:"))
                        writer.WriteLine(changeKey(line, direction));
                    else
                        writer.WriteLine(line);
                }
            }
        }
    }
}
