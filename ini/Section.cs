

namespace IniSharp
{

using System;
using System.Collections.Generic;

    class Section
    {
        public string Key { get; set; }
        public List<Pair> Pairs { get; set; }

        public Section()
        {
            Pairs = new List<Pair>();
        }
        public Section(string key) : this()
        {
            Key = key;
        }

        public Section(string key, List<Pair> pairs)
        {
            Key = key;
            Pairs = pairs;
        }

        public override string ToString()
        {
            return String.Format("Key: {0} Pairs: Count = {1}", Key, Pairs.Count);
        }
    }
}
