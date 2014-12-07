
namespace IniSharp
{
using System;

    class Pair
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Pair()
        {
            
        }
        public Pair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("{0}={1}", Key, Value);
        }
    }
}
