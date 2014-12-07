using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using IniSharp.Exceptions;
using IniSharp.Extension;

namespace IniSharp
{
    internal class IniFile
    {
        // [DllImport("kernel32")] private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        private static string _path;
        private const string CommentChar = ";";

        private static readonly Regex SectionRegex = new Regex(@"\[(.+?)\]");
        private static readonly Regex PairRegex = new Regex(@"^(.+?)=(.*?)$");

        public List<Section> Sections { get; private set; }

        public IniFile(string path)
        {
            Sections = new List<Section>();
            _path = Path.IsPathRooted(path) ? path : Path.Combine(Environment.CurrentDirectory, path);
            if (File.Exists(_path))
            {
                ProcessFile();
            }
            else
            {
               File.Create(_path).Close();
            }
        }

        public void Write(string section, string key, string newValue)
        {
            // WritePrivateProfileString(section, key, newValue, _path);
            var sp = new Stopwatch();sp.Start();
            ProcessFile();
            sp.Stop();Console.Write(sp.Elapsed.ToString());
        }

        private void ProcessFile()
        {
            using (var reader = new StreamReader(_path))
            {
                string line;
                
                var section = new Section();

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(CommentChar)) continue;
                    if (String.IsNullOrEmpty(line)) continue;

                    var isSection = SectionRegex.IsMatch(line);
                    var isPair = PairRegex.IsMatch(line);
                    if (!isSection && !isPair) throw new ParsingException();

                    if (isSection)
                    {
                        if (section.Pairs.Count != 0) Sections.Add(section);
                        section = new Section(SectionRegex.Match(line).Groups[1].Value);
                    }

                    if (isPair)
                    {
                        var pair = new Pair(PairRegex.GetGroupValue(line, 1), PairRegex.GetGroupValue(line, 2));
                        section.Pairs.Add(pair);
                    }

                }
            }
        }
    }
}
