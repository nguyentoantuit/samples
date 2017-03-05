using Clone;
using System;

namespace CloneTest.TestClasses
{
    public class Illness : BasicClone<Illness>
    {
        public DateTime DateOfIllness { get; set; }
        public string IllnessDescription { get; set; }
        public string Treatment { get; set; }
    }
}
