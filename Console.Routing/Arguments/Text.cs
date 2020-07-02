﻿using System.Text.RegularExpressions;

namespace ConsoleRouting
{
    public class Text : IArgument
    {
        public string Value { get; }

        public Text(string value)
        {
            this.Value = value;
        }

        public bool Match(string name)
        {
            return string.Compare(this.Value, name, ignoreCase: true) == 0;
        }

        public bool MatchAssignment(string name) 
        {
            var parts = Value.Split('=');
            return string.Compare(parts[0], name, ignoreCase: true) == 0;
        }

        public bool TryGetAssignment(string name, out Assignment assignment)
        {
            var parts = Value.Split('=');
            bool match = string.Compare(parts[0], name, ignoreCase: true) == 0;
            
            assignment = match 
                ? new Assignment(parts[0], parts[1])
                : Assignment.NotProvided();
            
            return match;
        }

        public bool IsLiteral()
        {
            return Regex.IsMatch(Value, @"^[a-zA-Z]+$");
        }

        public static implicit operator string (Text literal)
        {
            return literal.Value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

    }
}