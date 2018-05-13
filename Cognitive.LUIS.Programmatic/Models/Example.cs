﻿using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class Example
    {
        public string Text { get; set; }
        public string IntentName { get; set; }
        public IEnumerable<EntityLabel> EntityLabels { get; set; }
    }

    public class EntityLabel
    {
        public string EntityName { get; set; }
        public int StartCharIndex { get; set; }
        public int EndCharIndex { get; set; }
    }

    public class BatchExample
    {
        public Utterance value { get; set; }
        public bool hasError { get; set; }
        public Error error { get; set; }
    }


    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}
