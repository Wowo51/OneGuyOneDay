using System;
using System.Collections.Generic;

namespace ReteAlgorithm
{
    public class Fact
    {
        public string Identifier { get; set; }
        public Dictionary<string, object> Attributes { get; }

        public Fact(string identifier)
        {
            Identifier = identifier;
            Attributes = new Dictionary<string, object>();
        }
    }
}