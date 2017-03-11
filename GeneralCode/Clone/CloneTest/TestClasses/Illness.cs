using Clone;
using System;
using System.Collections.Generic;

namespace CloneTest.TestClasses
{
    public class Illness : BasicClone<Illness>
    {
        public DateTime DateOfIllness { get; set; }
        public string IllnessDescription { get; set; }
        public string Treatment { get; set; }
        public List<DateTime> NextVisit { get; set; }

        public Illness()
        {
            NextVisit = new List<DateTime>();
        }
    }
}
