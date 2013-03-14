using System;
using System.Collections.Generic;
using System.IO;

namespace EvilHangmanLibrary
{
    public static class Evil
    {
        public static List<string> GetWordsForLength(int Length)
        {
            List<string> returnList = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(@"res\dictionary.txt")))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.Length == Length)
                        {
                            returnList.Add(line);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return returnList;
        }
    }
}